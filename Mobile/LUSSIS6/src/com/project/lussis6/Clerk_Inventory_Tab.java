package com.project.lussis6;

import java.util.List;


import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
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
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

public class Clerk_Inventory_Tab extends Activity implements OnItemClickListener,OnClickListener{

	List<Stock> collectionPoints;
	ListView lv;
	EditText search;
	SimpleAdapter ad;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }		
		
		setContentView(R.layout.activity_clerk__inventory_tab);
		lv = (ListView) findViewById(R.id.stockList);
		getStockList();
		lv.setOnItemClickListener(this);
		Button b1=(Button)findViewById(R.id.button1);
		 search=(EditText)findViewById(R.id.search);
		search.addTextChangedListener(new TextWatcher() {
			
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
				String txt=search.getText().toString();
				ad.getFilter().filter(txt);
			}
		});
		b1.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent i=new Intent(getApplicationContext(),Clerk_DiscrepancyList.class);
				startActivity(i);
			}
		});
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.clerk__inventory_tab, menu);
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
	
	
	   private void getStockList() {
	    	
	    	new AsyncTask<Void, Void, List<Stock>>() {
	            @Override
	            protected List<Stock> doInBackground(Void... params) {
	            	Log.i("Thread","Pass1");
	                return Stock.getStockList();
	                
	            }
	            @Override
	            protected void onPostExecute(List<Stock> list) {
	            	Stock s=list.get(0);
	            	String a=s.get("ItemDescription");
	            	Log.i("Thread",a);
	            	ad=new SimpleAdapter(getApplicationContext(), list, R.layout.row, 
	        				new String[]{"ItemDescription"},
	                        new int[]{ R.id.StockListRow});
	        		lv.setAdapter(ad);
	        		Log.i("Thread","Pass2");
	            }
	        }.execute();
	        
	    	
	    }

	   
	   private void alertNoNetwork(){
		   new AlertDialog.Builder(Clerk_Inventory_Tab.this)
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
	public void onItemClick(AdapterView<?> parent, View view, int position,
			long id) {

		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();

		} else{   	

			Stock stk=(Stock) parent.getAdapter().getItem(position);
			Toast.makeText(getApplicationContext(), stk.get("ItemCode"), Toast.LENGTH_LONG).show();
			Intent i = new Intent(this, Clerk_InventoryDetail.class);
			i.putExtra("ItemCode", stk.get("ItemCode"));
			startActivity(i);

		}
	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		
	}
}
