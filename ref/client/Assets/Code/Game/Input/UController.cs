using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UController : MonoBehaviour
{
	public class Button
	{
		public int index;
		public UIButton			btn;
		public UISprite			icon;
		public Command			cmd;
		public List<Command>	validCmds;
	}
	
	Button	m_btn1 = new Button();
	Button	m_btn2 = new Button();
	Button	m_btn3 = new Button();
	Button	m_btn4 = new Button();
	Button	m_btn5 = new Button();
	
	public List<Button>	m_btns = new List<Button>();

	public UIButton[] m_btnSkill = new UIButton[2];

	Dictionary<Command, Button> cmdBtnMap = new Dictionary<Command, Button>();
	
	public bool visible { set { NGUITools.SetActive(gameObject, value); }  }

	/*
    private UIToggle BtnAutoDefense;
    public System.Action<bool> onAutoDefenseChanged;
    public bool EnableAutoDefenseButton
    {
        set
        {
            NGUITools.SetActive(BtnAutoDefense.gameObject, value);
            if (!value)
            {
                BtnAutoDefense.value = !value;
                if (onAutoDefenseChanged != null)
                    onAutoDefenseChanged(value);
            }
        }
    }
	*/

	void Awake()
	{
        //每次进入比赛时重置，检查时间时间（因为在大厅操作UI，时间会越来越慢）
        CheatingDeath.Instance.mAntiSpeedUp.ResetWatch();
        //Debug.LogWarning("CheatingDeath.Instance.mAntiSpeedUp.ResetWatch();");


        //比赛内允许多点触控
        if (UIManager.Instance.m_uiCamera != null)
            UIManager.Instance.m_uiCamera.allowMultiTouch = true;
		Transform target = GameUtils.FindChildRecursive( transform, "Btn1" );
		if( target != null )
		{
			Transform icon = GameUtils.FindChildRecursive( target, "Icon" );
			m_btn1.index = 0;
			m_btn1.icon = icon.GetComponent<UISprite>();
			m_btn1.btn = target.GetComponentInChildren<UIButton>();
			m_btn1.validCmds = new List<Command>();

			m_btn1.validCmds.Add(Command.Shoot);
			cmdBtnMap.Add(Command.Shoot, m_btn1);
			m_btn1.validCmds.Add(Command.Block);
			cmdBtnMap.Add(Command.Block, m_btn1);
			m_btn1.validCmds.Add(Command.Rebound);
			cmdBtnMap.Add(Command.Rebound, m_btn1);
			m_btn1.validCmds.Add(Command.BodyThrowCatch);
			cmdBtnMap.Add(Command.BodyThrowCatch, m_btn1);

		}
		
		target = GameUtils.FindChildRecursive( transform, "Btn2" );
		if( target != null )
		{
			Transform icon = GameUtils.FindChildRecursive( target, "Icon" );
			m_btn2.index = 1;
			m_btn2.icon = icon.GetComponent<UISprite>();
			m_btn2.btn = target.GetComponentInChildren<UIButton>();
			m_btn2.validCmds = new List<Command>();

			m_btn2.validCmds.Add(Command.Switch);
			cmdBtnMap.Add(Command.Switch, m_btn2);
			m_btn2.validCmds.Add(Command.TraceBall);
			cmdBtnMap.Add(Command.TraceBall, m_btn2);
			m_btn2.validCmds.Add(Command.Rush);
			cmdBtnMap.Add(Command.Rush, m_btn2);
		}
		
		target = GameUtils.FindChildRecursive( transform, "Btn3" );
		if( target != null )
		{
			Transform icon = GameUtils.FindChildRecursive( target, "Icon" );
			m_btn3.index = 2;
			m_btn3.icon = icon.GetComponent<UISprite>();
			m_btn3.btn = target.GetComponentInChildren<UIButton>();
			m_btn3.validCmds = new List<Command>();

			m_btn3.validCmds.Add(Command.Pass);
			cmdBtnMap.Add(Command.Pass, m_btn3);

			m_btn3.validCmds.Add(Command.Defense);
			cmdBtnMap.Add(Command.Defense, m_btn3);
		}

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;

		target = GameUtils.FindChildRecursive( transform, "Btn4" );
		if( target != null )
		{
			Transform icon = GameUtils.FindChildRecursive( target, "Icon" );
			m_btn4.index = 3;
			m_btn4.icon = icon.GetComponent<UISprite>();
			m_btn4.btn = target.GetComponentInChildren<UIButton>();
			m_btn4.validCmds = new List<Command>();

			m_btn4.validCmds.Add(Command.BackToBack);
			cmdBtnMap.Add(Command.BackToBack, m_btn4);
			m_btn4.validCmds.Add(Command.CrossOver);
			cmdBtnMap.Add(Command.CrossOver, m_btn4);
			m_btn4.validCmds.Add(Command.Steal);
			cmdBtnMap.Add(Command.Steal, m_btn4);
			m_btn4.validCmds.Add(Command.PickAndRoll);
			cmdBtnMap.Add(Command.PickAndRoll, m_btn4);
		}

		/*
		target = GameUtils.FindChildRecursive( transform, "Btn5" );
		if( target != null )
		{
			Transform icon = GameUtils.FindChildRecursive( target, "Icon" );
			m_btn5.index = 4;
			m_btn5.icon = icon.GetComponent<UISprite>();
			m_btn5.btn = target.GetComponentInChildren<UIButton>();
			m_btn5.validCmds = new List<Command>();

			m_btn5.validCmds.Add(Command.BackToBack);
			cmdBtnMap.Add(Command.BackToBack, m_btn5);
		}
		*/

		m_btns.Add(m_btn1);
		m_btns.Add(m_btn2);
		m_btns.Add(m_btn3);
		m_btns.Add(m_btn4);
		/*
        BtnAutoDefense = GameUtils.FindChildRecursive(transform, "Btn7").GetComponent<UIToggle>();
        BtnAutoDefense.onChange.Add(new EventDelegate(() =>
        {
            PlayerPrefs.SetInt("DisableAutoDefense", BtnAutoDefense.value ? 1 : 0);
            if (onAutoDefenseChanged != null)
                onAutoDefenseChanged(!BtnAutoDefense.value);
        }));

		int disableAutoDefense = 0;
		if( MainPlayer.Instance.pvp_regular.race_times != 0 )
         	disableAutoDefense = PlayerPrefs.GetInt("DisableAutoDefense");
        BtnAutoDefense.startsActive = (disableAutoDefense != 0);
        */
	}

	public void UpdateBtnState()
	{
		if( GameSystem.Instance.mClient == null )
			return;
		
		//UpdateBtnCmd();
		
#if UNITY_IPHONE || UNITY_ANDROID
        if (GameSystem.Instance.mClient.mInputManager.isNGDS)
            return;
		if( m_btn1.btn != null )
        {
            if (!m_btn1.btn.isPressed && GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Click)
            {
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Released = true;
            }
			GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Click = m_btn1.btn.isPressed;
        }
		if( m_btn2.btn != null )
		{
            if (!m_btn2.btn.isPressed && GameSystem.Instance.mClient.mInputManager.m_CmdBtn2Click)
            {
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn2Released = true;
            }
			GameSystem.Instance.mClient.mInputManager.m_CmdBtn2Click = m_btn2.btn.isPressed;
		}
		if( m_btn3.btn != null )
			GameSystem.Instance.mClient.mInputManager.m_CmdBtn3Click = m_btn3.btn.isPressed;
		if( m_btn4.btn != null )
			GameSystem.Instance.mClient.mInputManager.m_CmdBtn4Click = m_btn4.btn.isPressed;
		if( m_btn5.btn != null )
			GameSystem.Instance.mClient.mInputManager.m_CmdBtn5Click = m_btn5.btn.isPressed;
#endif
	}

    void OnDestroy()
    {
        //比赛外不允许多点触控
        if (  UIManager.Instance != null && UIManager.Instance.m_uiCamera != null)
            UIManager.Instance.m_uiCamera.allowMultiTouch = false;
    }

	public Button GetButton(Command cmd)
	{
		Button btn;
		cmdBtnMap.TryGetValue(cmd, out btn);
		return btn;
	}

	public Button GetButton(GameObject go)
	{
		foreach (Button btn in m_btns)
		{
			if (btn.btn.gameObject == go)
				return btn;
		}
		return new Button();
	}
	
	public void UpdateBtnCmd()
	{
		InputReader dispatcher = InputReader.Instance;
		if( dispatcher == null )
			return;
		int validCmdCnt = dispatcher.validCmdList.Count;
		for( int idx = 0; idx != m_btns.Count; ++idx )
		{
			if (m_btns[idx].btn == null)
				continue;
			m_btns[idx].icon.spriteName = "";
			m_btns[idx].btn.enabled = false;
			m_btns[idx].cmd = Command.None;
			for( int vIdx = 0; vIdx != validCmdCnt; vIdx++ )
			{
				Command validCmd = dispatcher.validCmdList[vIdx];
				if( !m_btns[idx].validCmds.Contains(validCmd) )
					continue;
                if (validCmd == Command.Switch
                    && ((GameSystem.Instance.mClient.mCurMatch.leagueType == GameMatch.LeagueType.eRegular1V1 && GameSystem.Instance.mClient.mCurMatch.GetMatchType() != GameMatch.Type.ePVP_1PLUS && GameSystem.Instance.mClient.mCurMatch.GetMatchType() != GameMatch.Type.ePVP_3On3)
                    || (GameSystem.Instance.mClient.mCurMatch.leagueType == GameMatch.LeagueType.eQualifyingNewerAI)))
                {
                    continue;
                }
                m_btns[idx].btn.enabled = true;
                m_btns[idx].cmd = validCmd;
                m_btns[idx].icon.spriteName = validCmd.ToString();
                
				//m_btns[idx].icon.MakePixelPerfect();
				break;
			}
		}
	}
}
