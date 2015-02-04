using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using BusinessLogicLayer;
using BusinessLogicLayer.Exception_Package;
using System.Data;
using System.Web.Security;


namespace PresentationLayer
{
    public partial class ReportDescrepancyUI : System.Web.UI.Page
    {
        ReportDescrepancyController rdControl = new ReportDescrepancyController();
        SearchStationeryController ssControl = new SearchStationeryController();
        DataTable dt = new DataTable();
        List<Stock> itemList;
        List<Stock> discrepancyList = new List<Stock>();
        int key = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                dt.Columns.AddRange(new DataColumn[9] { new DataColumn("ItemCode", typeof(String)),
                                new DataColumn("ItemCategory", typeof(string)),
                                new DataColumn("ItemDescription",typeof(string)),
                                new DataColumn("QtyAdjusted",typeof(int)),
                                new DataColumn("Deduct",typeof(bool)),
                                new DataColumn("Price",typeof(double)),
                                new DataColumn("Amount",typeof(double)),
                                new DataColumn("Reason",typeof(String)),
                                new DataColumn("key",typeof(int))
                               });
                Session["data"] = dt;
                ddlCategory.DataSource = ssControl.getCategoryList();
                ddlCategory.DataBind();
                Up1.Update();
                lblDisplayDate.Text = DateTime.Now.Date.ToShortDateString();
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Please select the items";


            }

        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            mpe1.Hide();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["data"];
            if (dt.Rows.Count != 0)
            {
                try
                {
                    saveGridVaules();
                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Failed to remove the item";
                }
            }
            ddlPopupCategory.DataSource = ssControl.getCategoryList();
            ddlPopupCategory.DataBind();
            String category = ddlCategory.SelectedItem.Text;
            if (category != null)
            {
                itemList = ssControl.getStockByCategory(category);
                if (itemList != null)
                {
                    ItemDetailsGrid.DataSource = itemList;
                    ItemDetailsGrid.DataBind();
                }
                else
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "No items found for this category";
                }
            }
            mpe1.Show();

        }

        protected void btnPopupSearch_Click(object sender, EventArgs e)
        {
            String category = ddlPopupCategory.SelectedItem.Text;

            itemList = ssControl.getStockByCategory(category);
            if (itemList != null)
            {
                ItemDetailsGrid.DataSource = itemList;
                ItemDetailsGrid.DataBind();
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblPopupStatus.Text = "No items found for this category";
            }

        }

        protected void btnPopupAddItems_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["data"];

            foreach (GridViewRow r in ItemDetailsGrid.Rows)
            {
                CheckBox chk = (CheckBox)r.FindControl("chkAdd");
                if (chk.Checked)
                {
                    if (dt.Rows.Count != 0)
                    {
                        DataRow dr = dt.Rows[dt.Rows.Count - 1];
                        key = Convert.ToInt32(dr["key"]) + 1;
                    }
                    else
                        key++;
                    String itemCode = r.Cells[1].Text;
                    String itemCategory = r.Cells[2].Text;
                    String itemDescription = r.Cells[3].Text;
                    double unitPrice = Convert.ToDouble(r.Cells[4].Text);
                    Stock i = rdControl.getStockByID(itemCode);
                    DataRow row = dt.NewRow();
                    row["key"] = key;
                    row["ItemCode"] = itemCode;
                    row["ItemCategory"] = itemCategory;
                    row["ItemDescription"] = itemDescription;
                    row["Price"] = unitPrice;
                    row["QtyAdjusted"] = 0;
                    row["Deduct"] = false;
                    row["Amount"] = 0.00;
                    dt.Rows.Add(row);
                    dt.AcceptChanges();


                }

            }
            discrepancyGrid.DataSource = dt;
            discrepancyGrid.DataBind();
            mpe1.Hide();
            Session["data"] = dt;
            btnCancel.Visible = true;
            btnSubmit.Visible = true;
            lblStatus.Text = "";

        }

        protected void discrepancyGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //try
            //{
            //    saveGridVaules();
            //}
            //catch (Exception ex)
            //{
            //    lblStatus.ForeColor = System.Drawing.Color.Red;
            //    lblStatus.Text = "Failed to remove the item";
            //}
            dt = (DataTable)Session["data"];
            int index = e.RowIndex;
            dt.Rows.RemoveAt(index);
            discrepancyGrid.DataSource = dt;
            discrepancyGrid.DataBind();
        }

        public void saveGridVaules()
        {
            dt = (DataTable)Session["data"];

            foreach (GridViewRow gr in discrepancyGrid.Rows)
            {
                int row_index = gr.RowIndex;
                foreach (DataRow row in dt.Rows)
                {
                    int dtKey = Convert.ToInt32(row["key"]);
                    int grdKey = Convert.ToInt32(discrepancyGrid.DataKeys[row_index]["key"]);
                    if (dtKey == grdKey)
                    {
                        TextBox txtQtyAdusted = (TextBox)gr.FindControl("txtQtyAdjusted");
                        row["QtyAdjusted"] = Convert.ToInt32(txtQtyAdusted.Text);
                        CheckBox chkDeduct = (CheckBox)gr.FindControl("chkDeduct");
                        if (chkDeduct.Checked)
                            row["Deduct"] = true;
                        else
                            row["Deduct"] = false;
                        TextBox txtReason = (TextBox)gr.FindControl("txtReason");
                        Label lblAmount = (Label)gr.FindControl("lblAmount");
                        row["Amount"] = Convert.ToDouble(lblAmount.Text);
                        row["Reason"] = txtReason.Text;

                        row.EndEdit();
                        dt.AcceptChanges();

                    }

                }
            }
            Session["data"] = dt;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            /** get employee name **/
            MembershipUser user = Membership.GetUser();
            String userName = user.UserName;
            String empID = rdControl.getEmployeeID(userName);
            double totalAmount = calculateTotalAmount();
            /** create discrepancy **/
            Discrepancy addDiscrepancy = new Discrepancy();
            addDiscrepancy.DateRaised = DateTime.Now;
            addDiscrepancy.DateUpdated = DateTime.Now;
            addDiscrepancy.DiscrepancyStatus = "Pending Approval";
            addDiscrepancy.DeletedFlag = false;
            addDiscrepancy.ApprovedBy = null;
            addDiscrepancy.Comment = null;
            addDiscrepancy.RaisedBy = empID;
            addDiscrepancy.TotalAmount = totalAmount;
            if (rdControl.insertDiscrepancy(addDiscrepancy))
            {
                int discrepancyID = rdControl.getDiscrepancyID();
                /** create discrepancy details **/
                foreach (GridViewRow row in discrepancyGrid.Rows)
                {
                    DiscrepancyDetail addDiscrepancyDetail = new DiscrepancyDetail();
                    addDiscrepancyDetail.DiscrepancyID = discrepancyID;
                    addDiscrepancyDetail.ItemCode = row.Cells[0].Text;
                    TextBox txtQtyAdjusted = (TextBox)row.FindControl("txtQtyAdjusted");
                    addDiscrepancyDetail.Quantity = Convert.ToInt32(txtQtyAdjusted.Text);
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    addDiscrepancyDetail.Amount = Convert.ToDouble(lblAmount.Text);
                    TextBox txtReason = (TextBox)row.FindControl("txtReason");
                    addDiscrepancyDetail.Reason = txtReason.Text;
                    CheckBox chkDeduct = (CheckBox)row.FindControl("chkDeduct");
                    if (chkDeduct.Checked)
                        addDiscrepancyDetail.IsAdded = false;
                    else
                        addDiscrepancyDetail.IsAdded = true;
                    addDiscrepancyDetail.DeletedFlag = false;

                    if (rdControl.insertDiscrepancyDetail(addDiscrepancyDetail))
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblStatus.Text = "Successfully submitted the request";
                    }
                    else
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = "Could not submit the request";
                    }

                }
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Could not submit the request";
            }
        }



        /** calculate total amount **/
        public double calculateTotalAmount()
        {
            double totalAmount = 0;

            foreach (GridViewRow row in discrepancyGrid.Rows)
            {
                Label lblAmount = (Label)row.FindControl("lblAmount");
                totalAmount = totalAmount + Convert.ToDouble(lblAmount.Text);
            }
            return totalAmount;
        }

        protected void txtQtyAdjusted_TextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            TextBox tb = (TextBox)sender;
            GridViewRow row = (GridViewRow)tb.Parent.Parent;

            TextBox txtQtyAdjusted = (TextBox)row.FindControl("txtQtyAdjusted");
            if (!String.IsNullOrEmpty(txtQtyAdjusted.Text))
            {
                Label lblAmount = (Label)row.FindControl("lblAmount");
                double price = Convert.ToDouble(row.Cells[4].Text);
                double qty = Convert.ToDouble(txtQtyAdjusted.Text);
                lblAmount.Text = (price * qty).ToString();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/ClerkHomeUI.aspx");
        }

        

    }
}

