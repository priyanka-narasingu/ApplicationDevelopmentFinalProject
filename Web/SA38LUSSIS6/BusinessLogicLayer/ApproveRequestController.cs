using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.Mail;
using BusinessLogicLayer.Exception_Package;
namespace BusinessLogicLayer
{
    public class ApproveRequestController
    {
        SA38ADTeam6Entities ent = new SA38ADTeam6Entities();
        public static int requestrow;
        

        public List<Request> getRequest()
        {
            var x = from y in ent.Requests
                    where y.RequestStatus.Trim().ToUpper() == "PENDING APPROVAL" || y.RequestStatus.Trim().ToUpper() == "REJECTED"
                    select y;
            List<Request> req = new List<Request>();
            return req = x.ToList<Request>();



        }
        public int getRequestRowNumber()
        {
            var x = from y in ent.Requests
                    where y.RequestStatus.Trim().ToUpper() == "PENDING APPROVAL" || y.RequestStatus.Trim().ToUpper() == "REJECTED"
                    select y;
            var count = x.Count();
            return Convert.ToInt32(count);
        }

        public Stock getDescription(String itemcode)
        {
            Stock s = ent.Stocks.First(x => x.ItemCode == itemcode);
            return s;

        }

        public List<RequestDetail> getReuestDetail(int requestid)
        {
            List<RequestDetail> rd = new List<RequestDetail>();
            var x = from y in ent.RequestDetails
                    where y.RequestID == requestid
                    select y;
            requestrow = x.Count();
            return rd = x.ToList();

        }

        public int getRequestRow()
        {
            return requestrow;
        }
        public void updateRequest(int requestno,string username)
        {
            try
            {
                Employee emp = ent.Employees.First(x=>x.UserName==username);
                Request r = ent.Requests.First(x => x.RequestID == requestno);
                r.RequestStatus = "Approved";
                r.ApprovedBy = emp.EmployeeID;
                ent.SaveChanges();
            }
            catch (UpdateFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void rejectRequest(int requestno, string comment)
        {
            try
            {
                Request r = ent.Requests.First(x => x.RequestID == requestno);
                r.RequestStatus = "Rejected";
                r.Comments = comment;
                ent.SaveChanges();
            }
            catch (UpdateFailedException ex)
            {
                Console.WriteLine (ex.Message);
            }
        }
        public Employee getEmployee(String empid)
        {
            Employee e = ent.Employees.First(x => x.EmployeeID == empid);
            return e;
        }

        public List<Request> getPendingRequestList(string deptID)
        {
            var q = from req in ent.Requests where req.RequestStatus.Trim().ToUpper() == "PENDING APPROVAL" && req.Employee1.DeptCode == deptID select req;
            Request r = new Request();
            r = q.First<Request>();
            string name = r.Employee1.EmployeeName;
            List<Request> mlist = q.ToList<Request>();
            return mlist;

        }

        public List<RequestDetail> getRequestDetail(string RequestID)
        {
            int requestid = Int32.Parse(RequestID);
            var q = from red in ent.RequestDetails where red.RequestID == requestid select red;
            RequestDetail rd = new RequestDetail();
            rd = q.First<RequestDetail>();
            string unitofmeasure = rd.Stock.UnitOfMeasure;
            List<RequestDetail> relist = q.ToList<RequestDetail>();
            return relist;

        }


        public Request updateRequestAccept(int requestID, string approveby)
        {
            var q = from req in ent.Requests where req.RequestID == requestID select req;
            Request r = q.First<Request>();
            r.ApprovedBy = approveby;
            r.DateUpdated = DateTime.Now;
            r.RequestStatus = "Approved";
            ent.SaveChanges();
            string email = r.Employee1.EmpEmail;
            NotifyToEmployee(requestID, "Approved", email);
            return r;
        }

        public Request updateRequestReject(int requestID, string comments)
        {
            var q = from req in ent.Requests where req.RequestID == requestID select req;
            Request r = q.First<Request>();
            r.DateUpdated = DateTime.Now;
            r.RequestStatus = "Rejected";
            r.Comments = comments;
            ent.SaveChanges();
            string email = r.Employee1.EmpEmail;
            NotifyToEmployee(requestID, "Rejected",email);
            return r;
        }
       public void NotifyToEmployee(int requestid,string status,string email)
        {
            MailMessage m = new MailMessage("a0120499@nus.edu.sg", email);
            m.Subject = status;
            m.Body = "RequestID "+requestid+"has "+status;
            SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
            c.Send(m);

        }
        public String getEmail(String name)
        {

            Employee emp = ent.Employees.First(x=>x.EmployeeName==name);
            return emp.EmpEmail.ToString();
        }
    }
}
