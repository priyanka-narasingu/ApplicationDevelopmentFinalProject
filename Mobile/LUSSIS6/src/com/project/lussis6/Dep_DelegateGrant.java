package com.project.lussis6;

import java.util.List;



import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;
 
public class Dep_DelegateGrant extends Activity implements OnItemClickListener{
	EditText searchEditText;
	Employee employee;
	ListView employeeListView;
	List<Employee> eList;
	List<Employee> employees;
	SimpleAdapter adapter;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
		if (finish) {
			startActivity(new Intent(getApplicationContext(), Login.class));
			finish();
			return;
		}
		
		setContentView(R.layout.activity_dep__delegate__tab);
		searchEditText=(EditText)findViewById(R.id.searchEditText);
		
		 employeeListView= (ListView) findViewById(R.id.employeeList);
		 employeeListView.setOnItemClickListener(this);
			// retrieve from your activity
			SharedPreferences prefs = getSharedPreferences("com.project.lussis6", Context.MODE_PRIVATE);
			String deptCode = prefs.getString("deptCode", null);
			Log.i("Dept Code", deptCode);
			getEmployees(deptCode);
		 searchEditText.addTextChangedListener(new TextWatcher() {
				
				@Override
				public void onTextChanged(CharSequence s, int start, int before, int count) {
					// TODO Auto-generated method stub
					
					
				}
				
				@Override
				public void beforeTextChanged(CharSequence s, int start, int count,
						int after) {
					// TODO Auto-generated method stub
					
				}
				
				@Override
				public void afterTextChanged(Editable s) {
					// TODO Auto-generated method stub
					String txt=searchEditText.getText().toString();
					adapter.getFilter().filter(txt);
				}
			});
		
			
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.dep__delegate__tab, menu);
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
	
	
	public void  getEmployees(final String deptCode) {
		
		
		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();

		} else{   

			new AsyncTask<Void, Void, List<Employee>>(){

				@Override
				protected List<Employee> doInBackground(Void...parms) {
					employees=Employee.getEmployeeList(deptCode);
					return employees;
				}
				protected void onPostExecute(List<Employee> list ) {
					adapter=new SimpleAdapter(getApplicationContext(), list, 
							R.layout.simplerow, 
							new String[]{"EmployeeName"},
							new int[]{ R.id.textView1});
					employeeListView.setAdapter(adapter); 

				}
			}.execute();

		}
	}
	
	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Dep_DelegateGrant.this)
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
	public void onItemClick(AdapterView<?> parent, View view, int position,
			long id) {
		
		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();

		} else{   

			Employee s = (Employee) parent.getAdapter().getItem(position);
			Toast.makeText(getApplicationContext(), s.get("EmployeeName") + " selected",
					Toast.LENGTH_SHORT).show();
			//finish();

			Intent intent=new Intent(this, Dep_DelegateGrant_Detail.class);
			intent.putExtra("EmployeeName", s.get("EmployeeName"));
			intent.putExtra("EmployeeID", s.get("EmployeeID"));

			startActivityForResult(intent, 101);

		}

		
	}
	public void onActivityResult (int requestCode, int resultCode, Intent data){
		
		if (resultCode==RESULT_OK && requestCode==101){
			//deptadapter.notify();
			//recreate();
			finish();
			Intent i=new Intent(this,Department_Home.class);
			startActivity(i);
		}
	}
	
}
