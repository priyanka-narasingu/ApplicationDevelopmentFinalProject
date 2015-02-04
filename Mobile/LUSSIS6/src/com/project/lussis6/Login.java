package com.project.lussis6;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;


public class Login extends Activity {
	
	
	Button btnLogin;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
                
        setContentView(R.layout.activity_login);
        
        
        final EditText txtUsername = (EditText) findViewById(R.id.editTextUsername);
        final EditText txtPassword = (EditText) findViewById(R.id.editTextPassword);
     
        btnLogin = (Button) findViewById(R.id.buttonLogin1);
        
        btnLogin.setEnabled(false);
        //btnLogin.setEnabled(false);	
        btnLogin.setOnClickListener(new OnClickListener () {
			@Override
			public void onClick(View v) {
	            				
				final String username = txtUsername.getText().toString();
				final String password = txtPassword.getText().toString();
				
				
				if (!NetworkStatus.isNetworkAvailable(getApplicationContext())){

					new AlertDialog.Builder(Login.this)
					.setTitle("Network service")
					.setMessage("There is no network service. Please ensure network service and try again")
					.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) { 
							//finish();
						}
					})					  	    
					.setIcon(android.R.drawable.ic_dialog_alert)
					.show();	
					
				} else{

					new AsyncTask<Void, Void, String>() {
						@Override
						protected String doInBackground(Void... params) {

							return LoginCredentials.getLoginCredentials(username, password);
						}		  	          		  	          


						@Override
						protected void onPostExecute(String result) {

							Log.i("Result ***", result);	

							// fails authentication with Membership provider
							if (result.trim().equals("\"failed\"")){

								Toast.makeText(Login.this, "Username and/or password is incorrect", Toast.LENGTH_LONG).show();							

								// passes authentication with Membership provider
							} else{						


								new AsyncTask<Void, Void, LoginCredentials>() {
									@Override
									protected LoginCredentials doInBackground(Void... params) {

										return LoginCredentials.getLoginCredentials(username);
									}		  	          		  	          

									@Override
									protected void onPostExecute(final LoginCredentials lc) {

										if (lc != null){																					

											Log.i("Login credentials ***", lc.toString());	
											
											
											SharedPreferences.Editor editor = getSharedPreferences("com.project.lussis6", Context.MODE_PRIVATE).edit();
											editor.putString("username", lc.get("username"));
											editor.putString("employeeName", lc.get("employeeName"));
											editor.putString("employeeID", lc.get("employeeID"));
											editor.putString("roleCode", lc.get("roleCode"));
											editor.putString("roleDescription", lc.get("roleDescription"));
											editor.putString("deptName", lc.get("deptName"));
											editor.putString("deptCode", lc.get("deptCode"));													
											editor.apply();
											
											
											
											// retrieve from your activity
											SharedPreferences prefs = getSharedPreferences("com.project.lussis6", Context.MODE_PRIVATE);
											String deptCode = prefs.getString("deptCode", null);
											Log.i("Dept Code", deptCode);
											
											

											new AlertDialog.Builder(Login.this)
											.setTitle("Login successful")
											.setMessage("Logged in as " + lc.get("roleDescription"))
											.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
												public void onClick(DialogInterface dialog, int which) { 
												

													if (lc.get("roleDescription").trim().equals("Store Clerk")){
														
														Intent intent = new Intent(Login.this, Clerk_Home.class); 																				
														//Intent intent = new Intent(Login.this, Clerk_Issue_Tab.class); 																						
														startActivity(intent);


													} else if (lc.get("roleDescription").trim().equals("Department Head")){										


														Intent intent = new Intent(Login.this, Department_Home.class);														
														startActivity(intent);

													} else {		

														// Catered for future upgrades - modules for other employees
														Toast.makeText(Login.this, "Sorry, there are not modules available for you yet, please wait for next upgrade", Toast.LENGTH_LONG).show();
													}			
													
													
													txtPassword.getText().clear();
													txtUsername.getText().clear();							
													txtUsername.requestFocus();

												}
											})					  	    
											.setIcon(android.R.drawable.ic_dialog_alert)
											.show();	

										}
									}
								}.execute();
								
								

							}

						}
					}.execute();
				}


			}

        });
        
        
        
     // to disable login button when username and password fields are empty
        txtUsername.addTextChangedListener(new TextWatcher(){  		   
			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub				
			}

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				if(txtUsername.getText().toString().trim().length()>0 && txtPassword.getText().toString().trim().length()>0)			
					btnLogin.setEnabled(true);
  		        else
  		        	btnLogin.setEnabled(false);				
			}

			@Override
			public void afterTextChanged(Editable s) {
				// TODO Auto-generated method stub				
			}

  		});
        
        
        // to disable login button when username and password fields are empty
        txtPassword.addTextChangedListener(new TextWatcher(){  		   
			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub				
			}

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				if(txtUsername.getText().toString().trim().length()>0 && txtPassword.getText().toString().trim().length()>0)			
					btnLogin.setEnabled(true);
  		        else
  		        	btnLogin.setEnabled(false);				
			}

			@Override
			public void afterTextChanged(Editable s) {
				// TODO Auto-generated method stub				
			}

  		});
        
    }
    
    
    
    public void initialiseExtras(){
    	
    }
    
    


//    @Override
//    public boolean onCreateOptionsMenu(Menu menu) {
//        // Inflate the menu; this adds items to the action bar if it is present.
//        getMenuInflater().inflate(R.menu.login, menu);
//        return true;
//    }
//
//    @Override
//    public boolean onOptionsItemSelected(MenuItem item) {
//        // Handle action bar item clicks here. The action bar will
//        // automatically handle clicks on the Home/Up button, so long
//        // as you specify a parent activity in AndroidManifest.xml.
//        int id = item.getItemId();
//        if (id == R.id.action_settings) {
//            return true;
//        }
//        return super.onOptionsItemSelected(item);
//    }
}
