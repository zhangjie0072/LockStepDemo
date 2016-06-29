using UnityEngine;
using System.Collections;

[System.Serializable]
public class ReceiverItem {
	public GameObject receiver;
	public string action = "OnSignal";
	public float delay;
	
	public IEnumerator SendWithDelay( MonoBehaviour sender ) 
	{
		yield return new WaitForSeconds(delay);
		if (receiver)
		{
			Debug.LogWarning("signal receiver is "+receiver.name);
			receiver.SendMessage (action);
			}
		else
			Debug.LogWarning ("No receiver of signal \""+action+"\" on object "+sender.name+" ("+sender.GetType().Name+")");
	}
}

[System.Serializable]
public class SignalSender
{
	public bool onlyOnce;
	public ReceiverItem[] receivers;
	
	private bool hasFired = false;
	
	public void SendSignals (MonoBehaviour sender) {
		if ( !hasFired || !onlyOnce) {
			foreach ( ReceiverItem receiver in receivers ) {
				sender.StartCoroutine (receiver.SendWithDelay(sender));
			}
			hasFired = true;
		}
	}
}
