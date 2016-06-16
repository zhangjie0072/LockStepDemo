using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class SelectSquad : MonoBehaviour
{
	GameObject back;
	GameObject ok;
	UIWidget roleList;
	UIScrollView list;
	UIGrid listGrid;
	UIGrid squadGrid;
	UILabel roleNum;

	GameObject prefabRoleItem;

	RoleItem[] squadItems = new RoleItem[3];
	List<RoleItem> listItems = new List<RoleItem>();

	PositionType curPosition = PositionType.PT_TOTAL;

	[HideInInspector]
	public bool single = false;
	public uint[] assistantsID = new uint[2];
	public bool selectCompleted { get { return single || (assistantsID[0] != 0 && assistantsID[1] != 0); } }
	public string okLabel { set { ok.GetComponentInChildren<UILabel>().text = value; } }

	public UIEventListener.VoidDelegate onBack
	{
		get { return UIEventListener.Get(back).onClick; }
		set { UIEventListener.Get(back).onClick = value; }
	}

	public UIEventListener.VoidDelegate onOK
	{
		get { return UIEventListener.Get(ok).onClick; }
		set { UIEventListener.Get(ok).onClick = value; }
	}


	void Awake()
	{
		Transform window = transform.FindChild("Window");

		back = window.FindChild("Back").gameObject;
		ok = window.FindChild("OK").gameObject;
		roleList = window.FindChild("RoleList").GetComponent<UIWidget>();
		list = window.FindChild("RoleList/List").GetComponent<UIScrollView>();
		listGrid = window.FindChild("RoleList/List/Grid").GetComponent<UIGrid>();
		squadGrid = window.FindChild("Grid").GetComponent<UIGrid>();
		roleNum = window.FindChild("RoleNum").GetComponent<UILabel>();

		UIEventListener.Get(window.FindChild("RoleList/Tabs/All").gameObject).onClick +=
			(GameObject) => { curPosition = PositionType.PT_TOTAL; FilterListItem(); };
		UIEventListener.Get(window.FindChild("RoleList/Tabs/C").gameObject).onClick +=
			(GameObject) => { curPosition = PositionType.PT_C; FilterListItem(); };
		UIEventListener.Get(window.FindChild("RoleList/Tabs/PF").gameObject).onClick +=
			(GameObject) => { curPosition = PositionType.PT_PF; FilterListItem(); };
		UIEventListener.Get(window.FindChild("RoleList/Tabs/SF").gameObject).onClick +=
			(GameObject) => { curPosition = PositionType.PT_SF; FilterListItem(); };
		UIEventListener.Get(window.FindChild("RoleList/Tabs/PG").gameObject).onClick +=
			(GameObject) => { curPosition = PositionType.PT_PG; FilterListItem(); };
		UIEventListener.Get(window.FindChild("RoleList/Tabs/SG").gameObject).onClick +=
			(GameObject) => { curPosition = PositionType.PT_SG; FilterListItem(); };

        prefabRoleItem = ResourceLoadManager.Instance.LoadPrefab("Prefab/Items/RoleItem") as GameObject;
	}

	void Start()
	{
		InitSquadItem();
		InitListItem();
		FilterListItem();
		RefreshRoleNum();
	}

	void InitSquadItem()
	{
		squadItems[0] = CommonFunction.InstantiateObject(prefabRoleItem, squadGrid.transform).GetComponent<RoleItem>();
		squadItems[0].gameObject.name += "0(Captain)";
		NGUITools.BringForward(squadItems[0].gameObject);
		squadItems[0].status = RoleItem.Status.Captain;
		squadItems[0].role_id = MainPlayer.Instance.CaptainID;
		if (!single)
		{
			for (int i = 1; i <= 2; ++i)
			{
				squadItems[i] = CommonFunction.InstantiateObject(prefabRoleItem, squadGrid.transform).GetComponent<RoleItem>();
				squadItems[i].gameObject.name += i + "(Assistant)";
				NGUITools.BringForward(squadItems[i].gameObject);
				if (assistantsID[i - 1] != 0)
				{
					squadItems[i].status = RoleItem.Status.Selected;
					squadItems[i].role_id = assistantsID[i - 1];
				}
				else
				{
					squadItems[i].status = RoleItem.Status.None;
				}
				squadItems[i].onReplace += OnReplace;
			}
		}
	}

	void InitListItem()
	{
		listGrid.cellHeight = prefabRoleItem.GetComponent<UIWidget>().height;
		int index = 0;
		foreach (Player player in MainPlayer.Instance.PlayerList)
		{
			RoleItem item = CommonFunction.InstantiateObject(prefabRoleItem, listGrid.transform).GetComponent<RoleItem>();
			item.gameObject.name += (index++).ToString();
			listItems.Add(item);
			NGUITools.BringForward(item.gameObject);
			item.GetComponent<UIWidget>().width = roleList.width;
			item.status = RoleItem.Status.Candidate;
			item.role_id = player.m_roleInfo.id;
			item.onJoin += OnJoin;
		}
	}

	void OnReplace(RoleItem roleItem)
	{
		if (assistantsID[0] == roleItem.role_id)
			assistantsID[0] = 0;
		else if (assistantsID[1] == roleItem.role_id)
			assistantsID[1] = 0;
		roleItem.status = RoleItem.Status.None;
		FilterListItem();
		RefreshRoleNum();
	}

	void OnJoin(RoleItem roleItem)
	{
		if (single) return;

		if (assistantsID[0] == 0)
		{
			assistantsID[0] = roleItem.role_id;
			squadItems[1].role_id = roleItem.role_id;
			squadItems[1].status = RoleItem.Status.Selected;
			FilterListItem();
			RefreshRoleNum();
		}
		else if (assistantsID[1] == 0)
		{
			assistantsID[1] = roleItem.role_id;
			squadItems[2].role_id = roleItem.role_id;
			squadItems[2].status = RoleItem.Status.Selected;
			FilterListItem();
			RefreshRoleNum();
		}
	}

	void RefreshRoleNum()
	{
		if (single)
		{
			roleNum.text = "1/1";
		}
		else
		{
			uint num = 1;
			if (assistantsID[0] != 0)
				++num;
			if (assistantsID[1] != 0)
				++num;
			roleNum.text = num + "/3";
		}
	}

	void FilterListItem()
	{
		int index = 0;
		foreach (RoleItem item in listItems)
		{
			bool enabled = (assistantsID[0] != item.role_id && assistantsID[1] != item.role_id);
			enabled &= (item.position == curPosition || curPosition == PositionType.PT_TOTAL);
			NGUITools.SetActive(item.gameObject, enabled);
			if (enabled)
				item.index = index++;
		}
		listGrid.Reposition();
		list.ResetPosition();
	}
}
