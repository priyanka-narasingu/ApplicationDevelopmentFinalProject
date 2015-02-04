package com.project.lussis6;


import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import android.os.Bundle;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.util.Log;
import android.widget.TabHost;
import android.app.TabActivity;
import android.widget.TabHost.OnTabChangeListener;
import android.os.Bundle;
import android.preference.PreferenceManager.OnActivityResultListener;
import android.view.Menu;
import android.view.MenuItem;

public class Department_Home extends TabActivity implements OnTabChangeListener{

	TabHost tabHost;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }	
        
		setContentView(R.layout.activity_department);
		tabHost = getTabHost();
        
        // Set TabChangeListener called when tab changed
        tabHost.setOnTabChangedListener(this);
     
        TabHost.TabSpec spec;
        Intent intent;
   
         /************* TAB1 ************/
        // Create  Intents to launch an Activity for the tab (to be reused)
        intent = new Intent().setClass(this, Dep_Approval_Tab.class);
        spec = tabHost.newTabSpec("First").setIndicator("Approval")
                      .setContent(intent);
         
        //Add intent to tab
        tabHost.addTab(spec);
   
        /************* TAB2 ************/
        SharedPreferences prefs = getSharedPreferences("com.project.lussis6", Context.MODE_PRIVATE);
		String deptCode = prefs.getString("deptCode", null);
		Log.i("Dept Code FROM INtent", deptCode);
        boolean result=Delegation.checkDelegate(deptCode);
              
        if(result)
        {
        	 intent = new Intent().setClass(this, Dep_DelegateRemove.class);
             spec = tabHost.newTabSpec("Second").setIndicator("Delegates")
                           .setContent(intent);  
             tabHost.addTab(spec);
             }
        else  {
        	intent = new Intent().setClass(this, Dep_DelegateGrant.class);
            spec = tabHost.newTabSpec("Second").setIndicator("Delegates")
                          .setContent(intent);  
            tabHost.addTab(spec);	
		}
        
        // Set drawable images to tab
//        tabHost.getTabWidget().getChildAt(1).setBackgroundResource(R.drawable.);
//        tabHost.getTabWidget().getChildAt(2).setBackgroundResource(R.drawable.tab3);
           
        // Set Tab1 as Default tab and change image   
        tabHost.getTabWidget().setCurrentTab(0);
	}

	
	@Override
	public void onTabChanged(String tabId) {
		// TODO Auto-generated method stub
		
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.dep__approval__tab, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			logOut();
			return true;
		}
		else if(id==R.id.refresh){
			finish();
			Intent i=new Intent(this,Department_Home.class);
			startActivity(i);
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	void logOut() {
        Intent intent = new Intent(this, Login.class);
        intent.putExtra("finish", true);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP); // To clean up all activities
        startActivity(intent);
        finish();
   }
	

//	public void onActivityResult (int requestCode, int resultCode, Intent data){
//		
//		if (resultCode==RESULT_OK){
//			//deptadapter.notify();
//			
////			finish();
////			Intent i=new Intent(this,Department_Home.class);
////			startActivity(i);
//			recreate();
//		}
//	}
}
