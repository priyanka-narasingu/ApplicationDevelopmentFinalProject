package com.project.lussis6;

import java.util.List;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class Clerk_Issue_Tab extends Activity implements OnItemClickListener {
	
	final static int ISSUE_STATIONERY = 101;
	
	List<CollectionPoints> collectionPoints;
	ListView lv;
	

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		boolean finish = getIntent().getBooleanExtra("finish", false);
		if (finish) {
			startActivity(new Intent(getApplicationContext(), Login.class));
			finish();
			return;
		}

		setContentView(R.layout.activity_clerk__issue__tab);

		lv = (ListView) findViewById(R.id.listViewCollectionPoint1);		

		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();    		

		} else {   	
			
			getCollectionPtList();
		}

		lv.setOnItemClickListener(this);

		SharedPreferences prefs = getSharedPreferences("com.project.lussis6", Context.MODE_PRIVATE);
		String deptCode = prefs.getString("deptCode", null);
		Log.i("Dept Code", deptCode);


	}

    
    @Override
    public void onItemClick(AdapterView<?> av, View v, int position, long id) {
    	
    	if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

    		alertNoNetwork();

    	} else{   	


    		CollectionPoints cPt = (CollectionPoints) av.getAdapter().getItem(position);
    		//Toast.makeText(getApplicationContext(), cPt.get("collectionPointCode") + " selected", Toast.LENGTH_SHORT).show();

    		Intent intent = new Intent(Clerk_Issue_Tab.this, Clerk_IssueDepartments.class);
    		intent.putExtra("collectionPointCode", cPt.get("collectionPointCode"));
    		startActivityForResult(intent, ISSUE_STATIONERY);

    	}
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
	
	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Clerk_Issue_Tab.this)
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
	
	
    private void getCollectionPtList() {
    	    	
    	new AsyncTask<Void, Void, List<CollectionPoints>>() {
            @Override
            protected List<CollectionPoints> doInBackground(Void... params) {
                return CollectionPoints.getCollectionPointsList();
            }
            @Override
            protected void onPostExecute(List<CollectionPoints> list) {
        		
        		lv.setAdapter(new SimpleAdapter(getApplicationContext(), list, R.layout.row_collection_point, 
        				new String[]{"collectionPointName", "collectionTime"},
                        new int[]{ R.id.textViewCollectionPt, R.id.textViewCollectionTime1}));	
        		        		
        		
            }
        }.execute();
        

//    	StrictMode.setThreadPolicy(ThreadPolicy.LAX);
//    	collectionPoints = CollectionPoints.getCollectionPointsList();
//    	lv.setAdapter(new SimpleAdapter(getApplicationContext(), collectionPoints, R.layout.row_collection_point, 
//				new String[]{"collectionPointName", "collectionTime"},
//                new int[]{ R.id.textViewCollectionPt, R.id.textViewCollectionTime1}));
   	
    	
    	/*
    	collectionPoints = new ArrayList<CollectionPoints>();
    	//manual data
    	collectionPoints.add(new CollectionPoints("ENS", "Engineering School-manual data", "11.00am"));
    	collectionPoints.add(new CollectionPoints("MDS", "Medical School-manual data", "9.30am"));
    	collectionPoints.add(new CollectionPoints("MGM", "Management School-manual data", "11.00am"));
    	collectionPoints.add(new CollectionPoints("SCI", "Science School-manual data", "9.30am"));
    	collectionPoints.add(new CollectionPoints("SSA", "Stationery Store-Administration Building-manual data", "9.30am"));
    	collectionPoints.add(new CollectionPoints("UHC", "University Hospital-manual data", "11.30am"));
    	*/
    	
    }
 
	
}
