using System;
using UnityEngine;

public class AutoDefenseTakeOver
{
    public bool InTakeOver { get; private set; }
    private bool _enabled;
    public bool Enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            Debug.Log("AutoDefenseTakeOver, Set enable: " + value);
            if (!value)
            {
                InTakeOver = false;
                _uncontrolTime = IM.Number.zero;
                _isControlled = false;
            }
            else
            {
                InTakeOver = true;
                _uncontrolTime = IM.Number.zero;
                _isControlled = false;
            }
        }
    }

    static IM.Number MAX_UNCONTROL_TIME = new IM.Number(0, 500);
    GameMatch _match;
    bool _isControlled;
    IM.Number _uncontrolTime;

    public AutoDefenseTakeOver(GameMatch match)
    {
        _match = match;
        MAX_UNCONTROL_TIME = IM.Number.Parse(GameSystem.Instance.CommonConfig.GetString("gDefenseAIWait"));
    }

    public void SetControlled()
    {
        _isControlled = true;
        _uncontrolTime = IM.Number.zero;
    }

    public void Update(IM.Number deltaTime)
    {
        if (!Enabled)
            return;

        //自动盯防
        if (_match.m_stateMachine.m_curState != null
            && _match.m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
        {
            if (!_isControlled)
                _uncontrolTime = _uncontrolTime + deltaTime;

            if (!InTakeOver && !_isControlled)
            {
                if (_uncontrolTime > MAX_UNCONTROL_TIME)
                {
                    InTakeOver = true;
                    Debug.Log("AutoDefenseTakeOver, Take over.");
                }
            }
            else if (InTakeOver && _isControlled)
            {
                InTakeOver = false;
                Debug.Log("AutoDefenseTakeOver, Resume.");
            }
        }
        else
        {
            _uncontrolTime = IM.Number.zero;
            InTakeOver = false;
        }

        _isControlled = false;
    }
}
