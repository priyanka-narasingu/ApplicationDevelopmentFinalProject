/* Development of View Requistion History Page*/
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


namespace PresentationLayer.DeptRep
{
    public partial class DeptRepViewRequistionHistory : System.Web.UI.Page
    {
        ViewRequisitionHistoryController vr = new ViewRequisitionHistoryController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MembershipUser user = Membership.GetUser();
                String userName = user.UserName;

                String deptName = vr.getDeptCode(userName);
                List<Request> lReq = vr.getEmpReqHistoryAllByDeptEmployees(deptName);
                buildReqGrid(lReq);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser();
            String userName = user.UserName;
            String deptName = vr.getDeptCode(userName);
            List<Request> lReqSrch = new List<Request>();

            String sStat = ddlStatus.SelectedValue;
            String dFromSt = (dtpFrom.Text) + " 12:00:00 AM";
            String dToSt = (dtpTo.Text) + " 11:59:59 PM";

            if (sStat == "All")
            {
                if (dtpFrom.Text == "" || dtpTo.Text == "")
                {
                    lReqSrch = vr.getEmpReqHistoryAllByDeptEmployees(deptName);
                    buildReqGrid(lReqSrch);
                }
                else
                {
                    DateTime dFrom = DateTime.ParseExact(dFromSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime dTo = DateTime.ParseExact(dToSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    lReqSrch = vr.getEmpReqHistoryAllByTimePeriodByDeptEmployees(dFrom, dTo, deptName);
                    buildReqGrid(lReqSrch);
                }
            }
            else
            {
                if (dtpFrom.Text == "" || dtpTo.Text == "")
                {
                    lReqSrch = vr.getEmpReqHistoryByStatusByDeptEmployees(deptName, sStat);
                    buildReqGrid(lReqSrch);
                }
                else
                {
                    DateTime dFrom = DateTime.ParseExact(dFromSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime dTo = DateTime.ParseExact(dToSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    lReqSrch = vr.getEmpReqHistoryByStatusTimePeriodByDeptEmployees(sStat, dFrom, dTo, deptName);
                    buildReqGrid(lReqSrch);
                }
            }

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
            lblrStatusCt.Text = grv.Cells[4].Text;
            lblrCmtCt.Text = grv.Cells[2].Text;

            DataTable dt = vr.getRequestDetails(rID);
            dgvRqDetails.DataSource = dt;
            dgvRqDetails.DataBind();

            this.mpeViewDetails.Show();

        }

        public void buildReqGrid(List<Request> l)
        {
            DataTable dt = new DataTable();

            DataColumn dc1 = new DataColumn();
            dc1.DataType = typeof(DateTime);
            dc1.ColumnName = "DateCreated";

            DataColumn dc2 = new DataColumn();
            dc2.DataType = typeof(int);
            dc2.ColumnName = "RequestID";

            DataColumn dc3 = new DataColumn();
            dc3.DataType = typeof(string);
            dc3.ColumnName = "EmployeeName";

            DataColumn dc4 = new DataColumn();
            dc4.DataType = typeof(String);
            dc4.ColumnName = "Comments";

            DataColumn dc5 = new DataColumn();
            dc5.DataType = typeof(String);
            dc5.ColumnName = "RequestStatus";

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);


            foreach (Request rq in l)
            {
                Employee eM = new Employee();

                DataRow dr = dt.NewRow();
                dr["DateCreated"] = rq.DateCreated;
                dr["RequestID"] = rq.RequestID;
                dr["EmployeeName"] = vr.getEmployeeName(rq.EmployeeID);
                dr["Comments"] = rq.Comments;
                dr["RequestStatus"] = rq.RequestStatus;

                dt.Rows.Add(dr);
            }

            dgvEmpReqHistory.DataSource = dt;
            dgvEmpReqHistory.DataBind();

        }
    }
}