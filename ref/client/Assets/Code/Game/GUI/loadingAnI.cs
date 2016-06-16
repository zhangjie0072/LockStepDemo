using UnityEngine;
using System.Collections;

public class loadingAnI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public delegate void onFinishDelegate();
    public onFinishDelegate _onFinishAn;


    public void FinishAn()
    {
       // Logger.LogError("FinishAn");
        if( _onFinishAn != null )
        {
            _onFinishAn();
        }
    }
      
	
	// Update is called once per frame
	void Update () {
	
	}
}
