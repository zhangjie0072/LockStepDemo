using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System;



public class MsgHandler
{
    public delegate void Handle(Pack pack);

    protected string m_strId;
    private Dictionary<MsgID, Handle> m_dict;
	public static HashSet<MsgID> m_noLogMsg;

    public MsgHandler()
    {
        m_dict = new Dictionary<MsgID, Handle>();
		m_noLogMsg = new HashSet<MsgID>();
		m_noLogMsg.Add(MsgID.HeartbeatID);
		m_noLogMsg.Add(MsgID.TimeTracerID);
		m_noLogMsg.Add(MsgID.MoveID);
		m_noLogMsg.Add(MsgID.AttackID);
		m_noLogMsg.Add(MsgID.StandID);
		m_noLogMsg.Add(MsgID.InputID);
		m_noLogMsg.Add(MsgID.BackToBackID);
		m_noLogMsg.Add(MsgID.BackBlockID);
		m_noLogMsg.Add(MsgID.BackCompeteID);
		m_noLogMsg.Add(MsgID.GameMsgID);
		m_noLogMsg.Add(MsgID.ClientInputID);
		m_noLogMsg.Add(MsgID.PlayFrameID);
		m_noLogMsg.Add(MsgID.CheckFrameID);
    }

    public void RegisterHandler(MsgID messageId, Handle handler)
    {
        if (!m_dict.ContainsKey(messageId))
            m_dict[messageId] = handler;
        else
            m_dict[messageId] += handler;
    }

    public void UnregisterHandler(MsgID messageId, Handle handler)
    {
        if (m_dict.ContainsKey(messageId))
        {
            m_dict[messageId] -= handler;
        }
        else
        {
            Debug.LogError("Unable to find handler : " + messageId);
        }
    }

	public virtual void Update()
	{
	}

    public void HandleMsg(Pack pack)
    {
        MsgID msgID = (MsgID)pack.MessageID;
        if (!m_noLogMsg.Contains(msgID))
        {
            Debug.Log("-------HandleMsg with MessageID: " + msgID);
        }

        Handle handler = null;
        if (!m_dict.TryGetValue(msgID, out handler))
        {
            Debug.Log("Error -- Can't find message handler with MessageID: " + msgID);
            return;
        }

        if (handler != null)
        {
            handler(pack);
        }
    }
}
