using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using fogs.proto.msg;
using LuaInterface;

public class FriendData
{
    public delegate void OnListChanged(FriendOperationType t);
    public OnListChanged onListChanged;
    public List<OnListChanged> _changedCallBackList = new List<OnListChanged>();

    public static OnListChanged FriendListChangedDelegate(LuaFunction func)
    {
        OnListChanged action = (val) => { func.Call(val); };
        return action;
    }

    /// <summary>
    /// 好友列表
    /// </summary>
    public List<FriendInfo> friend_list;
    /// <summary>
    /// 申请列表
    /// </summary>
    public List<FriendInfo> apply_list;
    /// <summary>
    /// 黑名单列表
    /// </summary>
    public List<FriendInfo> black_list;
    /// <summary>
    /// 礼物列表
    /// </summary>
    public List<FriendInfo> gift_list;
    /// <summary>
    /// 赠送次数
    /// </summary>
    public uint present_times;
    /// <summary>
    /// 收取礼物的次数
    /// </summary>
    public uint get_gift_times;

    /// <summary>
    /// 申请数量最大限制
    /// </summary>
    public uint maxFriendsApply;

    /// <summary>
    /// 删除列表中的数据
    /// </summary>
    /// <param name="list">列表</param>
    /// <param name="acc_id">玩家ID</param>
    /// <returns></returns>
    private bool RemoveByAccID(List<FriendInfo> list, uint acc_id)
    {
        bool result = false;
        for (int index = list.Count - 1; index >= 0; index--)
        {
            if (list[index].acc_id == acc_id)
            {
                list.RemoveAt(index);
                result = true;
            }
        }

        return result;
    }

    protected static FriendData _instance;
    public static FriendData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FriendData();
            }
            return _instance;
        }
        private set { }
    }

    public void Init()
    {
        this.friend_list = new List<FriendInfo>();
        this.apply_list = new List<FriendInfo>();
        this.black_list = new List<FriendInfo>();
        this.gift_list = new List<FriendInfo>();

        this.present_times = 0;
        this.get_gift_times = 0;

        this.maxFriendsApply = GameSystem.Instance.CommonConfig.GetUInt("gFriendsApplyMax");

        RequestAllListData();
    }


    private void RequestAllListData()
    {
        SendUpdateFriendList();

        FriendOperationReq req = new FriendOperationReq();
        req = new FriendOperationReq();
        req.type = FriendOperationType.FOT_QUERY_APPLY;
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack<FriendOperationReq>(0, req, MsgID.FriendOperationReqID);

        req = new FriendOperationReq();
        req.type = FriendOperationType.FOT_QUERY_BLACK;
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack<FriendOperationReq>(0, req, MsgID.FriendOperationReqID);

        req = new FriendOperationReq();
        req.type = FriendOperationType.FOT_QUERY_GIFT;
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack<FriendOperationReq>(0, req, MsgID.FriendOperationReqID);
    }

    public void SendUpdateFriendList()
    {
        FriendOperationReq req = new FriendOperationReq();
        req.type = FriendOperationType.FOT_QUERY;
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack<FriendOperationReq>(0, req, MsgID.FriendOperationReqID);
    }

    public void OnFriendOperationResp(FriendOperationResp resp)
    {
        if (resp.result != 0)
        {
            Logger.Log(string.Format("好友操作 Type:{0} Error:{1} ", resp.type, resp.result));
            CommonFunction.ShowErrorMsg((ErrorID)resp.result);
            return;
        }

        this.get_gift_times = resp.get_gift_times;
        this.present_times = resp.present_times;

        switch (resp.type)
        {
            case FriendOperationType.FOT_PRESEND:
                if (resp.op_friend != null) //resp.op_friend==null 属于自己赠送出去，不等于null是收到礼物
                {
                    RemoveByAccID(gift_list, resp.op_friend.acc_id);
                    gift_list.Add(resp.op_friend); //xxx收到礼物
                    this.ListChanged(FriendOperationType.FOT_QUERY_GIFT);
                    Logger.Log(string.Format("{0}送的礼物已收到！", resp.op_friend.name));
                }
                else
                {
                    if (resp.friend != null)
                    {
                        foreach (var fri in resp.friend)
                        {
                            var f = from n in friend_list where n.acc_id == fri.acc_id select n;
                            foreach (var v2 in f)
                            {
                                v2.present_flag = 1;
                            }
                        }
                    }
                    this.ListChanged(FriendOperationType.FOT_QUERY);
                }
                break;
            case FriendOperationType.FOT_ADD:
                RemoveByAccID(friend_list, resp.op_friend.acc_id);
                friend_list.Add(resp.op_friend);
                this.ListChanged(FriendOperationType.FOT_QUERY);
                if(this.RemoveByAccID(apply_list, resp.op_friend.acc_id)) //添加好友，需要删除申请列表中对应的记录
                    this.ListChanged(FriendOperationType.FOT_QUERY_APPLY);
                Logger.Log(string.Format("{0}已加入好友列表！", resp.op_friend.name));
                CommonFunction.ShowTip(string.Format(CommonFunction.GetConstString("STR_FRIENDS_TIPS_ADD"), resp.op_friend.name));
                break;
            case FriendOperationType.FOT_BLACK:
                RemoveByAccID(black_list, resp.op_friend.acc_id);
                black_list.Add(resp.op_friend);
                this.ListChanged(FriendOperationType.FOT_QUERY_BLACK);
                Logger.Log(string.Format("{0}已被加入黑名单！", resp.op_friend.name));
                CommonFunction.ShowTip(string.Format(CommonFunction.GetConstString("STR_FRIENDS_TIPS_BLACK_ADD"), resp.op_friend.name));
                break;
            case FriendOperationType.FOT_GETAWARDS:
                foreach(var fri in resp.friend)
                    this.RemoveByAccID(gift_list, fri.acc_id);
                this.ListChanged(FriendOperationType.FOT_QUERY_GIFT);
                Logger.Log(string.Format("你已查收领取礼物！"));
                CommonFunction.ShowTip(CommonFunction.GetConstString("STR_FRIENDS_TIPS_GET_GIFT"));
                break;
            case FriendOperationType.FOT_AGREE_APPLY:
                this.RemoveByAccID(apply_list, resp.op_friend.acc_id);
                this.RemoveByAccID(friend_list, resp.op_friend.acc_id);
                friend_list.Add(resp.op_friend);
                this.ListChanged(FriendOperationType.FOT_QUERY_APPLY);
                Logger.Log(string.Format("你同意{0}已加入好友列表！", resp.op_friend.name));
                CommonFunction.ShowTip(string.Format(CommonFunction.GetConstString("STR_FRIENDS_TIPS_AGREE"), resp.op_friend.name));
                break;
            case FriendOperationType.FOT_IGNORE_APPLY:
                this.RemoveByAccID(apply_list, resp.op_friend.acc_id);
                this.ListChanged(FriendOperationType.FOT_QUERY_APPLY);
                Logger.Log(string.Format("已忽略[{0}]好友申请信息！", resp.op_friend.name));
                break;
            case FriendOperationType.FOT_APPLY:
                this.RemoveByAccID(apply_list, resp.op_friend.acc_id);
                if (apply_list.Count >= maxFriendsApply)
                    apply_list.RemoveAt(0);
                apply_list.Add(resp.op_friend);
                this.ListChanged(FriendOperationType.FOT_QUERY_APPLY);
                Logger.Log(string.Format("{0}申请加您为好友！", resp.op_friend.name));
                break;
            case FriendOperationType.FOT_DEL_FRIEND:
                this.RemoveByAccID(friend_list, resp.op_friend.acc_id);
                this.ListChanged(FriendOperationType.FOT_QUERY);
                Logger.Log(string.Format("{0}已从好友列表中移除！", resp.op_friend.name));
                //CommonFunction.ShowTip(string.Format(CommonFunction.GetConstString("STR_FRIENDS_TIPS_DEL"), resp.op_friend.name));
                break;
            case FriendOperationType.FOT_DEL_BLACK:
                this.RemoveByAccID(black_list, resp.op_friend.acc_id);
                this.ListChanged(FriendOperationType.FOT_QUERY_BLACK);
                Logger.Log(string.Format("{0}已从黑名单中移除！", resp.op_friend.name));
                CommonFunction.ShowTip(string.Format(CommonFunction.GetConstString("STR_FRIENDS_TIPS_BLACK_DEL"), resp.op_friend.name));
                break;
            case FriendOperationType.FOT_QUERY:
                friend_list.Clear();
                friend_list.AddRange(resp.friend);
                this.ListChanged(FriendOperationType.FOT_QUERY);
                Logger.Log(string.Format("获取到{0}位好友信息！", friend_list.Count));
                break;
            case FriendOperationType.FOT_QUERY_BLACK:
                black_list.Clear();
                black_list.AddRange(resp.friend);
                this.ListChanged(FriendOperationType.FOT_QUERY_BLACK);
                Logger.Log(string.Format("获取到{0}位黑名单信息！", black_list.Count));
                break;
            case FriendOperationType.FOT_QUERY_APPLY:
                apply_list.Clear();
                apply_list.AddRange(resp.friend);
                this.ListChanged(FriendOperationType.FOT_QUERY_APPLY);
                Logger.Log(string.Format("获取到{0}位好友申请信息！", apply_list.Count));
                break;
            case FriendOperationType.FOT_QUERY_GIFT:
                gift_list.Clear();
                gift_list.AddRange(resp.friend);
                this.ListChanged(FriendOperationType.FOT_QUERY_GIFT);
                Logger.Log(string.Format("获取到{0}位礼物信息！", gift_list.Count));
                break;
            default:
                break;
        }
    }

    public void RegisterOnListChanged(OnListChanged callback)
    {
        if (_changedCallBackList.Contains(callback))
            return;
        this.onListChanged += callback;
        _changedCallBackList.Add(callback);
    }

    public void UnRegisterOnListChanged(OnListChanged callback)
    {
        if (this.onListChanged == null)
            return;

        if (_changedCallBackList.Contains(callback))
        {
            this.onListChanged -= callback;
            _changedCallBackList.Remove(callback);
        }
    }

    public void ListChanged(FriendOperationType t)
    {
        if (this.onListChanged != null)
        {
            onListChanged(t);
            Logger.Log("ListChanged:" + t.ToString());
        }
    }

    /// <summary>
    /// 获取列表对应的luaTable
    /// </summary>
    /// <param name="type">列表类型</param>
    /// <returns></returns>
    public LuaTable GetList(FriendOperationType type)
    {
        switch (type)
        {
            case FriendOperationType.FOT_QUERY:
                this.friend_list.Sort(SortFriendList);
                return this.friend_list.toLuaTable();
            case FriendOperationType.FOT_QUERY_APPLY:
                return this.apply_list.toLuaTable();
            case FriendOperationType.FOT_QUERY_BLACK:
                return this.black_list.toLuaTable();
            case FriendOperationType.FOT_QUERY_GIFT:
                return this.gift_list.toLuaTable();
            default:
                return null;
        }
    }

    public int GetListCount(FriendOperationType type)
    {
        switch (type)
        {
            case FriendOperationType.FOT_QUERY:
                return friend_list.Count;
            case FriendOperationType.FOT_QUERY_APPLY:
                return apply_list.Count;
            case FriendOperationType.FOT_QUERY_BLACK:
                return black_list.Count;
            case FriendOperationType.FOT_QUERY_GIFT:
                return gift_list.Count;
            default:
                return 0;
        }
    }

    /// <summary>
    /// 判断是否为好友
    /// </summary>
    /// <param name="acc_id">玩家ID</param>
    /// <returns></returns>
    public bool IsFriend(uint acc_id)
    {
        foreach (var fri in friend_list)
        {
            if (fri.acc_id == acc_id)
                return true;
        }

        return false;
    }

    /// <summary>
    /// 1、在线状态
    /// 2、友好度高的排列靠前
    /// 3、友好度一致时队伍ID由低到高排列
    /// --玩家状态
    //PlayerState = 
    //{
    //    NONE		= 0, --NONE
    //    NORMAL		= 1, --正常
    //    MATCH		= 2, --比赛匹配中
    //    GAME		= 3, --比赛中
    //    LOGOUT		= 4, --登出
    //    OFFLINE		= 5, --离线
    //    ROOM		= 8, --房间内
    //    CREATEGAME	= 9, --创建比赛中
    //    RECHARGE	= 10, --充值
    //    READYGAME	= 11, --准备进比赛中
    //    SELECTROLE	= 12, --选择球员进行比赛中
    //}
    /// </summary>
    /// <param name="AF1"></param>
    /// <param name="AF2"></param>
    /// <returns></returns>
    private static int SortFriendList(FriendInfo AF1, FriendInfo AF2)
    {
        uint a1 = (AF1.online == 5) ? 0u : 1u;
        uint a2 = (AF2.online == 5) ? 0u : 1u;

        if (a1 > a2)
            return -1;
        else if (a1 < a2)
            return 1;

        if (AF1.shinwakan > AF2.shinwakan)
            return -1;
        else if (AF1.shinwakan < AF2.shinwakan)
            return 1;

        if (AF1.acc_id < AF2.acc_id)
            return -1;
        else if (AF1.acc_id > AF2.acc_id)
            return 1;

        return 0;
    }
}

