    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;
    using System.ServiceModel.Web;

[ServiceContract]
public interface IService1
{
   
    [OperationContract]
    [WebGet(UriTemplate = "/Employee/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    iEmployee[] List(string deptCode);

    [OperationContract]
    [WebGet(UriTemplate = "/AllEmployees/{employeeID}", ResponseFormat = WebMessageFormat.Json)]
    iEmployee AllEmployees(string employeeID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/DelegateEmployee", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    bool DelegateEmployee(iDelegate del);

    [OperationContract]
    [WebGet(UriTemplate = "/GetDelegate/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    iDelegate GetDelegate(string deptCode);

    [OperationContract]
    [WebInvoke(UriTemplate = "/RevokeDelegate/{empID}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    bool revokeDelegate(string empID);

    [OperationContract]
    [WebGet(UriTemplate = "/CheckDelegate/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    bool checkDelegate(string deptCode);
}


[DataContract]
public class iEmployee
{
    string employeeID;
    string employeeName;
    string deptCode;
    string username;
    string password;
    string roleCode;

    public static iEmployee Make(string employeeID, string employeeName, string departmentCode, string username, string password, string roleCode)
    {
        iEmployee emp = new iEmployee();
        emp.employeeID = employeeID;
        emp.employeeName = employeeName;
        emp.deptCode = departmentCode;
        emp.username = username;
        emp.password = password;
        emp.roleCode = roleCode;

        return emp;

    }
    public static iEmployee M(string employeeID, string employeeName)
    {
        iEmployee emp = new iEmployee();
        emp.employeeID = employeeID;
        emp.employeeName = employeeName;

        return emp;

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
    public string DeptCode
    {
        get { return deptCode; }
        set { deptCode = value; }
    }


    [DataMember]
    public string UserName
    {
        get { return username; }
        set { username = value; }
    }

    [DataMember]
    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    [DataMember]
    public string Role
    {
        get { return roleCode; }
        set { roleCode = value; }
    }
}
[DataContract]
public class iDelegate
{
    string employeeID;
    string employeeName;
    string startDate;
    string endDate;
    

    public static iDelegate Make(string employeeID, string employeeName, string startDate, string endDate)
    {
        iDelegate emp = new iDelegate();
        emp.employeeID = employeeID;
        emp.employeeName = employeeName;
        emp.startDate = startDate;
        emp.endDate = endDate;
      
        return emp;

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
    public string StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }


    [DataMember]
    public string EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }

 
}
