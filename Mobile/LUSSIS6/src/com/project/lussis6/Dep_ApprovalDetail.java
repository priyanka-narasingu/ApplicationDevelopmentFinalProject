package com.project.lussis6;

import java.util.List;
import com.project.lussis6.R;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class Dep_ApprovalDetail extends Activity implements OnClickListener{
    ListView lv;
    TextView t3;
    TextView t4;
    Button bt1;
    Button bt2;
    EditText et1;
    List<RequestDetails> reqDetailList;
    ListAdapter Adapter =null;
    String requestId;
    String approveby;
    

	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }
        
		setContentView(R.layout.activity_dep__approval__detail);
		lv = (ListView)findViewById(R.id.listView1);
		t3 = (TextView)findViewById(R.id.textView3);
		t4 = (TextView)findViewById(R.id.textView4);
		bt1 = (Button)findViewById(R.id.accept);
		bt2 = (Button)findViewById(R.id.reject);
		
		Bundle bundle = getIntent().getExtras();
		
		
		if (bundle == null){
			return;
		}
		else{
			requestId = bundle.getString("requestID");
			String employeename =bundle.getString("employeeName");
			String datecreated = bundle.getString("dateCreated");

			t3.setText(employeename);
	        t4.setText(datecreated);
		}
		
		getReqDetailList();
	
		SharedPreferences prefs = getSharedPreferences("com.project.lussis6", MODE_PRIVATE);
		approveby = prefs.getString("employeeID", null);
		
		bt1.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				
				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();

				} else{   					
					
					new AsyncTask<Void, Void, String>() {
						@Override
						protected String doInBackground(Void... params) {


							return Requests.updateRequestsAccept(requestId,approveby);


						}		  	          	  	          


						@Override
						protected void onPostExecute(String result) {

							Log.i("Result ***", result);

							if (result.trim().equals("true")){

								new AlertDialog.Builder(Dep_ApprovalDetail.this)
								.setTitle("Success")
								.setMessage("Request has been approved")
								.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {

									public void onClick(DialogInterface dialog, int which) { 
										Toast.makeText(Dep_ApprovalDetail.this, "Details Updated", Toast.LENGTH_LONG).show();
										finish();
									}
								})
								.setIcon(android.R.drawable.ic_dialog_alert)
								.show();

								setResult(RESULT_OK);	
								
							} else{
								
								new AlertDialog.Builder(Dep_ApprovalDetail.this)
								.setTitle("Failed to update")
								.setMessage("Request update failed. Please try again later.")
								.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
									public void onClick(DialogInterface dialog, int which) { 
										finish();
									}
								})					  	    
								.setIcon(android.R.drawable.ic_dialog_alert)
								.show();	
							}



						}
					}.execute();
					
										
//
//					StrictMode.setThreadPolicy(ThreadPolicy.LAX);
//
//					Requests.updateRequestsAccpet(requestId,approveby);
//
//					new AlertDialog.Builder(Dep_ApprovalDetail.this)
//					.setTitle("Success")
//					.setMessage("Request has been approved")
//					.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
//
//						public void onClick(DialogInterface dialog, int which) { 
//							Toast.makeText(Dep_ApprovalDetail.this, "Details Updated", Toast.LENGTH_LONG).show();
//							finish();
//						}
//
//					})
//					.setIcon(android.R.drawable.ic_dialog_alert)
//					.show();
//
//					setResult(RESULT_OK);

				}
			     
			}
		});
		
		
		
		bt2.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				
				
				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();

				} else{   

					et1 = (EditText)findViewById(R.id.editText1);

					Requests.updateRequestsReject(requestId, et1.getText().toString());
					

					
					new AsyncTask<Void, Void, String>() {
						@Override
						protected String doInBackground(Void... params) {

							return Requests.updateRequestsReject(requestId, et1.getText().toString());


						}		  	          	  	          


						@Override
						protected void onPostExecute(String result) {

							Log.i("Result ***", result);

							if (result.trim().equals("true")){
								
								new AlertDialog.Builder(Dep_ApprovalDetail.this)
								.setTitle("Success")
								.setMessage("Request has been rejected")
								.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {

									public void onClick(DialogInterface dialog, int which) { 
										Toast.makeText(Dep_ApprovalDetail.this, "Details Updated", Toast.LENGTH_LONG).show();
										finish();
									}

								})
								.setIcon(android.R.drawable.ic_dialog_alert)
								.show();

								setResult(RESULT_OK);

								
							} else{
								
								new AlertDialog.Builder(Dep_ApprovalDetail.this)
								.setTitle("Failed to update")
								.setMessage("Request rejection failed to update. Please try again later.")
								.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
									public void onClick(DialogInterface dialog, int which) { 
										finish();
									}
								})					  	    
								.setIcon(android.R.drawable.ic_dialog_alert)
								.show();	
							}



						}
					}.execute();
					
					//////////////
//					
//					StrictMode.setThreadPolicy(ThreadPolicy.LAX);
//					et1 = (EditText)findViewById(R.id.editText1);
//					Requests.updateRequestsReject(requestId, et1.getText().toString());
//					//Toast.makeText(Dep_ApprovalDetail.this, "Details Updated", Toast.LENGTH_LONG).show();
//
//
//					new AlertDialog.Builder(Dep_ApprovalDetail.this)
//					.setTitle("Success")
//					.setMessage("Request has been rejected")
//					.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
//
//						public void onClick(DialogInterface dialog, int which) { 
//							Toast.makeText(Dep_ApprovalDetail.this, "Details Updated", Toast.LENGTH_LONG).show();
//							finish();
//						}
//
//					})
//					.setIcon(android.R.drawable.ic_dialog_alert)
//					.show();
//
//					setResult(RESULT_OK);
					//finish();

				}
		        
	       
		}
			
		});
		
		}
	
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.dep__approval__detail, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
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
	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Dep_ApprovalDetail.this)
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
	public void onClick(View arg0) {
		
	}
	
public void getReqDetailList(){
		
		new AsyncTask<Void, Void, List<RequestDetails>>(){
            @Override
            protected List<RequestDetails> doInBackground(Void... params) {
            	reqDetailList = RequestDetails.getRequestDetailList(requestId);
                return reqDetailList;
            }
            
            @Override
            protected void onPostExecute(List<RequestDetails> reqDetailList) {
            	Adapter = new SimpleAdapter(getApplicationContext(), reqDetailList, R.layout.row_request_detail,new String[]{"ItemDescription","Unitofmeasure","Quantity"}, 
        				new int[]{R.id.ItemDescription, R.id.Unitofmeasure, R.id.Quantity});
            	lv.setAdapter(Adapter);
   
            }
		}.execute();
       }
	
}
