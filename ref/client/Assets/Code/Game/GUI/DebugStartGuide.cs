using UnityEngine;

public class DebugStartGuide : MonoBehaviour
{
	public static void Show(GameObject go)
	{
		if (!GlobalConst.IS_GUIDE)
			return;
		GameObject panel = UIManager.Instance.CreateUI("Prefab/GUI/DebugStartGuide");
		NGUITools.BringForward(panel);
		UIEventListener.Get(panel.transform.FindChild("Start").gameObject).onClick += OnStartGuide;
		UIEventListener.Get(panel.transform.FindChild("Close").gameObject).onClick += OnClose;
	}

	static void OnStartGuide(GameObject go)
	{
		uint moduleID;
		if (uint.TryParse(go.transform.parent.FindChild("ModuleID").GetComponent<UILabel>().text, out moduleID))
		{
			GuideSystem.Instance.ReqBeginGuide(moduleID, true);
		}
		NGUITools.Destroy(go.transform.parent.gameObject);
	}

	static void OnClose(GameObject go)
	{
		NGUITools.Destroy(go.transform.parent.gameObject);
	}
}
