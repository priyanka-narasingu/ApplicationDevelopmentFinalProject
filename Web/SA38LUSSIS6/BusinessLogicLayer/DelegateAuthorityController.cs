using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using BusinessLogicLayer.Exception_Package;

namespace BusinessLogicLayer
{
    public class DelegateAuthorityController
    {

        SA38ADTeam6Entities context = new SA38ADTeam6Entities();

        //Get the list of all employees from a particular deparment
        public Employee[] getEmployees(string deptCode)
        {

            var emp = (from x in context.Employees
                       where x.DeptCode == deptCode && (x.RoleCode == "DE" || x.RoleCode == "DR")
                       select x);

            List<Employee> empList = emp.ToList();

            return (empList.ToArray<Employee>());

        }

        //Get the particular employee based on employee ID
        public Employee getEmployeeByID(string employeeID)
        {
            var e = (from x in context.Employees
                     where x.EmployeeID == employeeID
                     select x);

            Employee em = e.FirstOrDefault();
            return em;
        }

        //Assigning an employee as a delegate for a certain period of time
        public bool delegateEmployee(string employeeID, DateTime start, DateTime end)
        {
            Delegation delg = new Delegation();
            

            var d = (from x in context.Delegations
                     where x.EmployeeID == employeeID
                     select x.Employee.Department.DeptName);


            delg.EmployeeID = employeeID;
            delg.StartDate = start;
            delg.EndDate = end;
            delg.DelegatedFlag = true;
            context.AddToDelegations(delg);
           
            // Updating employee Role
            Employee emp = new Employee();
            emp = context.Employees.First(x=>x.EmployeeID==employeeID);
            emp.RoleCode = "DH";
            int i = context.SaveChanges();  
            return i > 0;
        }
        //Get the delegated employee for a particular department
        public Delegation getDelegatedEmployeeForDept(string deptcode)
        {
            var d = (from x in context.Delegations
                     where x.Employee.Department.DeptCode == deptcode && x.DelegatedFlag == true
                     select x);
            Delegation delg = d.FirstOrDefault();

            return delg;

        }

        //Revoking the delegation
        public bool revokeDelegate(String empID)
        {

            Delegation dg = context.Delegations.First(x => x.EmployeeID == empID);
            context.Delegations.DeleteObject(dg);

            //dg.DelegatedFlag = false;
            // Updating employee Role
            Employee emp = new Employee();
            emp = context.Employees.First(x => x.EmployeeID == empID);
            emp.RoleCode = "DE";
            int i=context.SaveChanges();
            return i > 0;
        }

        
        
        SA38ADTeam6Entities ent = new SA38ADTeam6Entities();

        public List<Employee> getEmployeeList(String deptcode)

        {
            List<Employee> emp = new List<Employee>();
            var x = from y in ent.Employees
                    where y.DeptCode == deptcode && (y.RoleCode == "DE" || y.RoleCode == "DR")

                    select y;
            return emp = x.ToList();

        }

        public Employee getEmployee(String username)
        {

            Employee emp = ent.Employees.First(x => x.UserName == username);
            return emp;
        }
        public void updateEmployee(String empname, String condition)
        {
            try
            {
                Employee emp = new Employee();

                emp = ent.Employees.First(x => x.EmployeeName == empname);


                if (condition == "delegate")
                {
                    emp.RoleCode = "DH";



                }
                if (condition == "revoke")
                {
                    emp.RoleCode = "DE";
                }
                ent.SaveChanges();
            }
            catch (UpdateFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void Delegate(String empname,DateTime fromdate,DateTime enddate)
        {
            try
            {
                Delegation dg = new Delegation();
                Employee emp = new Employee();
                emp = getEmployeeID(empname);
                dg.EmployeeID = emp.EmployeeID.ToString();
                dg.StartDate = fromdate;
                dg.EndDate = enddate;
                dg.DelegatedFlag = true;
                emp.RoleCode = "DH";
                ent.Delegations.AddObject(dg);
                ent.SaveChanges();
            }
            catch (UpdateFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void Delegate(String empname)
        {
            try
            {
                Delegation dg = new Delegation();
                Employee emp = new Employee();
                emp = getEmployeeID(empname);
                dg = ent.Delegations.First(x => x.EmployeeID == emp.EmployeeID);
                dg.DelegatedFlag = true;
                ent.SaveChanges();
            }
            catch (UpdateFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void reVoke(String empname)
        {
            try
            {
                //Delegation dg = new Delegation();
                //Employee emp = new Employee();
                //emp = getEmployeeID(empname);
                //dg.EmployeeID = emp.EmployeeID.ToString();
                //dg.DelegatedFlag = false;
                Employee emp = ent.Employees.First(x => x.EmployeeName == empname);
                Delegation dg = ent.Delegations.First(x => x.EmployeeID == emp.EmployeeID);
                emp.RoleCode = "DE";
                ent.Delegations.DeleteObject(dg);

                ent.SaveChanges();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }
        public Employee getEmployeeID(String empname)
        {

            Employee emp = ent.Employees.First(x => x.EmployeeName == empname);
            return emp;
        }
        public List<Delegation> getDelegatedEmployee(String deptcode)
        {
            List<Delegation> dele = new List<Delegation>();
            var x = from y in ent.Delegations
                    join j in ent.Employees on y.EmployeeID equals j.EmployeeID
                    where y.DelegatedFlag == true && j.DeptCode == deptcode
                    select y;
            return dele = x.ToList();
        }
        //method used by web
        //hello
        public string getUserName(string employeename)
        {
            Employee emp = new Employee();
            emp = ent.Employees.First(x=>x.EmployeeName==employeename);
            return emp.UserName.ToString();
        }
        public Delegation getDelegation(String employeeID)
        {

            Delegation dg = new Delegation();
            dg = ent.Delegations.First(x=>x.EmployeeID==employeeID);
            return dg;
        }
    }

}