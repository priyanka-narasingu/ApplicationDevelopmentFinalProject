using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class SearchStationeryController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();

        public List<Stock> getStock()
        {

            var q = from x in context.Stocks
                    select x;
            List<Stock> sklist = (List<Stock>)q.ToList();
            return sklist;
        }

        public List<Stock> getStockByCategory(String scat)
        {
            var q = from x in context.Stocks
                    where x.ItemCategory == scat
                    select x;
            List<Stock> sklist = (List<Stock>)q.ToList();
            return sklist;
        }

        /** get category list **/
        public List<String> getCategoryList()
        {
            var q = (from c in context.Stocks
                     select c.ItemCategory).Distinct().ToList();
            List<String> catList = q.ToList<String>();
            return catList;
        }

    }
}
