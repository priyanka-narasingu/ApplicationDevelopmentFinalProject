using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
namespace PresentationLayer
{
    public partial class RetrieveStationeryUI : System.Web.UI.Page
    {

        GenerateStationeryRetrievalController srControl = new GenerateStationeryRetrievalController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /** check if there is current stationery retrieval **/

                StationeryRetrieval sr = srControl.getstationeryRetrieval();
                if (sr != null)
                {
                    visbleControls();
                    displayStationeryRetrieval(sr);
                    lblStatus.Text = "There is a current stationery retrieval. Please generate the Disbursement List";
                }
                else
                {

                    try
                    {
                        bool success = srControl.generateStationeryRetrieval();
                        if (success)
                        {
                            StationeryRetrieval srNew = srControl.getstationeryRetrieval();
                            displayStationeryRetrieval(srNew);
                            visbleControls();
                        }
                        else
                        {
                            hideControls();
                            lblStatus.Text = "No approved or outstanding request found !";
                        }
                    }
                    catch (Exception ex)
                    {
                        hideControls();
                        lblStatus.Text = "Failed to generate stationery retrieval !";
                    }

                }
            }
        }


        protected void StationeryRetrievalGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            String itemCode;
            lblStatus.Text = "";

            if (StationeryRetrievalGrid.SelectedRow != null)
            {
                itemCode = StationeryRetrievalGrid.SelectedRow.Cells[0].Text;
                lblDBItemNo.Text = itemCode;
                lblDBDescription.Text = StationeryRetrievalGrid.SelectedRow.Cells[2].Text;
                var q = srControl.getDepartmentWiseList(Convert.ToInt32(lblDBRetrievalID.Text), itemCode);
                DepartmentWiseDetailsGrid.DataSource = q;
                DepartmentWiseDetailsGrid.DataBind();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            String departmentName;
            int actualQty;
            int requiredQty;
            int retrievalID;
            int sumOfActualQty = 0;
            String itemCode;
            lblStatus.Text = "";

            foreach (GridViewRow row in DepartmentWiseDetailsGrid.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("txtActualQty");
                actualQty = Convert.ToInt32(txt.Text);
                sumOfActualQty = sumOfActualQty + actualQty;
            }

            /** Validate if sum of actual quantity is not greater than the available qty **/
            if (srControl.validateSumActualQty(sumOfActualQty, lblDBItemNo.Text))
            {
                foreach (GridViewRow row in DepartmentWiseDetailsGrid.Rows)
                {
                    lblStatus.Text = "";
                    departmentName = row.Cells[0].Text;
                    requiredQty = Convert.ToInt32(row.Cells[1].Text);
                    TextBox txt = (TextBox)row.FindControl("txtActualQty");
                    actualQty = Convert.ToInt32(txt.Text);
                    retrievalID = Convert.ToInt32(lblDBRetrievalID.Text);
                    itemCode = lblDBItemNo.Text;
                    try
                    {
                        srControl.updateDepartmentWiseList(departmentName, itemCode, actualQty, requiredQty, retrievalID);
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblStatus.Text = "Stationery Retrieval Form saved successfully";
                    }
                    catch (Exception ex)
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = ex.Message;
                        break;
                    }

                }
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "The total actual quantity cannot be greater thant available quantity in stock";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/ClerkHomeUI.aspx");
        }

        /** hide UI conctrols **/
        public void hideControls()
        {
            RetrievalTable.Visible = false;
            RetrievalDetailTable.Visible = false;
            lblTitle2.Visible = false;
            btnCancel.Visible = false;
            btnPrint.Visible = false;
            btnSave.Visible = false;
        }

        public void visbleControls()
        {
            RetrievalTable.Visible = true;
            RetrievalDetailTable.Visible = true;
            lblTitle2.Visible = true;
            btnCancel.Visible = true;
            btnPrint.Visible = true;
            btnSave.Visible = true;
        }

        /** Get stationery retrieval details **/
        public void displayStationeryRetrieval(StationeryRetrieval sr)
        {
            lblDBRetrievalID.Text = sr.RetrievalID.ToString();
            lblDBDate.Text = sr.DateRetrieved.Value.ToString("dd/MM/yyyy");

            /** Get the item wise list **/
            StationeryRetrievalGrid.DataSource = srControl.getItemWiseList(sr.RetrievalID);
            StationeryRetrievalGrid.DataBind();

            /** Display first record in Department wise list **/
            StationeryRetrievalDetail sd = srControl.getFirstItem(sr.RetrievalID);
            lblDBItemNo.Text = sd.ItemCode;
            lblDBDescription.Text = sd.Stock.ItemDescription;
            DepartmentWiseDetailsGrid.DataSource = srControl.getDepartmentWiseList(sr.RetrievalID, sd.ItemCode);
            DepartmentWiseDetailsGrid.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/StationeryRetrievalReport.aspx");
        }

    }
}