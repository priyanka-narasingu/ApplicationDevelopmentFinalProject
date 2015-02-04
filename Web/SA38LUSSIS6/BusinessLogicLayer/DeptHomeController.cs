using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class DeptHomeController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();

        public int getRequestNo(string status, string deptCode)
        {

            var q = from x in context.Requests
                    where x.RequestStatus == status
                    select x;

            List<Request> eList = q.ToList<Request>();            
            List<Request> eListByDept = new List<Request>();
            foreach (Request e in eList)
            {
                if (getDeptCode(e.EmployeeID) == deptCode)
                {
                    eListByDept.Add(e);
                }
            }

            return eListByDept.Count;
            
        }

        public int getRequestNoByEmp(string status, string empID)
        {
            var lastWeekDate = DateTime.Now.AddDays(-7);
            var currDate = DateTime.Now;
            var q = from x in context.Requests
                    where x.EmployeeID == empID && (x.DateUpdated >=lastWeekDate && x.DateUpdated<=currDate)
                    select x;
            
            return q.Count();
        }


        public int getRequestNoByEmpPast30days(string status, string empID)
        {
            var pastDate = DateTime.Now.AddDays(-30);

            var q = from x in context.Requests
                    where x.EmployeeID == empID && x.RequestStatus == status && x.DateUpdated > pastDate
                    select x;

            return q.Count();
        }

        public int getRequestNoByDeptPast30days(string status, string dName)
        {
            var pastDate = DateTime.Now.AddDays(-30);

            var q = from x in context.Requests
                    where x.Employee1.DeptCode == dName && x.RequestStatus == status && x.DateUpdated > pastDate
                    select x;
            return q.Count();
        }



         public int getRequestNoPast30days(string status, string deptCode)
        {
            var pastDate = DateTime.Now.AddDays(-30);

            var q = from x in context.Requests
                    where x.RequestStatus == status && x.DateUpdated > pastDate
                    select x;

            List<Request> eList = q.ToList<Request>();            
            List<Request> eListByDept = new List<Request>();
            foreach (Request e in eList)
            {
                if (getDeptCode(e.EmployeeID) == deptCode)
                {
                    eListByDept.Add(e);
                }
            }

            return eListByDept.Count;
            
        }
        


        public string getDeptCode(String eID)
        {
            RequestStationeryController rsc = new RequestStationeryController();
            return rsc.getDeptCode(eID);          

        }

        public string getUserDept(string username)
        {
            SA38ADTeam6Entities context = new SA38ADTeam6Entities();

            var q = (from x in context.Employees
                     where x.UserName == username
                     select x).First();

            return q.DeptCode;
        }


        public string getDelegatedEmployeeForDept(string deptCode)
        {
            DelegateAuthorityController dac = new DelegateAuthorityController();

            Delegation d = dac.getDelegatedEmployeeForDept(deptCode);

            string display = "Nil";

            if (d != null)
            {
                display = d.Employee.EmployeeName;
            }


            return display;

        }

        public string getCollectionPointName(string deptCode)
        {
            var q = (from x in context.Departments
                     where x.DeptCode == deptCode
                     select x).First();


            //return q.CollectionPoint1.CollectionPointName; // to return name but may be too long for field
            return q.CollectionPoint;

        }


        public string getEmpID(string username)
        {

            var q = (from x in context.Employees
                     where x.UserName == username
                     select x).First();

            return q.EmployeeID;
        }


    }
}
