using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Data;
namespace PresentationLayer
{
    public partial class CreatePurchaseOrderUI : System.Web.UI.Page
    {
        CreatePurchaseOrderController cpControl = new CreatePurchaseOrderController();
        SearchStationeryController stControl = new SearchStationeryController();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Stock> stockList = cpControl.getItemsBelowReorder();
                dt = cpControl.calculateReorderQty(stockList);
                Session["data"] = dt;
                if (dt != null)
                {
                    PODetailGrid.DataSource = dt;
                    PODetailGrid.DataBind();
                }
                else
                    lblStatus.Text = "Currently no items are below reorder level";
                lblDBItemCode.Visible = false;
                ddlCategory.DataSource = cpControl.getItemCategory();
                ddlCategory.DataBind();

              
            }

        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            mpe1.Hide();
        }

        protected void PODetailGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            int row_index = PODetailGrid.SelectedIndex;
            if (PODetailGrid.SelectedRow != null)
            {
                String itemCode = PODetailGrid.SelectedRow.Cells[0].Text;
                Stock item = cpControl.getItemDetails(itemCode);
                lblDBDescription.Text = PODetailGrid.SelectedRow.Cells[2].Text;
                lblDBPrice.Text = PODetailGrid.SelectedRow.Cells[7].Text;
                txtReorderQty.Text = PODetailGrid.SelectedRow.Cells[4].Text;
                lblKey.Text = PODetailGrid.DataKeys[row_index]["key"].ToString();
                lblDBReorderLevel.Text = item.ReorderLevel.ToString();
                lblDBAvailableQty.Text = item.AvailableQty.ToString();
                lblDBMinReorderQty.Text = item.MinReorderQty.ToString();
                lblDBItemCode.Text = itemCode;
                ddlSupplierName.DataSource = cpControl.getSupplierList(itemCode);
                ddlSupplierName.DataBind();
                String supplierCode = ddlSupplierName.SelectedItem.Text;
                lblDBPrice.Text = cpControl.getPriceForSupplier(itemCode, supplierCode).ToString();
            }

        }

        protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            String SpplierCode = ddlSupplierName.SelectedItem.Text;
            lblDBPrice.Text = cpControl.getPriceForSupplier(lblDBItemCode.Text, SpplierCode).ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["data"];
            foreach (DataRow row in dt.Rows)
            {
                int dtKey = Convert.ToInt32(row["key"]);
                int grdKey = Convert.ToInt32(lblKey.Text);
                if (dtKey == grdKey)
                {
                    row["MinReorderQty"] = Convert.ToInt32(txtReorderQty.Text);
                    row["Supplier"] = ddlSupplierName.SelectedItem.Text;
                    row["Price"] = Convert.ToDouble(lblDBPrice.Text);
                    row.EndEdit();
                    dt.AcceptChanges();

                }

            }
            PODetailGrid.DataSource = dt;
            PODetailGrid.DataBind();
            Session["data"] = dt;

        }

        protected void PODetailGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dt = (DataTable)Session["data"];
            int index = e.RowIndex;
            dt.Rows.RemoveAt(index);
            PODetailGrid.DataSource = dt;
            PODetailGrid.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String category = ddlCategory.SelectedItem.Text;
            ddlPopupCategory.DataSource = cpControl.getItemCategory();
            ddlPopupCategory.DataBind();
            ItemDetailsGrid.DataSource = stControl.getStockByCategory(category);
            ItemDetailsGrid.DataBind();
            mpe1.Show();

        }

        protected void btnPopupSearch_Click(object sender, EventArgs e)
        {
            String sCat = ddlPopupCategory.SelectedValue.ToString();
            ItemDetailsGrid.DataSource = stControl.getStockByCategory(sCat);
            ItemDetailsGrid.DataBind();

        }

        protected void btnPopupAddItems_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["data"];
            int key;
            foreach (GridViewRow r in ItemDetailsGrid.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkAdd");
                if (chk.Checked)
                {
                    if (dt.Rows.Count == 0)
                        key = 1;
                    else
                    {
                        DataRow dr = dt.Rows[dt.Rows.Count - 1];
                        key = Convert.ToInt32(dr["key"]) + 1;
                    }
                        DataRow row = dt.NewRow();
                        row["key"] = key;
                        row["ItemCode"] = r.Cells[1].Text;
                        row["ItemCategory"] = r.Cells[2].Text;
                        row["ItemDescription"] = r.Cells[3].Text.ToString();
                        row["UnitOfMeasure"] = r.Cells[4].Text;
                        row["MinReorderQty"] = Convert.ToInt32(r.Cells[5].Text);
                        row["Supplier"] = "";
                        row["Price"] = 0.00;
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    
                }
            }

            PODetailGrid.DataSource = dt;
            PODetailGrid.DataBind();
            mpe1.Hide();
            Session["data"] = dt;
        }

        protected void btnGeneratePO_Click(object sender, EventArgs e)
        {
            double totalAmount = 0;
            int PONumber;
            String itemCode;
            int qty;
            double price;
            lblStatus.Text = "";

            List<String> supplierList = getDistinctSupplier();

            try
            {
                if (supplierList != null)
                {

                    foreach (String s in supplierList)
                    {
                        totalAmount = calculateTotalAmount(s);
                        cpControl.createPO(s, totalAmount);
                        PONumber = cpControl.getLatestPOID();

                        foreach (GridViewRow row in PODetailGrid.Rows)
                        {
                            /** create PO detail **/
                            if (row.Cells[5].Text.Equals(s))
                            {
                                itemCode = row.Cells[0].Text;
                                qty = Convert.ToInt32(row.Cells[4].Text);
                                price = Convert.ToDouble(row.Cells[6].Text);
                                cpControl.createPODetail(PONumber, itemCode, qty, price);

                            }
                        }
                    }
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Text = "Purchase order generated succcessfully";
                }
                

            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = ex.Message;

            }

        }

        /** get distinct suppliers from Grid**/
        public List<String> getDistinctSupplier()
        {
            List<String> supplierList = new List<String>();
            String supplierCode;
            if (validateGridvalues())
            {
                foreach (GridViewRow gr in PODetailGrid.Rows)
                {
                    supplierCode = gr.Cells[5].Text;
                    supplierList.Add(supplierCode);
                }
                supplierList = supplierList.Distinct().ToList();
                return supplierList;
            }
            return null;

        }

        /** calculate total amount for each supplier **/
        public double calculateTotalAmount(String supplierCode)
        {
            double totalAmount = 0;
            foreach (GridViewRow gr in PODetailGrid.Rows)
            {
                if (gr.Cells[5].Text == supplierCode)
                {
                    Label lblAmount = (Label)gr.FindControl("lblAmount");
                    totalAmount = totalAmount + Convert.ToDouble(lblAmount.Text);
                }
            }
            return totalAmount;

        }

        /** validate gridValues **/
        public bool validateGridvalues()
        {
            bool success = true;
            foreach (GridViewRow gr in PODetailGrid.Rows)
            {
                if ((gr.Cells[4].Text.Equals("0") || (String.IsNullOrEmpty(gr.Cells[5].Text)) || (gr.Cells[6].Text.Equals("0"))))
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Quantity and Supplier details are mandatory";
                    success = false;
                    break;
                }
            }
            return success;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/ClerkHomeUI.aspx");
        }
    }
}

