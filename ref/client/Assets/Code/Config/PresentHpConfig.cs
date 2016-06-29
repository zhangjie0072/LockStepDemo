using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

public class PresentHpConfig
{
    string name = GlobalConst.DIR_XML_PRESENTHP;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public Dictionary<string, string> configs = new Dictionary<string, string>();
    //private long preTime;

    public PresentHpConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadConfig();
        //preTime = GameSystem.mTime;
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		UnityEngine.Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            UnityEngine.Debug.LogError("LoadConfig failed: " + name);
            return;
        }
        
        XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PRESENTHP, text);
        XmlNode root = doc.SelectSingleNode("Data");
        foreach (XmlNode line in root.SelectNodes("Line"))
        {
            if (CommonFunction.IsCommented(line))
                continue;

            XmlElement elementTime = line.SelectSingleNode("time") as XmlElement;
            XmlElement elementValue = line.SelectSingleNode("hp") as XmlElement;

            configs.Add(elementTime.InnerText, elementValue.InnerText);
		}
		
    }
    public int IsGetPresentHP()
    {
        DateTime s = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        long iTime = long.Parse(GameSystem.mTime + "0000000");
        TimeSpan toNow = new TimeSpan(iTime);
        s = s.Add(toNow);

        string _htime = s.ToString("HH");
        string _mtime = s.ToString("mm");
        int _time = Int32.Parse(_htime) * 60 + Int32.Parse(_mtime);

        int index = 0;
        foreach (var item in configs)
        {
            index++;
            string[] time = item.Key.Split('&');
            for (int i = 0; i < time.Length; i++)
            {
                string[] splitConfigTime = time[i].Split(':');
                int configTime1 = Int32.Parse(splitConfigTime[0]) * 60 + Int32.Parse(splitConfigTime[1]);
                splitConfigTime = time[i + 1].Split(':');
                int configTime2 = Int32.Parse(splitConfigTime[0]) * 60 + Int32.Parse(splitConfigTime[1]);
                if (_time < configTime1 || _time > configTime2)
                {
                    break;
                }
                else
                {
                    return index;
                }
            }
        }
        return -1;
    }

    public int GetHP() 
    {
        string _value = "";
        foreach (var item in configs)
        {
            _value = item.Value;
            break;
        }
        return Int32.Parse(_value);
    }

    public string GetTimeInterval() 
    {
        string _timeinterval = "";
        foreach (var item in configs)
        {
            _timeinterval += item.Key + " ";
        }
        string _replaceTimeinterval = _timeinterval.Replace('&', '-');
        return _replaceTimeinterval;
    }
}
