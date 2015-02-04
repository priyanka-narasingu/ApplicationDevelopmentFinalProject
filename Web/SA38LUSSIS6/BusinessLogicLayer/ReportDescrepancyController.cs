using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace BusinessLogicLayer
{
    public class ReportDescrepancyController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();
        
        public List<Stock> getAllStock()
        {
            
            var q = from x in context.Stocks
                    select x;
            Stock s = new Stock();
            List<Stock> mlist = q.ToList<Stock>();
            return mlist;

        }
        public Stock getStockByID(string id)
        {      

            //string z = (memberIDbutton.Text.ToString());
            var q = from x in context.Stocks
                    where x.ItemCode == id
                    select x;
            Stock m = new Stock();
            m = q.First<Stock>();
            return m;
        }

        public bool insertDiscrepancy(Discrepancy d)
        {
            
            Discrepancy di = d;
            context.AddToDiscrepancies(di);
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool insertDiscrepancyDetail(DiscrepancyDetail dd)
        {
            
            DiscrepancyDetail ddt = dd;
            context.AddToDiscrepancyDetails(ddt);
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int getDiscrepancyID()
        {
           
            var q = from x in context.Discrepancies
                    orderby x.DiscrepancyID descending
                    select x;
            Discrepancy d = new Discrepancy();
            d = q.First<Discrepancy>();
            int dID = d.DiscrepancyID;
            return dID;

        }
  
        public String getEmployeeID(String userName)
        {
            var q = from e in context.Employees
                    where e.UserName.Equals(userName)
                    select e.EmployeeID;
            String uname = q.FirstOrDefault().ToString();
            return uname;
        }

       
    }
}
