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

	private float 	m_curTime;
	private string 	m_log = "";

	private const int m_nBufferSize = 256 * 1024;
	private byte[] m_buffer = new byte[m_nBufferSize];
	private int m_totalBytesRead = 0;
	private int m_processed = 0;
	protected List<Pack> _recvPacks = new List<Pack>();

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
		m_client.ReceiveTimeout = enable ? m_iReceiveTimeout : 0;
		m_client.Client.ReceiveTimeout = enable ? m_iReceiveTimeout : 0;
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
            m_client.ReceiveTimeout = m_iReceiveTimeout;
			m_client.Client.ReceiveTimeout = m_iReceiveTimeout;
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

		if( m_client == null || !m_client.Connected )
			return;
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

		m_client.GetStream().Close();
		m_client.Close();
		NotifyAllListener(NetworkEvent.disconnected);
	}

	private List<Pack> _msgCache = new List<Pack>();
	virtual public void Update(float fdeltaTime)
	{
		if (m_type != Type.eVirtualServer && !m_client.Connected)
			m_serverDisconnectedFlag_Passive = true;
		m_curTime = Time.time;
		lock(m_log)
		{
			if( m_log.Length != 0 )
			{
				Logger.Log(m_log);
				m_log = "";
			}
		}

		_msgCache.Clear();

		lock( m_MsgQueue )
		{
			if (m_MsgQueue.Count != 0)
			{
				_msgCache.AddRange(m_MsgQueue);
				m_MsgQueue.Clear();
			}
		}

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
		}

		if (m_serverConnectFailedFlag)
		{
			NotifyAllListener(NetworkEvent.connectFail);
			m_serverConnectFailedFlag = false;
		}
    }

	private void TCPReadCallBack(IAsyncResult ar)
	{
		//lock(m_stream)
		{
			m_totalBytesRead = m_stream.EndRead(ar);
			m_profiler.RecvDataCount(m_totalBytesRead);
		}

		m_processed = 0;
		_recvPacks.Clear();

		if (m_totalBytesRead > 0)
		{
			int processed = 0;
			while ((processed = DecodeBuffer(m_buffer, ref _recvPacks)) > 0)
			{
				m_processed += processed;
				if( m_totalBytesRead == m_processed )
					break;
			}

			if (_recvPacks.Count != 0)
			{
                foreach (Pack pack in _recvPacks)
                {
                    var msgID = (fogs.proto.msg.MsgID)pack.MessageID;
                    //ErrorDisplay.Instance.HandleLog("Receive msg:" + msgID, "TCPReadCallBack", LogType.Log);
                }

				lock(m_MsgQueue)
				{
					if( _recvPacks.Count != 0 )
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
                    m_stream.BeginRead(m_buffer, 0, m_nBufferSize, new AsyncCallback(TCPReadCallBack), this);
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

			{
				_recvPacks.Clear();

				bool bRecv = _RecvPack(ref _recvPacks, m_stream);
				if (!bRecv && m_type != Type.eVirtualServer)
				{
					ErrorDisplay.Instance.HandleLog(m_type + " Client: connection is closed, can not read data.", "", LogType.Error);
                    m_serverDisconnectedFlag_Passive = true;
                    break;
				}
				
				if (_recvPacks.Count != 0)
				{
					lock( m_MsgQueue )
					{
						m_MsgQueue.AddRange(_recvPacks);
					}
				}
			}

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
					mCurPack.ParseHeader(mCurPack.headerBuffer);
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
					mCurPack.ParseHeader(mCurPack.headerBuffer);
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
			m_stream.Close();
			Logger.LogError("network conn exception: " + exp.Message);
			return 0;
		}

		return processed;
	}
	
	public void SendPack<T>(uint type, T content, MsgID msg_id) where T : ProtoBuf.IExtensible
	{
        //if (msg_id != MsgID.CheckFrameID)
        //    Logger.Log("SendPack, Msg:" + msg_id);
		//lock(m_stream)
		{
	        if (m_type != Type.eVirtualServer)
	        {
	            if (m_client == null)
	                return;
	            if (!m_client.Connected)
	            {
	                Logger.LogWarning("SendPack, Msg:" + msg_id + " The connection is closed.");
	                return;
	            }
	        }
	        else
	        {
	            if (m_stream == null)
				{
					Logger.Log("virtual client stream is null");
					return;
				}
	        }

	        try
	        {
				long packTime = System.DateTime.Now.Ticks;
	            using(MemoryStream msgBodyStream = new MemoryStream())
				{
		            ProtoBuf.Serializer.Serialize<T>(msgBodyStream, content);

		            msgBodyStream.Seek(0, SeekOrigin.Begin);

		            Pack header = new Pack();
		            header.Type = type;
		            header.MessageID = (uint)msg_id;
		            header.AccountID = GameSystem.Instance.AccountID;
		            header.Length = (uint)msgBodyStream.Length;

		            byte[] bodyByteArray = new byte[msgBodyStream.Length];
		            msgBodyStream.Read(bodyByteArray, 0, bodyByteArray.Length);// 设置当前流的位置为流的开始
		            header.buffer = bodyByteArray;
		            encrypt(header);

		            byte[] msgByteArray = new byte[16 + (int)msgBodyStream.Length];
		            Array.Copy(header.AssemblyHeader(), 0, msgByteArray, 0, 16);
		            Array.Copy(bodyByteArray, 0, msgByteArray, 16, (int)msgBodyStream.Length);

					if (!m_stream.CanWrite)
					{
						Logger.Log("SendPack, Msg:" + msg_id + " stream write failed."); 
						return;
					}
					if (m_type == Type.eVirtualServer)
						m_stream.Seek(0, SeekOrigin.End);

					m_stream.BeginWrite(msgByteArray, 0, msgByteArray.Length, new AsyncCallback(TCPSendCallBack), System.DateTime.Now.Ticks);
					m_stream.Flush();
                    //Logger.Log("Write and flush-------");
				}
			}
			catch (Exception e)
	        {
				if (m_type == Type.eVirtualServer)
				{
					m_stream.Seek(0, SeekOrigin.Begin);
					m_stream.SetLength(0);
				}
				Logger.LogError(m_type + " NetworkConn.SendPack, Msg:" + msg_id + " Error: " + e.Message);
	        }
		}
    }

	private void TCPSendCallBack(IAsyncResult ar)
	{
		//lock(m_stream)
		{
			//Logger.Log( "send interval : " + (System.DateTime.Now.Ticks - (long)ar.AsyncState) * 0.0001f );
			m_stream.EndWrite(ar);
            //ErrorDisplay.Instance.HandleLog("Send over.", "", LogType.Log);
		}
	}


    void _Connect(object recvState)
    {
        if (m_type != Type.eVirtualServer)
        {
            if (m_client == null)
                return;

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
                m_client.Connect(new IPEndPoint(ip_addr, port));
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
    //------加密 ---
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

	protected override bool _RecvPack (ref List<Pack> outPacks, Stream stream)
	{
		//lock(stream)
		{
			try
			{
				stream.Seek(0, SeekOrigin.Begin);
				while(true)
				{
					byte[] headerBuffer = new byte[Pack.HeaderLength];
					int read = stream.Read(headerBuffer, 0, Pack.HeaderLength);
					if(read == 0)
						break;
					Pack inPack = new Pack();
					inPack.ParseHeader(headerBuffer);
					inPack.buffer = new byte[inPack.Length];
					read = stream.Read(inPack.buffer, 0, inPack.buffer.Length);
					decrypt(inPack);

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