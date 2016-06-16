using UnityEngine;

[RequireComponent(typeof(UIPanel))]
public class UIManagedPanel : MonoBehaviour
{
	void Awake()
	{
		UIManager.Instance.RegisterPanel(GetComponent<UIPanel>());
	}

	void OnDestroy()
	{
		if (UIManager.Instance != null)
			UIManager.Instance.UnregisterPanel(GetComponent<UIPanel>());
	}
}
