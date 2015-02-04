using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class ApproveStockAdjustmentController
    {
        SA38ADTeam6Entities ent = new SA38ADTeam6Entities();


        public List<Discrepancy> getDiscrepancyByStatus(string status,string role)
        {
            List<Discrepancy> req = new List<Discrepancy>();
            if (role == "StoreSup")
            {
                var x = from y in ent.Discrepancies
                        where y.DiscrepancyStatus.Trim().ToUpper() == status && y.TotalAmount < 250
                        select y;
                return req = x.ToList<Discrepancy>();
            }
            else
            {
                var x = from y in ent.Discrepancies
                        where y.DiscrepancyStatus.Trim().ToUpper() == status && y.TotalAmount >= 250
                        select y;
                return req = x.ToList<Discrepancy>();
            }
        }

        public List<Discrepancy> getDiscrepancyByStatus(string status,DateTime date,string role)
        {
            DateTime nextdate = date.AddDays(1);
            List<Discrepancy> req = new List<Discrepancy>();
            if (role == "StoreSup")
            {
                var x = from y in ent.Discrepancies
                        where y.DiscrepancyStatus.Trim().ToUpper() == status && y.DateRaised >= date && y.DateRaised < nextdate && y.TotalAmount<250
                        select y;
                return req = x.ToList<Discrepancy>();
            }
            else
            {
                var x = from y in ent.Discrepancies
                        where y.DiscrepancyStatus.Trim().ToUpper() == status && y.DateRaised >= date && y.DateRaised < nextdate && y.TotalAmount>=250
                        select y;
                return req = x.ToList<Discrepancy>();
            }
            
        }
        public string getEmployeeID(string employeeName)
        {
            var x = from y in ent.Employees where y.UserName == employeeName select y;
            Employee e = x.First<Employee>();
            return e.EmployeeID;
        }
        public Discrepancy getDiscrepancyByID(int disID)
        {
            var x = from y in ent.Discrepancies
                    where y.DiscrepancyID == disID
                    select y;
            Discrepancy d = new Discrepancy();
            d = x.First<Discrepancy>();
            return d;
        }

        public List<DiscrepancyDetail> getDiscrepancyDetailByID(int disID)
        {
            var x = from y in ent.DiscrepancyDetails
                    where y.DiscrepancyID == disID
                    select y;
            List<DiscrepancyDetail> disdetail = new List<DiscrepancyDetail>();
            return disdetail = x.ToList<DiscrepancyDetail>();
        }

        public bool updateDiscrepancyStatus(int disID, string status, string approveid,string comment)
        {
            var x = from y in ent.Discrepancies
                    where y.DiscrepancyID == disID
                    select y;
            Discrepancy d = x.First<Discrepancy>();
            d.DiscrepancyStatus = status;
            d.ApprovedBy = approveid;
            d.Comment = comment;
            d.DateUpdated = DateTime.Today.Date;
            try
            {
                ent.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool updateStock(int disID)
        {
            List<DiscrepancyDetail> ldd = this.getDiscrepancyDetailByID(disID);
            for (int i = 0; i < ldd.Count(); i++)
            {
                DiscrepancyDetail dd = ldd[i];
                string _itemcode = dd.ItemCode;
                int _quantity = (int)dd.Quantity;
                bool _isadded = (bool)dd.IsAdded;

                var x = from y in ent.Stocks
                        where y.ItemCode == _itemcode
                        select y;
                Stock s = x.First<Stock>();
                if (_isadded)
                {
                    s.AvailableQty = s.AvailableQty + _quantity;
                }
                else
                {
                    s.AvailableQty = s.AvailableQty - _quantity;
                }

                try
                {
                    ent.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
