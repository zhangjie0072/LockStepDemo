using fogs.proto.msg;
using System.Collections.Generic;

public class ErrorMessage : Singleton<ErrorMessage>
{
    private Dictionary<ErrorID, string> _messages = new Dictionary<ErrorID, string>();

    public ErrorMessage()
    {
        _messages[ErrorID.NOT_ENOUGH_MONEY] = CommonFunction.GetConstString("NOT_ENOUGH_MONEY");
        _messages[ErrorID.NOT_ENOUGH_DIAMOND] = CommonFunction.GetConstString("NOT_ENOUGH_DIAMOND");
        _messages[ErrorID.NOT_ENOUGH_STUFF] = CommonFunction.GetConstString("NOT_ENOUGH_STUFF");
        _messages[ErrorID.SKILL_SLOT_LOCK] = CommonFunction.GetConstString("SKILL_SLOT_LOCK");
        _messages[ErrorID.SKILL_SLOT_UNLOCK] = CommonFunction.GetConstString("SKILL_SLOT_UNLOCK");
        _messages[ErrorID.SKILL_SLOT_FILLED] = CommonFunction.GetConstString("SKILL_SLOT_FILLED");
        _messages[ErrorID.SKILL_SLOT_UNFILLED] = CommonFunction.GetConstString("SKILL_SLOT_UNFILLED");
        _messages[ErrorID.SKILL_EQUIPED] = CommonFunction.GetConstString("SKILL_EQUIPED");
        _messages[ErrorID.SKILL_UNEQUIPED] = CommonFunction.GetConstString("SKILL_UNEQUIPED");
        _messages[ErrorID.ALREADY_MATCHED] = CommonFunction.GetConstString("PVP_ALREADY_MATCHED");
        _messages[ErrorID.NOT_ENOUGH_ROLES] = CommonFunction.GetConstString("PVP_NO_ENOUGH_ROLES");
        _messages[ErrorID.UNKNOW_ERROR] = CommonFunction.GetConstString("UNKNOW_ERROR");
        _messages[ErrorID.INVALID_OPERATION] = CommonFunction.GetConstString("INVALID_OPERATION");
        _messages[ErrorID.CANNOT_COMPOSITE_GOODS] = CommonFunction.GetConstString("CANNOT_COMPOSITE_GOODS");
        _messages[ErrorID.WORLD_CHANNEL_COOL] = CommonFunction.GetConstString("WORLD_CHANNEL_COOL");
        _messages[ErrorID.GOODS_REACH_MAX] = CommonFunction.GetConstString("REACH_MAX_GOODSNUM");
        _messages[ErrorID.REACH_MAX] = CommonFunction.GetConstString("REACH_MAX");

        //好友相关
        _messages[ErrorID.REACH_FRIENDS_MAX] = CommonFunction.GetConstString("REACH_FRIENDS_MAX");
        _messages[ErrorID.REACH_BLACK_LIST_MAX] = CommonFunction.GetConstString("REACH_BLACK_LIST_MAX");
        _messages[ErrorID.REACH_FRIEND_PRESENT_MAX] = CommonFunction.GetConstString("STR_FRIENDS_NOT_GIVE");
        _messages[ErrorID.REACH_FRIEND_GET_GIFT_MAX] = CommonFunction.GetConstString("STR_FRIENDS_NOT_GET");
        _messages[ErrorID.ALREADY_PRESEND_FRIEND] = CommonFunction.GetConstString("ALREADY_PRESEND_FRIEND");
        _messages[ErrorID.ALREADY_GET_GIFT_FRIEND] = CommonFunction.GetConstString("ALREADY_GET_GIFT_FRIEND");
        _messages[ErrorID.TARGET_NOT_FRIEND] = CommonFunction.GetConstString("TARGET_NOT_FRIEND");
        _messages[ErrorID.TARGET_NOT_BLACKLIST] = CommonFunction.GetConstString("TARGET_NOT_BLACKLIST");
        _messages[ErrorID.TARGET_IN_BLACKLIST] = CommonFunction.GetConstString("TARGET_IN_BLACKLIST");
        _messages[ErrorID.TARGET_IN_FRIENDS] = CommonFunction.GetConstString("TARGET_IN_FRIENDS");
        _messages[ErrorID.TARGET_IS_NOT_EXIST] = CommonFunction.GetConstString("TARGET_IS_NOT_EXIST");
        _messages[ErrorID.NOT_FRIEND_CAN_PRESENT] = CommonFunction.GetConstString("NOT_FRIEND_CAN_PRESENT");

    }

    public string GetMessage(ErrorID ID)
    {
        string message = string.Empty;
        _messages.TryGetValue(ID, out message);
        return message;
    }
}
