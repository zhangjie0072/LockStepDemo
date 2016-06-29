using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 异步加载 ScrollView 控件中的Grid的子项
/// </summary>
[RequireComponent(typeof(UIScrollView))]
public class ScrollViewAsyncLoadItem : MonoBehaviour
{
    UIGrid grid;
    UIScrollView scroll;
    Coroutine coroutine = null;

    int ItemCount = 0;
    bool m_IsStartLoad = false;
    public System.Func<int, Transform, GameObject> OnCreateItem = null;
	public int LoadCountOnce = 1;	//一次yield return 加载的个数

    void Awake()
    {
        UIGrid[] grids = GetComponentsInChildren<UIGrid>(true);
        if (grids == null || grids.Length == 0)
        {
            Debug.LogError("ScrollViewIncLoad requires UIGrid.");
        }

        scroll = GetComponent<UIScrollView>();
        grid = grids[0];
    }

    System.Collections.IEnumerator LoadItems()
    {
        while (grid.transform.childCount > 0)
        {
            NGUITools.Destroy(grid.transform.GetChild(0).gameObject);
        }
        yield return null;
		int loadCount = 0;
        for (int index = 0; index < ItemCount; index++)
        {
            if (OnCreateItem == null)
            {
                break;
            }          

            //调用回调函数中的逻辑，填充控件内容
            GameObject item = OnCreateItem(index, grid.transform);
			loadCount++;
            if (item == null)
            {
                Debug.LogError(this.transform.name + "=>ScrollViewAsyncLoadItem load error! index = " + index.ToString());
                break;
            }

            grid.Reposition();
            if (index == 0)
            {
                scroll.ResetPosition();
            }
			if(loadCount >= LoadCountOnce)
			{
				loadCount = 0;
				yield return null;
			}
            //yield return new WaitForEndOfFrame();
        }

        coroutine = null;
    }

    void LateUpdate()
    {
        if (m_IsStartLoad)
        {
            m_IsStartLoad = false;
            StartLoad();
        }
    }

    /// <summary>
    /// 开始异步加载
    /// </summary>
    void StartLoad()
    {
        if (!NGUITools.GetActive(scroll))
            return;

        if (OnCreateItem == null)
            return;

        if (grid == null)
            return;

        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(LoadItems());
    }

    /// <summary>
    /// 刷新界面内容（item总数）
    /// </summary>
    /// <param name="itemCount"></param>
    public void Refresh(int itemCount)
    {
        ItemCount = itemCount;
        m_IsStartLoad = true;
        //StartLoad(null);
    }
}