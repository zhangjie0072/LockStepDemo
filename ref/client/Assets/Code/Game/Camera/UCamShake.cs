using UnityEngine;
using System.Collections;

public class UCamShake : MonoBehaviour {
	
	//配置
	public Vector3 m_vMagnitude = Vector3.one;
	public float m_fDurationSec = 1.0f;
	
	// Use this for initialization
	void Start () {
		
		UCamShakeMgr sm = Camera.main.GetComponent<UCamShakeMgr>( );
		if( sm != null )
			sm.AddCamShake( m_vMagnitude, m_fDurationSec, transform.position );
	}
	
}
