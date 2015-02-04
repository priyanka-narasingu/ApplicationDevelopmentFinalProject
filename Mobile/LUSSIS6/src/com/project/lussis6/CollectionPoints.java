package com.project.lussis6;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;


public class CollectionPoints extends HashMap<String, String> {

	/**
	 * 
	 */
	private static final long serialVersionUID = -8312273813796319987L;	
	//private static final String host = "http://10.10.1.142/wcfservices/Service.svc/"; //to change ipAddress to server
		

	public CollectionPoints(String collectionPointCode, String collectionPointName, String collectionTime) {
		put("collectionPointCode", collectionPointCode);
		put("collectionPointName", collectionPointName);
		put("collectionTime", collectionTime);
	}
	
	
	//need to review
	public static List<CollectionPoints> getCollectionPointsList() {
		List<CollectionPoints> list = new ArrayList<CollectionPoints>();
				
		try {
			
			JSONArray a = JasonParser.getJSONArrayFromUrl (Constants.HOST+"Service.svc/CollectionPoint");
			Log.i("----JSON objects-----", a.toString());
			
			try {
	            for (int i =0; i<a.length(); i++) {
	                JSONObject b = a.getJSONObject(i);
	                list.add(new CollectionPoints(b.getString("CollectionPointCode"),
	                		              b.getString("CollectionPointName"),
	                                      b.getString("CollectionTime")));    
	                	                
	            }
	        } catch (Exception e) {
	            Log.e("Collection Point List", "JASONArray error");
	        }

					    
		} catch (Exception e) {
			Log.i("JSON", "Error collection point list");
		}
		
		
        return list;
	}
	




}