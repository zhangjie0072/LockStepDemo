using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBall : MonoBehaviour 
{
	static Queue<ConveyorBall> ballQ = new Queue<ConveyorBall>();
	static uint currIndex;

	public uint index { get; private set; }

	public string normalBallSprite;
	public string specialBallSprite;

	static bool _fastForward;
	static public bool fastForward
	{
		get { return _fastForward; }
		set
		{
			_fastForward = value;
			foreach (ConveyorBall ball in ballQ)
				ball.GetComponent<Animator>().speed = value ? ball.fastForwardSpeed : ball.animSpeed;
		}
	}
	public float animSpeed = 1f;
	public float fastForwardSpeed = 3f;

	public delegate void Delegate(ConveyorBall ball); 
	public Delegate onReachExit;
	static public Delegate onCreateNewBall;

	void Awake()
	{
		index = currIndex++;
		Animator animator = GetComponent<Animator>();
		animator.playbackTime = 0f;
		animator.speed = fastForward ? fastForwardSpeed : animSpeed;
		ballQ.Enqueue(this);
	}

	void AddNewBall()
	{
		GameObject newball = Object.Instantiate(gameObject) as GameObject;
		newball.name = gameObject.name;
		newball.transform.parent = transform.parent;
		newball.transform.localScale = transform.localScale;

		if (onCreateNewBall != null)
			onCreateNewBall(newball.GetComponent<ConveyorBall>());
	}

	void ReachExit()
	{
		if (onReachExit != null)
			onReachExit(this);
		//Pause();
		//StartCoroutine(DestroyLater());
	}

	static public void Clear()
	{
		currIndex = 0;
		ballQ.Clear();
	}

	static public void Pause()
	{
		//Debug.Log("Conveyor pause");
		foreach (ConveyorBall ball in ballQ)
		{
			ball.GetComponent<Animator>().speed = 0f;
		}
	}

	static public void DestroyFront()
	{
		ConveyorBall ball = ballQ.Dequeue();
		NGUITools.Destroy(ball.gameObject);
	}

	IEnumerator DestroyLater()
	{
		yield return new WaitForSeconds(0f);
		DestroyFront();
		Resume();
	}

	static public void Resume()
	{
		//Debug.Log("Conveyor Resume");
		foreach (ConveyorBall ball in ballQ)
		{
			ball.GetComponent<Animator>().speed = ball.animSpeed;
		}
	}
}
