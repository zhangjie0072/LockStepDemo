using fogs.proto.msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

public enum PractiseStepBehaviour
{
	None,
	PlayerControl,	//玩家控制
	Face2Mate,		//面向第一个队友
	PickPass2Mate,	//拾球并传给队友
	Run2Basket,		//跑向篮下
	Layup,			//上篮
	Defense,		//盯防
	Shoot,			//投篮
}

public enum PractiseStepCondition
{
	None,
	GrabBall,		// 拾球
	CatchBall,		// 接球
	Goal,			// 进球
	HitGround,		// 球落地
	ButtonPress,	// 按钮按下
	ButtonRelease,	// 按钮松开
	InDist,			// 与某对象在XX距离内
	EnterArea,		// 进入区域
	EnterRow,		// 进入网格行区域
	EnterState,		// 进入状态
	DoubleDribble,	// 二次运球
	Defended,		// 被防守
	Undefended,		// 未被防守
	OnGround,		// 在地上
	BlockTiming,	// 到达盖帽时机
	ReboundTiming,	// 到达篮板时机
	Blocked,		// 盖帽结果
	BlockInArea,	// 在盖帽范围内
	BlockTooLate,	// 盖帽起跳太晚
	Rebound,		// 篮板结果
	ReboundInArea,	// 在篮板范围内
	ReboundTooLate,	// 篮板起跳太晚
	Wait,			// 等待
	ClickScreenEffect,	// 点击屏幕特效
}

public class PractiseStep
{
	public uint ID;
	public string name;
	public uint next;
	public uint failNext;
	public KeyValuePair<int, string> startObjective;
	public int endObjective;
	public int dialogRole;	// role index
	public string dialog;	// dialog
	public string voiceover;
	public int ballOwner = -2;	// -1 means no owner, -2 means ignore
	public Dictionary<int, int> oppo;	// offense role index, defense role index -1 means none
	public List<int> canPick;
	public Dictionary<int, bool> visible;
	public Dictionary<int, IM.Vector3> position;	// role index, position
	public Dictionary<int, string> orientation;	// role index, face to obj
	public Dictionary<int, PractiseStepBehaviour> behaviour;	// role index, behaviour
	public bool canMove;
	public List<Command> action;
	public List<Command> disabledAction;
	public bool hintMove;
	public float hintMoveAngle;
	public List<Command> hintAction;
	public string screenEffect;
	public Dictionary<string, string> sceneEffect;		// scene obj, effect name
	public Dictionary<string, string> sceneHint;		// scene obj, hint text
	public float timeScale;
	public bool mustGoal;
	public bool cantGoal;
	public KeyValuePair<int, int> shootSolution;
	public bool mustBlock;
	public bool mustCross;
	public List<KeyValuePair<PractiseStepCondition, object[]>> endCondition;	// end condition, params
	public List<KeyValuePair<PractiseStepCondition, object[]>> failCondition;	// fail condition, params
}

public class PractiseStepConfig
{
    string name1 = GlobalConst.DIR_XML_PRACTICE_STEP;
    bool isLoadFinish = false;
    private object LockObject = new object();

	Dictionary<uint, PractiseStep> steps = new Dictionary<uint, PractiseStep>();

	public PractiseStepConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
	}

    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name1);
		ReadStep();
		Logger.ConfigEnd(name1);
    }

	public PractiseStep GetFirstStep()
	{
		var e = steps.GetEnumerator();
		if (e.MoveNext())
			return e.Current.Value;
		return null;
	}

	public PractiseStep GetStep(uint ID)
	{
		PractiseStep step = null;
		steps.TryGetValue(ID, out step);
		return step;
	}

    void ReadStep()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PRACTICE_STEP, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
            if (CommonFunction.IsCommented(line))
                continue;

			PractiseStep step = new PractiseStep();
			if (!uint.TryParse(line.SelectSingleNode("ID").InnerText, out step.ID))
				LogError("ID is empty.");
			XmlNode nodeName = line.SelectSingleNode("name");
			if (nodeName != null)
				step.name = nodeName.InnerText;
			uint.TryParse(line.SelectSingleNode("next").InnerText, out step.next);
			uint.TryParse(line.SelectSingleNode("fail_next").InnerText, out step.failNext);
			string strObjective = line.SelectSingleNode("objective").InnerText;
			if (!string.IsNullOrEmpty(strObjective))
			{
				string[] tokens = strObjective.Split(',');
				if (tokens.Length == 3 && tokens[0] == "S")
					step.startObjective = new KeyValuePair<int, string>(int.Parse(tokens[1]), tokens[2]);
				else if (tokens.Length == 2 && tokens[0] == "E")
					step.endObjective = int.Parse(tokens[1]);
				else
					LogError(step.ID, "Illegal format: objective");
			}
			string strDlg = line.SelectSingleNode("dialog").InnerText;
			if (!string.IsNullOrEmpty(strDlg))
			{
				string[] tokens = strDlg.Split(':');
				if (tokens.Length == 2 && int.TryParse(tokens[0], out step.dialogRole))
					step.dialog = tokens[1];
				else
					LogError(step.ID, "Illegal format: dialog");
			}
			step.voiceover = line.SelectSingleNode("voiceover").InnerText;
			if (!int.TryParse(line.SelectSingleNode("ball_owner").InnerText, out step.ballOwner))
				step.ballOwner = -2;
			string strOppos = line.SelectSingleNode("oppo").InnerText;
			if (!string.IsNullOrEmpty(strOppos))
			{
				step.oppo = new Dictionary<int, int>();
				foreach (string strOppo in CommonFunction.SplitLines(strOppos))
				{
					if (string.IsNullOrEmpty(strOppo))
						continue;
					string[] tokens = strOppo.Split('-');
					int o = -1, d = -1;
					if (!int.TryParse(tokens[0], out o))
						o = -1;
					if (tokens.Length <= 1 || !int.TryParse(tokens[1], out d))
						d = -1;
					step.oppo.Add(o, d);
				}
			}
			string strCanPick = line.SelectSingleNode("can_pick").InnerText;
			if (!string.IsNullOrEmpty(strCanPick))
			{
				step.canPick = new List<int>();
				string[] tokens = strCanPick.Split('&');
				foreach (string token in tokens)
				{
					int index = 0;
					if (int.TryParse(token, out index))
						step.canPick.Add(index);
				}
			}
			string strVisibles = line.SelectSingleNode("visible").InnerText;
			if (!string.IsNullOrEmpty(strVisibles))
			{
				step.visible = new Dictionary<int, bool>();
				foreach (string strVisible in CommonFunction.SplitLines(strVisibles))
				{
					if (string.IsNullOrEmpty(strVisible))
						continue;
					string[] tokens = strVisible.Split(':');
					int index = 0;
					bool visible = false;
					if (!(tokens.Length == 2 && int.TryParse(tokens[0], out index) && bool.TryParse(tokens[1], out visible)))
						LogError(step.ID, "Illegal format: visible");
				}
			}
			string strPositions = line.SelectSingleNode("position").InnerText;
			if (!string.IsNullOrEmpty(strPositions))
			{
				step.position = new Dictionary<int, IM.Vector3>();
				foreach (string strPosition in CommonFunction.SplitLines(strPositions))
				{
					if (string.IsNullOrEmpty(strPosition))
						continue;
					string[] tokens = strPosition.Split(':');
					int index = 0;
					IM.Number x = IM.Number.zero, y = IM.Number.zero, z = IM.Number.zero;
					if (tokens.Length == 2 && int.TryParse(tokens[0], out index))
					{
						string[] tokens1 = tokens[1].Split(',');
						if (!(tokens1.Length == 3 &&
							IM.Number.TryParse(tokens1[0], out x) &&
							IM.Number.TryParse(tokens1[1], out y) &&
							IM.Number.TryParse(tokens1[2], out z)))
							LogError(step.ID, "Illegal format: position");	
					}
					else
						LogError(step.ID, "Illegal format: position");
					step.position.Add(index, new IM.Vector3(x, y, z));
				}
			}
			string strOrientations = line.SelectSingleNode("orientation").InnerText;
			if (!string.IsNullOrEmpty(strOrientations))
			{
				step.orientation = new Dictionary<int, string>();
				foreach (string strOrientation in CommonFunction.SplitLines(strOrientations))
				{
					if (string.IsNullOrEmpty(strOrientation))
						continue;
					string[] tokens = strOrientation.Split(':');
					int index = 0;
					if (tokens.Length == 2 && int.TryParse(tokens[0], out index))
					{
						step.orientation.Add(index, tokens[1]);
					}
					else
						LogError(step.ID, "Illegal format: orientation");
				}
			}
			string strBehaviours = line.SelectSingleNode("behaviour").InnerText;
			if (!string.IsNullOrEmpty(strBehaviours))
			{
				step.behaviour = new Dictionary<int, PractiseStepBehaviour>();
				foreach (string strBehaviour in CommonFunction.SplitLines(strBehaviours))
				{
					if (string.IsNullOrEmpty(strBehaviour))
						continue;
					string[] tokens = strBehaviour.Split(':');
					int index;
					PractiseStepBehaviour behaviour;
					if (tokens.Length == 2 && int.TryParse(tokens[0], out index))
					{
						behaviour = (PractiseStepBehaviour)Enum.Parse(PractiseStepBehaviour.None.GetType(), tokens[1]);
						step.behaviour.Add(index, behaviour);
					}
					else
						LogError(step.ID, "Illegal format: behaviour");
				}
			}
			string strAction = line.SelectSingleNode("action").InnerText;
			if (!string.IsNullOrEmpty(strAction))
			{
				string[] tokens = strAction.Split('&');
				foreach (string token in tokens)
				{
					if (token == "Move")
						step.canMove = true;
					else if (token.StartsWith("-"))
					{
						if (step.disabledAction == null)
							step.disabledAction = new List<Command>();
						step.disabledAction.Add((Command)Enum.Parse(Command.Max.GetType(), token.Substring(1)));
					}
					else
					{
						if (step.action == null)
							step.action = new List<Command>();
						step.action.Add((Command)Enum.Parse(Command.Max.GetType(), token));
					}
				}
			}
			string strHint = line.SelectSingleNode("hint").InnerText;
			if (!string.IsNullOrEmpty(strHint))
			{
				string[] tokens = strHint.Split('&');
				foreach (string token in tokens)
				{
					if (token.StartsWith("Move"))
					{
						step.hintMove = true;
						string[] moveTokens = token.Split(',');
						if (moveTokens.Length >= 2)
							step.hintMoveAngle = float.Parse(moveTokens[1]);
					}
					else
					{
						if (step.hintAction == null)
							step.hintAction = new List<Command>();
						step.hintAction.Add((Command)Enum.Parse(Command.Max.GetType(), token));
					}
				}
			}
			step.screenEffect = line.SelectSingleNode("screen_effect").InnerText;
			string strSceneEffects = line.SelectSingleNode("scene_effect").InnerText;
			if (!string.IsNullOrEmpty(strSceneEffects))
			{
				step.sceneEffect = new Dictionary<string, string>();
				foreach (string effect in CommonFunction.SplitLines(strSceneEffects))
				{
                    if (string.IsNullOrEmpty(effect))
                        continue;
					string[] tokens = effect.Split(':');
					Assert(tokens.Length == 2, step.ID, "Illegal format: scene_effect");
					step.sceneEffect.Add(tokens[0], tokens[1]);
				}
			}
			string strSceneHint = line.SelectSingleNode("scene_hint").InnerText;
			if (!string.IsNullOrEmpty(strSceneHint))
			{
				step.sceneHint = new Dictionary<string, string>();
				foreach (string hint in CommonFunction.SplitLines(strSceneHint))
				{
                    if (string.IsNullOrEmpty(hint))
                        continue;
					string[] tokens = hint.Split(':');
					Assert(tokens.Length == 2, step.ID, "Illegal format: scene_hint");
					step.sceneHint.Add(tokens[0], tokens[1]);
				}
			}
			if (!float.TryParse(line.SelectSingleNode("time_scale").InnerText, out step.timeScale))
				step.timeScale = 1f;
			string strMustGoal = line.SelectSingleNode("must_goal").InnerText;
			if (!string.IsNullOrEmpty(strMustGoal))
			{
				bool goal;
				bool.TryParse(strMustGoal, out goal);
				if (goal)
					step.mustGoal = true;
				else
					step.cantGoal = true;
			}
			string strShootSolution = line.SelectSingleNode("shoot_solution").InnerText;
			if (!string.IsNullOrEmpty(strShootSolution))
			{
				string[] tokens = strShootSolution.Split(',');
				step.shootSolution = new KeyValuePair<int, int>(int.Parse(tokens[0]), int.Parse(tokens[1]));
			}
			bool.TryParse(line.SelectSingleNode("must_block").InnerText, out step.mustBlock);
			bool.TryParse(line.SelectSingleNode("must_cross").InnerText, out step.mustCross);
			try{
				ParseCondition(line.SelectSingleNode("end_cond").InnerText, out step.endCondition);
			}
			catch(System.Exception ex)
			{
				LogError(step.ID, "Illegal format: end_cond. " + ex);
			}
			try{
				ParseCondition(line.SelectSingleNode("fail_cond").InnerText, out step.failCondition);
			}
			catch(System.Exception ex)
			{
				LogError(step.ID, "Illegal format: fail_cond" + ex);
			}

			steps.Add(step.ID, step);
		}
	}

	void ParseCondition(string text, out List<KeyValuePair<PractiseStepCondition, object[]>> cond)
	{
		if (string.IsNullOrEmpty(text))
			cond = null;

		cond = new List<KeyValuePair<PractiseStepCondition, object[]>>();
		foreach (string line in CommonFunction.SplitLines(text))
		{
			PractiseStepCondition condition = PractiseStepCondition.None;
			object[] parameters = null;
			if (string.IsNullOrEmpty(line))
				continue;
			string[] tokens = line.Split(':');
			condition = (PractiseStepCondition)Enum.Parse(PractiseStepCondition.None.GetType(), tokens[0]);
			string[] strParams = null;
			if (tokens.Length == 2)
				strParams = tokens[1].Split(',');
			int index = 0;
			switch (condition)
			{
				case PractiseStepCondition.GrabBall:
				case PractiseStepCondition.CatchBall:
				case PractiseStepCondition.DoubleDribble:
				case PractiseStepCondition.Defended:
				case PractiseStepCondition.Undefended:
				case PractiseStepCondition.OnGround:
				case PractiseStepCondition.ReboundTiming:
					parameters = new object[] { int.Parse(strParams[0]) };
					break;
				case PractiseStepCondition.EnterRow:
					parameters = new object[] { int.Parse(strParams[0]), int.Parse(strParams[1]), int.Parse(strParams[2]) };
					break;
				case PractiseStepCondition.ButtonPress:
				case PractiseStepCondition.ButtonRelease:
					parameters = new object[] { ParseCommand(strParams[0]) };
					break;
				case PractiseStepCondition.EnterArea:
					parameters = new object[] { int.Parse(strParams[0]), ParseArea(strParams[1]) };
					break;
				case PractiseStepCondition.EnterState:
					parameters = new object[] { int.Parse(strParams[0]), ParsePlayerState(strParams[1]) };
					break;
				case PractiseStepCondition.Wait:
					parameters = new object[] { float.Parse(strParams[0]) };
					break;
				case PractiseStepCondition.Rebound:
				case PractiseStepCondition.ReboundInArea:
				case PractiseStepCondition.ReboundTooLate:
				case PractiseStepCondition.Blocked:
				case PractiseStepCondition.BlockInArea:
				case PractiseStepCondition.BlockTooLate:
					parameters = new object[] { int.Parse(strParams[0]), bool.Parse(strParams[1]) };
					break;
				case PractiseStepCondition.InDist:
					parameters = new object[] { int.Parse(strParams[0]), strParams[1], float.Parse(strParams[2]), bool.Parse(strParams[3]) };
					break;
				case PractiseStepCondition.Goal:
				case PractiseStepCondition.HitGround:
				case PractiseStepCondition.BlockTiming:
				case PractiseStepCondition.ClickScreenEffect:
					// no parameter
					break;
			}
			cond.Add(new KeyValuePair<PractiseStepCondition, object[]>(condition, parameters));
		}
	}

	Command ParseCommand(string text)
	{
		return (Command)Enum.Parse(Command.Max.GetType(), text);
	}

	Area ParseArea(string text)
	{
		return (Area)Enum.Parse(Area.eInvalid.GetType(), text);
	}

	PlayerState.State ParsePlayerState(string text)
	{
		return (PlayerState.State)Enum.Parse(PlayerState.State.eMax.GetType(), text);
	}

	void LogError(string log)
	{
		ErrorDisplay.Instance.HandleLog("PractiseStep, " + log, "", LogType.Error);
	}

	void Assert(bool condition, string log)
	{
		if (!condition)
			LogError(log);
	}

	void LogError(uint ID, string log)
	{
		ErrorDisplay.Instance.HandleLog(string.Format("PractiseStep, ID:{0}, {1}", ID, log), "", LogType.Error);
	}

	void Assert(bool condition, uint ID, string log)
	{
		if (!condition)
			LogError(ID, log);
	}
}
