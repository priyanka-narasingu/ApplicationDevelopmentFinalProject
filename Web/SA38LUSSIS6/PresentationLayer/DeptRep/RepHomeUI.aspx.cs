using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Web.Security;

namespace PresentationLayer
{
    public partial class DeptRepHomeUI : System.Web.UI.Page
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

            String deptName = dhc.getDeptCode(empID);

            lblAppEmp.Text = dhc.getRequestNoByDeptPast30days(pendingApproval, deptName).ToString();


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptRep/RepStationeryRequistion.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptRep/DeptRepViewRequistionHistory.aspx");
        }
    }
}