using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.StoreClerk
{
    public partial class InventoryStatusReportUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dsInvStatus ds = new dsInvStatus();
            dsInvStatusTableAdapters.v_InvStatusReportTableAdapter ta = new dsInvStatusTableAdapters.v_InvStatusReportTableAdapter();

            crInvStatusReport rp = new crInvStatusReport();

            ta.Fill(ds.v_InvStatusReport);
            rp.SetDataSource(ds);

            crvInvStatusReport.ReportSource = rp;
        }
    }
}