using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class StoreHomeController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();
        public int getOutstandingRequests()
        {
            DateTime d = DateTime.Today.AddDays(-30);
            var y = from x in context.Requests where x.DateCreated>=d && x.RequestStatus=="Outstanding" select x;
            int count = y.Count<Request>();
            return count;
        }

        public int getRequest()
        {
            DateTime d = DateTime.Today.AddDays(-30);
            var y = from x in context.Requests where x.DateCreated >= d select x;
            int count = y.Count<Request>();
            return count;
        }

        public int getLowLevelStock()
        {
            var y = from x in context.Stocks where x.AvailableQty<x.ReorderLevel select x;
            int count = y.Count<Stock>();
            return count;
        }
        public int getApprovedRequests()
        {
            DateTime d = DateTime.Today.AddDays(-30);
            var y = from x in context.Requests where x.DateCreated >= d && x.RequestStatus=="Approved" select x;
            int count = y.Count<Request>();
            return count;
        }

        public int pendingDiscrepancy(string role) {
            if (role == "StoreSup")
            {
                var y = from x in context.Discrepancies where x.DiscrepancyStatus == "Pending Approval" && x.TotalAmount < 250 select x;
                int count = y.Count<Discrepancy>();
                return count;
            }
            else if (role=="")
            {
                var y = from x in context.Discrepancies where x.DiscrepancyStatus == "Pending Approval" select x;
                int count = y.Count<Discrepancy>();
                return count;
            }
            else
            {
                var y = from x in context.Discrepancies where x.DiscrepancyStatus == "Pending Approval" && x.TotalAmount >= 250 select x;
                int count = y.Count<Discrepancy>();
                return count;
            }
        }
    }
}
