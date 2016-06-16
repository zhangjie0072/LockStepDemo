using UnityEngine;
using System.Collections;

[AddComponentMenu("Battle/Effect/CamSlowMotion")]
public class UCamSlowMotion : MonoBehaviour {
	
	public float fDurationSec;
	public float fSlowRate;
	public bool bSmoothFade;
	
	void Start () {
		
		UCamSlowMotionMgr sm = Camera.main.GetComponent<UCamSlowMotionMgr>( );
		if( sm != null )
			sm.SetSlowMotionSimple( fDurationSec, fSlowRate, bSmoothFade );
	}
	
}
