using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
 public   class RepMaintainCollectionPointController
    {
         SA38ADTeam6Entities ent = new SA38ADTeam6Entities();

        //public List<Employee> getEmployee(String deptcode)
        //{
        //    List<Employee> emp = new List<Employee>();
        //    var x = from y in ent.Employees
        //            where y.DeptCode == deptcode && y.RoleCode != "DR"

        //            select y;
        //    return emp = x.ToList();



        //}
        public Department getDepartment(String deptcode)
        {
            Department dept = new Department();
            dept = ent.Departments.First(x => x.DeptCode == deptcode);
            return dept;
        }
        public int updatePin(string deptcode,int pinnumber)
        {
            Department dept = new Department();
            dept = getDepartment(deptcode);
            dept.DeptCollectionPin = pinnumber;
            ent.SaveChanges();
            
            return Convert.ToInt32( dept.DeptCollectionPin);
        
        }
        //public Employee getRepresentativedEmployee(String deptcode)
        //{
        //    Employee emp = new Employee();

        //    var x = from y in ent.Employees
        //            where y.DeptCode == deptcode && y.RoleCode == "DR"
        //            select y;
        //    return emp = x.First();



        //}
        public void updateDepartment(String deptcode, string collectionpoint)
        {
            Department dept = new Department(); ;
            var x = from y in ent.Departments
                    where y.DeptCode == deptcode
                    select y;
            dept = x.First();
            dept.CollectionPoint = collectionpoint;
            ent.SaveChanges();
        }
        //////public void updateEmployee(String empname)
        //////{
        //////    Employee emp = new Employee();
        //////    var x = from y in ent.Employees
        //////            where y.RoleCode == "DR"
        //////            select y;
        //////    emp = x.First();
        //////    emp.RoleCode = "DE";
        //////    ent.SaveChanges();
        //////    Employee emp1 = new Employee();
        //////    var j = from k in ent.Employees
        //////            where k.EmployeeName == empname
        //////            select k;
        //////    emp1 = j.First();
        //////    emp.RoleCode = "DR";
        //////    ent.SaveChanges();



        //////}
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
        
    }
    }

