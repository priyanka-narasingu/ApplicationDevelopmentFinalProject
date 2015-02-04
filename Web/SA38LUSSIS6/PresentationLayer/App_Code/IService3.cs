using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService3
{

    [OperationContract]
    [WebGet(UriTemplate = "/pendreq/{deptcode}", ResponseFormat = WebMessageFormat.Json)]
    iRequest[] RequestList(string deptcode);

    [OperationContract]
    [WebGet(UriTemplate = "/requestDetail/{RequestID}", ResponseFormat = WebMessageFormat.Json)]
    iRequestDetail[] getReqDetail(string RequestID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/updateRequestAccept/{requestID}/{approveby}", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    bool updateRequestAccept(string requestID, string approveby);

    [OperationContract]
    [WebInvoke(UriTemplate = "/updateRequestReject", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    bool updateRequestReject(iRejectcomments rj);
}

[DataContract]
public class iRequest
{

    int requestID;
    string employeeID;
    string dateCreated;
    string requestStatus;
    string comments;
    string approvedBy;
    string dateUpdated;
    string employeeName;
    string deptCode;



    public static iRequest Make(int requestID, string employeeID, string dateCreated, string requestStatus, string comments, string approvedBy, string dateUpdated)
    {
        iRequest r = new iRequest();
        r.requestID = requestID;
        r.EmployeeID = employeeID;
        r.dateCreated = dateCreated;
        r.requestStatus = requestStatus;
        r.comments = comments;
        r.approvedBy = approvedBy;
        r.dateUpdated = dateUpdated;

        return r;
    }

    [DataMember]
    public int RequestID
    {
        get { return requestID; }
        set { requestID = value; }
    }

    [DataMember]
    public string EmployeeID
    {
        get { return employeeID; }
        set { employeeID = value; }
    }

    [DataMember]
    public string DateCreated
    {
        get { return dateCreated; }
        set { dateCreated = value; }
    }

    [DataMember]
    public string RequestStatus
    {
        get { return requestStatus; }
        set { requestStatus = value; }
    }

    [DataMember]
    public string Comments
    {
        get { return comments; }
        set { comments = value; }
    }

    [DataMember]
    public string ApprovedBy
    {
        get { return approvedBy; }
        set { approvedBy = value; }
    }

    [DataMember]

    public string DateUpdated
    {
        get { return dateUpdated; }
        set { dateUpdated = value; }
    }

    [DataMember]
    public string EmployeeName
    {
        get { return employeeName; }
        set { employeeName = value; }
    }

    [DataMember]
    public string DeptCode
    {
        get { return deptCode; }
        set { deptCode = value; }
    }
}

[DataContract]
public class iRequestDetail
{
    int requestID;
    string itemCode;
    int quantity;
    string itemDescription;
    string unitofmeasure;


    public static iRequestDetail Make(int requestID, string itemCode, int quantity, string itemDescription, string unitofmeasure)
    {
        iRequestDetail r = new iRequestDetail();
        r.requestID = requestID;
        r.itemCode = itemCode;
        r.quantity = quantity;
        r.itemDescription = itemDescription;
        r.unitofmeasure = unitofmeasure;

        return r;
    }

    [DataMember]
    public int RequestID
    {
        get { return requestID; }
        set { requestID = value; }
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
    public string ItemDescription
    {
        get { return itemDescription; }
        set { itemDescription = value; }
    }

    [DataMember]
    public string Unitofmeasure
    {
        get { return unitofmeasure; }
        set { unitofmeasure = value; }
    }
}

    [DataContract]
    public class iRejectcomments
    {
        string requestID;
        string comments;

        public static iRejectcomments Make(string requestID, string comments) 
        {
            iRejectcomments rj = new iRejectcomments();
            rj.requestID = requestID;
            rj.comments = comments;
            return rj;
        }

     [DataMember]
     public string RequestID
        {
            get { return requestID; }
            set { requestID = value; }
        }

    [DataMember]
     public string Comments
     {
         get { return comments; }
         set { comments = value; }
     }
   }
