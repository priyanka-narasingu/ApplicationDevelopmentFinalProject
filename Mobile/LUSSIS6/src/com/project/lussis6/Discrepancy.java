package com.project.lussis6;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;

public class Discrepancy extends java.util.HashMap<String, String>{

	/**
	 * 
	 */
	private static final long serialVersionUID = -5045007929783596282L;
	//private static final String host ="http://10.10.1.143/LUSSIS6/Service2.svc/";
	
	
	
	public Discrepancy(){}
	public Discrepancy(String raisedBy,double totalAmount){
		put("RaisedBy",raisedBy);
		put("TotalAmount",Double.toString(totalAmount));
	}
//	
//	public static Discrepancy getDiscrepancy(String ID){
//		try{
//			JSONArray item=JasonParser.getJSONArrayFromUrl(host+"url");
//			JSONObject ob=item.getJSONObject(0);
//			return(new Discrepancy(ob.getString("DiscrepancyID"),
//					ob.getString("DateRaised")));
//		}
//		catch(Exception e){
//			Log.i("JSON","Error getDiscrepancy");
//		}
//		return null;
//		
//	}
	
	public static String insert(Discrepancy d){
		JSONObject dis=new JSONObject();
		try{
			dis.put("RaisedBy", d.get("RaisedBy"));
			dis.put("TotalAmount", d.get("TotalAmount"));
		}
		catch(Exception e){
			
		}
		String result=JasonParser.postStream(Constants.HOST+"Service2.svc/addDiscrepancy", dis.toString());
		return result;
	}
	
	public static String update(Discrepancy d){
		JSONObject dis=new JSONObject();
		try{
			dis.put("DiscrepancyID", d.get("DiscrepancyID"));
			dis.put("DateRaised", d.get("DateRaised"));
		}
		catch(Exception e){
			
		}
		String result=JasonParser.postStream(Constants.HOST+"Service2.svc/addDiscrepancy", dis.toString());
		return result;
	}
	
	
}
