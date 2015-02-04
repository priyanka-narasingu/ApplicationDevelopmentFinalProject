using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using BusinessLogicLayer.Exception_Package;
using System.Globalization;

namespace PresentationLayer
{
    public partial class DisbursemetListUI : System.Web.UI.Page
    {
        GenerateDisbursementListController gdControl = new GenerateDisbursementListController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                disbursementTable.Visible = false;
                FormButtons.Visible = false;
                lblStatus.Text = "";

                StationeryRetrieval stationeryRetrieval = gdControl.getStationeryRetrieval();
                if (stationeryRetrieval != null)
                {
                    lblDBRetrievalID.Text = stationeryRetrieval.RetrievalID.ToString();
                    ddlDeptName.DataSource = gdControl.getListOfDepartments(stationeryRetrieval);
                    ddlDeptName.DataBind();
                    
                    try
                    {
                        if (gdControl.getnerateAllDisbursementLists())
                        {
                            String departmentName = ddlDeptName.SelectedItem.Text;
                            if (departmentName != null)
                                displayDisbursementList(departmentName);
                        }


                    }
                    catch (CreateFailedException ex)
                    {
                        lblStatus.Text = ex.Message;
                    }

                }
                else
                    lblStatus.Text = "No current Stationery Retrieval available! Please generate Stationery Retrieval Form";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            if (!(ddlDeptName.Items.Count == 0))
            {
                String departmentName = ddlDeptName.SelectedItem.Text;
                displayDisbursementList(departmentName);
            }
            else
            {
                lblStatus.Text = "No search results found";
                makeControlsInvisible();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int actualQty;
            int requiredQty;
            int sumOfActualQty = 0;
            String itemCode;

            bool suceess = true;
            lblStatus.Text = "";
            String disbursementDate = txtDate.Text;
            String deptRep = lblDBRepName.Text;
            String departmentName = ddlDeptName.SelectedItem.Text;
            int disbursementID = Convert.ToInt32(lblDBDisbursementID.Text);

            foreach (GridViewRow row in DisbursementDetailGrid.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("txtActualQty");
                actualQty = Convert.ToInt32(txt.Text);
                sumOfActualQty = sumOfActualQty + actualQty;
            }

            /** Validate if sum of actual quantity is not greater than the available qty **/
            try
            {
                foreach (GridViewRow row in DisbursementDetailGrid.Rows)
                {
                    lblStatus.Text = "";
                    itemCode = row.Cells[0].Text;
                    requiredQty = Convert.ToInt32(row.Cells[2].Text);
                    TextBox txt = (TextBox)row.FindControl("txtActualQty");
                    actualQty = Convert.ToInt32(txt.Text);
                    gdControl.updateDisbursementList(itemCode, actualQty, requiredQty, disbursementID);
                }
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = ex.Message;
                suceess = false;
            }

            if (suceess)
            {
                gdControl.updateDisbursementStatus(disbursementID);
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Disbursement List generated successfully";
                /** send email notification **/
                try
                {
                    gdControl.notifyDisbursement(disbursementDate, deptRep, departmentName);
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", "alert('Email notification sent to the Representative !');", true);
                   
                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = ex.Message;
                }
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Confirm", "alert('Email notification sent to the Representative !');", true);
            Response.Redirect("~/StoreClerk/ClerkHomeUI.aspx");
        }

        /** make controls visisble **/
        public void makeControlsVisible()
        {
            disbursementTable.Visible = true;
            FormButtons.Visible = true;
        }

        /** make controls disabled **/
        public void makeControlsInvisible()
        {
            disbursementTable.Visible = false;
            FormButtons.Visible = false;
        }

        /** display disbursement list **/
        public void displayDisbursementList(String departmentName)
        {
            lblStatus.Text = "";
            disbursementTable.Visible = true;
            FormButtons.Visible = true;
            int retrievalID = Convert.ToInt32(lblDBRetrievalID.Text);
            StationeryDisbursement sd = gdControl.getStationeryDisbursement(retrievalID, departmentName);
            lblDBDisbursementID.Text = sd.DisbursementID.ToString();
            lblDBRepName.Text = gdControl.getDepartmentRepName(sd.DeptRep);
            lblDBCollectionPoint.Text = sd.CollectionPoint1.CollectionPointName;
            lblDBDisbursementStatus.Text = sd.DisbursementStatus;
            DisbursementDetailGrid.DataSource = gdControl.getStaioneryDisbursementDetails(sd.DisbursementID);
            DisbursementDetailGrid.DataBind();
        }
    }
}

