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
    public partial class DeptHeadHomeUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string approved = "Approved";
            string pendingApproval = "Pending Approval";


            DeptHomeController dhc = new DeptHomeController();
            MembershipUser user = Membership.GetUser();
            string userName = user.UserName;
            string deptCode = dhc.getUserDept(userName);
            
            
            lblPendingRequests.Text = dhc.getRequestNo(pendingApproval, deptCode).ToString();

            string delegatedEmployeeName = dhc.getDelegatedEmployeeForDept(deptCode);

            lblCurrentDelegation.Text = delegatedEmployeeName;

            string collectionPtName = dhc.getCollectionPointName(deptCode);

            lblCollectionPt.Text = collectionPtName;

            lblApproved.Text = dhc.getRequestNoPast30days(approved, deptCode).ToString();



        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptHead/DeptHeadDelegateAuthorityUI.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptHead/DeptHeadViewRequisitionHistoryUI.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptHead/DeptHeadViewRequisitionHistoryUI.aspx?status=Approved");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptHead/DeptHeadMaintainCollectionPointUI.aspx");
        }
    }
}