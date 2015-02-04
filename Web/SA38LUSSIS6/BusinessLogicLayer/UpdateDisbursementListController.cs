using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using BusinessLogicLayer.Exception_Package;
namespace BusinessLogicLayer
{
    public class UpdateDisbursementListController
    {
        /** Mobile **/
        string status = "Ready for Disbursement";
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();

        public List<CollectionPoint> getCollectionPointList()
        {

            var q = from x in context.CollectionPoints
                    select x;

            List<CollectionPoint> cList = q.ToList<CollectionPoint>();
            return cList;

        }


        public List<Department> getDepartmentList()
        {
            var q = from x in context.Departments
                    select x;

            List<Department> dList = q.ToList<Department>();
            return dList;
        }


        public List<Department> getDepartment(string collectionPtCode)
        {
            var q = from x in context.Departments
                    where x.CollectionPoint == collectionPtCode
                    select x;

            List<Department> dList = q.ToList<Department>();
            return dList;

        }


        public string getDeptName(int disburseID)
        {

            var q = (from x in context.StationeryDisbursements
                    where x.DisbursementID == disburseID
                    select x).First();


            return q.Department.DeptName;

        }

        public List<StationeryDisbursement> getStationeryDisbursementList()
        {
            var q = from x in context.StationeryDisbursements
                    select x;

            List<StationeryDisbursement> sList = q.ToList<StationeryDisbursement>();
            return sList;
        }

        public List<StationeryDisbursement> getStationeryDisbursementReadyForDisburse(string deptCode)
        {
            var q = from x in context.StationeryDisbursements
                    where x.DeptCode == deptCode
                    && (x.DisbursementStatus == status)
                    select x;

            List<StationeryDisbursement> sList = q.ToList<StationeryDisbursement>();
            return sList;
        }


        public List<StationeryDisbursementDetail> getStationeryDisbursementDetail(int disbursementId)
        {
            var q = from x in context.StationeryDisbursementDetails
                    where x.DisbursementID == disbursementId
                    select x;

            List<StationeryDisbursementDetail> sList = q.ToList<StationeryDisbursementDetail>();
            return sList;
        }


        public int updateDisbursementQty(StationeryDisbursementDetail sdd)
        {
            var q = (from x in context.StationeryDisbursementDetails
                     where x.ItemCode == sdd.ItemCode && x.DisbursementID == sdd.DisbursementID
                     select x).First();

            q.ActualQty = sdd.ActualQty;

            int result;

            try
            {
                result = context.SaveChanges();
            }
            catch (Exception e)
            {
                result = 0;             
            }

            return result;
        }


        public int updateDisbursementStatusMobile(int disbursementId, string disbursementStatus) 
        {
            var q = (from x in context.StationeryDisbursements
                     where x.DisbursementID == disbursementId
                     select x).First();

            q.DisbursementStatus = disbursementStatus;
            int result;
            try
            {
                result = context.SaveChanges();
            }
            catch (Exception e)
            {
                result = 0;
            }

            return result;
        }


        /** Web **/

        bool errorState = false;
        String errorMsg = "";
        /** get list of Department Names **/
        public IQueryable getDepartmentNames()
        {
            var q = from d in context.Departments
                    select d.DeptName;
            return q;
        }

        /** get 'Ready for disbursement' lists for a department **/
        public IQueryable getReadyForDisbursementLists(string departmentName)
        {
            var q = from x in context.StationeryDisbursements
                    where x.Department.DeptName.Equals(departmentName)
                   // && ((x.DisbursementStatus.Equals("Ready for Disbursement"))
                   //|| (x.DisbursementStatus.Equals("Request Accepted")))
                    select new
                    {
                        x.DisbursementID,
                        x.DateUpdated,
                        x.CollectionPoint1.CollectionPointName,
                        x.Employee.EmployeeName,
                        x.DisbursementStatus
                    };

            return q;
        }

        /** Get Stationery Disbursement details **/
        public IQueryable getDisbursementDetailList(int disbursementId)
        {
            var q = from x in context.StationeryDisbursementDetails
                    where x.DisbursementID == disbursementId
                    select new
                    {
                        x.ItemCode,
                        x.Stock.ItemDescription,
                        x.RequestedQty,
                        x.ActualQty

                    };


            return q;
        }

        /** populate disbursement status dropdown list **/
        public List<String> getDisbursementStatus()
        {
            List<String> disbursementStatusList = new List<string>();
            disbursementStatusList.Add("Request Accepted");
            disbursementStatusList.Add("Ready for Disbursement");
            disbursementStatusList.Add("Delivered");
            disbursementStatusList.Add("Partially Delivered");
            disbursementStatusList.Add("Cancel");
            return disbursementStatusList;
        }

        /** Validate collection pin **/
        public bool validateCollectionPin(String departmentName, int collectionPin)
        {
            bool success = false;
            var q = from d in context.Departments
                    where d.DeptName.Equals(departmentName)
                    select d;
            Department dept = (Department)q.FirstOrDefault();
            if (dept.DeptCollectionPin == collectionPin)
                success = true;
            return success;

        }

        /** Validate actual quantity **/
        public bool vailidateActulaQty(int requestedQty, int actualQty)
        {
            bool result = true;
            if (actualQty > requestedQty)
                result = false;
            return result;

        }

        /** update disbursement status **/
        public void updateDisbursementStatus(int disbursementID, String disbursementStatus)
        {
            /** validate collection pin **/

            var q = from sd in context.StationeryDisbursements
                    where (sd.DisbursementID == disbursementID)
                    select sd;
            StationeryDisbursement updateStationeryDisbursement = (StationeryDisbursement)q.FirstOrDefault();
            updateStationeryDisbursement.DisbursementStatus = disbursementStatus;
            context.SaveChanges();

        }

        /** update disbursement details **/
        public void updateDisbursementDetails(int disbursementID, String itemCode, int requestedQty, int actualQty)
        {
            /** validate requested and actual quantity **/
            if (vailidateActulaQty(requestedQty, actualQty))
            {
                var q = from dd in context.StationeryDisbursementDetails
                        where (dd.DisbursementID == disbursementID) && (dd.ItemCode.Equals(itemCode))
                        select dd;
                StationeryDisbursementDetail updateDisbursementDetails = (StationeryDisbursementDetail)q.FirstOrDefault();
                updateDisbursementDetails.ActualQty = actualQty;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    errorState = true;
                    errorMsg = "Update failed";
                    throw new UpdateFailedException(errorMsg);
                }

            }
            else
            {
                errorState = true;
                errorMsg = "The actual quantity cannot be more than requested quantity";
                throw new UpdateFailedException(errorMsg);

            }

        }

        /** create new stationery request for outstanding quantity **/
        public void createRequest(String departmentName)
        {
            var q = (from d in context.Departments
                     where d.DeptName.Equals(departmentName)
                     select d).FirstOrDefault();

            Request addRequest = new Request();
            addRequest.EmployeeID = q.DeptRep;
            addRequest.DateCreated = DateTime.Now;
            addRequest.DateUpdated = DateTime.Now;
            addRequest.RequestStatus = "Outstanding";
            addRequest.DeletedFlag = false;
            context.AddToRequests(addRequest);
            context.SaveChanges();

        }
        public void createRequestDetails(int requestID, String itemCode, int requestedQty)
        {
            RequestDetail addRequestDetail = new RequestDetail();
            addRequestDetail.RequestID = requestID;
            addRequestDetail.ItemCode = itemCode;
            addRequestDetail.Quantity = requestedQty;
            addRequestDetail.DeletedFlag = false;
            context.AddToRequestDetails(addRequestDetail);
            context.SaveChanges();

        }

        /** get latest request id **/
        public int getRequestID()
        {
            int requestID = (from r in context.Requests
                             orderby r.RequestID descending
                             select r.RequestID).FirstOrDefault();
            return requestID;
        }

    }






}
