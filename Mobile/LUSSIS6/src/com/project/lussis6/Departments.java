package com.project.lussis6;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;

public class Departments extends HashMap<String, String> {


	/**
	 * 
	 */
	private static final long serialVersionUID = 3829716108264820654L;
	//private static final String host = "http://10.10.1.142/wcfservices/Service.svc/"; //to change ipAddress to server
	

	public Departments( String deptCode, String deptName, String collectionPoint, String deptContactNo, int deptCollectionPin, String deptRepCode, String deptRepName) {
		put("deptCode", deptCode);
		put("deptName", deptName);	
		put("collectionPoint", collectionPoint);
		put("deptContactNo", deptContactNo);	
		put("deptCollectionPin", Integer.toString(deptCollectionPin));
		put("deptRepCode", deptRepCode);	
		put("deptRepName", deptRepName);	
		
		
		
	}		
	
	
	//need to review
		public static List<Departments> getDepartmentList() {
			List<Departments> list = new ArrayList<Departments>();
					
			try {
				
				JSONArray a = JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service.svc/Department");
				Log.i("----JSON objects-----", a.toString());
				
				try {
		            for (int i =0; i<a.length(); i++) {
		                JSONObject b = a.getJSONObject(i);
		                list.add(new Departments(b.getString("DeptCode"),
		                		              b.getString("DeptName"),
		                		              b.getString("CollectionPoint"),
		                		              b.getString("DeptContactNo"),
		                		              b.getInt("DeptCollectionPin"), 
		                		              b.getString("DeptRepCode"),
		                		              b.getString("DeptRepName"))); 
		                
		                	                
		            }
		        } catch (Exception e) {
		            Log.e("Department List", "JASONArray error");
		        }

						    
			} catch (Exception e) {
				Log.i("JSON", "Error department list");
			}
			
			
	        return list;
		}
		
		public static List<Departments> getDepartmentList(String collectionPointCode) {
			List<Departments> list = new ArrayList<Departments>();
					
			try {
				
				JSONArray a = JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service.svc/Department/"+ collectionPointCode);
				Log.i("----JSON objects-----", a.toString());
				
				try {
		            for (int i =0; i<a.length(); i++) {
		                JSONObject b = a.getJSONObject(i);
		                list.add(new Departments(b.getString("DeptCode"),
		                		              b.getString("DeptName"),
		                		              b.getString("CollectionPoint"),
		                		              b.getString("DeptContactNo"),
		                		              b.getInt("DeptCollectionPin"),
		                		              b.getString("DeptRepCode"),
		                		              b.getString("DeptRepName")));  
		                	                
		            }
		        } catch (Exception e) {
		            Log.e("Department List", "JASONArray error");
		        }

						    
			} catch (Exception e) {
				Log.i("JSON", "Error department list");
			}
			
			
	        return list;
		}
	
}
