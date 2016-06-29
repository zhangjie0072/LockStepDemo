using System;
using UnityEngine;
using System.Collections.Generic;

public class UTimeCounter24
	:MonoBehaviour
{
    private bool _stop;

	bool invalidate = true;

	private UISprite	s1, s2;
	private UISprite	s1_e, s2_e;
	void Awake()
	{
		Transform trans;
		trans = GameUtils.FindChildRecursive(transform, "S1");
		if( trans != null )
			s1 = trans.GetComponent<UISprite>();
		
		trans = GameUtils.FindChildRecursive(transform, "S2");
		if( trans != null )
			s2 = trans.GetComponent<UISprite>();

		trans = GameUtils.FindChildRecursive(transform, "S1_E");
		if( trans != null )
		{
			s1_e = trans.GetComponent<UISprite>();
			s1_e.gameObject.SetActive(false);
		}
		
		trans = GameUtils.FindChildRecursive(transform, "S2_E");
		if( trans != null )
		{
			s2_e = trans.GetComponent<UISprite>();
			s2_e.gameObject.SetActive(false);
		}
        invalidate = true;
		_stop = true;
	}

	void _UpdateUIByTime( float time )
	{
		if (!invalidate)
			return;

		int sec = Mathf.Max((int)time % 60, 0);
		int iS1, iS2 = 0;
		iS1 = sec / 10;
		iS2 = sec % 10;
		if( sec < 4 )
		{
			if( s1 != null )
				s1.gameObject.SetActive(false);
			if( s2 != null )
			{
				s2.color = Color.red;
			}

			if( s2_e != null )
			{
				s2_e.gameObject.SetActive(true);

				if( time < 0.0f || _stop )
				{
					s2_e.GetComponent<Animation>().Stop();
					s2_e.gameObject.SetActive(false);
				}
				else
				{
					s2_e.spriteName = "gameInterface_figure_white" + Convert.ToString(iS2);
					s2_e.MakePixelPerfect();
                    s2_e.GetComponent<Animation>()["Counter24"].speed = 4f;
					s2_e.GetComponent<Animation>().Rewind();
					s2_e.GetComponent<Animation>().Play();
				}
			}
		}
		else
		{
			if( s1 != null )
			{
				s1.gameObject.SetActive(true);
				s1.color = Color.white;
			}
			if( s2 != null )
			{
				s2.color = Color.white;
			}

			if( s1_e != null )
				s1_e.gameObject.SetActive(false);
			if( s2_e != null )
				s2_e.gameObject.SetActive(false);
		}

		if( s1 != null )
		{
			s1.spriteName = "gameInterface_figure_white" + Convert.ToString(iS1);
			s1.MakePixelPerfect();
		}
		if( s2 != null )
		{
			s2.spriteName = "gameInterface_figure_white" + Convert.ToString(iS2);
			s2.MakePixelPerfect();
		}

		invalidate = false;
	}

    private float _preTime;
    /**更新时间*/
    public void UpdateTime(float time,bool stop = false,bool invali = false)
    {
        invalidate = invali;
        _stop = stop;
        if (Mathf.FloorToInt(time) != Mathf.FloorToInt(_preTime))
            invalidate = true;
        _preTime = time;
        _UpdateUIByTime(time);
    }

	void FixedUpdate()
	{
        
	}
}
