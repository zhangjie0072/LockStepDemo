using UnityEngine;
using System.Collections;

public class LoginServerPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClose() 
    {
        NGUITools.Destroy(this.gameObject);
    }
}
