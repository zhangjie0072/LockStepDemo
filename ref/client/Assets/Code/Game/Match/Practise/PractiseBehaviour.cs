using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System.IO;

public class PractiseBehaviour : MonoBehaviour
{
	public enum ObjectiveState
	{
		Undone,
		Completed,
		Failed,
	}

	private GameMatch_Practise _match;
	public GameMatch_Practise match
	{
		set
		{
			_match = value;
			UIEventListener.Get(match.ui_tutorial).onClick = delegate(GameObject go)
			{
				_temp_free_mode = _free_mode;
				_free_mode = false;
				_temp_tutorial = true;
				OnStart();
			};
			OnMatchSetted();
		}
		get { return _match; }
	}

	protected bool _free_mode = false;
	protected bool _temp_tutorial = false;
	private bool _temp_free_mode;
    protected bool _is_practise_completed = false;

	private PractiseData _practise;
	public PractiseData practise
	{
		get { return _practise; }
		set
		{
			_practise = value;
			_states = new ObjectiveState[practise.num_total];
            _is_practise_completed = MainPlayer.Instance.IsPractiseCompleted(practise.ID);
            _free_mode = _is_practise_completed; // MainPlayer.Instance.IsPractiseCompleted(practise.ID);
            uint id = practise.ID;

            if (GameSystem.Instance.PractiseConfig.GetConfig(id).is_activity == 1)
            {
                _free_mode = (LoginIDManager.GetFirstPractice(id) == 1);
            }
        }
	}

	private ObjectiveState[] _states;

	protected const int EXERCISE_OBJ_INDEX = -1;
	protected int _curr_obj_index = EXERCISE_OBJ_INDEX;
	protected int _temp_curr_obj_index;

	public bool in_tutorial { 
        get 
        { 
            return _curr_obj_index == EXERCISE_OBJ_INDEX && !_free_mode; 
        }
    }

	private uint _num_complete;

	private bool _first = true;

	private PractiseResult result;

	private Dictionary<GameObject, Transform> _ui_effects = new Dictionary<GameObject, Transform>();

	void Awake()
	{
		OnAwake();
	}

	protected virtual void OnAwake()
	{
		//GameSystem.Instance.mNetworkManager.m_handler.RegisterHandler(MsgID.ExitGameReqID, OnEndPractise);
	}

	void Start()
	{
		if (_first)
		{
			OnFirstStart();
			_first = false;
		}
		OnStart();
	}

	protected virtual void OnFirstStart()
	{

	}

	protected virtual void OnStart()
	{
		StopAllCoroutines();

        if (_free_mode)
        {
            _curr_obj_index = 0;
           // match.HideSignal();
            match.HideGuideTip();
            _num_complete = 0;

            //if (practise.num_total > 0)
            if( !_is_practise_completed )
            {
                match.ShowSignal();
                match.SetSignal(_states);
            }
            else
            {
                match.HideSignal();
            }
        }
        else
        {
            if (_temp_tutorial)
            {
                _temp_curr_obj_index = _curr_obj_index;
                _curr_obj_index = EXERCISE_OBJ_INDEX;
            }
            else
            {
                _curr_obj_index = EXERCISE_OBJ_INDEX;
                _num_complete = 0;
                _states = new ObjectiveState[practise.num_total];
                if (practise.num_total > 0)
                {
                    match.ShowSignal();
                    match.SetSignal(_states);
                }
            }
            // if free-mode always display tutorial.
            NGUITools.SetActive(match.ui_tutorial, _free_mode);
            match.HideSignal();
        }
	}

    void Update()
    {
        ViewUpdate(Time.deltaTime);
    }

    public virtual void ViewUpdate(float deltaTime)
    {
		UpdateUIEffects();
    }

	public virtual void GameUpdate(IM.Number deltaTime)
	{
	}

	public virtual Team.Side GetNPCSide()
	{
		return Team.Side.eAway;
	}

	public virtual AIState.Type GetInitialAIState()
	{
		return AIState.Type.eInit;
	}

	public virtual bool ResetPlayerPos()
	{
		return false;
	}

    public virtual IM.PrecNumber AdjustShootRate(Player shooter, IM.PrecNumber rate)
	{
		return rate;
	}

    public virtual IM.Number AdjustBlockRate(Player shooter, Player blocker, IM.Number rate)
	{
		return rate;
	}

    public virtual IM.Number AdjustCrossRate(Player crosser, Player defender, IM.Number rate)
	{
		return rate;
	}

    public virtual ShootSolution GetShootSolution(UBasket basket, Area area, Player shooter, IM.PrecNumber rate)
	{
		return null;
	}

	public virtual bool IsCommandValid(Command command)
	{
		return true;
	}

	protected virtual void OnMatchSetted()
	{
	}

	protected GameObject CreateUIEffect(string prefab_name, Transform transform)
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab(prefab_name) as GameObject;
		GameObject obj = GameObject.Instantiate(prefab) as GameObject;
		obj.transform.parent = GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform;
		obj.transform.localScale = Vector3.one;
		obj.transform.localPosition = Vector3.zero;
		_ui_effects[obj] = transform;
		UpdateUIEffect(obj, transform);
		return obj;
	}

	protected void DestroyUIEffect(GameObject obj)
	{
		_ui_effects.Remove(obj);
		NGUITools.Destroy(obj);
	}

	protected void SetUIEffect(GameObject obj, Transform transform)
	{
		_ui_effects[obj] = transform;
		UpdateUIEffect(obj, transform);
	}

	protected void UpdateUIEffects()
	{
		foreach (KeyValuePair<GameObject, Transform> pair in _ui_effects)
		{
			if (pair.Key.activeInHierarchy)
			{
				UpdateUIEffect(pair.Key, pair.Value);
			}
		}
	}

	private void UpdateUIEffect(GameObject go, Transform transform)
	{
		Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
		Vector3 worldPos = GameSystem.Instance.mClient.mUIManager.m_uiCamera.GetComponent<Camera>().ViewportToWorldPoint(viewPos);
		go.transform.position = worldPos;
		Vector3 pos = go.transform.localPosition;
		pos.x = Mathf.FloorToInt(pos.x);
		pos.y = Mathf.FloorToInt(pos.y);
		pos.z = 2.0f;
		go.transform.localPosition = pos;
	}

	protected void ShowTipOnMainRole(string tip)
	{
		match.ShowTips( match.mainRole.model.head.position + Vector3.up, tip, new Color32(255, 75, 12, 255));
	}

	protected void Pause(bool pause = true)
	{
		GameSystem.Instance.mClient.pause = pause;
	}

	protected void FinishObjective(bool succeed)
	{
        //if (_free_mode)
        //    match.ShowIconTip(succeed);
        //else
        //{
			if (_curr_obj_index >= practise.num_total)
				return;
			if (_curr_obj_index != EXERCISE_OBJ_INDEX && !_is_practise_completed)
			{
				if (succeed)
					++_num_complete;
                if (!_temp_tutorial)
                {
                    match.ShowIconTip(succeed);
                }
				_states[_curr_obj_index] = succeed ? ObjectiveState.Completed : ObjectiveState.Failed;
				match.SetSignal(_states);
				++_curr_obj_index;
			}

            if( _curr_obj_index == EXERCISE_OBJ_INDEX )
			{
                // in tutorial.
				NGUITools.SetActive(match.ui_tutorial, true);
				match.ShowSignal();
				++_curr_obj_index;

				if (_temp_tutorial)
				{
					//restore previous objective progress
					_curr_obj_index = _temp_curr_obj_index;
					_free_mode = _temp_free_mode;
					//Don't show tutorial again
					if (_curr_obj_index == EXERCISE_OBJ_INDEX)
						++_curr_obj_index;
					_temp_tutorial = false;
				}
				if (_is_practise_completed)
                {
					match.HideSignal();
                }
			}

			if (_curr_obj_index >= practise.num_total)
			{
                //if (!MainPlayer.Instance.IsPractiseCompleted(practise.ID))
					StartCoroutine(Step_Complete(_num_complete >= practise.num_complete));
                //else
                //{
                //    _free_mode = true;
                //    OnStart();
                //}
			}
        //}
	}

	protected IEnumerator Step_Complete(bool passed)
	{
		yield return new WaitForSeconds(2);
        //GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PractiseResult") as GameObject;
        //GameObject obj = CommonFunction.InstantiateObject(prefab, GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform);
        //NGUITools.BringForward(obj);
        //result = obj.GetComponent<PractiseResult>();
        //result.practise = practise;
        //result.completed = passed;
        //result.states = _states;
        //result.num_complete = _num_complete;
        //result.onOK = go =>
        //{
        //    if (GlobalConst.IS_NETWORKING)
        //    {
        //        EndPractice practice = new EndPractice();
        //        practice.session_id = match.m_config.session_id;

        //        ExitGameReq req = new ExitGameReq();
        //        req.acc_id = MainPlayer.Instance.AccountID;
        //        req.type = MatchType.MT_PRACTICE;
        //        req.exit_type = passed ? ExitMatchType.EMT_END : ExitMatchType.EMT_OPTION;
        //        req.practice = practice;
        //        GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
        //    }

        //    ReturnToList();
        //};
        //result.onClose = delegate(GameObject go)
        //{
        //    _free_mode = MainPlayer.Instance.IsPractiseCompleted(practise.ID);
        //    if (result != null)
        //        NGUITools.Destroy(result.gameObject);
        //    OnStart();
        //};
        //result.onRestart = delegate(GameObject go)
        //{
        //    _free_mode = MainPlayer.Instance.IsPractiseCompleted(practise.ID);
        //    if (result != null)
        //        NGUITools.Destroy(result.gameObject);
        //    OnStart();
        //};

        //if (passed)
        //{
        //    MainPlayer.Instance.SetPractiseCompleted(practise.ID, true);
        //}

        EndPractice practice = new EndPractice();
        practice.session_id = match.m_config.session_id;
        practice.goal_times = _num_complete;
        ExitGameReq req = new ExitGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_PRACTICE;
		req.exit_type = ExitMatchType.EMT_END;
        req.practice = practice;
		PlatNetwork.Instance.SendExitGameReq(req);
	}

    private void OnEndPractise(EndPracticeResp practice)
	{
		//EndPracticeResp resp = Serializer.Deserialize<EndPracticeResp>(new MemoryStream(pack.buffer));
        if ((ErrorID)practice.result != ErrorID.SUCCESS)
		{
            Debug.Log("End practise error: " + (ErrorID)practice.result);
            CommonFunction.ShowErrorMsg((ErrorID)practice.result);
		}
	}

	protected void ReturnToList()
	{
        LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.ePractise);
	}
}
