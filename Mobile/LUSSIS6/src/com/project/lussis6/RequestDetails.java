package com.project.lussis6;

import java.util.*;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;

public class RequestDetails extends java.util.HashMap<String, String>{

	private static final long serialVersionUID = 3973249260049876884L;
    //private static final String host = "http://10.10.3.182/LUSSIS6/Service3.svc/";
    
    public RequestDetails(){}
    
    public RequestDetails(String RequestID, String ItemCode,String Quantity, String ItemDescription, String Unitofmeasure)
    	{
    	put("RequestID",RequestID);
    	put("ItemCode", ItemCode);
    	put("Quantity", Quantity);
    	put("ItemDescription", ItemDescription);
    	put("Unitofmeasure", Unitofmeasure);
    }
    
    
    /*public static RequestDetails getRequestDetail(String RequestID){
    	try{
    		 JSONArray item = JasonParser.getJSONArrayFromUrl(host+"/LUSSIS6/Service.svc/pendreq/"+RequestID);
             JSONObject ob = item.getJSONObject(0);
             return (new RequestDetails(ob.getString("RequestID"),
                 		ob.getString("ItemCode"),
                 		ob.getString("Quantity"),
                 		ob.getString("ItemDescription")));
                 		
 		} catch (Exception e) {
 			Log.i("JSON", "Error getRequestDetails");
    	}
    	return null;
    }
    */
   /*public static List<RequestDetails> getRequestDetailsList() {
    	List<RequestDetails> list = new ArrayList<RequestDetails>();
    	try{
    		JSONArray a =JasonParser.getJSONArrayFromUrl(host+"/adtest/Service.svc/list");
    		for (int i=0;i<a.length();i++){
    			String request =a.getString(i);
    			list.add(getRequestDetail(request));
    		}
    	} catch (Exception e){
    		Log.i("JSON","Error Full Request List");
    	}
    return list;
    	
    
    }
    */
    
    public static List<RequestDetails> getRequestDetailList(String RequestID){
    	List<RequestDetails> list = new ArrayList<RequestDetails>();
    	try{
    		JSONArray a =JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service3.svc/requestDetail/"+RequestID);
    		Log.i("JSON", a.toString());
    		for (int i=0;i<a.length();i++){
    			
    			JSONObject b=a.getJSONObject(i);
    			list.add(new RequestDetails(b.getString("RequestID"), (b.getString("ItemCode")),(b.getString("Quantity")),(b.getString("ItemDescription")),(b.getString("Unitofmeasure"))));
    		}
    	} catch (Exception e){
    		Log.i("JSON","Error Full RequestDetails List");
    	}
    	Log.i("Json", "Success");
    return list;
    	
    }
    
}