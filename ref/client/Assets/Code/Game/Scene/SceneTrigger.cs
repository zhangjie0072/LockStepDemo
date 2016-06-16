using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneTrigger : MonoBehaviour
{
	public bool oneShot;
	private bool triggered;
	private bool destroyed;

    public delegate bool TriggerCallBack(GameObject source, Collider collider);
    public TriggerCallBack onTrigger;

    void OnTriggerEnter(Collider collider)
    {
		if (onTrigger != null && (!oneShot || !triggered) && !destroyed)
		{
			if (onTrigger(gameObject, collider))
				destroyed = true;
			triggered = true;
		}
    }

	void Update()
	{
		if (destroyed)
			Object.Destroy(gameObject);
	}
}
