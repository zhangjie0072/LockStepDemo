using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;

class CSVUtility
{
    private string[][] Array;
    private byte[][] ArrayByte;

    public void ReadFile(string filePath)
    {
        //读取csv二进制文件  
        TextAsset binAsset = ResourceLoadManager.Instance.GetResources(filePath) as TextAsset;

        //读取每一行的内容  
        string[] lineArray = binAsset.text.Split("\r"[0]);

        //创建二维数组  
        Array = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            Array[i] = lineArray[i].Split(',');
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            for (int j = 0; j < Array[i].Length; ++j)
            {
                bw.Write(Array[i][j]);
            }
            bw.Close();
            ms.Close();
            ArrayByte[i] = ms.ToArray();
        }
    }

    public string GetDataByRowAndCol(int nRow, int nCol)
    {
        if (Array.Length <= 0 || nRow >= Array.Length)
            return "";
        if (nCol >= Array[0].Length)
            return "";

        return Array[nRow][nCol];
    }

    public string GetDataByIdAndName(int nId, string strName)
    {
        if (Array.Length <= 0)
            return "";

        int nRow = Array.Length;
        int nCol = Array[0].Length;
        for (int i = 1; i < nRow; ++i)
        {
            string strId = string.Format("\n{0}", nId);
            if (Array[i][0] == strId)
            {
                for (int j = 0; j < nCol; ++j)
                {
                    if (Array[0][j] == strName)
                    {
                        return Array[i][j];
                    }
                }
            }
        }

        return "";
    }

    public void SetProtobufValue<T>(ref Dictionary<uint, T> data)
    {
        if (Array.Length <= 0)
            return;

        for (uint i = 0; i < Array[0].Length; ++i)
        {
            T msg;
            msg = Serializer.Deserialize<T>(new MemoryStream(ArrayByte[i]));
            data.Add(i, msg);
        }
    }
}
