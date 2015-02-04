using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService2
{
    
    [OperationContract]
    [WebGet(UriTemplate = "/SList", ResponseFormat = WebMessageFormat.Json)]
    iStock[] StockList();

    [OperationContract]
    [WebGet(UriTemplate = "/StockByCode/{itemcode}", ResponseFormat = WebMessageFormat.Json)]
    iStock[] StockByID(string itemcode);

    [OperationContract]
    [WebInvoke(UriTemplate = "/addDiscrepancy", Method = "POST",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    bool addDiscrepancy(iDiscrepancy ides);

    [OperationContract]
    [WebInvoke(UriTemplate = "/addDiscrepancyDetail", Method = "POST",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    bool addDiscrepancyDetail(iDiscrepancyDetail idisd);
}






[DataContract]
public class iStock
{
    string itemCode;
    string itemDescription;
    string unitOfMeasure;
    int availableQty;
    string binNumber;
    float price;

    public static iStock Make(string itemCode, string itemDes, string uom, int avail, string bin, float price)
    {
        iStock stock = new iStock();
        stock.itemCode = itemCode;
        stock.itemDescription = itemDes;
        stock.unitOfMeasure = uom;
        stock.availableQty = avail;
        stock.binNumber = bin;
        stock.price = price;
        return stock;
    }

    [DataMember]
    public string ItemCode
    {
        get { return itemCode; }
        set { itemCode = value; }
    }

    [DataMember]
    public string ItemDescription
    {
        get { return itemDescription; }
        set { itemDescription = value; }
    }

    [DataMember]
    public string UnitofMeasure
    {
        get { return unitOfMeasure; }
        set { unitOfMeasure = value; }
    }


    [DataMember]
    public int AvailableQty
    {
        get { return availableQty; }
        set { availableQty = value; }
    }

    [DataMember]
    public string BinNumber
    {
        get { return binNumber; }
        set { binNumber = value; }
    }

    [DataMember]
    public float Price
    {
        get { return price; }
        set { price = value; }
    }

}

[DataContract]
public class iDiscrepancy
{
    string raisedBy;
    string totalAmount;


    public static iDiscrepancy Make( string raisedBy, string totalAmount)
    {
        iDiscrepancy des = new iDiscrepancy();
        des.raisedBy = raisedBy;
        des.totalAmount = totalAmount;
        return des;
    }


    [DataMember]
    public string RaisedBy
    {
        get { return raisedBy; }
        set { raisedBy = value; }
    }

    [DataMember]
    public string TotalAmount
    {
        get { return totalAmount; }
        set { totalAmount = value; }
    }



}

[DataContract]
public class iDiscrepancyDetail
{
    string itemCode;
    int quantity;
    string amount;
    string isAdded;
    string reason;


    public static iDiscrepancyDetail Make(string itemCode,int quantity,string amount,string isAdded,string reason)
    {
        iDiscrepancyDetail desd = new iDiscrepancyDetail();
        desd.itemCode = itemCode;
        desd.quantity = quantity;
        desd.amount = amount;
        desd.isAdded = isAdded;
        desd.reason = reason;
        return desd;
    }


    [DataMember]
    public string ItemCode
    {
        get { return itemCode; }
        set { itemCode = value; }
    }

    [DataMember]
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    [DataMember]
    public string Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    [DataMember]
    public string IsAdded
    {
        get { return isAdded; }
        set { isAdded = value; }
    }

    [DataMember]
    public string Reason
    {
        get { return reason; }
        set { reason = value; }
    }
}