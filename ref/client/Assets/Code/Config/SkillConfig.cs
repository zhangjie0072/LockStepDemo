using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum SkillType
{
    PASSIVE,    //被动技能
    ACTIVE,     //主动技能
}

public enum SkillSubType
{
	SPEC,		//特殊动作
    BASIC,		//基础动作
    NORMAL,		//基础技能
}

/**技能属性*/
public class SkillAttr
{
    public uint id;//技能ID
    public string name;//技能名字
    public string icon;//持能ICON图标
    public string intro;//技能说明
    public string cast;//操作说明
    public SkillType type;//技能类型
    public SkillSubType subtype;//技能子类型
    public uint action_type;//ActionType
    public List<int> positions = new List<int>();
    public List<uint> roles = new List<uint>();
    public List<uint> area = new List<uint>();//区域限制
    public List<int> condition = new List<int>();//前置条件
	public IM.Number attrange;//触发距离

	public Dictionary<uint, SkillLevel> levels = new Dictionary<uint,SkillLevel>();

	//public uint[] weights = new uint[6];
	public List<SkillAction> actions = new List<SkillAction>();
	public Dictionary<uint, uint> equip_conditions = new Dictionary<uint, uint>();
	public Dictionary<int, SkillSideEffect> side_effects = new Dictionary<int, SkillSideEffect>();

	public SkillLevel GetSkillLevel(uint id)
	{
		if (levels.ContainsKey(id))
			return levels[id];
		return null;
	}

	public bool SameAction(SkillAttr other)
	{
		if (other == null || other.actions.Count != actions.Count)
			return false;
		for (int i = 0; i < actions.Count; ++i)
		{
			if (!actions[i].SameInput(other.actions[i]))
				return false;
		}
		return true;
	}
}

public struct SkillInterrupt
{
    public int type;//打断的类型
	public int id;//打断的ID
}

public class SkillInput
{
	public EDirection moveDir = EDirection.eNone;
	public IM.Vector2 moveAngleRange = IM.Vector2.zero;
	public InputDirType inputType = InputDirType.eBasket;
	public Command cmd = Command.None;
	public static bool operator ==(SkillInput a, SkillInput b)
	{
		return Equals(a, b);
	}

	public static bool operator !=(SkillInput a, SkillInput b)
	{
		return !Equals(a, b);
	}

	static bool Equals(SkillInput a, SkillInput b)
	{
		if (object.Equals(a, null) || object.Equals(b, null))
			return object.Equals(a, b);
		return a.moveDir == b.moveDir && b.inputType == b.inputType && a.cmd == b.cmd;
	}
}

public class SkillAction
{
	public uint id;//表现ID
    public List<SkillInterrupt> interrupts = new List<SkillInterrupt>();//技能打断
	public List<SkillInput> inputs;
    public string action_id;
	public List<uint> skillEffects = new List<uint>();
	public string camera_animation;
	public SkillInput block_key;
	public bool SameInput(SkillAction other)
	{
		if (other == null || other.inputs.Count != inputs.Count)
			return false;
		for (int i = 0; i < inputs.Count; ++i)
		{
			if (inputs[i] != other.inputs[i])
				return false;
		}
		return true;
	}
}

public class SkillSideEffect
{
	public enum Type
	{
		eShootRate = 1,
		eBlockRate = 2,
	}

	public int type;
	public IM.Number value;
}

/**技能特效*/
public class SkillEffect
{
    public uint     id; //特效ID
	public string 	effectRes;//特效资源
	public uint		playBackType;//
	public uint		tagPointOwner;//1.player 2.basket 3.basketball
	public List<string>	tagPoints = new List<string>();
	public uint		moveWithTagPoint;//技能是否跟随
	public bool		spawned = false;
	public uint		startFrame;//开始帧
	public uint		endFrame;//结束帧
}

public class SkillLevel
{
	public uint level;//等级
	public List<SkillConsumable> consumables = new List<SkillConsumable>();//消耗品
    public Dictionary<string, uint> additional_attrs = new Dictionary<string, uint>();//属性加成
	public uint stama;//耐力的消耗
	public uint weight;//权重
	public Dictionary<uint, SkillSpec> parameters = new Dictionary<uint, SkillSpec>();//特殊效果参数
}

public class SkillConsumable
{
    public uint consumable_id;//消耗品ID
    public uint consumable_quantity;//消耗数量
}

public class SkillSlot
{
    public uint id;//技能槽ID
    public uint level_required;//等级要求
    public List<SkillConsumable> consumables = new List<SkillConsumable>();//升级消耗物品信息
}

public class SkillSpec
{
	public uint 				paramId;
	public IM.Number		    value;
	public SkillSpecParamOp		paramOp;
}

public class SkillConfig
{
    string name1 = GlobalConst.DIR_XML_SKILL_ACTION;
    string name2 = GlobalConst.DIR_XML_SKILL_ATTR;
	string name3 = GlobalConst.DIR_XML_SKILL_LEVEL;
    string name4 = GlobalConst.DIR_XML_SKILL_SLOT;
    //string name5 = GlobalConst.DIR_XML_SKILL_WEIGHT;
	string name6 = GlobalConst.DIR_XML_SKILL_EFFECTS;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    public Dictionary<uint, SkillAttr> skills = new Dictionary<uint, SkillAttr>();
    public Dictionary<uint, SkillSlot> slots = new Dictionary<uint, SkillSlot>();
    public List<SkillAttr> basic_skills = new List<SkillAttr>();
	public Dictionary<uint, SkillAction> actions = new Dictionary<uint, SkillAction>();
	public Dictionary<uint, SkillEffect> skillEffectItems = new Dictionary<uint, SkillEffect>();

    public SkillConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name4, LoadFinish);
		//ResourceLoadManager.Instance.GetConfigResource(name5);
        ResourceLoadManager.Instance.GetConfigResource(name6, LoadFinish);
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 5)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }

	public void ReadConfig()
    {
        if (isLoadFinish == false || GameSystem.Instance.AttrNameConfigData.isReadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }
		Debug.Log("Config reading " + name1);
		ReadSkillActionConfig();
		
		Debug.Log("Config reading " + name2);
		ReadSkillAttrConfig();
		
		Debug.Log("Config reading " + name3);
		ReadSkillSlotConfig();
		
		Debug.Log("Config reading " + name4);
		ReadSkillLevelConfig();
		
		Debug.Log("Config reading " + name6);
		//ReadSkillWeightsConfig();
		ReadSkillEffectsConfig();
    }

	private void ReadSkillLevelConfig()
	{
		string text = ResourceLoadManager.Instance.GetConfigText(name3);
		if (text == null)
		{
			Debug.LogError("LoadConfig failed: " + name3);
			return;
		}
		//读取以及处理XML文本的类
		XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name3, text);
		//解析xml的过程
		XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        Debug.Log("Start ReadSkilLevelConfig Parsing!");
		foreach (XmlElement xe in nodelist)
		{
			XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
			if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
				continue;
			uint skill_id = 0;
			SkillLevel level = new SkillLevel();
			foreach (XmlElement xel in xe)
			{
				if(xel.Name == "id")
				{
					uint.TryParse(xel.InnerText, out skill_id);
				}
				else if (xel.Name == "level")
				{
					uint.TryParse(xel.InnerText, out level.level);
				}
				else if (xel.Name == "consume")
				{
					SkillConsumable consumable = new SkillConsumable();
					string[] array = xel.InnerText.Split('&');
					foreach (string items in array)
					{
						string[] item = items.Split(':');
						if (item.Length == 2)
						{
							uint id, v;
							uint.TryParse(item[0], out consumable.consumable_id);
							uint.TryParse(item[1], out consumable.consumable_quantity);
						}
					}
                    level.consumables.Add(consumable);
				}
				else if (xel.Name == "addn_attr")
				{
					string[] array = xel.InnerText.Split('&');
					foreach (string items in array)
					{
						string[] item = items.Split(':');
						if (item.Length == 2)
						{
							uint id, v;
							uint.TryParse(item[0], out id);
							uint.TryParse(item[1], out v);
							level.additional_attrs.Add(GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(id), v);
							
						}
					}
				}
				else if (xel.Name == "stama")
				{
					if ( xel.InnerText == "" )
						level.stama = 0;
					else
						level.stama = uint.Parse(xel.InnerText);
				}
				else if (xel.Name == "weight")
				{
					if ( xel.InnerText == "" )
						level.weight = 0;
					else
						level.weight = uint.Parse(xel.InnerText);
				}
				else if (xel.Name == "special_parameter")
				{
					string[] array = xel.InnerText.Split('&');
					foreach (string items in array)
					{
						string[] item = items.Split(':');
						if (item.Length == 2)
						{
							SkillSpec skillSpec = new SkillSpec();
							skillSpec.paramOp = SkillSpecParamOp.eAdd;

                            //float v = 0.0f;
							uint.TryParse(item[0], out skillSpec.paramId);
							if( !item[1].Contains("%") )
							{
                                //bool resu = float.TryParse(item[1], out v);
                                //Debug.Log("NOT %:str[" + item[1] + "]Result:" + resu);
                                skillSpec.value = IM.Number.Parse(item[1]);
							}
							else
							{
								item[1].Remove( item[1].Length - 1);
								skillSpec.paramOp = SkillSpecParamOp.eMulti;
								string strValue = item[1].TrimEnd('%');
                                //bool resu = float.TryParse(strValue, out v);
                                //Debug.Log("NOT %:str[" + strValue + "]Result:" + resu);
                                skillSpec.value = IM.Number.Parse(strValue);
							}
							level.parameters.Add(skillSpec.paramId, skillSpec);
						}
					}
				}
			}
			SkillAttr skill;
			if (skills.TryGetValue(skill_id, out skill))
			{
				skill.levels.Add(level.level, level);
			}
		}
        Debug.Log("SSS");
	}

	private void ReadSkillEffectsConfig()
	{
		string text = ResourceLoadManager.Instance.GetConfigText(name6);
		if (text == null)
		{
			Debug.LogError("LoadConfig failed: " + name6);
			return;
		}
		skillEffectItems.Clear();
		
		//读取以及处理XML文本的类
		XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name6, text);
		//解析xml的过程
		XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
			if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
				continue;
			
			SkillEffect skillEffect = new SkillEffect();
			if( !uint.TryParse(line.SelectSingleNode("Effects_id").InnerText, out skillEffect.id) )
				continue;
			
			XmlNode xel = line.SelectSingleNode("Effects_name");
			if (xel.InnerText != string.Empty)
				skillEffect.effectRes = xel.InnerText;
			 
			if( !uint.TryParse(line.SelectSingleNode("Effects_type").InnerText, out skillEffect.playBackType) )
				continue;

			xel = line.SelectSingleNode("Effects_Skeleton");
			if (xel.InnerText != string.Empty)
			{
				string effectSkeleton = xel.InnerText;
				string[] tokens = effectSkeleton.Split(':');

				skillEffect.tagPointOwner = uint.Parse(tokens[0]);
				string[] tagPoints = tokens[1].Split('&');
				skillEffect.tagPoints.AddRange(tagPoints);
			}

			xel = line.SelectSingleNode("Effects_MoveWithTagPoint");
			if (xel.InnerText != string.Empty)
			{
				skillEffect.moveWithTagPoint = uint.Parse(xel.InnerText);
				//if( skillEffect.moveWithTagPoint == 1 )
				//	Debug.Log("dont move with tagpoint.");
			}

			xel = line.SelectSingleNode("Effects_Frame");
			if (xel.InnerText != string.Empty)
			{
				string effectFrame = xel.InnerText;
				string[] tokens = effectFrame.Split(':');
				
				skillEffect.startFrame 	= uint.Parse(tokens[0]);
				skillEffect.endFrame 	= uint.Parse(tokens[1]);
			}
			skillEffectItems.Add(skillEffect.id, skillEffect);
		}
	}

	private void ReadSkillActionConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        actions.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SKILL_ACTION, text);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

			SkillAction action = new SkillAction();
			if( !uint.TryParse(line.SelectSingleNode("id").InnerText, out action.id) )
				continue;

			XmlNode xel = line.SelectSingleNode("interrupt");
			if (xel.InnerText != string.Empty)
			{
				string[] interrupts = xel.InnerText.Split('&');
				foreach (string interrupt in interrupts)
				{
					string[] tokens = interrupt.Split(':');
					SkillInterrupt intter;
					intter.type = int.Parse(tokens[0]);
					intter.id = int.Parse(tokens[1]);
					action.interrupts.Add(intter);
				}
			}
			string play_key = line.SelectSingleNode("play_key").InnerText;
			action.inputs = ParseKey(play_key);
			action.action_id = line.SelectSingleNode("action_id").InnerText;

			xel = line.SelectSingleNode("Effects_id");
			if (xel.InnerText != string.Empty)
			{
				string[] tokens = xel.InnerText.Split(':');
				foreach( string subString in tokens )
					action.skillEffects.Add(uint.Parse(subString));
			}
			xel = line.SelectSingleNode("camera_animation");
			if (xel != null)
				action.camera_animation = xel.InnerText;
			xel = line.SelectSingleNode("block_key");
			if (xel != null)
			{
				List<SkillInput> block_key = ParseKey(xel.InnerText);
				if( block_key.Count != 0 )
				{
					action.block_key = block_key[0];
					//Debug.Log("action.block_key: " + action.block_key.moveDir);
				}
			}
			actions.Add(action.id, action);
		}
	}

	private static List<SkillInput> ParseKey(string strParam)
	{
		List<SkillInput> lstSkillInput = new List<SkillInput>();
		if(strParam.Length == 0)
			return lstSkillInput;

		string[] keys = strParam.Split('/');
		foreach( string param in keys )
		{
			SkillInput newSkillInput = new SkillInput();
			//combo
			if( param.Contains("+") )
			{
				string[] dirAndKey = param.Split('+');
				_GetJoystickInput(dirAndKey[0], newSkillInput);
				newSkillInput.cmd = _GetKeyType(dirAndKey[1]);
			}
			else
			{
				if( _IsCommand(param) )
					newSkillInput.cmd = _GetKeyType(param);
				else
					_GetJoystickInput(param, newSkillInput);
			}
			lstSkillInput.Add(newSkillInput);
		}
		return lstSkillInput;
	}

	static void _GetJoystickInput( string param, SkillInput skillInput )
	{
		string[] typeAndDir  = param.Split('&');
		skillInput.inputType = _GetInputDirType(typeAndDir[0]);
		skillInput.moveDir 	 = _GetMoveDir(typeAndDir[1]);
		if (typeAndDir.Length > 2)
			skillInput.moveAngleRange = _GetMoveAngleRange(typeAndDir[2]);
	}

	static Command _GetKeyType( string strType )
	{
		int key = 0;
		if( !int.TryParse(strType, out key) )
			return Command.None;

		return (Command)key;
	}

	static bool _IsCommand(string param)
	{
		int key = 0;
		return int.TryParse(param, out key);
	}

	static InputDirType _GetInputDirType( string strType )
	{
		InputDirType dirType = InputDirType.eBasket;
		if( strType == "basket" )
			dirType = InputDirType.eBasket;
		else if( strType == "world" )
			dirType = InputDirType.eJoyStick;

		return dirType;
	}

	static EDirection _GetMoveDir( string strType )
	{
		EDirection dirType = EDirection.eNone;
		if( strType == "forward" )
			dirType = EDirection.eForward;
		else if( strType == "back" )
			dirType = EDirection.eBack;
		else if( strType == "left" )
			dirType = EDirection.eLeft;
		else if( strType == "right" )
			dirType = EDirection.eRight;
		else
			Debug.LogError("invalid move dir type");
		return dirType;
	}

	static IM.Vector2 _GetMoveAngleRange( string strType )
	{
		string[] tokens = strType.Split('~');
		IM.Number from = IM.Number.Parse(tokens[0]);
		IM.Number to = IM.Number.Parse(tokens[1]);
		return new IM.Vector2(from, to);
	}


    private void ReadSkillAttrConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        basic_skills.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SKILL_ATTR, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            SkillAttr data = new SkillAttr();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out data.id);
                }
                else if (xel.Name == "name")
                {
                    data.name = xel.InnerText;
                }
                else if (xel.Name == "icon")
                {
                    data.icon = xel.InnerText;
                }
				else if (xel.Name == "actions")
				{
					string[] tokens = xel.InnerText.Split('/');
					uint actionId;
					SkillAction action;
					foreach (string token in tokens)
					{
						if (uint.TryParse(token, out actionId))
						{
							if (!actions.TryGetValue(actionId, out action))
								continue;
							data.actions.Add(action);
						}
					}
				}
                else if (xel.Name == "intro")
                {
                    data.intro = xel.InnerText;
                }
                else if (xel.Name == "cast")
                {
                    data.cast = xel.InnerText;
                }
                else if (xel.Name == "type")
                {
                    uint value;
                    uint.TryParse(xel.InnerText, out value);
                    data.type = (SkillType)value;
                }
                else if (xel.Name == "subtype")
                {
                    uint value;
                    uint.TryParse(xel.InnerText, out value);
                    data.subtype = (SkillSubType)value;
                }
                else if (xel.Name == "actiontype")
                {
                    uint value;
                    uint.TryParse(xel.InnerText, out value);
                    data.action_type = value;
                }
				else if (xel.Name == "sideeffect")
				{
					string[] sideeffects = xel.InnerText.Split('&');
					foreach (string effect in sideeffects)
					{
						if (string.IsNullOrEmpty(effect))
							continue;
						string[] tokens = effect.Split(':');
						SkillSideEffect side_effect = new SkillSideEffect();
						side_effect.type = int.Parse(tokens[0]);
						side_effect.value = IM.Number.Parse(tokens[1]);
						data.side_effects.Add(side_effect.type, side_effect);
					}
				}
                else if (xel.Name == "position")
                {
                    string[] positions = xel.InnerText.Split('&');
                    foreach (string pos in positions)
                    {
                        int position;
                        if (int.TryParse(pos, out position))
                        {
                            data.positions.Add(position);
                        }
                    }
                }
                else if (xel.Name == "role")
                {
                    string[] roles = xel.InnerText.Split('&');
                    foreach (string role in roles)
                    {
                        uint role_id;
                        if (uint.TryParse(role, out role_id))
                        {
                            data.roles.Add(role_id);
                        }
                    }
                }
                else if (xel.Name == "use_area")
                {
                    string[] areas = xel.InnerText.Split('&');
                    foreach (string area in areas)
                    {
                        uint area_id;
                        if (uint.TryParse(area, out area_id))
                        {
                            data.area.Add(area_id);
                        }
                    }
                }
                else if (xel.Name == "condition")
                {
                    string[] tokens = xel.InnerText.Split('&');
                    foreach (string token in tokens)
                    {
                        int condition;
                        if (!int.TryParse(token, out condition))
							continue;
                        data.condition.Add(condition);
                    }
                }
				else if (xel.Name == "attrange")
				{
                    IM.Number tempAttrange;
                    if (!IM.Number.TryParse(xel.InnerText, out tempAttrange))
                        continue;
                    data.attrange = tempAttrange;
				}
            }
			for (int i = 1; ; ++i )
			{
				XmlNode nodeid = xe.SelectSingleNode("equip_cond_id" + i);
				if (nodeid == null)
					break;
				XmlNode nodevalue = xe.SelectSingleNode("equip_cond_value" + i);
				if (nodevalue == null)
					break;
				uint id;
				uint value;
				if (uint.TryParse(nodeid.InnerText, out id) && uint.TryParse(nodevalue.InnerText, out value))
					data.equip_conditions.Add(id, value);
			}
            try
            {
                skills.Add(data.id, data);
                if (data.type == SkillType.ACTIVE && data.subtype == SkillSubType.BASIC)
                    basic_skills.Add(data);
            }
            catch (ArgumentException ex)
            {
				Debug.LogError(ex.Message);
            }
        }
    }

    private void ReadSkillSlotConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name4);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name4);
            return;
        }
        slots.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SKILL_SLOT, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            SkillSlot slot = new SkillSlot();
            foreach (XmlElement xel in xe)
            {
                if(xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out slot.id);
                }
                else if (xel.Name == "need_level")
                {
                    uint.TryParse(xel.InnerText, out slot.level_required);
                }
            }
            for (uint i = 1; ; ++i)
            {
                XmlElement elemId = xe.SelectSingleNode("consume_id" + i) as XmlElement;
                if (elemId == null)
                    break;
                XmlElement elemValue = xe.SelectSingleNode("consume_value" + i) as XmlElement;
                if (elemValue == null)
                    break;
                SkillConsumable consumable = new SkillConsumable();
                uint.TryParse(elemId.InnerText, out consumable.consumable_id);
                uint.TryParse(elemValue.InnerText, out consumable.consumable_quantity);
                slot.consumables.Add(consumable);
            }
            slots.Add(slot.id, slot);
        }
    }

	/*
	private void ReadSkillWeightsConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name5);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name5);
            return;
        }
        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SKILL_WEIGHT, text);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

			uint skill_id = uint.Parse(line.SelectSingleNode("skill_ID").InnerText);
			SkillAttr skill;
			if (skills.TryGetValue(skill_id, out skill))
			{
				for (int i = 0; i < 6; ++i)
				{
					string txt = line.SelectSingleNode("LV" + (i + 1) + "_weight").InnerText;
					uint.TryParse(txt, out skill.weights[i]);
				}
			}
		}
	}
	*/

    public SkillAttr GetSkill(uint skill_id)
    {
        SkillAttr skill;
        if (skills.TryGetValue(skill_id, out skill))
        {
            return skill;
        }
        else
        {
            return null;
        }
    }

    public SkillSlot GetSlot(uint slot_index)
    {
        SkillSlot slot;
        if (slots.TryGetValue(slot_index, out slot))
        {
            return slot;
        }
        else
        {
            return null;
        }
    }
}
