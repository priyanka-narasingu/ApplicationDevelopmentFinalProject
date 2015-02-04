package com.project.lussis6;



import java.text.DecimalFormat;
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
import android.content.ClipData.Item;
import android.os.AsyncTask;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.TextView;

public class Clerk_InventoryDetail extends Activity implements OnClickListener{
	SharedPreferences pref;
	final static int []widgets = new int[]{R.id.code,R.id.des,R.id.loc,R.id.uom,R.id.qty};
    final static String []keys = new String[]{"ItemCode","ItemDescription","Location","UnitOfMeasure","Quantity"};
    String itemcode="";
    Double price;
    String itemDescription="";
    EditText added;
    EditText removed;
    EditText reason;
    AlertDialog.Builder adb;
    String addqty;
    String reasons;
    String removeqty;
    InputMethodManager imm ;
    DecimalFormat df=new DecimalFormat("#.##");
    boolean status=false;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }		
        
		setContentView(R.layout.activity_clerk__inventory_detail);
		
		Bundle bundle = getIntent().getExtras();
	    if (bundle != null && bundle.containsKey("ItemCode")) {
	    	itemcode=bundle.getString("ItemCode");
	    	Log.i("itemcode",itemcode);
	    	populateStock(itemcode);
//	        java.util.HashMap<String,String> p = (java.util.HashMap<String,String>) bundle.get("stock");
//	        for (int i=0; i<widgets.length; i++) {
//	            TextView et = (TextView) findViewById(widgets[i]);
//	            et.setText(p.get(keys[i]));
//	            
//	        }
//	        itemcode = p.get("ItemCode");
//	        price=Double.parseDouble(p.get("Price"));
//	        itemDescription=p.get("ItemDescription");
	    }
	     added=(EditText)findViewById(R.id.addqty);
	     removed=(EditText)findViewById(R.id.removeqty);
	     reason=(EditText)findViewById(R.id.comments);
	     adb=new AlertDialog.Builder(this);
	    Button add=(Button)findViewById(R.id.add);
	    Button remove=(Button)findViewById(R.id.remove);
	    
	    add.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				

				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();

				} else{   	


					addqty=added.getText().toString();
					reasons=reason.getText().toString();
					if (addqty.matches("")||reasons.matches(""))
					{

						createAlert("Missing Values", "Added Quantity or Reason is missing!");

					}
					else{
						Log.i("add", addqty);
						Integer i=Integer.parseInt(addqty);
						
						Double amount=Double.valueOf(df.format(i*price));
						Log.i("amount", amount.toString());
						DiscrepancyDetails dd=new DiscrepancyDetails(itemDescription,itemcode, i, amount, "true", reasons);

						JSONObject jd=new JSONObject();
						try{
							jd.put("ItemDescription", dd.get("ItemDescription"));
							jd.put("ItemCode", dd.get("ItemCode"));
							jd.put("Quantity", dd.get("Quantity"));
							jd.put("Amount", dd.get("Amount"));
							jd.put("IsAdded", dd.get("IsAdded"));
							jd.put("Reason", dd.get("Reason"));

						}
						catch(Exception e)
						{
							e.printStackTrace();
						}
						try {
							putSharedPref(jd);
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						status=true;
						createAlert("Item added", "Item has added to discrepancy list");
					}

				}

			}
		});
	    
	    
	    remove.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				
				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();

				} else{  
				
					removeqty=removed.getText().toString();
					reasons=reason.getText().toString();
					if (removeqty.matches("")||reasons.matches(""))
					{
						
					 	createAlert("Missing Values", "Removed Quantity or Reason is missing!");
					 	
					}
					else
					{
						Integer i=Integer.parseInt(removeqty);
						TextView currentqty=(TextView)findViewById(R.id.qty);
						Integer icurrentqty=Integer.parseInt(currentqty.getText().toString());
						if(icurrentqty<i)
						{
							createAlert("Wrong Quantity", "Removed Quantity is more than On Hand Quantity");
						}
						else{
							Log.i("qty",i.toString());
							Double amount=Double.valueOf(df.format(i*price));
							DiscrepancyDetails dd=new DiscrepancyDetails( itemDescription,itemcode, -1*i, amount, "false", reason.getText().toString());
							
							JSONObject jd=new JSONObject();
							try{
								jd.put("ItemDescription", dd.get("ItemDescription"));
								jd.put("ItemCode", dd.get("ItemCode"));
								jd.put("Quantity", dd.get("Quantity"));
								jd.put("Amount", dd.get("Amount"));
								jd.put("IsAdded", dd.get("IsAdded"));
								jd.put("Reason", dd.get("Reason"));
								
							}
							catch(Exception e)
							{
								e.printStackTrace();
							}
							try {
								putSharedPref(jd);
							} catch (JSONException e) {
								// TODO Auto-generated catch block
								e.printStackTrace();
							}
							status=true;
							createAlert("Item added", "Item has added to discrepancy list");
						}
					}
				}
			}
		});
	}

	
	private void alertNoNetwork(){
		new AlertDialog.Builder(Clerk_InventoryDetail.this)
		.setTitle("Network service")
		.setMessage("There is no network service. Please ensure network service and try again")
		.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int which) { 
				
			}
		})					  	    
		.setIcon(android.R.drawable.ic_dialog_alert)
		.show();	
	}
	
	
	public void createAlert(String title, String message)
	{
		imm = (InputMethodManager)getSystemService(
	    	      Context.INPUT_METHOD_SERVICE);
		new AlertDialog.Builder(this)
		.setTitle(title)
		.setMessage(message)
		.setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
			
			@Override
			public void onClick(DialogInterface dialog, int which) {
				// TODO Auto-generated method stub
				if(status){
					imm.hideSoftInputFromWindow(reason.getWindowToken(), 0);
					setResult(RESULT_OK);
					finish();
				}
			}
		})
		.setIcon(android.R.drawable.ic_dialog_alert)
		.show();
	}
	public void populateStock(String itemcode)
	{
		new AsyncTask<String, Void, Stock>() {
            @Override
            protected Stock doInBackground(String... arg) {
            	Log.i("Thread",arg[0]);
                return Stock.getStock(arg[0]);
                
            }
            @Override
            protected void onPostExecute(Stock s) {
            	if(s!=null){
            	 for (int i=0; i<widgets.length; i++) {
     	            TextView et = (TextView) findViewById(widgets[i]);
     	            et.setText(s.get(keys[i]));
     	           Log.i("Thread",s.get(keys[i]));
     	            
     	        }
            	 itemDescription=s.get("ItemDescription");
            	 price=Double.parseDouble(s.get("Price"));
            }
            	else
            	{
            		status=true;
            		createAlert("Error", "Oops..... Wrong Item Code!");
            	}
            }
        }.execute(itemcode);
	}
	public void putSharedPref(JSONObject jo) throws JSONException
	{
		pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
		String a=pref.getString("AA", "a");
		JSONArray arr=new JSONArray();
		Log.i("SP2", pref.getString("AA", "a"));
		SharedPreferences.Editor editor = pref.edit();
//		editor.clear();
//		editor.commit();
		
//		JSONArray arr2=new JSONArray(pref.getString("AA", "a"));
//		arr2.put(jo);
//		editor.putString("AA", arr2.toString());
//		editor.commit();
		
		if(a=="a")
		{
			arr.put(jo);
		}
		else
		{
			arr=new JSONArray(pref.getString("AA", "a"));
			arr.put(jo);
		}
		
		editor.putString("AA",arr.toString());
		editor.commit();
		Log.i("SP", pref.getString("AA", "a"));
//		setResult(RESULT_OK);
//		finish();
	}
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.clerk__inventory_detail, menu);
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
