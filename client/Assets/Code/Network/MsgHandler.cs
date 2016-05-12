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
	private HashSet<MsgID> m_noLogMsg;

    public MsgHandler()
    {
        m_dict = new Dictionary<MsgID, Handle>();
		m_noLogMsg = new HashSet<MsgID>();
        m_noLogMsg.Add(MsgID.FrameInfoID);
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
            Logger.LogError("Unable to find handler : " + messageId);
        }
    }

	public virtual void Update()
	{
	}

    public virtual void HandleMsg(Pack pack)
    {
		MsgID msgID = (MsgID)pack.MessageID;
		if (!m_noLogMsg.Contains(msgID))
			Logger.Log("-------HandleMsg with MessageID: " + msgID);

        Handle handler = null;
        if (!m_dict.TryGetValue(msgID, out handler))
        {
            Logger.Log("Error -- Can't find message handler with MessageID: " + msgID);
            return;
        }

        if (handler != null)
            handler(pack);
    }
}
