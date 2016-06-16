using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class HandleAndroidMsg : MonoBehaviour
{
    Vector2 direction = Vector2.zero;
    int currentIndex = 0;
    
    void Start()
    {

    }
    void Update()
    {
        if (GameSystem.Instance.mClient.mInputManager.sendShock)
            SendNgdsShock();

    }

    public void SetButtonDown(string i) {
        int flag = int.Parse(i);
        Logger.Log("recive android msg down" + i);
        switch (flag)
        {
            //up
            case 1:        
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(0, 1);
                //GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Click = true;
                break;
            //down
            case 2:
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(0, -1);
                break;
            //left
            case 3:
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(-1, 0);
                break;
            //right
            case 4:
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(1, 0);
                break;
            //pss
            case 5:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn2Click = true;
                currentIndex = 5;
                break;
            //break
            case 6:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn3Click = true;
                currentIndex = 6;
                break;
            //shoot
            case 7:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Click = true;
                currentIndex = 7;
                break;
            //run
            case 8:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn4Click = true;
                currentIndex = 8;
                break;
            //reset ngds
            case 9:          
                bool isNgds = GameSystem.Instance.mClient.mInputManager.isNGDS;
                GameSystem.Instance.mClient.mInputManager.isNGDS = !isNgds;
                Logger.Log("ReSet ngds state!!:" + GameSystem.Instance.mClient.mInputManager.isNGDS);
                break;
        }  
    }


    public void SetButtonUp(string i)
    {
        int flag = int.Parse(i);
        Logger.Log("recive android msg up" + i);   
        switch (flag)
        {
            //up
            case 1:
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(0, 0);
                //GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Click = true;
                break;
            //down
            case 2:
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(0, 0);
                break;
            //left
            case 3:
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(0, 0);
                break;
            //right
            case 4:
                GameSystem.Instance.mClient.mInputManager.mHVDirection = new Vector2(0, 0);
                break;
            //pss
            case 5:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn2Click = false;
                if (currentIndex == 5)         
                    GameSystem.Instance.mClient.mInputManager.m_CmdBtn2Released = true;               
                break;
            //break
            case 6:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn3Click = false;
                break;
            //shoot
            case 7:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Click = false;
                if (currentIndex == 7)
                    GameSystem.Instance.mClient.mInputManager.m_CmdBtn1Released = true;
                break;
            //run
            case 8:
                GameSystem.Instance.mClient.mInputManager.m_CmdBtn4Click = false;
                break;
        }
    }

    public void SetDirection(string str)
    {      
        string[] temp = str.Split(':');
        float x = float.Parse(temp[0]);
        float y = float.Parse(temp[1]);
        direction.x = x;
        direction.y = y;
        Logger.Log("recive android direction x:" + x + "-----y:" + y);
        GameSystem.Instance.mClient.mInputManager.mHVDirection = direction;
    }

    public void SendNgdsShock()
    {
		#if UNITY_ANDROID
        Logger.Log("send ngds shock to android");
        GameSystem.Instance.mClient.mInputManager.sendShock = false;
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //jo.Call("SetMotor", 1);
        jo.CallStatic("SetMotor", 1);
        Invoke("StopShock", 0.5f);
		#endif
        
    }

    public void StopShock()
    {
		#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //jo.Call("SetMotor", 0);
        jo.CallStatic("SetMotor", 0); 
		#endif
    }
}

