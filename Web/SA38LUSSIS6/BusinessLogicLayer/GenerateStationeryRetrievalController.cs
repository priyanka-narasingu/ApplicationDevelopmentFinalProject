using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using BusinessLogicLayer.Exception_Package;

namespace BusinessLogicLayer
{
    public class GenerateStationeryRetrievalController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();
        bool errorState = false;
        bool success = true;
        string errorMsg = "";

        /**Get all Approved and Outstanding Requests **/
        public bool generateStationeryRetrieval()
        {
            var q = from rd in context.RequestDetails
                    where rd.Request.RequestStatus.Equals("Approved") ||
                          rd.Request.RequestStatus.Equals("Outstanding") &&
                          (rd.DeletedFlag == false)
                    select rd;
            List<RequestDetail> rdList = q.ToList<RequestDetail>();
            if (!(rdList.Count == 0))
            {
                /** get unique item codes for the list of selected requests **/
                List<String> ids = rdList.Select(x => x.ItemCode.ToString()).Distinct().ToList();
                ArrayList itemList = new ArrayList(ids.ToArray());

                /** Create new Stationery Retrieval **/
                StationeryRetrieval addStationeryRetrieval = new StationeryRetrieval();
                addStationeryRetrieval.DateRetrieved = DateTime.Now;
                addStationeryRetrieval.DeletedFlag = false;
                addStationeryRetrieval.RetrievalStatus = false;
                context.AddToStationeryRetrievals(addStationeryRetrieval);
                try
                {
                    context.SaveChanges();
                    /** Select the latest Stationery Retrieval ID **/
                    int RetrievalID = (from i in context.StationeryRetrievals
                                       where i.RetrievalStatus == false
                                       select i.RetrievalID).FirstOrDefault();
                    /** Create Item wise and Department wise list **/
                    createItemWiseList(rdList, itemList, RetrievalID);
                    createDepartmentWiseList(rdList, itemList, RetrievalID);
                    changeRequestStatus(rdList);
                }
                catch (Exception ex)
                {
                    success = false;
                    errorMsg = "Failed to generate Stationery Retrieval Form";
                    throw new CreateFailedException(errorMsg);
                }
            }
            else success = false;
            return success;

        }

        /** Create Stationery Retrieval Detail **/
        public void createItemWiseList(List<RequestDetail> rdList, ArrayList itemList, int RetrievalID)
        {
            String itemCode;
            int itemCount;
            int availableQty;

            foreach (var i in itemList)
            {
                itemCode = i.ToString();
                itemCount = 0;
                availableQty = 0;

                try
                {
                    foreach (RequestDetail r in rdList)
                    {
                        if (itemCode == r.ItemCode)
                        {
                            itemCount += Convert.ToInt32(r.Quantity);
                            availableQty = Convert.ToInt32(r.Stock.AvailableQty);
                        }

                    }

                    /**create new stationery retrieval detail**/
                    StationeryRetrievalDetail addStationeryRetrievalDetail = new StationeryRetrievalDetail();
                    addStationeryRetrievalDetail.RetrievalID = RetrievalID;
                    addStationeryRetrievalDetail.ItemCode = itemCode;
                    addStationeryRetrievalDetail.RequestedQty = itemCount;
                    addStationeryRetrievalDetail.AvailableQty = availableQty;
                    addStationeryRetrievalDetail.DeletedFlag = false;
                    context.AddToStationeryRetrievalDetails(addStationeryRetrievalDetail);
                    context.SaveChanges();
                    updateStock(itemCode, itemCount);
                   
                }
                catch (Exception ex)
                {
                    errorState = true;
                    errorMsg = "Failed to generate Stationery Retrieval Form";
                    throw new Exception_Package.CreateFailedException(errorMsg);
                }

            }

        }

        /** Create Department wise Stationery retrieval list **/
        public void createDepartmentWiseList(List<RequestDetail> rdList, ArrayList itemList, int RetrievalID)
        {
            String itemCode;
            String deptCode;
            int requestedQty;
            int availableQty;
            int actualQty;

            try
            {
                List<String> dpc = rdList.Select(x => x.Request.Employee1.DeptCode.ToString()).Distinct().ToList();
                ArrayList deptList = new ArrayList(dpc.ToArray());

                /**check the department for each item in Request detail**/
                foreach (var i in itemList)
                {
                    itemCode = i.ToString();
                    var avbQty = (from a in context.StationeryRetrievalDetails
                                  where a.ItemCode.Equals(itemCode) && (a.RetrievalID == RetrievalID)
                                  select a.AvailableQty).FirstOrDefault(); ;

                    availableQty = Convert.ToInt32(avbQty);


                    foreach (var d in deptList)
                    {
                        deptCode = d.ToString();
                        requestedQty = 0;
                        actualQty = 0;

                        foreach (RequestDetail rd in rdList)
                        {
                            if (rd.ItemCode.Equals(itemCode))
                            {
                                if (rd.Request.Employee1.Department.DeptCode.Equals(deptCode))
                                {
                                    requestedQty += Convert.ToInt32(rd.Quantity);

                                }
                            }
                        }

                        /** Distribute available quantity **/
                        if (!(requestedQty == 0))
                        {
                            if (!(availableQty <= 0))
                            {
                                if (requestedQty <= availableQty)
                                {
                                    actualQty = requestedQty;

                                }
                                else if (requestedQty > availableQty)
                                {
                                    actualQty = availableQty;
                                }
                                availableQty = availableQty - actualQty;
                            }

                            /** Create Department wise item list **/
                            StationeryRetrievalDept addStationeryRetrievalDept = new StationeryRetrievalDept();
                            addStationeryRetrievalDept.RetrievalID = RetrievalID;
                            addStationeryRetrievalDept.DeptCode = deptCode;
                            addStationeryRetrievalDept.ItemCode = itemCode;
                            addStationeryRetrievalDept.RequestedQty = requestedQty;
                            addStationeryRetrievalDept.ActualQty = actualQty;
                            addStationeryRetrievalDept.DeletedFlag = false;
                            context.AddToStationeryRetrievalDepts(addStationeryRetrievalDept);
                            context.SaveChanges();
                        }

                    }
                }
            }


            catch (Exception ex)
            {
                errorState = true;
                errorMsg = "Failed to generate Stationery Retrieval Form";
                throw new CreateFailedException(errorMsg);
            }

        }

        /** Get Stationery Retrieval ID **/
        public StationeryRetrieval getstationeryRetrieval()
        {
            var q = from i in context.StationeryRetrievals
                    where i.RetrievalStatus == false
                    select i;
            StationeryRetrieval sr = (StationeryRetrieval)q.FirstOrDefault();

            return sr;
        }

        /** Get item wise list **/
        public IQueryable getItemWiseList(int RetrievalID)
        {
            var q = from rd in context.StationeryRetrievalDetails
                    where rd.RetrievalID == RetrievalID
                    select new
                    {
                        rd.ItemCode,
                        rd.Stock.ItemCategory,
                        rd.Stock.ItemDescription,
                        rd.Stock.BinNumber,
                        rd.RequestedQty,
                        rd.AvailableQty
                    };
            return q;
        }

        /** Get first record in item wise list **/
        public StationeryRetrievalDetail getFirstItem(int retrievalID)
        {
            var q = from rd in context.StationeryRetrievalDetails
                    where (rd.RetrievalID == retrievalID)
                    orderby rd.ItemCode ascending
                    select rd;

            StationeryRetrievalDetail sd = (StationeryRetrievalDetail)q.FirstOrDefault();
            return sd;
        }
        /** Get Department Wise List **/
        public IQueryable getDepartmentWiseList(int retrievalID, String itemCode)
        {
            var q = from sd in context.StationeryRetrievalDepts
                    where (sd.RetrievalID == retrievalID) && (sd.ItemCode.Equals(itemCode))
                    select new
                    {
                        sd.Department.DeptName,
                        sd.RequestedQty,
                        sd.ActualQty
                    };
            return q;
        }

        /** Update Department wise item list **/
        public bool updateDepartmentWiseList(String departmentName, String itemCode, int actualQty, int requiredQty, int retrievalID)
        {
            if (vailidateActulaQty(requiredQty, actualQty))
            {
                var q = (from sd in context.StationeryRetrievalDepts
                         where (sd.RetrievalID == retrievalID) && (sd.Department.DeptName.Equals(departmentName)) &&
                         (sd.ItemCode.Equals(itemCode)) && (sd.DeletedFlag == false)
                         select sd).FirstOrDefault();
                if (q != null)
                {
                    StationeryRetrievalDept updateStationeryRetrievalDept = (StationeryRetrievalDept)q;
                    updateStationeryRetrievalDept.ActualQty = actualQty;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        errorState = true;
                        errorMsg = "Failed to generate Stationery Retrieval Form";
                        throw new UpdateFailedException(errorMsg);
                    }
                }

            }
            else
            {
                errorState = true;
                errorMsg = "The actual quantity cannot be greater than requested quantity";
                throw new UpdateFailedException(errorMsg);
            }

            return errorState;
        }

        /** Validate actual quantity **/
        public bool vailidateActulaQty(int requiredQty, int actualQty)
        {
            bool result = true;
            if (actualQty > requiredQty)
                result = false;
            return result;

        }

        /** validate sum of actual quantity against available qty **/
        public bool validateSumActualQty(int sumofActualQty, String itemCode)
        {
            bool result = true;
            var avbQty = (from a in context.StationeryRetrievalDetails
                          where a.ItemCode.Equals(itemCode)
                          select a.AvailableQty).FirstOrDefault(); ;
            int availableQty = Convert.ToInt32(avbQty);

            if (sumofActualQty > availableQty)
                result = false;
            return result;

        }

        /** Change Stationery request status to "Request accepted"**/
        public void changeRequestStatus(List<RequestDetail> rdList)
        {
            foreach (RequestDetail rd in rdList)
            {
                if (rd.Request.RequestStatus != "Request Accepted")
                {
                    rd.Request.RequestStatus = "Request Accepted";
                    context.SaveChanges();
                }

            }

        }

        /** update Stock on hand **/

        public void updateStock(String itemCode, int requestedQty)  // need to check it 
        {
            var q = from s in context.Stocks
                    where s.ItemCode.Equals(itemCode)
                    select s;
            Stock item = (Stock)q.FirstOrDefault();
            if (requestedQty <= item.AvailableQty)
            {
                item.AvailableQty = item.AvailableQty - requestedQty;
            }
            else if (requestedQty > item.AvailableQty)
            {
                item.AvailableQty = 0;
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                errorState = true;
                errorMsg = "Stock could not be updated";
                throw new UpdateFailedException(errorMsg);
            }
        }

    }
}
