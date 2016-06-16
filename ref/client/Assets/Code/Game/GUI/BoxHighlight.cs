using UnityEngine;
using System.Collections;

public class BoxHighlight : MonoBehaviour
{
	public float interval;
	public int countExtra;

	private GameObject frameAnim;

	void Awake()
	{
		frameAnim = transform.FindChild("Frame").gameObject;
	}

	void Start()
	{
		for (int i = 0; i < countExtra; ++i)
		{
			CreateNewFrameAnim(interval * (i + 1));
		}
	}

	private void CreateNewFrameAnim(float delay)
	{
		GameObject clone = CommonFunction.InstantiateObject(frameAnim, transform);
		clone.transform.localScale = Vector3.one;
		UITweener[] tweeners = clone.GetComponents<UITweener>();
		foreach (UITweener tweener in tweeners)
		{
			tweener.delay = delay;
			tweener.ResetToBeginning();
			tweener.PlayForward();
		}
	}
}
