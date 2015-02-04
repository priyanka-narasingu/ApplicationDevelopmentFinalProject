/* Development of View Requisition History*/
/* Developer: Vignesh Sridharan*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Globalization;
using System.Data;
using System.Web.Security;


namespace PresentationLayer.DeptEmp
{
    public partial class EmpViewRequisitionHistoryUI : System.Web.UI.Page
    {
        ViewRequisitionHistoryController vr = new ViewRequisitionHistoryController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MembershipUser user = Membership.GetUser();
                String userName = user.UserName;
                List<Request> lReq = vr.getEmpReqHistoryAllByDept(userName);
                dgvEmpReqHistory.DataSource = lReq;
                dgvEmpReqHistory.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser();
            String userName = user.UserName;

            List<Request> lReqSrch = new List<Request>();
            String sStat = ddlStatus.SelectedValue;
            String dFromSt = (dtpFrom.Text) + " 12:00:00 AM";
            String dToSt = (dtpTo.Text) + " 11:59:59 PM";

            if (sStat == "All")
            {
                if (dtpFrom.Text == "" || dtpTo.Text == "")
                {
                    lReqSrch = vr.getEmpReqHistoryAllByDept(userName);
                }
                else
                {
                    DateTime dFrom = DateTime.ParseExact(dFromSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime dTo = DateTime.ParseExact(dToSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    lReqSrch = vr.getEmpReqHistoryAllByTimePeriodByDept(dFrom, dTo, userName);
                }
            }
            else
            {
                if (dtpFrom.Text == "" || dtpTo.Text == "")
                {
                    lReqSrch = vr.getEmpReqHistoryByStatusByDept(userName, sStat);
                }
                else
                {
                    DateTime dFrom = DateTime.ParseExact(dFromSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime dTo = DateTime.ParseExact(dToSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    lReqSrch = vr.getEmpReqHistoryByStatusTimePeriodByDept(sStat, dFrom, dTo, userName);
                }
            }

            DataTable dtbl = new DataTable();


            dgvEmpReqHistory.DataSource = lReqSrch;
            dgvEmpReqHistory.DataBind();
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
                mpeViewDetails.Hide();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow grv = (GridViewRow)btn.NamingContainer;
            int rID = Convert.ToInt32(grv.Cells[1].Text);
            ViewState["reqqID"] = rID;

            lblrIDCt.Text = grv.Cells[1].Text;
            lblrDateCt.Text = grv.Cells[0].Text;
            lblrStatusCt.Text = grv.Cells[3].Text;
            lblrCmtCt.Text = grv.Cells[2].Text;

            DataTable dt = vr.getRequestDetails(rID);
            dgvRqDetails.DataSource = dt;
            dgvRqDetails.DataBind();

            this.mpeViewDetails.Show();

        }
    }
}