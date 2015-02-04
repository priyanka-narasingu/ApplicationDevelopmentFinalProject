package com.project.lussis6;

import java.util.HashMap;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;

public class StationaryDisbursement extends HashMap<String, String> {

	/**
	 * 
	 */
	private static final long serialVersionUID = 5123382190732216926L;
	//private static final String host = "http://10.10.1.142/wcfservices/Service.svc/"; //to change ipAddress to server
	

	public StationaryDisbursement(int disbursementId, int retrievalId, String deptCode, String deptRep, 
			String collectionPoint, String disbursementStatus) {
		put("disbursementId", Integer.toString(disbursementId));
		put("retrievalId", Integer.toString(retrievalId));
		put("deptCode", deptCode);		
		put("deptRep", deptRep);
		put("collectionPoint", collectionPoint);
		put("disbursementStatus", disbursementStatus);
	}
	
	
	public static String updateDisbursementStatus(int disbursementId, String disbursementStatus) {
		 
		String result=null;
		
		JSONObject item = new JSONObject();
		 try {		 		 
			 
			 item.put("DisbursementId", disbursementId);
			 item.put("DisbursementStatus", disbursementStatus);
			 		 		 	 		 
		 
		 } catch (Exception e) {
			 Log.e("Error updating disbursement status", e.toString());		 
		 }
		 try{
			 Log.i("Item-Disbursement Status", item.toString());
			 
			 
			 
			 result = JasonParser.postStream(Constants.HOST+"Service.svc/StationeryDisbursement/updateStatus", item.toString());
			 
			 Log.i("Result-Disbursement Status", result);
			 
		 } catch (Exception e) {
 			 Log.e("Update disbursement status error", e.toString());
 			 
		 }
		 
		 return result;
	}

	
	
	
}
