package com.project.lussis6;

import java.util.List;

import org.json.JSONObject;

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
import android.widget.Button;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class Clerk_IssueEdit extends Activity {
	
	private static StationaryDisbursementDetail sdd;
	private EditText editTextQty;
	private Button btnUpdateQty;
	

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		boolean finish = getIntent().getBooleanExtra("finish", false);
		if (finish) {
			startActivity(new Intent(getApplicationContext(), Login.class));
			finish();
			return;
		}

		setContentView(R.layout.activity_clerk__issue_edit);


		Intent intent = getIntent();
		String disbursementId = intent.getStringExtra("disbursementId");
		String itemCode = intent.getStringExtra("itemCode");
		String itemDescription = intent.getStringExtra("itemDescription");
		String unitOfMeasure = intent.getStringExtra("unitOfMeasure");
		final String requestedQty = intent.getStringExtra("requestedQty");
		String actualQty = intent.getStringExtra("actualQty");


		sdd = new StationaryDisbursementDetail(Integer.parseInt(disbursementId), itemCode, Integer.parseInt(requestedQty), Integer.parseInt(actualQty), itemDescription, unitOfMeasure);

		Log.i("sdd", sdd.toString());


		TextView txtItemDesc = (TextView) findViewById(R.id.textViewIssueItemDesc);
		txtItemDesc.setText(itemDescription);

		TextView txtItemCode = (TextView) findViewById(R.id.textViewItemCode1);
		txtItemCode.setText(itemCode);

		TextView txtItemUnitOfMeasure = (TextView) findViewById(R.id.textViewUnitOfMeasure);
		txtItemUnitOfMeasure.setText(unitOfMeasure);

		TextView txtItemRequestedQty = (TextView) findViewById(R.id.textViewRequestedQty);
		txtItemRequestedQty.setText(requestedQty);

		TextView txtItemActualQty = (TextView) findViewById(R.id.textViewItemQuantity);
		txtItemActualQty.setText(actualQty);


		btnUpdateQty = (Button) findViewById(R.id.buttonUpdateQuantity);
		btnUpdateQty.setEnabled(false);	
		btnUpdateQty.setOnClickListener(new OnClickListener () {
			@Override
			public void onClick(View v) {

				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();

				} else{

					final int updateQty = Integer.parseInt((editTextQty.getText().toString()));

					if (Integer.parseInt(requestedQty) >=updateQty){


						new AsyncTask<Void, Void, String>() {
							@Override
							protected String doInBackground(Void... params) {


								return StationaryDisbursementDetail.updateQuantity(sdd, updateQty);

							}		  	          		  	          


							@Override
							protected void onPostExecute(String result) {

								Log.i("Result ***", result);	  	        		  	        	


								if (result.trim().equals("true")){

									Toast.makeText(Clerk_IssueEdit.this, "Quantity has been changed", Toast.LENGTH_LONG).show();
									finish();


								} else{
									new AlertDialog.Builder(Clerk_IssueEdit.this)
									.setTitle("Update Quantity")
									.setMessage("Quantity update failed")
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

					} else {

						Toast.makeText(Clerk_IssueEdit.this, "Quantity must be less than requested quantity", Toast.LENGTH_SHORT).show();
					}

				}

			}


		});


		editTextQty = (EditText) findViewById(R.id.editTextActualQuantity);

		// to disable update quantity button when field is empty
		editTextQty.addTextChangedListener(new TextWatcher(){  		   
			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub				
			}

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				if(editTextQty.getText().toString().trim().length()>0)					
					btnUpdateQty.setEnabled(true);
				else
					btnUpdateQty.setEnabled(false);				
			}

			@Override
			public void afterTextChanged(Editable s) {
				// TODO Auto-generated method stub				
			}

		});



	}

private void alertNoNetwork(){
	
	new AlertDialog.Builder(Clerk_IssueEdit.this)
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
	// Inflate the menu; this adds items to the action bar if it is present.
	getMenuInflater().inflate(R.menu.clerk__issue_edit, menu);
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
}
