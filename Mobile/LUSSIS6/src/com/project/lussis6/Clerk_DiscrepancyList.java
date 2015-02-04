package com.project.lussis6;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.AdapterView.AdapterContextMenuInfo;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class Clerk_DiscrepancyList extends Activity implements OnItemClickListener{
SharedPreferences pref;
ListView lv;
double totalAmoumt;
boolean status;
TextView error;
Button submit;
List<DiscrepancyDetails> DiscrepancyList;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }	
        
		setContentView(R.layout.activity_clerk__discrepancy_list);
		error=(TextView)findViewById(R.id.nolist);
		submit=(Button)findViewById(R.id.submit);
		pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
		DiscrepancyList=getStockList();
		lv=(ListView)findViewById(R.id.discrepancyList);
		lv.setAdapter(new SimpleAdapter(getApplicationContext(), DiscrepancyList, R.layout.row_discrepancy, 
				new String[]{"ItemDescription","Quantity"},
                new int[]{ R.id.itemDescription,R.id.quantity}));
		
		lv.setOnItemClickListener(this);
		
		registerForContextMenu(lv);
		 
		submit.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
//				Discrepancy d=new Discrepancy("AR175410", totalAmoumt);
//				Discrepancy.insert(d);
//				for (int i = 0; i < DiscrepancyList.size(); i++) {
//					DiscrepancyDetails dd=DiscrepancyList.get(i);
//					DiscrepancyDetails.insert(dd);
//				}
				
				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();

				} else{   	

					addDiscrepancies();
					SharedPreferences.Editor editor = pref.edit();
					status=true;
					editor.clear();
					editor.commit();
					createAlert("Updated","Items Have been Updated!!!");

				}
			}
		});
	}
	

	
	
	public void createAlert(String title, String message)
	{
		new AlertDialog.Builder(this)
		.setTitle(title)
		.setMessage(message)
		.setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
			
			@Override
			public void onClick(DialogInterface dialog, int which) {
				// TODO Auto-generated method stub
				if(status){
					setResult(RESULT_OK);
					finish();
				}
			}
		})
		.setIcon(android.R.drawable.ic_dialog_alert)
		.show();
	}
	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Clerk_DiscrepancyList.this)
		.setTitle("Network service")
		.setMessage("There is no network service. Please ensure network service and try again")
		.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int which) { 
				
			}
		})					  	    
		.setIcon(android.R.drawable.ic_dialog_alert)
		.show();	
	}
	
	private void addDiscrepancies() {
    	
    	new AsyncTask<Void, Void, Void>() {
            @Override
            protected Void doInBackground(Void... param) {
            	Log.i("Thread","Pass1");
            	String resutl;
            	SharedPreferences prefs = getSharedPreferences("com.project.lussis6", Context.MODE_PRIVATE);
				String employeeID = prefs.getString("employeeID", null);
				Log.i("Dept Code", employeeID);
            	Discrepancy d=new Discrepancy(employeeID, totalAmoumt);
				resutl=Discrepancy.insert(d);
				Log.i("Status",resutl);
				for (int i = 0; i < DiscrepancyList.size(); i++) {
					DiscrepancyDetails dd=DiscrepancyList.get(i);
					Log.i("dd",dd.toString());
					DiscrepancyDetails.insert(dd);
				}
				
				Log.i("Thread","Pass2");
				return null;
            	
            }
        }.execute();
        
    	
    }
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.clerk__discrepancy_list, menu);
		return true;
	}
	
	@Override
	public void onCreateContextMenu(ContextMenu menu, View v, ContextMenuInfo menuInfo){
		super.onCreateContextMenu(menu, v, menuInfo);
		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.context_menu_discrepancy, menu);
	}
	
	@Override
	public boolean onContextItemSelected(MenuItem item){
		switch (item.getItemId()){
		case R.id.itemDelete:
			Toast.makeText(this, "Item removed from list", Toast.LENGTH_SHORT).show();
			AdapterContextMenuInfo info = (AdapterContextMenuInfo) item.getMenuInfo();
			int index = info.position;			
			DiscrepancyList.remove(index);		
			lv.setAdapter(new SimpleAdapter(getApplicationContext(), DiscrepancyList, R.layout.row_discrepancy, 
					new String[]{"ItemDescription","Quantity"},
	                new int[]{ R.id.itemDescription,R.id.quantity}));
			
			Log.i("Discrepancy List", DiscrepancyList.toString());
			JSONArray ja=new JSONArray();			
			for (int i=0;i<DiscrepancyList.size();i++)
			{
				JSONObject jd=new JSONObject();
				try{
					jd.put("ItemDescription", DiscrepancyList.get(i).get("ItemDescription"));
					jd.put("ItemCode", DiscrepancyList.get(i).get("ItemCode"));
					jd.put("Quantity",DiscrepancyList.get(i).get("Quantity"));
					jd.put("Amount", DiscrepancyList.get(i).get("Amount"));
					jd.put("IsAdded", DiscrepancyList.get(i).get("IsAdded"));
					jd.put("Reason", DiscrepancyList.get(i).get("Reason"));

				}
				catch(Exception e)
				{
					e.printStackTrace();
				}
				ja.put(jd);
			}
			
//			JSONArray obj = new JSONArray();
//			((List<DiscrepancyDetails>) obj).addAll(DiscrepancyList); 
//			
//			JSONArray a;
//			try {
//				a = new JSONArray(obj);`
//			} catch (JSONException e) {
//				// TODO Auto-generated catch block
//				e.printStackTrace();
//			}
			SharedPreferences.Editor editor = pref.edit();
			editor.putString("AA", ja.toString());
			editor.commit();
			
			return true;
		default:
			return super.onContextItemSelected(item);
				
		}
	}
	
	
	public List<DiscrepancyDetails> getStockList() {
		List<DiscrepancyDetails> list = new ArrayList<DiscrepancyDetails>();
		
		try {
			
			JSONArray a = new JSONArray(pref.getString("AA", "a"));
			Log.i("----JSON objects-----", a.toString());
			
			try {
	            for (int i =0; i<a.length(); i++) {
	                JSONObject b = a.getJSONObject(i);
	                list.add(new DiscrepancyDetails(
	                		b.getString("ItemDescription"),
	                		b.getString("ItemCode"), 
	                		Integer.valueOf(b.getString("Quantity")), 
	                		Double.valueOf(b.getString("Amount")), 
	                		b.getString("IsAdded"), 
	                		b.getString("Reason")));  
	                Log.i("flag",b.getString("Quantity"));
//	                if(b.getString("IsAdded").equals("true")){
	                totalAmoumt+=b.getDouble("Amount");
//	                Log.i("Status","adding");
//	                }
//	                else
//	                {
//	                	 totalAmoumt-=b.getDouble("Amount");
//	                }
	            }
	        } catch (Exception e) {
	            Log.e("Department List", "JASONArray error");
	           
	        }

			Log.i("JSON", Double.toString(totalAmoumt));		    
		} catch (Exception e) {
			Log.i("JSON", "Error department list");
			 error.setText("There is no Discrepancy List");
			 submit.setEnabled(false);
		}
		
		
        return list;
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
	public void onItemClick(AdapterView<?> av, View view, int position, long id) {
		// TODO Auto-generated method stub
		DiscrepancyDetails dd = (DiscrepancyDetails) av.getAdapter().getItem(position); 
		
		Toast.makeText(getApplicationContext(), dd.get("ItemCode") + " selected", Toast.LENGTH_SHORT).show();
		
		
	}


}
