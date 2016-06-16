using UnityEngine;
using System.Collections;

public class WeTestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if USE_WETEST
		this.gameObject.AddComponent<WeTest.U3DAutomation.U3DAutomationBehaviour>();
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
