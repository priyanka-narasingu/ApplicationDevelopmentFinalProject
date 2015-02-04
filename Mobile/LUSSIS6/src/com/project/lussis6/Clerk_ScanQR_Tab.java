package com.project.lussis6;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.Toast;

public class Clerk_ScanQR_Tab extends Activity implements OnClickListener{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }	
        
		setContentView(R.layout.activity_clerk__scan_qr__tab);
		 
		ImageButton scan=(ImageButton)findViewById(R.id.scan);
		scan.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent intent = new Intent("la.droid.qr.scan");
			    intent.putExtra("la.droid.qr.complete", true);
			    startActivityForResult(intent, 0);
			}
		});
	}
	
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
    	if(requestCode==0){
          if (resultCode==RESULT_OK && data.hasExtra("la.droid.qr.result") && data!=null) {
            String res = data.getExtras().getString("la.droid.qr.result");
            Toast.makeText(this, res, Toast.LENGTH_LONG).show();
           
            Intent i = new Intent(this, Clerk_InventoryDetail.class);
            i.putExtra("ItemCode", res);
            startActivity(i);
          }
    	}
    } 
    
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.clerk__scan_qr__tab, menu);
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
	

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		
	}
}
