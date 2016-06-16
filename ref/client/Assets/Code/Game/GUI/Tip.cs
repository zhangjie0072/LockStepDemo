using UnityEngine;

public class Tip : MonoBehaviour
{
    public void Show(string text, Transform parent)
    {
        GameObject tip = CommonFunction.InstantiateObject(gameObject, parent);
        GameObject tip_label = tip.transform.FindChild("TipLabel").gameObject;
        tip_label.GetComponent<UILabel>().text = text;
        tip_label.SetActive(true);
        tip_label.GetComponent<TweenAlpha>().ResetToBeginning();
        tip_label.GetComponent<TweenAlpha>().PlayForward();
        tip_label.GetComponent<TweenAlpha>().onFinished.Add(new EventDelegate(delegate() { NGUITools.Destroy(tip); }));
    }
}
