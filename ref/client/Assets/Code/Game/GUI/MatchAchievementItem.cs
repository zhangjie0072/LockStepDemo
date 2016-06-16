using UnityEngine;
using System.Collections;
using fogs.proto.config;

public class MatchAchievementItem : MonoBehaviour
{
	UIWidget widget;
	UISprite background;
	Transform iconTransform;
	GameObject icon;
	UILabel name;
	UILabel title;
	UILabel intro;
	GameObject glow;
	UITweener tail;
	UITweener slideIn;
	UITweener moveUp;
	UITweener fadeAway;

	public string bgPrefix;
	public Player player;
	public MatchAchievement matchAchievement;

	void Awake()
	{
		widget = transform.FindChild("Widget").GetComponent<UIWidget>();
		background = widget.transform.FindChild("Background").GetComponent<UISprite>();
		iconTransform = widget.transform.FindChild("Icon");
		name = widget.transform.FindChild("NameBar/Name").GetComponent<UILabel>();
		title = widget.transform.FindChild("Title").GetComponent<UILabel>();
		intro = widget.transform.FindChild("Intro").GetComponent<UILabel>();
		glow = widget.transform.FindChild("Glow").gameObject;
		tail = widget.transform.FindChild("Tail").GetComponent<UITweener>();
		UITweener[] tweens = widget.GetComponents<UITweener>();
		slideIn = tweens[0];
		moveUp = tweens[1];
		fadeAway = tweens[2];
		slideIn.SetOnFinished(() => { fadeAway.enabled = true; });
		moveUp.SetOnFinished(() => { fadeAway.enabled = true; });
		fadeAway.SetOnFinished(() => { NGUITools.Destroy(gameObject); });
		icon = CommonFunction.InstantiateObject("Prefab/GUI/CareerRoleIcon", iconTransform);
	}

	void Start()
	{
		int shapeID = 0;
		RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(player.m_roleInfo.id);
		LuaComponent iconLua = icon.transform.GetComponent<LuaComponent>();
		if (data != null)
		{
			//icon.spriteName = data.icon;
			//shapeID = data.shape;
		}
		else
		{
			NPCConfig npc = GameSystem.Instance.NPCConfigData.GetConfigData(player.m_roleInfo.id);
			//icon.spriteName = npc.icon;
			//shapeID = (int)npc.shape;
			iconLua.table.Set("npc", true);
		}
		iconLua.table.Set("id", player.m_roleInfo.id);
		iconLua.table.Set("showPosition", false);
		
        //if (1000 <= shapeID && shapeID < 1500)
        //    icon.atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconPortrait");
        //else if (1500 <= shapeID && shapeID < 1800)
        //    icon.atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconPortrait_1");
        //else if (1800 <= shapeID && shapeID < 2000)
        //    icon.atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconPortrait_2");
        //else
        //    icon.atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconPortrait_3");
		background.spriteName = bgPrefix + matchAchievement.level;
		name.text = player.m_name;
		title.text = matchAchievement.title;
		intro.text = string.Format(matchAchievement.intro, player.mStatistics.GetStatValue(matchAchievement.type));

		if (matchAchievement.level == 3)
		{
			NGUITools.SetActive(glow, true);
			NGUITools.SetActive(tail.gameObject, true);
			tail.enabled = true;
			if (!moveUp.enabled)
				slideIn.enabled = true;
		}
		else
		{
			NGUITools.SetActive(glow, false);
			NGUITools.SetActive(tail.gameObject, false);
			tail.enabled = false;
			slideIn.enabled = false;
			fadeAway.enabled = true;
		}
	}

	public void MoveUp()
	{
		moveUp.enabled = true;
		slideIn.enabled = false;
	}
}
