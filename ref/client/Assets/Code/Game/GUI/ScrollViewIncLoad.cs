using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(UIScrollView))]
public class ScrollViewIncLoad : MonoBehaviour
{
	public int maxLineNum = 30;
	public int preloadLineNum = 3;

	public System.Func<int, Transform, GameObject> onAcquireItem;	//Param: index, parent Return: item object
	public System.Action<int, GameObject> onDestroyItem;	//Param: index, item object

	UIScrollView scroll;
	UIGrid grid;

	List<GameObject> items = new List<GameObject>();
    GameObject _holdItem = null;
    public GameObject holdItem
    {
        get
        {
            return _holdItem;
        }
        set
        {
            if( _holdItem != null && !_holdItem.activeSelf )
            {
                NGUITools.Destroy(_holdItem);
            }
            _holdItem = value;
        }
    }

	bool isHorizontal;
	int startIndex = 0;
	int endIndex = 0;
	int curItemNum { get { return endIndex - startIndex; } }
	int maxPerLine;

	void Awake()
	{
		scroll = GetComponent<UIScrollView>();
		UIGrid[] grids = GetComponentsInChildren<UIGrid>(true);
		if (grids == null || grids.Length == 0)
			Debug.LogError("ScrollViewIncLoad requires UIGrid.");
		grid = grids[0];
		maxPerLine = grid.maxPerLine;
		if (maxPerLine == 0)
			maxPerLine = 1;
		isHorizontal = ((grid.arrangement == UIGrid.Arrangement.Horizontal) == (maxPerLine == 1));

	}

	void OnEnable()
	{
		scroll.onDragFinished += OnDragFinished;
	}

	void OnDisable()
	{
		scroll.onDragFinished -= OnDragFinished;
	}

	public void Refresh()
	{
		startIndex = 0;
		endIndex = 0;
		CommonFunction.ClearGridChild(grid.transform);
		Vector4 finalClipRegion = scroll.panel.finalClipRegion;
		LoadItems(true, isHorizontal ? finalClipRegion.z : finalClipRegion.w, isHorizontal ? grid.cellWidth : grid.cellHeight);
		grid.Reposition();
		scroll.ResetPosition();
	}

	void OnDragFinished()
	{
		float offset = GetConstrainOffset();
		if (offset < -2.0f)		// Load next
		{
			LoadItems(true, -offset, isHorizontal ? grid.cellWidth : grid.cellHeight);
			DestroyOverflow(true);
		}
		else if (offset > 2.0f)	// Load prev
		{
			LoadItems(false, offset, isHorizontal ? grid.cellWidth : grid.cellHeight);
			DestroyOverflow(false);
		}
		if (Mathf.Abs(offset) > 2.0f)	// New item loaded
		{
			grid.Reposition();
			scroll.InvalidateBounds();
			float offsetAfter = GetConstrainOffset();
			//Debug.Log("ScrollViewIncLoad, offset after: " + offsetAfter);
			if (Mathf.Abs(offsetAfter) < 2.0f)	// No need to bounce back
			{
				scroll.DisableSpring();
				scroll.currentMomentum = Vector3.zero;
			}
		}
		//Debug.Log("ScrollViewIncLoad, grid child count: " + grid.transform.childCount);
	}

	float GetConstrainOffset()
	{
		Vector3 constrainOffset = scroll.panel.CalculateConstrainOffset(scroll.bounds.min, scroll.bounds.max);
		return isHorizontal ? constrainOffset.x : constrainOffset.y;
	}

	void LoadItems(bool isLoadNext, float totalSpace, float itemSize)
	{
		int loadLineNum = Mathf.CeilToInt(totalSpace / itemSize);
		loadLineNum += preloadLineNum;
		if (loadLineNum > maxLineNum)
			Debug.LogError("ScrollViewIncLoad, num of line to load is larger than max line.");
		Debug.Log("ScrollViewIncLoad, load line num: " + loadLineNum + " totalSpace:" + totalSpace + " itemSize:" + itemSize);
		int loadNum = loadLineNum * maxPerLine;
		if (onAcquireItem != null)
		{
			if (isLoadNext)
			{
				int newEndIndex = endIndex + loadNum;
				while (newEndIndex > items.Count)
					items.Add(null);
				for (; endIndex < newEndIndex; ++endIndex)
				{
					//Debug.Log("ScrollViewIncLoad, acquire next index: " + endIndex);
					GameObject item = onAcquireItem(endIndex, grid.transform);
					if (item == null)
						break;
					items[endIndex] = item;
				}
			}
			else
			{
				int newStartIndex = startIndex - loadNum;
				newStartIndex = Mathf.Max(newStartIndex, 0);
				int realLoadLineNum = (startIndex - newStartIndex) / maxPerLine;
				for (; startIndex > newStartIndex; --startIndex)
				{
					//Debug.Log("ScrollViewIncLoad, acquire prev index: " + (startIndex - 1));
					GameObject item = onAcquireItem(startIndex - 1, grid.transform);
					if (item == null)
						break;
					items[startIndex - 1] = item;
				}
				scroll.MoveRelative(isHorizontal ?
					new Vector3(realLoadLineNum * grid.cellWidth, 0, 0) :
					new Vector3(0, realLoadLineNum * grid.cellHeight, 0));
			}
		}
	}

	void DestroyOverflow(bool isDestroyFront)
	{
		if (isDestroyFront)
		{
			int destroyNum = (Mathf.CeilToInt((float)curItemNum / maxPerLine) - maxLineNum) * maxPerLine;
			Debug.Log("ScrollViewIncLoad, destroy front num: " + destroyNum);
			if (destroyNum > 0)
			{
				int newStartIndex = startIndex + destroyNum;
				for (; startIndex < newStartIndex; ++startIndex)
				{
					//Debug.Log("ScrollViewIncLoad, destroy front index: " + startIndex);
					GameObject itemToDestroy = items[startIndex];
					if (onDestroyItem != null)
						onDestroyItem(startIndex, itemToDestroy);
                    if( itemToDestroy != holdItem )
                    {
					    Object.DestroyImmediate(itemToDestroy);
                    }
                    else
                    {
                        itemToDestroy.SetActive(false);
                        itemToDestroy.transform.parent = UIManager.Instance.m_uiRoot.transform;
                    }
					items[startIndex] = null;
				}
				int destroyLineNum = Mathf.FloorToInt((float)destroyNum / maxPerLine);
				scroll.MoveRelative(isHorizontal ?
					new Vector3(-destroyLineNum * grid.cellWidth, 0, 0) :
					new Vector3(0, -destroyLineNum * grid.cellHeight, 0));
				//Debug.Log("ScrollViewIncLoad, scroll pos: " + scroll.transform.localPosition);
			}
		}
		else
		{
			int destroyNum = curItemNum - maxLineNum * maxPerLine;
			Debug.Log("ScrollViewIncLoad, destroy end num: " + destroyNum);
			if (destroyNum > 0)
			{
				int newEndIndex = endIndex - destroyNum;
				newEndIndex = Mathf.Max(newEndIndex, 0);
				for (; endIndex > newEndIndex; --endIndex)
				{
					//Debug.Log("ScrollViewIncLoad, destroy end index: " + (endIndex - 1));
					GameObject itemToDestroy = items[endIndex - 1];
					if (onDestroyItem != null)
						onDestroyItem(endIndex - 1, itemToDestroy);
                    if( itemToDestroy != holdItem )
                    {
					    Object.DestroyImmediate(items[endIndex - 1]);
                    }
                    else
                    {
                        itemToDestroy.SetActive(false);
                        itemToDestroy.transform.parent = UIManager.Instance.m_uiRoot.transform;
                    }
					items[endIndex - 1] = null;
				}
			}
		}
	}
}
