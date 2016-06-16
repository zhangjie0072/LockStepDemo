using UnityEngine;
using System.Collections;

public class UCamSlowMotionMgr : MonoBehaviour {
	
	public class CamSlowMotionNode{
		public float m_fDuration;
		public float m_fSlowRate;
		public CamSlowMotionNode( float fDuration, float fSlowRate ){ m_fDuration = fDuration; m_fSlowRate = fSlowRate;}
	}
	
	public class CamSlowMotionParam{
		public ArrayList m_lstParam = new ArrayList();
	}
	
	CamSlowMotionParam m_Param;
	float m_fRealTimeAtStart = 0;
	
	void Update(){
		if( m_Param == null )return;
		
		float fTimeEllaps = Time.realtimeSinceStartup - m_fRealTimeAtStart;
		float fTimeForNode = 0;
		
		bool bEnd = true;
		foreach( CamSlowMotionNode n in m_Param.m_lstParam ){
			if( fTimeEllaps < fTimeForNode + n.m_fDuration ) {
				bEnd = false;
				Time.timeScale = n.m_fSlowRate;
				return;
			}
			
			fTimeForNode += n.m_fDuration;
		}
		
		if( bEnd ){
			Reset();
		}
	}
	
	public void SetSlowMotion( CamSlowMotionParam p ) {
		m_Param = p;
		m_fRealTimeAtStart = Time.realtimeSinceStartup;
	}
	
	//参数分别是，总时间，初始慢镜头倍率，是否平滑恢复
	public void SetSlowMotionSimple( float fDurationSec, float fSlowRate, bool bSmoothFade ){
		CamSlowMotionParam p = new CamSlowMotionParam();
		if( bSmoothFade == false ){
			p.m_lstParam.Add( new CamSlowMotionNode(fDurationSec, fSlowRate) );
		}
		else{
			const int nSeg = 5;
			for( int i=0; i<nSeg; i++ ){
				float fRate = 1.0f - ( 1.0f - fSlowRate ) * ( nSeg-i ) / nSeg;
				p.m_lstParam.Add( new CamSlowMotionNode(fDurationSec / nSeg, fRate ) );
			}
		}
		
		SetSlowMotion( p );
	}
	
	public void Reset() {
		m_Param = null;
		m_fRealTimeAtStart = 0;
		Time.timeScale = 1.0f;
	}
}
