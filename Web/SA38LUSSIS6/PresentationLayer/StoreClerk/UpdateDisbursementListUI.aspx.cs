using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using BusinessLogicLayer.Exception_Package;
namespace PresentationLayer
{
    public partial class UpdateDisbursementListUI : System.Web.UI.Page
    {
        UpdateDisbursementListController udControl = new UpdateDisbursementListController();
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!(IsPostBack))
            {
                ddlDepartmentName.DataSource = udControl.getDepartmentNames();
                ddlDepartmentName.DataBind();
                ddlDisbursementStatus.DataSource = udControl.getDisbursementStatus();
                ddlDisbursementStatus.DataBind();
                //btnCancel.Visible = false;
              
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            ddlDisbursementStatus.Enabled = true;
            txtCollectionPin.Enabled = true;
            btnPopupUpdate.Enabled = true;
            String departmentName = ddlDepartmentName.SelectedItem.Text;
            StationeryDisbursementGrid.DataSource = udControl.getReadyForDisbursementLists(departmentName);
            StationeryDisbursementGrid.DataBind();
            if (StationeryDisbursementGrid.Rows.Count == 0)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Sorry, no disbursement lists found for the chosen department";
            }
            else
            {
                //btnCancel.Visible = true;
                
            }
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            mpe1.Hide();
        }
     

        protected void btnPopupUpdate_Click(object sender, EventArgs e)
        {
            String itemCode;
            int disbursementID;
            int requestedQty;
            int actualQty;
            String departmentName;
            int collectionPin;
            String disbursementStatus;
            int Qty;
            int requestID;
            Dictionary<String, int> outstanding = new Dictionary<String, int>();
            lblPopupStatus.Text = "";

            departmentName = lblDBDepartmentName.Text;
            disbursementID = Convert.ToInt32(lblDBDisbursementID.Text);
            disbursementStatus = ddlDisbursementStatus.SelectedItem.Text;

            if ((ddlDisbursementStatus.SelectedItem.Text != "Ready for Disbursement") && (!String.IsNullOrEmpty(txtCollectionPin.Text)))
            {
                //update disbursement status
                collectionPin = Convert.ToInt32(txtCollectionPin.Text);
                if (udControl.validateCollectionPin(departmentName, collectionPin))
                {
                    udControl.updateDisbursementStatus(disbursementID, disbursementStatus);
                    //update disbursement details
                    foreach (GridViewRow row in DisbursementDetailsGrid.Rows)
                    {
                        itemCode = row.Cells[0].Text;
                        requestedQty = Convert.ToInt32(row.Cells[2].Text);
                        TextBox txt = (TextBox)row.FindControl("txtActualQty");
                        actualQty = Convert.ToInt32(txt.Text);
                        try
                        {
                            udControl.updateDisbursementDetails(disbursementID, itemCode, requestedQty, actualQty);
                            lblPopupStatus.ForeColor = System.Drawing.Color.Green;
                            lblPopupStatus.Text = "Disbursement List succesfully updated";
                        }
                        catch (UpdateFailedException ex)
                        {
                            lblPopupStatus.Text = ex.Message;
                            break;
                        }
                        if (disbursementStatus.Equals("Partially Delivered"))
                        {
                            if (actualQty < requestedQty)
                            {
                                Qty = requestedQty - actualQty;
                                outstanding.Add(itemCode, Qty);
                            }
                        }
                        else if (disbursementStatus.Equals("Cancel"))
                        {
                            outstanding.Add(itemCode, requestedQty);
                        }

                    }
                    if (outstanding.Count != 0)
                    {
                        /** Create new request **/
                        udControl.createRequest(departmentName);
                        /**get latest request ID **/
                        requestID = udControl.getRequestID();
                        /** Create request details **/
                        foreach (KeyValuePair<string, int> item in outstanding)
                        {
                            udControl.createRequestDetails(requestID, item.Key, item.Value);
                        }

                    }
                }
                else
                {
                    lblPopupStatus.ForeColor = System.Drawing.Color.Red;
                    lblPopupStatus.Text = "Please enter valid colletion pin";
                }

            }
            else if ((ddlDisbursementStatus.SelectedItem.Text == "Ready for Disbursement") || (ddlDisbursementStatus.SelectedItem.Text == "Request Accepted"))
            {
                //update dibusement details
                foreach (GridViewRow row in DisbursementDetailsGrid.Rows)
                {
                    itemCode = row.Cells[0].Text;
                    requestedQty = Convert.ToInt32(row.Cells[2].Text);
                    TextBox txt = (TextBox)row.FindControl("txtActualQty");
                    actualQty = Convert.ToInt32(txt.Text);
                    try
                    {
                        udControl.updateDisbursementDetails(disbursementID, itemCode, requestedQty, actualQty);
                        lblPopupStatus.ForeColor = System.Drawing.Color.Green;
                        lblPopupStatus.Text = "Disbursement List succesfully updated";
                    }
                    catch (UpdateFailedException ex)
                    {
                        lblPopupStatus.ForeColor = System.Drawing.Color.Red;
                        lblPopupStatus.Text = ex.Message;
                        break;
                    }
                }
            }
            else
            {
                lblPopupStatus.ForeColor = System.Drawing.Color.Red;
                lblPopupStatus.Text = "Please enter collection pin";
            }
        }

        protected void StationeryDisbursementGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPopupStatus.Text = "";
            ddlDisbursementStatus.Enabled = true;
            txtCollectionPin.Enabled = true;
            btnPopupUpdate.Enabled = true;
            if (StationeryDisbursementGrid.SelectedRow != null)
            {
                String disbursementStatus = StationeryDisbursementGrid.SelectedRow.Cells[4].Text;
                if (!disbursementStatus.Equals("Request Accepted") && (!disbursementStatus.Equals("Ready for Disbursement")))
                {
                    ddlDisbursementStatus.Enabled = false;
                    txtCollectionPin.Enabled = false;
                    btnPopupUpdate.Enabled = false;
                }

                lblDBDisbursementID.Text = StationeryDisbursementGrid.SelectedRow.Cells[0].Text;
                lblDBCollectionPoint.Text = StationeryDisbursementGrid.SelectedRow.Cells[2].Text;
                lblDBRepresentative.Text = StationeryDisbursementGrid.SelectedRow.Cells[3].Text;
                lblDBDepartmentName.Text = ddlDepartmentName.SelectedItem.Text;
                ddlDisbursementStatus.Text = disbursementStatus;
                DisbursementDetailsGrid.DataSource = udControl.getDisbursementDetailList(Convert.ToInt32(lblDBDisbursementID.Text));
                DisbursementDetailsGrid.DataBind();
                mpe1.Show();
            }
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/StoreClerk/ClerkHomeUI.aspx");
        //}

    }
}
