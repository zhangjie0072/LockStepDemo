package com.Uminton.Streetball;

import com.umeng.message.PushAgent;
import com.umeng.message.UTrack;
import com.umeng.message.UmengMessageHandler;
import com.umeng.message.entity.UMessage;

import android.app.Application;
import android.app.Notification;
import android.content.Context;
import android.os.Handler;
import android.util.Log;
import android.widget.Toast;


public class MyApplication extends ourpalm.android.channels.Info.Ourpalm_Channels_Application {
	private static Context context;
   String TAG = "164";
   private PushAgent mPushAgent;
	@Override
	public void onCreate(){
		super.onCreate();
		context = getApplicationContext();

		mPushAgent = PushAgent.getInstance(context);
		mPushAgent.setDebugMode(true);
      
	}

	public static Context getContext(){
		return context;
	}
}
