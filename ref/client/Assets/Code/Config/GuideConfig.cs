using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

public enum ControlEffect
{
	Finger = 1,
	LeftArrow,
	RightArrow,
	TopArrow,
	BottomArrow,
	Halo,
	BlinkArea,
}

public class GuideModule
{
	public uint ID;
	public string uiName;
	public List<ConditionType> conditionTypes = new List<ConditionType>();
	public List<string> conditionParams = new List<string>();
	public uint firstStep;
	public uint endStep;
	public uint restartStep;
	public string linkID;
	public uint linkSubID;
	public uint nextModule;
	public List<ConditionType> skipConditions = new List<ConditionType>();
	public List<string> skipConditionParams = new List<string>();
}

public class GuideStep
{
	public enum CompleteCondition
	{
		None = 0,
		ClickHighlightButton,
		ClickTipButton,
		ClickAnywhere,
		Delay3Seconds,
		AfterFunc,
		EndWithUI,
	}

	public enum TipArrowType
	{
		None = 0,
		Top,
		Bottom,
		Left,
		Right,
	}

	public uint ID;
	public uint nextStep;
	public string uiName;
	public List<string> highlightCtrls = new List<string>();
	public List<ControlEffect> ctrlEffects = new List<ControlEffect>();
	public string highlightButton;
	public List<ControlEffect> buttonEffects = new List<ControlEffect>();
	public List<Vector2> effectPos = new List<Vector2>();
	public List<string> disabledButtons = new List<string>();
	public string tipText;
	public TipArrowType tipArrowType;
	public float tipArrowOffset;
	public string tipButtonText;
	public string secondButtonText;
	public Vector2 tipPos;
	public string instructor;
	public Vector2 instructorPos;
	public List<CompleteCondition> conditions = new List<CompleteCondition>();
	public string linkID;
	public uint linkSubID;
	public string specialFunc;
}

public class GuideConfig
{
    string name1 = GlobalConst.DIR_XML_GUIDE_MODULE;
    string name2 = GlobalConst.DIR_XML_GUIDE_STEP;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

	private Dictionary<uint, GuideModule> modules = new Dictionary<uint, GuideModule>();
	private Dictionary<uint, GuideStep> steps = new Dictionary<uint, GuideStep>();
	private Dictionary<string, List<GuideModule>> uiGuideMap = new Dictionary<string, List<GuideModule>>();

	public GuideConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
		//ReadModule();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
		//ReadStep();
	}
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 2)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }
    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name1);
		ReadModule();
		Logger.ConfigBegin(name2);
		Logger.ConfigEnd(name1);
		ReadStep();
		Logger.ConfigEnd(name2);
    }

	public List<GuideModule> GetModules(string uiName)
	{
		List<GuideModule> list = null;
		uiGuideMap.TryGetValue(uiName, out list);
		return list;
	}

	public GuideModule GetModule(uint ID)
	{
		GuideModule module = null;
		modules.TryGetValue(ID, out module);
		return module;
	}

	public GuideStep GetStep(uint ID)
	{
		GuideStep step = null;
		steps.TryGetValue(ID, out step);
		return step;
	}

    void ReadModule()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            ErrorDisplay.Instance.HandleLog("LoadConfig failed: " + name1, "", LogType.Error);
            return;
        }
        
        XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_GUIDE_MODULE, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
            if (CommonFunction.IsCommented(line))
                continue;
			GuideModule module = new GuideModule();
			module.ID = uint.Parse(line.SelectSingleNode("id").InnerText);
			module.uiName = line.SelectSingleNode("ui_name").InnerText;
			string types = line.SelectSingleNode("type").InnerText;
			if (!string.IsNullOrEmpty(types))
			{
				string[] tokens = types.Split('&');
				foreach (string token in tokens)
				{
					module.conditionTypes.Add((ConditionType)int.Parse(token));
				}
			}
			string param = line.SelectSingleNode("param").InnerText;
			if (!string.IsNullOrEmpty(param))
			{
				module.conditionParams.AddRange(param.Split('&'));
			}
			if (module.conditionTypes.Count != module.conditionParams.Count)
				ErrorDisplay.Instance.HandleLog("Guide module config error, wrong param num. ID: " + module.ID, "", LogType.Error);
			module.firstStep = uint.Parse(line.SelectSingleNode("first_step").InnerText);
			uint.TryParse(line.SelectSingleNode("end_step").InnerText, out module.endStep);
			uint.TryParse(line.SelectSingleNode("restart_step").InnerText, out module.restartStep);
			string linkID = line.SelectSingleNode("link_id").InnerText;
			if (!string.IsNullOrEmpty(linkID))
			{
				string[] tokens = linkID.Split(',');
				module.linkID = tokens[0];
				if (tokens.Length > 1)
					uint.TryParse(tokens[1], out module.linkSubID);
			}
			uint.TryParse(line.SelectSingleNode("next_module").InnerText, out module.nextModule);
			modules.Add(module.ID, module);
			List<GuideModule> list;
			if (!uiGuideMap.TryGetValue(module.uiName, out list))
			{
				list = new List<GuideModule>();
				uiGuideMap.Add(module.uiName, list);
			}
			types = line.SelectSingleNode("skip_condition").InnerText;
			if (!string.IsNullOrEmpty(types))
			{
				string[] tokens = types.Split('&');
				foreach (string token in tokens)
				{
					module.skipConditions.Add((ConditionType)int.Parse(token));
				}
			}
			param = line.SelectSingleNode("skip_param").InnerText;
			if (!string.IsNullOrEmpty(param))
			{
				module.skipConditionParams.AddRange(param.Split('&'));
			}
			if (module.skipConditions.Count != module.skipConditionParams.Count)
				ErrorDisplay.Instance.HandleLog("Guide module config error, wrong skip param num. ID: " + module.ID, "", LogType.Error);
			list.Add(module);
		}
	}

    void ReadStep()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            ErrorDisplay.Instance.HandleLog("LoadConfig failed: " + name2, "", LogType.Error);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_GUIDE_STEP, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
            if (CommonFunction.IsCommented(line))
                continue;

			GuideStep step = new GuideStep();
			step.ID = uint.Parse(line.SelectSingleNode("id").InnerText);
			if (steps.ContainsKey(step.ID))
				ErrorDisplay.Instance.HandleLog("Step ID: " + step.ID + " already existed.", "", LogType.Error);
			uint.TryParse(line.SelectSingleNode("next_step").InnerText, out step.nextStep);
			step.uiName = line.SelectSingleNode("ui_name").InnerText;
			string hlctrls = line.SelectSingleNode("highlight_control").InnerText;
			if (!string.IsNullOrEmpty(hlctrls))
			{
				step.highlightCtrls.AddRange(hlctrls.Split('&'));
			}
			XmlNode node = line.SelectSingleNode("control_effect");
			if (node != null && !string.IsNullOrEmpty(node.InnerText))
			{
				string[] tokens = node.InnerText.Split('&');
				foreach (string token in tokens)
				{
					step.ctrlEffects.Add((ControlEffect)int.Parse(token));
				}
			}
			step.highlightButton = line.SelectSingleNode("highlight_button").InnerText;
			if (!string.IsNullOrEmpty(step.highlightButton))
			{
				step.highlightCtrls.Add(step.highlightButton);
			}
			string effects = line.SelectSingleNode("button_effect").InnerText;
			if (!string.IsNullOrEmpty(effects))
			{
				string[] tokens = effects.Split('&');
				foreach (string token in tokens)
				{
					step.buttonEffects.Add((ControlEffect)int.Parse(token));
				}
			}
			string effectPos = line.SelectSingleNode("effect_position").InnerText;
			if (!string.IsNullOrEmpty(effectPos))
			{
				string[] tokensPos = effectPos.Split('&');
				foreach (string tokenPos in tokensPos)
				{
					string[] tokensXY = tokenPos.Split(',');
					Vector2 pos = new Vector2(float.Parse(tokensXY[0]), float.Parse(tokensXY[1]));
					step.effectPos.Add(pos);
				}
			}
			else
			{
				step.effectPos.Add(Vector2.zero);
			}
			string disabledButton = line.SelectSingleNode("disabled_button").InnerText;
			if (!string.IsNullOrEmpty(disabledButton))
			{
				step.disabledButtons.AddRange(disabledButton.Split('&'));
			}
			step.tipText = line.SelectSingleNode("tip").InnerText;
			node = line.SelectSingleNode("tip_arrow_pos");
			if (node != null)
				step.tipArrowType = (GuideStep.TipArrowType)(int.Parse(node.InnerText));
			node = line.SelectSingleNode("tip_arrow_offset");
			if (node != null)
				float.TryParse(node.InnerText, out step.tipArrowOffset);
			step.tipButtonText = line.SelectSingleNode("tip_button_text").InnerText;
			node = line.SelectSingleNode("second_button_text");
			if (node != null)
				step.secondButtonText = node.InnerText;
			string tipPos = line.SelectSingleNode("tip_position").InnerText;
			if (!string.IsNullOrEmpty(tipPos))
			{
				string[] tokens = tipPos.Split(',');
				step.tipPos.x = float.Parse(tokens[0]);
				step.tipPos.y = float.Parse(tokens[1]);
			}
			node = line.SelectSingleNode("instructor");
			if (node != null)
				step.instructor = node.InnerText;
			node = line.SelectSingleNode("instructor_pos");
			if (node != null && !string.IsNullOrEmpty(node.InnerText))
			{
				string[] tokens = node.InnerText.Split(',');
				step.instructorPos.Set(float.Parse(tokens[0]), float.Parse(tokens[1]));
			}
			string conditions = line.SelectSingleNode("complete_condition").InnerText;
			if (!string.IsNullOrEmpty(conditions))
			{
				string[] tokens = conditions.Split('&');
				foreach (string token in tokens)
				{
					step.conditions.Add((GuideStep.CompleteCondition)int.Parse(token));
				}
			}
			string linkID = line.SelectSingleNode("link_id").InnerText;
			if (!string.IsNullOrEmpty(linkID))
			{
				string[] tokens = linkID.Split(',');
				step.linkID = tokens[0];
				if (tokens.Length > 1)
					uint.TryParse(tokens[1], out step.linkSubID);
			}
			step.specialFunc = line.SelectSingleNode("special_entry_point").InnerText;
			steps.Add(step.ID, step);
		}
	}
}
