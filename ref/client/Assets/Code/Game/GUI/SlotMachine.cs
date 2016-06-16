using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour 
{
	public GameObject itemPrefab;
	public int itemCount = 3;
	public float itemDuration = 1f;
	public float minSpeed = 0.5f;
	public float maxSpeed = 1.2f;
	[HideInInspector]
	public GameObject selectedItem;
	public int preselectedItemIndex = -1;
	private GameObject preselectedItem;

	public delegate void OnCreateItemDelegate(GameObject item, int index);
	public OnCreateItemDelegate onCreateItem;

	public delegate void OnEndDelegate(GameObject selectedItem);
	public OnEndDelegate onEnd;

	public UIEventListener.VoidDelegate onHandleClick
	{
		get { return UIEventListener.Get(handleAnim.gameObject).onClick; }
		set { UIEventListener.Get(handleAnim.gameObject).onClick = value; }
	}

	UITweener handleAnim;
	UIPanel scrollArea;
	TweenPosition[] tweeners;
	Queue<GameObject> visibleQ = new Queue<GameObject>();
	Queue<GameObject> invisibleQ = new Queue<GameObject>();
	GameObject lastItem;
	int scrollAreaHeight;
	int itemHeight;
	int visibleItemCount;
	Vector3 nextPosition;
	bool appendOnNextFrame;
	bool inAligning;
	bool initialized;
	bool autoBegin;

	public void Begin()
	{
		if (!initialized)
		{
			autoBegin = true;
			return;
		}

		foreach (GameObject item in visibleQ)
		{
			TweenPosition tween = item.GetComponent<TweenPosition>();
			tween.enabled = true;
			tween.PlayForward();
		}
		GetComponent<Animator>().enabled = true;
		GetComponent<Animator>().speed = Random.Range(minSpeed, maxSpeed);

		selectedItem = null;
		if (!handleAnim.enabled)
			OnHandleClick(handleAnim.gameObject);
		handleAnim.GetComponent<BoxCollider>().enabled = false;
	}

	public void End()
	{
		GetComponent<Animator>().enabled = false;

		if (preselectedItem != null)
			selectedItem = preselectedItem;
		else
		{
			foreach (GameObject item in visibleQ)
			{
				if (Mathf.Abs(item.transform.localPosition.y) <= itemHeight / 2)
				{
					selectedItem = item;
					break;
				}
			}
		}

		//align
		if (NGUITools.GetActive(selectedItem) &&
			selectedItem.transform.localPosition.y < 0f &&
			selectedItem.transform.localPosition.y > -itemHeight / 2 )
		{
			foreach (GameObject item in visibleQ)
			{
				TweenPosition tween = item.GetComponent<TweenPosition>();
				tween.PlayReverse();
			}
		}
		inAligning = true;
	}

	void Awake()
	{
		handleAnim = transform.FindChild("Handle").GetComponent<UITweener>();
		scrollArea = transform.FindChild("ScrollArea").GetComponent<UIPanel>();

		onHandleClick += OnHandleClick;
	}

	void Start () 
	{
		////code for testing
		//Begin();
		//onCreateItem += OnCreateItem;
		//onHandleClick += OnHandleClickTest;
		//onEnd += OnEnd;
		////----------------

		//create items
		for (int i = 0; i < itemCount; ++i)
		{
			GameObject item = CommonFunction.InstantiateObject(itemPrefab, scrollArea.transform);
			item.SetActive(false);
			invisibleQ.Enqueue(item);
			if (i == preselectedItemIndex)
				preselectedItem = item;

			if (onCreateItem != null)
				onCreateItem(item, i);
		}
		
		scrollAreaHeight = (int)scrollArea.clipRange.w;
		itemHeight = invisibleQ.Peek().GetComponent<UIWidget>().height;
		visibleItemCount = Mathf.CeilToInt((float)scrollAreaHeight / itemHeight) + 1;
		if (visibleItemCount > itemCount)
			Logger.LogError("Visible item count: " + visibleItemCount + " item count: " + itemCount + 
				", enlarge item or shrink scroll area.");

		//add tween to item
		tweeners = new TweenPosition[itemCount];
		for (int i = 0; i < itemCount; ++i)
		{
			GameObject item = invisibleQ.Dequeue();
			TweenPosition tween = item.AddComponent<TweenPosition>();
			tween.from.y = itemHeight * visibleItemCount / 2;
			tween.to.y = -tween.from.y;
			tween.method = UITweener.Method.Linear;
			tween.enabled = false;
			tween.duration = itemDuration;
			tween.AddOnFinished(() => { OnItemTweenFinished(item); });
			tweeners[i] = tween;
			invisibleQ.Enqueue(item);
		}

		//place visible item
		int y = -scrollAreaHeight / 2 + (scrollAreaHeight / 2 - itemHeight / 2) % itemHeight - itemHeight / 2;
		while(true)
		{
			GameObject item = invisibleQ.Dequeue();
			lastItem = item;
			item.SetActive(true);
			item.transform.localPosition = new Vector3(0f, y, 0f);
			TweenPosition tween = item.GetComponent<TweenPosition>();
			tween.tweenFactor = (tween.from.y - y) / (tween.from.y - tween.to.y);
			tween.Sample(tween.tweenFactor, false);
			visibleQ.Enqueue(item);
			if ((y + itemHeight / 2) >= scrollAreaHeight / 2)
				break;
			y += itemHeight;
		}

		initialized = true;
		if (autoBegin)
			Begin();
	}
	
	void Update () 
	{
		if (appendOnNextFrame)
		{
			Append();
			appendOnNextFrame = false;
		}
		ModifyTweenDuration();
	}

	void LateUpdate()
	{
		if (lastItem.transform.localPosition.y <= scrollAreaHeight / 2 - itemHeight / 2)
		{
			nextPosition = lastItem.transform.localPosition;
			nextPosition.y += itemHeight;
			appendOnNextFrame = true;
		}

		if (inAligning)
		{
			if (!NGUITools.GetActive(selectedItem))
				return;
			float y = selectedItem.transform.localPosition.y;
			bool isForward = visibleQ.Peek().GetComponent<TweenPosition>().amountPerDelta > 0;
			if (isForward && y < 0 && y > -itemHeight / 2)
				inAligning = false;
			else if (!isForward && y > 0 && y < itemHeight / 2)
				inAligning = false;

			if (!inAligning)
			{
				//stop move
				foreach (GameObject item in visibleQ)
				{
					Vector3 position = item.transform.localPosition;
					position.y -= y;
					item.transform.localPosition = position;
					TweenPosition tween = item.GetComponent<TweenPosition>();
					tween.enabled = false;
				}

				handleAnim.GetComponent<BoxCollider>().enabled = true;

				if (onEnd != null)
					onEnd(selectedItem);
			}
		}
	}

	void Append()
	{
		GameObject item = invisibleQ.Dequeue();
		lastItem = item;
		item.SetActive(true);
		item.transform.localPosition = nextPosition;
		TweenPosition tween = item.GetComponent<TweenPosition>();
		tween.enabled = true;
		tween.tweenFactor = (tween.from.y - nextPosition.y) / (tween.from.y - tween.to.y);
		tween.Sample(tween.tweenFactor, false);
		tween.PlayForward();
		visibleQ.Enqueue(item);
	}

	void ModifyTweenDuration()
	{
		foreach (TweenPosition tween in tweeners)
			tween.duration = itemDuration;
	}

	void OnItemTweenFinished(GameObject item)
	{
		if (item != visibleQ.Peek())
			Logger.LogError("It's impossible.");
		visibleQ.Dequeue();
		item.SetActive(false);
		item.GetComponent<TweenPosition>().enabled = false;
		invisibleQ.Enqueue(item);
	}

	void OnHandleClick(GameObject go)
	{
		handleAnim.enabled = true;
		handleAnim.ResetToBeginning();
		handleAnim.PlayForward();
	}

	//code for testing
	void OnCreateItem(GameObject item, int index)
	{
		item.transform.FindChild("Name").GetComponent<UILabel>().text = "Item" + index;
	}

	void OnHandleClickTest(GameObject go)
	{
		Begin();
	}

	void OnEnd(GameObject item)
	{
		Logger.Log("Selected item: " + item.transform.FindChild("Name").GetComponent<UILabel>().text);
	}
}
