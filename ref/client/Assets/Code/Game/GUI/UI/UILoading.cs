using UnityEngine;
using System.Collections;

public class UILoading
{
    public static string UIName { get { return typeof(UILoading).Name; } }

    public UIProgressBar _loadingBar;
    public UILabel _barValue;
    public UILabel _tip1;
    public UILabel _tip2;

    public Animator _animator;
    public UITexture _bg;

    public void Initialize(GameObject root)
    {
        _loadingBar = root.transform.FindChild("Process").GetComponent<UIProgressBar>();

        _barValue = root.transform.FindChild("Num").GetComponent<UILabel>();
        _tip1 = root.transform.FindChild("Tip1").GetComponent<UILabel>();
        _tip2 = root.transform.FindChild("Tip2").GetComponent<UILabel>();

        _animator = root.transform.GetComponent<Animator>();
        _bg = root.transform.FindChild("Bg").GetComponent<UITexture>();
    }

    public void Uninitialize()
    {
        //TODO:
    }
}