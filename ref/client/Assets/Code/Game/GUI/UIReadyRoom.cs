using fogs.proto.msg;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIReadyRoom : MonoBehaviour
{
    private GameObject _role_item_prefab;
    private UIWidget _window;
    private UIButton _button_start;
	private FrameAnimation _button_start_text_anim;
	private RoleItemReady _my_role_item;
	private RoleItemReady _selected_role_item;
	private RoleItemReady[] _role_item_list = new RoleItemReady[3];
    protected SelectSquad _select_role;

    private GameMatch_Ready _match;
    public GameMatch_Ready match
    {
        get { return _match; }
        set
        {
            _match = value;

			_selected_role_item = _my_role_item;
            SelectRole();
            OnSelectRoleOK(null);
        }
    }

	private uint roleCount;

    void Awake()
    {
        _window = transform.FindChild("Window").GetComponent<UIWidget>();
        UIEventListener.Get(transform.FindChild("Window/Back").gameObject).onClick = OnBack;
        UIEventListener.Get(transform.FindChild("Window/ChangeRoom").gameObject).onClick = OnChangeRoomClicked;
        UIEventListener.Get(transform.FindChild("Window/Inventory").gameObject).onClick = OnInventoryClicked;
        UIEventListener.Get(transform.FindChild("Window/Social").gameObject).onClick = OnSocialClicked;
        UIEventListener.Get(transform.FindChild("Window/Skill").gameObject).onClick = OnSkillClicked;
        _button_start = transform.FindChild("Window/Start").GetComponent<UIButton>();
        UIEventListener.Get(_button_start.gameObject).onClick = OnStartClicked;
		_button_start_text_anim = transform.FindChild("Window/Start/Text").GetComponent<FrameAnimation>();
        _role_item_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/RoleItemReady") as GameObject;

		OnAwake();

        AddMyTeam();
    }

	protected virtual void OnAwake()
	{
	}

	void Start()
	{
		GuideSystem.Instance.ReqBeginGuide("UIReadyRoom(Clone)");
	}

    private void AddMyTeam()
    {
        _my_role_item = CommonFunction.InstantiateObject(_role_item_prefab, transform).GetComponent<RoleItemReady>();
        _my_role_item.widget.leftAnchor.target = _window.transform;
        _my_role_item.widget.leftAnchor.Set(0f, 8);
        _my_role_item.widget.rightAnchor.target = _window.transform;
        _my_role_item.widget.rightAnchor.Set(0f, 8 + _my_role_item.widget.width);
        _my_role_item.widget.topAnchor.target = _window.transform;
        _my_role_item.widget.topAnchor.Set(1f, -8);
        _my_role_item.widget.bottomAnchor.target = _window.transform;
        _my_role_item.widget.bottomAnchor.Set(1f, -8 - _my_role_item.widget.height);
        _my_role_item.widget.ResetAnchors();
        _my_role_item.status = RoleItemReady.Status.Host;
        _my_role_item.signal_count = 3;
		_my_role_item.onReplace = OnReplaceRole;
        NGUITools.BringForward(_my_role_item.gameObject);
    }

    private void OnReplaceRole(GameObject go)
    {
        foreach (RoleItemReady role_item in _role_item_list)
        {
            if (role_item != null)
            {
                if (go.transform.IsChildOf(role_item.transform))
                {
                    _selected_role_item = role_item;
                }
            }
        }

        SelectRole();
    }

    private void SelectRole()
    {
        if (_select_role == null)
        {
			roleCount = GetRoleCount();
            _select_role = GameSystem.Instance.mClient.mUIManager.CreateUI("SelectSquad", transform).GetComponent<SelectSquad>();
			_select_role.okLabel = CommonFunction.GetConstString("BUTTON_CONFIRM");
            _select_role.single = (roleCount == 1);
			_select_role.onBack = OnSelectRoleOK;
            _select_role.onOK = OnSelectRoleOK;
            for (int i = 0; i < MainPlayer.Instance.RaceInfo.attacker.fighters.Count; ++i)
            {
                Logger.Log("attacker " + i + " role id " + MainPlayer.Instance.RaceInfo.attacker.fighters[i].role_id + " status " + MainPlayer.Instance.RaceInfo.attacker.fighters[i].status);
                if (MainPlayer.Instance.RaceInfo.attacker.fighters[i].status == FightStatus.FS_ASSIST1)
				{
                    _select_role.assistantsID[0] = MainPlayer.Instance.RaceInfo.attacker.fighters[i].role_id;
				}
                else if (MainPlayer.Instance.RaceInfo.attacker.fighters[i].status == FightStatus.FS_ASSIST2)
				{
                    _select_role.assistantsID[1] = MainPlayer.Instance.RaceInfo.attacker.fighters[i].role_id;
				}
            }
        }
        NGUITools.BringForward(_select_role.gameObject);
		NGUITools.SetActive(_select_role.gameObject, true);
    }

	protected virtual uint GetRoleCount()
	{
		return 3;
	}

    private void OnSelectRoleClose(GameObject go)
    {
        _select_role.gameObject.SetActive(false);
    }

    private void OnSelectRoleOK(GameObject go)
    {
		NGUITools.SetActive(_select_role.gameObject, false);
        if (_selected_role_item != null)
        {
            SetRoleItem(_selected_role_item, _select_role.assistantsID);
			if (_selected_role_item == _my_role_item)
			{
				_selected_role_item.name = MainPlayer.Instance.Name;
				_selected_role_item.level = MainPlayer.Instance.Level;
			}
        }

        if (match.m_mainRole == null)
        {
			GameMatch.Config.TeamMember mem = new GameMatch.Config.TeamMember();
            mem.id = MainPlayer.Instance.CaptainID.ToString();
            mem.pos = (int)FightStatus.FS_MAIN;
            mem.team = Team.Side.eHome;
            mem.team_name = MainPlayer.Instance.Name;
            Player player = match.CreatePlayer(mem);
            match.SetMainRole(player);
        }

        MainPlayer.Instance.RaceInfo.attacker.fighters.Clear();
        for (int i = 0; i < 2; ++i)
        {
			if (_select_role.assistantsID[i] != 0)
			{
				FightRole info = new FightRole();
				info.role_id = _select_role.assistantsID[i];
				info.status = FightStatus.FS_ASSIST1 + i;
				MainPlayer.Instance.RaceInfo.attacker.fighters.Add(info);
			}
        }

		if (_select_role.selectCompleted)
		{
			_button_start.GetComponent<FrameAnimation>().Play();
			_button_start_text_anim.Play();
		}
		else
		{
			_button_start.GetComponent<FrameAnimation>().Stop();
			_button_start_text_anim.Stop();
		}
    }

    private void SetRoleItem(RoleItemReady role_item, uint[] role_ids)
    {
		role_item.role_id = MainPlayer.Instance.CaptainID;
        for (uint i = 0; i < 3; ++i)
        {
			if (i == 0)
				role_item.SetSignal(0, true);
			else
				role_item.SetSignal(i, role_ids[i - 1] != 0);
        }
    }

    protected virtual void OnBack(GameObject go)
    {
    }

    protected virtual void OnStartClicked(GameObject go)
    {
    }

    protected virtual void OnChangeRoomClicked(GameObject go)
    {
    }

    private void OnInventoryClicked(GameObject go)
    {
        //GameSystem.Instance.mClient.mUIManager.GoodsCtrl.ShowUIForm();
    }

    private void OnSocialClicked(GameObject go)
    {

    }

    private void OnSkillClicked(GameObject go)
    {
        //GameSystem.Instance.mClient.mUIManager.SkillCtrl.ShowUIForm();
    }
}
