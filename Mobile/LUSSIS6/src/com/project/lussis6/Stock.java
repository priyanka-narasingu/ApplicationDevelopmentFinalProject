package com.project.lussis6;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;
import android.util.Log;
import com.project.lussis6.Constants;

public class Stock extends HashMap<String, String> {		
	/**
	 * 
	 */
	private static final long serialVersionUID = -3257709465818032479L;
	//private static final String host = "http://10.10.1.143/LUSSIS6/Service2.svc/";
	
	public Stock( String itemDes,String itemcode,String uom,String loc,int qty,Double price) {
		put("ItemDescription", itemDes);
		put("ItemCode",itemcode);
		put("Location",loc);
		put("Quantity",Integer.toString(qty));
		put("UnitOfMeasure",uom);
		put("Price",Double.toString(price));		
	}	
	
	public static List<Stock> getStockList() {
		List<Stock> list = new ArrayList<Stock>();
				
		try {
			
			JSONArray a = JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service2.svc/SList");
			Log.i("----JSON objects-----", a.toString());
			
			try {
	            for (int i =0; i<a.length(); i++) {
	                JSONObject b = a.getJSONObject(i);
	                list.add(new Stock(b.getString("ItemDescription"),
	                		b.getString("ItemCode"),
	                		b.getString("UnitofMeasure"),
	                		b.getString("BinNumber"),
	                		b.getInt("AvailableQty"),
	                		b.getDouble("Price")));  
	                	                
	            }
	        } catch (Exception e) {
	            Log.e("Department List", "JASONArray error");
	        }

					    
		} catch (Exception e) {
			Log.i("JSON", "Error department list");
		}
		
		
        return list;
	}
	public static Stock getStock(String code) {
				
		try {
			
			JSONArray a = JasonParser.getJSONArrayFromUrl(Constants.HOST+"Service2.svc/StockByCode/"+code);
			Log.i("----JSON objects-----", a.toString());
			
	                JSONObject b = a.getJSONObject(0);
	                return (new Stock(b.getString("ItemDescription"),
	                		b.getString("ItemCode"),
	                		b.getString("UnitofMeasure"),
	                		b.getString("BinNumber"),
	                		b.getInt("AvailableQty"),
	                		b.getDouble("Price")));  
	           

					    
		} catch (Exception e) {
			Log.i("JSON", "Error department list");
			return null;
		}
		
		
	}

}
