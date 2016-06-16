using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class ShootSimulation
	: Singleton<ShootSimulation>
{
	private UBasket			m_basket;
	private UBasketball		m_ball;

	public void Build(UBasket basket, UBasketball ball)
	{
		m_basket = basket;
		m_ball = ball;
	}

	public bool DoSimulate(int sector, IM.Vector3 angleAdjustment, IM.Number speed, ref ShootSolution solution)
	{
		if( sector == -1 )
			return false;

		IM.Vector3 vecInitPos, vecInitVel;
		IM.Vector3 vecFinPos, vecFinVel;
		/*
		if(sector == 0)
		{
			vecInitPos = m_basket.m_rim.center;
			vecInitVel = new IM.Vector3(0.0f, 0.0f, 1.0f);
			
			vecInitVel = Quaternion.AngleAxis(m_fBeta, IM.Vector3.up) * vecInitVel;
			vecInitVel = Quaternion.AngleAxis(m_fAlpha, IM.Vector3.Cross(vecInitVel.normalized, IM.Vector3.up)) * vecInitVel;
			
			vecInitVel.Normalize();
			vecInitVel *= m_fVel_ini;
		}
		else
		*/
		{
			vecInitPos = m_ball.position;
			vecInitVel = m_basket.m_rim.center - vecInitPos;
			vecInitVel.y = IM.Number.zero;
			
			vecInitVel = IM.Quaternion.AngleAxis(angleAdjustment.x, IM.Vector3.up) * vecInitVel;
			vecInitVel = IM.Quaternion.AngleAxis(angleAdjustment.y, IM.Vector3.Cross(vecInitVel.normalized, IM.Vector3.up)) * vecInitVel;
			
			vecInitVel.Normalize();
			vecInitVel *= speed;
		}
		solution.m_vInitPos = vecInitPos;
		solution.m_vInitVel = vecInitVel;
		solution.m_vFinPos  = vecInitPos;
		solution.m_vFinVel  = vecInitVel;
		solution.m_vStartPos = vecInitPos;
		
		_Simulating(vecInitPos, vecInitVel, ref solution);
		
		vecInitPos = solution.m_vInitPos;
		vecFinPos = solution.m_vFinPos;
		vecFinVel = solution.m_vFinVel;

		return true;
	}

	bool _Simulating(IM.Vector3 vecInitPos, IM.Vector3 vecInitVel, ref ShootSolution solution)
	{
		solution.ClearCurve();

		ShootSolution.SShootCurve shootCurve = new ShootSolution.SShootCurve();
		int iBoundCount = 0;
		bool bSuccess = false;
		while(true)
		{
			IM.Vector3 vecFinVel;
			
			IM.Number fTime_Rim = IM.Number.zero;
			IM.Number fTime_BackBoard = IM.Number.zero;
			IM.Number fTime_Ground = IM.Number.zero;

			CalculateShootCurve(ref shootCurve, vecInitPos, vecInitVel);
			if(!_CheckCollision_Rim(shootCurve, ref fTime_Rim) || fTime_Rim < new IM.Number(0, 100))
				fTime_Rim = new IM.Number(10000);
			if(!_CheckCollision_BackBoard(shootCurve, ref fTime_BackBoard) || fTime_BackBoard < new IM.Number(0, 100))
				fTime_BackBoard = new IM.Number(10000);
			if(!_CheckCollision_Ground(shootCurve, ref fTime_Ground))
				fTime_Ground = new IM.Number(10000);

			//collide rim
			if(fTime_Rim < fTime_BackBoard && fTime_Rim < fTime_Ground)
			{
				shootCurve.fTime = fTime_Rim;
				_DoCollision_Rim(solution.m_vBounceRimAdjustment, shootCurve, fTime_Rim, ref vecInitPos, ref vecInitVel);
				
				if(_CheckShootSuccess(shootCurve, ref fTime_Rim))		
					bSuccess = true;
				
				iBoundCount ++;
				solution.AddCurve(shootCurve.fTime, shootCurve.fX_a, shootCurve.fX_b, shootCurve.fX_c,
				                         shootCurve.fY_a, shootCurve.fY_b, shootCurve.fY_c, shootCurve.fZ_a, shootCurve.fZ_b, shootCurve.fZ_c);
				
				solution.m_vFinPos = vecInitPos;
				solution.m_vFinVel = vecInitVel;
			}
			//collide backboard
			else if(fTime_BackBoard < fTime_Rim && fTime_BackBoard < fTime_Ground)
			{
				shootCurve.fTime = fTime_BackBoard;
				
				iBoundCount ++;
				_DoCollision_BackBoard(solution.m_fBounceBackboard, shootCurve, fTime_BackBoard, ref vecInitPos, ref vecInitVel);
				
				if(_CheckShootSuccess(shootCurve, ref fTime_BackBoard))		
					bSuccess = true;
				
				solution.AddCurve(shootCurve.fTime, shootCurve.fX_a, shootCurve.fX_b, shootCurve.fX_c,
				                         shootCurve.fY_a, shootCurve.fY_b, shootCurve.fY_c, shootCurve.fZ_a, shootCurve.fZ_b, shootCurve.fZ_c);
				
				solution.m_vFinPos = vecInitPos;
				solution.m_vFinVel = vecInitVel;
			}
			else
			{
				shootCurve.fTime = fTime_Ground;
				IM.Number fTime_Goal = IM.Number.zero;
				if(_CheckShootSuccess(shootCurve, ref fTime_Goal))
				{
					Logger.Log("goal success");

					bSuccess = true;
					solution.AddCurve(fTime_Goal, shootCurve.fX_a, shootCurve.fX_b, shootCurve.fX_c,
					                         shootCurve.fY_a, shootCurve.fY_b, shootCurve.fY_c, shootCurve.fZ_a, shootCurve.fZ_b, shootCurve.fZ_c);

					//goal in rim
					vecFinVel = solution.m_vFinVel;
					vecFinVel.x *= new IM.Number(0, 100);
					vecFinVel.y *= new IM.Number(0, 200);
					vecFinVel.z *= new IM.Number(0, 100);
					
					solution.m_vFinVel = vecFinVel;
				}
				else
				{
					bSuccess = false;
					solution.AddCurve(fTime_Ground, shootCurve.fX_a, shootCurve.fX_b, shootCurve.fX_c,
					                         shootCurve.fY_a, shootCurve.fY_b, shootCurve.fY_c, shootCurve.fZ_a, shootCurve.fZ_b, shootCurve.fZ_c);
				}
				/*
				else if(iBoundCount > 0)
				{
					bSuccess = false;

					vecFinVel = solution.m_vFinVel;
					vecFinVel.x *= 0.5f;
					vecFinVel.y *= 1.3f;
					vecFinVel.z *= 0.5f;
					
					solution.m_vFinVel = vecFinVel;
					solution.AddCurve(fTime_Ground, shootCurve.fX_a, shootCurve.fX_b, shootCurve.fX_c,
					                      shootCurve.fY_a, shootCurve.fY_b, shootCurve.fY_c, shootCurve.fZ_a, shootCurve.fZ_b, shootCurve.fZ_c);
				}
				*/
				
				solution.m_bSuccess = bSuccess;
				break;
			}
			
			if(iBoundCount > 100)
			{
				Logger.Log("Invalid shoot solution: too many bounds.");
				//solution.ClearCurve();
				return false;
			}

			if( shootCurve.fTime > new IM.Number(1000))
				return false;
		}
		return true;
	}

	public void CalculateShootCurve(ref ShootSolution.SShootCurve curve, IM.Vector3 vecInitPos, IM.Vector3 vecInitVel)
	{
		curve.fX_a = IM.Number.zero;
		curve.fX_b = vecInitVel.x;
		curve.fX_c = vecInitPos.x;

        curve.fY_a = IM.Number.half * IM.Vector3.gravity.y;
		curve.fY_b = vecInitVel.y;
		curve.fY_c = vecInitPos.y;

		curve.fZ_a = IM.Number.zero;
		curve.fZ_b = vecInitVel.z;
		curve.fZ_c = vecInitPos.z;
	}
	
	bool _CheckCollision_Rim(ShootSolution.SShootCurve curve, ref IM.Number fTime)
	{
		IM.Number fIncTime = new IM.Number(0, 001);
		IM.Number fTimeCur = IM.Number.zero;
		
		IM.Vector3 vecBallPos;

		int deathLoopGuarder = 0;
		while(true)
		{
			deathLoopGuarder++;
			if( deathLoopGuarder > 20000 )
				return false;

			fTimeCur += fIncTime;
			
			vecBallPos.x = curve.fX_a * fTimeCur * fTimeCur + curve.fX_b * fTimeCur + curve.fX_c;
			vecBallPos.y = curve.fY_a * fTimeCur * fTimeCur + curve.fY_b * fTimeCur + curve.fY_c;
			vecBallPos.z = curve.fZ_a * fTimeCur * fTimeCur + curve.fZ_b * fTimeCur + curve.fZ_c;

			if(vecBallPos.y < IM.Number.zero)
				return false;
			if(IM.Math.Abs(vecBallPos.y - m_basket.m_rim.center.y) > m_ball.m_ballRadius)
				continue;
			if((vecBallPos - m_basket.m_rim.center).magnitude > (m_ball.m_ballRadius + m_basket.m_rim.radius))
				continue;
			
			IM.Vector3 vecRim2Ball = vecBallPos - m_basket.m_rim.center;
			IM.Vector3 vecRim2BallHoriz = vecRim2Ball;
			vecRim2BallHoriz.y = IM.Number.zero;
			
			IM.Number fRadiusRim = IM.Math.Sqrt(m_ball.m_ballRadius * m_ball.m_ballRadius - vecRim2Ball.y * vecRim2Ball.y); 
			IM.Number fDistance2D = vecRim2BallHoriz.magnitude;
			
			if(fDistance2D > m_ball.m_ballRadius)
			{
				if(fDistance2D < fRadiusRim + m_basket.m_rim.radius) 
				{
					fTime = fTimeCur;
					return true;
				}
			}
			else
			{
				if(fDistance2D + fRadiusRim > m_basket.m_rim.radius)
				{
					fTime = fTimeCur;
					return true;
				}
			}
		}
		
		return false;
	}
	
	bool _CheckCollision_BackBoard(ShootSolution.SShootCurve curve, ref IM.Number fTime)
	{
		IM.Number a = curve.fZ_a;
		IM.Number b = curve.fZ_b;
		IM.Number c = curve.fZ_c - m_basket.m_backboard.center.z + m_ball.m_ballRadius;
		
		if(a == IM.Number.zero)
		{
			fTime = -c / b;
			if(b <= IM.Number.zero)		
				return false;
		}
		else
		{
            IM.Number fTime_1 = (-b - IM.Math.Sqrt(b * b - new IM.Number(4) * a * c)) / IM.Number.two / a;
            IM.Number fTime_2 = (-b + IM.Math.Sqrt(b * b - new IM.Number(4) * a * c)) / IM.Number.two / a;
			
			if(fTime_1 > fTime_2)
			{
				IM.Number fTemp = fTime_2;
				fTime_2 = fTime_1;
				fTime_1 = fTemp;
			}
			
			if(fTime_1 > IM.Number.zero)		
			{
				fTime = fTime_1;
			}
			else if(fTime_2 > IM.Number.zero)
			{
				fTime = fTime_2;
			}
			else	
				return false;
		}
		
		IM.Vector3 vecBallPos = new IM.Vector3(
			curve.fX_a * fTime * fTime + curve.fX_b * fTime + curve.fX_c,
			curve.fY_a * fTime * fTime + curve.fY_b * fTime + curve.fY_c,
			curve.fZ_a * fTime * fTime + curve.fZ_b * fTime + curve.fZ_c);
		
		if(IM.Math.Abs(vecBallPos.x - m_basket.m_backboard.center.x) < m_basket.m_backboard.width / IM.Number.two &&
		   IM.Math.Abs(vecBallPos.y - m_basket.m_backboard.center.y) < m_basket.m_backboard.height / IM.Number.two)
		{
			return true;
		}
		
		return false;
	}
	
	bool _CheckCollision_Ground(ShootSolution.SShootCurve curve, ref IM.Number fTime)
	{
		IM.Number a = curve.fY_a;
		IM.Number b = curve.fY_b;
		IM.Number c = curve.fY_c - m_ball.m_ballRadius;

        IM.Number fTime_1 = (-b - IM.Math.Sqrt(b * b - new IM.Number(4) * a * c)) / IM.Number.two / a;
        IM.Number fTime_2 = (-b + IM.Math.Sqrt(b * b - new IM.Number(4) * a * c)) / IM.Number.two / a;

		if(fTime_1 > fTime_2)
		{
			IM.Number fTemp = fTime_2;
			fTime_2 = fTime_1;
			fTime_1 = fTemp;
		}
		
		if(fTime_1 > IM.Number.zero)		
		{
			fTime = fTime_1;
			return true;
		}
		else if(fTime_2 > IM.Number.zero)
		{
			fTime = fTime_2;
			return true;
		}
		
		return false;
	}
	
	void _DoCollision_Rim( IM.Vector3 vBounceRim, ShootSolution.SShootCurve shootCurve, IM.Number fTime, ref IM.Vector3 vecResultPos, ref IM.Vector3 vecResultVel)
	{
		fTime -= new IM.Number(0, 001);
		IM.Vector3 vecBallPos = new IM.Vector3(
		shootCurve.fX_a * fTime * fTime + shootCurve.fX_b * fTime + shootCurve.fX_c,
		shootCurve.fY_a * fTime * fTime + shootCurve.fY_b * fTime + shootCurve.fY_c,
		shootCurve.fZ_a * fTime * fTime + shootCurve.fZ_b * fTime + shootCurve.fZ_c);
		fTime += new IM.Number(0, 001);

		IM.Vector3 vecBallVel = new IM.Vector3(
		IM.Number.two * shootCurve.fX_a * fTime + shootCurve.fX_b,
		IM.Number.two * shootCurve.fY_a * fTime + shootCurve.fY_b,
		IM.Number.two * shootCurve.fZ_a * fTime + shootCurve.fZ_b);
		
		IM.Vector3 vecRim2Ball = vecBallPos - m_basket.m_rim.center;
		IM.Vector3 vecRim2BallHori = vecRim2Ball;
		vecRim2BallHori.y = IM.Number.zero;
		vecRim2BallHori.Normalize();

        IM.Vector3 vecContactPos = m_basket.m_rim.center + m_basket.m_rim.radius * vecRim2BallHori;
		IM.Vector3 vecForce = vecBallPos - vecContactPos;
		vecForce.Normalize();
		/*
		if(vecForce.y > 0.7f)
		{
			vecForce.y = 1.0f;
			vecForce.x = vecForce.z = IM.Number.zero;
		}
		*/
		IM.Number fDotProd = IM.Number.Raw(IM.Vector3.Dot(vecBallVel, vecForce));
		vecResultVel = vecBallVel -  vecForce * fDotProd;

		vecResultVel.x *= vBounceRim.x;
		vecResultVel.y *= vBounceRim.y;
		vecResultVel.z *= vBounceRim.z;

		vecResultPos = vecBallPos;
	}
	
	
	void _DoCollision_BackBoard(IM.Number fBounceBackboard, ShootSolution.SShootCurve shootCurve, IM.Number fTime, ref IM.Vector3 vecResultPos, ref IM.Vector3 vecResultVel)
	{
		IM.Vector3 vecBallPos = new IM.Vector3(
			shootCurve.fX_a * fTime * fTime + shootCurve.fX_b * fTime + shootCurve.fX_c,
			shootCurve.fY_a * fTime * fTime + shootCurve.fY_b * fTime + shootCurve.fY_c,
			shootCurve.fZ_a * fTime * fTime + shootCurve.fZ_b * fTime + shootCurve.fZ_c);

		IM.Vector3 vecBallVel = new IM.Vector3(
			vecBallVel.x = IM.Number.two * shootCurve.fX_a * fTime + shootCurve.fX_b,
			vecBallVel.y = IM.Number.two * shootCurve.fY_a * fTime + shootCurve.fY_b,
			vecBallVel.z = IM.Number.two * shootCurve.fZ_a * fTime + shootCurve.fZ_b);
		
		//vecBallVel.x = -0.7f * vecBallVel.x;
		vecBallVel.z = -fBounceBackboard * vecBallVel.z;
		
		vecResultPos = vecBallPos;
		vecResultVel = vecBallVel;
	}

	
	bool _CheckShootSuccess(ShootSolution.SShootCurve shootCurve, ref IM.Number fTime)
	{
		IM.Number a = shootCurve.fY_a;
		IM.Number b = shootCurve.fY_b;
		IM.Number c = shootCurve.fY_c - m_basket.m_rim.center.y;

        if (b * b - new IM.Number(4) * a * c < IM.Number.zero)
            return false;

        IM.Number fTime_1 = (-b - IM.Math.Sqrt(b * b - new IM.Number(4) * a * c)) / IM.Number.two / a;
        IM.Number fTime_2 = (-b + IM.Math.Sqrt(b * b - new IM.Number(4) * a * c)) / IM.Number.two / a;
		
		if(fTime_1 > fTime_2)
		{
			IM.Number fTemp = fTime_1;
			fTime_1 = fTime_2;
			fTime_2 = fTemp;
		}
		
		if(fTime_2 >= IM.Number.zero && fTime_2 < shootCurve.fTime)		
		{
			fTime = fTime_2;
		}
		else if(fTime_1 >= IM.Number.zero && fTime_1 < shootCurve.fTime)
		{
			fTime = fTime_1;
		}
		else 
			return false;
		
		IM.Vector3 vecBallPos = new IM.Vector3(
			shootCurve.fX_a * fTime * fTime + shootCurve.fX_b * fTime + shootCurve.fX_c,
			shootCurve.fY_a * fTime * fTime + shootCurve.fY_b * fTime + shootCurve.fY_c,
			shootCurve.fZ_a * fTime * fTime + shootCurve.fZ_b * fTime + shootCurve.fZ_c);
		
		IM.Number fEpsilon = new IM.Number(0, 010);
		IM.Number fDistRim2Ball = (vecBallPos - m_basket.m_rim.center).magnitude;
		if(fDistRim2Ball < (m_basket.m_rim.radius - m_ball.m_ballRadius + fEpsilon))
		{
			IM.Number speedY = IM.Number.two * shootCurve.fY_a * fTime + shootCurve.fY_b;
			if(speedY < IM.Number.zero)
				return true;
		}
		
		return false;
	}

};

