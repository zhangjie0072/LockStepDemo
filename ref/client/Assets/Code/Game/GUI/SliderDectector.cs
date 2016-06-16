using UnityEngine;
using System.Collections;

public class SliderDectector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
    
    public int adjust = 200;
    Vector2 first, second;

    public delegate void LeftDlg();
    public delegate void RightDlg();

    public LeftDlg leftDlg;
    public RightDlg rightDlg;


    public void OnGUI()
    {
	if (Event.current.type == EventType.MouseDown)
        {
            first = Event.current.mousePosition;
        }

        if (Event.current.type == EventType.MouseDrag)
        {

            second = Event.current.mousePosition;

            if (first.x - second.x > adjust)
            {
                if (leftDlg != null )
                {
                    leftDlg();
                }
               
            }

            if (second.x - first.x > adjust)
            {
                if (rightDlg != null )
                {
                    rightDlg();

                }
                first = second;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
