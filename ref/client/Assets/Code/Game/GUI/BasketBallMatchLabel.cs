using UnityEngine;
using System.Collections;

public class BasketBallMatchLabel : MonoBehaviour {


    private float _speed = 1.0f;

    private float[] _ends = new float[6]{-830,-822,-840,-759,-646,-100};

    private Vector3[] _starts = new Vector3[6]
    {
         new Vector3(351,529,0),
         new Vector3(-58,543,0),
         new Vector3(374,541,0),
         new Vector3(664,539,0),
         new Vector3(648,535,0),
         new Vector3(843,257,0)
    };

    GameObject[] _lablesGo = new GameObject[6];
	// Use this for initialization
	void Start () {

        //for (int i = 0; i < 6; i++ )
        //{
        //    string str=  "Label"+(i+1);
        //    _lablesGo[i] = transform.FindChild(str).gameObject;
        //    _lablesGo[i].SetActive(false);
        //}
    }
	

   
	// Update is called once per frame
	void Update () {


        //for (int i = 0; i < 6; i++ )
        //{
        //    UpdateGO(_lablesGo[i],i);
        //}
	     
	}

    void UpdateGO( GameObject go, int index )
    {
        //go.SetActive(true);
        //float offx = Mathf.Cos(40 * Mathf.Deg2Rad) * _speed;
        //float offy = Mathf.Sin(40 * Mathf.Deg2Rad) * _speed;
        //Vector3 vOff = new Vector3(offx, offy, 0);

        //go.transform.localPosition -= vOff;

        //if( go.transform.localPosition.x < _ends[index])
        //{
        //    go.transform.localPosition = _starts[index];
        //}
       
    }
}
