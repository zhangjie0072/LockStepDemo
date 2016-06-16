using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using UnityEngine;

using ProtoBuf;
using fogs.proto.msg;

public class NetworkConn
{
    public enum NetworkEvent
    {
        connected,
        connectFail,
        disconnected
    }

    public enum Type
    {
        eLoginServer,
        ePlatformServer,
        eGameServer,
        eVirtualServer
    }
    public Type m_type { get; private set; }
    enum EncryptType
    {
        NOT_ENCRYPT = 0,
        WHOLE_ENCRYPT,
        HEAD_ENCRYPT,
        TAIL_ENCRYPT
    };
	private EncryptType m_encryptType;
	private uint m_maxEncryptLen;
	private uint m_secretKey;
	protected MsgHandler m_handler { get; private set; }

	public string 	m_ip;
	public int 		m_port;

    public interface Listener
    {
        void OnEvent(NetworkEvent nEvent, NetworkConn sender);
    }

    private TcpClient 	m_client;

	protected  Stream	m_stream;

    private int m_iReceiveTimeout;

    protected Thread m_recvMsgThread;
	protected List<Pack> m_MsgQueue = new List<Pack>();	
    protected List<Listener> m_listeners = new List<Listener>();

	protected bool m_serverConnectedFlag = false;
	protected bool m_serverDisconnectedFlag_Initiative = false;
	protected bool m_serverDisconnectedFlag_Passive = false;
	protected bool m_serverConnectFailedFlag = false;

    protected string 	m_strName;
	protected List<Pack> _recvPacks = new List<Pack>();
	
	//private float 	m_curTime;
	private string 	m_log = "";
	private const int m_nBufferSize = 256 * 1024;
	private byte[] m_buffer = new byte[m_nBufferSize];
	private int m_totalBytesRead = 0;
	private int m_processed = 0;
	private float m_readTimeCounter = 0.0f;

	private bool _beginRead = false;

    private uint m_msgSN = 0; //消息序列号

	private bool m_beginRead{ get{ return _beginRead; } set{ _beginRead = value; }  }

	public NetworkProfiler	m_profiler;

    public NetworkConn(MsgHandler msgHandler, string serverName, Type serverType, int iReceiveTimeout)
    {
        m_encryptType = EncryptType.WHOLE_ENCRYPT;
        m_maxEncryptLen = 100;
        m_secretKey = 0xBCAF5432;
        m_strName = serverName;
        m_type = serverType;
        m_handler = msgHandler;
        m_iReceiveTimeout = iReceiveTimeout;
    }

	public void EnableTimeout(bool enable)
	{
		if( m_client == null || m_client.Client == null )
			return;

		Logger.Log(m_type + "Enable timeout " + enable);
		m_client.ReceiveTimeout = enable ? m_iReceiveTimeout*1000 : 0;
//		m_client.Client.ReceiveTimeout = enable ? m_iReceiveTimeout*1000 : 0;
	}

    virtual public void Connect(string ip, int port)
    {
		Logger.Log("Network.Connect " + m_strName);
		if( m_profiler == null )
			m_profiler = new NetworkProfiler(this);

        if (m_client == null)
        {
			Logger.Log("New TcpClient " + m_strName);
            m_client = new TcpClient();

			m_client.ReceiveTimeout = m_iReceiveTimeout*1000;
//			m_client.Client.ReceiveTimeout = m_iReceiveTimeout*1000;

			m_client.NoDelay = true;
			m_client.Client.NoDelay = true;

        }
        if (m_client.Connected)
        {
            Logger.Log("server: " + m_strName + " already connected.");
            return;
        }

		Logger.Log("Queue _Connect " + m_strName);
        if (!ThreadPool.QueueUserWorkItem(_Connect, new object[2] { ip, port }))
			Logger.LogError("Queue _Connect failed, " + m_strName);
    }

	public bool IsConnected()
	{
		return m_client.Connected;
	}

    public bool IsSocketConnected(Socket socket = null)
    {
		if (socket == null && m_client != null)
			socket = m_client.Client;
        bool part1 = socket.Poll(1000, SelectMode.SelectRead);
        bool part2 = (socket.Available == 0);
        if (part1 && part2)
            return false;
        else
            return true;
    }

	virtual public void Close()
	{
		Logger.Log("Close conn:" + m_strName);
		if( m_stream == null )
			return;

		if( m_client == null )
			return;

		m_readTimeCounter = 0.0f;
		m_beginRead = false;
		m_profiler = null;
		m_serverDisconnectedFlag_Initiative = true;

		if (IsConnected() && IsSocketConnected())
		{
			try
			{
				m_client.Client.Shutdown(SocketShutdown.Both);
			}
			catch (SocketException ex)
			{
				Logger.LogWarning("Shutdown socket, " + ex.Message);
			}
		}


		if( m_type == Type.eVirtualServer )
		{
			if( m_recvMsgThread != null )
				m_recvMsgThread.Join();
		}

		//m_client.GetStream().Close();
		m_client.Close();
		NotifyAllListener(NetworkEvent.disconnected);
	}

	private List<Pack> _msgCache = new List<Pack>();
    virtual public void Update(float fdeltaTime)
    {
        if (m_beginRead)
            m_readTimeCounter += Time.deltaTime;
        //登录服务器没有心跳机制，取消超时断线
        if (m_beginRead && m_readTimeCounter > m_iReceiveTimeout && m_type != Type.eLoginServer && m_type != Type.eVirtualServer)
        {
            Logger.Log("Disconnected: can not receive heartbeat data within: " + m_iReceiveTimeout + " sec.");
            m_serverDisconnectedFlag_Passive = true;
        }

        //m_curTime = Time.time;
        //lock (m_log)
        //{
        //    if (m_log.Length != 0)
        //    {
        //        Logger.Log(m_log);
        //        m_log = "";
        //    }
        //}

        //将网络消息缓存池中的数据放入待处理队列
        _msgCache.Clear();
        lock (m_MsgQueue)
        {
            if (m_MsgQueue.Count != 0)
            {
                _msgCache.AddRange(m_MsgQueue);
                m_MsgQueue.Clear();
            }
        }

        //没有看明白是干什么使用的，貌似只是个日志输出
        //if (m_type == Type.ePlatformServer || m_type == Type.eLoginServer)
        //{
        //    int countWithOutTimeTracer = _msgCache.FindAll(msg =>
        //    {
        //        MsgID msgID = (MsgID)msg.MessageID;
        //        return msgID != MsgID.TimeTracerID && msgID != MsgID.HeartbeatID;
        //    }).Count;
        //    if (countWithOutTimeTracer > 0)
        //    {
        //        Logger.Log("msg cnt: " + countWithOutTimeTracer);
        //        foreach (Pack pack in _msgCache)
        //        {
        //            Logger.Log("Msg:" + (MsgID)pack.MessageID);
        //        }
        //    }
        //}

		//if( (System.DateTime.Now.Ticks - now) * 0.0001f > 10.0f ) 
		//	Logger.LogError( "out lock .update : " + (System.DateTime.Now.Ticks - now) * 0.0001f );


		foreach( Pack pack in _msgCache )
			m_handler.HandleMsg(pack);
		
		if( m_profiler != null )
			m_profiler.FixedUpdate(fdeltaTime);
		
		if( m_handler != null )
			m_handler.Update();

        if (m_serverConnectedFlag)
        {
			if( m_type != Type.eVirtualServer )
			{
				m_stream = m_client.GetStream();
				AsyncRead();
				//m_recvMsgThread = new Thread(AsyncRead);
				//m_recvMsgThread.Name = m_strName + "_thread";
				//m_recvMsgThread.Start();
			}
			else
			{
				m_stream = new MemoryStream();
            	m_recvMsgThread = new Thread(_Recv);
				m_recvMsgThread.Name = m_strName + "_thread";
            	m_recvMsgThread.Start();
			}

			NotifyAllListener(NetworkEvent.connected);
            m_serverConnectedFlag = false;
			m_serverDisconnectedFlag_Initiative = false;
			m_serverDisconnectedFlag_Passive = false;

			if( m_profiler != null )
				m_profiler.Init();
        }

		if( m_serverDisconnectedFlag_Passive )
		{
			NotifyAllListener(NetworkEvent.disconnected);
			m_serverDisconnectedFlag_Passive = false;
			m_readTimeCounter = 0.0f;
			m_beginRead = false;
		}

		if (m_serverConnectFailedFlag)
		{
			NotifyAllListener(NetworkEvent.connectFail);
			m_serverConnectFailedFlag = false;
			m_readTimeCounter = 0.0f;
			m_beginRead = false;
		}
    }

	private void TCPReadCallBack(IAsyncResult ar)
	{
		//lock(m_stream)
		{
			m_totalBytesRead = m_stream.EndRead(ar);
			m_profiler.RecvDataCount(m_totalBytesRead);
		}

		m_readTimeCounter = 0.0f;

		m_processed = 0;
		_recvPacks.Clear();

		if (m_totalBytesRead > 0)
		{

			m_beginRead = false;
			
			int processed = 0;
			while ((processed = DecodeBuffer(m_buffer, ref _recvPacks)) > 0)
			{
				m_processed += processed;
				if( m_totalBytesRead == m_processed )
					break;
			}

			if (_recvPacks.Count != 0)
			{
				foreach(Pack pack in _recvPacks)
				{
					long now = System.DateTime.Now.Ticks;
					MsgID msgID = (MsgID)pack.MessageID;
					if( msgID == MsgID.GameMsgID )
					{
						GameMsg msg = Serializer.Deserialize<GameMsg>(new MemoryStream(pack.buffer));

						List<Player> players = GameSystem.Instance.mClient.mPlayerManager.m_Players;
						Player sender = players.Find( (Player player)=>{ return player.m_roomPosId == msg.senderID; } );
						
						GameMatch match = GameSystem.Instance.mClient.mCurMatch;
						if(match == null)
							break;
						
						if( sender == null && match is GameMatch_PVP )
						{
							Logger.LogError("Can not find sender: " + msg.senderID + " for command: " + msg.eState);
							break;
						}
						{
							/*
							SimulateCommand cmd = match.GetSmcCommandByGameMsg(sender, msg);
							if( cmd != null && sender.m_smcManager != null )
							{
								if( cmd is SMC_BackCompete || cmd is SMC_BackBlock )
									Logger.Log("SMC: " + cmd + " cnt: " + _recvPacks.Count );
								NetworkManager nm = GameSystem.Instance.mNetworkManager;
								double dConsumeTime = (DateTime.Now.Ticks - msg.curTime) * 0.0001;
								if( dConsumeTime > 50.0f )
								{
									lock(m_log)
									{
										m_log = "Command: " + cmd.m_state + " time consume: " + string.Format("{0:f4}", dConsumeTime);
									}
								}
							}
							*/
						}
					}
                    //心跳包的立即回发
                    if (msgID == MsgID.HeartbeatID)
                    {
                        Heartbeat data = Serializer.Deserialize<Heartbeat>(new MemoryStream(pack.buffer));
                        GameSystem.mTime = (long)data.server_time + 1;
                        //检查是否加速外挂作弊
                        if (CheatingDeath.Instance.mAntiSpeedUp.m_beginWatch)
                        {
                            //Debug.LogWarning(string.Format("local=>{0:f4} server=>{1:f4} diff=>{2:f4} ",
                            //    CheatingDeath.Instance.mAntiSpeedUp.m_clientTime, data.server_time,
                            //     CheatingDeath.Instance.mAntiSpeedUp.m_clientTime - data.server_time));
                            data.server_time = CheatingDeath.Instance.mAntiSpeedUp.m_clientTime;
                        }
                        else
                        {
                            CheatingDeath.Instance.mAntiSpeedUp.BeginWatch(data.server_time);
                        }
                        PlatNetwork.Instance.SendHeartbeatMsg(data);
                       
                        //GameSystem.Instance.mNetworkManager.m_platMsgHandler.HeartbeatHandle(pack);
                    }
                    if ( msgID == MsgID.GameBeginRespID )
					{
						GameBeginResp resp = Serializer.Deserialize<GameBeginResp>(new MemoryStream(pack.buffer));
						GameSystem.Instance.mNetworkManager.m_dServerTime = resp.cur_time;
						GameSystem.Instance.mNetworkManager.m_dLocalTime = DateTime.Now.Ticks * 0.0001;
					}
				}

                lock (m_MsgQueue)
                {
                    if (_recvPacks.Count != 0)
                        m_MsgQueue.AddRange(_recvPacks);
                }
			}

			AsyncRead();
		}
		else
		{
			if (m_type != Type.eVirtualServer)
			{
				ErrorDisplay.Instance.HandleLog(m_type + " Client: connection is closed, can not read data.", "", LogType.Error);
				m_serverDisconnectedFlag_Passive = true;
				ErrorDisplay.Instance.HandleLog(m_type + " App paused, skip this failure.", "", LogType.Log);
			}
		}
		
	}
	
	private void AsyncRead()
	{
		//lock(m_stream)
		{
			if (m_stream.CanRead)
			{
				try
				{
					//if( !GameSystem.Instance.appPaused )
					{
						m_readTimeCounter = 0.0f;
						m_beginRead = true;
						m_stream.BeginRead(m_buffer, 0, m_nBufferSize, new AsyncCallback(TCPReadCallBack), this);
					}
				} 
				catch (Exception e)
				{
					Logger.LogError("Network IO problem " + e.ToString());
				}
			}
		}
	}
	
	public void AddEventListener(Listener lsn)
	{
		if (m_listeners.Contains(lsn))
			return;
		m_listeners.Add(lsn);
	}
	
	public void RemoveEventListener(Listener lsn)
	{
		if (!m_listeners.Contains(lsn))
			return;
		m_listeners.Remove(lsn);
	}
	
	public void NotifyAllListener(NetworkEvent nEvent)
	{
		foreach (Listener lsn in m_listeners)
		{
			lsn.OnEvent(nEvent, this);
		}
	}
	
	virtual protected void _Recv(object recvState)
    {
        while (true)
        {
			if (m_serverDisconnectedFlag_Initiative || m_serverDisconnectedFlag_Passive)
			{
				ErrorDisplay.Instance.HandleLog(m_type + " Flg_Initiative:" + m_serverDisconnectedFlag_Initiative + " Flag_Passivie:" + m_serverDisconnectedFlag_Passive, "", LogType.Log);
				break;
			}

			if (!GameSystem.Instance.appPaused)
			{
				_recvPacks.Clear();

				bool bRecv = _RecvPack(ref _recvPacks, m_stream);
				if (!bRecv && m_type != Type.eVirtualServer)
				{
					ErrorDisplay.Instance.HandleLog(m_type + " Client: connection is closed, can not read data.", "", LogType.Error);
					if (!GameSystem.Instance.appPaused)
					{
						m_serverDisconnectedFlag_Passive = true;
						break;
					}
					else
						ErrorDisplay.Instance.HandleLog(m_type + " App paused, skip this failure.", "", LogType.Log);
				}
				
				if (_recvPacks.Count != 0)
				{
					//Logger.Log("Pack to handle: " + _recvPacks.Count);

					foreach(Pack pack in _recvPacks)
					{
						long now = System.DateTime.Now.Ticks;
						MsgID msgID = (MsgID)pack.MessageID;
						if( msgID == MsgID.GameMsgID )
						{
							GameMsg msg = Serializer.Deserialize<GameMsg>(new MemoryStream(pack.buffer));
							//Logger.Log( "deserialize time  : " + (System.DateTime.Now.Ticks - now) * 0.0001f );

							List<Player> players = GameSystem.Instance.mClient.mPlayerManager.m_Players;
							Player sender = players.Find( (Player player)=>{ return player.m_roomPosId == msg.senderID; } );
							//Logger.Log( "find time  : " + (System.DateTime.Now.Ticks - now) * 0.0001f );

							GameMatch match = GameSystem.Instance.mClient.mCurMatch;
							if(match == null)
								break;
							
							if( sender == null && match is GameMatch_PVP )
							{
								Logger.LogError("Can not find sender: " + msg.senderID + " for command: " + msg.eState);
								break;
							}
							{
								SimulateCommand cmd = match.GetSmcCommandByGameMsg(sender, msg);
								//Logger.Log( "SimulateCommand time  : " + (System.DateTime.Now.Ticks - now) * 0.0001f );

								if( cmd != null && sender.m_smcManager != null && !sender.m_bSimulator )
								{
									NetworkManager nm = GameSystem.Instance.mNetworkManager;
									double dConsumeTime = nm.m_dServerTime + (DateTime.Now.Ticks * 0.0001 - nm.m_dLocalTime) - msg.curTime;
									//Logger.Log( "Command: " + cmd.m_state + " time consume: " + string.Format("{0:f4}", dConsumeTime) );
									if( dConsumeTime > 20.0 )
										m_log = "Command: " + cmd.m_state + " time consume: " + string.Format("{0:f4}", dConsumeTime);
								}
							}
						}

						if( msgID == MsgID.GameBeginRespID )
						{
							GameBeginResp resp = Serializer.Deserialize<GameBeginResp>(new MemoryStream(pack.buffer));
							GameSystem.Instance.mNetworkManager.m_dServerTime = resp.cur_time;
							GameSystem.Instance.mNetworkManager.m_dLocalTime = DateTime.Now.Ticks * 0.0001;
						}
					}

                    lock (m_MsgQueue)
                    {
                        m_MsgQueue.AddRange(_recvPacks);
                    }
				}
			}
			else
				ErrorDisplay.Instance.HandleLog(m_type + " App paused, skip recv pack.", "", LogType.Log);

			if( m_type == Type.eLoginServer || m_type == Type.ePlatformServer )
				Thread.Sleep(20);
			else
				Thread.Sleep(1);
        }
    }

    protected Pack mCurPack = null;

	protected virtual bool _RecvPack(ref List<Pack> outPacks, Stream stream)
	{
		m_totalBytesRead = stream.Read(m_buffer, 0, m_nBufferSize);
		m_processed = 0;

		//Logger.Log("numberOfBytesRead=" + m_totalBytesRead);
		if (m_totalBytesRead > 0)
		{
			int processed = 0;
			while ((processed = DecodeBuffer(m_buffer, ref outPacks)) > 0)
			{
				m_processed += processed;
				if( m_totalBytesRead == m_processed )
					break;
			}
		}
		return true;
	}

	private int DecodeBuffer(byte[] buffer, ref List<Pack> outPacks)
	{
		Pack pack = null;
		int processed = 0;
		
		try
		{
			int restSizeInBuffer = m_totalBytesRead - m_processed;
			if (mCurPack == null)
			{
				mCurPack = new Pack();

				if( restSizeInBuffer >= Pack.HeaderLength )
				{
					Buffer.BlockCopy(buffer, m_processed, mCurPack.headerBuffer, 0, Pack.HeaderLength);
					mCurPack.ParseHeader();
					mCurPack.buffer = new byte[mCurPack.Length];
					mCurPack.curHeaderSize = Pack.HeaderLength;

					processed += Pack.HeaderLength;
				}
				else
				{
					Buffer.BlockCopy(buffer, m_processed, mCurPack.headerBuffer, 0, restSizeInBuffer);
					mCurPack.curHeaderSize = restSizeInBuffer;

					processed += restSizeInBuffer;

					Logger.Log("header to combine");

					return processed;
				}
			}

			if( mCurPack.curHeaderSize < Pack.HeaderLength )
			{
				int restHeaderToReceive = Pack.HeaderLength - mCurPack.curHeaderSize;
				if( restSizeInBuffer >= restHeaderToReceive )
				{
					Buffer.BlockCopy(buffer, m_processed, mCurPack.headerBuffer, mCurPack.curHeaderSize, restHeaderToReceive);
					mCurPack.ParseHeader();
					mCurPack.buffer = new byte[mCurPack.Length];
					mCurPack.curHeaderSize = Pack.HeaderLength;

					processed += restHeaderToReceive;
				}
				else
				{
					Buffer.BlockCopy(buffer, m_processed, mCurPack.headerBuffer, mCurPack.curHeaderSize, restSizeInBuffer);
					mCurPack.curHeaderSize += restSizeInBuffer;

					processed += restSizeInBuffer;

					Logger.Log("header to combine");

					return processed;
				}
			}

			int restSizeToReceive = (int)mCurPack.Length - mCurPack.curRecSize;
			restSizeInBuffer -= processed;
			if( restSizeInBuffer < 0 || restSizeToReceive < 0 )
			{
				Logger.LogError("restSizeInBuffer: " + restSizeInBuffer + "restSizeToReceive: " + restSizeToReceive);
				return 0;
			}


			if( restSizeToReceive <= restSizeInBuffer )
			{
				Buffer.BlockCopy(buffer, m_processed + processed, mCurPack.buffer, mCurPack.curRecSize, restSizeToReceive);
				pack = mCurPack;
				mCurPack = null;
				decrypt(pack);

#if UNITY_EDITOR
                MsgID msg_id = (MsgID)pack.MessageID;
                if (!MsgHandler.m_noLogMsg.Contains(msg_id))
                    Logger.Log("-------_RecvPack with MessageID22: " + msg_id.ToString());
#endif

                outPacks.Add(pack);

				processed += restSizeToReceive;
			}
			else
			{
				Buffer.BlockCopy(buffer, m_processed + processed, mCurPack.buffer, mCurPack.curRecSize, restSizeInBuffer);
				mCurPack.curRecSize += restSizeInBuffer;

				processed += restSizeInBuffer;

				Logger.Log("body to combine");
			}
		}
		catch (Exception exp)
		{
			pack = null;
			mCurPack = null;
			Logger.LogError("network conn exception: " + exp.Message);
			return 0;
		}

		return processed;
	}

    bool CanSend()
    {
        if (m_type != Type.eVirtualServer)
        {
            if (m_client == null)
            {
                Logger.LogWarning("The client is null.");
                return false;
            }
            if (!m_client.Connected)
            {
                Logger.LogWarning("The connection is closed.");
                return false;
            }
        }
        else
        {
            if (m_stream == null)
            {
                Logger.LogWarning("Virtual client stream is null");
                return false;
            }
        }

        if (!m_stream.CanWrite)
        {
            Logger.LogWarning("Stream can not write.");
            return false;
        }
        return true;
    }
	
	public void SendPack<T>(uint type, T content, MsgID msg_id) where T : ProtoBuf.IExtensible
	{
#if UNITY_EDITOR
        if(!MsgHandler.m_noLogMsg.Contains(msg_id))
            Logger.Log("-------SendMsg with MessageID: " + msg_id.ToString());
#endif

        if (!CanSend())
        {
            Logger.LogError("SendPack failed, Msg: " + msg_id);
            return;
        }

        Pack pack = SerializeToPack(type, content, msg_id);

        //encrypt(pack);

        SendPack(pack);
    }

	Pack SerializeToPack<T>(uint type, T content, MsgID msg_id) where T : ProtoBuf.IExtensible
    {
        using(MemoryStream msgBodyStream = new MemoryStream())
        {
            ProtoBuf.Serializer.Serialize<T>(msgBodyStream, content);

            msgBodyStream.Seek(0, SeekOrigin.Begin);

            Pack pack = GenerateHeader(type, (uint)msg_id, (uint)msgBodyStream.Length, m_type);

            pack.buffer = new byte[msgBodyStream.Length];
            msgBodyStream.Read(pack.buffer, 0, pack.buffer.Length);// ���õ�ǰ����λ��Ϊ���Ŀ�ʼ

            return pack;
        }
    }

    protected virtual void SendPack(Pack pack)
    {
        byte[] msgByteArray = pack.Assembly();

        try
        {
            m_stream.BeginWrite(msgByteArray, 0, msgByteArray.Length, 
                //new AsyncCallback(TCPSendCallBack), pack.MessageID);
                new AsyncCallback(TCPSendCallBack), System.DateTime.Now.Ticks);
            m_stream.Flush();
        }
        catch (Exception e)
        {
            Logger.LogError(m_type + " NetworkConn.SendPack, Msg:" + pack.MessageID + " Error: " + e.Message);
        }
    }

	protected void TCPSendCallBack(IAsyncResult ar)
	{
		//lock(m_stream)
		{
			//Logger.Log( "send interval : " + (System.DateTime.Now.Ticks - (long)ar.AsyncState) * 0.0001f );
			m_stream.EndWrite(ar);
		}
	}


    public void SendMsgFromLua(uint msgID, LuaStringBuffer msgBody)
    {
#if UNITY_EDITOR
        var msg_id = (MsgID)msgID;
        if (!MsgHandler.m_noLogMsg.Contains(msg_id))
            Logger.Log("-------SendMsg with MessageID: " + msg_id.ToString());
#endif

        NetworkStream ns = m_client.GetStream();
        if (!ns.CanWrite)
            return;

        Pack header = GenerateHeader(0, msgID, (uint)msgBody.buffer.Length);

        byte[] msgByteArray = new byte[Pack.HeaderLength + (int)msgBody.buffer.Length];
        Array.Copy(header.headerBuffer, 0, msgByteArray, 0, Pack.HeaderLength);
        Array.Copy(msgBody.buffer, 0, msgByteArray, Pack.HeaderLength, (int)msgBody.buffer.Length);

        ns.Write(msgByteArray, 0, msgByteArray.Length);
    }

    void _Connect(object recvState)
    {
        if (m_type != Type.eVirtualServer)
        {
            if (m_client == null)
                return;

			m_beginRead = false;
			m_readTimeCounter = 0.0f;

            object[] array = recvState as object[];
            string ip = Convert.ToString(array[0]);
            int port = Convert.ToInt32(array[1]);

            ErrorDisplay.Instance.HandleLog("CLIENT: " + "begin to connect to server: " + m_strName, "", LogType.Log);
            IPAddress ip_addr;
            try
            {
                ip_addr = IPAddress.Parse(ip);
            }
            catch (SystemException)
            {
                try
                {
                    IPHostEntry entry = Dns.GetHostEntry(ip);
                    string aaa = entry.HostName;
                    ip_addr = entry.AddressList[0];
                }
                catch (SystemException e)
                {
					m_serverConnectFailedFlag = true;

                    Close();
                    ErrorDisplay.Instance.HandleLog(e.ToString(), "", LogType.Error);
                    return;
                }
            }

            try
            {
                //m_client.Connect(new IPEndPoint(ip_addr,port));
                //m_client.Connect(ip_addr, port);
                m_client.Connect(ip, port);
                ErrorDisplay.Instance.HandleLog("CLIENT: " + "connect to ip: " + ip + " port: " + port + " successfully.", "", LogType.Log);
                m_serverConnectedFlag = true;

				m_ip = ip;
				m_port = port;
            }
            catch (System.Exception e)
            {
				m_serverConnectFailedFlag = true;
                ErrorDisplay.Instance.HandleLog(e.ToString(), "", LogType.Error);
            }
        }
        else
            m_serverConnectedFlag = true;
    }
    //------���� ---
    //
    void setEncrypt(EncryptType type, uint maxLen, uint key)
    {
        m_encryptType = type;
        m_maxEncryptLen = maxLen;
        m_secretKey = key;
    }

    void disableEncrypt()
    {
        m_encryptType = EncryptType.NOT_ENCRYPT;
    }

	public void encrypt(Pack pack)
    {
        //Logger.Log("<<<encrypt, " + pack.buffer);

        if (enDecrypt(pack.buffer, pack.Length))
        {
            pack.setEncrypted();
        }
    }

    public void decrypt(Pack pack)
    {
        if (pack.isEncrypted())
        {
            if (enDecrypt(pack.buffer, pack.Length))
            {
                pack.setDecrypted();
            }
        }
    }

	public bool enDecrypt(byte[] data, uint len)
    {
        if (m_encryptType == EncryptType.NOT_ENCRYPT)
        {
            return false;
        }

        byte[] encryptedData = data;
        if (m_encryptType == EncryptType.HEAD_ENCRYPT)
        {
            len = m_maxEncryptLen >= len ? len : m_maxEncryptLen;
        }
        else if (m_encryptType == EncryptType.TAIL_ENCRYPT)
        {
            //if(len > m_maxEncryptLen)
            //{
            //    encryptedData = data + (len - m_maxEncryptLen);
            //}
            len = m_maxEncryptLen >= len ? len : m_maxEncryptLen;
        }

        int intLen = (int)len / 4;
        for (int i = 0; i < intLen; ++i)
        {
            uint value = BitConverter.ToUInt32(encryptedData, i * 4);
            value = value ^ m_secretKey;
            Array.Copy(BitConverter.GetBytes(value), 0, encryptedData, i * 4, 4);
        }

        for (int i = intLen * 4; i < encryptedData.Length; ++i)
        {
            uint value = Convert.ToUInt32(encryptedData[i]);
            value = value ^ m_secretKey;
            Array.Copy(BitConverter.GetBytes(value), 0, encryptedData, i, 1);
        }

        return true;
    }
    
    //产生消息序列号
    private Pack GenerateHeader(uint msgType, uint msgID, uint msgLen, NetworkConn.Type serverType = NetworkConn.Type.ePlatformServer)
    {
        msgType = (msgType & 0xC000000F) | (m_msgSN << 4);
        if (serverType != NetworkConn.Type.eVirtualServer && msgID != (uint)(MsgID.HeartbeatID))
        {
            m_msgSN++;
        }
        Pack header = new Pack(msgType, (uint)msgID, msgLen);
        return header;
    }
}

public class VirtualNetworkConn
    : NetworkConn
{
	private		int	m_curBufferPos = 0; 


    public VirtualNetworkConn(MsgHandler msgHandler)
        : base(msgHandler, "virtual", Type.eVirtualServer, 0)
    {
    }

    public override void Connect(string ip, int port)
    {
        m_serverConnectedFlag = true;
    }

	public override void Close()
	{
		m_serverDisconnectedFlag_Initiative = true;
		m_serverDisconnectedFlag_Passive = false;
		m_stream.Close();
		m_profiler = null;
		NotifyAllListener(NetworkEvent.disconnected);
	}

    protected override void SendPack(Pack pack)
    {
        byte[] msgByteArray = pack.Assembly();

        lock(m_stream)
        {
	        try
	        {
                m_stream.Seek(0, SeekOrigin.End);

                m_stream.BeginWrite(msgByteArray, 0, msgByteArray.Length, 
                    //new AsyncCallback(TCPSendCallBack), pack.MessageID);
                    new AsyncCallback(TCPSendCallBack), System.DateTime.Now.Ticks);
                m_stream.Flush();
			}
			catch (Exception e)
	        {
                m_stream.Seek(0, SeekOrigin.Begin);
                m_stream.SetLength(0);
				Logger.LogError(m_type + " NetworkConn.SendPack, Msg:" + pack.MessageID + " Error: " + e.Message);
	        }
		}
    }

	protected override bool _RecvPack (ref List<Pack> outPacks, Stream stream)
	{
		lock(stream)
		{
			try
			{
				stream.Seek(0, SeekOrigin.Begin);
				while(true)
				{
					Pack inPack = new Pack();
					inPack.headerBuffer = new byte[Pack.HeaderLength];
					int read = stream.Read(inPack.headerBuffer, 0, Pack.HeaderLength);
					if(read == 0)
						break;
					inPack.ParseHeader();
					inPack.buffer = new byte[inPack.Length];
					read = stream.Read(inPack.buffer, 0, inPack.buffer.Length);
					decrypt(inPack);
#if UNITY_EDITOR
                    MsgID msg_id = (MsgID)inPack.MessageID;
                    if (!MsgHandler.m_noLogMsg.Contains(msg_id))
                        Logger.Log("-------_RecvPack with MessageID11: " + msg_id.ToString());
#endif
                    outPacks.Add(inPack);
				}
				stream.Seek(0, SeekOrigin.Begin);
				stream.SetLength(0);
			}
			catch (Exception exp)
			{
				m_curBufferPos = 0;
				Logger.LogError("network conn exception: " + exp.Message);
				stream.Seek(0, SeekOrigin.Begin);
				stream.SetLength(0);

				return false;
			}
		}
		//Logger.Log("outPacks count " + outPacks.Count);

		return outPacks.Count != 0;
	}
}

public class VirtualNetworkConnSimple : NetworkConn
{
    public VirtualNetworkConnSimple(MsgHandler msgHandler)
        : base(msgHandler, "simple virtual", Type.eVirtualServer, 0)
    {
    }

    public override void Connect(string ip, int port)
    {
        m_serverConnectedFlag = true;
    }

	public override void Close()
	{
		m_serverDisconnectedFlag_Initiative = true;
		m_serverDisconnectedFlag_Passive = false;
		m_stream.Close();
		m_profiler = null;
		NotifyAllListener(NetworkEvent.disconnected);
	}

    protected override void SendPack(Pack pack)
    {
        lock (m_MsgQueue)
        {
            m_MsgQueue.Add(pack);
        }
    }

	protected override bool _RecvPack (ref List<Pack> outPacks, Stream stream)
	{
		return true;
	}
}