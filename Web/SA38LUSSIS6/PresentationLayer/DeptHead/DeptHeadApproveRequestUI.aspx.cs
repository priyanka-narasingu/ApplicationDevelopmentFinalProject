using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using BusinessLogicLayer;
using System.Net.Mail;
using System.Data.Entity;


namespace PresentationLayer
{
    public partial class DeptApproveRequestUI : System.Web.UI.Page
    {
        ApproveRequestController ap = new ApproveRequestController();
        MembershipUser user;
        int requestid;
        string status;
        string name;
        String reqDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Membership.GetUser();

            mypanel.Visible = false;
            requestid = Convert.ToInt32(Request.QueryString["id"]);
            status = Request.QueryString["Status"];
            name = Request.QueryString["name"];
            reqDate = Request.QueryString["date"];




            if (status == "Pending Approval")
            {
                mypanel.Visible = true;
            }


            //if (!string.IsNullOrEmpty(Session["date"].ToString()) )
            //{
            //    Session.Add("backdate", Session["date"].ToString());
            //}
            //if (Session["status"].ToString() == "Pending Approval")
            //{

            //    mypanel.Visible = true; }
            //else
            //{

            //    mypanel.Visible = false; }
            //int requestid = Convert.ToInt32(Session["requestid"].ToString());
            //lblemployee.Text = "Employee name " + Session["name"].ToString();
            if (!IsPostBack)
            {

                lblReqID.Text = requestid.ToString();
                lblReqDate.Text = reqDate;
                lblemployee.Text = name;
                lblReqStat.Text = status;

                List<RequestDetail> rd = ap.getReuestDetail(requestid);


                int requestdetailrow = ap.getRequestRow();
                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("S.No", typeof(int)), new DataColumn("CategoryItem", typeof(string)), new DataColumn("Description", typeof(string)), new DataColumn("Quantity", typeof(int)) });
                for (int i = 0; i < requestdetailrow; i++)
                {

                    Stock s = ap.getDescription(rd[i].ItemCode.ToString());

                    dt.Rows.Add(i + 1, s.ItemCategory, s.ItemDescription, rd[i].Quantity);
                }
                //lblemployee.Text = "Employee name: " + Request.QueryString["name"].ToString();
                dgvReqDetails.DataSource = dt;
                dgvReqDetails.DataBind();
            }


        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            //int requestid = Convert.ToInt32(Session["requestid"].ToString());
            ap.updateRequest(requestid, user.UserName.ToString());

            string email = ap.getEmail(name);
            MailMessage m = new MailMessage("a0122185@nus.edu.sg", email);
            m.Subject = "Approved Request";
            m.Body = "Request ID " + requestid + " has approved";
            SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
            c.Send(m);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('The request had been approved'); window.location='" + Request.ApplicationPath + "/DeptHead/DeptHeadViewRequisitionHistoryUI.aspx';", true);

        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            // int requestid = Convert.ToInt32(Session["requestid"].ToString());
            ap.rejectRequest(requestid, txtComment.Text);

            string email = ap.getEmail(name);
            MailMessage m = new MailMessage("a0122185@nus.edu.sg", email);
            m.Subject = "Your request has rejected";
            m.Body = "Request ID " + requestid + " has rejected";
            SmtpClient c = new SmtpClient("lynx.iss.nus.edu.sg");
            c.Send(m);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('The request had been rejected'); window.location='" + Request.ApplicationPath + "/DeptHead/DeptHeadViewRequisitionHistoryUI.aspx';", true);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptHead/DeptHeadViewRequisitionHistoryUI.aspx");
        }

        protected void btnback_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptHead/DeptHeadViewRequisitionHistoryUI.aspx");
            Session.Add("backstatus", status);
        }

    }
}