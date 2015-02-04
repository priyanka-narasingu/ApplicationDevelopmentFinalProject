/*Modified by Vignesh Sridharan*/

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
    public partial class DeptViewRequisitionHistoryUI : System.Web.UI.Page
    {
        ApproveRequestController ap = new ApproveRequestController();
        DeptHeadViewRequisitionHistoryController dp = new DeptHeadViewRequisitionHistoryController();
        MembershipUser user;
        List<Request> req = new List<Request>();
        List<Request> r = new List<Request>();
        int rownumber;

        protected void Page_Load(object sender, EventArgs e)
        {
            String homeStatus = Request.QueryString["status"];

            user = Membership.GetUser();
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Session["backstatus"] as string))
                {

                    String _status = Session["backstatus"].ToString();
                    req = dp.getRequest(_status);
                    r = dp.getRequestByUser(user.UserName.ToString(), _status);
                    populateGridView();

                }
                else if (homeStatus != null)
                {
                    ddlstatus.SelectedValue = "Approved";
                    r = dp.getRequestByUser(user.UserName.ToString(), "Approved");
                    populateGridView();
                }
                else
                {
                    ddlstatus.SelectedValue = "Pending Approval";
                    r = dp.getRequestByUser(user.UserName.ToString(), "Pending Approval");
                    populateGridView();

                }
            }


        }
        protected void generatebtn_Click(object sender, EventArgs e)
        {

            String status = ddlstatus.SelectedItem.Text;


            if (txtCalendar.Text != "")
            {
                DateTime startdate = Convert.ToDateTime(txtCalendar.Text); ;
                lbldatte.Text = startdate.ToShortDateString();
                DateTime testdate = dp.getDate();
                lbltestdate.Text = testdate.ToShortDateString();
                r = dp.getRequestByDate(user.UserName.ToString(), status, startdate);
                populateGridView();
            }
            else
            {
                req = dp.getRequest(status);
                r = dp.getRequestByUser(user.UserName.ToString(), status);
                populateGridView();

            }

        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)lb.Parent.Parent;
            int currentRow = gvr.RowIndex;
            string a = dgvRequest.Rows[currentRow].Cells[1].Text;
            String status = dgvRequest.Rows[currentRow].Cells[5].Text;
            string name = dgvRequest.Rows[currentRow].Cells[2].Text;
            String rDate = dgvRequest.Rows[currentRow].Cells[3].Text;
            Response.Redirect("~/DeptHead/DeptHeadApproveRequestUI.aspx?id=" + a + "&Status=" + status + "&name=" + name + "&date=" + rDate);
        }
        public void populateGridView()
        {
            DataTable dt = new DataTable();
            rownumber = dp.getrequestrownumber();



            Employee emp = new Employee();
            dt.Columns.AddRange(new DataColumn[6]{new DataColumn("S.NO",typeof(int)),new DataColumn("RequistionID",typeof(string)),new DataColumn("EmployeeName",typeof(string)),
            new DataColumn("DateRequested",typeof(String)),new DataColumn("Comment",typeof(string)),new DataColumn("Status",typeof(string))});

            string st;

            try
            {
                for (int i = 0; i < rownumber; i++)
                {



                    st = Convert.ToDateTime(r[i].DateCreated).ToString("dd/MM/yyyy");

                    emp = ap.getEmployee(r[i].EmployeeID.ToString());
                    dt.Rows.Add(i + 1, r[i].RequestID, emp.EmployeeName, st, r[i].Comments, r[i].RequestStatus);

                }
            }
            catch (Exception ex)
            {

            }

            lblnotfound.Text = "";
            dgvRequest.DataSource = dt;
            dgvRequest.DataBind();
        }


    }
}