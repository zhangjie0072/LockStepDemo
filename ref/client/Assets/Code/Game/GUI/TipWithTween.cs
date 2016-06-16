using UnityEngine;
using System.Collections;

public class TipWithTween : MonoBehaviour
{
    private UILabel _title;
    private UILabel _message;

    public string message
    {
        get { return _message.text; }
        set { _message.text = value; }
    }
    void Awake()
    {
        _title = transform.FindChild("Title").GetComponent<UILabel>();
        _message = transform.FindChild("Info").GetComponent<UILabel>();
        _message.text = CommonFunction.GetConstString("STORE_BUY_GOODS_GOLD_NOT_ENOUGH");
        GetComponent<TweenAlpha>().ResetToBeginning();
        GetComponent<TweenAlpha>().PlayForward();
        GameObject.Destroy(gameObject, 2f);
    }
}
