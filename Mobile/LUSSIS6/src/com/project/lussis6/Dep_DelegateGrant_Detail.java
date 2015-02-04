package com.project.lussis6;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;



import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.DatePickerDialog.OnDateSetListener;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class Dep_DelegateGrant_Detail extends Activity {
	private static final int DIALOG_START_DATE = 1;
	private static final int DIALOG_END_DATE = 2;    
	Button startDateButton;
	Button endDateButton;
	Button delegateButton;
	  private Calendar startDate = Calendar.getInstance(TimeZone.getTimeZone("GMT"));
	  private Calendar endDate=Calendar.getInstance(TimeZone.getTimeZone("Asia/Singapore"));
	  private Calendar dateToday=Calendar.getInstance(TimeZone.getTimeZone("Asia/Singapore"));
      private SimpleDateFormat dateFormatter = new SimpleDateFormat(
          "EEE, dd MMMM yyyy");
      String employeeName;
      String employeeID;
         TextView employeeNameTextView;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
		if (finish) {
			startActivity(new Intent(getApplicationContext(), Login.class));
			finish();
			return;
		}
		
		setContentView(R.layout.activity_dep__delegate_grant__detail);
		
		 startDateButton=(Button)findViewById(R.id.startDateBtn);
	     endDateButton=(Button)findViewById(R.id.endDateBtn);
	     delegateButton=(Button)findViewById(R.id.delegateBtn);
	        employeeNameTextView=(TextView)findViewById(R.id.employeeNametxt);
	        employeeName=getIntent().getExtras().get("EmployeeName").toString();
	        Log.i("INTENT", employeeName);
	        employeeNameTextView.setText(employeeName);
	        employeeID=getIntent().getExtras().get("EmployeeID").toString();
	        
	        long gmtTime = startDate.getTime().getTime();
	        long timezoneAlteredTime = gmtTime + TimeZone.getTimeZone("Asia/Singapore").getRawOffset();
	        endDate.setTimeInMillis(timezoneAlteredTime);
	        dateFormatter.setTimeZone(TimeZone.getDefault());
	       // dateToday.setTimeInMillis(timezoneAlteredTime);
	        dateToday=(Calendar) startDate.clone();
	        dateToday.add(Calendar.DATE, -1);
	        Log.i("DATE TODAY", dateFormatter.format(dateToday.getTime()).toString());
			
	        
	        startDateButton.setText(dateFormatter.format(startDate.getTime()));
	        endDateButton.setText(dateFormatter.format(endDate.getTime()));
	       
	        startDateButton.setOnClickListener(new OnClickListener() {
				
				@Override
				public void onClick(View v) {
					showDialog(DIALOG_START_DATE);
					
				}
			});
	        endDateButton.setOnClickListener(new OnClickListener() {
				
				@Override
				public void onClick(View v) {
				
					showDialog(DIALOG_END_DATE);
					
				}
			});
	        delegateButton.setOnClickListener(new OnClickListener() {
				
				@Override
				public void onClick(View v) {
					
					
					if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			    		alertNoNetwork();

					} else{   


						if(endDate.before(startDate)){
							Log.i("ALERT DATE1", startDate.toString());
							new AlertDialog.Builder(Dep_DelegateGrant_Detail.this)
							.setTitle("Delegation Date")
							.setMessage("Delegation end date cannot be earlier than delegation start date!")
							.setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog, int which) { 

								}
							})
							.setIcon(android.R.drawable.ic_dialog_alert)
							.show();

						}
						else if(startDate.before(dateToday))
						{
							Log.i("ALERT DATE2", startDate.toString());
							Log.i("DATE TODAY", dateFormatter.format(dateToday.getTime()).toString());

							new AlertDialog.Builder(Dep_DelegateGrant_Detail.this)
							.setTitle("Delegation Date")
							.setMessage("Delegation start date cannot be earlier than today's date!")
							.setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog, int which) { 

								}
							})
							.setIcon(android.R.drawable.ic_dialog_alert)
							.show();

						}
						else if(endDate.after(startDate)) {
							Log.i("ALERT DATE3", startDate.toString());
							new AlertDialog.Builder(Dep_DelegateGrant_Detail.this)
							.setTitle("Delegate Employee")
							.setMessage("Are you sure you want to delegate to "+employeeName)
							.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog, int which) { 

									new AsyncTask<Void, Void, String>() {
										@Override
										protected String doInBackground(Void... params) {
											Log.i("Thread","Pass1");
											return updateDelegate(employeeID, employeeName, startDate.getTime(),  endDate.getTime());
										}
										@Override
										protected void onPostExecute(String result) {
											setResult(RESULT_OK);
											finish();
//											Intent intent= new Intent(Dep_DelegateGrant_Detail.this, Department_Home.class);
//											startActivity(intent);
											Log.i("Thread","Pass2");
										}
									}.execute();

								}
							})
							.setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog, int which) { 
									// do nothing
								}
							})
							.setIcon(android.R.drawable.ic_dialog_alert)
							.show();

						}


					}

					
					
				}
			});
	     
	}
	
	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Dep_DelegateGrant_Detail.this)
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
	

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.dep__delegate_grant__detail, menu);
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
    protected Dialog onCreateDialog(int id)
    {
    	switch (id)
        {
        case DIALOG_START_DATE:
       return new DatePickerDialog(this, new OnDateSetListener()
       {

           @Override
           public void onDateSet(DatePicker view, int year,
                   int monthOfYear, int dayOfMonth)
           {
        	   startDate.set(year, monthOfYear, dayOfMonth);
               startDateButton.setText(dateFormatter.format(startDate.getTime()));
               
           }
       }, startDate.get(Calendar.YEAR),
          startDate.get(Calendar.MONTH),
          startDate.get(Calendar.DAY_OF_MONTH));
        case DIALOG_END_DATE:
        	return new DatePickerDialog(this, new OnDateSetListener()
            {

                @Override
                public void onDateSet(DatePicker view, int year,
                        int monthOfYear, int dayOfMonth)
                {
                    
                	endDate.set(year, monthOfYear, dayOfMonth);
                   
                    endDateButton.setText(dateFormatter.format(endDate.getTime()));
                }
            },endDate.get(Calendar.YEAR),
              endDate.get(Calendar.MONTH),
              endDate.get(Calendar.DAY_OF_MONTH));
       }
       		return null;
    }
	
	
public String updateDelegate(String empID,String empName, Date start, Date end)	
{
	
	Log.i("UPDATE", start.toString());
	
	Delegation delegation= new Delegation();
	Delegation d =new Delegation();
	SimpleDateFormat dateFormatDB = new SimpleDateFormat("dd-MM-yyyy");
	//dateFormatDB.format(start.getTime());
	Log.i("TIME",dateFormatDB.format(end.getTime()));
	Log.i("TIME",dateFormatDB.format(start.getTime()));
	
	delegation.put("EmployeeID",empID);
	delegation.put("EmployeeName",empName);
	
	delegation.put("StartDate", dateFormatDB.format(start.getTime()));
	delegation.put("EndDate",dateFormatDB.format(end.getTime()));
	
	return	d.updateDelegation(delegation);
	//Toast.makeText(this,"Delegated to "+empName, Toast.LENGTH_LONG).show();
	
	
}


}
