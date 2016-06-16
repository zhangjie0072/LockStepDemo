using UnityEngine;
using System.Collections.Generic;

public class UCamCtrl_SkillAction : MonoBehaviour, PlayerActionEventHandler.Listener
{
	static Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();

	string animName;
	Player player;

	Animator animator;
	UCamShakeMgr shakeMgr;
	float timeScaleBackup;
	bool timeScaleModified;

	public Vector3 shakeMagnitude = Vector3.one;

	public static void Play(Player player, string animName)
	{
		GameObject obj = null;
		if (objects.TryGetValue(animName, out obj))
		{
			if (obj.activeInHierarchy)
				return;
			else
				obj.SetActive(true);
		}
		else
		{
			Object prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Camera/" + animName);
			obj = Object.Instantiate(prefab) as GameObject;
			UCamCtrl_SkillAction camCtrl = obj.GetComponent<UCamCtrl_SkillAction>();
			camCtrl.animName = animName;
			camCtrl.player = player;
            /*
			Skybox oriSkybox = GameSystem.Instance.mClient.mCurMatch.m_cam.GetComponent<Skybox>();
			Camera newCam = obj.GetComponentInChildren<Camera>();
			Skybox newSkybox = newCam.gameObject.AddMissingComponent<Skybox>();
			newSkybox.material = oriSkybox.material;
            */
			objects.Add(animName, obj);
		}
		GameSystem.Instance.mClient.mCurMatch.m_cam.enabled = false;
	}

	void Awake()
	{
		shakeMgr = GetComponent<UCamShakeMgr>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (shakeMgr != null)
		{
			Vector3 vShake = shakeMgr.GetShakeDelta();
			transform.position += vShake;
		}
	}

	void SetAnimParam()
	{
		if (animator != null && player != null)
		{
			animator.SetFloat("PlayerX", (float)player.position.x);
            animator.SetFloat("PlayerY", (float)player.position.y);
            animator.SetFloat("PlayerZ", (float)player.position.z);
		}
	}

	void Start()
	{
		SetAnimParam();
	}

	void OnEnable()
	{
		timeScaleBackup = GameSystem.Instance.mClient.timeScale;
		SetAnimParam();

		GameSystem.Instance.mClient.mCurMatch.mCurScene.mBall.onRebound += OnRebound;
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager.m_Players)
			player.eventHandler.AddEventListener(this);
	}

	void OnDisable()
	{
		GameSystem.Instance.mClient.mCurMatch.mCurScene.mBall.onRebound -= OnRebound;
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager.m_Players)
			player.eventHandler.RemoveEventListener(this);
	}

	void OnDestroy()
	{
		objects.Remove(animName);
	}

	public void OnEvent(PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
		if (animEvent == PlayerActionEventHandler.AnimEvent.eBlock)
			OnFinish();
		else if (animEvent == PlayerActionEventHandler.AnimEvent.eIntercepted)
			OnFinish();
	}

	void OnRebound(UBasketball ball)
	{
		OnFinish();
	}

	void Shake(float shakeDuration = 0.5f)
	{
		if (shakeMgr != null)
		{
			shakeMgr.m_CamCtrl = GameSystem.Instance.mClient.mCurMatch.m_cam;
			shakeMgr.AddCamShake(shakeMagnitude, shakeDuration, transform.position);
		}
	}

	void PlaySpeed(float speed = 1f)
	{
		timeScaleModified = true;
		GameSystem.Instance.mClient.timeScale = speed;
	}

	void OnFinish()
	{
		gameObject.SetActive(false);
		GameSystem.Instance.mClient.mCurMatch.m_cam.enabled = true;
		if (timeScaleModified)
			GameSystem.Instance.mClient.timeScale = timeScaleBackup;
	}
}
