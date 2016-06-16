using System.Collections;
using UnityEngine;

public class UCamCtrl_Match : MonoBehaviour
{
	public Transform m_trLook;
	
	public Vector3 	m_HalfSizeBound 	=  new Vector3( 1.0f, 1.0f, 1.0f );
	
	public float	m_RadiusThresholdMinV = -6.0f;
	public float	m_RadiusThresholdMaxV = -1.0f;
	
	public float 	m_MaxLookAngleV 	= 40.0f;
	public float	m_MaxDistToTarget	= 10.0f;
	public float	m_MaxAngleToTarget	= 10.0f;
	
	public float 	m_MinLookAngleV 	= 25.0f;
	public float	m_MinDistToTarget 	= 5.0f;
	public float	m_MinAngleToTarget	= 10.0f;
	
	public float	m_MoveSpeedH		= 0.8f;
	
	public UCamShakeMgr m_Shake;
	public UCamZoomMgr	m_Zoom;

	private GameMatch	m_curMatch;
	private PlayGround	m_playGround;
	private UBasket		m_basket;

	void Awake() 
	{
		m_Shake = GetComponent<UCamShakeMgr>();
		m_Zoom	= GetComponent<UCamZoomMgr>();
		m_curMatch = GameSystem.Instance.mClient.mCurMatch;
		m_playGround = m_curMatch.mCurScene.mGround;
		m_basket = m_curMatch.mCurScene.mBasket;
	}
	
	void LateUpdate () 
	{
		if( m_trLook == null || m_curMatch == null || m_basket == null )
			return;
		
		if( m_Zoom == null || !m_Zoom.m_bZoomState )
		{
			float fTargetZPos = Mathf.Clamp( m_trLook.position.z, m_RadiusThresholdMinV, m_RadiusThresholdMaxV );
			
			float fRatioV = (fTargetZPos - m_RadiusThresholdMaxV) / (m_RadiusThresholdMinV - m_RadiusThresholdMaxV);
			fRatioV	= Mathf.Clamp(fRatioV, 0.0f, 1.0f);
			
			float fLookAngleV = 	Mathf.Lerp(m_MinLookAngleV, 	m_MaxLookAngleV, fRatioV);
			float fDistToTarget = 	Mathf.Lerp(m_MinDistToTarget, 	m_MaxDistToTarget, fRatioV);
			float fAngleToTarget = 	Mathf.Lerp(m_MinAngleToTarget, 	m_MaxAngleToTarget, fRatioV);
			
			Vector3 dirLookV = (Quaternion.AngleAxis(fLookAngleV, Vector3.right) * (Vector3.forward) ).normalized;
			Vector3 dirToTargetV = (Quaternion.AngleAxis(fAngleToTarget, Vector3.right) * (Vector3.forward) ).normalized;
			
			Vector3 cameraPosV = m_trLook.position - dirToTargetV * fDistToTarget;
			cameraPosV = new Vector3(0.0f, cameraPosV.y, cameraPosV.z);

            Vector3 vShootTargetPos = m_basket.m_vShootTarget.ToUnity2();
			Vector3 dirLookH = vShootTargetPos - transform.position;
			dirLookH.y = 0.0f;
			dirLookH.Normalize();

			float fAngleH = Vector3.Angle((Vector3.forward), dirLookH);
			if( Vector3.Cross((Vector3.forward), dirLookH).y < 0.0f )
				fAngleH = -fAngleH;
			
			transform.forward = Quaternion.AngleAxis( fAngleH, Vector3.up ) * dirLookV;
			transform.position = _UpdateViewBound(new Vector3(m_trLook.transform.position.x * m_MoveSpeedH, cameraPosV.y, cameraPosV.z));
		}
		else
			m_Zoom.OnUpdate(Time.deltaTime);
		
		Vector3 vShake = m_Shake.GetShakeDelta();
		transform.position += vShake;
	}
	
	Vector3 _UpdateViewBound( Vector3 cameraPos )
	{
		if( m_curMatch.mCurScene.mGround == null )
			return cameraPos; 
		
		PlayGround ground = m_curMatch.mCurScene.mGround;
		return new Vector3(
		Mathf.Clamp(cameraPos.x, m_HalfSizeBound.x - (float)ground.mHalfSize.x, (float)ground.mHalfSize.x - m_HalfSizeBound.x),
			cameraPos.y,
			transform.forward.z > 0.0f? Mathf.Min( cameraPos.z, (float)ground.mHalfSize.y - m_HalfSizeBound.z) : Mathf.Max( cameraPos.z, m_HalfSizeBound.z - (float)ground.mHalfSize.y )
			);
	}
}
