using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BusinessLogicLayer;
using System.Web.Security;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service3 : IService3
{
    public iRequest[] RequestList(string deptcode)
    {
        ApproveRequestController ar = new ApproveRequestController();
        List<Request> list = new List<Request>();
        list = ar.getPendingRequestList(deptcode);
        List<iRequest> ilist = new List<iRequest>();
        for (int i = 0; i < list.Count; i++)
        {
            iRequest ireq = new iRequest();
            ireq = ChangeRequestEntitytoiRequest(list[i]);
            ilist.Add(ireq);
        }

        return (ilist.ToArray<iRequest>());
    }
    public iRequest ChangeRequestEntitytoiRequest(Request req)
    {
        iRequest ireq = new iRequest();
        ireq.RequestID = req.RequestID;
        ireq.EmployeeID = req.EmployeeID;
        ireq.DateCreated = Convert.ToDateTime(req.DateCreated).ToString("MM-dd-yyyy");
        ireq.RequestStatus = req.RequestStatus;
        ireq.Comments = req.Comments;
        ireq.ApprovedBy = req.ApprovedBy;
        ireq.DateUpdated = Convert.ToDateTime(req.DateUpdated).ToString("MM-dd-yyyy");
        ireq.EmployeeName = req.Employee1.EmployeeName;
        ireq.DeptCode = req.Employee1.DeptCode;
        return ireq;
    }

    public bool updateRequestAccept(string requestID, string approveby)
    {
        int reid = Int32.Parse(requestID);
        ApproveRequestController ar = new ApproveRequestController();
        Request rq = new Request();
        rq.RequestID = reid;
        rq.ApprovedBy = approveby;
        ar.updateRequestAccept(rq.RequestID, rq.ApprovedBy);
        return true;
    }


    public bool updateRequestReject(iRejectcomments rj)
    {
        ApproveRequestController ar = new ApproveRequestController();
        ar.updateRequestReject(Int32.Parse(rj.RequestID), rj.Comments);
        return true;
    }

    public iRequestDetail[] getReqDetail(string RequestID)
    {
        ApproveRequestController ar = new ApproveRequestController();
        List<RequestDetail> rdlist = new List<RequestDetail>();
        rdlist = ar.getRequestDetail(RequestID);
        List<iRequestDetail> irdlist = new List<iRequestDetail>();
        for (int i = 0; i < rdlist.Count; i++)
        {
            iRequestDetail ird = new iRequestDetail();
            ird = ChangeRequestEntitytoiRequestDetail(rdlist[i]);
            irdlist.Add(ird);
        }
        return irdlist.ToArray();
    }

    public iRequestDetail ChangeRequestEntitytoiRequestDetail(RequestDetail rd)
    {
        iRequestDetail ird = new iRequestDetail();
        ird.RequestID = rd.RequestID;
        ird.ItemCode = rd.ItemCode;
        ird.Quantity = (int)rd.Quantity;
        ird.ItemDescription = rd.Stock.ItemDescription;
        ird.Unitofmeasure = rd.Stock.UnitOfMeasure;
        return ird;

    }

}





