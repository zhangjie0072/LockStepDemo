using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class HintManager : MonoSingleton<HintManager>
{
    const byte MaxHintCount = 5;
    private Queue<string> mHintStr;
    private bool _currentShowHint;
    public bool _isLoad;
    private float _maxLoadPrice;
    private LoadState mLoadState = LoadState.None;
    private float _currentLoadPrice;
    private List<HintData> mHintData;
    private float MaxChangeTime = 5.0F;
    private float _CurrentTime = 0.0F;

    public void Initialize()
    {
        mHintStr = new Queue<string>();
        mHintData = new List<HintData>();
        GetLoadHintData();
        _currentShowHint = false;
        //UISystem.Instance.LoadPerfab(HintUI.UIName, Vector3.zero);
        //UISystem.Instance.HintUI.Initialize();
    }

    public void GetLoadHintData()
    {
        //mHintData.Clear();
        ////读取以及处理XML文本的类
        //XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TIPS);
        ////解析xml的过程
        //HintData data;
        //data.id = 0;
        //data.info = "";
        //XmlNodeList nodelist = doc.SelectSingleNode("Data").ChildNodes;
        //foreach (XmlElement xe in nodelist)
        //{
        //    foreach (XmlElement xel in xe)
        //    {
        //        if (xel.Name == "tips_id")
        //        {
        //            data.id = ushort.Parse(xel.InnerText);
        //        }
        //        if (xel.Name == "tips_info")
        //        {
        //            data.info = xel.InnerText;
        //        }
        //    }
        //    mHintData.Add(data);
        //}
    }

    public void Update()
    {
        if (_currentShowHint)
        {
            CreateHintItem();
        }

        if (_isLoad)
        {
            Loading();
            if (Time.time - _CurrentTime > MaxChangeTime)
            {
                SetLoadHintLbInfo();
            }

        }
    }

    public void AddHintStr(string str)
    {
        if (string.IsNullOrEmpty(str))
            return;
        mHintStr.Enqueue(str);
        if (!_currentShowHint)
            _currentShowHint = !_currentShowHint;
    }

    public void CreateHintItem()
    {
        //if (mHintStr.Count > 0)
        //    UISystem.Instance.HintUI.CreateHintItem(mHintStr.Dequeue());
        //else
        //    _currentShowHint = false;
    }

    public void Loading()
    {
        switch (mLoadState)
        {
            case LoadState.LoadStart:
                {
                    LoadStart();
                    break;
                }
            case LoadState.LoadFinish:
                {
                    LoadFinish();
                    break;
                }
        }
    }

    public void OpenLoad()
    {
        //_currentLoadPrice = 0.0F;
        //_maxLoadPrice = UnityEngine.Random.Range(0.7F, 0.9F);
        //mLoadState = LoadState.LoadStart;
        //UISystem.Instance.HintUI.OpenLoadBG();
        //SetLoadHintLbInfo();
        //_isLoad = true;
    }

    public void CloseLoad()
    {
        mLoadState = LoadState.LoadFinish;
    }

    public void SetLoadHintLbInfo()
    {
        //_CurrentTime = Time.time;
        //if (mHintData.Count > 0)
        //{
        //    int indx = UnityEngine.Random.Range(0, mHintData.Count);
        //    UISystem.Instance.HintUI.ChangeLoadHint(mHintData[indx].info);
        //}
    }

    public void LoadFinish()
    {
        //_currentLoadPrice += UnityEngine.Random.Range(0.2f, 0.4f);
        //_currentLoadPrice = Mathf.Clamp01(_currentLoadPrice);
        //UISystem.Instance.HintUI.Loading(_currentLoadPrice);
        //if (_currentLoadPrice == 1)
        //{
        //    _isLoad = false;
        //    mLoadState = LoadState.None;
        //    _currentLoadPrice = 0.0F;
        //    UISystem.Instance.HintUI.CloseLoadBG();
        //}
    }

    public void LoadStart()
    {
        //if (_currentLoadPrice < _maxLoadPrice)
        //{
        //    _currentLoadPrice += UnityEngine.Random.Range(0.0005f, 0.0008f);
        //    _currentLoadPrice = Mathf.Clamp01(_currentLoadPrice);
        //    UISystem.Instance.HintUI.Loading(_currentLoadPrice);
        //}
    }

    public void ErrorCodeHint(int errorCode)
    {
        //switch (errorCode)
        //{
        //    case GlobalConst.ERR_STOREMAXHP:
        //        {
        //            AddHintStr(ConstString.ERROR_STORE_MAXHP);
        //            break;
        //        }

        //    case GlobalConst.ERR_LACKOFGLOD:
        //        {
        //            AddHintStr(ConstString.GLODNOTENOUGH);
        //            break;
        //        }

        //    case GlobalConst.ERR_LACKOFGEM:
        //        {
        //            AddHintStr(ConstString.DIAMONDNOTENOUGH);
        //            break;
        //        }

        //}
    }


}
