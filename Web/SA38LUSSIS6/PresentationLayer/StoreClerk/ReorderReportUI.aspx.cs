/* Developer: Vignesh Sridharan*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.StoreClerk
{
    public partial class ReorderReportUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dsReorderRep ds = new dsReorderRep();
            dsReorderRepTableAdapters.v_ReorderReportTableAdapter ta = new dsReorderRepTableAdapters.v_ReorderReportTableAdapter();

            crReorderReport rp = new crReorderReport();

            ta.Fill(ds.v_ReorderReport);
            rp.SetDataSource(ds);

            crvReorderReport.ReportSource = rp;
        }
    }
}