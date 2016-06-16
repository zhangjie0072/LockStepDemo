using UnityEngine;
using System.Collections;

public class basketballLabel_subItem : MonoBehaviour {


    private float _speed = 1.0f;
    private Transform _transform;


    
    void Awake()
    {
        _transform = transform;
    }
    
	// Use this for initialization
	void Start () {
        //Quaternion rota = new Quaternion();
        //rota.z = 40 * Mathf.Deg2Rad;
        //_transform.localRotation = rota;
	}
	
	// Update is called once per frame
	void Update () {

        float offx = Mathf.Cos(40 * Mathf.Deg2Rad) * _speed;
        float offy = Mathf.Sin(40 * Mathf.Deg2Rad) * _speed;
        Vector3 vOff = new Vector3(_speed, 0, 0);

        _transform.localPosition -= vOff;
	}


    void setPosition( float x, float y )
    {
        Vector3 pos = new Vector3(x, y, 0);

        _transform.localPosition = pos;
    }
}
