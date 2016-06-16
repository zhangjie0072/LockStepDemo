using UnityEngine;
using System.Collections;

public abstract class defaultCsvDataParent
{
    public abstract string[] getKeyArray();
    public abstract string[][] getDataArray();
    public abstract int num();
    public abstract int keynum();
    private int getTypeNum(string typeName)
    {
        return -1;
    }
    public abstract string getKeyByData(string _data);
    public abstract string getData(int num, string typeName);
    public abstract int getInt(int num, string typeName);
    public abstract float getFloat(int num, string typeName);
}