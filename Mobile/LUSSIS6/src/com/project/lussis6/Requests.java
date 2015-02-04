package com.project.lussis6;

import java.util.*;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;



public class Requests extends java.util.HashMap<String, String>{

	private static final long serialVersionUID = 3973249260049876884L;
    //private static final String host = "http://10.10.3.182/LUSSIS6/Service3.svc/";
    
    
    public Requests(){}
    
    public Requests(String EmployeeName, String DateCreated, String RequestID, String Comments, String RequestStatus, String ApprovedBy,String DateUpdated){
    	put("EmployeeName", EmployeeName);
    	put("DateCreated",DateCreated);
    	put("RequestID", RequestID);
    	put("Comments", Comments);
    	put("RequestStatus", RequestStatus);
    	put("ApprovedBy", ApprovedBy);
    	put("DateUpdated", DateUpdated);
   
    }
    
    /*public static Requests getRequests(String RequestID){
    	try{
    		 JSONArray item = JasonParser.getJSONArrayFromUrl(host+"/LUSSIS6/Service.svc/getRequest/"+RequestID);
             JSONObject ob = item.getJSONObject(0);
             return (new Requests(ob.getString("EmployeeName"),
             		    ob.getString("DateCreated"),
             		    ob.getString("RequestID"),
             		    ob.getString("Comments"),
             		  ob.getString("RequestStatus"), 
             		 ob.getString("ApprovedBy"),
             		ob.getString("DateUpdated")));
                 		
 		} catch (Exception e) {
 			Log.i("JSON", "Error getRequests");
    	}
    	return null;
    }
    */
    
    public static String updateRequestsAccept(String requestid, String approveby){
    	String result = JasonParser.postStreams(Constants.HOST+"Service3.svc/updateRequestAccept/"+requestid+"/"+approveby);
        return result;
    }
    
    public static String updateRequestsReject(String requestid, String comments)
    {
    	JSONObject item = new JSONObject();
    	try{
    		item.put("RequestID", requestid);
    		item.put("Comments", comments);
    		
    	}catch (Exception e)
    	{
    		Log.e("Error updating comments", e.toString());	
    	}
    	String result = JasonParser.postStream(Constants.HOST+"Service3.svc/updateRequestReject",item.toString());
    	return result;
    	
    }
    	
    	/*JSONObject requests = new JSONObject();
    	try{
    		//requests.put("EmployeeName",m.get("EmployeeName"));
    		//requests.put("DateCreated", m.get("DateCreated"));
    		//requests.put("RequestsID",m.get("RequestsID"));
    		requests.put("Comments", m.get("Comments"));
    		requests.put("RequestStatus",m.get("RequestStatus"));
    		//requests.put("ApprovedBy", m.get("ApprovedBy"));
    		requests.put("DateUpdated", m.get("DateUpdated"));	
    	}
    	
         catch (Exception e) {
    }
    String result = JasonParser.postStream(host+"/LUSSIS6/Service.svc/updateRequest/", requests.toString());
    return result;
    }
    */
    public static List<Requests> getRequestsList(String deptCode){
    	List<Requests> list = new ArrayList<Requests>();
    	try{
    		JSONArray a =JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service3.svc/pendreq/"+deptCode);
    		Log.i("JSON", a.toString());
    		for (int i=0;i<a.length();i++){
    			JSONObject b=a.getJSONObject(i);
    			list.add(new Requests(b.getString("EmployeeName"), (b.getString("DateCreated")),(b.getString("RequestID")),(b.getString("Comments")),(b.getString("RequestStatus")),(b.getString("ApprovedBy")),(b.getString("DateUpdated"))));
    		}
    	} catch (Exception e){
    		Log.i("JSON","Error Full Requests List");
    	}
    	Log.i("Json", "Success");
    return list;
    	
    }
}

