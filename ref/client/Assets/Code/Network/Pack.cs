using System;
using System.IO;
using UnityEngine;
using fogs.proto.msg;

public class Pack
{
    public static int HeaderLength = 16;

    public uint Type { get; private set; }
    public uint MessageID { get; private set; }
    public uint Length { get; private set; }
    public uint AccountID { get; private set; }

	public byte[] headerBuffer = new byte[HeaderLength];
	public int curHeaderSize = 0;

    public byte[] buffer;
    public int curRecSize = 0;

    public Pack() { }

    public Pack(uint msgType, uint msgID, uint msgLen)
    {
        Type = msgType;
        MessageID = msgID;
        Length = msgLen;
        AccountID = MainPlayer.Instance.AccountID;
        AssemblyHeader();
    }

    public void ParseHeader()
    {
        Type = BitConverter.ToUInt32(headerBuffer, 0);
        MessageID = BitConverter.ToUInt32(headerBuffer, 4);
        Length = BitConverter.ToUInt32(headerBuffer, 8);
        AccountID = BitConverter.ToUInt32(headerBuffer, 12);
    }

    void AssemblyHeader()
    {
        byte[] _type = BitConverter.GetBytes(Type);
        Array.Copy(_type, 0, headerBuffer, 0, 4);

        byte[] _msgID = BitConverter.GetBytes(MessageID);
        Array.Copy(_msgID, 0, headerBuffer, 4, 4);

        byte[] _length = BitConverter.GetBytes(Length);
        Array.Copy(_length, 0, headerBuffer, 8, 4);

        byte[] _accountID = BitConverter.GetBytes(AccountID);
        Array.Copy(_accountID, 0, headerBuffer, 12, 4);
    }

    public byte[] Assembly()
    {
        byte[] msgByteArray = new byte[HeaderLength + buffer.Length];
        Array.Copy(headerBuffer, 0, msgByteArray, 0, HeaderLength);
        Array.Copy(buffer, 0, msgByteArray, HeaderLength, buffer.Length);
        return msgByteArray;
    }

    public void setEncrypted()
    {
        Type |= 0x80000000; //最高位设为1
    }
    public void setDecrypted()
    {
        Type &= 0x7FFFFFFF; //最高位设为0
    }
    public bool isEncrypted()
    {
        return (Type & 0x80000000) == 0 ? false : true;
    }
};
