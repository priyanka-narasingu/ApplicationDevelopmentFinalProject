using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using BusinessLogicLayer.Exception_Package;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Web;


namespace BusinessLogicLayer
{
    public class GenerateDisbursementListController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();
        bool success = true;
        bool errorState = false;
        string errorMsg = "";

        /** Get the latest Stationery Retrieval ID **/
        public StationeryRetrieval getStationeryRetrieval()
        {
            var q = from i in context.StationeryRetrievals
                    where i.RetrievalStatus == false
                    orderby i.RetrievalID descending
                    select i;
            StationeryRetrieval sr = (StationeryRetrieval)q.FirstOrDefault();
            return sr;
        }

        /** Get list of Departments from Stationery Retrieval **/
        public List<String> getListOfDepartments(StationeryRetrieval sr)
        {
            List<String> departmentNames = (from sd in context.StationeryRetrievalDepts
                                            where (sd.RetrievalID == sr.RetrievalID) && (sd.DeletedFlag == false)
                                            select sd.Department.DeptName).Distinct().ToList();

            return departmentNames;
        }

        /** generate Disbursement List **/
        public bool getnerateAllDisbursementLists()
        {
            StationeryRetrieval sr = getStationeryRetrieval();

            /** Get the list of Department wise stationery requests **/
            var sdList = (from sd in context.StationeryRetrievalDepts
                          where (sd.RetrievalID == sr.RetrievalID) &&
                          (sd.DeletedFlag == false)
                          select sd).ToList();
            try
            {
                /** Get list of unique department codes **/
                List<String> dcs = sdList.Select(x => x.DeptCode).Distinct().ToList();
                ArrayList deptCodeList = new ArrayList(dcs.ToArray());

                foreach (String deptCode in deptCodeList)
                {
                    /** Create Stationery Disbursement **/

                    createStationeryDisbursement(sr.RetrievalID, deptCode);


                    /** Retreive latest Stationery Disbursement record **/
                    int disbursementID = (from d in context.StationeryDisbursements
                                          orderby d.DisbursementID descending
                                          select d.DisbursementID).FirstOrDefault();

                    foreach (StationeryRetrievalDept sd in sdList)
                    {
                        if (sd.DeptCode.Equals(deptCode))
                        {
                            /**Create Stationery Disbursement details**/
                            StationeryDisbursementDetail addStationeryDisbursementDetail = new StationeryDisbursementDetail();
                            addStationeryDisbursementDetail.DisbursementID = disbursementID;
                            addStationeryDisbursementDetail.ItemCode = sd.ItemCode;
                            addStationeryDisbursementDetail.RequestedQty = sd.RequestedQty;
                            addStationeryDisbursementDetail.ActualQty = sd.ActualQty;
                            addStationeryDisbursementDetail.DeletedFlag = false;
                            context.AddToStationeryDisbursementDetails(addStationeryDisbursementDetail);
                            context.SaveChanges();
                        }
                    }

                }
                /** change Statiobnery Retrieval status **/
                sr.RetrievalStatus = true;
                context.SaveChanges();

            }

            catch (Exception ex)
            {
                success = false;
                errorMsg = "Failed to generate Disbursement List for department";
                throw new CreateFailedException(errorMsg);
            }

            return success;
        }

        /** Create Stationery Disbursement **/
        public void createStationeryDisbursement(int retrievalID, String deptCode)
        {
            /** get departmet rep and collectiion point **/

            var q = from d in context.Departments
                    where d.DeptCode.Equals(deptCode)
                    select d;
            Department department = (Department)q.FirstOrDefault();
            try
            {
                StationeryDisbursement addStationeryDisbursement = new StationeryDisbursement();
                addStationeryDisbursement.RetrievalID = retrievalID;
                addStationeryDisbursement.DeptCode = deptCode;
                addStationeryDisbursement.DeptRep = department.DeptRep;
                addStationeryDisbursement.CollectionPoint = department.CollectionPoint;
                addStationeryDisbursement.DisbursementStatus = "Request Accepted";
                addStationeryDisbursement.DateUpdated = DateTime.Now;
                addStationeryDisbursement.DeletedFlag = false;
                context.AddToStationeryDisbursements(addStationeryDisbursement);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                errorMsg = "Failed to generate Disbursement List for department";
                throw new CreateFailedException(errorMsg);
            }


        }

        /** get Sationery Disbursement details **/

        public StationeryDisbursement getStationeryDisbursement(int retrievalID, String departmentName)
        {
            var q = from sd in context.StationeryDisbursements
                    where (sd.RetrievalID == retrievalID) && (sd.Department.DeptName.Equals(departmentName))
                    && (sd.DisbursementStatus.Equals("Request Accepted"))
                    select sd;
            StationeryDisbursement stationeryDisbursement = (StationeryDisbursement)q.FirstOrDefault();
            return stationeryDisbursement;

        }

        /** get department representative name **/
        public String getDepartmentRepName(String employeeID)
        {
            String repName = (from r in context.Employees
                              where r.EmployeeID.Equals(employeeID)
                              select r.EmployeeName).FirstOrDefault();
            return repName;
        }


        /** get stationery disbursement details **/
        public IQueryable getStaioneryDisbursementDetails(int disbursementId)
        {
            var q = from dd in context.StationeryDisbursementDetails
                    where (dd.DisbursementID == disbursementId) && (dd.DeletedFlag == false)
                    select new
                    {
                        dd.ItemCode,
                        dd.Stock.ItemDescription,
                        dd.RequestedQty,
                        dd.ActualQty
                    };
            return q;

        }

        /** Update Disbursement List **/

        public bool updateDisbursementList(String itemCode, int actualQty, int requiredQty, int disbursementID)
        {
            if (vailidateActulaQty(requiredQty, actualQty))
            {
                var q = (from sd in context.StationeryDisbursementDetails
                         where (sd.DisbursementID == disbursementID) &&
                         (sd.ItemCode.Equals(itemCode)) && (sd.DeletedFlag == false)
                         select sd).FirstOrDefault();
                if (q != null)
                {
                    StationeryDisbursementDetail updateDisbursementDetail = (StationeryDisbursementDetail)q;
                    updateDisbursementDetail.ActualQty = actualQty;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        errorState = true;
                        errorMsg = "Failed to update Disbursement List for the department";
                        throw new UpdateFailedException(errorMsg);
                    }
                }

            }
            else
            {
                errorState = true;
                errorMsg = "The actual quantity cannot be greater than required quantity";
                throw new UpdateFailedException(errorMsg);
            }

            return errorState;
        }

        /** update disbursement status **/
        public void updateDisbursementStatus(int disbursementID)
        {
            var q = from d in context.StationeryDisbursements
                    where d.DisbursementID == disbursementID
                    select d;
            StationeryDisbursement updateDisbursementStatus = (StationeryDisbursement)q.FirstOrDefault();
            updateDisbursementStatus.DisbursementStatus = "Ready for Disbursement";
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                errorState = true;
                errorMsg = "Update to Disbursement List failed";
                throw new UpdateFailedException(errorMsg);
            }
        }


        /** Validate actual quantity **/
        public bool vailidateActulaQty(int requiredQty, int actualQty)
        {
            bool result = true;
            if (actualQty > requiredQty)
                result = false;
            return result;

        }

        /** notify Disbursement to Department **/
        public void notifyDisbursement(String disbursementDate, String deptRepName, String departmentName)
        {
            /** get department rep email ID **/
            var q = from d in context.Employees
                    where d.EmployeeName.Equals(deptRepName)
                    select d.EmpEmail;
            String toEmail = q.ToString();

            /** get collection point details **/
            var p = (from c in context.Departments
                     where c.DeptName.Equals(departmentName)
                     select new
                     {
                         c.CollectionPoint1.CollectionPointName,
                         c.CollectionPoint1.CollectionTime
                     }).First();
            String collectionPoint = p.CollectionPointName;
            String collectionTime = p.CollectionTime;
            try
            {
                MailMessage m = new MailMessage("a0120540@nus.edu.sg", "deptrep.lussis@gmail.com"); 
                m.Subject = "Stationery Disbursement notification";
                m.IsBodyHtml = true;
                m.Body = "<div>Hello  " + deptRepName + ",<br/></br>" + " The Stationery requested by the department is ready for disbursement on " +
                          disbursementDate + "  at  " + collectionPoint + "   " + collectionTime + "<br/></br></br>" +
                         "Thank you <br/></br>" + "Logic University Store";

                SmtpClient smtp = new SmtpClient("lynx.iss.nus.edu.sg");
                smtp.Send(m);
            }
            catch (Exception ex)
            {
                errorMsg = "Email notification failed !";
                throw new NotificationFailedException(errorMsg);
            }
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.EnableSsl = true;
            //NetworkCredential NetworkCred = new NetworkCredential("gauri.2211", "Tegami2#2");
            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = NetworkCred;
            //smtp.Port = 587;
           
            
        }



    }
}
