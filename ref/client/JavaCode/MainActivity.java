package com.tencent.freestyle;

import java.text.DecimalFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;

import org.json.JSONException;
import org.json.JSONObject;

import ourpalm.android.callback.Ourpalm_CallBackListener;
import ourpalm.android.callback.Ourpalm_GiftExchangeCallBack;
import ourpalm.android.callback.Ourpalm_PaymentCallBack;
import ourpalm.android.info.GameInfo;
import ourpalm.android.opservice.Ourpalm_OpService_Entry;
import ourpalm.android.pay.Ourpalm_Entry;
import ourpalm.tools.android.logs.Logs;

import com.umeng.message.ALIAS_TYPE;
import com.umeng.message.IUmengRegisterCallback;
import com.umeng.message.IUmengUnregisterCallback;
import com.umeng.message.MsgConstant;
import com.umeng.message.PushAgent;
import com.umeng.message.UTrack;
import com.umeng.message.UmengMessageHandler;
import com.umeng.message.UmengNotificationClickHandler;
import com.umeng.message.UmengRegistrar;
import com.umeng.message.entity.UMessage;
import com.umeng.message.local.UmengLocalNotification;
import com.umeng.message.local.UmengNotificationBuilder;
import com.umeng.message.proguard.k.e;
//import com.ourpalm.test.base_v3.R;
//import com.ourpalm.test_ourpalm_v3.MainActivity;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.ActivityManager;
import android.app.Notification;
import android.app.ActivityManager.RunningServiceInfo;
import android.app.ActivityManager.RunningTaskInfo;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.opengl.GLSurfaceView;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.NotificationCompat;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup.LayoutParams;
import android.widget.Button;
import android.widget.RelativeLayout;
import android.widget.RemoteViews;
import android.widget.Toast;

//UnityPlayerActivity
public class MainActivity extends UnityPlayerActivity implements
        OnClickListener {
    public static UnityPlayerActivity instance = null;

    public final String gameVer = "0.0.1";
    public final String gameResVer = "0.0.1";
    private final String gameName = "街头篮球";
    private final static String RoleLv = "10";
    private final static String RoleVipLv = "3";
    private final static String ServerName = "";
    private final static String ServerId = "10";

    private String Ourpalm_UserName;
    private String Ourpalm_UserId;
    private boolean cangocenter;
    private boolean canswitchaccount;
    private boolean canlogout;
    private Button mButton_login;
    private String TAG = "164";
    public static Context context;

    private Handler handler = null;
    private Handler handler1 = null;
    private Handler handlerGift = null;
    // public View _demoLoginView;
    public RelativeLayout layout;
    public GLSurfaceView mView;
    public String _tokenId = "";
    public String _userId = "";
    public String _palmId = "";
    public String _nickName = "";
    public String _userName = "";
    public boolean isLoginState = false;

    public static boolean _isFirstLogin = true;
    public int _hpRestoreTime = 360;
	private PushAgent mPushAgent;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        context = getApplicationContext();
        UmengInitSdk();
        LoginInit();
        handler = new Handler();
        handler1 = new Handler();
        handlerGift = new Handler();
        instance = this;

		
//		if (MiPushRegistar.checkDevice(this)) {
//            MiPushRegistar.register(this, "2882303761517400865", "5501740053865");
//		}
		}
        // _demoLoginView = findViewById(R.layout.ourpalm_demo_login);
        // Log.i(TAG,"onCreate _demoLoginView =" + _demoLoginView);
//    }

    public void PressBack() {
        Ourpalm_Entry.getInstance(this).Ourpalm_ExitGame(true);
    }
    
    public void OnApplicationPause(boolean pause)
    {
    	Log.i(TAG,"OnApplicationPause in android" +  pause);
    	if( pause)
    	{
    		moveTaskToBack(true);  		
    	}
    	else
    	{
    		//onResume();
    		//moveTaskToFront(true);
    	}
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
            Ourpalm_Entry.getInstance(this).Ourpalm_ExitGame(true);
            return true;
        }
        return super.onKeyDown(keyCode, event);
    }



    @Override
    protected void onPause() {
        super.onPause();
        // Logs.i("info", "onPause");
        Log.i(TAG,"onPause is called");
        // �������
        Ourpalm_Entry.getInstance(this).Ourpalm_onPause();
    }

    @Override
    protected void onRestart() {
        // TODO Auto-generated method stub
        super.onRestart();
        // Logs.i("info", "onRestart");
        // �������
        Ourpalm_Entry.getInstance(this).Ourpalm_onRestart();
    }

    @Override
    protected void onResume() {
        // TODO Auto-generated method stub
        super.onResume();
        Log.i(TAG,"onResume is called");
        // Logs.i("info", "onResume");
        // �������
        Ourpalm_Entry.getInstance(this).Ourpalm_onResume();
    }

    @Override
    protected void onStart() {
        // TODO Auto-generated method stub
        super.onStart();
        // Logs.i("info", "onStart");
        // �������
        Ourpalm_Entry.getInstance(this).Ourpalm_onStart();
    }

    @Override
    protected void onStop() {
        // TODO Auto-generated method stub
        super.onStop();
        // Logs.i("info", "onStop");
        // �������
        Ourpalm_Entry.getInstance(this).Ourpalm_onStop();
    }

    @Override
    protected void onDestroy() {
        // TODO Auto-generated method stub
        super.onDestroy();
        // Logs.i("info", "onDestroy");
        // �������
        Ourpalm_Entry.getInstance(this).Ourpalm_onDestroy();
        // ɱ����̣���Ϸ������onDestroy������ɱ����̣�
        // android.os.Process.killProcess(android.os.Process.myPid());
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        // TODO Auto-generated method stub
        super.onActivityResult(requestCode, resultCode, data);
        // Logs.i("info", "onActivityResult");
        // �������
        Ourpalm_Entry.getInstance(this).Ourpalm_onActivityResult(requestCode,
                resultCode, data);
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        // TODO Auto-generated method stub
        super.onConfigurationChanged(newConfig);
        // Logs.i("info", "onConfigurationChanged");
        // Must call.
        Ourpalm_Entry.getInstance(this).Ourpalm_onConfigurationChanged(
                newConfig);
    }

    @Override
    protected void onNewIntent(Intent intent) {
        // TODO Auto-generated method stub
        super.onNewIntent(intent);
        // Logs.i("info", "onNewIntent");
        // Must call.
        Ourpalm_Entry.getInstance(this).Ourpalm_onNewIntent(intent);
    }

    public void SetLoginState(int loginState) {
        isLoginState = loginState == 1;
        Log.i(TAG, "## SetLoginState SetLoginState=" + loginState
                + "isLoginState=" + isLoginState);
    }

    public void LoginInit() {
        Log.i(TAG, "Android Login called() from csharp");
        String gameVersion = "1";
        String resVersion = "1";
        gameVersion = getGameVersion();
        resVersion = getResVersion();


        Ourpalm_Entry.getInstance(this).Ourpalm_Init(GameInfo.GameType_Online,
                gameVersion, resVersion, new Ourpalm_CallBackListener() {
                    @Override
                    public void Ourpalm_LogoutSuccess() {
                        // TODO Auto-generated method stub
                    }

                    @Override
                    public void Ourpalm_LogoutFail(int code) {
                        // TODO Auto-generated method stub

                    }

                    @Override
                    public void Ourpalm_LoginSuccess(String tokenId, String data) {
                        // TODO Auto-generated method stub
                        _tokenId = tokenId;
                        Log.i(TAG, "Ourpalm_LoginSuccess called!");
                        Log.i(TAG, "MainActivity, tokenId == " + tokenId
                                + ", user data == " + data);
//                        SetLocalPush(0,1,30,"测试消息!");
                        try {
                            JSONObject json = new JSONObject(data);
                            Ourpalm_UserId = json.getString("id");
                            Ourpalm_UserName = json.getString("userName");
                            _palmId = json.getJSONObject("returnJson")
                                    .getString("palmId");
                            _nickName = json.getJSONObject("returnJson")
                                    .getString("nickName");

                            Log.i(TAG, "Ourpalm_UserName == "
                                    + Ourpalm_UserName + ", Ourpalm_UserId == "
                                    + Ourpalm_UserId);
                            _userId = Ourpalm_UserId;
                            Log.i(TAG, "_userId set value =" + _userId);
                            _userName = Ourpalm_UserName;
                        } catch (JSONException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        }

                        // Toast.makeText(MainActivity.this,
                        // "Login successfully",
                        // Toast.LENGTH_LONG).show();

                        setContentView(mUnityPlayer.getView());
                        Log.i(TAG, "Ourpalm_UserId=" + Ourpalm_UserId);
                        Log.i(TAG, "before _userId=" + _userId);
                        _userId = Ourpalm_UserId;
                        Log.i(TAG, "after _userId=" + _userId);

                        String strCanGoCenter = cangocenter ? "1" : "0";
                        String strSwitchAccount = canswitchaccount ? "1" : "0";
                        String strCanLogout = canlogout ? "1" : "0";

                        String msg = Ourpalm_UserId
                                + "&"
                                + _tokenId
                                + "&"
                                + _nickName
                                + "&"
                                + _userName
                                + "&"
                                + Ourpalm_Entry.getInstance(
                                        MainActivity.instance).getServiceId()
                                + "&" + strCanGoCenter + "&" + strSwitchAccount
                                + "&" + strCanLogout;

                        Log.i(TAG, "ms-------------VerifyCDKeyReq = " + msg);

                        // if( !isLoginState )
                        {
                            Log.i(TAG, "##UILogo(Clone) sendisLoginState="
                                    + isLoginState);
                            UnityPlayer.UnitySendMessage("UILogo(Clone)",
                                    "HandleResp", msg);
                        }
                        // else
                        {
                            Log.i(TAG, "##UILogin(Clone) sendisLoginState="
                                    + isLoginState);
                            UnityPlayer.UnitySendMessage("UILogin(Clone)",
                                    "HandleResp", msg);
                        }
                    }

                    @Override
                    public void Ourpalm_LoginFail(int code) {
                        // TODO Auto-generated method stub
                        // ��Ϸ����ʵ��
                        Log.i(TAG, "Ourpalm_LoginFail called! code= " + code);
                        if (code == 11) {
                            // player cancel to login.
                            PressBack();
                            // Ourpalm_Entry.getInstance(this).Ourpalm_ExitGame(true);
                        }
                    }

                    @Override
                    public void Ourpalm_InitSuccess() {
                        // TODO Auto-generated method stub
                        Log.i(TAG, "Ourpalm_InitSuccess called!");
                    }

                    @Override
                    public void Ourpalm_InitFail(int code) {
                        // TODO Auto-generated method stub
                        Log.i(TAG, "Ourpalm_InitFail called!");
                    }

                    @Override
                    public void Ourpalm_ExitGame() {
                        // TODO Auto-generated method stub
                        // ��Ϸʹ����ȤSDK�˳��ӿ�ʱ����ʵ�ִ˻ص�
                        Log.i(TAG, "Ourpalm_ExitGame called!");
                        MainActivity.this.finish();
                    }

                    @Override
                    public void Ourpalm_SwitchingAccount(boolean Success,
                            String tokenId, String userInfo) {
                        // TODO Auto-generated method stub
                        Log.i(TAG, "Ourpalm_SwitchingAccount called!");
                        if (Success) {
                            // ��ǰ���������л��˺Ź���ʱ����Ϸ��Ҫʵ�ִ˽ӿ��߼�
                            // Success=true��ʾ�л��ɹ���Success=false��ʾ�л�ʧ�ܣ�tokenId��userInfoͬ��½�ɹ��ӿ�
                            // tokenId����Ϸ�ͻ�����Ҫ������Ϸ����������Ϸ��������tokenId����Ȥ�û�����������Ȩ
                            // ��Ϸ����ʵ��
                            Logs.i(TAG, "MainActivity, tokenId == " + tokenId
                                    + ", userInfo == " + userInfo);
                            try {
                                JSONObject json = new JSONObject(userInfo);
                                Ourpalm_UserId = json.getString("id");
                                Ourpalm_UserName = json.getString("userName");
                                Logs.i(TAG, "Ourpalm_UserName == "
                                        + Ourpalm_UserName
                                        + ", Ourpalm_UserId == "
                                        + Ourpalm_UserId);
                            } catch (JSONException e) {
                                // TODO Auto-generated catch block
                                e.printStackTrace();
                            }

                            // Toast.makeText(MainActivity.this, "�л��˺ųɹ�",
                            // Toast.LENGTH_LONG).show();

                        } else {
                            // Toast.makeText(MainActivity.this, "�л��˺�ʧ��",
                            // Toast.LENGTH_LONG).show();
                        }
                    }
                });

        String enable = Ourpalm_Entry.getInstance(MainActivity.this)
                .Ourpalm_GetEnableInterface();
        Logs.i(TAG, "enable == " + enable);

        try {
            JSONObject json = new JSONObject(enable);
            if ("Enabled".equals(json.getString("UserCenter"))) {
                this.cangocenter = true;
            }
            if ("Enabled".equals(json.getString("SwitchAccount"))) {
                this.canswitchaccount = true;
            }
            if ("Enabled".equals(json.getString("Logout"))) {
                this.canlogout = true;
            }
        } catch (JSONException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

    }

    public void Login() {
        initGameLogin();
    }

    // public void Ourpalm_Pay(final String propId, final String chargeCash,
    // final String currencyType, final String propName, final StringpropCount,
    // final StringpropDes,finalString Gameurl,final String ExtendParams,
    // final Ourpalm_PaymentCallBack callBack, final String rolelv, final String
    // roleviplv)

    public void Pay(String propId, String price, String currencyType,
            String propName, String propCount, String des, String url,
            String extend, String lv, String vipLv) {
        Logs.i(TAG, "propId == " + propId);
        Logs.i(TAG, "price == " + price);
        Logs.i(TAG, "currencyType == " + currencyType);
        Logs.i(TAG, "propName == " + propName);
        Logs.i(TAG, "propCount == " + propCount);
        Logs.i(TAG, "des == " + des);
        Logs.i(TAG, "url == " + url);
        Logs.i(TAG, "extend == " + extend);
        Logs.i(TAG, "lv == " + lv);
        Logs.i(TAG, "vipLv == " + vipLv);

        Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_Pay(propId, price,
                currencyType, propName, propCount, des, url, extend,
                new Ourpalm_PaymentCallBack() {

                    @Override
                    public void Ourpalm_PaymentSuccess(int code, String ssid,
                            String pbid) {
                        // TODO Auto-generated method stub
                        Logs.i("info", "Ourpalm_PaymentSuccess, code = " + code
                                + " ,ssid = " + ssid + " ,pbid = " + pbid);
                        Toast.makeText(MainActivity.this, "����ɹ�",
                                Toast.LENGTH_SHORT).show();

                        // SendRoleCredit();
                    }

                    @Override
                    public void Ourpalm_PaymentFail(int code, String ssid,
                            String pbid) {
                        // TODO Auto-generated method stub
                        Logs.i("info", "Ourpalm_PaymentFail, code = " + code
                                + " ,ssid = " + ssid + " ,pbid = " + pbid);
                        // Toast.makeText(MainActivity.this, "DEMO,֧��ʧ����",
                        // Toast.LENGTH_SHORT).show();
                    }

                    @Override
                    public void Ourpalm_OrderSuccess(int code, String ssid,
                            String pbid) {
                        // TODO Auto-generated method stub
                        Logs.i("info", "Ourpalm_OrderSuccess, code = " + code
                                + " ,ssid = " + ssid + " ,pbid = " + pbid);
                        // Toast.makeText(MainActivity.this, "DEMO,�µ��ɹ���",
                        // Toast.LENGTH_SHORT).show();
                    }
                }, lv, vipLv);
    }

    public void SendGoodsLog(int roleLv, int vipLv, String updateType,
            String itemId, String itemName, int itemCount, String custom) {
        HashMap<String, Object> map = new HashMap<String, Object>();
        Log.d(TAG, "roleLv=" + roleLv);
        Log.d(TAG, "roleVipLevel=" + vipLv);
        Log.d(TAG, "updateType=" + updateType);
        Log.d(TAG, "itemId=" + itemId);
        Log.d(TAG, "itemName=" + itemName);
        Log.d(TAG, "itemCount=" + itemCount);
        Log.d(TAG, "custom=" + custom);

        map.put("roleLevel", roleLv);
        map.put("roleVipLevel", vipLv);
        map.put("updateType", updateType);
        map.put("itemId", itemId);
        map.put("itemName", itemName);
        map.put("itemCount", itemCount);
        map.put("custom", custom);
        Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_SendGameInfoLog(
                "9", "role-item-update", map);
    }

    public void SetLoginInfo(int type, String gameservername,
            String gameserverid, String rolename, String roleid, String rolelv,
            String roleviplv) {
        Log.d(TAG, "SetLoginInfo,type=" + type);
        Log.d(TAG, "SetLoginInfo,gameservername=" + gameservername);
        Log.d(TAG, "SetLoginInfo,gameserverid=" + gameserverid);
        Log.d(TAG, "SetLoginInfo,rolename=" + rolename);
        Log.d(TAG, "SetLoginInfo,roleid=" + roleid);
        Log.d(TAG, "SetLoginInfo,rolelv=" + rolelv);
        Log.d(TAG, "SetLoginInfo,roleviplv=" + roleviplv);

        Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_SetGameInfo(type,
                gameservername, gameserverid, rolename, roleid, rolelv,
                roleviplv);
    }

    public void SendPlayerLog(int roleLv, int vipLv, String propKey,
            String propValue, String rangeAbility) {
        Log.d(TAG, "roleLevel=" + roleLv);
        Log.d(TAG, "roleVipLevel=" + vipLv);
        Log.d(TAG, "propKey=" + propKey);
        Log.d(TAG, "propValue=" + propValue);
        Log.d(TAG, "rangeability=" + rangeAbility);

        HashMap<String, Object> map = new HashMap<String, Object>();
        map.put("roleLevel", roleLv);
        map.put("roleVipLevel", vipLv);
        map.put("propKey", propKey);
        map.put("propValue", propValue);
        map.put("rangeability", rangeAbility);

        Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_SendGameInfoLog(
                "10", "role-prop-update", map);
        

    }
    
    
   public class Gift implements Ourpalm_GiftExchangeCallBack
   {
       @Override
       public  void Ourpalm_Success(String packageId, String extendParams){
       	Toast.makeText(MainActivity.this, "�һ��ɹ�,�뵽�����в���", Toast.LENGTH_LONG).show();  
       }
       @Override
       public  void Ourpalm_Fail(int code){
			if(code == 21197) {
				Toast.makeText(MainActivity.this, "��������ȷ�������", Toast.LENGTH_LONG).show(); 
			}
       	if(code == 21190) {
				Toast.makeText(MainActivity.this, "������ѱ�ʹ��", Toast.LENGTH_LONG).show(); 
			}
			if(code == 21191) {
				Toast.makeText(MainActivity.this, "���������ȡ", Toast.LENGTH_LONG).show(); 
			}
       }
   }
   
   String _giftCode;
   String _url;
   String _extendParams;
   
	public void SendGiftExchangeCode(String giftCode, String url, String extendParams) 
	{    
 		Log.i(TAG,"SendGiftExchangeCode giftCode=" + giftCode);
		Log.i(TAG,"SendGiftExchangeCode url=" + url);
		Log.i(TAG,"SendGiftExchangeCode extendParams=" + extendParams);
		
		_giftCode = giftCode;
		_url = url;
		_extendParams = extendParams;
        new Thread() {
            public void run() {
                handler.post(runnableUIGift);
            }
        }.start();
        
        
        

    }

	
    Runnable runnableUIGift = new Runnable() {
        @SuppressWarnings("deprecation")
        @Override
        public void run() {
    		Log.i(TAG,"SendGiftExchangeCode _giftCode=" + _giftCode);
    		Log.i(TAG,"SendGiftExchangeCode _url=" + _url);
    		Log.i(TAG,"SendGiftExchangeCode _extendParams=" + _extendParams);
    		
            Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_GiftExchange(
            		_giftCode,
            		_url,
            		_extendParams,
    	            new Gift()
    	       );
            return;
        }
    };
    
    
    public static final int NETTYPE_WIFI = 0x01;
    public static final int NETTYPE_CMWAP = 0x02;
    public static final int NETTYPE_CMNET = 0x03;

    public int getNetworkType() {
        int netType = 0;
        ConnectivityManager connectivityManager = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo networkInfo = connectivityManager.getActiveNetworkInfo();
        if (networkInfo == null) {
            return netType;
        }
        int nType = networkInfo.getType();
        if (nType == ConnectivityManager.TYPE_MOBILE) {
            String extraInfo = networkInfo.getExtraInfo();
            if (!extraInfo.isEmpty()) {
                if (extraInfo.toLowerCase().equals("cmnet")) {
                    netType = NETTYPE_CMNET;
                } else {
                    netType = NETTYPE_CMWAP;
                }
            }
        } else if (nType == ConnectivityManager.TYPE_WIFI) {
            netType = NETTYPE_WIFI;
        }
        return netType;
    }

    public boolean isNetworkConnected() {
        ConnectivityManager cm = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo ni = cm.getActiveNetworkInfo();
        return ni != null && ni.isConnectedOrConnecting();
    }

    public void SendRoleCredit(String orderNo, String payChannelId,
            String currencyType, String currencyAmount,
            String virtualCurrencyId, String virtualCurrencyAmount) {
        HashMap<String, Object> map = new HashMap<String, Object>();
        Log.d(TAG, "orderNo=" + orderNo);
        Log.d(TAG, "payChannelId=" + payChannelId);
        Log.d(TAG, "currencyType=" + currencyType);
        Log.d(TAG, "currencyAmount=" + currencyAmount);
        Log.d(TAG, "virtualCurrencyId=" + virtualCurrencyId);
        Log.d(TAG, "virtualCurrencyAmount=" + virtualCurrencyAmount);

        map.put("orderNo", orderNo);
        map.put("payChannelId", payChannelId);
        map.put("currencyType", currencyType);
        map.put("currencyAmount", currencyAmount);
        map.put("virtualCurrencyId", virtualCurrencyId);
        map.put("virtualCurrencyAmount", virtualCurrencyAmount);
        Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_SendGameInfoLog(
                "8", "role-credit", map);
    }

    public void onClick(View view) {
        // TODO Auto-generated method stub
        if (view == this.mButton_login) {
            Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_Login();
        }
    }

    private void initGameLogin() {
        Logs.i(TAG, "initGameLogin -- for android setContentView(login)");
        new Thread() {
            public void run() {
                handler.post(runnableUI);
            }
        }.start();


    }



    Runnable runnableUI = new Runnable() {
        @SuppressWarnings("deprecation")
        @Override
        public void run() {
            Log.i(TAG, "runnableUI --called");

            Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_Login();
            return;
        }
    };

    public void SetHandel(String infoString, int infoInt) {

    }

    private int alarmCount = 0;

    public void RecvPush(int id, int type, String date, String time,
            int online, String content) {
        try {
            setReminder(true, id, type, date, time, online, content, 5);
        } catch (ParseException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    public void setHpRestoreTime(int hpRestoreTime) {
        _hpRestoreTime = hpRestoreTime;
        Log.i(TAG, "_hpRestoreTime=" + _hpRestoreTime);
    }

    public void GoCenter() {
        Log.i(TAG, "Android GoCenter is called");
        // �����û����Ľӿڣ���Ϸ�������(SDK�ӿ�)
        if (this.cangocenter) {
            Ourpalm_Entry.getInstance(MainActivity.this).Ourpalm_GoCenter();
        } else {
            Toast.makeText(MainActivity.this, "��ǰ����SDK֧���û����Ļ�����̳����",
                    Toast.LENGTH_SHORT).show();
        }
    }

    public void SwitchAccount() {
        if (this.canswitchaccount) {
            Ourpalm_Entry.getInstance(MainActivity.this)
                    .Ourpalm_SwitchAccount();
        } else {
            Toast.makeText(MainActivity.this, "��ǰ����SDK��֧���л��˺Ź���",
                    Toast.LENGTH_SHORT).show();
        }
    }

    public void Logout() {
        if (this.canlogout) {
            Ourpalm_Entry.getInstance(this).Ourpalm_Logout();
        } else {
            Toast.makeText(MainActivity.this, "��ǰ����SDK��֧���˺�ע����",
                    Toast.LENGTH_SHORT).show();
        }
    }

    public void EnterServiceQuestion(){
        Ourpalm_OpService_Entry.getInstance().EnterServiceQuestion(); 
    }

    public void SetLocalPush(int id, int online, int second, String content )
    {
        AlarmManager am = (AlarmManager) getSystemService(ALARM_SERVICE);
        Intent alartIntent = new Intent(this, this.getClass());
        alartIntent.putExtra("id", id);
        alartIntent.putExtra("content", content);
        alartIntent.putExtra("online", online);
        alartIntent.putExtra("packageName", getPackageName());
        Log.i(TAG,"SetLocalPush getPackageName()=" + getPackageName());

        PendingIntent pi = PendingIntent.getBroadcast(MainActivity.this,
                alarmCount++, alartIntent, 0);
        
        Calendar c = Calendar.getInstance();
        c.setTimeInMillis(System.currentTimeMillis());
        
        c.add(Calendar.SECOND, second);
        am.set(AlarmManager.RTC_WAKEUP, c.getTimeInMillis(), pi);
       
        //传递消息给友盟推送服务
        AddLocalNotification(id,second,content,0);
    }

    /**
     * Set the alarm
     *
     * @param b
     *            whether enable the Alarm clock or not
     * @throws ParseException
     */
    private void setReminder(boolean b, int id, int type, String date,
            String time, int online, String content, int second)
            throws ParseException {
    	Log.i(TAG,"setReminder b=" + b);
    	Log.i(TAG,"setReminder id=" + id);
    	Log.i(TAG,"setReminder type=" + type);
    	Log.i(TAG,"setReminder date=" + date);
    	Log.i(TAG,"setReminder time=" + time);
	   Log.i(TAG,"setReminder online=" + online);
	   Log.i(TAG,"setReminder content=" + content);
	   Log.i(TAG,"setReminder second=" + second);
	   
	   
   
   
        AlarmManager am = (AlarmManager) getSystemService(ALARM_SERVICE);
        Intent alartIntent = new Intent(this, this.getClass());
        alartIntent.putExtra("id", id);
        alartIntent.putExtra("content", content);
        alartIntent.putExtra("online", online);
        alartIntent.putExtra("packageName", getPackageName());
        Log.i(TAG,"setReminder getPackageName()=" + getPackageName());
        
        SimpleDateFormat fmt0 = new SimpleDateFormat();
        Calendar c0 = Calendar.getInstance();
        final long now = System.currentTimeMillis();
        
        fmt0.applyPattern("MM/dd/yyyy-HH:mm");
        String format0 = date + "-" + time;
        Date d0 = fmt0.parse(format0);
        c0.setTimeInMillis(d0.getTime());

        if (c0.getTimeInMillis() > now) {
            AddLocalNotification(id,(int)((c0.getTimeInMillis()-now)/1000),content,type);
        }
        	
        
        

        PendingIntent pi = PendingIntent.getBroadcast(MainActivity.this,
                alarmCount++, alartIntent, 0);

        if (type == 4) {
            pi = PendingIntent.getBroadcast(MainActivity.this, 999999,
                    alartIntent, 0);
        }

        if (b) {
            // just use current time + 10s as the Alarm time.
            Calendar c = Calendar.getInstance();
            
            c.setTimeInMillis(System.currentTimeMillis());
            long nextTime = 0;

            int curYear = c.get(Calendar.YEAR);
            int curMoth = c.get(Calendar.MONTH);
            int curDay = c.get(Calendar.DAY_OF_MONTH);
            int curHour = c.get(Calendar.HOUR_OF_DAY);
            int curMin = c.get(Calendar.MINUTE);

            switch (type) {
            case 1:
                // day
                final long[] checkedWeeksDay = parseDateWeeks("1,2,3,4,5,6,7");
                if (null != checkedWeeksDay) {
                    for (long week : checkedWeeksDay) {
                        c.set(Calendar.DAY_OF_WEEK, (int) (week + 1));

                        long triggerAtTime = c.getTimeInMillis();
                        SimpleDateFormat fmt = new SimpleDateFormat();
                        fmt.applyPattern("yyyy-MM-dd-HH:mm");
                        String format = curYear + "-" + (curMoth + 1) + "-"
                                + curDay + "-" + time;

                        Date d = fmt.parse(format);
                        triggerAtTime = d.getTime();

                        if (triggerAtTime <= now) {
                            triggerAtTime += AlarmManager.INTERVAL_DAY * 7;
                        }
                        // save the recent alarm.
                        if (0 == nextTime) {
                            nextTime = triggerAtTime;
                        } else {
                            nextTime = Math.min(triggerAtTime, nextTime);
                        }
                    }
                    am.set(AlarmManager.RTC_WAKEUP, nextTime, pi);
                }

                break;

            case 2:
            // week
            {
                final long[] checkedWeeks = parseDateWeeks(date);
                if (null != checkedWeeks) {
                    for (long week : checkedWeeks) {
                        c.set(Calendar.DAY_OF_WEEK, (int) (week + 1));

                        long triggerAtTime = c.getTimeInMillis();

                        SimpleDateFormat fmt = new SimpleDateFormat();
                        fmt.applyPattern("yyyy-MM-dd-HH:mm");
                        String format = curYear + "-" + (curMoth + 1) + "-"
                                + curDay + "-" + time;
                        Date d = fmt.parse(format);
                        triggerAtTime = d.getTime();

                        if (triggerAtTime <= now) {
                            triggerAtTime += AlarmManager.INTERVAL_DAY * 7;
                        }
                        if (0 == nextTime) {
                            nextTime = triggerAtTime;
                        } else {
                            nextTime = Math.min(triggerAtTime, nextTime);
                        }
                    }
                    am.set(AlarmManager.RTC_WAKEUP, nextTime, pi);
                }
            }
                break;

            case 3:
            // date
            {
                SimpleDateFormat fmt = new SimpleDateFormat();
                fmt.applyPattern("MM/dd/yyyy-HH:mm");
                String format = date + "-" + time;
                Date d = fmt.parse(format);
                c.setTimeInMillis(d.getTime());

                if (c.getTimeInMillis() < now) {
                    break;
                }
                am.set(AlarmManager.RTC_WAKEUP, c.getTimeInMillis(), pi);
            }
                break;

            case 4: {
                if (date == null || date.equals("0")) {
                    am.cancel(pi);
                    break;
                }
                int hp = Integer.parseInt(date);
                int maxHp = Integer.parseInt(time);
                if (hp >= maxHp) {
                    am.cancel(pi);
                    break;
                }
                Log.i(TAG, "maxHp=" + maxHp);
                int hpOff = (maxHp - hp);
                hpOff = hpOff * _hpRestoreTime;
                // hpOff = hpOff * 5;
                c.add(Calendar.SECOND, hpOff);
                Log.i(TAG, "hpOff=" + hpOff);
                am.set(AlarmManager.RTC_WAKEUP, c.getTimeInMillis(), pi);
            }

                break;
            }
        } else {
            am.cancel(pi);
        }

    }

    public static long[] parseDateWeeks(String value) {
        long[] weeks = null;
        try {
            final String[] items = value.split(",");
            weeks = new long[items.length];
            int i = 0;
            for (String s : items) {
                weeks[i++] = Long.valueOf(s);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return weeks;
    }

    public String getGameVersion() {
        try {
            PackageManager manager = this.getPackageManager();
            PackageInfo info = manager.getPackageInfo(this.getPackageName(), 0);
            return this.getString(R.string.gameVersion);
        } catch (Exception e) {
            e.printStackTrace();
            return "1.0.0";

        }
    }

    public String getResVersion() {
        try {
            PackageManager manager = this.getPackageManager();
            PackageInfo info = manager.getPackageInfo(this.getPackageName(), 0);
            return this.getString(R.string.resVersion);
        } catch (Exception e) {
            e.printStackTrace();
            return "1.0.0";
        }
    }
    public void UmengInitSdk()
    {

 	    Log.e("PushAgent","push agent initialize ");
		mPushAgent = PushAgent.getInstance(context);
		mPushAgent.setDebugMode(true);
		mPushAgent.setPushCheck(true);    //默认不检查集成配置文件
//		mPushAgent.setLocalNotificationIntervalLimit(false);  //默认本地通知间隔最少是10分钟

		//sdk开启通知声音
		mPushAgent.setNotificationPlaySound(MsgConstant.NOTIFICATION_PLAY_SDK_ENABLE);
		// sdk关闭通知声音
//		mPushAgent.setNotificationPlaySound(MsgConstant.NOTIFICATION_PLAY_SDK_DISABLE);
		// 通知声音由服务端控制
//		mPushAgent.setNotificationPlaySound(MsgConstant.NOTIFICATION_PLAY_SERVER);
//		mPushAgent.setNotificationPlayLights(MsgConstant.NOTIFICATION_PLAY_SDK_DISABLE);
//		mPushAgent.setNotificationPlayVibrate(MsgConstant.NOTIFICATION_PLAY_SDK_DISABLE);
		
		//应用程序启动统计
		//参考集成文档的1.5.1.2
		//http://dev.umeng.com/push/android/integration#1_5_1
		mPushAgent.onAppStart();
//		Log.e("PushAgent","package name "+this.getPackageName());
		//开启推送并设置注册的回调处理
// 	    Log.e("PushAgent"," enable push agent ");
// 	    mPushAgent.enable();
// 	    mPushAgent.enable(mRegisterCallback);
 	    mPushAgent.setPushIntentServiceClass(MyPushIntentService.class);
 	    
 	    
// 	    Log.e("PushAgent","push agent appkey?"+mPushAgent.getMessageAppkey());
// 	    Log.e("PushAgent","push agent app sec?"+mPushAgent.getMessageSecret());
 	    Log.e("PushAgent","push agent enabled?"+mPushAgent.isEnabled());
 	   String device_token = UmengRegistrar.getRegistrationId(context);
//	    Log.e("PushAgent","push agent device_token?"+device_token);
		List<UmengLocalNotification> localNotifications = PushAgent.getInstance(context).findAllLocalNotifications();

//	    Log.e("PushAgent","push agent localNotifications?"+localNotifications.size());
	    for(int i=0;i<localNotifications.size();i++)
	    {
		    Log.e("PushAgent","push agent localNotification:"+localNotifications.get(i).getContent()+","+localNotifications.get(i).getDateTime());
	    }

	    mPushAgent.setMessageHandler(new UmengMessageHandler()
        {
        	/**
			 * 参考集成文档的1.6.3
			 * http://dev.umeng.com/push/android/integration#1_6_3
			 * */
			@Override
			public void dealWithCustomMessage(final Context context, final UMessage msg) {
				new Handler().post(new Runnable() {
					
					@Override
					public void run() {
						// TODO Auto-generated method stub
						// 对自定义消息的处理方式，点击或者忽略
						boolean isClickOrDismissed = true;
						Toast.makeText(context, msg.custom, Toast.LENGTH_LONG).show();
						if(isClickOrDismissed) {
							//自定义消息的点击统计
							//关闭统计
							UTrack.getInstance(getApplicationContext()).trackMsgClick(msg);
						} else {
							//自定义消息的忽略统计
							//关闭统计
							UTrack.getInstance(getApplicationContext()).trackMsgDismissed(msg);
						}
						Log.e("PushAgent","push agent deal with custom message?"+context);
					}
				});
			}
			
			/**
			 * 参考集成文档的1.6.4
			 * http://dev.umeng.com/push/android/integration#1_6_4
			 * */
			@Override
			public Notification getNotification(Context context,
					UMessage msg) {
				switch (msg.builder_id) {

				default:

					Log.e("PushAgent","push agent msg.builder_id?"+msg.builder_id+",context "+context+",umessage "+msg.toString());
					//默认为0，若填写的builder_id并不存在，也使用默认。
					return super.getNotification(context, msg);
				}
			}
			@Override
			public void dealWithNotificationMessage(final Context context, final UMessage msg)
			{
				Log.e("PushAgent","push agent msg?"+",context "+context+",umessage "+msg.toString());
			}
        	
        });
		

		/**
		 * 该Handler是在BroadcastReceiver中被调用，故
		 * 如果需启动Activity，需添加Intent.FLAG_ACTIVITY_NEW_TASK
		 * 参考集成文档的1.6.2
		 * http://dev.umeng.com/push/android/integration#1_6_2
		 * */
		UmengNotificationClickHandler notificationClickHandler = new UmengNotificationClickHandler(){
			@Override
			public void dealWithCustomAction(Context context, UMessage msg) {
				Toast.makeText(context, msg.custom, Toast.LENGTH_LONG).show();
				Log.e("PushAgent","push agent dealWithCustomAction?"+context);
			}
		};
		//使用自定义的NotificationHandler，来结合友盟统计处理消息通知
		//参考http://bbs.umeng.com/thread-11112-1-1.html
		//CustomNotificationHandler notificationClickHandler = new CustomNotificationHandler();
		mPushAgent.setNotificationClickHandler(notificationClickHandler);
    }
	//此处是注册的回调处理
	//参考集成文档的1.7.10
	//http://dev.umeng.com/push/android/integration#1_7_10
	public IUmengRegisterCallback mRegisterCallback = new IUmengRegisterCallback() {
		
		@Override
		public void onRegistered(String registrationId) {
			// TODO Auto-generated method stub
			handler.post(new Runnable() {
				
				@Override
				public void run() {
					// TODO Auto-generated method stub
//					updateStatus();

				}
			});
		}
	};
	
	//此处是注销的回调处理
	//参考集成文档的1.7.10
	//http://dev.umeng.com/push/android/integration#1_7_10
	public IUmengUnregisterCallback mUnregisterCallback = new IUmengUnregisterCallback() {
		
		@Override
		public void onUnregistered(String registrationId) {
            // TODO Auto-generated method stub
//			Log.e("PushAgent","registrationId "+registrationId);
			handler.postDelayed(new Runnable() {

				@Override
				public void run() {
					// TODO Auto-generated method stub
//					updateStatus();
				}
			}, 2000);
		}
	};
	//本地通知
	@SuppressLint("SimpleDateFormat") public void AddLocalNotification(int id ,int secondsLeft,String content,int type)
	{
		Log.e("PushAgent","local notification added id "+id+",time "+secondsLeft+",content "+content);
//		Calendar c = Calendar.getInstance();
//        c.setTimeInMillis(System.currentTimeMillis());
//        
//        c.add(Calendar.SECOND, secondsLeft);
//		开发者可以自定义本地通知
// 	    Log.e("PushAgent","push agent enabled?"+mPushAgent.isEnabled());
 	    
 		//初始化通知
		UmengLocalNotification localNotification = new UmengLocalNotification();
		//设置通知开始时间
		long time = System.currentTimeMillis() + secondsLeft * 1000;
		SimpleDateFormat format=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");  
        Date d=new Date(time);  
        String t=format.format(d);
		localNotification.setTitle(gameName);  //设置通知标题
		localNotification.setTicker(gameName);
        localNotification.setContent(content);
        localNotification.setDateTime(t);
        Log.e(TAG, t+","+localNotification.getContent());
        //设置重复次数，默认是1
        int	repeatTime = 1;
        int	repeatInterval = 1;
        switch(type)
        {
        case 1:
        {
        	repeatTime = 9999;
        	repeatInterval = 1;
            //设置重复单位，默认是天
            localNotification.setRepeatingUnit(UmengLocalNotification.REPEATING_UNIT_DAY);
        	break;
        }
        case 2:
        {
        	repeatTime = 9999;
        	repeatInterval = 7;
            //设置重复单位，默认是天
            localNotification.setRepeatingUnit(UmengLocalNotification.REPEATING_UNIT_DAY);
        	
        	break;
        }
        default:
        {
        	repeatTime = 1;
        	repeatInterval = 1;
            //设置重复单位，默认是天
            localNotification.setRepeatingUnit(UmengLocalNotification.REPEATING_UNIT_HOUR);
        	break;
        }
        	
        }
        localNotification.setRepeatingNum(repeatTime);
        //设置重复间隔，默认是1
        localNotification.setRepeatingInterval(repeatInterval);        
        
        //初始化通知样式
        UmengNotificationBuilder builder = localNotification.getNotificationBuilder();
        //设置小图标
        builder.setSmallIconDrawable("ic_launcher");
        //设置大图标
        builder.setLargeIconDrawable("ic_launcher");
        //设置自动清除
        builder.setFlags(Notification.FLAG_NO_CLEAR);
        
        localNotification.setNotificationBuilder(builder);
        
        mPushAgent.addLocalNotification(localNotification);
//        Log.e("PushAgent","add  local  notification ");
       
	};
}
