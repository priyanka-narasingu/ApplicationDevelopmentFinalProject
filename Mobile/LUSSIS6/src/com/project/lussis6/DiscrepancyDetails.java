package com.project.lussis6;

import java.util.ArrayList;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.content.SharedPreferences;
import android.util.Log;
import com.project.lussis6.Constants;

public class DiscrepancyDetails extends java.util.HashMap<String, String>{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	//private static final String host ="http://10.10.1.143/LUSSIS6/Service2.svc";
	
	
	public DiscrepancyDetails(){}
	
	public DiscrepancyDetails(String itemDescription,String itemCode,int quantity,Double amount
			,String isAdded,String reason){
		put("ItemCode",itemCode);
		put("ItemDescription",itemDescription);
		put("Quantity",Integer.toString(quantity));
		put("Amount",Double.toString(amount));
		put("IsAdded",isAdded);
		put("Reason",reason);
	}
	public static String insert(DiscrepancyDetails d){
		JSONObject dis=new JSONObject();
		try{
			dis.put("ItemCode", d.get("ItemCode"));
			dis.put("Quantity", d.get("Quantity"));
			dis.put("Amount", d.get("Amount"));
			dis.put("IsAdded", d.get("IsAdded"));
			dis.put("Reason", d.get("Reason"));
		}
		catch(Exception e){
			
		}
		String result=JasonParser.postStream(Constants.HOST+"Service2.svc/addDiscrepancyDetail", dis.toString());
		return result;
	}

}
