using UnityEngine;
using System.Collections;

/// <summary>
/// tip挂的脚本
/// 1、点击任何区域关闭tips
/// 2、点击其它区域的按钮，需要执行其它按钮的操作
/// 3、点击tips只关闭按钮，tips下的按钮不执行任何操作
/// </summary>
public class UITips : MonoBehaviour
{
	public static UITips tip;

	// Use this for initialization
	void Start ()
	{
		if (tip != null)
		{
			Destroy (tip.gameObject);
		}
		tip = this;
	}
	
	void OnDestroy()
	{
		//UIEventListener.Get(gameObject).onClick = null;

        if (tip != null && tip == this)
        {
            Destroy(tip.gameObject);
        }
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        if (Input.GetMouseButtonUp(0))
        {
            if (tip != null)
            {
                Destroy(tip.gameObject);
                tip = null;
            }
        }
#elif UNITY_IPHONE || UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (tip != null)
                {
                    Destroy(tip.gameObject);
                    tip = null;
                }
            }
        }
#endif
    }
}

