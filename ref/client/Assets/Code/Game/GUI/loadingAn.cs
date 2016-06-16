using UnityEngine;
using System.Collections;

public class loadingAn : MonoBehaviour {

    GameObject[] _gos = new GameObject[10];
    Animation[] _animations = new Animation[10];
    Animator[] _animators = new Animator[10];
    uint _index = 0;

	// Use this for initialization
	void Start () {

       

        for( int i =0; i< 10;i++)
        {
            string child = "l" + i;
            _gos[i] = transform.FindChild(child).gameObject;
            _animations[i] = _gos[i].GetComponent<Animation>();
            _animations[i].Stop();

            _gos[i].GetComponent<loadingAnI>()._onFinishAn = FinishAn;
            //_animators[i] = _gos[i].GetComponent<Animator>();
         
        }



        bool isPlayOk = _animations[_index].Play("loading");
        //_animators[_index].Play();
        if (isPlayOk)
        {
            int i = 3;
        }
        else
        {
            int i = 4;
        }
	}
	
    public void FinishAn()
    {
        _animations[_index].Stop();

        _index++;
        if( _index >= 10 )
        {
            _index = 0;
        }
        bool isPlayOk = _animations[_index].Play("loading");

        if (isPlayOk)
        {
            int i = 3;
        }
        else
        {
            int i = 4;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
