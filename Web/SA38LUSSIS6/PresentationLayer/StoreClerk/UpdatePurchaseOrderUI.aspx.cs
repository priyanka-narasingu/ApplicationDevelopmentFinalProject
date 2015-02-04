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
    public partial class UpdatePurchaseOrderUI : System.Web.UI.Page
    {
        UpdatePurchaseOrderController upControl = new UpdatePurchaseOrderController();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
              
                ddlOrderStatus.DataSource = upControl.getPOStatus();
                ddlOrderStatus.DataBind();
                lblDBPONumber.Text = Session["PONumber"].ToString();
                int PONumber = Convert.ToInt32(lblDBPONumber.Text);
                PurchaseOrder po = upControl.getPO(PONumber);
                lblDBSupplier.Text = po.Supplier.ToString();
                lblDBPODate.Text = po.DateRaised.Value.ToString("dd/MM/yyyy");
                lblDBTotalAmount.Text = po.TotalAmount.ToString();
                String POStatus = po.POStatus;
                ddlOrderStatus.Text = POStatus;
                PODetailGrid.DataSource = upControl.getPODetail(PONumber);
                PODetailGrid.DataBind();

                if ((POStatus.Equals("Cancel")) || (POStatus.Equals("Delivered")))
                {
                   if(POStatus.Equals("Delivered"))
                   {
                    txtDeliveryDate.Text = po.DeliveryDate.Value.ToString("dd/MM/yyyy");
                    txtDeliveryOrderNumber.Text = po.DeliveryNo.ToString();
                   }
                    txtRemarks.Text = po.Comments;
                    txtDeliveryDate.Enabled = false;
                    txtDeliveryOrderNumber.Enabled = false;
                    txtRemarks.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnCancel.Enabled = false;
                    ddlOrderStatus.Enabled = false;
                }
            }

        }

        

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            String status = ddlOrderStatus.SelectedItem.Text;
            int PONumber = Convert.ToInt32(lblDBPONumber.Text);
            String remarks = txtRemarks.Text;

            if (status.Equals("Delivered"))
            {
                this.Page.Validate("Delivered");
                if (this.Page.IsValid)
                {
                    String deliveryNo = txtDeliveryOrderNumber.Text;
                    String dt = txtDeliveryDate.Text + " 12:00:00 AM";
                    DateTime deliveryDate = DateTime.ParseExact(dt, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    if (status != null)
                    {
                        upControl.updatePO(PONumber, deliveryNo, deliveryDate, status);
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblStatus.Text = "Record successfully saved";
                    }
                    else
                    {
                        upControl.updatePO(PONumber, deliveryNo, deliveryDate, status);
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblStatus.Text = "Record successfully saved";
                    }
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            String status = ddlOrderStatus.SelectedItem.Text;
            int PONumber = Convert.ToInt32(lblDBPONumber.Text);
            String remarks = txtRemarks.Text;

            if (status.Equals("Cancel"))
            {
                if (String.IsNullOrWhiteSpace(txtRemarks.Text))
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Please enter reason for cancelation";
                }
                else
                {
                    upControl.CancelPO(PONumber, status, remarks);
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Text = "Purchase Order cancelled";
                }
            }
        }
    }
}