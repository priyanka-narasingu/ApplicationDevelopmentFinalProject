using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

    [OperationContract]
    [WebGet(UriTemplate = "/CollectionPoint", ResponseFormat = WebMessageFormat.Json)]
    ICollectionPoint[] CollectionPointList();
    
    [OperationContract]
    [WebGet(UriTemplate = "/Department/{collectionPointCode}", ResponseFormat = WebMessageFormat.Json)]
    IDepartment[] GetDepartment(string collectionPointCode);

    [OperationContract]
    [WebGet(UriTemplate = "/Department", ResponseFormat = WebMessageFormat.Json)]
    IDepartment[] DepartmentList();
    
    [OperationContract]
    [WebGet(UriTemplate = "/StationeryDisbursement/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    IStationeryDisbursementDetail[] GetStationeryDisbursementDetails(string deptCode);

    [OperationContract]
    [WebGet(UriTemplate = "/StationeryDisbursement", ResponseFormat = WebMessageFormat.Json)]
    IStationeryDisbursement[] StationeryDisbursementList();

    [OperationContract]
    [WebInvoke(UriTemplate = "/StationeryDisbursement/update", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    bool UpdateDisbursement(IStationeryDisbursementDetail sdd);

    [OperationContract]
    [WebInvoke(UriTemplate = "/StationeryDisbursement/updateStatus", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    bool UpdateDisbursementStatus(IDisburseId disburseId);
    
    [OperationContract]
    [WebInvoke(UriTemplate = "/Login", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    string Login(ILogin ilogin);

    [OperationContract]
    [WebGet(UriTemplate = "/Login/{userName}", ResponseFormat = WebMessageFormat.Json)]
    ILoginCredentials getLoginCredentials(string username);


}

[DataContract]
public class ILogin
{
    string userName;
    string password;

    public static ILogin Make(string userName, string password)
    {
        ILogin ilogin = new ILogin();
        ilogin.userName = userName;
        ilogin.password = password;
        return ilogin;
    }

    [DataMember]
    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }

    [DataMember]
    public string Password
    {
        get { return password; }
        set { password = value; }
    }    
}

public class ILoginCredentials
{
    string username;
    string employeeID;
    string employeeName;
    string roleCode;
    string roleDescription;
    string deptName;
    string deptCode;

    public static ILoginCredentials Make(string username, string employeeID, string employeeName, string roleCode, string roleDescription, string deptName, string deptCode)
    {
        ILoginCredentials iLoginCredentials = new ILoginCredentials();
        iLoginCredentials.username = username;
        iLoginCredentials.employeeID = employeeID;
        iLoginCredentials.employeeName = employeeName;
        iLoginCredentials.roleCode = roleCode;
        iLoginCredentials.roleDescription = roleDescription;
        iLoginCredentials.deptName = deptName;
        iLoginCredentials.deptCode = deptCode;
        return iLoginCredentials;
    }

    [DataMember]
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    [DataMember]
    public string EmployeeID
    {
        get { return employeeID; }
        set { employeeID = value; }
    }

    [DataMember]
    public string EmployeeName
    {
        get { return employeeName; }
        set { employeeName = value; }
    }
    [DataMember]
    public string RoleCode
    {
        get { return roleCode; }
        set { roleCode = value; }
    }
    [DataMember]
    public string RoleDescription
    {
        get { return roleDescription; }
        set { roleDescription = value; }
    }
    [DataMember]
    public string DeptName
    {
        get { return deptName; }
        set { deptName = value; }
    }
    [DataMember]
    public string DeptCode
    {
        get { return deptCode; }
        set { deptCode = value; }
    }



}



[DataContract]
public class IDisburseId
{
    int disbursementId;
    string disbursementStatus;

    public static IDisburseId Make(int disbursementId, string disbursementStatus)
    {
        IDisburseId dId = new IDisburseId();
        dId.disbursementId = disbursementId;
        dId.disbursementStatus = disbursementStatus;


        return dId;
    }


    [DataMember]
    public int DisbursementId
    {
        get { return disbursementId; }
        set { disbursementId = value; }
    }

    [DataMember]
    public string DisbursementStatus
    {
        get { return disbursementStatus; }
        set { disbursementStatus = value; }
    }


}

[DataContract]
public class ICollectionPoint
{
    string collectionPointCode;
    string collectionPointName;
    string collectionTime;


    public static ICollectionPoint Make(string collectionPointCode, string collectionPointName, string collectionTime)
    {
        ICollectionPoint cPt = new ICollectionPoint();
        cPt.collectionPointCode = collectionPointCode;
        cPt.collectionPointName = collectionPointName;
        cPt.collectionTime = collectionTime;

        return cPt;

    }

    [DataMember]
    public string CollectionPointCode
    {
        get { return collectionPointCode; }
        set { collectionPointCode = value; }
    }

    [DataMember]
    public string CollectionPointName
    {
        get { return collectionPointName; }
        set { collectionPointName = value; }
    }

    [DataMember]
    public string CollectionTime
    {
        get { return collectionTime; }
        set { collectionTime = value; }
    }

}

[DataContract]
public class IDepartment
{
    string deptCode;
    string deptName;
    string collectionPoint;
    string deptContactNo;
    int deptCollectionPin;
    string deptRepCode;
    string deptRepName;


    public static IDepartment Make(string deptCode, string deptName, string collectionPoint, string deptContactNo, int deptCollectionPin, string deptRepCode, string deptRepName)
    {
        IDepartment dept = new IDepartment();
        dept.deptCode = deptCode;
        dept.deptName = deptName;
        dept.collectionPoint = collectionPoint;
        dept.deptContactNo = deptContactNo;
        dept.deptCollectionPin = deptCollectionPin;
        dept.deptRepCode = deptRepCode;
        dept.deptRepName = deptRepName;

        return dept;

    }

    [DataMember]
    public string DeptCode
    {
        get { return deptCode; }
        set { deptCode = value; }
    }

    [DataMember]
    public string DeptName
    {
        get { return deptName; }
        set { deptName = value; }
    }

    [DataMember]
    public string CollectionPoint
    {
        get { return collectionPoint; }
        set { collectionPoint = value; }
    }

    [DataMember]
    public string DeptContactNo
    {
        get { return deptContactNo; }
        set { deptContactNo = value; }
    }

    [DataMember]
    public int DeptCollectionPin
    {
        get { return deptCollectionPin; }
        set { deptCollectionPin = value; }
    }

    [DataMember]
    public string DeptRepCode
    {
        get { return deptRepCode; }
        set { deptRepCode = value; }
    }

    [DataMember]
    public string DeptRepName
    {
        get { return deptRepName; }
        set { deptRepName = value; }
    }


}

[DataContract]
public class IStationeryDisbursement
{
    int disbursementId;
    int retrievalId;
    string deptCode;
    string deptRep;
    string collectionPoint;
    string disbursementStatus;
    
    public static IStationeryDisbursement Make(int disbursementId, int retrievalId, string deptCode, string deptRep, string collectionPoint, string disbursementStatus)
    {
        IStationeryDisbursement sd = new IStationeryDisbursement();
        sd.disbursementId = disbursementId;
        sd.retrievalId = retrievalId;
        sd.deptCode = deptCode;
        sd.deptRep = deptRep;
        sd.collectionPoint = collectionPoint;
        sd.disbursementStatus = disbursementStatus;

        return sd;

    }

    [DataMember]
    public int DisbursementId
    {
        get { return disbursementId; }
        set { disbursementId = value; }
    }

    [DataMember]
    public int RetrievalId
    {
        get { return retrievalId; }
        set { retrievalId = value; }
    }

    [DataMember]
    public string DeptCode
    {
        get { return deptCode; }
        set { deptCode = value; }
    }

    [DataMember]
    public string DeptRep
    {
        get { return deptRep; }
        set { deptRep = value; }
    }

    [DataMember]
    public string CollectionPoint
    {
        get { return collectionPoint; }
        set { collectionPoint = value; }
    }

    [DataMember]
    public string DisbursementStatus
    {
        get { return disbursementStatus; }
        set { disbursementStatus = value; }
    }

}



[DataContract]
public class IStationeryDisbursementDetail
{
    int disbursementId;
    string itemCode;
    int requestedQty;
    int actualQty;
    string itemDescription;
    string unitOfMeasure;


    public static IStationeryDisbursementDetail Make(int disbursementId, string itemCode, int requestedQty, int actualQty, string itemDescription, string unitOfMeasure)
    {
        IStationeryDisbursementDetail sdd = new IStationeryDisbursementDetail();
        sdd.disbursementId = disbursementId;
        sdd.itemCode = itemCode;
        sdd.requestedQty = requestedQty;
        sdd.actualQty = actualQty;
        sdd.itemDescription = itemDescription;
        sdd.unitOfMeasure = unitOfMeasure;


        return sdd;

    }

    [DataMember]
    public int DisbursementId
    {
        get { return disbursementId; }
        set { disbursementId = value; }
    }

    [DataMember]
    public string ItemCode
    {
        get { return itemCode; }
        set { itemCode = value; }
    }

    [DataMember]
    public int RequestedQty
    {
        get { return requestedQty; }
        set { requestedQty = value; }
    }

    [DataMember]
    public int ActualQty
    {
        get { return actualQty; }
        set { actualQty = value; }
    }

    [DataMember]
    public string ItemDescription
    {
        get { return itemDescription; }
        set { itemDescription = value; }
    }

    [DataMember]
    public string UnitOfMeasure
    {
        get { return unitOfMeasure; }
        set { unitOfMeasure = value; }
    }

}