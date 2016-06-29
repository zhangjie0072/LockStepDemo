using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Linq;

public class DaySignData
{
    public uint sign_award;
    public uint award_count;
    public uint vip_level;
    public uint vip_award;
}

public class MonthSignData
{
    public uint sign_times;
    public uint sign_award1;
    public uint award_count1;
    public uint sign_award2;
    public uint award_count2;
    public uint sign_award3;
    public uint award_count3;
}

public class SignConfig
{
    string name1 = GlobalConst.DIR_XML_DAY_SIGN;
    string name2 = GlobalConst.DIR_XML_MONTH_SIGN;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    //public Dictionary<uint, DaySignData> dsigndataLists = new Dictionary<uint, DaySignData>();
    public Dictionary<uint, MonthSignData> msigndataLists = new Dictionary<uint, MonthSignData>();
    public Dictionary<uint, Dictionary<uint, DaySignData>> allDaySignData = new Dictionary<uint, Dictionary<uint, DaySignData>>();
	
    public SignConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        //ReadDaySign();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        //ReadMonthSign();
    }
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 2)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }
    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }
		Debug.Log("Config reading " + name1);
        ReadDaySign();
		
		Debug.Log("Config reading " + name2);
		ReadMonthSign();
		
    }

    void ReadDaySign()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_DAY_SIGN, text);
        XmlNode node= xmlDoc.SelectSingleNode("Data");
        //uint currentMonth = GetCurrentMonth();
        foreach (XmlNode line in node.SelectNodes("Line"))
        {
            if (CommonFunction.IsCommented(line))
                continue;

            XmlElement cMonth = line.SelectSingleNode("current_month") as XmlElement;
            if (!allDaySignData.ContainsKey(uint.Parse(cMonth.InnerText)))
            {
                Dictionary<uint, DaySignData> dsigndataLists = new Dictionary<uint, DaySignData>();
                allDaySignData.Add(uint.Parse(cMonth.InnerText), dsigndataLists);
            }
            DaySignData dsigndata = new DaySignData();
            XmlElement signTime = line.SelectSingleNode("sign_times") as XmlElement;
            XmlElement signAward = line.SelectSingleNode("sign_award") as XmlElement;
            XmlElement awardCount = line.SelectSingleNode("award_count") as XmlElement;
            XmlElement vipLevel = line.SelectSingleNode("vip_level") as XmlElement;
            XmlElement vipAward = line.SelectSingleNode("vip_award") as XmlElement;
            dsigndata.sign_award = uint.Parse(signAward.InnerText);
            dsigndata.award_count = uint.Parse(awardCount.InnerText);
            if (vipAward.InnerText != null && vipAward.InnerText != "")
                dsigndata.vip_award = uint.Parse(vipAward.InnerText);
            if (vipLevel.InnerText != null && vipLevel.InnerText != "")
                dsigndata.vip_level = uint.Parse(vipLevel.InnerText);
            if (!allDaySignData[uint.Parse(cMonth.InnerText)].ContainsKey(uint.Parse(signTime.InnerText)))
                allDaySignData[uint.Parse(cMonth.InnerText)].Add(uint.Parse(signTime.InnerText), dsigndata);
        }
    }

    public uint GetCurrentMonth() 
    {
        DateTime s = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        long iTime = long.Parse(GameSystem.mTime + "0000000");
        TimeSpan toNow = new TimeSpan(iTime);
        s = s.Add(toNow);

        return uint.Parse(s.ToString("MM"));
    }

    public DaySignData GetDaySignData(uint _signtimes) 
    {
        uint currentMonth = GetCurrentMonth();
        Dictionary<uint, DaySignData> daySignList;
        allDaySignData.TryGetValue(currentMonth, out daySignList);
        if (daySignList != null && daySignList.Count > 0)
        {
            DaySignData daySignData;
            if (daySignList.ContainsKey(_signtimes))
            {
                daySignList.TryGetValue(_signtimes, out daySignData);
                return daySignData;
            }
            else
                return null;
        }
        return null;
    }

    public uint GetSignDays()
    {
        uint currentMonth = GetCurrentMonth();
        Dictionary<uint, DaySignData> daySignList;
        allDaySignData.TryGetValue(currentMonth, out daySignList);
        if (daySignList != null && daySignList.Count > 0)
        {
            uint days = daySignList.Keys.Max();
            return days;
        }
        return 0;
    }

    void ReadMonthSign()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_MONTH_SIGN, text);
        XmlNode node = xmlDoc.SelectSingleNode("Data");
        //uint currentMonth = GetCurrentMonth();
        foreach (XmlNode line in node.SelectNodes("Line"))
        {
            if (CommonFunction.IsCommented(line))
                continue;

            MonthSignData msigndata = new MonthSignData();
            XmlElement signTime = line.SelectSingleNode("sign_times") as XmlElement;
            XmlElement signAward1 = line.SelectSingleNode("sign_award1") as XmlElement;
            XmlElement awardCount1 = line.SelectSingleNode("award_count1") as XmlElement;
            XmlElement signAward2 = line.SelectSingleNode("sign_award2") as XmlElement;
            XmlElement awardCount2 = line.SelectSingleNode("award_count2") as XmlElement;
            XmlElement signAward3 = line.SelectSingleNode("sign_award3") as XmlElement;
            XmlElement awardCount3 = line.SelectSingleNode("award_count3") as XmlElement;
            msigndata.sign_times = uint.Parse(signTime.InnerText);
            msigndata.sign_award1 = uint.Parse(signAward1.InnerText);
            msigndata.award_count1 = uint.Parse(awardCount1.InnerText);
            if (signAward2.InnerText != null && signAward2.InnerText != "")
                msigndata.sign_award2 = uint.Parse(signAward2.InnerText);
            if (awardCount2.InnerText != null && awardCount2.InnerText != "")
                msigndata.award_count2 = uint.Parse(awardCount2.InnerText);
            if (signAward3.InnerText != null && signAward3.InnerText != "")
                msigndata.sign_award3 = uint.Parse(signAward3.InnerText);
            if (awardCount3.InnerText != null && awardCount3.InnerText != "")
                msigndata.award_count3 = uint.Parse(awardCount3.InnerText);
            msigndataLists.Add(uint.Parse(signTime.InnerText), msigndata);
        }
    }

    public MonthSignData GetMonthSignData(uint _gettimes)
    {
        MonthSignData monthData = null;
        if (msigndataLists.ContainsKey((_gettimes + 1) * 5)) 
        {
            msigndataLists.TryGetValue((_gettimes + 1) * 5, out monthData);
        }
        return monthData;
    }
}

