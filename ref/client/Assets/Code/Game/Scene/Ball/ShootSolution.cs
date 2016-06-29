using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class ShootSolution
{
	// t = elapsed time from the beginning of the curve
	// x(t) = fX_a * t^2 + fX_b * t + fX_c
	// y(t) = fY_a * t^2 + fY_b * t + fY_c
	// z(t) = fZ_a * t^2 + fZ_b * t + fZ_c
	public struct SShootCurve : System.IEquatable<SShootCurve>
	{
		public IM.Number fTime;
		public IM.Number fX_a, fX_b, fX_c;
		public IM.Number fY_a, fY_b, fY_c;
		public IM.Number fZ_a, fZ_b, fZ_c;

		public IM.Vector3 GetPosition(IM.Number fEclipseTime)
		{
			if( fEclipseTime < IM.Number.zero)
				fEclipseTime = IM.Number.zero;

			if( fEclipseTime > fTime )
				fEclipseTime = fTime;

			IM.Vector3 vecBallPos = new IM.Vector3(
				fX_a * fEclipseTime * fEclipseTime + fX_b * fEclipseTime + fX_c,
				fY_a * fEclipseTime * fEclipseTime + fY_b * fEclipseTime + fY_c,
				fZ_a * fEclipseTime * fEclipseTime + fZ_b * fEclipseTime + fZ_c );

			return vecBallPos;
		}

		public IM.Number GetHighestTime()
		{
			return -IM.Number.half * fY_b / fY_a;
		}

		public IM.Vector3 GetHighestPosition()
		{
			return GetPosition(GetHighestTime());
		}

		public void GetTimeByHeight( IM.Number height, out IM.Number time1, out IM.Number time2 )
		{
			IM.Number fValue = fY_b * fY_b - new IM.Number(4) * fY_a * (fY_c - height);
			if( IM.Math.Abs(fValue) < new IM.Number(0, 001))
			{
				IM.Number time = -fY_b / IM.Number.two / fY_a;
                time1 = time;
                time2 = time;
                return;
			}

			if( fValue < IM.Number.zero)
            {
                time1 = -IM.Number.one;
                time2 = -IM.Number.one;
                return;
            }

			IM.Number t1 = (-fY_b - IM.Math.Sqrt(fValue)) / IM.Number.two / fY_a;
			IM.Number t2 = (-fY_b + IM.Math.Sqrt(fValue)) / IM.Number.two / fY_a;

			time1 = IM.Math.Min( t1, t2 );
			time2 = IM.Math.Max( t1, t2 );
		}

        public bool Equals(SShootCurve other)
        {
            return fTime.Equals(other.fTime) &&
                fX_a.Equals(other.fX_a) && fX_b.Equals(other.fX_b) && fX_c.Equals(other.fX_c) &&
                fY_a.Equals(other.fY_a) && fY_b.Equals(other.fY_b) && fY_c.Equals(other.fY_c) &&
                fZ_a.Equals(other.fZ_a) && fZ_b.Equals(other.fZ_b) && fZ_c.Equals(other.fZ_c);
        }
    };

    public struct CircleCurve
    {
        public float R;
        public float x0;
        public float z0;
    }

    public struct LineCurve
    {
        public IM.Vector3 x0;
        public IM.Vector3 preVec;
        public IM.Vector3 dir;
        public IM.Number midDis;
        public IM.Number mTime;
        public int count;
        public bool GetPosition(IM.Number time, out IM.Vector3 res)
        {
            int index = 1;
            if (count % 2 == 0)
                index = 1;
            else
                index = -1;
            UBasket basket = GameSystem.Instance.mClient.mCurMatch.mCurScene.mBasket;
            IM.Number radius = GameSystem.Instance.mClient.mCurMatch.mCurScene.mBall.m_ballRadius;

            res.y = new IM.Number(3, 100);
            res.x = x0.x + 2 * dir.x * time * index;
            res.z = x0.z + 2 * dir.z * time * index;
           
            if (count < 4)
            {
                if (IsKnock(preVec, res))
                {                
                    ++count;
                    x0 = preVec;
                }
                else
                {
                    preVec = res;
                }
                return true;
            }
                               
            return false;
        }

        private bool IsKnock(IM.Vector3 pre, IM.Vector3 now)
        {
            UBasket basket = GameSystem.Instance.mClient.mCurMatch.mCurScene.mBasket;
            IM.Number radius = GameSystem.Instance.mClient.mCurMatch.mCurScene.mBall.m_ballRadius;  

            IM.Number d1 = basket.m_rim.radius - (IM.Vector3.Distance(pre, basket.m_rim.center)) - radius;
            IM.Number d2 = basket.m_rim.radius - (IM.Vector3.Distance(now, basket.m_rim.center)) - radius;
            if (d1 >= 0 && d2 < 0)
                return true;
            return false;
        }
    }

	public int	m_id;
	public enum Type
	{
		Shoot,
		Layup,
		Dunk,
	}
     public  enum AnimationType{
        none,
        ballCircle,
        ballBounce,
        ballUpDown,
    } 

	public IM.Number m_fTime;
	public bool m_bSuccess;
	public bool m_bCleanShot;
	public Type m_type;
	public IM.Number m_fMaxHeight;
    public AnimationType m_animationType;
    public IM.Number m_playTime;
    public IM.Number m_playSpeed;
    public IM.Number m_reductionIndex;

	public IM.Vector3 m_vInitPos;
	public IM.Vector3 m_vInitVel;
	public IM.Vector3 m_vFinPos;
	public IM.Vector3 m_vFinVel;
	public IM.Vector3 m_vStartPos;

	public IM.Vector3	m_vBounceRimAdjustment;
	public IM.Number	m_fBounceBackboard;
    public bool m_isLock;
	public List<SShootCurve> m_ShootCurveList = new List<SShootCurve>();
	
	public ShootSolution(int id)
	{
		m_fTime = IM.Number.zero;
		m_id = id;
	}

	public ShootSolution Clone()
	{
		ShootSolution solution = new ShootSolution(m_id);
		solution.m_fTime = m_fTime;
		solution.m_bSuccess = m_bSuccess;
		
		solution.m_vInitPos = m_vInitPos;
		solution.m_vInitVel = m_vInitVel;
		solution.m_vFinPos = m_vFinPos;
		solution.m_vFinVel = m_vFinVel;
		solution.m_vStartPos = m_vStartPos;
		
		solution.m_vBounceRimAdjustment = m_vBounceRimAdjustment;
		solution.m_fBounceBackboard = m_fBounceBackboard;

		solution.m_ShootCurveList = new List<SShootCurve>(m_ShootCurveList.ToArray());
		return solution;
	}

	public bool Create( XmlElement solutionNode, bool bSuccess, bool bForEditor = false )
	{
		ClearCurve();
		m_bSuccess = bSuccess;
		
		XmlElement node = solutionNode.SelectSingleNode("./init/position") as XmlElement;
		if(node == null)
			return false;
		m_vInitPos = IM.Vector3.Parse(node.InnerText);

		node = solutionNode.SelectSingleNode("./init/velocity") as XmlElement;
		if(node == null)
			return false;
		m_vInitVel = IM.Vector3.Parse(node.InnerText);

		node = solutionNode.SelectSingleNode("./init/bounce_rim") as XmlElement;
		if(node != null)
			m_vBounceRimAdjustment = IM.Vector3.Parse(node.InnerText);

		node = solutionNode.SelectSingleNode("./init/bounce_backboard") as XmlElement;
		if(node != null)
			m_fBounceBackboard = IM.Number.Parse(node.InnerText);

		node = solutionNode.SelectSingleNode("./time") as XmlElement;
		if(node == null)
			return false;
		m_fTime = IM.Number.Parse(node.InnerText);

		node = solutionNode.SelectSingleNode("./clean_shot") as XmlElement;
		if (node != null)
			m_bCleanShot = bool.Parse(node.InnerText);

		node = solutionNode.SelectSingleNode("./type") as XmlElement;
		if (node != null)
			m_type = (Type)(int.Parse(node.InnerText));

		node = solutionNode.SelectSingleNode("./max_height") as XmlElement;
		if (node != null)
			m_fMaxHeight = IM.Number.Parse(node.InnerText);

        node = solutionNode.SelectSingleNode("./animationType") as XmlElement;
        if (node != null)
            m_animationType = (AnimationType)(int.Parse(node.InnerText));
        node = solutionNode.SelectSingleNode("./playTime") as XmlElement;
        if (node != null)
            m_playTime = IM.Number.Parse(node.InnerText);
        node = solutionNode.SelectSingleNode("./playSpeed") as XmlElement;
        if (node != null)
            m_playSpeed = IM.Number.Parse(node.InnerText);
        node = solutionNode.SelectSingleNode("./redunctionIndex") as XmlElement;
        if (node != null)
            m_reductionIndex = IM.Number.Parse(node.InnerText);
        node = solutionNode.SelectSingleNode("./isLock") as XmlElement;
        if (node != null)
            m_isLock = bool.Parse(node.InnerText);
		XmlNodeList curveNodes = solutionNode.SelectNodes("./curve");
		foreach( XmlElement curveNode in curveNodes )
		{
			ShootSolution.SShootCurve shootCurve = new ShootSolution.SShootCurve();
			string[] values = curveNode.InnerText.Split(' ');
			shootCurve.fTime = IM.Number.Parse(values[0]);

			shootCurve.fX_a = IM.Number.Parse(values[1]);
			shootCurve.fX_b = IM.Number.Parse(values[2]);
			shootCurve.fX_c = IM.Number.Parse(values[3]);

			shootCurve.fY_a = IM.Number.Parse(values[4]);
			shootCurve.fY_b = IM.Number.Parse(values[5]);
			shootCurve.fY_c = IM.Number.Parse(values[6]);

			shootCurve.fZ_a = IM.Number.Parse(values[7]);
			shootCurve.fZ_b = IM.Number.Parse(values[8]);
			shootCurve.fZ_c = IM.Number.Parse(values[9]);

			m_ShootCurveList.Add(shootCurve);
		}

		node = solutionNode.SelectSingleNode("./fin/position") as XmlElement ;
		if( node == null )
			return false;
		m_vFinPos = IM.Vector3.Parse(node.InnerText);

		node = solutionNode.SelectSingleNode("./fin/velocity") as XmlElement ;
		if(node == null)
			return false;
		m_vFinVel = IM.Vector3.Parse(node.InnerText);

		int cnt = m_ShootCurveList.Count;
		if(!bForEditor && !bSuccess && cnt >= 2)
		{
			ShootSolution.SShootCurve lastCurve = m_ShootCurveList[cnt - 1];

			m_vFinPos.x = lastCurve.fX_c;
			m_vFinPos.y = lastCurve.fY_c;
			m_vFinPos.z = lastCurve.fZ_c;
			
			m_vFinVel.x = lastCurve.fX_b;
			m_vFinVel.y = lastCurve.fY_b;
			m_vFinVel.z = lastCurve.fZ_b;

			m_fTime -= lastCurve.fTime;

			m_ShootCurveList.Remove(lastCurve);
		}

		return true;
	}

	public bool Save(XmlDocument pDoc, XmlElement pSolution)
	{
		XmlNode pNodeInit = pDoc.CreateElement("init");
		XmlNode pNodePosition = pDoc.CreateElement("position");
		XmlNode pNodeVelocity = pDoc.CreateElement("velocity");
		XmlNode pNodeBounceRim = pDoc.CreateElement("bounce_rim");
		XmlNode pNodeBounceBackboard = pDoc.CreateElement("bounce_backboard");

		pSolution.AppendChild(pNodeInit);
		pNodeInit.AppendChild(pNodePosition);
		pNodeInit.AppendChild(pNodeVelocity);
		pNodeInit.AppendChild(pNodeBounceRim);
		pNodeInit.AppendChild(pNodeBounceBackboard);

		string text = string.Format("{0:f3} {1:f3} {2:f3}", m_vInitPos.x, m_vInitPos.y, m_vInitPos.z);
		pNodePosition.AppendChild(pDoc.CreateTextNode(text));
		text = string.Format("{0:f3} {1:f3} {2:f3}", m_vInitVel.x, m_vInitVel.y, m_vInitVel.z);
		pNodeVelocity.AppendChild(pDoc.CreateTextNode(text));
		text = string.Format("{0:f3} {1:f3} {2:f3}", m_vBounceRimAdjustment.x, m_vBounceRimAdjustment.y, m_vBounceRimAdjustment.z);
		pNodeBounceRim.AppendChild(pDoc.CreateTextNode(text));
		text = string.Format("{0:f3}", m_fBounceBackboard);
		pNodeBounceBackboard.AppendChild(pDoc.CreateTextNode(text));
		
		XmlNode pNodeTime = pDoc.CreateElement("time");
		pSolution.AppendChild(pNodeTime);
		pNodeTime.AppendChild(pDoc.CreateTextNode(m_fTime.ToString()));
		XmlNode pNodeCleanShot = pDoc.CreateElement("clean_shot");
		pSolution.AppendChild(pNodeCleanShot);
		pNodeCleanShot.AppendChild(pDoc.CreateTextNode(m_bCleanShot.ToString()));
		XmlNode pNodeType = pDoc.CreateElement("type");
		pSolution.AppendChild(pNodeType);
		pNodeType.AppendChild(pDoc.CreateTextNode(((int)m_type).ToString()));
		XmlNode pNodeMaxHeight = pDoc.CreateElement("max_height");
		pSolution.AppendChild(pNodeMaxHeight);
		IM.Number fMaxHeight = m_ShootCurveList[m_ShootCurveList.Count - 1].GetHighestPosition().y;
		pNodeMaxHeight.AppendChild(pDoc.CreateTextNode(fMaxHeight.ToString()));

		//for editor, don't save the last one.
		XmlNode pNodeCurve;
		for( int idx = 0; idx != m_ShootCurveList.Count; idx++ )
		{
			ShootSolution.SShootCurve curve = m_ShootCurveList[idx];
			string value = string.Format("{0:f3} {1:f3} {2:f3} {3:f3} {4:f3} {5:f3} {6:f3} {7:f3} {8:f3} {9:f3}", 
			              curve.fTime, curve.fX_a, curve.fX_b, curve.fX_c,
			              curve.fY_a, curve.fY_b, curve.fY_c, 
			              curve.fZ_a, curve.fZ_b, curve.fZ_c);

			pNodeCurve = pDoc.CreateElement("curve");
			pSolution.AppendChild(pNodeCurve);
			pNodeCurve.AppendChild(pDoc.CreateTextNode(value));
		}
		
		XmlNode pNodeFin = pDoc.CreateElement("fin");
		pNodePosition 	= pDoc.CreateElement("position");
		pNodeVelocity 	= pDoc.CreateElement("velocity");

		pSolution.AppendChild(pNodeFin);
		pNodeFin.AppendChild(pNodePosition);
		pNodeFin.AppendChild(pNodeVelocity);

		text = string.Format("{0:f3} {1:f3} {2:f3}", m_vFinPos.x, m_vFinPos.y, m_vFinPos.z);
		pNodePosition.AppendChild(pDoc.CreateTextNode(text));

		text = string.Format("{0:f3} {1:f3} {2:f3}", m_vFinVel.x, m_vFinVel.y, m_vFinVel.z);
		pNodeVelocity.AppendChild(pDoc.CreateTextNode(text));

        XmlNode animationClip = pDoc.CreateElement("animationType");
        XmlNode animationTime = pDoc.CreateElement("playTime");
        XmlNode animationSpeed = pDoc.CreateElement("playSpeed");
        XmlNode reductionIndex = pDoc.CreateElement("redunctionIndex");
        XmlNode isLock = pDoc.CreateElement("isLock");

        pSolution.AppendChild(animationClip);
        animationClip.AppendChild(pDoc.CreateTextNode(((int)m_animationType).ToString()));

        pSolution.AppendChild(animationTime);
        animationTime.AppendChild(pDoc.CreateTextNode(m_playTime.ToString()));

        pSolution.AppendChild(animationSpeed);
        animationSpeed.AppendChild(pDoc.CreateTextNode(m_playSpeed.ToString()));

        pSolution.AppendChild(reductionIndex);
        reductionIndex.AppendChild(pDoc.CreateTextNode(m_reductionIndex.ToString()));

        pSolution.AppendChild(isLock);
        isLock.AppendChild(pDoc.CreateTextNode(m_isLock.ToString()));


		return true;
	}

	public void ClearCurve()
	{
		m_ShootCurveList.Clear();
		m_fTime = IM.Number.zero;
	}

	public void AddCurve(IM.Number fTime, IM.Number fX_a, IM.Number fX_b, IM.Number fX_c, IM.Number fY_a, IM.Number fY_b, IM.Number fY_c, IM.Number fZ_a, IM.Number fZ_b, IM.Number fZ_c)
	{
		SShootCurve ShootCurve = new SShootCurve();
		ShootCurve.fTime = fTime;
		ShootCurve.fX_a = fX_a;
		ShootCurve.fX_b = fX_b;
		ShootCurve.fX_c = fX_c;
		ShootCurve.fY_a = fY_a;
		ShootCurve.fY_b = fY_b;
		ShootCurve.fY_c = fY_c;
		ShootCurve.fZ_a = fZ_a;
		ShootCurve.fZ_b = fZ_b;
		ShootCurve.fZ_c = fZ_c;
		
		m_ShootCurveList.Add(ShootCurve);
		m_fTime += fTime;

		IM.Vector3 vFinPos = new IM.Vector3();
		IM.Vector3 vFinVel = new IM.Vector3();
		
		vFinPos.x = fX_a * fTime * fTime + fX_b * fTime + fX_c;
		vFinPos.y = fY_a * fTime * fTime + fY_b * fTime + fY_c;
		vFinPos.z = fZ_a * fTime * fTime + fZ_b * fTime + fZ_c;
		
		vFinVel.x = IM.Number.two * fX_a * fTime + fX_b;
		vFinVel.y = IM.Number.two * fY_a * fTime + fY_b;
		vFinVel.z = IM.Number.two * fZ_a * fTime + fZ_b;

		m_vFinPos = vFinPos;
		m_vFinVel = vFinVel;
	}

	public bool GetEvent(out BallEvent eventId, out Vector3 vecDeltaV, UBasketball ball, UBasket basket, IM.Number fTimeStart, IM.Number fTimeEnd, int iIter)
	{
		eventId = BallEvent.eNone;
		vecDeltaV = new Vector3();

		if(fTimeStart > m_fTime)
		{
			Debug.LogError("ball shoot solution start time > current time.");
			return false;
		}

		if( m_bSuccess //&& (iCurveEvent == m_ShootCurveList.Count - 1))
		   && fTimeEnd > m_fTime )
		{
			eventId = BallEvent.eGoal;
			return true;
		}

		IM.Number fTime = IM.Number.zero, fTimePrev = -new IM.Number(0, 001);
		SShootCurve pCurve;

		int iCurve, iCurveStart, iCurveEvent, iCurveEnd;
		iCurve = 0;

		iCurveStart = iCurveEvent = iCurveEnd = m_ShootCurveList.Count;

		foreach(SShootCurve curve in m_ShootCurveList)
		{
			pCurve = curve;
			fTime += pCurve.fTime;
			
			if(fTime >= fTimeStart && fTimePrev <= fTimeStart)	
			{
				iCurveStart = iCurve;
			}
			
			if(fTime > fTimeEnd && fTimePrev <= fTimeEnd)
			{
				iCurveEnd = iCurve;
				break;
			}
			
			iCurve++;
			fTimePrev = fTime;
		}

		if(iIter < iCurveEnd - iCurveStart)
		{
			iCurveEvent = iCurveStart + iIter;
			pCurve = m_ShootCurveList[iCurveEvent];

			IM.Vector3 vecInitVel 	= new IM.Vector3();
			IM.Vector3 vecFinVel 	= new IM.Vector3();
			IM.Vector3 vecPos 		= new IM.Vector3();
			
			vecPos.x = pCurve.fX_a * pCurve.fTime * pCurve.fTime + pCurve.fX_b * pCurve.fTime + pCurve.fX_c;
			vecPos.y = pCurve.fY_a * pCurve.fTime * pCurve.fTime + pCurve.fY_b * pCurve.fTime + pCurve.fY_c;
			vecPos.z = pCurve.fZ_a * pCurve.fTime * pCurve.fTime + pCurve.fZ_b * pCurve.fTime + pCurve.fZ_c;
			
			vecInitVel.x = IM.Number.two * pCurve.fX_a * pCurve.fTime + pCurve.fX_b;
			vecInitVel.y = IM.Number.two * pCurve.fY_a * pCurve.fTime + pCurve.fY_b;
			vecInitVel.z = IM.Number.two * pCurve.fZ_a * pCurve.fTime + pCurve.fZ_b;
			
			if(iCurveEvent + 1 >= m_ShootCurveList.Count)
			{
				vecFinVel = m_vFinVel;
			}
			else
			{
				SShootCurve pCurveNext = m_ShootCurveList[iCurveEvent+1];
				vecFinVel.x = pCurveNext.fX_b;
				vecFinVel.y = pCurveNext.fY_b;
				vecFinVel.z = pCurveNext.fZ_b;
			}
			
			vecDeltaV = (Vector3)(vecFinVel - vecInitVel);

			if(IM.Math.Abs(vecPos.z - (basket.m_backboard.center.z - ball.m_ballRadius)) < new IM.Number(0, 020))
			{
				eventId = BallEvent.eCollBoard;
			}
			else
			{
				eventId = BallEvent.eCollRim;
			}
			return true;
		}
		else 
		{
			return false;
		}
	}

	public bool GetPosition(IM.Number fTimeElapsed, out IM.Vector3 vPos)
	{
		vPos = new IM.Vector3();
		if(m_ShootCurveList.Count == 0)
			return false;

		if( fTimeElapsed > m_fTime )
			return false;

		IM.Number fTime = IM.Number.zero;
		IM.Number fTimePrev = IM.Number.zero;

		//foreach(SShootCurve curve in m_ShootCurveList)
        for (int i = 0; i < m_ShootCurveList.Count; ++i)
        {
            SShootCurve curve = m_ShootCurveList[i];
            fTime += curve.fTime;

            if (fTime >= fTimeElapsed)
            {
                IM.Number fTimeCurve = fTimeElapsed - fTimePrev;

                vPos.x = curve.fX_a * fTimeCurve * fTimeCurve + curve.fX_b * fTimeCurve + curve.fX_c;
                vPos.y = curve.fY_a * fTimeCurve * fTimeCurve + curve.fY_b * fTimeCurve + curve.fY_c;
                vPos.z = curve.fZ_a * fTimeCurve * fTimeCurve + curve.fZ_b * fTimeCurve + curve.fZ_c;

                if (i == 0)     // µÚÒ»ÌõÇúÏßÐèÒªÐÞ¸ÄÆðÊ¼Î»ÖÃ
                {
                    //IM.Number fY = vPos.y;
                    IM.Number fAlpha = fTimeCurve / curve.fTime;
                    vPos += (m_vStartPos - m_vInitPos) * (IM.Number.one - fAlpha);

                    /*
                    float fTime_Ymax = -curve.fY_b / 2.0f / curve.fY_a;
                    float fAlpha_Y = fTimeCurve / fTime_Ymax;
                    if(fAlpha_Y > 1.0f)
                        vPos.y = fY;
                    else
                        vPos.y = fY + (m_vStartPos.y - m_vInitPos.y) * (1.0f - fAlpha_Y);
                        */
                }
                return true;
            }
            fTimePrev = fTime;
        }

		/*
		if( fTimeElapsed < m_fTime )
		{
			ShootSolution.SShootCurve curve = new ShootSolution.SShootCurve();
			ShootSimulation.Instance.CalculateShootCurve(ref curve, m_vFinPos, m_vFinVel);
			vPos = curve.GetPosition(fTimeElapsed - fTimePrev);
			return true;
		}
		*/

		return false;
	}

    public IM.Number CalcReductionIndex(IM.Vector3 pos, Type type)
    {
        IM.Number dis = IM.Number.zero;
        IM.Number index = IM.Number.one;
        dis = (pos.magnitude - new IM.Number(1, 900) < 1) ? IM.Number.one : (pos.magnitude - new IM.Number(1, 900));
        if (type == Type.Layup)
            index = dis / 100 * dis * new IM.Number(0, 200);
        else if (type == Type.Shoot)
            index = dis / 100 * dis * new IM.Number(0, 500);
        else
            index = IM.Number.one;
        return index;
    }
};