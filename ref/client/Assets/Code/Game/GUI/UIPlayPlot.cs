using UnityEngine;
using System.Collections;
using fogs.proto.config;

public class UIPlayPlot : MonoBehaviour
{
	public System.Action onNext;

	UISprite[] dialogs = new UISprite[2]; 
    UILabel[] texts = new UILabel[2];
    UISprite[] icons = new UISprite[2];
    UILabel[] names = new UILabel[2];

	void Awake()
	{
		dialogs[0] = transform.FindChild("DialogShowLeft").GetComponent<UISprite>();
		dialogs[1] = transform.FindChild("DialogShowRight").GetComponent<UISprite>();
		for (int i = 0; i < 2; ++i)
		{
			texts[i] = dialogs[i].transform.FindChild("Dialog").GetComponent<UILabel>();
			icons[i] = dialogs[i].transform.FindChild("Icon/Sprite").GetComponent<UISprite>();
			names[i] = dialogs[i].transform.FindChild("Name/Label").GetComponent<UILabel>();
		}
		UIEventListener.Get(gameObject).onClick = OnNextDialogClick;
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			if (onNext != null)
				onNext();
		}
	}

	public void Show(int side, uint id, string text, float delay = 5f)
	{
		for (int i = 0; i < 2; ++i)
		{
			NGUITools.SetActive(dialogs[i].gameObject, i == side);
		}

		uint shapeID = 0;
		string icon = "";
		string name = "";
		RoleBaseData2 baseConfig = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(id);
		if (baseConfig != null)
		{
			shapeID = (uint)baseConfig.shape;
			icon = baseConfig.icon_bust;
			name = baseConfig.name;
		}
		else
		{
			NPCConfig npcConfig = GameSystem.Instance.NPCConfigData.GetConfigData(id);
			shapeID = npcConfig.shape;
            baseConfig = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(shapeID);
			icon = baseConfig.icon_bust;
			name = npcConfig.name;
		}

		string portAtlas = "Atlas/icon/iconBust";
		if (shapeID >= 1000 && shapeID < 1500)
			portAtlas = portAtlas;
		else if (shapeID < 1800)
			portAtlas += "_1";
		else if (shapeID < 2000)
			portAtlas += "_2";
		else
			Debug.Log("cannot getPortrait by id=" + shapeID);

		icons[side].atlas = ResourceLoadManager.Instance.GetAtlas(portAtlas);
		icons[side].spriteName = icon;
		icons[side].MakePixelPerfect();
		names[side].text = name;
		texts[side].text = text.Replace("%self%", MainPlayer.Instance.Name);

		NGUITools.SetActive(gameObject, true);

		StopAllCoroutines();
		if (delay > 0f)
			StartCoroutine(AutoNext(delay));
	}

	public void Hide()
	{
		NGUITools.SetActive(gameObject, false);
	}

	IEnumerator AutoNext(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (onNext != null)
			onNext();
	}

	void OnNextDialogClick(GameObject go)
	{
		if (onNext != null)
			onNext();
	}
}
