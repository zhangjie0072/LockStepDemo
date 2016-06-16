using UnityEngine;
using System.Collections;

public class UnlockRadiance : MonoBehaviour
{
	UILabel label;

	public string text
	{
		set { label.text = value; }
	}

	public static void Show(string text)
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UnlockRadiance") as GameObject;
		UnlockRadiance instance = CommonFunction.InstantiateObject(prefab,
			UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<UnlockRadiance>();
		instance.text = text;
		UIManager.Instance.BringPanelForward(instance.gameObject);
	}

	void Awake()
	{
		label = transform.FindChild("Window/Box/Label").GetComponent<UILabel>();
		UIEventListener.Get(gameObject).onClick += (GameObject) => { NGUITools.Destroy(gameObject); };
	}
}
