using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BusinessLogicLayer;
using System.Web.Security;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{

    UpdateDisbursementListController udlc = new UpdateDisbursementListController();
    LoginController lc = new LoginController();


    static string connection = "data source=(local); Integrated security=SSPI; initial catalog=sa38adteam6";
    private static string readyForDisbursement = "Ready For Disbursement";
    //static string approved = "approved";

    private static string cancelDisbursement = "Cancel";
    private static string partialDisbursement = "Partially Delivered";



    public ICollectionPoint[] CollectionPointList()
    {        
        List<ICollectionPoint> iList = new List<ICollectionPoint>();
        List<CollectionPoint> list = udlc.getCollectionPointList();


        foreach (CollectionPoint cPt in list)
        {
            ICollectionPoint iCPt = new ICollectionPoint();
            iCPt.CollectionPointCode = cPt.CollectionPointCode;
            iCPt.CollectionPointName = cPt.CollectionPointName;
            iCPt.CollectionTime = cPt.CollectionTime;
            iList.Add(iCPt);
        }
        
        return (iList.ToArray<ICollectionPoint>());
                
    }


    public IDepartment[] GetDepartment(string collectionPointCode)
    {
        
        List<IDepartment> iList = new List<IDepartment>();
        List<Department> list = udlc.getDepartment(collectionPointCode);


        foreach (Department dept in list)
        {
            IDepartment idept = new IDepartment();
            idept.DeptCode = dept.DeptCode;
            idept.DeptName = dept.DeptName;
            idept.CollectionPoint = dept.CollectionPoint;
            idept.DeptContactNo = dept.DeptContactNo;
            idept.DeptCollectionPin = (int)dept.DeptCollectionPin;
            idept.DeptRepCode = dept.DeptRep;
            idept.DeptRepName = dept.Employee.EmployeeName;
            iList.Add(idept);
        }
        
        return (iList.ToArray<IDepartment>());
        
    }


    public IDepartment[] DepartmentList()
    {
        List<IDepartment> iList = new List<IDepartment>();
        List<Department> list = udlc.getDepartmentList();


        foreach (Department dept in list)
        {
            IDepartment idept = new IDepartment();
            idept.DeptCode = dept.DeptCode;
            idept.DeptName = dept.DeptName;
            idept.CollectionPoint = dept.CollectionPoint;
            idept.DeptContactNo = dept.DeptContactNo;
            idept.DeptCollectionPin = (int) dept.DeptCollectionPin;
            idept.DeptRepCode = dept.DeptRep;
            idept.DeptRepName = dept.Employee.EmployeeName;
            iList.Add(idept);
        }

        return (iList.ToArray<IDepartment>());
    }


    public IStationeryDisbursementDetail[] GetStationeryDisbursementDetails(string deptCode)
    {          
        
        List<StationeryDisbursement> listSD = udlc.getStationeryDisbursementReadyForDisburse(deptCode);
        List<StationeryDisbursementDetail> listSDD = new List<StationeryDisbursementDetail>();
        List<StationeryDisbursementDetail> listSDD1 = new List<StationeryDisbursementDetail>();

        //to get the list of stationerydisbursement for each dept
        foreach (StationeryDisbursement sd1 in listSD)
        {
            
            listSDD = udlc.getStationeryDisbursementDetail(sd1.DisbursementID);

            foreach (StationeryDisbursementDetail sd2 in listSDD)
            {
                listSDD1.Add(sd2);
            }
    
        }


        List<IStationeryDisbursementDetail> iListSDD = new List<IStationeryDisbursementDetail>();

        foreach (StationeryDisbursementDetail sdd in listSDD1)
        {
            IStationeryDisbursementDetail iStat = new IStationeryDisbursementDetail();
            iStat.DisbursementId = (int)sdd.DisbursementID;
            iStat.ItemCode = sdd.ItemCode;
            iStat.RequestedQty = (int)sdd.RequestedQty;
            iStat.ActualQty = (int)sdd.ActualQty;
            iStat.ItemDescription = sdd.Stock.ItemDescription;
            iStat.UnitOfMeasure = sdd.Stock.UnitOfMeasure;
            iListSDD.Add(iStat);

        }
        
        return iListSDD.ToArray<IStationeryDisbursementDetail>();

                
    }



    public IStationeryDisbursement[] StationeryDisbursementList()
    {       
        List<IStationeryDisbursement> iList = new List<IStationeryDisbursement>();
        List<StationeryDisbursement> list = udlc.getStationeryDisbursementList();


        foreach (StationeryDisbursement sd in list)
        {
            IStationeryDisbursement iSD = new IStationeryDisbursement();
            iSD.DisbursementId = (int)sd.DisbursementID;
            iSD.RetrievalId = (int)sd.RetrievalID;
            iSD.DeptCode = sd.DeptCode;
            iSD.DeptRep = sd.DeptRep;
            iSD.CollectionPoint = sd.CollectionPoint;
            iSD.DisbursementStatus = sd.DisbursementStatus;
            iList.Add(iSD);
        }

        return (iList.ToArray<IStationeryDisbursement>());
        
    }


    public bool UpdateDisbursement(IStationeryDisbursementDetail sdd)
    {
        StationeryDisbursementDetail sdd1 = new StationeryDisbursementDetail();
        
        sdd1.DisbursementID = sdd.DisbursementId;
        sdd1.ItemCode = sdd.ItemCode;
        sdd1.RequestedQty = sdd.RequestedQty;
        sdd1.ActualQty = sdd.ActualQty;

        int result = udlc.updateDisbursementQty(sdd1);

        return (result > 0);
                

    }


    public bool UpdateDisbursementStatus(IDisburseId disburseId)
    {
        


        string deptName = udlc.getDeptName(disburseId.DisbursementId);

        int result = udlc.updateDisbursementStatusMobile(disburseId.DisbursementId, disburseId.DisbursementStatus);

        if (disburseId.DisbursementStatus == cancelDisbursement)
        {            

            udlc.createRequest(deptName);

            List<StationeryDisbursementDetail> list = udlc.getStationeryDisbursementDetail(disburseId.DisbursementId);


            foreach (StationeryDisbursementDetail sdd in list)
            {
                udlc.createRequestDetails(udlc.getRequestID(), sdd.ItemCode, (int)sdd.RequestedQty);
            }
            
        }
        else if (disburseId.DisbursementStatus == partialDisbursement)
        {
            udlc.createRequest(deptName);

            List<StationeryDisbursementDetail> list = udlc.getStationeryDisbursementDetail(disburseId.DisbursementId);

            foreach (StationeryDisbursementDetail sdd in list)
            {
                int shortfall = (int) sdd.RequestedQty - (int) sdd.ActualQty;

                if (shortfall > 0)
                {
                    udlc.createRequestDetails(udlc.getRequestID(), sdd.ItemCode, shortfall);
                }
            }
        }

        return (result > 0);
        

    }


    public string Login(ILogin ilogin)
    {
        string userRole = "failed";
                
        if (Membership.ValidateUser(ilogin.UserName, ilogin.Password))
        {
            LoginController lc = new LoginController();
            userRole = lc.getUserRole(ilogin.UserName);            
        }

        return userRole;
    }

    public ILoginCredentials getLoginCredentials(string username)
    {
        Employee emp = lc.getLoginCredentials(username);

        ILoginCredentials ilc = new ILoginCredentials();
        ilc.Username = emp.UserName;
        ilc.EmployeeID = emp.EmployeeID;
        ilc.EmployeeName = emp.EmployeeName;
        ilc.RoleCode = emp.RoleCode;
        ilc.RoleDescription = emp.UserRole.RoleDescription;
        ilc.DeptName = emp.Department.DeptName;
        ilc.DeptCode = emp.DeptCode;

        return ilc;

    }


}





