using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class InputReader : Singleton<InputReader>
{
    InputDirection curDir = InputDirection.None;
    Command curCmd = Command.None;
    public const int DIR_NUM = (int)InputDirection.Max - 2;
    public const float ANGLE_PER_DIR = 360 / DIR_NUM;

    public List<Command> validCmdList = new List<Command>();
    private bool _enabled = true;
    public bool enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value; 
            //如果禁用，立即向服务器告知
            if (!value)
                GameMsgSender.SendInput(InputDirection.None, Command.None);
        }
    }
    public Player player;

    public void Update(GameMatch match)
    {
		if (!enabled)
		{
			_UpdateValidCommand(match);
			return;
		}

        _UpdateValidCommand(match);
        InputDirection dir = ConvertToDirection(GameSystem.Instance.mClient.mInputManager.mHVDirection);

        Command cmd = Command.None;
        if (match.m_uiController == null)
            return;
		if( GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Click && 
            match.m_uiController.m_btns[0].cmd != Command.None)
		{
			cmd = match.m_uiController.m_btns[0].cmd;
		}
		else if (GameSystem.Instance.mClient.mInputManager.m_CmdBtn2Click &&
            match.m_uiController.m_btns[1].cmd != Command.None)
		{
			cmd = match.m_uiController.m_btns[1].cmd;
		}
		else if (GameSystem.Instance.mClient.mInputManager.m_CmdBtn3Click &&
            match.m_uiController.m_btns[2].cmd != Command.None)
		{
			cmd = match.m_uiController.m_btns[2].cmd;
		}
		else if (GameSystem.Instance.mClient.mInputManager.m_CmdBtn4Click &&
            match.m_uiController.m_btns[3].cmd != Command.None)
		{
			cmd = match.m_uiController.m_btns[3].cmd;
		}

        if (curCmd != cmd || curDir != dir)
        {
            GameMsgSender.SendInput(dir, cmd);
        }
        curCmd = cmd;
        curDir = dir;
    }

    static InputDirection ConvertToDirection(Vector2 hv)
    {
        return ConvertToDirection(hv.x, hv.y);
    }

    static InputDirection ConvertToDirection(float hori, float vert)
    {
        if (Mathf.Approximately(hori, 0f) && Mathf.Approximately(vert, 0f))
            return InputDirection.None;
        Vector3 vec = new Vector3(hori, 0f, vert);
        vec.Normalize();
        float horiAngle = Quaternion.FromToRotation(Vector3.forward, vec).eulerAngles.y;
        int dir = (int)(horiAngle / ANGLE_PER_DIR);
        return (InputDirection)(dir + 1);
    }

	void _UpdateValidCommand(GameMatch match)
	{
		validCmdList.Clear();

        if (player == null)
            return;
		
		bool bOffense = (player.m_team.m_role == GameMatch.MatchRole.eOffense);
		UBasketball ball = match.mCurScene.mBall;
		UBasket basket = match.mCurScene.mBasket;
		bool hasTeamMate = player.m_team.GetMemberCount() > 1;
		
		if(bOffense)
		{
			if( player.m_bWithBall )
			{
				if (hasTeamMate && match.IsCommandValid(Command.Pass))
					validCmdList.Add(Command.Pass);
				if (match.IsCommandValid(Command.Shoot))
					validCmdList.Add(Command.Shoot);
				if (player.m_position == PositionType.PT_C || player.m_position == PositionType.PT_PF)
				{
					if (match.IsCommandValid(Command.BackToBack))
						validCmdList.Add(Command.BackToBack);
				}
				else
				{
					if (match.IsCommandValid(Command.CrossOver))
						validCmdList.Add(Command.CrossOver);
				}
			}
			else
			{
				if( ball != null)
				{
					if( ball.m_owner != null || ball.m_ballState == BallState.eUseBall_Pass )
					{
						if ( match.IsCommandValid(Command.RequireBall))
							validCmdList.Add(Command.RequireBall);
					}

					if( ball.m_owner != null && ball.m_ballState != BallState.eUseBall_Pass )
					{
					   if ( match.IsCommandValid(Command.PickAndRoll) )
					   		validCmdList.Add(Command.PickAndRoll);
					}
				}
				if (match.IsCommandValid(Command.CutIn))
					validCmdList.Add(Command.CutIn);
			}
			if (ball == null || (ball.m_ballState != BallState.eLoseBall && ball.m_ballState != BallState.eRebound))
			{
				if ( match.IsCommandValid(Command.Rush)  )
					validCmdList.Add(Command.Rush);
			}
		}
		//defense
		else
		{
            if (ball != null)
            {
                if (ball.m_ballState == BallState.eUseBall
                    || ball.m_ballState == BallState.eUseBall_Pass
                    || ball.m_ballState == BallState.eUseBall_Shoot)
                {
                    if (match.IsCommandValid(Command.Rush) && match.GetMatchType() == GameMatch.Type.ePVP_3On3)
                        validCmdList.Add(Command.Rush);
                    if (match.IsCommandValid(Command.Defense))
                        validCmdList.Add(Command.Defense);
                }

                if (ball != null && ball.m_owner != null && match.IsCommandValid(Command.Steal) || (ball.m_ballState == BallState.eUseBall_Pass))
                    validCmdList.Add(Command.Steal);

                if (ball != null && (ball.m_ballState == BallState.eUseBall || ball.m_ballState == BallState.eUseBall_Shoot))
                {
                    if (match.IsCommandValid(Command.Block))
                        validCmdList.Add(Command.Block);
                }
            }
			if (hasTeamMate && match.IsCommandValid(Command.Switch))
				validCmdList.Add(Command.Switch);
		}
		if (ball != null && ball.m_ballState == BallState.eLoseBall)
		{
			if (match.GetMatchType() == GameMatch.Type.ePVP_3On3)
			{
				if (match.IsCommandValid(Command.Rush))
					validCmdList.Add(Command.Rush);
			}
			else
			{
				if (match.IsCommandValid(Command.TraceBall))
					validCmdList.Add(Command.TraceBall);
			}

			if (hasTeamMate && match.IsCommandValid(Command.Switch))
				validCmdList.Add(Command.Switch);

			if(player.m_position == PositionType.PT_SG || player.m_position == PositionType.PT_PG)
			{
				if (match.IsCommandValid(Command.BodyThrowCatch))
					validCmdList.Add(Command.BodyThrowCatch);
			}
		}
		if (ball != null && 
			(ball.m_ballState == BallState.eLoseBall ||
			ball.m_ballState == BallState.eRebound ||
			ball.m_ballState == BallState.eNone) )
		{
			if (hasTeamMate && match.IsCommandValid(Command.Switch))
				validCmdList.Add(Command.Switch);
		}

        MatchState curMatchState = match.m_stateMachine.m_curState;

        if (!player.m_bWithBall
            && (ball != null && ball.m_ballState == BallState.eRebound
            || (curMatchState != null && curMatchState.m_eState == MatchState.State.eTipOff)))
        {
            if (match.IsCommandValid(Command.Rebound))
                validCmdList.Add(Command.Rebound);
            //float fDist = GameUtils.HorizonalDistance(player.position, basket.m_vShootTarget).ToUnity();
            //if( fDist < 3.0f )
            //	m_validCmdList.Add(Command.JockeyForPosition);
        }

		if (match.m_uiController != null)
			match.m_uiController.UpdateBtnCmd();
	}
}
