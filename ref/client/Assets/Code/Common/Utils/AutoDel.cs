using UnityEngine;
using System.Collections;

public class AutoDel : MonoBehaviour {
	
	public float m_fDelDelay = 1.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_fDelDelay -= Time.deltaTime;
		if( m_fDelDelay < 0 ){
			GameObject.Destroy(this.gameObject);	
		}
	}
	
	
}
