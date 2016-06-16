using System;
using UnityEngine;

public class UScore
	: MonoBehaviour
{
	private uint m_curScore;
	private UISprite m_S1up, m_S1down, m_S2up, m_S2down;
	private Animator m_uiAnimator;

	void Awake()
	{
		m_S1up = transform.FindChild("S1up").GetComponent<UISprite>();
		m_S1down = transform.FindChild("S1down").GetComponent<UISprite>();
		m_S2up = transform.FindChild("S2up").GetComponent<UISprite>();
		m_S2down = transform.FindChild("S2down").GetComponent<UISprite>();

		m_S1up.spriteName = m_S1down.spriteName = m_S2up.spriteName = m_S2down.spriteName = "0";
		m_uiAnimator = GetComponent<Animator>();
	}
	
	public void SetScore(uint score)
	{
		if( m_curScore == score )
			return;

		m_S2up.spriteName = score.ToString();
		m_S2down.spriteName = score.ToString();
		m_uiAnimator.Play("E_Score");
		m_curScore = score;
	}

	void OnAnimFinish()
	{
		m_S1up.spriteName = m_S1down.spriteName = m_S2up.spriteName = m_S2down.spriteName = m_curScore.ToString();
	}
}

