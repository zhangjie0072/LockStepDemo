using System;
using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

//public class ChapterSection2
//{
//    public uint roleID;
//    public string chapter_id;
//    public string section_id;
//}



public class RoleBaseData2
{
    public int id;
    public int display;
    public int bias;
    public int position;
    public string icon;
    public string icon_bust;
    public string icon_bg;
    public int shape;
    public string name;
    public string intro;
    public string specialityInfo;
    public int gender;
	public int init_star;
	public int talent;
	public IM.Number talent_value;
    public List<uint> training_slots = new List<uint>();
	//public List<KeyValuePair<uint, bool>> training_skill = new List<KeyValuePair<uint, bool>>();
	public List<uint> training_skill_all = new List<uint>(); //全部技能列表
	public List<uint> training_skill_show = new List<uint>(); //需要显示的技能列表
    
	public List<int> special_skills  = new List<int>(); // Hold(保留字段)
    public uint special_skill; // Hold(保留字段)
    
    //public Dictionary<uint, uint> special_attrs = new Dictionary<uint, uint>(); //Hold(保留字段)
    public uint special_attr = 0; //在RoleBase配置中的special_attr中配置天赋ID，使指定球员拥有对应ID天赋

    public Dictionary<uint, uint> recruit_consume = new Dictionary<uint, uint>(); //消费类型、消费金额
    public string recruit_consume_string; //recruit_consume 首字母等于@，显示则显示纯文字内容
    //public uint recruit_consume_id;
    //public uint recruit_consume_value;
    public uint recruit_output_id;
    public uint recruit_output_value;
    public string access_way;
    public string goodAt;
    public string init_anim;
    public string display_effect;
    public List<string> other_anims;
    public List<string> player_sound = new List<string>();
    


    public Dictionary<uint, uint> attrs = new Dictionary<uint, uint>(); // id-value.
    public Dictionary<string, uint> attrs_symbol = new Dictionary<string, uint>(); // symbol -value.

	public List<uint> match_msg_ids = new List<uint>();

    //public int type;
    //public int init_state;

    
    
    
    //public int quality;
    //public int quality_min;
    //public int quality_max;


  
    //public int buy_consume;

}

//public class CaptainListData
//{
//    public uint id;
//    public uint position;
//}
public class BaseDataConfig2
{
    string name = GlobalConst.DIR_XML_ROLEBASE;
    bool isLoadFinish = false;
    private object LockObject = new object();

    //public Dictionary<uint, List<string>> ChapterSectionPath = new Dictionary<uint, List<string>>();
    public static Dictionary<uint, RoleBaseData2> roleBaseDatas = new Dictionary<uint, RoleBaseData2>();
    //public static List<RoleBaseData> RoleBaseDatas = new List<RoleBaseData>();
   // public static List<ChapterSection2> paths = new List<ChapterSection2>();

    //private List<RoleBaseData> roleDataList = new List<RoleBaseData>();
    //private List<RoleBaseData> captainDataList = new List<RoleBaseData>();

    //public static List<CaptainListData> AllMaleCaptainDataList = new List<CaptainListData>();//所有男队长
    //public static List<CaptainListData> AllFemalCaptainDataList = new List<CaptainListData>();//所有女队长

    //public Dictionary<uint,fogs.proto.config.RoleBaseConfig2> RoleBaseDatas2 = new Dictionary<uint,fogs.proto.config.RoleBaseConfig2> ();

    public BaseDataConfig2()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ROLEBASE, LoadFinish);
        //ReadRoleBaseData();
    }

    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

    public uint GetAttrValue( uint roleId, uint attrid )
    {
        RoleBaseData2 rd = roleBaseDatas[roleId];
		uint value;
		if (!rd.attrs.TryGetValue(attrid, out value))
		{
            if (attrid == GlobalConst.ALL_HEDGING_ID)
                return 0;
            if (GameSystem.Instance.AttrNameConfigData.IsFactor(attrid))
                return 0;
			Debug.LogError("No attr: " + attrid + " role ID: " + roleId);
		}
        return value;
    }
    public void ReadConfig()
    {
        if (isLoadFinish == false || GameSystem.Instance.AttrNameConfigData.isReadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name);
            return;
        }
        roleBaseDatas.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ROLEBASE, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            RoleBaseData2 data = new RoleBaseData2();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "ID")
                {
                    int.TryParse(xel.InnerText, out data.id);
                }
                if (xel.Name == "display")
                {
                    int.TryParse(xel.InnerText, out data.display);
                }
                if (xel.Name == "bias")
                {
                    int.TryParse(xel.InnerText, out data.bias);
                }
                else if (xel.Name == "position")
                {
                    int.TryParse(xel.InnerText, out data.position);
                }
                else if (xel.Name == "icon")
                {
                    data.icon = xel.InnerText;
                }
                else if (xel.Name == "icon_bust")
                {
                    data.icon_bust = xel.InnerText;
                }
                else if (xel.Name == "icon_bg")
                {
                    data.icon_bg = xel.InnerText;
                }
                else if (xel.Name == "shape")
                {
                    int.TryParse(xel.InnerText, out data.shape);
                }
                else if (xel.Name == "name")
                {
                    data.name = xel.InnerText;
                }
                else if (xel.Name == "intro")
                {
                    data.intro = xel.InnerText;
                }
                else if (xel.Name == "gender")
                {
                    int.TryParse(xel.InnerText, out data.gender);
                }
                else if (xel.Name == "goodAt")
                {
                    data.goodAt = xel.InnerText;
                }
                else if (xel.Name == "init_star")
                {
                    int.TryParse(xel.InnerText, out data.init_star);
                }
                else if(xel.Name == "specialityInfo")
                {
                    data.specialityInfo = xel.InnerText;
                }
                else if (xel.Name == "talent")
                {
                    string[] vs = xel.InnerText.Split(':');

                    int talent;
                    IM.Number talent_value;
                    int.TryParse(vs[0], out talent);
                    IM.Number.TryParse(vs[1], out talent_value);
                    data.talent = talent;
                    data.talent_value = talent_value;
                }
                else if (xel.Name == "training_slots")
                {
                    string[] vs = xel.InnerText.Split('&');
                    foreach (string v in vs)
                    {
                        uint slot;
                        uint.TryParse(v, out slot);
                        data.training_slots.Add(slot);
                    }
                }
				else if (xel.Name == "training_skill")
				{
					if( xel.InnerText.Length != 0 )
					{
						string[] skills = xel.InnerText.Split('&');
						for (int i = 0; i < skills.Length; ++i)
						{
							string[] sk = skills[i].Split(':');
							uint value = 0, key = 0;
							uint.TryParse(sk[0], out key);
							uint.TryParse(sk[1], out value);
							if (value == 1)
							{
								data.training_skill_show.Add(key);
							}
							data.training_skill_all.Add(key);
						}
					}
				}
                else if (xel.Name == "special_skill")
                {
                    // Hold（保留字段
                    uint.TryParse(xel.InnerText, out data.special_skill);
                }
                else if (xel.Name == "special_skill")
                {
                    // Hold（保留字段
                    uint.TryParse(xel.InnerText, out data.special_skill);
                }
                else if (xel.Name == "recruit_consume")
                {
                    if (xel.InnerText.StartsWith("@"))
                    {
                        data.recruit_consume_string = xel.InnerText.Substring(1);
                    }
                    else
                    {
                        string[] attrs = xel.InnerText.Split('&');
                        foreach (string attr in attrs)
                        {
                            string[] item = attr.Split(':');

                            if (item.Length == 2)
                            {
                                uint id;
                                uint value;
                                uint.TryParse(item[0], out id);
                                uint.TryParse(item[1], out value);
                                data.recruit_consume.Add(id, value);
                            }
                        }
                    }

                }
                else if (xel.Name == "recruit_output")
                {
                    string[] vs = xel.InnerText.Split(':');


                    if (vs.Length == 2)
                    {
                        uint recruit_output_id;
                        uint recruit_output_value;
                        uint.TryParse(vs[0], out recruit_output_id);
                        uint.TryParse(vs[1], out recruit_output_value);
                        data.recruit_output_id = recruit_output_id;
                        data.recruit_output_value = recruit_output_value;
                    }

                }
                else if (xel.Name == "access_way")
                {
                    data.access_way = xel.InnerText;
                    //string[] chapter = data.access_way.Split('&');
                    //foreach (string chap in chapter)
                    //{
                    //    if (ChapterSectionPath.ContainsKey((uint)data.id) == false)
                    //    {
                    //        ChapterSectionPath[(uint)data.id] = new List<string>();
                    //    }
                    //    ChapterSectionPath[(uint)data.id].Add(chap);
                    //}
                    //if (ChapterSectionPath.ContainsKey((uint)data.id))
                    //{
                    //    for (int i = 0; i < ChapterSectionPath[(uint)data.id].Count; i++)
                    //    {
                    //        string path = ChapterSectionPath[(uint)data.id][i];
                    //        int pos = path.IndexOf(':');
                    //        if (pos == -1)
                    //            continue;
                    //        string chapterID = path.Substring(0, pos);
                    //        string sectionID = path.Substring(pos + 1, chapterID.Length);
                    //        ChapterSection chapPath = new ChapterSection();
                    //        chapPath.roleID = (uint)data.id;
                    //        chapPath.chapter_id = chapterID;
                    //        chapPath.section_id = sectionID;
                    //        paths.Add(chapPath);
                    //    }
                    //}
                }
                else if (xel.Name == "attrs")
                {
                    string[] attrs = xel.InnerText.Split('&');
                    foreach (string attr in attrs)
                    {
                        string[] item = attr.Split(':');

                        if (item.Length == 2)
                        {
                            uint id;
                            uint value;
                            uint.TryParse(item[0], out id);
                            uint.TryParse(item[1], out value);
                            data.attrs.Add(id, value);
                            string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(id);
                            data.attrs_symbol.Add(symbol, value);
                        }
                    }
                }
                else if (xel.Name == "display_action" && !string.IsNullOrEmpty(xel.InnerText)) 
                {
                    string[] anims = xel.InnerText.Split(':');
                    data.init_anim = anims[0];
                    if (anims[1].Length > 0)
                    {
                        data.other_anims = new List<string>();
                        foreach (string other_anim in anims[1].Split('&'))
                        {
                            data.other_anims.Add(other_anim);
                        }
                    }
                }

                //else if (xel.Name == "quality")
                //{
                //    int.TryParse(xel.InnerText, out data.quality);
                //}

                //else if (xel.Name == "quality_min")
                //{
                //    int.TryParse(xel.InnerText, out data.quality_min);
                //}

                //else if (xel.Name == "quality_max")
                //{
                //    int.TryParse(xel.InnerText, out data.quality_max);
                //}

          
                //else if(xel.Name == "special_skill")
                //{
                //    uint.TryParse(xel.InnerText, out data.special_skill);
                //}
                else if (xel.Name == "special_attr")
                {
                    uint.TryParse(xel.InnerText, out data.special_attr);
                    //string[] attrs = xel.InnerText.Split('&');
                    //for (int i = 0; i < attrs.Length; ++i)
                    //{
                    //    string[] items = attrs[i].Split(':');
                    //    if (items.Length == 2)
                    //    {
                    //        uint k, v;
                    //        uint.TryParse(items[0], out k);
                    //        uint.TryParse(items[1], out v);
                    //        data.special_attrs.Add(k, v);
                    //    }
                    //}
                }
                else if (xel.Name == "sound") 
                {
                    if (!string.IsNullOrEmpty(xel.InnerText)) 
                    {
                        data.player_sound.Add(xel.InnerText);
                    }
                }
				else if (xel.Name == "match_msg_id") 
				{
					if (!string.IsNullOrEmpty(xel.InnerText)) 
					{
						string[] strMsgIds = xel.InnerText.Split('&');
						foreach( string strMsgId in strMsgIds )
							data.match_msg_ids.Add( uint.Parse(strMsgId) );
					}
				}
				
				else if (xel.Name == "display_effect") 
                {
                    if (!string.IsNullOrEmpty(xel.InnerText)) 
                    {
                        data.display_effect = xel.InnerText;
                    }
                }

     
                //else if (xel.Name == "buy_consume")
                //{
                //    int.TryParse(xel.InnerText, out data.buy_consume);
                //}
            }
            if (roleBaseDatas.ContainsKey((uint)data.id) == false)
            {
                roleBaseDatas[(uint)data.id] = data;
            }
		}
		
    }

    public RoleBaseData2 GetConfigData(uint roleID)
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            return roleBaseDatas[roleID];
        }
        return null;
    }

    public IM.Number GetTalent(uint roleID)
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            return roleBaseDatas[roleID].talent_value;
        }
        return IM.Number.one;
    }

    public int GetIntTalent(uint roleID)
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            return roleBaseDatas[roleID].talent;
        }
        return 1;
    }

    public List<RoleBaseData2> GetConfig()
    {
        List<RoleBaseData2> tempList = new List<RoleBaseData2>();
        foreach (RoleBaseData2 value in roleBaseDatas.Values)
        {
            tempList.Add(value);
        }
        return tempList;
    }
    //获取我的球员列表
    //public List<RoleBaseData> GetMyRoleConfig()
    //{
    //    return roleDataList;
    //}
    ////获取可选的队长列表
    //public List<RoleBaseData> GetCaptainRoleConfig()
    //{
    //    return captainDataList;
    //}

    public List<string> GetIconList()
    {
        List<string> iconList = new List<string>();
        foreach (RoleBaseData2 value in roleBaseDatas.Values)
        {
            iconList.Add(value.icon);
        }
        return iconList;
    }

    public string GetIcon(uint roleID)
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            return roleBaseDatas[roleID].icon;
        }
        return null;
    }

    public uint GetIDByIcon(string icon)
    {
        foreach (RoleBaseData2 value in roleBaseDatas.Values)
        {
            if (value.icon == icon)
                return (uint)value.id;
        }
        return 0;
    }
    //根据性别和职业获取ID
    //public uint GetIDByGenderAndPosition(int position, int gender)
    //{
    //    foreach (RoleBaseData value in RoleBaseDatas.Values)
    //    {

    //        if (value.type == 1 && value.init_state == 1)
    //        {
    //            if (value.gender == gender && value.position == position)
    //            {
    //                return (uint)value.id;
    //            }
    //        }
    //    }
    //    return 0;
    //}


    public PositionType GetPosition(uint roleID)
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            return (PositionType)roleBaseDatas[roleID].position;
        }
        return PositionType.PT_NONE;
    }

    public string GetIntro(uint roleID)
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            return roleBaseDatas[roleID].intro;
        }
        return null;
    }
    /// <summary>
    /// 通过ID找到球员当前品阶 刘佳力 10-28
    /// </summary>
    /// <param name="roleID"></param>
    /// <returns></returns>
    //public uint GetQualityByID(uint roleID)
    //{
    //    if (roleBaseDatas.ContainsKey(roleID))
    //    {
    //        return (uint)roleBaseDatas[roleID].quality;
    //    }
    //    return 0;
    //}
    /// <summary>
    /// 通过ID找到球员的最高品阶
    /// </summary>
    /// <param name="roleID"></param>
    /// <returns></returns>
    //public uint GetMaxQualityByID(uint roleID)
    //{
    //    if (roleBaseDatas.ContainsKey(roleID))
    //    {
    //        return (uint)roleBaseDatas[roleID].quality_max;
    //    }
    //    return 0;
    //}

    //public bool CheckIsAssistant(uint roleID)
    //{
    //    return roleBaseDatas[roleID].type == 2 ? true : false;
    //}

    public uint GetInitAnimationID(uint roleID) 
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            string anim = roleBaseDatas[roleID].init_anim;
            if (anim != null)
            {
                string anim_id = anim.Substring(4);
                return uint.Parse(anim_id);
            }
            else
                return 0;
        }
        return 0;
    }

    public List<uint> GetOtherAnimationsID(uint roleID)
    {
        if (roleBaseDatas.ContainsKey(roleID))
        {
            List<uint> otherAnims = new List<uint>();
            foreach (string anim in roleBaseDatas[roleID].other_anims)
            {
                otherAnims.Add(uint.Parse(anim.Substring(4)));
            }
            return otherAnims;
        }
        return null;
    }

    public uint GetRandomAnimationID(uint roleID) 
    {
        System.Random random = new System.Random();
        int ran = random.Next(1, roleBaseDatas[roleID].other_anims.Count + 1);
        string anim = roleBaseDatas[roleID].other_anims[ran - 1];
        if (anim != null)
            return uint.Parse(anim.Substring(4));
        return 0;
    }

    public List<string> GetPlayerSoundByID(uint roleID) 
    {
        if (roleBaseDatas.ContainsKey(roleID) && roleBaseDatas[roleID].player_sound.Count > 0)
        {
            return roleBaseDatas[roleID].player_sound;
        }
        return null;
    }

    public string GetPlayerEffectByID(uint roleID) 
    {
        if (roleBaseDatas.ContainsKey(roleID)) 
        {
            string effectName = roleBaseDatas[roleID].display_effect;
            if (!string.IsNullOrEmpty(effectName))
                return effectName;
        }
        return null;
    }
}
