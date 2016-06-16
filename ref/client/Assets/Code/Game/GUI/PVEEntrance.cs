using UnityEngine;
using System.Collections;

public class PVEEntrance : MonoBehaviour
{
	GameObject close;
	GameObject career;
	GameObject tour;

	public UIEventListener.VoidDelegate onClose
	{
		get { return UIEventListener.Get(close).onClick; }
		set { UIEventListener.Get(close).onClick = value; }
	}

	public UIEventListener.VoidDelegate onCareer
	{
		get { return UIEventListener.Get(career).onClick; }
		set { UIEventListener.Get(career).onClick = value; }
	}

	public UIEventListener.VoidDelegate onTour
	{
		get { return UIEventListener.Get(tour).onClick; }
		set { UIEventListener.Get(tour).onClick = value; }
	}

	void Awake()
	{
		close = transform.FindChild("Close").gameObject;
		career = transform.FindChild("Career").gameObject;
		tour = transform.FindChild("Tour").gameObject;
	}
}
