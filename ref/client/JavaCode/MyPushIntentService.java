package com.tencent.freestyle;

import org.android.agoo.client.BaseConstants;
import org.json.JSONObject;

import android.content.Context;
import android.content.Intent;

import com.umeng.common.message.Log;
import com.umeng.message.UTrack;
import com.umeng.message.UmengBaseIntentService;
import com.umeng.message.entity.UMessage;

/**
 * Developer defined push intent service. 
 * Remember to call {@link com.umeng.message.PushAgent#setPushIntentServiceClass(Class)}. 
 * @author lucas
 *
 */
//完全自定义处理类
//参考文档的1.6.5
//http://dev.umeng.com/push/android/integration#1_6_5
public class MyPushIntentService extends UmengBaseIntentService{
	private static final String TAG = "PushAgent";//MyPushIntentService.class.getName();

	@Override
	protected void onMessage(Context context, Intent intent) {
		// 需要调用父类的函数，否则无法统计到消息送达
		super.onMessage(context, intent);
		try {
			//可以通过MESSAGE_BODY取得消息体
			String message = intent.getStringExtra(BaseConstants.MESSAGE_BODY);
			UMessage msg = new UMessage(new JSONObject(message));
			Log.e(TAG, "message="+message);    //消息体
			Log.e(TAG, "custom="+msg.custom);    //自定义消息的内容
			Log.e(TAG, "title="+msg.title);    //通知标题
			Log.e(TAG, "text="+msg.text);    //通知内容
			// code  to handle message here
			// ...
			
			// 对完全自定义消息的处理方式，点击或者忽略
			boolean isClickOrDismissed = true;
			if(isClickOrDismissed) {
				//完全自定义消息的点击统计
				//关闭消息统计
				UTrack.getInstance(getApplicationContext()).trackMsgClick(msg);
			} else {
				//完全自定义消息的忽略统计
				//关机消息统计
				UTrack.getInstance(getApplicationContext()).trackMsgDismissed(msg);
			}
			
			// 使用完全自定义消息来开启应用服务进程的示例代码
			// 首先需要设置完全自定义消息处理方式
			// mPushAgent.setPushIntentServiceClass(MyPushIntentService.class);
			// code to handle to start/stop service for app process
			JSONObject json = new JSONObject(msg.custom);
            String topic = json.getString("topic");
            Log.e(TAG, "topic="+topic);
			if(topic != null && topic.equals("appName:startService")) {
				// 在友盟portal上新建自定义消息，自定义消息文本如下
				//{"topic":"appName:startService"}
				if(Helper.isServiceRunning(context, NotificationService.class.getName())) 
					return; 
				Intent intent1 = new Intent(); 
				intent1.setClass(context, NotificationService.class); 
				context.startService(intent1);
			} else if(topic != null && topic.equals("appName:stopService")) {
				// 在友盟portal上新建自定义消息，自定义消息文本如下
				//{"topic":"appName:stopService"}
				if(!Helper.isServiceRunning(context, NotificationService.class.getName())) 
					return; 
				Intent intent1 = new Intent(); 
				intent1.setClass(context, NotificationService.class); 
				context.stopService(intent1);
			}
		} catch (Exception e) {
			Log.e(TAG, e.getMessage());
		}
	}
}
