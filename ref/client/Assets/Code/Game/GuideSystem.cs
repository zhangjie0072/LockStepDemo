using fogs.proto.msg;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class GuideSystem : Singleton<GuideSystem>
{
	const bool IS_NETWORKING = true;

	public GuideModule curModule { get; private set; }
	uint readyModuleID;
	public GuideStep curStep { get; private set; }
	GuideStep readyStep;

	public bool requestingBeingGuide { get; private set; }

	bool isDebugging = false;

	GameObject prefabMask;
	GameObject prefabTip;
	GameObject prefabTapEffect;
	GameObject prefabArrowEffect;
	GameObject prefabHalo;
	GameObject prefabBlinkArea;

	List<GameObject> masks = new List<GameObject>();
	GameObject tip;
	UIPanel panelInstructor;
	Collider shutter;

	Dictionary<string, bool> buttonStates = new Dictionary<string, bool>();
	List<GameObject> listenedFinishButton = new List<GameObject>();
	List<GameObject> effects = new List<GameObject>();
	GameUtils.Timer4View timerFinishStep;

	public bool guideHiding = false;

	bool showTipAfterAnimating = false;

	//special guide
	bool toListenPractiseGuide = false;

	bool checkUncompleted = true;

	bool initialized = false;

	Transform basePanel
	{
		get { return GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform; }
	}

	public GuideSystem()
	{
	}

	public void Init()
	{
		if (initialized)
			return;
        prefabMask = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/GuideMask") as GameObject;
        prefabTip = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/GuideTip") as GameObject;
        prefabTapEffect = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TapEffect") as GameObject;
        prefabArrowEffect = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ArrowEffect") as GameObject;
        prefabHalo = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/YellowHalo") as GameObject;
        prefabBlinkArea = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/BlinkArea") as GameObject;

		timerFinishStep = new GameUtils.Timer4View(3f, EndCurStep);
		timerFinishStep.stop = true;

		initialized = true;
	}

	public void Update()
	{
		if (curStep == null && readyStep != null)
		{
			if (string.IsNullOrEmpty(readyStep.uiName) || IsUIVisible(readyStep.uiName, readyStep.highlightButton))
			{
				GuideStep stepToRun = readyStep;
				readyStep = null;
				RunStep(stepToRun);
			}
		}
		else if (curStep != null)
		{
			bool isUIVisible = IsUIVisible(curStep.uiName);
			bool isButtonCovered = !IsUIVisible(curStep.uiName, curStep.highlightButton);

			if (curStep.conditions.Contains(GuideStep.CompleteCondition.EndWithUI) && !isUIVisible)
			{
				EndCurStep();
			}
			else
			{
				if (isUIVisible)
				{
					if (!guideHiding && isButtonCovered)
						Hide(true);
					else if (guideHiding && !isButtonCovered)
						Hide(false);

					Animator animator = basePanel.FindChild(curStep.uiName).GetComponent<Animator>();
					if (showTipAfterAnimating && !IsAnimating(animator))
					{
						ShowTip(curStep);
						showTipAfterAnimating = false;
					}
				}
				else
				{
					//Interrupted
					ModuleClear();
				}
			}
		}

		if (timerFinishStep != null)
			timerFinishStep.Update(Time.deltaTime);
		
		if (toListenPractiseGuide)
		{
			GameMatch_Practise match = GameSystem.Instance.mClient.mCurMatch as GameMatch_Practise;
			if (match != null)
			{
				PractiseBehaviourBaseGuide behaviour = match.practise_behaviour as PractiseBehaviourBaseGuide;
				if (behaviour != null)
				{
					Transform tmBasePanel = GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform;
					Transform tmBackButton = tmBasePanel.FindChild("UIPractise(Clone)/ButtonBack(Clone)");
					if (tmBackButton != null)
						NGUITools.SetActive(tmBackButton.gameObject, false);
					behaviour.onOver += OnPractiseGuideOver;
					toListenPractiseGuide = false;
				}

				PractiseBehaviourSkillGuide behaviour1 = match.practise_behaviour as PractiseBehaviourSkillGuide;
				if (behaviour1 != null)
				{
					behaviour1.onOver += OnPractiseGuideOver;
					toListenPractiseGuide = false;
				}
			}
		}
	}

	public void OnLevelWasLoaded(int level)
	{
		if (!initialized)
			return;
		listenedFinishButton.Clear();
		timerFinishStep.stop = true;
		buttonStates.Clear();
		effects.Clear();
		masks.Clear();
		tip = null;

		guideHiding = false;
		shutter = null;
	}

	public void ModuleClear()
	{
		checkUncompleted = true;
		StepClear();
		curModule = null;
		readyModuleID = 0;
		curStep = null;
		readyStep = null;
	}

	public void ReqBeginGuide(string uiName)
	{
		if (!GlobalConst.IS_GUIDE)
			return;
		if (checkUncompleted)
		{
			checkUncompleted = false;
			uint uncompletedGuide = MainPlayer.Instance.GetUncompletedGuide();
			if (uncompletedGuide != 0)
			{
				GuideModule module = GameSystem.Instance.GuideConfig.GetModule(uncompletedGuide);
				if (module.restartStep == 0)
				{
					ReqBeginGuide(uncompletedGuide);
					return;
				}
				else
				{
                    if (module.skipConditions.Count > 0)	//directly skip
                    {
                        if (ConditionValidator.Instance.Validate(module.skipConditions, module.skipConditionParams, true))
                        {
                            Logger.Log("Skip guide: " + module.ID);
                            ReqBeginGuide(module.ID);
                            return;
                        }
                    }

					curStep = GameSystem.Instance.GuideConfig.GetStep(00001);
					if (curStep == null)
						Logger.LogError("GuideSystem: missing special step(00001) config.");
					HighlightControls(curStep);
					ShowTip(curStep);
					return;
				}
			}
		}
		List<GuideModule> modules = GameSystem.Instance.GuideConfig.GetModules(uiName);
		if (modules != null)
		{
			foreach (GuideModule module in modules)
			{
				if (ReqBeginGuide(module.ID))
					break;
			}
		}
	}

	public bool ReqBeginGuide(uint id, bool debug = false)
	{
		Logger.Log("Request to begin guide module " + id);
		if (curModule != null && curModule.nextModule != id)
		{
			Logger.Log("Request to begin guide module " + id + " but a guide is running ID: " + curModule.ID);
			return false;
		}
		if (!debug)
		{
			if (MainPlayer.Instance.IsGuideCompleted(id))
			{
				Logger.Log("Request to begin guide module " + id + " but this guide is completed.");
				return false;
			}
			GuideModule module = GameSystem.Instance.GuideConfig.GetModule(id);
			if (module == null)
				Logger.LogError("Guide module " + id + " not found.");
			//if (module.skipConditions.Count > 0)
			//{
			//	if (ConditionValidator.Instance.Validate(module.skipConditions, module.skipConditionParams))
			//	{
			//		isDebugging = debug;
			//		SendBeginGuideReq(id, debug);
			//		return false;	//This guide will be skipped, continue on next guide.
			//	}
			//}
			if (!ConditionValidator.Instance.Validate(module.conditionTypes, module.conditionParams))
				return false;
		}
		isDebugging = debug;
		SendBeginGuideReq(id, debug);
		return true;
	}

	public void SendBeginGuideReq(uint id, bool debug = false)
	{
		if (IS_NETWORKING)
		{
			BeginGuideModuleReq req = new BeginGuideModuleReq();
			req.module_id = id;
			req.debug = debug ? 1u : 0u;
			GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.BeginGuideReqID);
			Logger.Log("Send begin guide req " + id);
			CommonFunction.ShowWaitMask();
			requestingBeingGuide = true;
		}
		else
		{
			if (curModule == null)
			{
				BeginGuide(id, debug);
			}
			else
			{
				readyModuleID = id;
			}
		}
	}

	public void ReqEndGuide(uint id)
	{
		if (MainPlayer.Instance.IsGuideCompleted(id))
			Logger.LogError("Can not request to end a completed guide. ID: " + id);
		if (IS_NETWORKING)
		{
			Logger.Log("Send end guide req, ID: " + id);
			EndGuideModuleReq req = new EndGuideModuleReq();
			req.module_id = id;
			GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.EndGuideReqID);
		}
		else
		{
			EndGuide(id);
		}
	}

	public void BeginGuideHandler(Pack pack)
	{
		requestingBeingGuide = false;
		CommonFunction.HideWaitMask();
		BeginGuideModuleResp resp = Serializer.Deserialize<BeginGuideModuleResp>(new MemoryStream(pack.buffer));
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS)
		{
			Logger.Log("Begin guide " + resp.module_id + " from server.");
			isDebugging = (resp.debug == 1);
			if (curModule == null)
			{
				BeginGuide(resp.module_id, isDebugging);
			}
			else if (curModule.ID != resp.module_id)
			{
				Logger.Log("Guide module: " + curModule.ID + " is still running. Wait it end.");
				readyModuleID = resp.module_id;
			}
			else
			{
				Logger.Log("Guide module: " + curModule.ID + " is already running. Ignore this time.");
			}
		}
		else
		{
			CommonFunction.ShowErrorMsg(err);
		}
	}

	public void EndGuideHandler(Pack pack)
	{
		EndGuideModuleResp resp = Serializer.Deserialize<EndGuideModuleResp>(new MemoryStream(pack.buffer));
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS)
		{
			EndGuide(resp.module_id, resp.awards);
		}
		else
		{
			CommonFunction.ShowErrorMsg(err);
		}
	}

	void BeginGuide(uint moduleID, bool debug = false)
	{
		GuideModule module = GameSystem.Instance.GuideConfig.GetModule(moduleID);
		bool isRestart = (MainPlayer.Instance.GetUncompletedGuide() == moduleID);
		if (isRestart && module.restartStep == 0)
		{
			ReqEndGuide(moduleID);
			return;
		}
		if (!isRestart && !IsUIVisible(module.uiName))	// UI visible is not required by restarted guide module
		{
			Logger.Log("Begin guide, UI not visible, ID: " + module.ID + " UI: " + module.uiName);
			return;
		}
		if (!debug)
		{
			if (module.skipConditions.Count > 0)	//directly skip
			{
				if (ConditionValidator.Instance.Validate(module.skipConditions, module.skipConditionParams, true))
				{
					Logger.Log("Skip guide: " + module.ID);
					ReqEndGuide(module.ID);
					return;
				}
			}
			if (!ConditionValidator.Instance.Validate(module.conditionTypes, module.conditionParams))
				return;
		}
		curModule = module;
		uint firstStep = isRestart ? module.restartStep : module.firstStep;
		readyStep = GameSystem.Instance.GuideConfig.GetStep(firstStep);
	}

	void EndGuide(uint moduleID, List<KeyValueData> awards = null)
	{
		Logger.Log("End guide " + moduleID);
		MainPlayer.Instance.SetGuideCompleted(moduleID);
		if (awards != null && awards.Count > 0)
			ShowAwardsAcquire(awards);
		GuideModule module = GameSystem.Instance.GuideConfig.GetModule(moduleID);
		if (module.nextModule != 0)
			ReqBeginGuide(module.nextModule, isDebugging);
		else
			ReqBeginGuide(module.uiName);
	}

	void ShowAwardsAcquire(List<KeyValueData> awards)
	{
		LuaComponent luaCom = UIManager.Instance.CreateUI("Prefab/GUI/GoodsAcquirePopup").GetComponent<LuaComponent>();
		LuaInterface.LuaFunction setGoodsData = luaCom.table["SetGoodsData"] as LuaInterface.LuaFunction;
		foreach (KeyValueData award in awards)
		{
			setGoodsData.Call(new object[] { luaCom.table, award.id, award.value });
		}
		UIManager.Instance.BringPanelForward(luaCom.gameObject);
	}

	bool IsUIVisible(string uiName, string checkCoverButton = "")
	{
		Transform ui = basePanel.FindChild(uiName);
		if (ui != null && NGUITools.GetActive(ui.gameObject))
		{
            if (checkCoverButton != "")
            {
				Transform button = basePanel.FindChild(checkCoverButton);
				if (button == null)
				{
					Logger.Log("Button not found: " + checkCoverButton);
					return false;
				}
				Camera cam = UICamera.currentCamera;
				if (cam == null)
					return true;
				Vector3 viewPoint = cam.WorldToViewportPoint(button.transform.position);
				//是否在屏幕外
				if (viewPoint.x < 0f || viewPoint.x > 1f || viewPoint.y < 0f || viewPoint.y > 1f)
					return true;
				//检测是否正在播动画
				Animator animator = button.GetComponentInParent<Animator>();
				if (animator != null)
				{
					// The position of GameObject that controlled by animation is 0 at animation's beginning. Consider it as invisible.
					if (AtBeginning(animator))
						return false;
					// No ray cast test when animating.
					if (IsAnimating(animator))
						return true;
				}
				//射线检测
				Vector3 screenPoint = cam.WorldToScreenPoint(button.transform.position);
				if (shutter == null)
				{
					RaycastHit hit;
					if (UICamera.Raycast(screenPoint, out hit))
					{
						if (hit.collider.transform == button)
							return true;
						else
						{
							shutter = hit.collider;
							Logger.Log("Guide, shuttered by " + shutter.name);
							return false;
						}
					}
					else
					{
						//Logger.LogError("Guide, ray cast failed on highlight button: " + checkCoverButton);
						return false;
					}
				}
				else
				{
					Ray ray = cam.ScreenPointToRay(screenPoint);
					if (shutter.bounds.IntersectRay(ray) && 
						NGUITools.CalculateRaycastDepth(shutter.gameObject) > NGUITools.CalculateRaycastDepth(button.gameObject))
						return false;
					else
					{
						shutter = null;
						return true;
					}
				}
            }
			return true;
		}

		if (!uiName.EndsWith("(Clone)"))
		{
			return IsUIVisible(uiName + "(Clone)", checkCoverButton);
		}
		return false;
	}

	bool AtBeginning(Animator animator)
	{
		AnimatorClipInfo[] infos = animator.GetCurrentAnimatorClipInfo(0);
		if (infos.Length == 0)
			return false;
		bool isInTransition = animator.IsInTransition(0);
		float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if (!isInTransition && time == 0f)
			return true;
		else
			return false;
	}

	bool IsAnimating(Animator animator)
	{
		AnimatorClipInfo[] infos = animator.GetCurrentAnimatorClipInfo(0);
		if (infos.Length == 0)
			return false;
		bool isInTransition = animator.IsInTransition(0);
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		if (isInTransition || info.normalizedTime <= 1f || info.loop)	//transition状态时，time值很大很奇怪
			return true;
		else
			return false;
	}

	void Hide(bool hide = true)
	{
		foreach (GameObject mask in masks)
			NGUITools.SetActive(mask, !hide, false);
		NGUITools.SetActive(tip, !hide, false);
		foreach (GameObject effect in effects)
			NGUITools.SetActive(effect, !hide, false);
		guideHiding = hide;
	}

	void RunStep(GuideStep step)
	{
		Logger.Log("Run guide step " + step.ID);
		curStep = step;
		if (string.IsNullOrEmpty(step.specialFunc))
		{
			HighlightControls(step);
			Animator animator = basePanel.FindChild(curStep.uiName).GetComponent<Animator>();
			if (animator != null && IsAnimating(animator))
				showTipAfterAnimating = true;
			else
				ShowTip(step);
			if (step.conditions.Contains(GuideStep.CompleteCondition.Delay3Seconds))
			{
				timerFinishStep.stop = false;
			}
			guideHiding = false;
			shutter = null;
		}
		else
		{
			MethodInfo method = GetType().GetMethod(step.specialFunc);
			if (method != null)
				method.Invoke(this, new object[] { });
			else
				LuaScriptMgr.Instance.DoString(step.specialFunc);

			if (step.conditions.Contains(GuideStep.CompleteCondition.AfterFunc))
			{
				EndCurStep();
			}
		}
	}

	void StepClear()
	{
		foreach (GameObject button in listenedFinishButton)
		{
			UIEventListener.Get(button).onClick -= OnFinishStepClick;
            if (button.GetComponent<UIScrollView>() != null)
                button.GetComponent<UIScrollView>().enabled = true;
		}
		listenedFinishButton.Clear();
		timerFinishStep.stop = true;

		ResumeButtonState();

		foreach (GameObject effect in effects)
		{
			NGUITools.Destroy(effect);
		}
		effects.Clear();

		foreach (GameObject mask in masks)
		{
			if (mask != null)
			{
				NGUITools.Destroy(mask);
			}
		}
		masks.Clear();
		if (tip != null)
		{
			NGUITools.Destroy(tip);
			Logger.Log("Clear tip, " + (curStep != null ? curStep.ID.ToString() : "NULL"));
			tip = null;
		}
		if (panelInstructor != null)
		{
			NGUITools.Destroy(panelInstructor.gameObject);
			panelInstructor = null;
		}

		showTipAfterAnimating = false;
		guideHiding = false;
		shutter = null;
	}

	void EndStep(GuideStep step)
	{
		StepClear();


		if (!string.IsNullOrEmpty(step.linkID))
		{
			GameSystem.Instance.mClient.mUIManager.ShowSpecifiedUI(step.linkID, step.linkSubID);
		}

		if (step.ID == curModule.endStep || (step.nextStep == 0 && curModule.endStep == 0))
		{
			ReqEndGuide(curModule.ID);
		}
		readyStep = GameSystem.Instance.GuideConfig.GetStep(step.nextStep);
		curStep = null;

		if (readyStep == null)
		{
			curModule = null;
			if (readyModuleID != 0)
			{
				BeginGuide(readyModuleID, isDebugging);
				readyModuleID = 0;
			}
		}
	}

	void HighlightControls(GuideStep step)
	{
		if (step.highlightCtrls.Count == 0)
		{
			Logger.Log("Create full screen tip, " + step.ID.ToString());
			UIPanel maskPanel = NGUITools.AddChild<UIPanel>(basePanel.gameObject);
			GameObject mask = maskPanel.gameObject;
			//mask.AddComponent<UIManagedPanel>();
			mask.transform.localPosition = new Vector3(0f, 0f, -600f);
			masks.Add(mask);
			mask.name = "GuideMaskPanel";
			//NGUITools.BringForward(mask);
			maskPanel.depth = 10000;
			GameObject bg = CommonFunction.InstantiateObject(prefabMask, maskPanel.transform);
			bg.GetComponent<UIWidget>().depth = -1;
			if (step.conditions.Contains(GuideStep.CompleteCondition.ClickAnywhere))
			{
				UIEventListener.Get(bg).onClick += OnFinishStepClick;
				listenedFinishButton.Add(bg);
			}
		}
		else if (step.highlightCtrls[0] != "All")
		{
			//Create mask panel
			UIPanel maskPanel = NGUITools.AddChild<UIPanel>(basePanel.gameObject);
			GameObject mask = maskPanel.gameObject;
			//mask.AddComponent<UIManagedPanel>();
			mask.transform.localPosition = new Vector3(0f, 0f, -600f);
			masks.Add(mask);
			mask.name = "GuideMaskPanel";
			//NGUITools.BringForward(mask);
			maskPanel.depth = 10000;

			//Calculate highlight controls bounds
			string highlightCtrl = step.highlightCtrls[0];
			Transform tmHLCtrl = basePanel.FindChild(highlightCtrl);
			if (tmHLCtrl == null)
				Logger.LogError("Highlight control not found: " + highlightCtrl);
			if (tmHLCtrl.GetComponent<UIWidget>() == null)
				Logger.LogError("Highlight control not widget: " + highlightCtrl);
			Bounds bounds = tmHLCtrl.GetComponent<UIWidget>().CalculateBounds(basePanel);
			//Mask top
			GameObject maskTop = CommonFunction.InstantiateObject(prefabMask, maskPanel.transform);
			maskTop.name = "MaskTop";
			UIWidget widgetTop = maskTop.GetComponent<UIWidget>();
			widgetTop.depth = 0;
			widgetTop.pivot = UIWidget.Pivot.Bottom;

            UITexture txr_top = maskTop.GetComponent<UITexture>();
			//maskTop.transform.localPosition = new Vector3(0f, Mathf.Round(bounds.max.y), 0f);
			//widgetTop.height = Mathf.RoundToInt((float)widgetTop.height / 2 - bounds.max.y);
			if (step.conditions.Contains(GuideStep.CompleteCondition.ClickAnywhere))
			{
				UIEventListener.Get(maskTop).onClick += OnFinishStepClick;
				listenedFinishButton.Add(maskTop);
			}
			//Mask bottom
			GameObject maskBottom = CommonFunction.InstantiateObject(prefabMask, maskPanel.transform);
			maskBottom.name = "MaskBottom";
			UIWidget widgetBottom = maskBottom.GetComponent<UIWidget>();
			widgetBottom.depth = 0;
			widgetBottom.pivot = UIWidget.Pivot.Top;

            UITexture txr_btm = maskBottom.GetComponent<UITexture>();
            //maskBottom.transform.localPosition = new Vector3(0f, Mathf.Round(bounds.min.y), 0f);
            //widgetBottom.height = Mathf.RoundToInt((float)widgetBottom.height / 2 + bounds.min.y);
            if (step.conditions.Contains(GuideStep.CompleteCondition.ClickAnywhere))
			{
				UIEventListener.Get(maskBottom).onClick += OnFinishStepClick;
				listenedFinishButton.Add(maskBottom);
			}
			//Mask left
			GameObject maskLeft = CommonFunction.InstantiateObject(prefabMask, maskPanel.transform);
			maskLeft.name = "MaskLeft";
			UIWidget widgetLeft = maskLeft.GetComponent<UIWidget>();
			widgetLeft.depth = 0;
			widgetLeft.pivot = UIWidget.Pivot.BottomRight;

            UITexture txr_lft = maskLeft.GetComponent<UITexture>();
            //maskLeft.transform.localPosition = new Vector3(Mathf.RoundToInt(bounds.min.x), Mathf.RoundToInt(bounds.min.y), 0f);
            //widgetLeft.width = Mathf.RoundToInt((float)widgetLeft.width / 2 + bounds.min.x);
            //widgetLeft.height = Mathf.RoundToInt(bounds.size.y);
            if (step.conditions.Contains(GuideStep.CompleteCondition.ClickAnywhere))
			{
				UIEventListener.Get(maskLeft).onClick += OnFinishStepClick;
				listenedFinishButton.Add(maskLeft);
			}
			//Mask right
			GameObject maskRight = CommonFunction.InstantiateObject(prefabMask, maskPanel.transform);
			maskRight.name = "MaskRight";
			UIWidget widgetRight = maskRight.GetComponent<UIWidget>();
			widgetRight.depth = 0;
			widgetRight.pivot = UIWidget.Pivot.BottomLeft;

            UITexture txr_rgt = maskRight.GetComponent<UITexture>();
            //maskRight.transform.localPosition = new Vector3(Mathf.RoundToInt(bounds.max.x), Mathf.RoundToInt(bounds.min.y), 0f);
            //widgetRight.width = Mathf.RoundToInt((float)widgetRight.width / 2 - bounds.max.x);
            //widgetRight.height = Mathf.RoundToInt(bounds.size.y);
            if (step.conditions.Contains(GuideStep.CompleteCondition.ClickAnywhere))
			{
				UIEventListener.Get(maskRight).onClick += OnFinishStepClick;
				listenedFinishButton.Add(maskRight);
			}
			//Mask center
			Transform tmParent = tmHLCtrl.parent;
			if (tmParent.GetComponent<UIGrid>() != null)
				tmParent = tmParent.parent;
			GameObject maskCenter = CommonFunction.InstantiateObject(prefabMask, tmParent);
			Vector3 eulerAngles = maskCenter.transform.eulerAngles;
			eulerAngles.z = 0f;
			maskCenter.transform.eulerAngles = eulerAngles;
			masks.Add(maskCenter);
			maskCenter.name = "MaskCenter";
			UIWidget widgetCenter = maskCenter.GetComponent<UIWidget>();
			//Bounds localBounds = tmHLCtrl.GetComponent<UIWidget>().CalculateBounds(tmHLCtrl.parent);
			//maskCenter.transform.localPosition = new Vector3(Mathf.RoundToInt(localBounds.center.x), Mathf.RoundToInt(localBounds.center.y), 0f);
			//widgetCenter.width = Mathf.RoundToInt(localBounds.size.x);
			//widgetCenter.height = Mathf.RoundToInt(localBounds.size.y);
			int minDepth = UIManager.Instance.GetMinDepth(tmHLCtrl.gameObject);
			UIManager.Instance.AdjustDepth(tmHLCtrl.gameObject, 1);
			widgetCenter.depth = minDepth;
			if (step.conditions.Contains(GuideStep.CompleteCondition.ClickAnywhere))
			{
				UIEventListener.Get(maskCenter).onClick += OnFinishStepClick;
				listenedFinishButton.Add(maskCenter);
			}

			//set anchors
			//center
			widgetCenter.topAnchor.target = tmHLCtrl;
			widgetCenter.topAnchor.Set(1f, 0f);
			widgetCenter.bottomAnchor.target = tmHLCtrl;
			widgetCenter.bottomAnchor.Set(0f, 0f);
			widgetCenter.leftAnchor.target = tmHLCtrl;
			widgetCenter.leftAnchor.Set(0f, 0f);
			widgetCenter.rightAnchor.target = tmHLCtrl;
			widgetCenter.rightAnchor.Set(1f, 0f);
			widgetCenter.ResetAnchors();
			//top
			widgetTop.bottomAnchor.target = widgetCenter.transform;
			widgetTop.bottomAnchor.Set(1f, 0f);
			widgetTop.ResetAnchors();
			//bottom
			widgetBottom.topAnchor.target = widgetCenter.transform;
			widgetBottom.topAnchor.Set(0f, 0f);
			widgetBottom.ResetAnchors();
			//left
			widgetLeft.topAnchor.target = widgetCenter.transform;
			widgetLeft.topAnchor.Set(1f, 0f);
			widgetLeft.bottomAnchor.target = widgetCenter.transform;
			widgetLeft.bottomAnchor.Set(0f, 0f);
			widgetLeft.rightAnchor.target = widgetCenter.transform;
			widgetLeft.rightAnchor.Set(0f, 0f);
			widgetLeft.ResetAnchors();
			//right
			widgetRight.topAnchor.target = widgetCenter.transform;
			widgetRight.topAnchor.Set(1f, 0f);
			widgetRight.bottomAnchor.target = widgetCenter.transform;
			widgetRight.bottomAnchor.Set(0f, 0f);
			widgetRight.leftAnchor.target = widgetCenter.transform;
			widgetRight.leftAnchor.Set(1f, 0f);
			widgetRight.ResetAnchors();

			//Handle control effect
			if (step.ctrlEffects.Count > 0)
			{
				GameObject effectNode = new GameObject("EffectNode");
				effectNode.layer = LayerMask.NameToLayer("GUI");
				effectNode.transform.parent = maskPanel.transform;
				effectNode.transform.localScale = Vector3.one;
				//effectNode.transform.position = tmHLCtrl.position;
				//Vector3 localPosition = effectNode.transform.localPosition;
				//localPosition.z = -650f;
				//effectNode.transform.localPosition = localPosition;
				UIWidget widgetEffect = effectNode.AddComponent<UIWidget>();
				widgetEffect.topAnchor.target = tmHLCtrl;
				widgetEffect.topAnchor.Set(1f, 0f);
				widgetEffect.bottomAnchor.target = tmHLCtrl;
				widgetEffect.bottomAnchor.Set(0f, 0f);
				widgetEffect.leftAnchor.target = tmHLCtrl;
				widgetEffect.leftAnchor.Set(0f, 0f);
				widgetEffect.rightAnchor.target = tmHLCtrl;
				widgetEffect.rightAnchor.Set(1f, 0f);
				widgetEffect.ResetAnchors();
				AddControlEffect(step.ctrlEffects, null, effectNode.transform, tmHLCtrl.GetComponent<UIWidget>());
			}

			//Handle highlight button
			if (!string.IsNullOrEmpty(step.highlightButton))
			{
				Transform tmHLBtn = basePanel.FindChild(step.highlightButton);
				if (tmHLBtn == null)
					Logger.LogError("Highlight control not found: " + step.highlightButton);
				GameObject effectNode = new GameObject("EffectNode");
				effectNode.layer = LayerMask.NameToLayer("GUI");
				effectNode.transform.parent = maskPanel.transform;
				effectNode.transform.localScale = Vector3.one;
				//effectNode.transform.position = tmHLBtn.position;
				//Vector3 localPosition = effectNode.transform.localPosition;
				//localPosition.z = -650f;
				//effectNode.transform.localPosition = localPosition;
				UIWidget widgetEffect = effectNode.AddComponent<UIWidget>();
				widgetEffect.topAnchor.target = tmHLCtrl;
				widgetEffect.topAnchor.Set(1f, 0f);
				widgetEffect.bottomAnchor.target = tmHLCtrl;
				widgetEffect.bottomAnchor.Set(0f, 0f);
				widgetEffect.leftAnchor.target = tmHLCtrl;
				widgetEffect.leftAnchor.Set(0f, 0f);
				widgetEffect.rightAnchor.target = tmHLCtrl;
				widgetEffect.rightAnchor.Set(1f, 0f);
				widgetEffect.ResetAnchors();
				AddControlEffect(step.buttonEffects, step.effectPos, effectNode.transform, tmHLBtn.GetComponent<UIWidget>());
				if (step.conditions.Contains(GuideStep.CompleteCondition.ClickHighlightButton))
				{
					if (tmHLBtn.GetComponent<BoxCollider>() == null)
						Logger.LogError("Guide: Highlight button have no BoxCollider. " + step.highlightButton);
					UIEventListener.Get(tmHLBtn.gameObject).onClick += OnFinishStepClick;
					listenedFinishButton.Add(tmHLBtn.gameObject);
                    UIDragScrollView component = tmHLBtn.GetComponent<UIDragScrollView>();
                    if (component != null)
                        component.enabled = false;
				}
			}
		}
		else if (step.conditions.Contains(GuideStep.CompleteCondition.ClickHighlightButton))
		{
			Transform tmHLBtn = basePanel.FindChild(step.highlightButton);
			if (tmHLBtn == null)
				Logger.LogError("Highlight control not found: " + step.highlightButton);
			if (tmHLBtn.GetComponent<BoxCollider>() == null)
				Logger.LogError("Guide: Highlight button have no BoxCollider. " + step.highlightButton);
			UIEventListener.Get(tmHLBtn.gameObject).onClick += OnFinishStepClick;
			listenedFinishButton.Add(tmHLBtn.gameObject);
		}
		else if (step.conditions.Contains(GuideStep.CompleteCondition.ClickAnywhere))
		{
			BoxCollider[] colliders = basePanel.GetComponentsInChildren<BoxCollider>();
			foreach (BoxCollider collider in colliders)
			{
				UIEventListener.Get(collider.gameObject).onClick += OnFinishStepClick;
				listenedFinishButton.Add(collider.gameObject);
			}
		}

		foreach (string disabledButton in step.disabledButtons)
		{
			Transform tm = basePanel.FindChild(disabledButton);
			if (tm == null)
				Logger.LogError("Disabled button not found: " + disabledButton);
			BoxCollider collider = tm.GetComponent<BoxCollider>();
			buttonStates.Add(disabledButton, collider.enabled);
			collider.enabled = false;
		}
	}

	public void AddControlEffect(List<ControlEffect> buttonEffects, List<Vector2> pos, Transform node, UIWidget control = null)
	{
		for (int i = 0; i < buttonEffects.Count; ++i)
		{
			ControlEffect effectType = buttonEffects[i];
			GameObject effect = null;
			if (effectType == ControlEffect.Finger)
			{
				effect = CommonFunction.InstantiateObject(prefabTapEffect, node);
			}
			else if (effectType == ControlEffect.TopArrow || effectType == ControlEffect.BottomArrow ||
					 effectType == ControlEffect.LeftArrow || effectType == ControlEffect.RightArrow)
			{
				effect = CommonFunction.InstantiateObject(prefabArrowEffect, node);
				if (effectType == ControlEffect.BottomArrow)
					effect.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
				else if (effectType == ControlEffect.LeftArrow)
					effect.transform.localEulerAngles = new Vector3(0f, 0f, 90);
				else if (effectType == ControlEffect.RightArrow)
					effect.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			}
			else if (effectType == ControlEffect.Halo)
			{
				effect = CommonFunction.InstantiateObject(prefabHalo, node);
			}
			else if (effectType == ControlEffect.BlinkArea)
			{
				effect = CommonFunction.InstantiateObject(prefabBlinkArea, node);
				UIWidget widgetEffect = effect.GetComponent<UIWidget>();
				widgetEffect.leftAnchor.target = control.transform;
				widgetEffect.leftAnchor.Set(0f, 0f);
				widgetEffect.rightAnchor.target = control.transform;
				widgetEffect.rightAnchor.Set(1f, 0f);
				widgetEffect.topAnchor.target = control.transform;
				widgetEffect.topAnchor.Set(1f, 0f);
				widgetEffect.bottomAnchor.target = control.transform;
				widgetEffect.bottomAnchor.Set(0f, 0f);
				widgetEffect.ResetAnchors();
			}
			effect.transform.localPosition = new Vector3(0f, 0f, 0f);
			if (pos != null && pos.Count > i)
				effect.transform.localPosition = new Vector3(pos[i].x, pos[i].y, 0f);
			UIManager.Instance.AdjustDepth(effect, i + 1);
			effects.Add(effect);
		}
	}

	void ResumeButtonState()
	{
		foreach (KeyValuePair<string, bool> state in buttonStates)
		{
			Transform button = basePanel.FindChild(state.Key);
			if (button != null)
			{
				button.GetComponent<BoxCollider>().enabled = state.Value;
			}
		}
		buttonStates.Clear();
	}

	public void ShowTip(GuideStep step)
	{
		tip = CommonFunction.InstantiateObject(prefabTip, basePanel);
		tip.GetComponent<UIPanel>().depth = 10001;
		tip.transform.localPosition = new Vector3(step.tipPos.x, step.tipPos.y, -650f);
		if (string.IsNullOrEmpty(step.tipText))
		{
			NGUITools.SetActive(tip.transform.FindChild("Tip").gameObject, false);
			NGUITools.SetActive(tip.transform.FindChild("Button").gameObject, false);
			NGUITools.SetActive(tip.transform.FindChild("Frame").gameObject, false);
		}
		else
		{
			tip.transform.FindChild("Tip").GetComponent<UILabel>().text = step.tipText;
			UIWidget button = tip.transform.FindChild("Button").GetComponent<UIWidget>();
			UIWidget frame = tip.transform.FindChild("Frame").GetComponent<UIWidget>();
			UIWidget arrow = frame.transform.FindChild("Arrow").GetComponent<UIWidget>();
			if (!string.IsNullOrEmpty(step.tipButtonText))
			{
				if (step.tipButtonText.StartsWith("\\"))
				{
					NGUITools.SetActive(button.gameObject, false);
					if (step.tipButtonText == "\\NEXT")
					{
						button = tip.transform.FindChild("Next").GetComponent<UIWidget>();
						NGUITools.SetActive(button.gameObject, true);
					}
				}
				else
				{
					UILabel label = button.transform.FindChild("Label").GetComponent<UILabel>();
					label.text = step.tipButtonText;
					label.leftAnchor.target = label.rightAnchor.target;
					label.leftAnchor.Set(label.rightAnchor.relative, label.rightAnchor.absolute - step.tipButtonText.Length * label.height);
					label.ResetAnchors();
				}
				if (step.conditions.Contains(GuideStep.CompleteCondition.ClickTipButton))
				{
					UIEventListener.Get(button.gameObject).onClick += OnFinishStepClick;
					listenedFinishButton.Add(button.gameObject);
				}
				else
				{
					button.GetComponent<BoxCollider>().enabled = false;
				}

				GameObject secondButton = tip.transform.FindChild("SecondButton").gameObject;
				if (!string.IsNullOrEmpty(step.secondButtonText))
				{
					NGUITools.SetActive(secondButton, true);
					UIEventListener.Get(secondButton).onClick += OnSecondButtonClick;
					UILabel label = secondButton.transform.FindChild("Label").GetComponent<UILabel>();
					label.text = step.secondButtonText;
					label.rightAnchor.target = label.leftAnchor.target;
					label.rightAnchor.Set(label.leftAnchor.relative, label.leftAnchor.absolute + step.secondButtonText.Length * label.height);
					label.ResetAnchors();
				}
			}
			else
			{
				NGUITools.SetActive(button.gameObject, false);
				frame.bottomAnchor.target = frame.topAnchor.target;
				frame.bottomAnchor.Set(0f, -frame.topAnchor.absolute);
			}

			frame.UpdateAnchors();
			switch (step.tipArrowType)
			{
				case GuideStep.TipArrowType.None:
					NGUITools.SetActive(arrow.gameObject, false);
					break;
				case GuideStep.TipArrowType.Top:
					NGUITools.SetActive(arrow.gameObject, true);
					arrow.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
					arrow.transform.localPosition = new Vector3(step.tipArrowOffset, frame.height / 2 - 0, 0f);
					break;
				case GuideStep.TipArrowType.Bottom:
					NGUITools.SetActive(arrow.gameObject, true);
					arrow.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					arrow.transform.localPosition = new Vector3(step.tipArrowOffset, -frame.height / 2 + 0, 0f);
					break;
				case GuideStep.TipArrowType.Left:
					NGUITools.SetActive(arrow.gameObject, true);
					arrow.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
					arrow.transform.localPosition = new Vector3(-frame.width / 2 + 0, step.tipArrowOffset, 0f);
					break;
				case GuideStep.TipArrowType.Right:
					NGUITools.SetActive(arrow.gameObject, true);
					arrow.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
					arrow.transform.localPosition = new Vector3(frame.width / 2 - 0, step.tipArrowOffset, 0f);
					break;
			}
		}

		UISprite instructor = tip.transform.FindChild("Instructor").GetComponent<UISprite>();
		if (!string.IsNullOrEmpty(step.instructor))
		{
			panelInstructor = NGUITools.AddChild<UIPanel>(UIManager.Instance.m_uiRootBasePanel.gameObject);
			panelInstructor.depth = 10002;
			panelInstructor.transform.localPosition = new Vector3(0f, 0f, -650f);
			panelInstructor.name = "GuideInstructor";
			instructor.transform.SetParent(panelInstructor.transform);
			instructor.spriteName = step.instructor;
			instructor.MakePixelPerfect();
			instructor.transform.localPosition = new Vector3(step.instructorPos.x, step.instructorPos.y);
			NGUITools.SetActive(instructor.gameObject, true);

			if (step.instructorPos.x > step.tipPos.x)
			{
				UISprite sprFrame = tip.transform.FindChild("Frame").GetComponent<UISprite>();
				sprFrame.flip = UIBasicSprite.Flip.Horizontally;
				int left = sprFrame.leftAnchor.absolute;
				int right = sprFrame.rightAnchor.absolute;
				sprFrame.leftAnchor.absolute = -right;
				sprFrame.rightAnchor.absolute = -left;
				sprFrame.ResetAnchors();
				sprFrame.UpdateAnchors();
			}
			else
				instructor.flip = UIBasicSprite.Flip.Horizontally;
		}
		else
		{
			NGUITools.SetActive(instructor.gameObject, false);
		}
	}

	void OnFinishStepClick(GameObject go)
	{
		if (curStep != null && curStep.ID != 00001)
			EndCurStep();
		else
		{
			StepClear();
			curStep = null;
			GuideModule module = GameSystem.Instance.GuideConfig.GetModule(MainPlayer.Instance.GetUncompletedGuide());
			UIManager.Instance.ShowSpecifiedUI(module.linkID, module.linkSubID);
			ReqBeginGuide(module.ID);
		}
	}

	void OnSecondButtonClick(GameObject go)
	{
		EndStep(curStep.nextStep != 0 ? GameSystem.Instance.GuideConfig.GetStep(curStep.nextStep) : curStep);
	}

	void EndCurStep()
	{
		EndStep(curStep);
	}

	//special guide
	public void BeginPractiseGuide()
	{
		toListenPractiseGuide = true;
		PractiseData practise = GameSystem.Instance.PractiseConfig.GetConfig(20001);
		practise.self_id = MainPlayer.Instance.CaptainID;
		//UIPractiseListControl.CreateMatch(practise, 0);
	}

	void OnPractiseGuideOver()
	{
		EndCurStep();
	}

	public void BeginSkillCastGuide()
	{
		toListenPractiseGuide = true;
		PractiseData practise = GameSystem.Instance.PractiseConfig.GetConfig(20002);
		uint role_id = 0;
		foreach (Player player in MainPlayer.Instance.PlayerList)
		{
			foreach (SkillSlotProto slot in player.m_roleInfo.skill_slot_info)
			{
				if (slot.skill_id == PractiseBehaviourSkillGuide.SKILL_ID)
				{
					role_id = player.m_id;
					break;
				}
			}
			if (role_id != 0)
				break;
		}
		if (role_id == 0)
			Logger.LogError("BeginSkillCastGuide, there is no role who has specific skill");
		practise.self_id = role_id;
		GameSystem.Instance.mClient.CreateMatch(practise, 0L);
	}
}
