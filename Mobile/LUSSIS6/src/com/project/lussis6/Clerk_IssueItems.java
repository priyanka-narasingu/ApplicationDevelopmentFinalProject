package com.project.lussis6;

import java.util.ArrayList;
import java.util.List;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.InputFilter;
import android.text.method.DigitsKeyListener;
import android.text.method.PasswordTransformationMethod;
import android.util.Log;
import android.view.Gravity;
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
import android.widget.TextView;
import android.widget.Toast;
import com.project.lussis6.Constants;


public class Clerk_IssueItems extends Activity implements OnItemClickListener{
	
	final static int ISSUE_STATIONERY_EDIT = 103;
	List<StationaryDisbursementDetail> statDisbursementDetail;
	ListView lv;
	String deptCode;
	int deptCollectionPin;
	int disbursementId;
	Button btnCancelDelivery;
	Button btnCollected;
	TextView txtEmpty;


	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		boolean finish = getIntent().getBooleanExtra("finish", false);
        if (finish) {
            startActivity(new Intent(getApplicationContext(), Login.class));
            finish();
            return;
        }
        
		setContentView(R.layout.activity_clerk__issue_items);

		lv = (ListView) findViewById(R.id.listViewIssueItemsNew);
		
		txtEmpty = (TextView) findViewById(R.id.textViewEmptyItemListWarning);
		txtEmpty.setText("");


		Intent intent = getIntent();
		deptCode = intent.getStringExtra("deptCode");
		String deptName = intent.getStringExtra("deptName");
		String deptRepName = intent.getStringExtra("deptRepName");
		deptCollectionPin = Integer.parseInt(intent.getStringExtra("deptCollectionPin"));



		TextView tvName = (TextView) findViewById (R.id.textViewRepName);
		tvName.setText(deptRepName); // need to change to rep name

		getItemList(deptCode);

		lv.setOnItemClickListener(this);


		btnCancelDelivery = (Button) findViewById(R.id.buttonCancelDelivery);
		btnCancelDelivery.setOnClickListener(new OnClickListener () {
			@Override
			public void onClick(View v) {
				
				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					alertNoNetwork();	

				} else{

					new AlertDialog.Builder(Clerk_IssueItems.this)
					.setTitle("Cancel Delivery")
					.setMessage("Are you sure you want to cancel delivery?")
					.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) { 
							// continue with cancellation
							// connect to database and update status	 	       

							updateDisbursementStatus(disbursementId, Constants.CANCEL);

							//finish();		  	        			  	        	
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
		});


		btnCollected = (Button) findViewById(R.id.buttonCollected);
		btnCollected.setOnClickListener(new OnClickListener () {
			@Override
			public void onClick(View v) {
				
				
				launchPinDialog("PIN", "Enter your Department PIN");

			}
		});


	}

	
	public void launchPinDialog(String title, String message){


		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();	

		} else{

			AlertDialog.Builder alert = new AlertDialog.Builder(Clerk_IssueItems.this);

			alert.setTitle(title);
			alert.setMessage(message);


			// Set an EditText view to get user input 
			final EditText input = new EditText(Clerk_IssueItems.this);
			alert.setView(input);

			input.setFilters(new InputFilter[] {
					// Max 4 char
					new InputFilter.LengthFilter(4),
					// Digits only.
					DigitsKeyListener.getInstance() 
			});

			// Digits only & use numeric soft-keyboard.
			input.setKeyListener(DigitsKeyListener.getInstance());				
			input.setTransformationMethod(PasswordTransformationMethod.getInstance());
			input.setGravity(Gravity.CENTER_HORIZONTAL);

			alert.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
				public void onClick(DialogInterface dialog, int whichButton) {
					
					
					int pin = 0;
					if (input.getText().toString().trim().length() != 0)
					{
						pin = Integer.parseInt(input.getText().toString());
					}
					
					

					// check if PIN is correct				

					if (pin == deptCollectionPin){

						String disburseResult = "Delivered";
						Log.i("statDisbursementDetail", statDisbursementDetail.toString());

						for (StationaryDisbursementDetail sddTemp: statDisbursementDetail){

							Log.i("sddTemp", sddTemp.get("requestedQty"));
							Log.i("sddTemp", sddTemp.get("actualQty"));

							if (!sddTemp.get("requestedQty").equals(sddTemp.get("actualQty"))){
								disburseResult = "Partially Delivered";
							}														

						}

						updateDisbursementStatus(disbursementId, disburseResult);

					} else{							

						launchPinDialog("Incorrect PIN", "Please enter your PIN again");
					}

				}
			});

			alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
				public void onClick(DialogInterface dialog, int whichButton) {
					// Cancelled
				}
			});

			alert.show();	
		}

	}

	private void alertNoNetwork(){
		new AlertDialog.Builder(Clerk_IssueItems.this)
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
		getMenuInflater().inflate(R.menu.clerk__issue_items, menu);
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
	

	private void getItemList(final String deptName) {

		statDisbursementDetail = new ArrayList<StationaryDisbursementDetail>();


		if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

			alertNoNetwork();	

		} else{

			new AsyncTask<Void, Void, List<StationaryDisbursementDetail>>() {
				@Override
				protected List<StationaryDisbursementDetail> doInBackground(Void... params) {
					return StationaryDisbursementDetail.getItemList(deptName);
				}
				@Override
				protected void onPostExecute(List<StationaryDisbursementDetail> list) {

					statDisbursementDetail = list;
					if(!list.isEmpty()){
						StationaryDisbursementDetail sdd1 = list.get(0);
						disbursementId = Integer.parseInt(sdd1.get("disbursementId"));
						lv.setAdapter(new SimpleAdapter(getApplicationContext(), list, R.layout.row_disbursement_items, 
								new String[]{"itemDescription", "actualQty", "itemCode"},
								new int[]{ R.id.textViewItemName, R.id.textViewItemQty, R.id.textViewItemCode}));	

					} else {

						btnCancelDelivery.setEnabled(false);
						btnCollected.setEnabled(false);
						txtEmpty.setText("There are no items in list");

					}

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

	private void updateDisbursementStatus(final int disbursementId, final String disbursementStatus) {

		new AsyncTask<Void, Void, String >() {
			@Override
			protected String doInBackground(Void... params) {
				return StationaryDisbursement.updateDisbursementStatus(disbursementId, disbursementStatus);
			}
			@Override
			protected void onPostExecute(String result) {

				if (result.trim().equals("true") && (disbursementStatus.equals(Constants.DELIVERED))){

					new AlertDialog.Builder(Clerk_IssueItems.this)
					.setTitle("Success")
					.setMessage("Items have been collected")
					.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) { 
							finish();
						}
					})					  	    
					.setIcon(android.R.drawable.ic_dialog_alert)
					.show();	 


				} else if (result.trim().equals("true") && disbursementStatus.equals(Constants.PARTIALLY_DELIVERED)){

					new AlertDialog.Builder(Clerk_IssueItems.this)
					.setTitle("Disbursement Status")
					.setMessage("Stationery has been partially issued")
					.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) { 
							finish();
						}
					})					  	    
					.setIcon(android.R.drawable.ic_dialog_alert)
					.show();	 
									
					
				} else if (result.trim().equals("true") && disbursementStatus.equals(Constants.CANCEL)){

						new AlertDialog.Builder(Clerk_IssueItems.this)
						.setTitle("Disbursement Status")
						.setMessage("Disbursement has been cancelled")
						.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int which) { 
								finish();
							}
						})					  	    
						.setIcon(android.R.drawable.ic_dialog_alert)
						.show();	 				
				
					
				} else{
					new AlertDialog.Builder(Clerk_IssueItems.this)
					.setTitle("Disbursement Status")
					.setMessage("Failed to update disbursement status")
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
	}


	@Override
	public void onItemClick(AdapterView<?> av, View v, int position, long id) {
		StationaryDisbursementDetail  sdd = (StationaryDisbursementDetail) av.getAdapter().getItem(position);
		//Toast.makeText(getApplicationContext(), sdd.get("itemCode") + " selected", Toast.LENGTH_SHORT).show();

		Intent intent = new Intent(Clerk_IssueItems.this, Clerk_IssueEdit.class);
		intent.putExtra("disbursementId", sdd.get("disbursementId"));
		intent.putExtra("itemCode", sdd.get("itemCode"));
		intent.putExtra("itemDescription", sdd.get("itemDescription"));
		intent.putExtra("unitOfMeasure", sdd.get("unitOfMeasure"));
		intent.putExtra("requestedQty", sdd.get("requestedQty"));
		intent.putExtra("actualQty", sdd.get("actualQty"));


		startActivityForResult(intent, ISSUE_STATIONERY_EDIT);

	}

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data){
		if (requestCode == ISSUE_STATIONERY_EDIT ) {
			getItemList(deptCode);
		}		

	}


}
