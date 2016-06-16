using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class LoseBallEvent
{
	public IM.Number	fTime;
	public string	    strEventName;
	public IM.Number	fEventParam;
};

public class LoseBallSimResult 
{
	public IM.Vector3 	vecPos;
	public IM.Vector3 	vecVel;
	public bool		bStop = false;
	public List<LoseBallEvent>	events = new List<LoseBallEvent>();
};

public class LoseBallSimulator
{
	private static IM.Number m_fBounce = -new IM.Number(0,640);

	private LoseBallSimResult	m_loseBallResult = new LoseBallSimResult();
	private UBasketball 		m_ball;
	private PlayGround			m_ground;

	public LoseBallSimulator(UBasketball ball, PlayGround ground)
	{
		m_ball = ball;
		m_ground = ground;
	}

	public LoseBallSimResult DoSimulate(IM.Vector3 vecStartPos, IM.Vector3 vecStartVel, IM.Number fTimeElapsedPrev, IM.Number fTimeElapsed)
	{
		m_loseBallResult.events.Clear();
		
		IM.Vector3 position, velocity;

		IM.Number fTime = fTimeElapsed;
		while(true)
		{
			position = IM.Number.half * fTime * fTime * IM.Vector3.gravity + fTime * vecStartVel + vecStartPos;
            velocity = fTime * IM.Vector3.gravity + vecStartVel;

			//bounce
			if(position.y <= m_ball.m_ballRadius)
			{
				IM.Number a = IM.Number.half * IM.Vector3.gravity.y;
				IM.Number b = vecStartVel.y;
				IM.Number c = vecStartPos.y - m_ball.m_ballRadius;
				
				IM.Number fTime_1, fTime_2;
				GameUtils.CalcSolutionQuadraticFunc(out fTime_1, out fTime_2, a, b, c);
				
				IM.Number fTimeColl = fTime_1;
				if(fTime_2 <= fTime)
				{
					fTimeColl = fTime_2;
				}
				else
				{
					fTimeColl = fTime_1;
				}
				
				vecStartPos = IM.Number.half * fTimeColl * fTimeColl * IM.Vector3.gravity + fTimeColl * vecStartVel + vecStartPos;
				vecStartVel = fTimeColl * IM.Vector3.gravity + vecStartVel;
				
				if(fTimeElapsed - fTime + fTimeColl > fTimeElapsedPrev)
				{
					LoseBallEvent ballEvent = new LoseBallEvent();
					ballEvent.strEventName = "Coll_Ground";
					ballEvent.fTime = fTimeElapsed - fTime + fTimeColl;
					ballEvent.fEventParam = IM.Math.Abs(vecStartVel.y);
					m_loseBallResult.events.Add(ballEvent);
					m_loseBallResult.bStop = false;
				}
				
				vecStartVel.y = m_fBounce * vecStartVel.y;
				fTime -= fTimeColl;

				if(IM.Math.Abs(vecStartVel.y) <= new IM.Number(0, 001))
				{
					position = vecStartPos;
                    position.y = m_ball.m_ballRadius;
					velocity = IM.Vector3.zero;
					break;
				}
			}
			else
			{
				break;
			}
		}

		m_loseBallResult.vecPos = position;
		m_loseBallResult.vecVel = velocity;

		if( velocity == IM.Vector3.zero && position.y < (m_ball.m_ballRadius + IM.Number.half) )
			m_loseBallResult.bStop = true;
		else
			m_loseBallResult.bStop = false;

		return m_loseBallResult;
	}
};