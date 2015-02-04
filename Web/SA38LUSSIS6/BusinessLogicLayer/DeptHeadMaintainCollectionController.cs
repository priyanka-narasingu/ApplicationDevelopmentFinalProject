using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using BusinessLogicLayer.Exception_Package;

namespace BusinessLogicLayer
{
    public class DeptHeadMaintainCollectionController
    {

        SA38ADTeam6Entities ent = new SA38ADTeam6Entities();

        public List<Employee> getEmployee(String deptcode)
        {
            List<Employee> emp = new List<Employee>();
            var x = from y in ent.Employees
                    where y.DeptCode == deptcode

                    select y;
            return emp = x.ToList();



        }

        public Employee getRepresentativedEmployee(String deptcode)
        {
            Employee emp = new Employee();

            var x = from y in ent.Employees
                    where y.DeptCode == deptcode && y.RoleCode == "DR"
                    select y;
            return emp = x.First();

        }

        public String getEmpRole(String uName)
        {
            var q = from x in ent.Employees
                    where x.UserName == uName
                    select x.RoleCode;
            return q.FirstOrDefault();
        }

        public List<String> getDeptEmpList(String dCode)
        {
            var q = from x in ent.Employees
                    where x.DeptCode == dCode && x.RoleCode != "DR" && x.RoleCode != "DH"
                    select x.EmployeeName;
            return q.ToList();
        }

        public void updateRole(String uName, String uRole)
        {
            var q = from e in ent.Employees
                    where e.UserName.Equals(uName)
                    select e;
            Employee emp = q.FirstOrDefault();
            emp.RoleCode = uRole;
            ent.SaveChanges();
        }

        public void updateDepartmentRep(String dCode, String eID)
        {
            var q = from x in ent.Departments
                    where x.DeptCode.Equals(dCode)
                    select x;
            Department dp = (Department)q.FirstOrDefault();
            dp.DeptRep = eID;
            ent.SaveChanges();
        }

        public String getEmployeeID(String uName)
        {
            var q = from x in ent.Employees
                    where x.UserName.Equals(uName)
                    select x.EmployeeID;
            return q.FirstOrDefault().ToString();
        }

        public void updateDepartment(String deptcode, string collectionpoint)
        {
            try
            {
                Department dept = new Department(); ;
                var x = from y in ent.Departments
                        where y.DeptCode == deptcode
                        select y;
                dept = x.First();
                dept.CollectionPoint = collectionpoint;
                ent.SaveChanges();

            }
            catch (UpdateFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void updateEmployee(String empname)
        {
            try
            {
                Employee emp = new Employee();
                var x = from y in ent.Employees
                        where y.RoleCode == "DR"
                        select y;
                emp = x.First();
                emp.RoleCode = "DE";
                ent.SaveChanges();
                Employee emp1 = new Employee();
                var j = from k in ent.Employees
                        where k.EmployeeName == empname
                        select k;
                emp1 = j.First();
                emp.RoleCode = "DR";
                ent.SaveChanges();
            }
            catch (UpdateFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public String getDeptCode(String username)
        {
            Employee e = new Employee();
            e = ent.Employees.First(x => x.UserName == username);
            Employee emp = new Employee();
            var k = from y in ent.Employees
                    where y.EmployeeID == e.EmployeeID
                    select y;
            emp = k.First();
            return emp.DeptCode.ToString();

        }
        public String getUserName(String empname)
        {
            Employee emp = ent.Employees.First(x => x.EmployeeName == empname);
            return emp.UserName;
        }

        public Department getDepartment(String deptcode)
        {
            Department dept = new Department();
            dept = ent.Departments.First(x => x.DeptCode == deptcode);
            return dept;
        }

    }
}
