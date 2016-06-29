using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum MatchSoundEvent
{
	//match
	FallGround	= 1,//有球员摔倒时
	Block		= 2,//发生盖帽时
	Rebound		= 3,//抢得对方篮板球时
	Critical	= 4,//零秒绝杀时
	FarShoot	= 5,//投进三分后
	Dunk		= 6,//扣篮成功后
	NormalShoot	= 7,//除三分球和扣篮外，普通进球后
	Foul		= 8,//14s违例倒计时为0时
	ReadyGo		= 9,//开始比赛倒计时前1s
	Last3Sec	= 10,//比赛结束前3s时
	Last0Sec	= 11,//比赛倒计时为0时
	MatchOverPose = 12,//开始播放结束球员动作时
	Foul3Sec    = 13,//14s违例倒计时为3s,2s,1s时
	EnterLoading= 14,//进入loading时
	Openning	= 15,//播放开场秀时
	BeginCounting= 16,//开始比赛倒计时时
	Shot3Pt		= 17,//投三分球时
	Shot2Pt		= 18,//投两分球时
	StealSuc	= 19,//抢断成功时
	//FadeAwayShot= 20,//后仰跳投时
	GameBegin	= 21,//比赛开始
	GameOver	= 22,//比赛结束时
	PassBallSuc	= 23,//传球成功时
	InterceptSuc= 24,//截球成功
	MatchWin	= 25,//比赛结算时比赛胜利
	MatchLose	= 26,//比赛结算时比赛失败
	MatchOverTime = 27,//进加时赛
	BuzzerBeater = 28,//压哨球

	//player
	LeaveGround	= 40,//跳跃
	Shoot		= 41,//投篮出手
	Steal		= 42,//抢断挥手
	FallDown	= 43,//摔倒
	DunkNoGoal	= 44,//扣篮不进触碰篮筐
	ShootNoGoal	= 45,//投篮不进触碰篮筐
	FootStep 	= 46,//跑步脚碰地
	BallOnGround= 47,//运球球着地
	GrabBall	= 48,//手触碰球（抢断成功碰球、接球碰球、扑球成功碰球、截球成功碰球）
	PassBall	= 49,//传球出手
	BlockBall	= 50,//盖帽碰球
	BodyThrowCatch = 51,//扑球倒地帧
	Goal		= 52,//球入网
	RunToShoot	= 53,//跑动状态下停下投篮
	OnGround	= 54,//跳起后着地帧
}

public class MatchSound
{
	public class Item
	{
        public IM.Number fLag = IM.Number.zero;
		public string soundId;
	}

	public MatchSoundEvent	soundEvent;
	public List<Item>	sounds = new List<Item>();
	public List<Item>	narrator_sounds = new List<Item>();
}

public class MatchSoundConfig
{
	string name1 = GlobalConst.DIR_XML_MATCH_SOUND;
    uint count = 0;
    bool isLoadFinish = false;
    private object LockObject = new object();
	private List<MatchSound> m_matchSounds = new List<MatchSound>();

	public MatchSoundConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
	}

	public MatchSound GetSounds(MatchSoundEvent evt)
	{
		return m_matchSounds.Find((MatchSound ms)=>{return ms.soundEvent == evt;});
	}

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 1)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }

    public void ReadConfig()
	{
		try
		{
	        if (isLoadFinish == false)
	            return;

            Debug.Log("Config reading " + name1);
            isLoadFinish = false;
	        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

	        string text = ResourceLoadManager.Instance.GetConfigText(name1);
	        if (text == null)
	        {
	            ErrorDisplay.Instance.HandleLog("LoadConfig failed: " + name1, "", LogType.Error);
	            return;
	        }
	        
	        XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_MATCH_SOUND, text);
			XmlNode root = doc.SelectSingleNode("Data");
			foreach (XmlNode line in root.SelectNodes("Line"))
			{
	            if (CommonFunction.IsCommented(line))
	                continue;

				MatchSoundEvent evt = (MatchSoundEvent)(int.Parse(line.SelectSingleNode("event").InnerText));
				MatchSound ms = GetSounds(evt);
				if( ms == null )
				{
					ms = new MatchSound();
					ms.soundEvent = evt;
				}
				string sounds = line.SelectSingleNode("sounds").InnerText;
				if( sounds.Length != 0 )
				{
					string[] sound = sounds.Split('&');
					foreach( string str in sound )
					{
						string[] tag = str.Split(':');
						MatchSound.Item item = new MatchSound.Item();
						item.soundId = tag[0];
						item.fLag = IM.Number.Parse(tag[1]);
						ms.sounds.Add(item);
					}
				}

				sounds = line.SelectSingleNode("narrator_sounds").InnerText;
				if( sounds.Length != 0 )
				{
					string[]sound = sounds.Split('&');
					foreach( string str in sound )
					{
						string[] tag = str.Split(':');
						MatchSound.Item item = new MatchSound.Item();
						item.soundId = tag[0];
						item.fLag = IM.Number.Parse(tag[1]);
						ms.narrator_sounds.Add(item);
					}
				}
				m_matchSounds.Add(ms);
			}
		}
		catch( XmlException exp )
		{
			Debug.Log("MatchSound config failed: " + exp.Message );
		}
		
    }
}
