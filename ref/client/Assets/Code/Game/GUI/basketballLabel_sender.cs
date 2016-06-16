using UnityEngine;
using System.Collections;

public class basketballLabel_sender : MonoBehaviour {


    static private int _num = 4;
    private int _curIndex = 0;
    private GameObject[] _items = new GameObject[_num];


	// Use this for initialization
	void Start () {
	    for( int i = 0; i < _num; i++ )
        {
            _items[i] = GameSystem.Instance.mClient.mUIManager.CreateUI("basketballLabel_subItem", transform);
            _items[i].transform.localPosition = Vector3.zero;
            Vector3 v = new Vector3( i*584, i*0,0);
            _items[i].transform.localPosition += v;

        }
 

	}
	
	// Update is called once per frame
	void Update () {
        int last = _curIndex - 1;
        if (last == -1)
        {
            last = _num-1;
        }
        if(  _items[last].transform.localPosition.x <10 )
        {
            updateNew();
        }

	}

    void updateNew()
    {
        //return;
        int last = _curIndex - 1;
        if( last == -1 )
        {
            last = _num-1;
        }
        Vector3 v = new Vector3( 1*584, 1*0,0);
        _items[_curIndex].transform.localPosition = _items[last].transform.localPosition + v;

        _curIndex++;
        if (_curIndex >= _num)
        {
            _curIndex = 0;
        }
    }
}
