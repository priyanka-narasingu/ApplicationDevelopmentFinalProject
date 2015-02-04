package com.project.lussis6;


import android.os.Bundle;
import android.content.Intent;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TabHost;
import android.app.TabActivity;
import android.widget.TabHost.OnTabChangeListener;

public class Clerk_Home extends TabActivity implements OnTabChangeListener{
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
		
		setContentView(R.layout.activity_clerk);
		// Get TabHost Refference
        tabHost = getTabHost();
         
        // Set TabChangeListener called when tab changed
        tabHost.setOnTabChangedListener(this);
     
        TabHost.TabSpec spec;
        Intent intent;
   
         /************* TAB1 ************/
        // Create  Intents to launch an Activity for the tab (to be reused)
        intent = new Intent().setClass(this, Clerk_Inventory_Tab.class);
        spec = tabHost.newTabSpec("First").setIndicator("Inventory")
                      .setContent(intent);
         
        //Add intent to tab
        tabHost.addTab(spec);
   
        /************* TAB2 ************/
        intent = new Intent().setClass(this, Clerk_ScanQR_Tab.class);
        spec = tabHost.newTabSpec("Second").setIndicator("Scan QR Code")
                      .setContent(intent);  
        tabHost.addTab(spec);
   
        /************* TAB3 ************/
        intent = new Intent().setClass(this, Clerk_Issue_Tab.class);
        spec = tabHost.newTabSpec("Third").setIndicator("Issue Stationery")
                      .setContent(intent);
        tabHost.addTab(spec);
        
   
//        // Set drawable images to tab
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
		getMenuInflater().inflate(R.menu.clerk__issue__tab, menu);
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
		return super.onOptionsItemSelected(item);
	}
	
	void logOut() {
        Intent intent = new Intent(this, Login.class);
        intent.putExtra("finish", true);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP); // To clean up all activities
        startActivity(intent);
        finish();
   }

}
