using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Data;
using System.Web.Security;
namespace PresentationLayer
{
    public partial class ApproveStockAdjustmentUI : System.Web.UI.Page
    {
        String discrepancyID;
        ApproveStockAdjustmentController _approveadj = new ApproveStockAdjustmentController();
        string comment;
        string userid;
        MembershipUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Membership.GetUser();
            userid = _approveadj.getEmployeeID(user.UserName.ToString());
            discrepancyID=Request.QueryString["id"];
            Discrepancy _discrepancy = new Discrepancy();
            _discrepancy = _approveadj.getDiscrepancyByID(int.Parse(discrepancyID));
            lblDBDiscrepancyID.Text = _discrepancy.DiscrepancyID.ToString();
            lblDBRaisedBy.Text = _discrepancy.Employee1.EmployeeName;
            lblDBDateRaised.Text = Convert.ToDateTime(_discrepancy.DateRaised).ToString("dd/MM/yyyy");
            this.populateGrid();
            if (_discrepancy.DiscrepancyStatus != "Pending Approval")
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
                txtReasonRejection.Visible = false;
                lblReasonRejection.Visible = false;
            }
        }
        void populateGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]{new DataColumn("ItemCode",typeof(string)),new DataColumn("ItemCategory",typeof(string)),new DataColumn("ItemDescription",typeof(string)),new DataColumn("Quantity",typeof(int))
                ,new DataColumn("IsAdded",typeof(bool)),new DataColumn("Amount",typeof(double)),new DataColumn("Reason",typeof(string))});
            string st;
            List<DiscrepancyDetail> lDis = _approveadj.getDiscrepancyDetailByID(int.Parse(discrepancyID));
            try
            {
                for (int i = 0; i < lDis.Count(); i++)
                {
                    double amount = (double)lDis[i].Amount;
                    dt.Rows.Add(lDis[i].ItemCode, lDis[i].Stock.ItemCategory, lDis[i].Stock.ItemDescription, lDis[i].Quantity,lDis[i].IsAdded,Math.Round(amount,2),lDis[i].Reason);
                }
            }
            catch (Exception ex)
            {

            }

            DiscrepancyDetailsGrid.DataSource = dt;
            DiscrepancyDetailsGrid.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            bool status = _approveadj.updateDiscrepancyStatus(int.Parse(discrepancyID), "Approved", userid, "");
            if (status)
                status = _approveadj.updateStock(int.Parse(discrepancyID));

            if (status)
            {
                Session["status"] = "Pending Approval";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                    "alert('The Stock Adjustment has been approved!'); window.location='" +
                    Request.ApplicationPath + "/StoreSup/SupViewStockAdjustmentUI.aspx';", true);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            comment = txtReasonRejection.Text.ToString();
            bool status = _approveadj.updateDiscrepancyStatus(int.Parse(discrepancyID), "Rejected", userid, comment);
            if (status)
            {
                Session["status"] = "Pending Approval";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                    "alert('The Stock Adjustment has been rejected!'); window.location='" +
                    Request.ApplicationPath + "/StoreSup/SupViewStockAdjustmentUI.aspx';", true);
            }
        }
    }
}