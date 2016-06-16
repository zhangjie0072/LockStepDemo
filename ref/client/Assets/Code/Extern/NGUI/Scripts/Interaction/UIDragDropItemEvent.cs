using UnityEngine;

/// <summary>
/// UIDragDropItem控件扩展，以支持外部事件回调
/// </summary>
public class UIDragDropItemEvent : UIDragDropItem
{
    /// <summary>
    /// 事件函数
    /// </summary>
    public UIEventListener.VoidDelegate OnDragDrop;

    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();

        if (cloneOnDrag)
        {
            GameObject clone = UICamera.currentTouch.dragged;
            LuaComponent[] components = clone.transform.GetComponentsInChildren<LuaComponent>();
            foreach (var c in components)
            {
                c.enabled = false;
            }
        }
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);

        if (this.originalItem != null && this.originalItem is UIDragDropItemEvent)
        {
            UIDragDropItemEvent item = this.originalItem as UIDragDropItemEvent;
            if (item.OnDragDrop != null)
            {
                item.OnDragDrop(surface);
            }
        }
    }
}
