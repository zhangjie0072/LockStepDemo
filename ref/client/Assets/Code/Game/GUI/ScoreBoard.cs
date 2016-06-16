using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class ScoreBoard : MonoBehaviour
{
	public string spritePrefix = "gameInterface_figure_black";

	public int winScore;
	public int curScore;
	public int digitCount = 0;
	public bool isLeft;
	public bool bgVisible;
	public int gap = 0;

	UIWidget widget;
	UISprite bg;
	List<UISprite> digitSprites = new List<UISprite>();

	void Awake()
	{
		widget = GetComponent<UIWidget>();
		bg = transform.FindChild("BG").GetComponent<UISprite>();
	}

	void Start()
	{
		NGUITools.SetActive(bg.gameObject, bgVisible);
		widget.rawPivot = isLeft ? UIWidget.Pivot.Right : UIWidget.Pivot.Left;
	}

	void Update()
	{
		List<string> suffixes = new List<string>();
		if (winScore > 0)
		{
			uint[] digits = CommonFunction.GetDigits((uint)winScore, (uint)digitCount);
			foreach (uint d in digits)
			{
				suffixes.Add(d.ToString());
			}
			suffixes.Add("Sprit");
		}
		{
			uint[] digits = CommonFunction.GetDigits((uint)curScore, (uint)digitCount);
			foreach (uint d in digits)
			{
				suffixes.Add(d.ToString());
			}
		}
		if (!isLeft)
			suffixes.Reverse();
		float x = (isLeft ? -1 : 1) * 8f;
		int maxHeight = 0;
		for (int i = 0; i < suffixes.Count; ++i)
		{
			UISprite sprite = GetDigitSprite(suffixes[i], i);
			sprite.rawPivot = isLeft ? UIWidget.Pivot.Right : UIWidget.Pivot.Left;
			sprite.transform.localPosition = new Vector3(x, 0f, 0f);
			x += (sprite.width + gap) * (isLeft ? -1 : 1);
			maxHeight = Mathf.Max(maxHeight, sprite.height + 16);
		}
		x += (8f - gap) * (isLeft ? -1 : 1);
		widget.width = (int)Mathf.Abs(x);
		widget.height = maxHeight;
	}

	UISprite GetDigitSprite(string suffix, int index)
	{
		UISprite sprite;
		if (index >= digitSprites.Count)
		{
			sprite = NGUITools.AddSprite(gameObject, bg.atlas, spritePrefix + suffix);
			digitSprites.Add(sprite);
		}
		else
		{
			sprite = digitSprites[index];
		}
		sprite.spriteName = spritePrefix + suffix;
		sprite.MakePixelPerfect();
		return sprite;
	}
}
