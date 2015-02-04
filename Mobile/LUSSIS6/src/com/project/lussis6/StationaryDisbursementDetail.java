package com.project.lussis6;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;

public class StationaryDisbursementDetail extends HashMap<String, String> {

	
	/**
	 * 
	 */
	private static final long serialVersionUID = -1090548352648463566L;
	//private static final String host = "http://10.10.1.142/wcfservices/Service.svc/"; //to change ipAddress to server
	

	public StationaryDisbursementDetail(int disbursementId, String itemCode, int requestedQty, int actualQty, String itemDescription, String unitOfMeasure) {
		put("disbursementId", Integer.toString(disbursementId));
		put("itemCode", itemCode);	
		put("requestedQty", Integer.toString(requestedQty));		
		put("actualQty", Integer.toString(actualQty));
		put("itemDescription", itemDescription);
		put("unitOfMeasure", unitOfMeasure);
		
	}	
	
	
	public static List<StationaryDisbursementDetail> getItemList(String deptCode){
		
		
		List<StationaryDisbursementDetail> list = new ArrayList<StationaryDisbursementDetail>();
		
		try {
			
			JSONArray a = JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service.svc/StationeryDisbursement/" + deptCode);
			Log.i("----JSON objects-----", a.toString());
			
			try {
	            for (int i =0; i<a.length(); i++) {
	                JSONObject b = a.getJSONObject(i);
	                list.add(new StationaryDisbursementDetail(b.getInt("DisbursementId"),
	                		              b.getString("ItemCode"),
	                		              b.getInt("RequestedQty"),
	                                      b.getInt("ActualQty"),
	                                      b.getString("ItemDescription"),
	                                      b.getString("UnitOfMeasure")));    
	                	                
	            }
	        } catch (Exception e) {
	            Log.e("Disbursement List", "JASONArray error");
	            Log.e("Disbursement List", e.toString());
	        }

					    
		} catch (Exception e) {
			Log.i("JSON", "Error Disbursement list");
		}
		
		
        return list;
		
		
		
	}
	
	
	public static String updateQuantity(StationaryDisbursementDetail sdd, int updateQty) {
		 
		String result=null;
		
		JSONObject item = new JSONObject();
		 try {
			 
			 item.put("DisbursementId", Integer.parseInt(sdd.get("disbursementId")));
			 item.put("ItemCode", sdd.get("itemCode"));
			 item.put("RequestedQty", Integer.parseInt(sdd.get("requestedQty")));
			 item.put("ActualQty", updateQty);	 			 
			 item.put("ItemDescription", sdd.get("itemDescription"));
			 item.put("UnitOfMeasure", sdd.get("unitOfMeasure"));
		 	 		 
		 
		 } catch (Exception e) {
			 Log.e("Update Error creating item", e.toString());		 
		 }
		 try{
			 Log.i("Item", item.toString());
			 
			 result = JasonParser.postStream(Constants.HOST+"Service.svc/StationeryDisbursement/update", item.toString());
			 
			 Log.i("Result", result);
			 
		 } catch (Exception e) {
 			 Log.e("Update Error", e.toString());
 			 
		 }
		 
		 return result;
	}
	
}
