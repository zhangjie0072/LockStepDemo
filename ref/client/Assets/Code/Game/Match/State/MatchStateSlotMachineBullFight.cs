using fogs.proto.config;
using fogs.proto.msg;
using System;
using UnityEngine;

public class MatchStateSlotMachineBullFight : MatchState
{
	GameMatch_BullFight match;
	GameUtils.Timer timerBegin;
	GameUtils.Timer timerEnd;
	GameObject[] items = new GameObject[4];
	int selectedItemIndex;

	public MatchStateSlotMachineBullFight(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eSlotMachineBullFight;
		match = m_match as GameMatch_BullFight;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		base.OnEnter(lastState);

		InputReader.Instance.enabled = false;
		NGUITools.SetActive(match.slotMachine.gameObject, true);
		NGUITools.BringForward(match.slotMachine.gameObject);
		match.slotMachine.onCreateItem += OnCreateSlotMachineItem;
		match.slotMachine.onEnd += OnSlotMachineEnd;

		timerBegin = new GameUtils.Timer(IM.Number.one, BeginSlotMachine);
		timerBegin.stop = false;
	}
	
	override public void GameUpdate (IM.Number fDeltaTime)
	{
		base.GameUpdate(fDeltaTime);

		if (timerBegin != null)
			timerBegin.Update(fDeltaTime);
		if (timerEnd != null)
			timerEnd.Update(fDeltaTime);
	}

	public override void OnExit()
	{
		InputReader.Instance.enabled = true;
	}

	private void OnCreateSlotMachineItem(GameObject item, int index)
	{
		items[index] = item;
		if (index % 2 == 0)
		{
			item.transform.FindChild("Name").GetComponent<UILabel>().text = match.mainRole.m_name;
			RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(match.mainRole.m_roleInfo.id);
			item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = data.icon;
			string position = ((PositionType)data.position).ToString();
			item.transform.FindChild("Position").GetComponent<UISprite>().spriteName = position;
		}
		else
		{
			item.transform.FindChild("Name").GetComponent<UILabel>().text = match.npc.m_name;
			NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(match.npc.m_roleInfo.id);
			item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = config.icon;
			string position = ((PositionType)config.position).ToString();
			item.transform.FindChild("Position").GetComponent<UISprite>().spriteName = position;
		}
	}

	private void OnSlotMachineEnd(GameObject selectedItem)
	{
		for (int i = 0; i < 4; ++i)
		{
			if (selectedItem == items[i])
			{
				selectedItemIndex = i % 2;
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
		m_stateMachine.SetState(MatchState.State.eBegin);
	}

	public override bool IsCommandValid(Command command)
	{
		return false;
	}
}
