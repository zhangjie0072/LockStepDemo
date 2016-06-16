using UnityEngine;
using System.Collections.Generic;

public class AI_Debug_Init : AIState
{
	public AI_Debug_Init(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eDebug_Init;
	}

    public override void Update(IM.Number fDeltaTime)
	{
        if (m_match.gameMatchTime < IM.Number.one)
            m_system.SetTransaction(AIState.Type.eDebug_Shoot);
	}
}