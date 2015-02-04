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
    public partial class ViewPurchaseOrderUI : System.Web.UI.Page
    {
        UpdatePurchaseOrderController upControl = new UpdatePurchaseOrderController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSupplierName.DataSource = upControl.getAllSupplierList();
                ddlSupplierName.DataBind();

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String supplierName = ddlSupplierName.SelectedItem.Text;
            if (String.IsNullOrEmpty(txtPODate.Text))
            {
                PODetailGrid.DataSource = upControl.getPOListBySupplier(supplierName);
                PODetailGrid.DataBind();
            }
            else
            {
                String dt = txtPODate.Text + " 12:00:00 AM";
                try
                {
                    DateTime PODate = DateTime.ParseExact(dt, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    PODetailGrid.DataSource = upControl.getPOListBySuppAndDate(supplierName, PODate);
                    PODetailGrid.DataBind();
                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Please enter the date in correct format";
                }
            }
        }

        protected void PODetailGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PONumber"] = PODetailGrid.SelectedRow.Cells[0].Text;
            Response.Redirect("~/StoreClerk/UpdatePurchaseOrderUI.aspx");

        }
    }
}