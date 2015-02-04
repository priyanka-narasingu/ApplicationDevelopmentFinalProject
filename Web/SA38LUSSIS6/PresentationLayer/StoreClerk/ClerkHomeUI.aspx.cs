using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;


namespace PresentationLayer
{
    public partial class ClerkHomeUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StoreHomeController _storehomecontroller = new StoreHomeController();
            lblRecenRequests.Text = _storehomecontroller.getOutstandingRequests().ToString();
            lblLowStockAlert.Text = _storehomecontroller.getLowLevelStock().ToString();
            lblApprovedRequests.Text = _storehomecontroller.getApprovedRequests().ToString();
            lblPendingDiscrepacies.Text = _storehomecontroller.pendingDiscrepancy("").ToString();                                  
        }

        
    }
}