package com.project.lussis6;

import java.security.PublicKey;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.R.bool;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.util.Log;
import com.project.lussis6.Constants;

public class Delegation extends java.util.HashMap<String, String>{ 
	
	private static final long serialVersionUID = 3973249260049876884L;
    
	//private static final String host = "http://10.10.1.141/SA38/Service1.svc/";
	
    
    public Delegation(){}
    
    public Delegation(String EmployeeID, String EmployeeName, String startDate, String endDate)
    {
    	put("EmployeeID", EmployeeID);
    	put("EmployeeName", EmployeeName);
    	put("StartDate", startDate);
    	put("EndDate", endDate);
    	
    	
    }

    public static String updateDelegation(Delegation d){
    	JSONObject delegateJsonObject = new JSONObject();
    	try{
    		delegateJsonObject.put("EmployeeID", d.get("EmployeeID"));
    		delegateJsonObject.put("EmployeeName",d.get("EmployeeName"));
             delegateJsonObject.put("StartDate", d.get("StartDate"));
            delegateJsonObject.put("EndDate",d.get("EndDate"));
           
                 
             } catch (Exception e) {
            	 Log.e("JSON", "Error updateDelegation");	
            	 Log.e("OBJ",d.toString());
         		Log.e("JSON", e.toString());
             }
             String result = JasonParser.postStream(Constants.HOST+"Service1.svc/DelegateEmployee",delegateJsonObject.toString());
             Log.i("UPDATE", delegateJsonObject.toString());
             Log.i("DELEG", result);
             return result;
    	}
    
    public static Delegation getDelegate(String deptCode)
    {
    	try{
    		//StrictMode.setThreadPolicy(ThreadPolicy.LAX);
     		 JSONObject ob = JasonParser.getJSONFromUrl(Constants.HOST+"Service1.svc/GetDelegate/"+deptCode);
             Delegation d=new Delegation(ob.getString("EmployeeID"),
           		    ob.getString("EmployeeName"),
               		ob.getString("StartDate"),
               		ob.getString("EndDate"));
              
              Log.i("JSONObj", d.toString());
             return d;
              
   }catch (Exception e) {
		Log.e("JSON", "Error getDelegate");	
		Log.e("JSON", e.toString());	
   }
   	return null;
   }
    public static String revoke(String empID)
    {
    	try{
    		Log.i("EMP", empID);
    		StrictMode.setThreadPolicy(ThreadPolicy.LAX);
     		 String result= JasonParser.postStreams(Constants.HOST+"Service1.svc/RevokeDelegate/"+empID);
                         
              Log.i("RESULT REVOKE", result);
              return result;
    	 }catch (Exception e) {
    			Log.e("JSON", "Error revoke");	
    			Log.e("JSON", e.toString());	
    	   }
    	return null;
    }
    public static boolean checkDelegate(String deptCode)
    {
    	try {
    		boolean check=false;
    		StrictMode.setThreadPolicy(ThreadPolicy.LAX);
    		String result=JasonParser.getStream(Constants.HOST+"Service1.svc/CheckDelegate/"+deptCode);
    		;
    		if(result.trim().contentEquals("true"))
    		{
    			check=true;
    		}
    		else {
    			check=false;
			}
    		Log.i("RESULT", result);
    		return check;
    		
    		
		} catch (Exception e) {
			Log.e("URL", "Error in CheckDelegate");
			Log.e("URL", e.toString());
		}
    	return false;
    }
    
    }

   