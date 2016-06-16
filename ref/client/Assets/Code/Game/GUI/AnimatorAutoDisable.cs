using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class AnimatorAutoDisable : MonoBehaviour
{
	Animator animator;
	bool animatorDisabledByFinish;
	bool initialEnabled;

	void Awake()
	{
		animator = GetComponent<Animator>();
		initialEnabled = animator.enabled;
	}

	void OnFinish()
	{
		if (!animator.IsInTransition(0))
		{
			StartCoroutine(DisableAnimator());
		}
	}

	IEnumerator DisableAnimator()
	{
		yield return new WaitForEndOfFrame();
		animator.enabled = false;
		animatorDisabledByFinish = true;
	}

	void OnEnable()
	{
		if (animatorDisabledByFinish && initialEnabled)
		{
			animator.enabled = true;
			animatorDisabledByFinish = false;
		}
	}
}
