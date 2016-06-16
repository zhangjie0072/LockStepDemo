using UnityEngine;
using fogs.proto.msg;

public class PlayerInfoVisualizer
{
	public 	UILabel  m_uiName;
	private UISprite m_uiPosition;
	
	private Player m_owner;
	
	private static Object m_resPlayerInfo;
	public GameObject	m_goPlayerInfo;

	private UISprite	m_uiState;
	public GameObject	m_goState;
	private float 		m_YState = 0.0f;

	private UIStaminaBar	m_uiStaminaBar;
	public StrengthBar 	m_strengthBar;

	UITweener m_nameFadeAway;

	bool m_bWithBall;
	Vector3 m_oriPositionPos;

	GameUtils.Timer4View m_timerHideName;

	public PlayerInfoVisualizer(Player owner)
	{
		m_owner = owner;
		m_bWithBall = owner.m_bWithBall;

		m_timerHideName = new GameUtils.Timer4View(4f, OnTimerHideName, 1);
		
		if( m_resPlayerInfo == null )
            m_resPlayerInfo = ResourceLoadManager.Instance.LoadPrefab("prefab/indicator/playerInfo");
		
		m_goPlayerInfo = GameObject.Instantiate(m_resPlayerInfo) as GameObject;
		m_uiName = GameUtils.FindChildRecursive(m_goPlayerInfo.transform, "Name").GetComponentInChildren<UILabel>();
		m_nameFadeAway = m_uiName.GetComponent<UITweener>();
		m_nameFadeAway.SetOnFinished(OnNameFadeAwayFinished);
        m_uiName.fontSize = 25;
		m_uiName.text = owner.m_name;

		//if( owner.m_team != null )
		//	m_uiName.color = owner.m_team.m_side == Team.Side.eHome ? Color.red : Color.green;

		m_uiPosition = GameUtils.FindChildRecursive(m_goPlayerInfo.transform, "Position").GetComponentInChildren<UISprite>();
		m_oriPositionPos = m_uiPosition.transform.localPosition;
		m_uiPosition.spriteName = owner.m_position.ToString();
		OnNameFadeAwayFinished();

		GameUtils.SetLayerRecursive(m_goPlayerInfo.transform, LayerMask.NameToLayer("GUI"));

		m_goState = new GameObject(owner.m_id + "_state");
		m_goState.layer = LayerMask.NameToLayer("GUI");

		GameObject state = new GameObject("sprite");
		state.layer = LayerMask.NameToLayer("GUI");
		state.transform.parent = m_goState.transform;
		state.transform.localPosition = Vector3.zero;
		state.transform.localScale = Vector3.one;

		m_uiState = NGUITools.AddSprite(state, m_uiPosition.atlas, "none");
		m_uiState.MakePixelPerfect();
		m_uiState.gameObject.transform.localScale = Vector3.one * 0.3f;

		m_uiStaminaBar = GameUtils.FindChildRecursive(m_goPlayerInfo.transform, "StaminaBar").GetComponent<UIStaminaBar>();
		if( m_uiStaminaBar != null )
		{
			m_uiStaminaBar.m_attachedPlayer = m_owner;
			m_uiStaminaBar.gameObject.SetActive(false);
		}
	}

	public void SetActive(bool bActive)
	{
		if( m_goPlayerInfo != null )
			m_goPlayerInfo.SetActive(bActive);
		if( m_goState != null )
			m_goState.SetActive(bActive);
	}

	public void Update()
	{
		if (m_timerHideName != null)
			m_timerHideName.Update(Time.deltaTime);

		if( m_owner == null || m_goPlayerInfo == null )
			return;
		
		PlayerState.State playerState = m_owner.m_StateMachine.m_curState.m_eState;
		if (m_owner.m_bWithBall && m_uiState != null)
		{
			//m_uiState.gameObject.SetActive(true);
			//m_uiState.spriteName = "Ball";
		}
		else if( playerState == PlayerState.State.eStolen ||
			playerState == PlayerState.State.eCrossed )
		{
			m_uiState.gameObject.SetActive(true);
			m_uiState.spriteName = "Exclam";
		}
		else
		{
			m_uiState.gameObject.SetActive(false);
			m_uiState.spriteName = "None";
		}

		if (!m_bWithBall && m_owner.m_bWithBall)	// Player get ball
		{
			// Show
			m_uiPosition.transform.localPosition = m_oriPositionPos;
			NGUITools.SetActive(m_uiName.gameObject, true);
			m_uiName.alpha = 1f;
			m_nameFadeAway.enabled = false;
			// Begin countdown
			m_timerHideName.Reset();
			m_timerHideName.stop = false;
		}
		else if (m_bWithBall && !m_owner.m_bWithBall)	// Player lose ball
		{
			// Fade away instantly
			m_timerHideName.stop = true;
			OnTimerHideName();
		}

		m_bWithBall = m_owner.m_bWithBall;

		UIManager uiMgr = GameSystem.Instance.mClient.mUIManager;
		if( uiMgr == null || uiMgr.m_uiCamera == null )
			return;

		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		if( curMatch != null && curMatch.m_uiInGamePanel != null )
		{
			{
				m_goPlayerInfo.transform.parent = curMatch.m_uiInGamePanel.transform;
				m_goPlayerInfo.transform.localPosition = Vector3.zero;
				m_goPlayerInfo.transform.localScale = Vector3.one;
			}

			{
				m_goState.transform.parent = curMatch.m_uiInGamePanel.transform;
				m_goState.transform.localPosition = Vector3.zero;
				m_goState.transform.localScale = Vector3.one;
			}
		}

		Vector3 viewPos = Camera.main.WorldToViewportPoint(m_owner.position.ToUnity2());
		Vector3 viewHeadPos = Camera.main.WorldToViewportPoint(m_owner.model.head.position);

		Vector3 worldPos = uiMgr.m_uiCamera.GetComponent<Camera>().ViewportToWorldPoint(viewPos);
		Vector3 posHead = Vector3.zero;

		Vector3 worldHeadPos = uiMgr.m_uiCamera.GetComponent<Camera>().ViewportToWorldPoint(viewHeadPos);
		m_goPlayerInfo.transform.position = worldHeadPos;
		posHead = m_goPlayerInfo.transform.localPosition;

		m_goPlayerInfo.transform.position = worldPos;
		Vector3 pos = m_goPlayerInfo.transform.localPosition;
		pos.x = Mathf.FloorToInt(pos.x);
		pos.y = Mathf.FloorToInt(pos.y);
		pos.z = 0.0f;
		m_goPlayerInfo.transform.localPosition = pos;

		m_YState = (posHead.y - pos.y) + 50.0f;

		m_goState.transform.position = worldPos;
		m_uiState.transform.localPosition = Vector3.up * m_YState;

		pos = m_goState.transform.localPosition;
		pos.x = Mathf.FloorToInt(pos.x);
		pos.y = Mathf.FloorToInt(pos.y);
		pos.z = 2.0f;
		m_goState.transform.localPosition = pos;
	}

    public void CreateStrengthBar()
    {
        GameObject obj = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/StrengthBar", m_goPlayerInfo.transform);
		obj.transform.localPosition = Vector3.zero;
        m_strengthBar = obj.GetComponent<StrengthBar>();
        m_strengthBar.player = m_owner;
    }

	public void DestroyStrengthBar()
	{
		NGUITools.Destroy(m_strengthBar.gameObject);
		m_strengthBar = null;
	}

	public void ShowStaminaBar(bool bShow)
	{
		if( m_uiStaminaBar == null )
			return;
		NGUITools.SetActive(m_uiStaminaBar.gameObject, bShow);
	}

	void OnTimerHideName()
	{
		m_nameFadeAway.ResetToBeginning();
		m_nameFadeAway.PlayForward();
	}

	void OnNameFadeAwayFinished()
	{
		NGUITools.SetActive(m_uiName.gameObject, false);
		Vector3 pos = m_uiPosition.transform.localPosition;
		pos.x = 0;
		m_uiPosition.transform.localPosition = pos;
	}
}