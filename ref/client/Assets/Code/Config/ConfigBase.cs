using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ItemBase
{
    public int ID;
}

public abstract class ConfigBase
{
    protected string name = "";
    private bool isLoadFinish = false;
    private object LockObject = new object();

    public ConfigBase(string name)
    {
        this.name = name;
        this.Initialize();
    }

    protected void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //Read();
    }

    protected void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }

        UIManager.Instance.LoginCtrl.SetNoticeActive();
    }

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;

		Debug.Log("Config reading " + name);
        try
        {
            this.ReadConfigParse();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);        	
        }

		
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }
    }

    protected abstract void ReadConfigParse();
}
