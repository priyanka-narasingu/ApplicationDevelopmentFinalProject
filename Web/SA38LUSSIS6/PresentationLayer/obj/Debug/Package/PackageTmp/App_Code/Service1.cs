using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BusinessLogicLayer;
using System.Globalization;



public class Service1 : IService1
{
    SA38ADTeam6Entities context = new SA38ADTeam6Entities();
    DelegateAuthorityController delegateAuthorityController = new DelegateAuthorityController();


    public iEmployee[] List(string deptCode)
    {  
       Employee[] employee =delegateAuthorityController.getEmployees(deptCode);

        List<iEmployee> list = new List<iEmployee>();
        foreach (BusinessLogicLayer.Employee e in employee)
        {
            iEmployee em = iEmployee.Make(e.EmployeeID, e.EmployeeName, e.DeptCode, e.UserName, e.Password, e.RoleCode);

            list.Add(em);
        }
 
        return (list.ToArray<iEmployee>());


    }
    public iEmployee AllEmployees(string employeeID)
    {
        iEmployee iemployee = null;
 
Employee employee = delegateAuthorityController.getEmployeeByID(employeeID);

        iemployee = iEmployee.Make(employee.EmployeeID, employee.EmployeeName, employee.DeptCode, employee.UserName, employee.Password, employee.RoleCode);        return iemployee;
    }

    public bool DelegateEmployee(iDelegate idel)
    {
        string empID=idel.EmployeeID.ToString();
  
        DateTime start=Convert.ToDateTime(idel.StartDate.ToString());
        DateTime end=Convert.ToDateTime(idel.EndDate.ToString());
        bool delFlag = delegateAuthorityController.delegateEmployee(empID, start, end);
        return delFlag; 

    }
    public iDelegate GetDelegate(string deptCode)
    {
        Delegation delegation = delegateAuthorityController.getDelegatedEmployeeForDept(deptCode);
        String sDate= delegation.StartDate.ToString();
        String eDate= delegation.EndDate.ToString();
        iDelegate idelegate = new iDelegate();

        DateTime s = (DateTime) delegation.StartDate;
        DateTime d = (DateTime)delegation.EndDate;

 

        idelegate.EmployeeID = delegation.EmployeeID;

        idelegate.EmployeeName = delegation.Employee.EmployeeName;
        idelegate.StartDate = s.ToShortDateString().Replace("/", "-");
        idelegate.EndDate = d.ToShortDateString().Replace("/", "-");

        return idelegate;

    }
    public bool revokeDelegate(string empID)
    {
        bool checkRevoke = delegateAuthorityController.revokeDelegate(empID);

        return checkRevoke;
    }

    public bool checkDelegate(string deptCode)
    {
        bool delegatedFlag;
        Delegation delegation;        
        delegation = delegateAuthorityController.getDelegatedEmployeeForDept(deptCode);
        if(delegation!=null )
        {
            delegatedFlag=true;
        }
        else
        {
            DateTime d = (DateTime)delegation.EndDate;
            bool checkDate = d.Date < DateTime.Now.Date;
            if (checkDate == true)
            {   delegatedFlag = false;
                delegateAuthorityController.revokeDelegate(delegation.EmployeeID);
            }
            delegatedFlag = false;
        }
        return delegatedFlag;
    }


}








