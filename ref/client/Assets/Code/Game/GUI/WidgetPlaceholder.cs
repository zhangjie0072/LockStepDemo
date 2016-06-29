using UnityEngine;

public class WidgetPlaceholder : MonoBehaviour
{
    public GameObject prefab;
	public GameObject spawnedObj { get; private set; }

	public static GameObject Replace(Transform tm)
	{
		return Replace(tm.gameObject);
	}

	public static GameObject Replace(GameObject obj)
	{
		WidgetPlaceholder placeholder = obj.GetComponent<WidgetPlaceholder>();
		if (placeholder != null)
		{
			bool isActive = NGUITools.GetActive(obj);
			if (isActive)
				placeholder.Replace();
			else
				NGUITools.SetActive(obj, true);
			NGUITools.SetActive(placeholder.spawnedObj, isActive);
			return placeholder.spawnedObj;
		}
		return obj;
	}

	void Awake()
	{
		if (prefab != null)
			Replace();
	}

	void Replace()
	{
        UIWidget widget = GetComponentInChildren<UIWidget>();
        if (widget == null)
        {
            Debug.LogError("Widget placeholder can not be used without widget.");
        }
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        UIWidget new_widget = obj.GetComponentInChildren<UIWidget>();
        if (new_widget == null)
        {
            Debug.LogError("The prefab isn't a widget.");
        }
        obj.name = gameObject.name;
        obj.transform.parent = transform.parent;
        obj.transform.localPosition = transform.localPosition;
        obj.transform.localRotation = transform.localRotation;
        obj.transform.localScale = transform.localScale;
        new_widget.alpha = widget.alpha;
		Bounds bounds = widget.CalculateBounds(transform.parent);
		new_widget.SetRect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);
        new_widget.width = widget.width;
        new_widget.height = widget.height;
        new_widget.aspectRatio = widget.aspectRatio;
        new_widget.keepAspectRatio = widget.keepAspectRatio;
        new_widget.leftAnchor = widget.leftAnchor;
        new_widget.rightAnchor = widget.rightAnchor;
        new_widget.topAnchor = widget.topAnchor;
        new_widget.bottomAnchor = widget.bottomAnchor;
		new_widget.ResetAnchors();
        new_widget.UpdateAnchors();
		NGUITools.AdjustDepth(new_widget.gameObject, widget.depth - new_widget.depth);
		UIPanel panel = GetComponent<UIPanel>();
		if (panel != null)
		{
			UIPanel new_panel = obj.GetComponent<UIPanel>();
			NGUITools.AdjustDepth(new_panel.gameObject, panel.depth - new_panel.depth);
		}
		NGUITools.Destroy(gameObject);
		gameObject.SetActive(false);	//TODO: Sometimes, it will not be destroyed.
		spawnedObj = obj;
    }
}
