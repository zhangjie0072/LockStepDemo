package com.Uminton.Streetball;






import android.app.Activity;
import android.app.Notification;  
import android.app.NotificationManager;  
import android.app.PendingIntent;
import android.content.Intent;
import android.net.Uri;  
import android.os.Bundle;  
import android.provider.MediaStore.Audio;  
import android.support.v4.app.NotificationCompat;
import android.util.Log;
import android.view.View;  
import android.widget.Button;  
import android.widget.TextView;

public class MyAlarm extends Activity {  
  

    public static final int NOTIFICATION_ID=1;   
      
    @Override  
    protected void onCreate(Bundle savedInstanceState) {  
         super.onCreate(savedInstanceState);  
         setContentView(R.layout.alarm);  
         
         
        // create the instance of NotificationManager  
        final NotificationManager nm=(NotificationManager) getSystemService(NOTIFICATION_SERVICE); 
        
        // create the instance of Notification  
        Notification n=new Notification();  
        n.sound=Uri.withAppendedPath(Audio.Media.INTERNAL_CONTENT_URI, "20");  
        // Post a notification to be shown in the status bar  
        nm.notify(NOTIFICATION_ID, n);  
          
        /* display some information */  
        TextView tv=(TextView)findViewById(R.id.tv);  
        
        /* the button by which you can cancel the alarm */  
        Button btnCancel=(Button)findViewById(R.id.btn);  
        btnCancel.setOnClickListener(new View.OnClickListener() {  
				@Override  
				public void onClick(View arg0) {  
					nm.cancel(NOTIFICATION_ID);  
					finish();  
				}  
			});  
    }

    public PendingIntent getDefalutIntent(int flags){  
        PendingIntent pendingIntent= PendingIntent.getActivity(this, 1, new Intent(), flags);  
        return pendingIntent;  
    }  
      
    
}  

