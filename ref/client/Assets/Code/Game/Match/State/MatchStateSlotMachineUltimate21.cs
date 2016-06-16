using fogs.proto.config;
using fogs.proto.msg;
using System;
using UnityEngine;

public class MatchStateSlotMachineUltimate21 : MatchState
{
	GameMatch_Ultimate21 match;
	GameUtils.Timer timerBegin;
	GameUtils.Timer timerEnd;
	GameObject[] items = new GameObject[3];
	int selectedItemIndex;

	public MatchStateSlotMachineUltimate21(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eSlotMachineUltimate21;
		match = m_match as GameMatch_Ultimate21;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		base.OnEnter(lastState);

		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			player.m_enableAction = false;
			player.m_enableMovement = false;
			player.m_enablePickupDetector = false;
			player.Hide();
		}

		match.m_mainRole.m_inputDispatcher.m_enable = false;
		NGUITools.SetActive(match.slotMachine.gameObject, true);
		NGUITools.BringForward(match.slotMachine.gameObject);
		match.slotMachine.onCreateItem += OnCreateSlotMachineItem;
		match.slotMachine.onEnd += OnSlotMachineEnd;

		timerBegin = new GameUtils.Timer(IM.Number.one, BeginSlotMachine);
		timerBegin.stop = false;
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if (timerBegin != null)
			timerBegin.Update(fDeltaTime);
		if (timerEnd != null)
			timerEnd.Update(fDeltaTime);
	}

	public override void OnExit()
	{
		match.m_mainRole.m_inputDispatcher.m_enable = true;
	}

	private void OnCreateSlotMachineItem(GameObject item, int index)
	{
		items[index] = item;
		Player player = match.players[index];
		item.transform.FindChild("Name").GetComponent<UILabel>().text = player.m_name;
		int icon;
		if (index == 0)
		{
			RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(player.m_roleInfo.id);
			item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = data.icon;
			string position = ((PositionType)data.position).ToString();
			item.transform.FindChild("Position").GetComponent<UISprite>().spriteName = position;
			icon = data.shape;
		}
		else
		{
			NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(player.m_roleInfo.id);
			item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = config.icon;
			string position = ((PositionType)config.position).ToString();
			item.transform.FindChild("Position").GetComponent<UISprite>().spriteName = position;
			icon = (int)config.shape;
		}
		UIAtlas atlas = null;
		if (1000 <= icon && icon <= 1499)
            atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconPortrait");
		else if (1500 <= icon && icon <= 1799)
            atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconPortrait_1");
		else if (1800 <= icon && icon <= 1999)
            atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconPortrait_2");
		item.transform.FindChild("Icon").GetComponent<UISprite>().atlas = atlas;
	}

	private void OnSlotMachineEnd(GameObject selectedItem)
	{
		for (int i = 0; i < 3; ++i)
		{
			if (selectedItem == items[i])
			{
				selectedItemIndex = i;
				break;
			}
		}

		timerEnd = new GameUtils.Timer(new IM.Number(1,500), BeginMatch);
		timerEnd.stop = false;
	}

	private void BeginSlotMachine()
	{
		match.slotMachine.Begin();
		timerBegin.stop = true;
	}

	private void BeginMatch()
	{
		timerEnd.stop = true;
		match.SetStartingAttacker(selectedItemIndex);
		NGUITools.SetActive(match.slotMachine.gameObject, false);
		m_stateMachine.SetState(MatchState.State.ePlayerCloseUp);
	}

	public override bool IsCommandValid(Command command)
	{
		return false;
	}
}
