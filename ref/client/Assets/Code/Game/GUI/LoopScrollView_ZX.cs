using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>  
/// Loop循环ScrollView  
/// 使用的时候挂载在ScrollView下面的UIGrid上就可以了  
/// 然后UIGrid下面放置部分Item即可实现循环Item  
/// </summary>  
[RequireComponent(typeof(UIGrid))]
public class LoopScrollView_ZX : MonoBehaviour
{
    #region Members

    /// <summary>  
    /// 存储物件列表  
    /// </summary>  
    private List<UIWidget> m_itemList = new List<UIWidget>();

    private Vector4 m_posParam;
    private Transform m_cachedTransform;

    /// <summary>  
    /// 起始下标  
    /// </summary>  
    private int m_startIndex;
    /// <summary>  
    /// 最大长度  
    /// </summary>  
    private int m_MaxCount;

    /// <summary>  
    /// 物件刷新代理事件  
    /// </summary>  
    /// <param name="go"></param>  
    public delegate void OnItemChange(GameObject go);
    private OnItemChange m_pItemChangeCallBack;

    /// <summary>  
    /// item点击代理事件  
    /// </summary>  
    /// <param name="go"></param>  
    /// <param name="i"></param>  
    public delegate void OnClickItem(GameObject go, int i);
    private OnClickItem m_pOnClickItemCallBack;

    /// <summary>  
    /// 父ScrollView  
    /// </summary>  
    public UIScrollView m_scrollView;

    #endregion

    void Awake()
    {
        m_cachedTransform = this.transform;
        m_scrollView = m_cachedTransform.parent.GetComponent<UIScrollView>();

        // 设置Cull  
        m_scrollView.GetComponent<UIPanel>().cullWhileDragging = true;

        UIGrid _grid = this.GetComponent<UIGrid>();
        float _cellWidth = _grid.cellWidth;
        float _cellHeight = _grid.cellHeight;
        m_posParam = new Vector4(_cellWidth,
                                                    _cellHeight,
                                                    _grid.arrangement == UIGrid.Arrangement.Horizontal ? 1 : 0,
                                                    _grid.arrangement == UIGrid.Arrangement.Vertical ? 1 : 0);
    }

    /// <summary>  
    /// 初始化表  
    /// </summary>  
    public void Init(bool _clickItem)
    {
        m_itemList.Clear();

        for (int i = 0; i < m_cachedTransform.childCount; ++i)
        {
            Transform _t = m_cachedTransform.GetChild(i);
            UIWidget uiW = _t.GetComponent<UIWidget>();
            if (uiW == null)
            {
                uiW = _t.gameObject.AddComponent<UIWidget>();
            }
            uiW.name = m_itemList.Count.ToString();
            m_itemList.Add(uiW);

            // 允许响应点击事件  
            if (_clickItem)
            {
                // 碰撞  
                BoxCollider _c = _t.GetComponent<BoxCollider>();
                if (_c == null)
                {
                    _c = _t.gameObject.AddComponent<BoxCollider>();
                    uiW.autoResizeBoxCollider = true;
                }

                // 事件接收  
                UIEventListener _listener = _t.GetComponent<UIEventListener>();
                if (_listener == null)
                {
                    _listener = _t.gameObject.AddComponent<UIEventListener>();
                }

                // 点击回调  
                _listener.onClick = OnClickListItem;
            }
        }
    }

    /// <summary>  
    /// 更新表单  
    /// </summary>  
    /// <param name="_count">数量</param>  
    public void UpdateListItem(int _count)
    {
        m_startIndex = 0;
        m_MaxCount = _count;

        for (int i = 0; i < m_itemList.Count; i++)
        {
            UIWidget _item = m_itemList[i];
            _item.name = i.ToString();
            _item.Invalidate(true);

            NGUITools.SetActive(_item.gameObject, i < _count);
        }
    }

    public List<UIWidget> GetItemList()
    {
        return m_itemList;
    }

    public List<T> GetItemList<T>() where T : Component
    {
        List<T> _list = new List<T>();

        foreach (UIWidget _wd in m_itemList)
        {
            _list.Add(_wd.GetComponent<T>());
        }

        return _list;
    }

    public List<T> GetItemListInChildren<T>() where T : Component
    {
        List<T> _list = new List<T>();

        foreach (UIWidget _wd in m_itemList)
        {
            _list.Add(_wd.GetComponentInChildren<T>());
        }

        return _list;
    }

    void LateUpdate()
    {
        if (m_itemList.Count <= 1)
        {
            return;
        }

        int _sourceIndex = -1;
        int _targetIndex = -1;
        int _sign = 0;

        bool firstVislable = m_itemList[0].isVisible;
        bool lastVisiable = m_itemList[m_itemList.Count - 1].isVisible;

        // 如果都显示,那么返回  
        if (firstVislable == lastVisiable)
        {
            return;
        }

        // 得到需要替换的源和目标  
        if (firstVislable)
        {
            _sourceIndex = m_itemList.Count - 1;
            _targetIndex = 0;
            _sign = 1;
        }
        else if (lastVisiable)
        {
            _sourceIndex = 0;
            _targetIndex = m_itemList.Count - 1;
            _sign = -1;
        }

        // 如果小于真正的初始索引或大于真正的结束索引,返回  
        int realSourceIndex = int.Parse(m_itemList[_sourceIndex].gameObject.name);
        int realTargetIndex = int.Parse(m_itemList[_targetIndex].gameObject.name);

        if (realTargetIndex <= m_startIndex ||
            realTargetIndex >= (m_MaxCount - 1))
        {
            m_scrollView.restrictWithinPanel = true;
            return;
        }

        m_scrollView.restrictWithinPanel = false;
        UIWidget movedWidget = m_itemList[_sourceIndex];

        Vector3 _offset = new Vector2(_sign * m_posParam.x * m_posParam.z,
                                                       _sign * m_posParam.y * m_posParam.w);

        movedWidget.cachedTransform.localPosition = m_itemList[_targetIndex].cachedTransform.localPosition + _offset;

        m_itemList.RemoveAt(_sourceIndex);
        m_itemList.Insert(_targetIndex, movedWidget);
        movedWidget.name = (realSourceIndex > realTargetIndex ? (realTargetIndex - 1) : (realTargetIndex + 1)).ToString();

        OnItemChangeMsg(movedWidget.gameObject);
    }

    /// <summary>  
    /// 设置代理  
    /// </summary>  
    /// <param name="_onItemChange"></param>  
    /// <param name="_onClickItem"></param>  
    public void SetDelegate(OnItemChange _onItemChange,
                                         OnClickItem _onClickItem)
    {
        m_pItemChangeCallBack = _onItemChange;

        if (_onClickItem != null)
        {
            m_pOnClickItemCallBack = _onClickItem;
        }
    }

    void OnItemChangeMsg(GameObject go)
    {
        if (m_pItemChangeCallBack != null)
        {
            m_pItemChangeCallBack(go);
        }
    }

    void OnClickListItem(GameObject go)
    {
        int _i = int.Parse(go.name);

        if (m_pOnClickItemCallBack != null)
        {
            m_pOnClickItemCallBack(go, _i);
        }
    }
}