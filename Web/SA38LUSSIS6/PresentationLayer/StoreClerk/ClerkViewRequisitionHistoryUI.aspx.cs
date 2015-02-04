using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Globalization;

namespace PresentationLayer
{
    public partial class ClerkViewRequisitionHistoryUI : System.Web.UI.Page
    {
        ViewRequisitionHistoryController vrControl = new ViewRequisitionHistoryController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblStatus.Text = "";
                ddlStatus.DataSource = vrControl.getReqStatusForStore();
                ddlStatus.DataBind();
               
                requestDetailGrid.DataSource = vrControl.getAllReqForClerk();
                requestDetailGrid.DataBind();
                if (requestDetailGrid.Rows.Count == 0)
                {
                    lblStatus.Text = "No current requests";
                    //btnCancel.Visible = false;
                }
            }
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            mpe1.Hide();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            //btnCancel.Visible = true;
            String status = ddlStatus.SelectedItem.Text;
            String dFrom = txtFromdate.Text;
            String dTo = txtToDate.Text;
            if (status.Equals("All"))
            {
                requestDetailGrid.DataSource = vrControl.getAllReqForClerk();

            }

            else if ((String.IsNullOrEmpty(dFrom)) && (String.IsNullOrEmpty(dTo)))
            {
                requestDetailGrid.DataSource = vrControl.getReqByStatus(status);
            }
            else if ((!String.IsNullOrEmpty(dFrom)) && (!String.IsNullOrEmpty(dTo)))
            {
                dFrom = dFrom + " 12:00:00 AM";
                dTo = dTo + " 11:59:59 PM";
                try
                {
                    DateTime dFromst = DateTime.ParseExact(dFrom, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime dTost = DateTime.ParseExact(dTo, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    requestDetailGrid.DataSource = vrControl.getReqByStatusTimePeriod(status, dFromst, dTost);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Invalid date format";
                }
            }
            else if ((String.IsNullOrEmpty(dFrom)) || (String.IsNullOrEmpty(dTo)))
            {
                if (!String.IsNullOrEmpty(dFrom))
                 {
                    dFrom = dFrom + " 12:00:00 AM";
                    try
                    {
                        DateTime dFromSt = DateTime.ParseExact(dFrom, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        requestDetailGrid.DataSource = vrControl.getReqByFromDateStatus(dFromSt,status);
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Invalid date format";
                    }

                }

            }
            requestDetailGrid.DataBind();
            if (requestDetailGrid.Rows.Count == 0) 
            {
                lblStatus.Text = "No requests found";
                //btnCancel.Visible = false;
            }

        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/StoreClerk/ClerkHomeUI.aspx");
        //}

        protected void requestDetailGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int requestID = Convert.ToInt32(requestDetailGrid.SelectedRow.Cells[0].Text);
            Request req = vrControl.getRequest(requestID);
            lblDBRequestDate.Text = requestDetailGrid.SelectedRow.Cells[2].Text;
            lblDBRequestID.Text = req.RequestID.ToString();
            lblDBDeptName.Text = req.Employee1.Department.DeptName;
            lblDBComments.Text = req.Comments;
            lblDBRequestStatus.Text = req.RequestStatus.ToString();
            requestPopupGrid.DataSource = vrControl.getRequestDetails(requestID);
            requestPopupGrid.DataBind();
            mpe1.Show();
        }


    }
}