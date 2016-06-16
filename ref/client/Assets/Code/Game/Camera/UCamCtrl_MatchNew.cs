using System.Collections;
using UnityEngine;

public class UCamCtrl_MatchNew : MonoBehaviour
{
	public Transform m_trLook;
	
	public Vector3 	m_HalfSizeBound = new Vector3( 1.0f, 1.0f, 1.0f );

	public float	m_CamHeightThreshold = 1.0f;

	public float	m_RadiusThresholdMinV = 1.0f;
	public float	m_RadiusThresholdMaxV = 6.0f;
	
	public float 	m_MaxLookAngleV 	= 40.0f;
	public float	m_MaxDistToTarget	= 10.0f;
	
	public float 	m_MinLookAngleV 	= 25.0f;
	public float	m_MinDistToTarget 	= 5.0f;

	public float 	m_MinBallEffectHeight 	= 3.7f;
	public float	m_MaxBallEffectHeight 	= 4.5f;
	
	[HideInInspector]
	public bool 	m_UseSwitchSpeed;

	public float	m_heightAdjust = 4.0f;

	public Vector3 	m_CloseUpRestoreSpeed = new Vector3(0.5f, 0.5f, 0.5f);
	public Vector3	m_moveSpeed = new Vector3(0.4f, 0.4f, 0.4f);
	public Vector3	m_switchRoleSpeed = new Vector3(0.5f, 0.5f, 0.5f);

	[HideInInspector]
	public bool m_Staying = true;
	[HideInInspector]
	public bool m_PositionImmediately = false;

	public UCamShakeMgr m_Shake;
	public UCamZoomMgr	m_Zoom;

	private GameMatch	m_curMatch;
	private PlayGround	m_playGround;
	private UBasketball m_focusBall;

	private Vector3		m_curSwitchSpeed;
	private Vector3		m_curSpeed;

	private float		m_fCurRotateSpeedX;
	private float		m_fCurRotateSpeedY;

	void Awake() 
	{
		m_Shake = GetComponent<UCamShakeMgr>();
		if( m_Shake == null )
			m_Shake = gameObject.AddComponent<UCamShakeMgr>();

		m_Zoom	= GetComponent<UCamZoomMgr>();
		if( m_Zoom == null )
			m_Zoom = gameObject.AddComponent<UCamZoomMgr>();

		m_curMatch = GameSystem.Instance.mClient.mCurMatch;
	}
	
	void LateUpdate () 
	{
		if( m_curMatch == null || m_curMatch.m_stateMachine == null || m_curMatch.m_stateMachine.m_curState == null )
			return;

		MatchState.State curState = m_curMatch.m_stateMachine.m_curState.m_eState;

		if( curState == MatchState.State.eOpening
		   || curState == MatchState.State.eOver )
			return;

		/*
		if( m_PositionImmediately )
		{
			Positioning(true);
			m_PositionImmediately = false;
		}
		else
			Positioning(false);
			*/
        if (!m_PositionImmediately && !Mathf.Approximately(Time.deltaTime, 0f))
			Positioning(false);
	}

	public void Positioning(bool instantly = false)
	{
		if( m_trLook == null || m_curMatch == null )
			return;

		if( m_playGround == null )
			m_playGround = m_curMatch.mCurScene.mGround;
		if( m_focusBall == null )
			m_focusBall = m_curMatch.mCurScene.mBall;

		float fTargetZPos = Mathf.Clamp( m_trLook.position.z, m_RadiusThresholdMinV, m_RadiusThresholdMaxV );
		float fRatioV = (fTargetZPos - m_RadiusThresholdMinV) / (m_RadiusThresholdMaxV - m_RadiusThresholdMinV);
		fRatioV	= Mathf.Clamp(fRatioV, 0.0f, 1.0f);
		
		float fLookAngleV = Mathf.Lerp(m_MinLookAngleV, m_MaxLookAngleV, fRatioV);
		float fHeightAdjust = 0.0f;
		if( m_focusBall != null && !m_Zoom.m_bZoomState )
		{
			if( m_focusBall.m_owner == null && m_focusBall.m_ballState != BallState.eUseBall_Pass )
			{
				float fDelta = m_focusBall.transform.position.y - m_MinBallEffectHeight;
				if( fDelta > 0.0f )
					fHeightAdjust = fDelta / (m_MaxBallEffectHeight - m_MinBallEffectHeight) * m_heightAdjust;
			}
		}
		
		float fDistToTarget = 	Mathf.Lerp(m_MinDistToTarget, 	m_MaxDistToTarget, fRatioV);
		
		Vector3 dirLookV = (Quaternion.AngleAxis(fLookAngleV, Vector3.right) * (Vector3.forward)).normalized;
		Vector3 cameraPosV = m_trLook.position - dirLookV * fDistToTarget;
		cameraPosV.y += m_CamHeightThreshold + fHeightAdjust;
		Vector3 logicCameraPos = _UpdateViewBound(new Vector3(m_trLook.transform.position.x, cameraPosV.y, cameraPosV.z));

		m_Staying = true;

		Vector3 speed = m_UseSwitchSpeed ? m_switchRoleSpeed : m_moveSpeed;
		if( m_Zoom == null || !m_Zoom.m_bZoomState )
		{
			float fAngleToTargetX = Vector3.Angle(Vector3.forward, dirLookV);
			float fCurAngleX = transform.localEulerAngles.x;
			float fTotalDeltaAngleX = fAngleToTargetX - fCurAngleX;
			if (!instantly)
			{				
				fAngleToTargetX = Mathf.SmoothDampAngle(fCurAngleX, fAngleToTargetX, ref m_fCurRotateSpeedX, speed.x);
			}

			float fAngleToTargetY = 0f;
			float fCurAngleY = transform.localEulerAngles.y;
			if (!instantly)
			{
				float maxAngleDeltaY = 1f;
				if (!Mathf.Approximately(fAngleToTargetX, fCurAngleX) && !Mathf.Approximately(fTotalDeltaAngleX, 0f) )
				{
					float angleDeltaRatio = (fAngleToTargetX - fCurAngleX) / fTotalDeltaAngleX;
					maxAngleDeltaY = Mathf.Abs(fAngleToTargetY - fCurAngleY) * angleDeltaRatio;
				}
				fAngleToTargetY = Mathf.MoveTowardsAngle(fCurAngleY, fAngleToTargetY, maxAngleDeltaY);
				//fAngleToTargetY = Mathf.SmoothDampAngle(fCurAngleY, fAngleToTargetY, ref m_fCurRotateSpeedY, speed.z);
			}

			transform.rotation = Quaternion.Euler(fAngleToTargetX, fAngleToTargetY, 0.0f);

			if (!instantly)
			{
				if( GameUtils.HorizonalDistance(transform.position, logicCameraPos) > 0.01f )
					m_Staying = false;

				if( m_UseSwitchSpeed )
				{
                    transform.position = new Vector3(
                    Mathf.SmoothDamp(transform.position.x, logicCameraPos.x, ref m_curSwitchSpeed.x, speed.x),
                    Mathf.SmoothDamp(transform.position.y, logicCameraPos.y, ref m_curSwitchSpeed.y, speed.y),
                    Mathf.SmoothDamp(transform.position.z, logicCameraPos.z, ref m_curSwitchSpeed.z, speed.z) );
				}
				else
				{
                    transform.position = new Vector3(
                    Mathf.SmoothDamp(transform.position.x, logicCameraPos.x, ref m_curSpeed.x, speed.x),
                    Mathf.SmoothDamp(transform.position.y, logicCameraPos.y, ref m_curSpeed.y, speed.y),
                    Mathf.SmoothDamp(transform.position.z, logicCameraPos.z, ref m_curSpeed.z, speed.z) );
				}
			}
			else
			{
				transform.position = logicCameraPos;
			}
		}
		else if (!instantly)
			m_Zoom.OnUpdate(Time.deltaTime);

		if (!instantly)
		{
			Vector3 vShake = m_Shake.GetShakeDelta();
			transform.position += vShake;
		}
	}

    void OnPause()
    {
        m_curSpeed = Vector3.zero;
        m_curSwitchSpeed = Vector3.zero;
    }
	
	Vector3 _UpdateViewBound( Vector3 cameraPos )
	{
		if( m_curMatch.mCurScene.mGround == null )
			return cameraPos; 
		
		PlayGround ground = m_curMatch.mCurScene.mGround;
		return new Vector3(
		Mathf.Clamp(cameraPos.x, m_HalfSizeBound.x - (float)ground.mHalfSize.x, (float)ground.mHalfSize.x - m_HalfSizeBound.x),
			cameraPos.y,
			transform.forward.z > 0.0f? Mathf.Min( cameraPos.z, (float)ground.mHalfSize.y - m_HalfSizeBound.z) : Mathf.Max( cameraPos.z, m_HalfSizeBound.z - (float)ground.mHalfSize.y)
			);
	}
}
