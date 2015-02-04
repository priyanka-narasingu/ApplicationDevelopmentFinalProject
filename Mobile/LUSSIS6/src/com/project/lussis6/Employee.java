package com.project.lussis6;

import java.util.*;
import org.json.JSONArray;
import org.json.JSONObject;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.util.Log;
import com.project.lussis6.Constants;

public class Employee extends java.util.HashMap<String, String>{
	
	private static final long serialVersionUID = 3973249260049876884L;
    
	//private static final String host = "http://10.10.1.141/SA38/Service1.svc/";
    
    public Employee(){}
    
    public Employee(String EmployeeID, String EmployeeName, String DeptCode){
    	put("EmployeeID", EmployeeID);
    	put("EmployeeName", EmployeeName);
    	put("DeptCode", DeptCode);
    	
    	
    }

     
    public static Employee getEmployee(String EmployeeID){
    	
    	try{
      		 JSONObject ob = JasonParser.getJSONFromUrl(Constants.HOST+"Service1.svc/AllEmployees/"+EmployeeID);
               
               Log.i("JSONObj",ob.toString());
               return (new Employee(ob.getString("EmployeeID"),
               		    ob.getString("EmployeeName"),
                   		ob.getString("DeptCode")));
               
    }catch (Exception e) {
		Log.e("JSON", "Error getEmployee");	
		Log.e("JSON", e.toString());	
    }
    	return null;
    }
    
    public static List<Employee> getEmployeeList(String deptCode){
    List<Employee> list = new ArrayList<Employee>();
   // StrictMode.setThreadPolicy(ThreadPolicy.LAX);
    try{
    
    	JSONArray a = JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service1.svc/Employee/"+deptCode);
    	for(int i=0; i<a.length();i++){
    	JSONObject ob = a.getJSONObject(i);
    	String employee = ob.getString("EmployeeID").toString();
    	list.add(getEmployee(employee));
    	Log.i("ELIST",list.toString());
    }
    }catch (Exception e){
		Log.e("JSON","Error Full Employee List");
    } 
    return list;
}
   
}
    