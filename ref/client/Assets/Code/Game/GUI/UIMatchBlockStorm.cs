using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class UIMatchBlockStorm : MonoBehaviour
{
	public string spritePrefix = "gameInterface_figure_black";

	[HideInInspector]
	private int _score;
	public int score
	{
		set
		{
			_score = value;
			digitInvalidate = true;
		}
		get { return _score; }
	}
	[HideInInspector]
	private int _maxScore;
	public int maxScore
	{
		set
		{
			_maxScore = value;
			digitInvalidate = true;
		}
		get { return _maxScore; }
	}
	private uint _starNum = 0;
	[HideInInspector]
	public uint starNum
	{
		set
		{
			if (_starNum == value) return;

			UITweener[] tweeners = star[_starNum].GetComponents<UITweener>();
			foreach (UITweener tweener in tweeners)
				tweener.enabled = true;

			uint prevStarNum = _starNum;
			tweeners[0].AddOnFinished(() =>
			{
				NGUITools.SetActive(star[prevStarNum].gameObject, false);
				if (value != 3)
				{
					NGUITools.SetActive(star[value].gameObject, true);
				}
				else
				{
					NGUITools.SetActive(star[value - 1].gameObject, true);
				}
			});
			_starNum = value;
		}
	}

	public TimerBoard timerBoard { get; private set; }

	UISprite digit;
	UISprite[] digits;
	UIWidget[] star = new UIWidget[3];
	Transform timerNode;

	bool digitInvalidate = true;

	void Awake()
	{
		digit = transform.FindChild("Digit").GetComponent<UISprite>();
		timerNode = transform.FindChild("TimerNode");
		star[0] = transform.FindChild("Star1").GetComponent<UIWidget>();
		star[1] = transform.FindChild("Star2").GetComponent<UIWidget>();
		star[2] = transform.FindChild("Star3").GetComponent<UIWidget>();

		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TimerBoard") as GameObject;
		timerBoard = CommonFunction.InstantiateObject(prefab, timerNode).GetComponent<TimerBoard>();
		timerBoard.backgroundVisible = false;

        prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ButtonBack") as GameObject;
		GameObject goExit = CommonFunction.InstantiateObject(prefab, transform.FindChild("ButtonBack"));
		UIEventListener.Get(goExit).onClick = OnExit;
	}

	void Update()
	{
		if (digitInvalidate)
		{
			RefreshDigits();
		}
	}

	void RefreshDigits()
	{
		if (digits != null)
		{
			digits[0] = null;
			foreach (UISprite d in digits)
			{
				if (d != null)
					NGUITools.Destroy(d.gameObject);
			}
		}
		uint[] scoreDigits = CommonFunction.GetDigits((uint)score);
		uint[] maxScoreDigits = CommonFunction.GetDigits((uint)maxScore);
		digits = new UISprite[scoreDigits.Length + maxScoreDigits.Length + 1];
		digits[0] = digit;
		UISprite prev = digit;
		for (int i = 1; i < digits.Length; ++i )
		{
			//UISprite sprite = NGUITools.AddChild<UISprite>(gameObject);
			UISprite sprite = CommonFunction.InstantiateObject(digit.gameObject, transform).GetComponent<UISprite>();
			digits[i] = sprite;
			sprite.topAnchor = digit.topAnchor;
			sprite.bottomAnchor = digit.bottomAnchor;
			sprite.leftAnchor.target = prev.transform;
			sprite.leftAnchor.Set(1f, 0f);
			sprite.rightAnchor.target = prev.transform;
			sprite.rightAnchor.Set(1f, digit.width);
			sprite.ResetAnchors();
			prev = sprite;
		}
		int index = digits.Length - 1;
		for (int i = 0; i < maxScoreDigits.Length; ++i, --index )
		{
			digits[index].spriteName = spritePrefix + maxScoreDigits[i];
		}
		digits[index--].spriteName = spritePrefix + "Sprit";
		for (int i = 0; i < scoreDigits.Length; ++i, --index )
		{
			digits[index].spriteName = spritePrefix + scoreDigits[i];
		}

		digitInvalidate = false;
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
