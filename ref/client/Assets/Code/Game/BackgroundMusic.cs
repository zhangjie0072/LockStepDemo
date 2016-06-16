using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour
{
	public string musicName;

	void Start()
	{
		PlaySoundManager.PlaySound(musicName);
		Object.Destroy(gameObject);
	}

}
