package com.project.lussis6;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ListActivity;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class Clerk_IssueDepartments extends Activity implements OnItemClickListener {

	final static int ISSUE_STATIONERY_LIST = 102;
	List<Departments> departmentList;
	ListView lv;
	TextView txtEmptyDeptList;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }
		
		setContentView(R.layout.activity_clerk__issue_departments);
		
		lv = (ListView) findViewById(R.id.listViewDepartment);
		txtEmptyDeptList = (TextView) findViewById(R.id.textViewEmptyDeptList);
		txtEmptyDeptList.setText("");
		
		Intent intent = getIntent();
		String collectionPointCode = intent.getStringExtra("collectionPointCode");
		
		//Toast.makeText(this, collectionPointCode + " saved", Toast.LENGTH_SHORT).show();
		
		
		getDeptList(collectionPointCode);
		lv.setOnItemClickListener(this);
				
		
//        setListAdapter(new SimpleAdapter
//               (this, departmentList, android.R.layout.simple_list_item_1,
//                 new String[]{"deptName"},
//                 new int[]{ android.R.id.text1}));

		
	}


	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.clerk__issue_departments, menu);
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
	public void onItemClick(AdapterView<?> av, View v, int position, long id) {


		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();
		
		} else{		 

			Departments dept = (Departments) av.getAdapter().getItem(position);
			//Toast.makeText(getApplicationContext(), dept.get("deptCode") + " selected", Toast.LENGTH_SHORT).show();

			Intent intent = new Intent(Clerk_IssueDepartments.this, Clerk_IssueItems.class);
			intent.putExtra("deptCode", dept.get("deptCode"));
			intent.putExtra("deptName",  dept.get("deptName"));  
			intent.putExtra("deptRepName",  dept.get("deptRepName")); 
			intent.putExtra("deptCollectionPin",dept.get("deptCollectionPin"));

			startActivityForResult(intent, ISSUE_STATIONERY_LIST);
		}

	}

private void alertNoNetwork(){
	
	new AlertDialog.Builder(Clerk_IssueDepartments.this)
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

	
	
private void getDeptList() {
    	
    	
	departmentList = new ArrayList<Departments>();
    	
	new AsyncTask<Void, Void, List<Departments>>() {
        @Override
        protected List<Departments> doInBackground(Void... params) {
            return Departments.getDepartmentList();
        }
        @Override
        protected void onPostExecute(List<Departments> list) {
        	
        	
        	if(!list.isEmpty()){
        		
        		lv.setAdapter(new SimpleAdapter(getApplicationContext(), list, R.layout.row_department, 
        				new String[]{"deptName", "deptContactNo"},
                        new int[]{ R.id.textViewDepartment, R.id.textViewCollectionPt2}));	
				
			} else {
				
				txtEmptyDeptList.setText("No depts for this collection point");				
			}        	      	
    		
        }
    }.execute();
	
		
	/*
	//manual data
	departmentList.add(new Departments("ARCH", "Architecture Department-manual data", "Stationery Store", "Test", 1234));
	departmentList.add(new Departments("ARTS", "Arts Department-manual data", "Stationery Store", "Test2", 2345));
	departmentList.add(new Departments("BUSI", "Business Department-manual data", "Stationery Store", "Test3", 3456));
	*/
	
    }
	
private void getDeptList(final String collectionPointCode) {
	
	

	if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

		alertNoNetwork();	

	} else{

		departmentList = new ArrayList<Departments>();

		new AsyncTask<Void, Void, List<Departments>>() {
			@Override
			protected List<Departments> doInBackground(Void... params) {
				return Departments.getDepartmentList(collectionPointCode);
			}
			@Override
			protected void onPostExecute(List<Departments> list) {

				lv.setAdapter(new SimpleAdapter(getApplicationContext(), list, R.layout.row_department, 
						new String[]{"deptName", "deptContactNo"},
						new int[]{ R.id.textViewDepartment, R.id.textViewCollectionPt2}));	
			}
		}.execute();

	}

	/*
	//manual data
	departmentList.add(new Departments("ARCH", "Architecture Department-manual data", "Stationery Store", "Test", 1234));
	departmentList.add(new Departments("ARTS", "Arts Department-manual data", "Stationery Store", "Test2", 2345));
	departmentList.add(new Departments("BUSI", "Business Department-manual data", "Stationery Store", "Test3", 3456));
	*/
	
    }



}
