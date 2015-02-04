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
    public partial class SupViewStockAdjustmentUI : System.Web.UI.Page
    {
        ApproveStockAdjustmentController ap = new ApproveStockAdjustmentController();
        MembershipUser user;
        string role = "StoreMgr";
        string _status;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Membership.GetUser();
            if (!string.IsNullOrEmpty(Session["status"] as string))
            {
                _status = Session["status"].ToString();
                populateGrid(_status);
                Session["status"] = null;

            }
            else
            {
                populateGrid("Pending Approval");
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)lb.Parent.Parent;
            int currentRow = gvr.RowIndex;
            string a = GridView1.Rows[currentRow].Cells[0].Text;
            Response.Redirect("~/StoreSup/SupApproveStockAdjustmentUI.aspx?id=" + a);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _status = ddlStatus.SelectedItem.Text.Trim().ToUpper();
            populateGrid(_status);
        }

        private void populateGrid(string status)
        {
            if (Roles.IsUserInRole(user.UserName, "StoreSup"))
            {
                role = "StoreSup";
            }

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5]{new DataColumn("DiscrepancyID",typeof(string)),new DataColumn("DateRaised",typeof(string)),new DataColumn("RaisedBy",typeof(string)),
            new DataColumn("TotalAmount",typeof(double)),new DataColumn("DiscrepancyStatus",typeof(string))});
            string st;
            string date = txtCalendar.Text.ToString();
            List<Discrepancy> lDis = new List<Discrepancy>();
            if (date != "")
            {
                DateTime searchdate = Convert.ToDateTime(date);
                lDis = ap.getDiscrepancyByStatus(status, searchdate, role);
            }
            else
            {
                lDis = ap.getDiscrepancyByStatus(status, role);
            }
            try
            {
                if (lDis.Count < 1)
                {
                    lblnoData.Text = "No Record for " + status + "!";
                }
                else
                {
                    lblnoData.Text = "";
                    for (int i = 0; i < lDis.Count(); i++)
                    {


                        double amount = (double)lDis[i].TotalAmount;
                        st = Convert.ToDateTime(lDis[i].DateRaised).ToString("dd/MM/yyyy");
                        dt.Rows.Add(lDis[i].DiscrepancyID, st, lDis[i].Employee1.EmployeeName, Math.Round(amount, 2), lDis[i].DiscrepancyStatus);

                    }
                }

            }
            catch (Exception ex)
            {

            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}