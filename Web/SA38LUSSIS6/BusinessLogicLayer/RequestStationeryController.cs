using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class RequestStationeryController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();

        public List<String> getItemCategory()
        {
            var q = (from x in context.Stocks
                     select x.ItemCategory).Distinct();
            List<String> lcat = (List<String>)q.ToList();
            return lcat;
        }

        public int getCurrentReqID()
        {
            var q = from x in context.Requests
                    orderby x.RequestID descending
                    select x.RequestID;
            int rd = q.FirstOrDefault();

            return rd + 1;
        }

        public int getReqID()
        {
            var q = from x in context.Requests
                    orderby x.RequestID descending
                    select x.RequestID;
            int rd = q.FirstOrDefault();

            return rd;
        }

        public String getEmpID(String uname)
        {
            var q = from x in context.Employees
                    where x.UserName == uname
                    select x.EmployeeID;
            return q.FirstOrDefault();
        }

        public String getEmpName(String uname)
        {
            var q = from x in context.Employees
                    where x.UserName == uname
                    select x.EmployeeName;
            return q.FirstOrDefault();
        }

        public String getDeptName(String eID)
        {
            var q = from x in context.Employees
                    where x.EmployeeID == eID
                    select x.Department.DeptName;

            return q.FirstOrDefault();
        }

        public String getDeptCode(String eID)
        {
            var q = from x in context.Employees
                    where x.EmployeeID == eID
                    select x.Department.DeptCode;

            return q.FirstOrDefault();
        }

        public String getDeptHeadName(String dCode)
        {
            var q = from x in context.Employees
                    where x.DeptCode == dCode && x.RoleCode == "DH"
                    select x.EmployeeName;
            return q.FirstOrDefault();
        }

        public String getDepHeadEmail(String dCode)
        {
            var q = from x in context.Employees
                    where x.DeptCode == dCode && x.RoleCode == "DH"
                    select x.EmpEmail;
            return q.FirstOrDefault();
        }

        public void saveRequestInfo(String eId, DateTime dc, string rs, string rc, DateTime du)
        {
            Request r = new Request();
            r.EmployeeID = eId;
            r.DateCreated = dc;
            r.RequestStatus = rs;
            r.Comments = rc;
            r.DateUpdated = du;
            r.DeletedFlag = false;

            context.AddToRequests(r);
            context.SaveChanges();
        }

        public void saveRequestDetailInfo(List<RequestDetail> ldet)
        {

            for (int i = 0; i < ldet.Count; i++)
            {
                RequestDetail rd = new RequestDetail();
                rd.RequestID = ldet[i].RequestID;
                rd.ItemCode = ldet[i].ItemCode;
                rd.Quantity = ldet[i].Quantity;
                rd.DeletedFlag = ldet[i].DeletedFlag;
                context.AddToRequestDetails(rd);
                context.SaveChanges();
            }
        }

    }
}
