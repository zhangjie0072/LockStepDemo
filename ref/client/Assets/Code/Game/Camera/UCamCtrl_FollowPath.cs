using UnityEngine;
using System.Collections.Generic;

public class UCamCtrl_FollowPath : MonoBehaviour 
{
	public enum PathType
	{
		eWinPath,
		eLosePath
	}

	public interface Listener
	{
		void OnComplete(PathType type);
	}

	public iTweenPath 		_path;
	public float 			time = 10.0f;
	public float 			delay = 0;
	public Transform		lookAt;
	public string			onComplete = "OnComplete";
	public float			lookTime = 0.5f;  
	public iTween.EaseType 	easeType = iTween.EaseType.linear;
	public iTween.LoopType	loopType = iTween.LoopType.none;
	
	public float 			debugRadius = 1.0f;

	private List<Listener>	listeners = new List<Listener>();

	// Use this for initialization
	public void Move() 
	{
		if( _path == null )
			return;

		iTween.Reset();
		//apply base position
		List<Vector3> tmp_nodes = new List<Vector3>(_path.nodes);
		for( int iNode = 0; iNode != tmp_nodes.Count; ++iNode )
			tmp_nodes[iNode] = _path.nodes[iNode] + _path.transform.position;
		gameObject.transform.position = tmp_nodes[0];
		if( lookAt != null )
			iTween.MoveTo(gameObject, iTween.Hash("path", tmp_nodes.ToArray(), "movetopath", true, "time", time, "looktime", lookTime, "looktarget", lookAt,"easeType", easeType.ToString(), "loopType", loopType.ToString(), "delay", delay, "oncomplete",onComplete, "oncompletetarget", gameObject) );
		else
			iTween.MoveTo(gameObject, iTween.Hash("path", tmp_nodes.ToArray(), "movetopath", true, "time", time, "orienttopath", true ,"easeType", easeType.ToString(), "loopType", loopType.ToString(), "delay", delay, "oncomplete",onComplete, "oncompletetarget", gameObject) );
	}

	public void AddListeners(Listener lsn)
	{
		if( listeners.Contains(lsn) )
			return;
		listeners.Add(lsn);
	}

	public void RemoveAllListeners()
	{
		listeners.Clear();
	}

	public void Stop()
	{
		if( _path == null )
			return;
		iTween.Stop();
	}

	void NotifyAllListeners(PathType type)
	{
		foreach(Listener lsn in listeners )
			lsn.OnComplete(type);
	}

	void OnDisable()
	{
		if( _path != null )
			_path.enabled = false;
		iTween tween = gameObject.GetComponent<iTween>();
		if( tween != null )
			tween.enabled = false;
	}

	void OnComplete()
	{
		iTween tween = gameObject.GetComponent<iTween>();
		if( tween != null )
			tween.enabled = false;
	}

	void OnWinCamMoveComplete()
	{
		iTween tween = gameObject.GetComponent<iTween>();
		if( tween != null )
			tween.enabled = false;

		NotifyAllListeners(PathType.eWinPath);
	}

	void OnLoseCamMoveComplete()
	{
		iTween tween = gameObject.GetComponent<iTween>();
		if( tween != null )
			tween.enabled = false;

		NotifyAllListeners(PathType.eLosePath);
	}
}
