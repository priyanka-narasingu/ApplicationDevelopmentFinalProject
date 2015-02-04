using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class EmpHomeUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string approved = "Approved";
            string pendingApproval = "Pending Approval";


            DeptHomeController dhc = new DeptHomeController();
            MembershipUser user = Membership.GetUser();
            string userName = user.UserName;

            string empID = dhc.getEmpID(userName);


            lblPendingRequests.Text = dhc.getRequestNoByEmp(pendingApproval, empID).ToString();

            //lblPendingRequests.Text = empID;

            lblAppEmp.Text = dhc.getRequestNoByEmpPast30days(pendingApproval, empID).ToString();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptEmp/EmpStationeryRequisitionUI.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptEmp/EmpViewRequisitionHistoryUI.aspx");
        }
    }
}