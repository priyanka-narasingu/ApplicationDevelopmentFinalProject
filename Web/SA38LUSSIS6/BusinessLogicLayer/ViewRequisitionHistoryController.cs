using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogicLayer
{
    public class ViewRequisitionHistoryController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();

        public List<Request> getEmpReqHistoryAll()
        {
            var q = from x in context.Requests
                    orderby x.DateCreated descending
                    select x;
            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryByStatus(String rStat)
        {
            var q = from x in context.Requests
                    where x.RequestStatus == rStat
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryAllByTimePeriod(DateTime fDate, DateTime tDate)
        {
            var q = from x in context.Requests
                    where (x.DateCreated >= fDate && x.DateCreated <= tDate)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryByStatusTimePeriod(String rStat, DateTime fDate, DateTime tDate)
        {
            var q = from x in context.Requests
                    where x.RequestStatus == rStat && (x.DateCreated >= fDate && x.DateCreated <= tDate)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        /*For Dept Representative*/

        public String getDeptCode(String username)
        {
            Employee e = new Employee();
            e = context.Employees.First(x => x.UserName == username);
            Employee emp = new Employee();
            var k = from y in context.Employees
                    where y.EmployeeID == e.EmployeeID
                    select y;
            emp = k.First();
            return emp.DeptCode.ToString();

        }

        public String getEmployeeName(String eID)
        {
            var q = from x in context.Employees
                    where x.EmployeeID == eID
                    select x.EmployeeName;
            return q.FirstOrDefault();
        }

        public List<Request> getEmpReqHistoryAllByDeptEmployees(String dCode)
        {
            var q = from x in context.Requests
                    where x.Employee1.DeptCode == dCode
                    orderby x.DateCreated descending
                    select x;
            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryByStatusByDeptEmployees(String dCode, String rStat)
        {
            var q = from x in context.Requests
                    where (x.Employee1.DeptCode == dCode) && (x.RequestStatus == rStat)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryAllByTimePeriodByDeptEmployees(DateTime fDate, DateTime tDate, String dCode)
        {
            var q = from x in context.Requests
                    where (x.DateCreated >= fDate && x.DateCreated <= tDate) && (x.Employee1.DeptCode == dCode)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryByStatusTimePeriodByDeptEmployees(String rStat, DateTime fDate, DateTime tDate, String dCode)
        {
            var q = from x in context.Requests
                    where x.RequestStatus == rStat && (x.DateCreated >= fDate && x.DateCreated <= tDate) && (x.Employee1.DeptCode == dCode)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        /*For Dept Employee*/

        public List<Request> getEmpReqHistoryAllByDept(String uName)
        {
            var q = from x in context.Requests
                    where x.Employee1.UserName == uName
                    orderby x.DateCreated descending
                    select x;
            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryByStatusByDept(String uName, String rStat)
        {
            var q = from x in context.Requests
                    where (x.Employee1.UserName == uName) && (x.RequestStatus == rStat)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryAllByTimePeriodByDept(DateTime fDate, DateTime tDate, String uName)
        {
            var q = from x in context.Requests
                    where (x.DateCreated >= fDate && x.DateCreated <= tDate) && (x.Employee1.UserName == uName)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        public List<Request> getEmpReqHistoryByStatusTimePeriodByDept(String rStat, DateTime fDate, DateTime tDate, String uName)
        {
            var q = from x in context.Requests
                    where x.RequestStatus == rStat && (x.DateCreated >= fDate && x.DateCreated <= tDate) && (x.Employee1.UserName == uName)
                    select x;

            List<Request> lEmpRq = (List<Request>)q.ToList();
            return lEmpRq;
        }

        /*For Dept Employee*/

        public DataTable getRequestDetails(int reqID)
        {

            List<RequestDetail> lc = getItemCodeRD(reqID);

            DataTable dt = new DataTable();

            DataColumn dc1 = new DataColumn();
            dc1.DataType = typeof(string);
            dc1.ColumnName = "ItemCode";

            DataColumn dc2 = new DataColumn();
            dc2.DataType = typeof(string);
            dc2.ColumnName = "ItemCategory";

            DataColumn dc3 = new DataColumn();
            dc3.DataType = typeof(string);
            dc3.ColumnName = "ItemDescription";

            DataColumn dc4 = new DataColumn();
            dc4.DataType = typeof(int);
            dc4.ColumnName = "Quantity";

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);

            foreach (RequestDetail st in lc)
            {
                Stock sRD = new Stock();

                DataRow dr = dt.NewRow();
                dr["ItemCode"] = st.ItemCode;
                dr["ItemCategory"] = getItemCategory(st.ItemCode);
                dr["ItemDescription"] = getItemDesc(st.ItemCode);
                dr["Quantity"] = st.Quantity;

                dt.Rows.Add(dr);
            }
            return dt;
        }


        public String getItemCategory(String iCode)
        {
            var q = from x in context.Stocks
                    where x.ItemCode == iCode
                    select x.ItemCategory;

            return q.FirstOrDefault();
        }


        public String getItemDesc(String iCode)
        {
            var q = from x in context.Stocks
                    where x.ItemCode == iCode
                    select x.ItemDescription;

            return q.FirstOrDefault();
        }

        public List<RequestDetail> getItemCodeRD(int rqID)
        {
            var q = from x in context.RequestDetails
                    where x.RequestID == rqID
                    select x;
            List<RequestDetail> lcde = (List<RequestDetail>)q.ToList();
            return lcde;
        }

        /** For Store Clerk **/
        public List<String> getReqStatusForStore()
        {
            List<String> statusList = new List<string>();
            statusList.Add("All");
            statusList.Add("Approved");
            statusList.Add("Outstanding");
            statusList.Add("Request Accepted");
            return statusList;
        }

        /** get requests by status **/
        public IQueryable getReqByStatus(String status)
        {
            var q = from r in context.Requests
                    where r.RequestStatus.Equals(status)
                    select new
                    {
                        r.RequestID,
                        r.Employee1.Department.DeptName,
                        r.RequestStatus,
                        r.DateCreated
                    };
            return q;
        }


        /** get request by time period and status **/
        public IQueryable getReqByStatusTimePeriod(String rStat, DateTime fDate, DateTime tDate)
        {
            var q = from x in context.Requests
                    where x.RequestStatus == rStat && (x.DateCreated >= fDate && x.DateCreated <= tDate)
                    select new
                    {
                        x.RequestID,
                        x.Employee1.Department.DeptName,
                        x.RequestStatus,
                        x.DateCreated

                    };

            return q;
        }

        /** Display all approved and outstanding and request accepted **/
        public IQueryable getAllReqForClerk()
        {
            var q = from r in context.Requests
                    where r.RequestStatus.Equals("Approved") || r.RequestStatus.Equals("Outstanding") || r.RequestStatus.Equals("Request Accepted")
                    orderby r.RequestStatus ascending
                    select new
                    {
                        r.RequestID,
                        r.Employee1.Department.DeptName,
                        r.RequestStatus,
                        r.DateCreated

                    };
            return q;

        }

        /** get request by from date and status **/
        public IQueryable getReqByFromDateStatus(DateTime fDate, String status)
        {
            var q = from x in context.Requests
                    where x.RequestStatus == status && (x.DateCreated == fDate)
                    select new
                    {
                        x.RequestID,
                        x.Employee1.Department.DeptName,
                        x.RequestStatus,
                        x.DateCreated

                    };
            return q;
        }

        /** get request **/
        public Request getRequest(int requestID)
        {
            var q = from r in context.Requests
                    where r.RequestID == requestID
                    select r;
            Request req = (Request)q.FirstOrDefault();
            return req;
        }

    }
}
