using UnityEngine;
using System.Collections;

public class DBG_1 : MonoBehaviour {

    float speed = 1;
    float posx = 1280;
    // Use this for initialization
    void Start()
    {
        Vector3 pos = transform.localPosition;
        pos.x = 1280;
        transform.localPosition = pos;
    }


    public void set_y(uint y)
    {
        Vector3 pos = transform.localPosition;
        pos.y = y;
        transform.localPosition = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x -= speed;
        if (pos.x <= -1280)
        {
            pos.x = 1280;
        }



        //posx -= speed;
        //if (posx <= -1280)
        //{
        //    posx = 1280;
        //}

        transform.localPosition = pos;
 
    }
}
