using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class MgrHomeUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StoreHomeController shc = new StoreHomeController();
            lblRecentRequests.Text = shc.getRequest().ToString();
            lblPendingDiscrepencies.Text = shc.pendingDiscrepancy("StoreMgr").ToString();
        }
    }
}