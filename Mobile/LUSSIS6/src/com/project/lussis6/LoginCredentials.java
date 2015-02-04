package com.project.lussis6;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;

public class LoginCredentials extends HashMap<String, String> {

	/**
	 * 
	 */
	private static final long serialVersionUID = -8536089851220326119L;	
	//private static final String host = "http://10.10.1.142/wcfservices/Service.svc/"; //to change ipAddress to server
	

	public LoginCredentials(String username, String employeeID, String employeeName, String roleCode, String roleDescription, String deptName, String deptCode) {
		put("username", username);		
		put("employeeID", employeeID);
		put("employeeName", employeeName);
		put("roleCode", roleCode);
		put("roleDescription", roleDescription);
		put("deptName", deptName);
		put("deptCode", deptCode);
	}
		
	public static String getLoginCredentials(String username, String password) {
								
		String result=null;
		
		JSONObject item = new JSONObject();
		 try {		 		 
			 
			 item.put("UserName", username);
			 item.put("Password", password);
			 		 		 	 		 
		 
		 } catch (Exception e) {
			 Log.e("Error sending login credentials", e.toString());		 
		 }
		 try{
			 Log.i("Login", item.toString());			 			 
			 
			 result = JasonParser.postStream(Constants.HOST+"Service.svc/Login", item.toString());			 	 
             			 			 
			 Log.i("Result-Login", result);
			 
		 } catch (Exception e) {
			 Log.e("Login error", e.toString());
			 
		 }
		 
		 return result;
						
	}
	
	
	public static LoginCredentials getLoginCredentials(String username) {
				
		LoginCredentials lc=null;
				
		try {
			
			
			JSONObject a = JasonParser.getJSONFromUrl(Constants.HOST+"Service.svc/Login/"+username);
			Log.i("----JSON objects-----", a.toString());
			
			lc = new LoginCredentials(a.getString("Username"), a.getString("EmployeeID"), a.getString("EmployeeName"),
            		a.getString("RoleCode"),a.getString("RoleDescription"),a.getString("DeptName"),a.getString("DeptCode"));
			
					    
		} catch (Exception e) {
			Log.i("JSON", "Error login credentials");
		}
		
		
        return lc;
	}
	
	

}
