using UnityEngine;

public class GameObjectPlaceholder : MonoBehaviour
{
    public GameObject prefab;
	public GameObject spawnedObj { get; private set; }
    private bool isReplaced = false;

	public static GameObject Replace(Transform tm)
	{
		return Replace(tm.gameObject);
	}

	public static GameObject Replace(GameObject obj)
	{
		GameObjectPlaceholder placeholder = obj.GetComponent<GameObjectPlaceholder>();
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
		Replace();
	}

	void Replace()
    {
        if( !isReplaced )
        {
            GameObject obj = GameObject.Instantiate(prefab) as GameObject;
            obj.name = gameObject.name;
            obj.transform.parent = transform.parent;
            obj.transform.localPosition = transform.localPosition;
            obj.transform.localRotation = transform.localRotation;
            obj.transform.localScale = transform.localScale;
            NGUITools.Destroy(gameObject);
            NGUITools.BringForward(obj);
            spawnedObj = obj;
            isReplaced = true;
        }
    }
}
