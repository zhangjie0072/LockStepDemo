using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class UIMatchReboundStorm : MonoBehaviour
{
	public string spritePrefix = "";

	[HideInInspector]
	public int leftScore;
	[HideInInspector]
	public int rightScore;
	[HideInInspector]
	public TimerBoard timerBoard { get; private set; }

	UILabel leftNameLabel;
	UILabel rightNameLabel;
	UISprite leftUnits;
	UISprite leftTens;
	UISprite leftHundreds;
	UISprite leftThousands;
	UISprite rightUnits;
	UISprite rightTens;
	UISprite rightHundreds;
	UISprite rightThousands;
	Transform timerNode;

	void Awake()
	{
		leftNameLabel = transform.FindChild("LeftName").GetComponent<UILabel>();
		rightNameLabel = transform.FindChild("RightName").GetComponent<UILabel>();
		leftUnits = transform.FindChild("LeftScore/Units").GetComponent<UISprite>();
		leftTens = transform.FindChild("LeftScore/Tens").GetComponent<UISprite>();
		leftHundreds = transform.FindChild("LeftScore/Hundreds").GetComponent<UISprite>();
		leftThousands = transform.FindChild("LeftScore/Thousands").GetComponent<UISprite>();
		rightUnits = transform.FindChild("RightScore/Units").GetComponent<UISprite>();
		rightTens = transform.FindChild("RightScore/Tens").GetComponent<UISprite>();
		rightHundreds = transform.FindChild("RightScore/Hundreds").GetComponent<UISprite>();
		rightThousands = transform.FindChild("RightScore/Thousands").GetComponent<UISprite>();
		timerNode = transform.FindChild("TimerNode");

		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TimerBoard") as GameObject;
		timerBoard = CommonFunction.InstantiateObject(prefab, timerNode).GetComponent<TimerBoard>();
		timerBoard.backgroundVisible = false;

        prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ButtonBack") as GameObject;
		GameObject goExit = CommonFunction.InstantiateObject(prefab, transform.FindChild("ButtonBack"));
		UIEventListener.Get(goExit).onClick = OnExit;
	}

	void Update()
	{
		uint[] digits = CommonFunction.GetDigits((uint)leftScore, 4);
		leftUnits.spriteName = spritePrefix + digits[0];
		leftTens.spriteName = spritePrefix + digits[1];
		leftHundreds.spriteName = spritePrefix + digits[2];
		leftThousands.spriteName = spritePrefix + digits[3];
		digits = CommonFunction.GetDigits((uint)rightScore, 4);
		rightUnits.spriteName = spritePrefix + digits[0];
		rightTens.spriteName = spritePrefix + digits[1];
		rightHundreds.spriteName = spritePrefix + digits[2];
		rightThousands.spriteName = spritePrefix + digits[3];
	}

    private void OnExit(GameObject go)
    {
        GameSystem.Instance.mClient.pause = true;
        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("MATCH_TIPS_EXIT_MATCH"), 
			UIManager.Instance.m_uiRootBasePanel.transform, OnConfirmExit, OnCancelExit);
    }

    private void OnConfirmExit(GameObject go)
    {
		if (!GameSystem.Instance.mNetworkManager.connPlat)
		{
			PlatNetwork.Instance.onReconnected += ExitMatch;
			GameSystem.Instance.mNetworkManager.autoReconnInMatch = true;
			GameSystem.Instance.mNetworkManager.Reconnect();
			return;
		}
		ExitMatch();
	}

	private void ExitMatch()
    {
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
        GameMatch.LeagueType type = curMatch.leagueType;
        if (type == GameMatch.LeagueType.eCareer)
        {
            EndSectionMatch career = new EndSectionMatch();
            career.session_id = curMatch.m_config.session_id;

            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_CAREER;
            req.exit_type = ExitMatchType.EMT_OPTION;
            req.career = career;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
            GameSystem.Instance.mClient.mUIManager.curLeagueType = curMatch.leagueType;
        }
		else if (type == GameMatch.LeagueType.eTour)
		{
			TourExitReq req = new TourExitReq();
			req.session_id = curMatch.m_config.session_id;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.TourExitReqID);
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", curMatch.leagueType);
		}
        else
        {
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", curMatch.leagueType);
        }

		curMatch.m_stateMachine.m_curState.OnExit();
        GameSystem.Instance.mClient.pause = false;
    }

    private void OnCancelExit(GameObject go)
    {
        GameSystem.Instance.mClient.pause = false;
    }
}
