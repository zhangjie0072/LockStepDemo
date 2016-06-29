using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.config;
using System.Runtime.InteropServices;
public class PushItem
{
	public uint id;
	public uint type;
	public string date;
	public string time;
	public uint online;
	public string content;
}


public class PushConfig
{
    string name = GlobalConst.DIR_XML_PUSH;
    bool isLoadFinish = false;
    private object LockObject = new object();

	Dictionary<uint,PushItem> _items = new Dictionary<uint,PushItem>();
	Dictionary<uint/*type*/,PushItem> _pushInfos = new Dictionary<uint, PushItem>();
	public string _hpStr = null;
	public int _hpId = 0;

	public PushConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadPushData();
	}

    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

#if IOS_SDK
    [DllImport("__Internal")]
    public static extern void recvPush(int identifier, int type, string date, string time, int online, string content);
	[DllImport("__Internal")]
    public static extern void setHpRestoreTime(int storeTime);

    public void RecvPushWrapper(int identifier, int type, string date, string time, int online, string content)
    {
        recvPush(identifier, type, date, time, online, content);
    }
#endif

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name);
            return;
        }

		Debug.Log("Config reading " + name);
        _items.Clear();
        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PUSH, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
			{
				continue;
			}

			PushItem item = new PushItem();
            foreach (XmlElement xel in xe)
            {
//				Debug.Log("read push config "+xel.Name+","+xel.InnerText);
                uint value = 0;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
					item.id = value;
                }
                else if (xel.Name == "type")
                {
                    uint.TryParse(xel.InnerText, out value);
					item.type = value;

                }
                else if (xel.Name == "date")
                {
					item.date = xel.InnerText;
                }
                else if (xel.Name == "time")
                {
					item.time = xel.InnerText;
                }
                else if (xel.Name == "online")
                {
                    uint.TryParse(xel.InnerText, out value);
                    item.online= value;
                }
                else if (xel.Name == "content")
                {
					item.content = xel.InnerText;
                }
            }
			if (!_items.ContainsKey(item.id))
            {

				if(item.date!="0")
				{
					System.DateTime Now = System.DateTime.Now;
					System.DateTime CDt = System.DateTime.Parse(item.date);
//					Debug.Log("Now "+Now.ToShortDateString()+" cdt "+CDt.ToShortDateString());
					if(Now.CompareTo(CDt.AddDays(30))<0 && Now.AddDays(30).CompareTo(CDt)>0)
					{

//						Debug.Log("add item id to list "+item.date);
						_items.Add(item.id,item);
					}
				}
				else{
					_items.Add(item.id,item);
				}

            }
        }
		
    }

    public void SetConfigForPhoneSys()
    {
       // all config read finish then call this function.
#if ANDROID_SDK
    if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

            object[] datas = new object[] {(int)GameSystem.Instance.CommonConfig.GetUInt("gHpRestoreTime") };
            jo.Call("setHpRestoreTime", datas);
        }
#endif

#if IOS_SDK
        setHpRestoreTime((int)GameSystem.Instance.CommonConfig.GetUInt("gHpRestoreTime"));
#endif
        foreach (PushItem item in _items.Values)
        {
			switch(item.type)
			{
				// first win push info modify
				case 5:
				{
					PushItem pi = null;
					if(_pushInfos.TryGetValue(item.type,out pi))
					{
						item.date = pi.date;
						item.time = pi.time;
						//将信息存储到pushInfo里面，获得首胜后再次推送
						pi = item;

//						_pushInfos.Remove(pi.type);
						#if ANDROID_SDK
						if (Application.platform == RuntimePlatform.Android)
						{

							AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
							AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
							int id = (int)item.id;
							int type = (int)item.type;
							int online = (int)item.online;
							/*
								public void SetLocalPush(int id, int online, int second, String content )
							*/
							int time ;
							if(!int.TryParse(item.time,out time))
							{
								time = 0;
							}
							object[] datas = new object[] { id,online,time,item.content };
//							Debug.Log("SetLocalPush push config "+time+","+type+","+item.content);
							jo.Call("SetLocalPush", datas);

						}
						#endif
						#if IOS_SDK
//						int id = (int)item.id;
//						int type = (int)item.type;
//						int online = (int)item.online;
//						Debug.Log("PushConfig recvPush Start called");
//						recvPush(id, type, item.date,item.time,online,item.content );
//						Debug.Log("PushConfig SetLocalPush End called");
						#endif

					}
					
					break;
				}
				default:
				{		
					#if ANDROID_SDK
					if (Application.platform == RuntimePlatform.Android)
					{

					AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
					int id = (int)item.id;
					int type = (int)item.type;
					int online = (int)item.online;

//					Debug.Log("recvPush push config "+item.time+","+item.type+","+item.content);
					object[] datas = new object[] { id, type, item.date,item.time,online,item.content };
					jo.Call("RecvPush", datas);

					if( type == 4)
					{
					_hpStr = item.content;
					_hpId = (int)item.id;
					}

					}
					#endif
					#if IOS_SDK
					int id = (int)item.id;
					int type = (int)item.type;
					int online = (int)item.online;
					Debug.Log("PushConfig recvPush Start called");
					recvPush(id, type, item.date,item.time,online,item.content );
					Debug.Log("PushConfig recvPush End called");
					if( type == 4)
					{
					_hpStr = item.content;
					_hpId = (int)item.id;
					}
					#endif
					break;
				}
			}
		

        }
    }
	/// <summary>
	/// 设置推送消息
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="type">Type.</param>
	/// <param name="online">Online.</param>
	/// <param name="date">Date.</param>
	/// <param name="time">Time.</param>
	/// <param name="content">Content.</param>
	public void SetPushInfo(int id/*推送ID*/
	                        ,int type/*推送类型*/
	                        ,int online/*离线提示或者游戏内提示*/
	                        ,int date/*推送日期*/
	                        ,int time/*推送时间（剩余时间/s）*/
	                        ,string content/*推送文本*/
	                        )
	{
		string s_date = ""+date;
//		System.DateTime dt = System.DateTime.Now;
//		dt = dt.AddSeconds(time);
		string s_time = ""+time;//dt.GetDateTimeFormats('t')[0].ToString()";
//		Debug.Log("s_timet "+s_time);
		PushItem pi;
		//modify exist element
		bool configAlreadyRead = false;
		foreach(PushItem item in _items.Values)
		{
			//已经读取了配置从配置设置数据直接发送
			if(item.type == type)
			{
				configAlreadyRead = true;
				content = item.content;
				online = (int)item.online;
				id = (int)item.id;
				break;
			}
		}
		if(!configAlreadyRead){
			//如果已经发送过一次，通过配置来重置时间再次发送
			if(_pushInfos.ContainsKey((uint)type))
			{
				pi = _pushInfos[(uint)type];
				pi.time = s_time;
				content = pi.content;
				online = (int)pi.online;
				id = (int)pi.id;
			}
			else{
				//还未读取配置，设置配置时间
				pi = new PushItem();
				pi.id = (uint)id;
				pi.date = s_date;
				pi.online = (uint)online;
				pi.time = s_time;
				pi.content = content;
				pi.type = (uint)type;
				_pushInfos.Add(pi.type,pi);
				Debug.Log("send push info after config read.");
				return;
			}
		}
		Debug.Log("send push info directly.");

	#if ANDROID_SDK
		if (Application.platform == RuntimePlatform.Android)
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			object[] datas = new object[] { id, online,time,content };
//			Debug.Log("SetLocalPush push config "+time+","+type+","+content);
			jo.Call("SetLocalPush", datas);			
		}
	#endif
	#if IOS_SDK
//		Debug.Log("PushConfig recvPush Start called");
//		recvPush(id, type, s_date,time,online,content );
//		Debug.Log("PushConfig recvPush End called");
//		if( type == 4)
//		{
//			_hpStr = content;
//			_hpId = id;
//		}
	#endif
	}
}