using UnityEngine;
using System.Collections;

public class TouchGuideUp : MonoBehaviour
{
	public string fingerNormal;
	public string fingerClick;
	public string pointNormal;
	public string pointClick;

	public float angle
	{
		set
		{
			transform.localEulerAngles = new Vector3(0f, 0f, value);
			fingerNode.localEulerAngles = new Vector3(0f, 0f, -value);
		}
	}

	Transform fingerNode;
	UISprite finger;
	UISprite point;

	void Awake()
	{
		point = transform.FindChild("Point").GetComponent<UISprite>();
		fingerNode = point.transform.GetChild(0);
		finger = fingerNode.GetChild(0).GetComponent<UISprite>();
	}

	void Click()
	{
		finger.spriteName = fingerClick;
		point.spriteName = pointClick;
	}

	void Normal()
	{
		finger.spriteName = fingerNormal;
		point.spriteName = pointNormal;
	}
}
