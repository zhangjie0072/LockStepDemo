using UnityEngine;
using System.Collections;

public class TimerBoard : MonoBehaviour
{
	public string spritePrefix1 = "gameInterface_figure_white";
	public string spritePrefix2 = "gameInterface_figure_red";

    private bool _isVisible = true;
    private float _preTime = 0.0f;
	public bool isChronograph;
	public bool backgroundVisible { set { NGUITools.SetActive(background.gameObject, value); } }

    //public delegate void Delegate();
    //public Delegate onTimeup;

	bool invalidate = true;

	UISprite minutesTens;
	UISprite minutesUnits;
	UISprite colon;
	UISprite secondsTens;
	UISprite secondsUnits;
	UISprite background;
	GameObject infinity;

	void Awake()
	{
		minutesTens = transform.FindChild("MinutesTens").GetComponent<UISprite>();
		minutesUnits = transform.FindChild("MinutesUnits").GetComponent<UISprite>();
		colon = transform.FindChild("Colon").GetComponent<UISprite>();
		secondsTens = transform.FindChild("SecondsTens").GetComponent<UISprite>();
		secondsUnits = transform.FindChild("SecondsUnits").GetComponent<UISprite>();
		background = transform.FindChild("BG").GetComponent<UISprite>();
		infinity = transform.FindChild("Infinity").gameObject;
	}

	void Start()
	{
        
	}


	void FixedUpdate()
	{
        
	}

    /**更新时间*/
    public void UpdateTime(float time)
    {
        if (float.IsPositiveInfinity(time))
            return;
        bool sameBefore = Mathf.FloorToInt(_preTime) == Mathf.FloorToInt(time);
        _preTime = time;
        bool showCentiSec = time < 10 && isChronograph;
        if (showCentiSec || !sameBefore)
            invalidate = true;

        if (!invalidate)
            return;

        string spritePrefix;
        if (time < 10)
            spritePrefix = spritePrefix2;
        else
            spritePrefix = spritePrefix1;
        uint seconds = (uint)time % 60;
        uint minutes = (uint)time / 60;
        uint centiseconds = (uint)(time * 100) % 60;
        uint[] digits = CommonFunction.GetDigits(showCentiSec ? centiseconds : seconds, 2);
        secondsUnits.spriteName = spritePrefix + digits[0];
        secondsTens.spriteName = spritePrefix + digits[1];
        colon.spriteName = spritePrefix + "colon";
        digits = CommonFunction.GetDigits(showCentiSec ? seconds : minutes, 2);
        minutesUnits.spriteName = spritePrefix + digits[0];
        minutesTens.spriteName = spritePrefix + digits[1];

        invalidate = false;
    }

    /**设置面板时间的是否显示
     * b == true :显示
     * b == false:不显示
    */
    public void SetVisible(bool b)
    {
        _isVisible = b;
        NGUITools.SetActive(minutesTens.gameObject, b);
		NGUITools.SetActive(minutesUnits.gameObject, b);
		NGUITools.SetActive(colon.gameObject, b);
		NGUITools.SetActive(secondsTens.gameObject, b);
		NGUITools.SetActive(secondsUnits.gameObject, b);
		NGUITools.SetActive(infinity.gameObject, !b);
    }
}
