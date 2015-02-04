using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class ReplenishStockController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();
          //Get the values to populate
             public List<Stock> populateReplenishStock(string category)
            {
                var s=(from x in context.Stocks
                       where x.ItemCategory==category
                       select x);
    
                List<Stock> sList=s.ToList<Stock>();
                return sList;

            }

            public List<String> getItemCategory()
            {
                var q = (from x in context.Stocks
                         select x.ItemCategory).Distinct();
                List<String> lcat = (List<String>)q.ToList();
                return lcat;
            }

            public bool updateStock(List<Stock> stock)
            {
                 int i=0;
                foreach (Stock st in stock)
                {
                    var q = (from x in context.Stocks
                             where x.ItemCode == st.ItemCode
                             select x);
                    Stock s = q.First<Stock>();

                    s.MinReorderQty = st.MinReorderQty;
                    s.ReorderLevel = st.ReorderLevel;
                    s.AvailableQty = st.AvailableQty;

                    i= context.SaveChanges();
                }
                return i>0;
            }

    }
}
