using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIFinishEvent : MonoBehaviour
{
	public delegate void OnFinishDel();
	public OnFinishDel	onFinish;

	public void OnFinish()
	{
		if( onFinish != null )
			onFinish();
	}
}