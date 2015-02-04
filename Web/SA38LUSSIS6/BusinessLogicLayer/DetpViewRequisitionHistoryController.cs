using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogicLayer
{
 public   class DetpViewRequisitionHistoryController
    {
         private static int requestrownumber;

       
        SA38ADTeam6Entities ent = new SA38ADTeam6Entities();
        
    
        public List<Request> getRequest(string status)
        {
            List<Request> req = new List<Request>();
            var x = from y in ent.Requests
                    where y.RequestStatus.Trim().ToUpper() == status
                    select y;
            requestrownumber = x.Count();
            return req = x.ToList();
            
            
        }
        public List<Request> getRequestByUser(String username,string status)
        {
            Employee emp = new Employee();
            emp = ent.Employees.First(x=>x.UserName==username);
            List<Employee> emplist = new List<Employee>();
            String st = emp.DeptCode.ToString();
            var a = from b in ent.Employees
                    where b.DeptCode == st
                    select b;
            emplist = a.ToList();
            List<Request> req = new List<Request>();
            //foreach (Employee e in emplist)
            //{
                
            //    var k = from i in ent.Requests
            //            where i.EmployeeID == e.EmployeeID && i.RequestStatus==status
            //            select i;
            //    req = k.ToList();
                
            //};
            string s = status;
            var z = from y in ent.Requests
                    join j in ent.Employees
                    on y.EmployeeID equals j.EmployeeID
                    where j.DeptCode==st && y.RequestStatus.Trim()==status
                    select y;
            return req = z.ToList();
    
        
        }
        //public List<Request> getRequest(DateTime date, String status)
        //{
        //    List<Request> req = new List<Request>();
        //    var x = from y in ent.Requests
        //            where y.RequestStatus.Trim().ToUpper() == status && y.DateCreated == date
        //            select y;
        //    return req = x.ToList();

        //}
       
        public List<Employee> getEmployee()
        {
            List<Employee> emp = new List<Employee>();
            var x = from y in ent.Employees
                    join j in ent.Requests
                    on y.EmployeeID equals j.EmployeeID
                    where y.EmployeeID == j.EmployeeID
                    select y;
            return emp = x.ToList();
        }
      
        public List<DateTime> getDate()
        {
            List<DateTime> date = new List<DateTime>();
            
            var x = from y in ent.Requests
                    select y.DateCreated;
            foreach (DateTime dt in x)
            {
                date.Add(dt);
            }
            return date;

        }
        public int getrequestrownumber()
        { return requestrownumber; }

        public List<Request> getRequestByDate(DateTime date, string status)
        {
            List<Request> req = new List<Request>();
            var x = from y in ent.Requests
                    where y.RequestStatus == status && y.DateCreated == date
                    select y;
            return req = x.ToList();


        }
    }
    }

