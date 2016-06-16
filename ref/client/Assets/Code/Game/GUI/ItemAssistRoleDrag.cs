using UnityEngine;
using System.Collections.Generic;

public class ItemAssistRoleDrag : UIDragDropItem
{
    public delegate void OnDrop(GameObject surface, uint roleID);
    public OnDrop onRelease;

    protected override void OnDragDropStart()
    {
        onRelease = transform.parent.FindChild("Icon").GetComponent<ItemAssistRoleDrag>().onRelease;

        base.OnDragDropStart();

        //UICamera.currentTouch.dragged.transform.FindChild("GetInfo").gameObject.SetActive(false);
        //UICamera.currentTouch.dragged.transform.FindChild("Name").gameObject.SetActive(false);
        //UICamera.currentTouch.dragged.transform.FindChild("Position").gameObject.SetActive(false);
        //UICamera.currentTouch.dragged.transform.GetComponent<UISprite>().enabled = false;
        UICamera.currentTouch.dragged.transform.FindChild("Side").gameObject.SetActive(false);
        NGUITools.BringForward(UICamera.currentTouch.dragged);

    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        string roleSelectedIDstr = transform.FindChild("ID").GetComponent<UILabel>().text;
        uint roleSelectedID = uint.Parse(roleSelectedIDstr);
        if (surface.name == "1Player" || surface.name == "2AI" || surface.name == "3AI")
        {
            if (onRelease != null)
                onRelease(surface, roleSelectedID);
        }
        else
        {
            Logger.Log("position error: no set position");
            return;
        }

    }
}
