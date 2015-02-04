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
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Adapter;
import android.widget.AdapterView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.TextView;

public class Dep_Approval_Tab extends Activity implements OnItemClickListener{
	ListView t;
	List<Requests> list;
	ListAdapter deptadapter = null;
	Adapter approveadapter = null;
	String deptcode;
	TextView tv;
	final static int DETAILS = 101;
    
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }
        
		setContentView(R.layout.activity_dep__approval__tab);
		t = (ListView)findViewById(R.id.listView1);
		Log.i("approve", "Arrived");
		
		SharedPreferences prefs = getSharedPreferences("com.project.lussis6", MODE_PRIVATE);
		Log.i("pref", "Arrived");
		deptcode = prefs.getString("deptCode", null);
		Log.i("dept", "deptcode");
		
		getReqList();
		tv =(TextView)findViewById(R.id.textView1);
		
	
		t.setOnItemClickListener(this);
	}

	public void getReqList(){
		Log.i("getReq","KK");
		new AsyncTask<Void, Void, List<Requests>>(){
            @Override
            protected List<Requests> doInBackground(Void... params) {
                list = Requests.getRequestsList(deptcode);
                Log.i("list",list.toString());
                return list;
            }
            
            @Override
            protected void onPostExecute(List<Requests> list) {
            	
            	if (!list.isEmpty()){
            	deptadapter = new SimpleAdapter(getApplicationContext(), list, R.layout.row_request,new String[]{"EmployeeName","DateCreated"}, 
        				new int[]{R.id.EmployeeName, R.id.DateCreated});
            	t.setAdapter(deptadapter);
            	tv.setText("");
            	}
            	
            	else 
            	{
            		tv.setText("No request has been found");
            	}
            	
   
	}
		}.execute();
	}

	
	public void onItemClick(AdapterView<?> av, View v, int position, long id) {
		
		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();

		} else{   	

			Requests item = (Requests) av.getAdapter().getItem(position);
			Intent i = new Intent(this,Dep_ApprovalDetail.class);
			String requestID = item.get("RequestID");
			String employeeName = item.get("EmployeeName");
			String dateCreated = item.get("DateCreated");
			i.putExtra("requestID", requestID);
			i.putExtra("employeeName", employeeName);
			i.putExtra("dateCreated", dateCreated);
			startActivityForResult(i, DETAILS);

		}
	}
	
	public void onActivityResult (int requestCode, int resultCode, Intent data){
		
		if (resultCode==RESULT_OK && requestCode==DETAILS){
			//deptadapter.notify();
			//recreate();
			finish();
			Intent i=new Intent(this,Department_Home.class);
			startActivity(i);
		}
	}
	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Dep_Approval_Tab.this)
		.setTitle("Network service")
		.setMessage("There is no network service. Please ensure network service and try again")
		.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int which) { 
				
			}
		})					  	    
		.setIcon(android.R.drawable.ic_dialog_alert)
		.show();	
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.dep__approval__tab, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
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

}


	

