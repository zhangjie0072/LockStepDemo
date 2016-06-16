using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpeedUpButton : MonoBehaviour
{
	[System.Serializable]
	public struct SpeedLevel
	{
		public float speed;
		public string text;
	}
	public SpeedLevel[] speedLevels;

	UILabel label;
	int index = 0;
    UISprite btnSprite;

	void Awake()
	{
		label = transform.GetComponentInChildren<UILabel>();
        Transform btnTransform = transform.FindChild("Button");
        btnSprite = btnTransform.GetComponent<UISprite>();
		UIEventListener.Get(btnTransform.gameObject).onClick = OnClick;
	}

	void Start()
	{
		SetSpeed(0);
	}

	void OnDestroy()
	{
		if (GameSystem.Instance.mClient != null)
			GameSystem.Instance.mClient.timeScale = 1f;
	}

	void OnClick(GameObject go)
	{
		++index;
		if (index >= speedLevels.Length)
			index = 0;

		SetSpeed(index);
	}

	void SetSpeed(int index)
	{
		GameSystem.Instance.mClient.timeScale = speedLevels[index].speed;
		label.text = speedLevels[index].text;

        if( index == 0 )
        {
            btnSprite.flip = UIBasicSprite.Flip.Nothing;
        }
        else if( index == 1)
        {
            btnSprite.flip = UIBasicSprite.Flip.Horizontally;
        }
        else
        {
            Logger.LogError("Not Enough picture to support more speed mode!");
        }
	}

}
