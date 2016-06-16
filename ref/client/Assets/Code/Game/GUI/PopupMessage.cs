using UnityEngine;

public class PopupMessage : MonoBehaviour
{
    private static PopupMessage _instance = null;

    private GameObject _mask;
    private UILabel _title;
    private GameObject _close;
    private UILabel _message;
    private GameObject _ok;
    private UILabel _ok_label;
    private GameObject _cancel;
    private UILabel _cancel_label;

    public string message
    {
        get { return _message.text; }
        set { _message.text = value; }
    }

    public UIEventListener.VoidDelegate onOKClick
    {
        set
        {
            if (value != null)
                UIEventListener.Get(_ok).onClick += value;
        }
    }

    public UIEventListener.VoidDelegate onCancelClick
    {
        set
        {
            if (value != null)
            {
                UIEventListener.Get(_cancel).onClick += value;
                UIEventListener.Get(_close).onClick += value;
            }
        }
    }

    public string okLabel
    {
        set { _ok_label.text = value; }
    }

    public string cancelLabel
    {
        set
        {
            if (value == "")
                HideCancelButton();
            _cancel_label.text = value;
        }
    }

    void Awake()
    {
        //make sure only one instance is alive
        if (_instance != null)
        {
            NGUITools.Destroy(_instance.gameObject);
        }
        _instance = this;

        _mask = transform.FindChild("Mask").gameObject;
        _title = transform.FindChild("Window/Title").GetComponent<UILabel>();
        _close = transform.FindChild("Window/Close").gameObject;
        _message = transform.FindChild("Window/Message").GetComponent<UILabel>();
        _ok = transform.FindChild("Window/OK").gameObject;
        _ok_label = _ok.transform.FindChild("Text").GetComponent<UILabel>();
        _cancel = transform.FindChild("Window/Cancel").gameObject;
        _cancel_label = _cancel.transform.FindChild("Text").GetComponent<UILabel>();
        UIEventListener.Get(_ok).onClick += OnClose;
        UIEventListener.Get(_cancel).onClick += OnClose;
        UIEventListener.Get(_close).onClick += OnClose;
    }

    private void OnClose(GameObject go)
    {
        _mask.SetActive(false);
        UITweener[] tweeners = transform.GetComponentsInChildren<UITweener>();
        foreach (UITweener tweener in tweeners)
        {
            if (tweener.tweenGroup == 2)
            {
                tweener.onFinished.Add(new EventDelegate(OnTweenFinished));
                tweener.PlayForward();
                break;
            }
        }
    }

    private void OnTweenFinished()
    {
        NGUITools.Destroy(gameObject);
    }

    private void HideCancelButton()
    {
        _cancel.SetActive(false);
        _close.SetActive(false);
        //position the OK button to the center
        UIWidget widget = _ok.GetComponent<UIWidget>();
        widget.leftAnchor.target = null;
        widget.rightAnchor.target = null;
        Vector3 position = _ok.transform.localPosition;
        position.x = 0;
        _ok.transform.localPosition = position;
    }
}
