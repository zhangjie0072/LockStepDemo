using System;
using System.IO;
using UnityEngine;

public class Pack
{
    public static int HeaderLength = 16;

    public uint Type;
    public uint MessageID;
    public uint Length;
    public uint AccountID;

	public byte[] headerBuffer = new byte[HeaderLength];
	public int curHeaderSize = 0;

    public byte[] buffer;
    public int curRecSize = 0;

    public Pack()
    {
    }

    public void ParseHeader(byte[] headerBuffer)
    {
        Type = BitConverter.ToUInt32(headerBuffer, 0);
        MessageID = BitConverter.ToUInt32(headerBuffer, 4);
        Length = BitConverter.ToUInt32(headerBuffer, 8);
        AccountID = BitConverter.ToUInt32(headerBuffer, 12);
    }

    public Byte[] AssemblyHeader()
    {
        byte[] _head = new byte[HeaderLength];
        byte[] _type = BitConverter.GetBytes(Type);
        Array.Copy(_type, 0, _head, 0, 4);

        byte[] _msgID = BitConverter.GetBytes(MessageID);
        Array.Copy(_msgID, 0, _head, 4, 4);

        byte[] _length = BitConverter.GetBytes(Length);
        Array.Copy(_length, 0, _head, 8, 4);

        byte[] _accountID = BitConverter.GetBytes(AccountID);
        Array.Copy(_accountID, 0, _head, 12, 4);

        return _head;
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
