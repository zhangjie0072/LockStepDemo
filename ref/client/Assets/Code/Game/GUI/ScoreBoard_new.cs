using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class ScoreBoard_new : MonoBehaviour
{
	public string spritePrefix = "gameInterface_figure_black";

	public int winScore = 0;
	public int gap = 0;
	
	public int digitCount = 2;
	public bool isLeft;
	private int m_curScore = 0;

	private UIWidget widget;
	private List<UScore> m_digits = new List<UScore>();
	private int m_scoreWidth = 0;

	void Awake()
	{
		widget = GetComponent<UIWidget>();
	}

	public void Initialize()
	{
		GameObject resDigits = ResourceLoadManager.Instance.GetResources("Prefab/GUI/E_Score") as GameObject;
		for( int idx = 0; idx != digitCount; idx++ )
		{
			GameObject goDigit = NGUITools.AddChild(gameObject, resDigits);
			m_digits.Add(goDigit.GetComponent<UScore>());
			UISprite digit = goDigit.GetComponentInChildren<UISprite>();
			m_scoreWidth = digit.width;
		}
		if( digitCount == 2 )
		{
			Vector3 target = m_digits[0].transform.localPosition;
			target.x += m_scoreWidth * 0.5f;
			TweenPosition.Begin(m_digits[0].gameObject, 0.3f, target);
			
			target = m_digits[1].transform.localPosition;
			target.x -= m_scoreWidth * 0.5f;
			TweenPosition.Begin(m_digits[1].gameObject, 0.3f, target);
		}
		else if( digitCount == 3 )
		{
			Vector3 target = m_digits[0].transform.localPosition;
			target.x += m_scoreWidth;
			TweenPosition.Begin(m_digits[0].gameObject, 0.3f, target);
			
			target = m_digits[1].transform.localPosition;
			target.x -= m_scoreWidth;
			TweenPosition.Begin(m_digits[2].gameObject, 0.3f, target);
		}
	}

	public void Refresh(int uScore)
	{
		if( m_curScore == uScore )
			return;
		m_curScore = uScore;
		uint[] digits = CommonFunction.GetDigits((uint)uScore, (uint)digitCount);
		for(int idx = 0; idx != digits.Length; idx++)
			m_digits[idx].SetScore(digits[idx]);
	}
}
