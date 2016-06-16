using System.Collections;
using UnityEngine;

public class PractiseBehaviourMove : PractiseBehaviour
{
	enum Step
	{
		None,
		GrabBall,
		Move1,
		Complete,
	}
    //private Transform _ball_pos;
    //private Transform _move_pos;
    private GameObject movePosGo;
	private BoxCollider _collider1;
	private BoxCollider _collider2;

	private Step _step = Step.None;

	private GameObject _obj_effect;
	private GameObject _tip_on_scene;
	private UILabel _tip_text;

	public delegate void Delegate();
	public Delegate onOver;

	public override bool IsCommandValid(Command command)
	{
		return false;
	}

	protected override void OnAwake()
	{
		base.OnAwake();
        //GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/PractiseMovePos") as GameObject;
        //GameObject pos_obj = CommonFunction.InstantiateObject(prefab, transform);
        //_ball_pos = pos_obj.transform.FindChild("Ball");
        //_move_pos = pos_obj.transform.FindChild("1");
	}

	protected override void OnFirstStart()
	{
		base.OnFirstStart();
		_obj_effect = Object.Instantiate(ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/Position")) as GameObject;
		match.HideSignal();
	}

	protected override void OnStart()
	{
		//there's no free mode in this practise
		_free_mode = false;

		base.OnStart();

		match.ShowTouchGuide(OnTouchGuidePress);
		Step_GrabBall();
	}

	private void OnTouchGuidePress(GameObject go, bool pressed)
	{
		if (_step == Step.GrabBall)
			StartCoroutine(ShowTipMoveToGrabBall());
		UIEventListener.Get(go).onPress -= OnTouchGuidePress;
	}

	private IEnumerator ShowTipMoveToGrabBall()
	{
		match.HideGuideTip();
		yield return new WaitForSeconds(0.5f);
		match.ShowGuideTip();
		match.tip = practise.tips[3];
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

		if (_step == Step.GrabBall && match.mCurScene.mBall.m_owner != null)
			Step_Wait_Move();
	}

	private void Step_GrabBall()
	{
        IM.Transform ballTrans = GameSystem.Instance.MatchPointsConfig.PractiseMovePos.ball_transform;
		match.m_mainRole.DropBall(match.mCurScene.mBall);
		match.m_mainRole.m_StateMachine.SetState(PlayerState.State.eStand);
        match.mCurScene.mBall.transform.localPosition = (Vector3)ballTrans.position;
		match.ResetPlayerPos();

		match.ShowGuideTip();
		match.tip = practise.tips[0];

        GameObject ballPosGo = new GameObject("ballPos");
        ballPosGo.transform.position = (Vector3)ballTrans.position;
        ballPosGo.transform.rotation = (Quaternion)ballTrans.rotation;
		_obj_effect.transform.parent = ballPosGo.transform;
		_obj_effect.transform.localPosition = Vector3.zero;
		_obj_effect.SetActive(true);
		_tip_on_scene = CreateUIEffect("Prefab/GUI/TipOnScene", ballPosGo.transform);
		_tip_text = _tip_on_scene.transform.FindChild("Tip").GetComponent<UILabel>();
		_tip_text.text = practise.tips[4];

		_step = Step.GrabBall;
	}

	private void Step_Wait_Move()
	{
		match.ShowIconTip(true);
		Step_Move();
	}

	private void Step_Move()
	{
		match.tip = practise.tips[1];
		match.ShowGuideTip();
		match.ShowTouchGuide();
        IM.Transform move1Trans = GameSystem.Instance.MatchPointsConfig.PractiseMovePos.move1_transform;
        if (movePosGo == null)
            movePosGo = new GameObject("move1Pos");
        movePosGo.AddComponent<SceneTrigger>();
        movePosGo.transform.position = (Vector3)move1Trans.position;
        movePosGo.transform.rotation = (Quaternion)move1Trans.rotation;
        _obj_effect.transform.parent = movePosGo.transform;
		_obj_effect.transform.localPosition = Vector3.zero;
		_obj_effect.SetActive(true);
		_tip_on_scene.SetActive(true);
        SetUIEffect(_tip_on_scene, movePosGo.transform);
		_tip_text.text = practise.tips[5];
        movePosGo.GetComponent<SceneTrigger>().onTrigger = OnTrigger;
		_step = Step.Move1;
	}

	private void Step_Over()
	{
		match.ShowIconTip(true);
		match.tip = practise.tips[2];
		_obj_effect.SetActive(false);
		_tip_on_scene.SetActive(false);
		match.HideTouchGuide();
		_step = Step.Complete;
		if (onOver != null)
			onOver();
		if (!MainPlayer.Instance.IsPractiseCompleted(practise.ID))
			StartCoroutine(Step_Complete(true));
		else
			StartCoroutine(ReturnToListLater());
	}

	private bool OnTrigger(GameObject source, Collider collider)
	{
		if (collider.gameObject == match.m_mainRole.gameObject)
		{
            if (source == movePosGo.gameObject && _step == Step.Move1)
			{
				Step_Over();
			}
		}
		return false;
	}

	private IEnumerator ReturnToListLater()
	{
		yield return new WaitForSeconds(2);
		ReturnToList();
	}
}
