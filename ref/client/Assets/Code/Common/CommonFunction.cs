using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using fogs.proto.msg;
using System.Security.Cryptography;
using System.Xml;
using LuaInterface;

public static class CommonFunction
{
    //获取文字常量
    public static string GetConstString(string name)
    {
        string str = GameSystem.Instance.ConstStringConfigData.GetInfoByName(name);
        str = str.Replace(@"\n", "\n");
        return str;
    }
    public static string GetLeftTimeStr(long vTotalTime)
    {
        return string.Format(GlobalConst.TIMEPATTERN, (vTotalTime / 3600).ToString(), vTotalTime % 3600 / 60, vTotalTime % 60);
    }

    public static long GetLfetTime(long vTotalTime)
    {
        return vTotalTime - SceneProc.mTime;
    }

    public static bool IsEndTime(long vTotalTime)
    {
        long _nowTime = SceneProc.mTime;
        if (_nowTime >= vTotalTime)
            return true;
        else
            return false;
    }

    public static bool GetFileMD5(string filePath, out string fileMD5)
    {
        fileMD5 = "";
        if (!File.Exists(filePath))
        {
            return false;
        }
        MD5CryptoServiceProvider md5Generator = new MD5CryptoServiceProvider();
        FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] hash = md5Generator.ComputeHash(file);
        fileMD5 = System.BitConverter.ToString(hash);
        file.Close();
        return true;
    }

    public static GameObject InstantiateObject(GameObject prefab, Transform parent)
    {
        if (prefab == null)
            return null;
        //Debug.Log("------------InstantiateObject() -- " + prefab.name);
        GameObject clone = GameObject.Instantiate(prefab) as GameObject;
        if (clone != null && parent != null)
        {
            clone.transform.parent = parent;
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localRotation = Quaternion.identity;
            clone.transform.localScale = prefab.transform.localScale;
			if (clone.GetComponent<UIPanel>() == null)
			{
				UIWidget widget = NGUITools.FindInParents<UIWidget>(parent);
				UIWidget[] cloneWidgets = clone.GetComponentsInChildren<UIWidget>(true);
				if (cloneWidgets != null && cloneWidgets.Length > 0 && widget != null)
					NGUITools.AdjustDepth(clone, widget.depth + 1);
			}
        }
        return clone;
    }

	public static GameObject InstantiateObject(string prefabPath, Transform parent)
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab(prefabPath) as GameObject;
		return InstantiateObject(prefab, parent);
	}


    public static void ShowUpdateTip(string message, Transform parent = null,
        UIEventListener.VoidDelegate onOKClick = null, UIEventListener.VoidDelegate onCancelClick = null,
        string okLabel = null, string cancelLabel = null)
    {
        if (parent == null)
        {
            UIManager.Instance.CreateUIRoot();
            parent = UIManager.Instance.m_uiRootBasePanel.transform;
        }
        GameObject popup_message_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PopupUpdateTip") as GameObject;
        GameObject popup_message_obj = CommonFunction.InstantiateObject(popup_message_prefab, parent);
        Vector3 position = popup_message_obj.transform.localPosition;
        position.z = -500;
        popup_message_obj.transform.localPosition = position;
        NGUITools.BringForward(popup_message_obj);
		popup_message_obj.transform.FindChild("Window/Message").GetComponent<UILabel>().text = message;
		GameObject buttonOK = popup_message_obj.transform.FindChild("Window/ButtonOK").gameObject;
		GameObject buttonCancel = popup_message_obj.transform.FindChild("Window/ButtonCancel").gameObject;
		UIEventListener.Get(buttonOK).onClick += (go) => NGUITools.Destroy(popup_message_obj);
		UIEventListener.Get(buttonCancel).onClick += (go) => NGUITools.Destroy(popup_message_obj);
		if (onOKClick != null)
			UIEventListener.Get(buttonOK).onClick += onOKClick;
		if (onCancelClick != null)
			UIEventListener.Get(buttonCancel).onClick += onCancelClick;
    }

    public static LuaComponent ShowPopupMsg(string message, Transform parent = null,
        UIEventListener.VoidDelegate onOKClick = null, UIEventListener.VoidDelegate onCancelClick = null,
        string okLabel = null, string cancelLabel = null)
    {
		if (parent == null)
		{
			UIManager.Instance.CreateUIRoot();
			parent = UIManager.Instance.m_uiRootBasePanel.transform;
		}
		if (onCancelClick == null)
		{
			if (okLabel == null)
				okLabel = GetConstString("BUTTON_OK");
			if (cancelLabel == null)
				cancelLabel = "";
		}
		else
		{
			if (okLabel == null)
				okLabel = GetConstString("BUTTON_CONFIRM");
			if (cancelLabel == null)
				cancelLabel = GetConstString("BUTTON_CANCEL");
		}
        GameObject popup_message_prefab;
        if (message.Contains("[-]"))
        {
            popup_message_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TipWithTween") as GameObject;
        }
        else
        {
            if (onCancelClick == null)
            {
                popup_message_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PopupMessage") as GameObject;
            }
            else
            {
                popup_message_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PopupMessage1") as GameObject;
            }  
        }
        GameObject popup_message_obj = CommonFunction.InstantiateObject(popup_message_prefab, parent);
        Vector3 position = popup_message_obj.transform.localPosition;
        position.z = -500;
        popup_message_obj.transform.localPosition = position;
        LuaComponent popup_message = popup_message_obj.GetComponent<LuaComponent>();
        //popup_message.table.Set("message", message);
        LuaInterface.LuaFunction func = popup_message.table["SetMessage"] as LuaInterface.LuaFunction;
        func.Call(new object[] { popup_message.table, message, message.Length > 12 });
        popup_message.table.Set("onOKClick", onOKClick);
        popup_message.table.Set("onCancelClick", onCancelClick);
        popup_message.table.Set("okLabel", okLabel);
        popup_message.table.Set("cancelLabel", cancelLabel);
        NGUITools.BringForward(popup_message_obj);
        return popup_message;
    }
    /// <summary>
    /// 渐变提示框
    /// </summary>
    /// <param name="message"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static LuaComponent ShowTip(string message, Transform parent = null)
    {
        //if (parent == null)
        //    parent = GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform;
        //GameObject tipWithTweenObj = UIManager.Instance.CreateUI("Prefab/GUI/TipWithTween", parent);
        //TipWithTween tipWithTween = tipWithTweenObj.GetComponent<TipWithTween>();
        //tipWithTween.message = message;
        //Vector3 pos = tipWithTweenObj.transform.localPosition;
        //tipWithTweenObj.transform.localPosition = new Vector3(pos.x, pos.y, -450.0f);
        //tipWithTweenObj.GetComponent<UIPanel>().depth = 5000;
        //return tipWithTween;
        if (parent == null)
        {
            UIManager.Instance.CreateUIRoot();
            parent = UIManager.Instance.m_uiRootBasePanel.transform;
        }
        GameObject popup_message_prefab;
        if (message.Contains("[-]"))
        {
            popup_message_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TipWithTween") as GameObject;
        }
        else
        {
            popup_message_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PopupMessage") as GameObject;
        }
            GameObject popup_message_obj = CommonFunction.InstantiateObject(popup_message_prefab, parent);
            Vector3 position = popup_message_obj.transform.localPosition;
            position.z = -500;
            popup_message_obj.transform.localPosition = position;
            LuaComponent popup_message = popup_message_obj.GetComponent<LuaComponent>();
            //popup_message.table.Set("message", message);
            LuaInterface.LuaFunction func = popup_message.table["SetMessage"] as LuaInterface.LuaFunction;
            func.Call(new object[] { popup_message.table, message });
            NGUITools.BringForward(popup_message_obj);
 
        return popup_message;
    }

    public static LuaComponent ShowErrorMsg(ErrorID code)
    {
        return ShowErrorMsg(code, null);
    }

    public static LuaComponent ShowErrorMsg(ErrorID code, Transform parent)
    {
        return ShowErrorMsg(code, parent, null);
    }

    public static LuaComponent ShowErrorMsg(ErrorID code, Transform parent, UIEventListener.VoidDelegate onOKClick)
    {
        string message = ErrorMessage.Instance.GetMessage(code);
        if (message == null || message == string.Empty)
            message = string.Format(CommonFunction.GetConstString(code.ToString()), code);

        if (parent == null)
        {
			parent = UIManager.Instance.m_uiRootBasePanel.transform;
        }

        return ShowPopupMsg(message, parent, onOKClick, null, CommonFunction.GetConstString("BUTTON_OK"), "");
    }

    static GameObject waitMask = null;
    public static GameObject ShowWaitMask()
    {
		if (waitMask != null)
			return waitMask;
		waitMask = UIManager.Instance.CreateUI("WaitMask");
		NGUITools.SetActive(waitMask.gameObject, true);
        return waitMask;
    }

    public static void HideWaitMask()
    {
		if (waitMask != null)
		{
			NGUITools.Destroy(waitMask);
			waitMask = null;
		}
    }

    public static uint GetObjectID(GameObject go)
    {
        if (go)
        {
            Transform trans = go.transform.FindChild("ID");
            if (trans)
            {
                UILabel lblID = trans.GetComponent<UILabel>();
                if (lblID)
                {
                    uint value;
                    uint.TryParse(lblID.text, out value);
                    return value;
                }
            }
        }
        return 0;
    }

    public static void DebugLog(object vInfo, LogType vDebugType = LogType.Log)
    {
        if (GlobalConst.IS_OPENORRELEASE)
        {
            switch (vDebugType)
            {
                case LogType.Log:
                    {
                        Debug.Log(vInfo);
                        break;
                    }
                case LogType.Warning:
                    {
                        Debug.LogWarning(vInfo);
                        break;
                    }
                case LogType.Error:
                    {
                        Debug.LogError(vInfo);
                        break;
                    }
            }
        }
    }

    public static double PointToAngle(Vector3 AOrigin, Vector3 APoint, double AEccentricity)
    {
        if (APoint.x == AOrigin.x)
        {
            if (APoint.y > AOrigin.y)
                return Math.PI * 0.5;
            else
                return Math.PI * 1.5;
        }
        else if (APoint.y == AOrigin.y)
        {
            if (APoint.x > AOrigin.x)
                return 0;
            else
                return Math.PI;
        }
        else
        {
            double Result = Math.Atan((AOrigin.y - APoint.y) / (AOrigin.x - APoint.x) * AEccentricity);
            if ((APoint.x < AOrigin.x) && (APoint.y > AOrigin.y))
                return Result + Math.PI;
            else if ((APoint.x < AOrigin.x) && (APoint.y < AOrigin.y))
                return Result + Math.PI;
            else if ((APoint.x > AOrigin.x) && (APoint.y < AOrigin.y))
                return Result + 2 * Math.PI;
            else
                return Result;
        }
    }

    //大厅动作播放
    public static void PlayAnimation(Player player, int posID, bool playNeedBall = true)
    {
        if (player == null || player.gameObject == null || player.gameObject.GetComponent<Animation>() == null || player.gameObject.GetComponent<Animation>().isPlaying)
        {
            return;
        }

        string posName = posID.ToString();
        if (player.m_gender == GenderType.GT_MALE)
            posName = "posM" + posName;
        else if (player.m_gender == GenderType.GT_FEMALE)
            posName = "posG" + posName;
        player.PlayAnimation(posName);
    }

    //Clear UIGrid Child
    public static void ClearGridChild(Transform trans)
    {
        if (trans == null)
            return;

        UIGrid grid = trans.GetComponent<UIGrid>();
        if (grid)
        {
			while (grid.transform.childCount > 0)
            {
                NGUITools.Destroy(grid.transform.GetChild(0).gameObject);
            }
        }
    }

    public static void ClearTableChild(Transform trans)
    {
        if (trans == null)
            return;

        UITable table = trans.GetComponent<UITable>();
        if (table)
        {
            for (int i = 0; i < table.children.Count; ++i )
            {
                NGUITools.Destroy(table.children[i].gameObject);
            }
            table.children.Clear();
        }
    }

	public static void ClearChild(Transform tm)
	{
		if (tm == null)
			return;
		while (tm.childCount > 0)
			NGUITools.Destroy(tm.GetChild(0).gameObject);
	}

    //
    public static XmlDocument LoadXmlConfig(string fileName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        if (GlobalConst.IS_USEBINFILE)
        {
            xmlDoc.LoadXml(XMLUtility.Bin2XML(fileName));
        }
        else
        {
            string conf = ResourceLoadManager.Instance.GetConfigText(fileName);
            xmlDoc.LoadXml(conf);
        }
        return xmlDoc;
    }
    
    //
    public static XmlDocument LoadXmlConfig(string fileName, string text)
    {
        XmlDocument xmlDoc = new XmlDocument();
        if (GlobalConst.IS_USEBINFILE)
        {
            xmlDoc.LoadXml(XMLUtility.Bin2XML(fileName));
        }
        else
        {
            xmlDoc.LoadXml(text);
        }
        return xmlDoc;
    }

    //
    public static XmlDocument LoadXmlConfig(string fileName, object obj)
    {
        XmlDocument xmlDoc = new XmlDocument();
        if (GlobalConst.IS_USEBINFILE)
        {
            xmlDoc.LoadXml(XMLUtility.Bin2XML(fileName));
        }
        else
        {
            TextAsset conf = obj as TextAsset;
            xmlDoc.LoadXml(conf.text);
        }
        return xmlDoc;
    }

	public static bool IsCommented(XmlNode line)
	{
		XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
		if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
			return true;
		return false;
	}

	public static string[] SplitLines(string text)
	{
		return text.Split('\r', '\n');
	}

	public static T GetComponentInParent<T>(Transform tm) where T : Component
	{
		T component = null;
		do
		{
			component = tm.GetComponent<T>();
			tm = tm.parent;
		} while (component == null && tm != null);
		return component;
	}

	public static T GetComponentInParent<T>(GameObject go) where T : Component
	{
		return GetComponentInParent<T>(go.transform);
	}

	//十进制切分一个数字，可指定位数
	public static uint[] GetDigits(uint number, uint digitCount = 0)
	{
		List<uint> digits = new List<uint>();
		do
		{
			digits.Add(number % 10);
			number = number / 10;
		} while (number > 0);
		while (digitCount > 0 && digits.Count < digitCount)
			digits.Add(0u);
		return digits.ToArray();
	}

    public static string GetQualityString(string orig)
    {
        return orig.Substring(0, 4).ToUpper();
    }

    public static string GetQualitySymbolString(string orig)
    {
        if (orig.Length > 5)
            return orig.Substring(5, orig.Length - 5).ToLower();
        return "";
    }

	public static void Break()
	{
		Debug.Break();
	}

    public static void SetAnimatorShade(bool ans)
    {
        if (UIManager.Instance.m_uiRootBasePanel == null)
            return;

        Transform trans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("Mask/AnimatorShader");
        if (trans == null)
            return;

        GameObject shade = trans.gameObject;
        if (shade.activeSelf != ans)
        {
            NGUITools.SetActive(shade, ans);
        }
    }

	public static double TickTime()
	{
		return System.DateTime.Now.Ticks * 0.0000001;
	}
	/// <summary>
	/// Shows the wait ui .
	/// </summary>
	public static void ShowWait()
	{
		UIWait.ShowWait();
	}
	public static void StopWait()
	{
		UIWait.StopWait();
	}
	public static void SetPushInfo(int id,int type,int online,int date,int time,string content)
	{
		if(time<0 || content == string.Empty)
			return;
		GameSystem.Instance.pushConfig.SetPushInfo(id,type,online,date,time,content);
	}
	/// <summary>
	/// 获取列表对应的luaTable
	/// </summary>
	/// <param name="type">列表类型</param>
	/// <returns></returns>
	public static LuaTable GetListByType(int type,object L_Userdata)
	{
		Debug.Log("get list by type "+type);
		switch (type)
		{
		case 1:
			{
//				List<fogs.proto.config.TaskConditionInfo> list = L_Userdata as List<fogs.proto.config.TaskConditionInfo>;
//				if(list!=null)
//					return list.toLuaTable();
			}
			break;
		case 2:
			{
				List<uint> list = L_Userdata as List<uint>;
				if(list!=null)
					return list.toLuaTable();
			}
			break;
		case 3://AwardConfig
			{
				List<fogs.proto.config.AwardConfig> list = L_Userdata as List<fogs.proto.config.AwardConfig>;
				if (list!=null)
					return list.toLuaTable();
			}
			break;
		default:
			break;
		}
		return null;
	}

}
