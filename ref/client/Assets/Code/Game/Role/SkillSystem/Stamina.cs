using UnityEngine;
using System.Collections.Generic;

/**体力*/
public class Stamina
{
    /**常量,体力充分间隔值 70%*/
	public static IM.Number fSufficientValue = new IM.Number(0,700);
    /**常量，体力不足间隔值 35%*/
	public static IM.Number fInsufficientValue = new IM.Number(0,350);

    /**最大体力*/
	public IM.Number		m_maxStamina{get; private set;}
    /**当前体力*/
	public IM.Number		m_curStamina{get; private set;}
    /**可恢复性enable*/
	public bool			m_bEnableRecover = true;

	public IM.Number m_curRatio { get { return m_curStamina / m_maxStamina; } }

	private	bool	m_recover = true;
    /**恢复等待时间*/
	private IM.Number	m_recoverWaitTime = IM.Number.zero;

	private Player	m_owner;

	public Stamina( Player owner, int maxStamina )
	{
		m_owner = owner;
        m_curStamina = m_maxStamina = new IM.Number(maxStamina);
	}

    /**体力恢复*/
	public void RecoverStamina(IM.Number deltaTime)
	{
		if( !m_bEnableRecover )
			return;

		if( !m_recover )
		{
			m_recoverWaitTime += deltaTime;
		}
		else
		{
			IM.Number curStaminaPercentage = m_curStamina / m_maxStamina;
			foreach( KeyValuePair<IM.Number,PhRegainConfig.PhStage> stage in GameSystem.Instance.PhRegainConfig)
			{
				if(stage.Value.value1 <= curStaminaPercentage && curStaminaPercentage < stage.Value.value2)
				{
					m_curStamina += stage.Value.regain * deltaTime;
					break;
				}
			}
			m_curStamina = IM.Math.Clamp(m_curStamina, IM.Number.zero, m_maxStamina);
		}
		
		if( m_recoverWaitTime > IM.Number.one )
		{
			m_recoverWaitTime = IM.Number.zero;
			m_recover = true;
		}
	}
	
    /**消耗体力*/
	public bool	ConsumeStamina( IM.Number value )
	{
        if (IM.Number.Approximately(value,IM.Number.zero)) 
			return false;
        IM.Number imValue = value;
        if (m_curStamina < imValue)
			return false;

        m_curStamina -= imValue;
		m_curStamina = IM.Math.Clamp(m_curStamina, IM.Number.zero, m_maxStamina);
		m_recover = false;
        m_recoverWaitTime = IM.Number.zero;

		return true;
	}
	
    /**体力重置*/
	public void	ResetStamina()
	{
		m_curStamina = m_maxStamina;
	}

}