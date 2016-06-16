using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatchHelper
{
	public bool enabled = true;

	public enum CatchType
	{
		eChestCatch,
		eRightCatch,
		eMissCatch,
	}

	public CatchType catchType{get; private set;}

	private Player _player;
	private IM.Number _detectorSize = new IM.Number(3);
	
	private IM.Number _timeCnt;
	private IM.Number _targetAnimTime;

    private Dictionary<string, IM.Vector3> _dictAnimBallSLocalPos = new Dictionary<string, IM.Vector3>();
	
	public CatchHelper(Player player)
	{
		_player = player;
		catchType = CatchType.eChestCatch;
	}

    //所有Catch动作在OnCatch帧时，球的位置（相对于root)
	public void ExtractBallLocomotion()
	{
        _dictAnimBallSLocalPos.Clear();

        _player.animMgr.Stop();

        foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> item in _player.m_animAttributes.m_catch)
        {
            string clipName = item.Key;
            int frame = item.Value.GetKeyFrame("OnCatch").frame;

            IM.Vector3 pos;
            if (!AnimationManager.GetNodePosition(SampleNode.Ball, clipName, frame, out pos))
                Logger.LogError(string.Format("CatchHelper, can't get ball pos at frame: {0} of clip:{1}", frame, clipName));

            _dictAnimBallSLocalPos.Add(item.Key, pos);
        }
	}
	
    //计算接球动画开始时间（球总飞行时间 - 动画播到OnCatch帧的时间）
	IM.Number _CalcAnimBeginTime(string anim, UBasketball ball)
	{
		IM.Vector3 localBall = _dictAnimBallSLocalPos[anim];
        IM.Vector3 newBallTarget = _player.TransformNodePosition(SampleNode.Ball, localBall);
		Player passer = ball.m_actor;
		
		IM.Number predicateDist = IM.Vector3.Distance(newBallTarget, ball.position);
		IM.Number flyTime = predicateDist / passer.m_speedPassBall;
			
		int keyFrame = _player.m_animAttributes.m_catch[_player.animMgr.GetOriginName(anim)].GetKeyFrame("OnCatch").frame;
        IM.Number frameRate = _player.animMgr.GetFrameRate(anim);
		IM.Number animTime = keyFrame / frameRate;
		
		return IM.Math.Max(flyTime - animTime, IM.Number.zero);
	}

    //决定接球动画类型
	public void SetCatchMotionByPasser(Player passer, uint passSkillValue)
	{
        Dictionary<string, uint> data = passer.m_attrData.attrs;
		if( data == null )
		{
			Logger.LogError("Can not find data.");
			return;
		}
		
		Debugger.Instance.m_steamer.message = "Pass ball.";
		IM.Number fDistance = GameUtils.HorizonalDistance(_player.position, passer.position);
		if( fDistance < new IM.Number(10) )
		{
			catchType = CatchType.eChestCatch;
			Debugger.Instance.m_steamer.message = " In distance 10m.";
		}
		else
		{
            //TODO--
            //IM.Number fPassRate = new IM.Number(0,700) + (data["pass"] + passSkillValue) * 0.0012f - (fDistance - 10.0f) * 0.03f;
            IM.Number fPassRate = new IM.Number(0, 700) + (data["pass"] + passSkillValue) * new IM.Number(0, 1) - (fDistance - new IM.Number(10)) * new IM.Number(0, 30);
			IM.Number fRandom = IM.Random.value;
			
			Debugger.Instance.m_steamer.message = "Pass ball. Pass rate: " + fPassRate + ". Random value: " + fRandom;
			if( fRandom < fPassRate )
				catchType = CatchType.eRightCatch;
			else
				catchType = CatchType.eMissCatch;
		}
		
		Debugger.Instance.m_steamer.message += "Catch Type: " + catchType; 
	}


	public void Update(IM.Number deltaTime)
	{
		if(!enabled || _player.m_bSimulator)
			return;
		//should be a parallel state, but not now
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;
		if(ball == null || ball.m_actor == null)
			return;
		Player passer = ball.m_actor;
		PlayerState.State curState = passer.m_StateMachine.m_curState.m_eState;
		if(!_player.m_bToCatch || curState == PlayerState.State.eCatch)
			return;
		
        //进入一定距离范围内后，才开始计算何时开始进入Catch状态播动画
		IM.Number fDistance = GameUtils.HorizonalDistance(ball.position, _player.position);
		if(fDistance > _detectorSize)
			return;
			
		if( IM.Number.Approximately(_targetAnimTime, IM.Number.zero) )
		{
			if(curState == PlayerState.State.eStand)
				_targetAnimTime = _CalcAnimBeginTime("standCatchBallLRChest", ball);
			else if(curState == PlayerState.State.eRun)
				_targetAnimTime = _CalcAnimBeginTime("runCatchBallRHead", ball);
		}
		
		if( _timeCnt > _targetAnimTime )
		{
			_player.m_StateMachine.SetState(PlayerState.State.eCatch);
			_timeCnt = IM.Number.zero;
		}
		else
			_timeCnt += deltaTime;
	}
}