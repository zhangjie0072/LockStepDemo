using UnityEngine;

public class MatchState: PlayerActionEventHandler.Listener 
{
	public enum State
	{
		eNone 					= 0,
		eOpening				= 1,//开场秀
		eBegin 					= 2,//3秒倒计时的阶段
		ePlaying 				= 3,//比赛中
		eGoal 					= 4,//得分
		eFoul 					= 5,//犯规（目前只存在24秒犯规）
		eOver 					= 6,//比赛结束
        eTipOff 				= 7,//跳球
        ePlotBegin 				= 8,//开始剧情对话
        ePlotEnd 				= 9,//结否时的剧情对话
		eShowRule 				= 10,//显示规则
		eSlotMachineUltimate21 	= 11,//老虎机（极限21分）
		eSlotMachineBullFight 	= 12,//老虎机（斗牛）
		eShowSkillGuide 		= 13,//显示技能引导
		eFreeThrowStart 		= 14,//自由上篮
		ePlayerCloseUp 			= 15,//显示技能引导
		eOverTime				= 21,//加时

		eMax 					= 25,
	}
	public State	m_eState{ get; protected set;}
	public GameMatch m_match{ get; protected set;}

	protected MatchStateMachine m_stateMachine;

	public MatchState (MatchStateMachine owner)
	{
		m_stateMachine = owner;
		m_eState = State.eNone;
		m_match = m_stateMachine.m_owner;
	}
	
    /**从一种状态到另一个状态*/
	virtual public void OnEnter ( MatchState lastState )
	{
	}

    /**动作上的事件触发执行*/
	virtual public void OnEvent (PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
        //Debug.Log("match state on event:" + animEvent);
        if (animEvent == PlayerActionEventHandler.AnimEvent.ePass)
        {
            UBasketball ball = m_match.mCurScene.mBall;
            if (ball.m_interceptor == null)
                ball.m_catcher.m_bToCatch = true;
            PlaySoundManager.Instance.PlaySound(MatchSoundEvent.PassBall);
        }
	}

    //逻辑层
	virtual public void GameUpdate(IM.Number fDeltaTime)
	{
        //设置主角脚下环颜色
        SetRoleCirleColor();
	}

    //显示层
    virtual public void ViewUpdate(float deltaTime)
    {

    }

	virtual public void OnExit ()
	{
	}

	virtual public bool IsCommandValid(Command command)
	{
		return true;
	}

    protected void SetRoleCirleColor()
    {
        if (m_match.mainRole != null)
        {
            if (m_match.mCurScene.mGround.In3PointRange(m_match.mainRole.position.xz, IM.Number.zero))
            {
                Color yellow = new Color(1f, 252f / 255, 10f / 255, 1);
                m_match.mainRole.UpdateIndicator(yellow);
            } 
            else
            {
                Color blue = new Color(10f / 255, 228f / 255, 1, 1);
                m_match.mainRole.UpdateIndicator(blue);
            }
        }
    }

}