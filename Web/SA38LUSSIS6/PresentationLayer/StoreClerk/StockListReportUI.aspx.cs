/* Developer: Vignesh Sridharan*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace PresentationLayer.StoreClerk
{
    public partial class StockListReportUI : System.Web.UI.Page
    {
                
        protected void Page_Load(object sender, EventArgs e)
        {
            dsStockList ds = new dsStockList();
            dsStockListTableAdapters.v_StockListTableAdapter ta = new dsStockListTableAdapters.v_StockListTableAdapter();

            crStockList rp = new crStockList();

            ta.Fill(ds.v_StockList);
            rp.SetDataSource(ds);

            crvStockList.ReportSource = rp;
        }
    }
}