using UnityEngine;
using System.Collections;

public class GuideTip : MonoBehaviour
{
	UILabel tipLabel;
	GameObject firstButton;
	UILabel firstButtonLabel;
	GameObject nextButton;
	UISprite instructorSprite;
	UIWidget frame;

	public string tip
	{
		get { return tipLabel.text; }
		set { tipLabel.text = value; }
	}
	public bool firstButtonVisible
	{
		get { return NGUITools.GetActive(firstButton); }
		set {
			NGUITools.SetActive(firstButton, value);
			//frame.bottomAnchor.absolute = value ? -96 : -59;
			//frame.ResetAnchors();
			//frame.UpdateAnchors();
		}
	}
	public string firstButtonText
	{
		get { return firstButtonLabel.text; }
		set { firstButtonLabel.text = value; }
	}
	public bool nextButtonVisible
	{
		get { return NGUITools.GetActive(nextButton); }
		set { NGUITools.SetActive(nextButton, value); }
	}
	public bool instructorVisible
	{
		get { return NGUITools.GetActive(instructorSprite.gameObject); }
		set { NGUITools.SetActive(instructorSprite.gameObject, value); }
	}
	public string instructor
	{
		set { instructorSprite.spriteName = value; instructorSprite.MakePixelPerfect(); }
	}
	public Vector3 instructorPos
	{
		set { instructorSprite.transform.localPosition = value; }
	}
	public UIEventListener.VoidDelegate onFirstButtonClick
	{
		get { return UIEventListener.Get(firstButton).onClick; }
		set { UIEventListener.Get(firstButton).onClick = value; }
	}

	void Awake()
	{
		tipLabel = transform.FindChild("Tip").GetComponent<UILabel>();
		firstButton = transform.FindChild("Button").gameObject;
		firstButtonLabel = firstButton.GetComponentInChildren<UILabel>();
		nextButton = transform.FindChild("Next").gameObject;
		instructorSprite = transform.FindChild("Instructor").GetComponent<UISprite>();
		frame = transform.FindChild("Frame").GetComponent<UIWidget>();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			if (onFirstButtonClick != null)
				onFirstButtonClick(firstButton);
		}

	}

	public void AutoHide(float delay = 0f)
	{
		StartCoroutine(HideLater(delay));
	}

	IEnumerator HideLater(float delay = 0f)
	{
		yield return new WaitForSeconds(delay);
		Hide();
	}

	public void Show()
	{
		NGUITools.SetActive(gameObject, true);
	}

	public void Hide()
	{
		NGUITools.SetActive(gameObject, false);
	}
}
