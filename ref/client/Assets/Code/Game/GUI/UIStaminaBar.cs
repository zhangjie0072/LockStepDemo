using System;
using UnityEngine;

public class UIStaminaBar : MonoBehaviour
{
	[HideInInspector]
	public Player	m_attachedPlayer;
	private UIProgressBar	m_progBar;

	private Color32	r = new Color32(0xdf, 0x06, 0x00, 0xff);
	private Color32	y = new Color32(0xfb, 0xc4, 0x34, 0xff);
	private Color32	g = new Color32(0x78, 0xda, 0x13, 0xff);

	void Awake()
	{
		m_progBar = GameUtils.FindChildRecursive(transform, "Bar").GetComponent<UIProgressBar>();
	}
	

	void Update()
	{
		m_progBar.value = (float)m_attachedPlayer.m_stamina.m_curStamina / (float)m_attachedPlayer.m_stamina.m_maxStamina;

		if( m_progBar.value > (float)Stamina.fSufficientValue)
			m_progBar.foregroundWidget.color = g;
		else if( m_progBar.value > (float)Stamina.fInsufficientValue)
			m_progBar.foregroundWidget.color = y;
		else
			m_progBar.foregroundWidget.color = r;
	}
}
