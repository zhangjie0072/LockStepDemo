using UnityEngine;
using System.Collections;

public class UCamShakeMgr : MonoBehaviour {
	
	//配置
	public float m_fListenInnerDist = 2;
	public float m_fListenMaxDistance = 20;	//当Shake发生时，如果它的距离过远
	
	//shake相关
	Vector3 m_vShakeDirNormalized = Vector3.zero;
	float m_fShakeMagnitute = 0;
	float m_fShakeDuration = 0;
	//当前时间
	float m_fShakeCurTime = 0;

	[HideInInspector]
	public UCamCtrl_MatchNew m_CamCtrl;
	
	// Use this for initialization
	void Start () {
		m_CamCtrl = GetComponent<UCamCtrl_MatchNew>();
	}
	
	// Update is called once per frame
	void Update () {
		m_fShakeCurTime += Time.deltaTime;
	}
	
	public void AddCamShake( Vector3 vShake, float fDurationSec, Vector3 vSrcPosition ) 
	{
		float fMagnitudeByDist = 1.0f;
		{
			//根据与摄像机Lookat的距离，求振幅的衰减
			Vector3 vDeltaPos = Vector3.zero;
			if( m_CamCtrl.m_trLook != null )
				vDeltaPos =  m_CamCtrl.m_trLook.position - vSrcPosition;
			
			float fDist = vDeltaPos.magnitude;
			if( fDist > m_fListenMaxDistance )return;
			
			if( fDist > m_fListenInnerDist && fDist < m_fListenMaxDistance ){
				fMagnitudeByDist = 1- (fDist - m_fListenInnerDist) / (m_fListenMaxDistance - m_fListenInnerDist );	
			}	
		}
		
		
		//need to normalize shake dir
		float fNewShakeMagnitude = vShake.magnitude;
		Vector3 vShakeNormalize = vShake / fNewShakeMagnitude;
		fNewShakeMagnitude *= fMagnitudeByDist;
		
		//如果新的振幅大于当前振幅，则设置震动，否则忽略本次输入
		float fCurShakeMag = _GetCurMagnitute();
		if( fNewShakeMagnitude > fCurShakeMag ){
			m_fShakeCurTime = 0;
			m_vShakeDirNormalized = vShakeNormalize;
			m_fShakeMagnitute = fNewShakeMagnitude;
			m_fShakeDuration = fDurationSec;
		}
	}
	
	float _GetCurMagnitute(){
		if( m_fShakeDuration <= 0 )return 0;
		if( m_fShakeCurTime > m_fShakeDuration )return 0;
		return m_fShakeMagnitute * ( m_fShakeDuration - m_fShakeCurTime ) / m_fShakeDuration;
	}
	
	public Vector3 GetShakeDelta(){
		float fCurMag = _GetCurMagnitute();
		return (Random.value * 2 - 1) * fCurMag * m_vShakeDirNormalized;
	}
}
