package com.project.lussis6;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import android.R.string;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.format.DateFormat;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class Dep_DelegateRemove extends Activity {

	Delegation delegation= new Delegation();
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
		if (finish) {
			startActivity(new Intent(getApplicationContext(), Login.class));
			finish();
			return;
		}
		
		setContentView(R.layout.activity_dep__delegate__remove);
		TextView employeenameTextView=(TextView)findViewById(R.id.empNametxt);
		TextView startDateTextView=(TextView)findViewById(R.id.startTxt);
		TextView endDateTextView=(TextView)findViewById(R.id.endTxt);
		Button revokeButton=(Button)findViewById(R.id.revokeBtn);
		getDelegate();
		Log.i("DATE", delegation.get("StartDate").toString() );
		employeenameTextView.setText(delegation.get("EmployeeName").toString());
		
       SimpleDateFormat formatter = new SimpleDateFormat("dd-MM-yyyy");
       SimpleDateFormat displayDate=new SimpleDateFormat("EEE, dd MMMM yyyy");
       Date sDate=null;
       Date eDate=null;
		try {
			sDate = (Date) formatter.parse(delegation.get("StartDate").toString());
			eDate=(Date) formatter.parse(delegation.get("EndDate").toString());
			} 
		catch (ParseException e) {
			
			e.printStackTrace();
			}
	       Log.i("DATE", sDate.toString());
	       	
       startDateTextView.setText(displayDate.format(sDate.getTime()).toString());
       endDateTextView.setText(displayDate.format(eDate.getTime()).toString());
		revokeButton.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				
				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();    		

				} else {   	

					new AsyncTask<Void, Void, String>() {
						@Override
						protected String doInBackground(Void... params) {
							Log.i("Thread","Pass1");
							return revoke();

						}
						@Override
						protected void onPostExecute(String s) {
							Toast.makeText(Dep_DelegateRemove.this, "Delegation revoked", Toast.LENGTH_LONG).show();
							setResult(RESULT_OK);
							finish();
							Intent intent= new Intent(Dep_DelegateRemove.this, Department_Home.class);
							startActivity(intent);
							Log.i("Thread","Pass2");
						}
					}.execute();

				}
			}
		});
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.dep__delegate__remove, menu);
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
	
	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Dep_DelegateRemove.this)
		.setTitle("Network service")
		.setMessage("There is no network service. Please ensure network service and try again")
		.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int which) { 
				//finish();
			}
		})					  	    
		.setIcon(android.R.drawable.ic_dialog_alert)
		.show();	
	}
	
	
	void logOut() {
        Intent intent = new Intent(this, Login.class);
        intent.putExtra("finish", true);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP); // To clean up all activities
        startActivity(intent);
        finish();
   }
	
	
	public void getDelegate()
	{
		
		SharedPreferences prefs = getSharedPreferences("com.project.lussis6", Context.MODE_PRIVATE);
		String deptCode = prefs.getString("deptCode", null);
		Log.i("Dept Code", deptCode);
		delegation=Delegation.getDelegate(deptCode);
		
	}
	public String revoke()
	{
		String result;
		result=Delegation.revoke(delegation.get("EmployeeID").toString());
		
		return result;
	}
	
}
