
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BusinessLogicLayer;


namespace PresentationLayer
{
    public partial class ReplenishStockUI : System.Web.UI.Page
    {
        ReplenishStockController replenishStockController = new ReplenishStockController();
        List<Stock> addlst = new List<Stock>();
        Stock sk = new Stock();
        protected void Page_Load(object sender, EventArgs e)
        {
           lblSuccess.Text = "";
            if (!IsPostBack)
            {
                List<String> cg = replenishStockController.getItemCategory();
                ddlCategory.DataSource = cg;
                ddlCategory.DataBind();
                string category = ddlCategory.SelectedValue.ToString();
                List<Stock> sL = replenishStockController.populateReplenishStock(category);
                StockDetailsGrid.DataSource = sL;
                StockDetailsGrid.DataBind();
            }

        }

        
         
        protected bool validateTextBox()
        {
            bool value=true;
            foreach (GridViewRow r in StockDetailsGrid.Rows)
            {               
                int ReorderLevel = Convert.ToInt32((r.Cells[4].FindControl("txtReorderLevel") as TextBox).Text);
                int MinReorderQty = Convert.ToInt32((r.Cells[5].FindControl("txtReorderQty") as TextBox).Text);
                if (ReorderLevel <= 0 || MinReorderQty <= 0)
                {
                    value = false;

                }

            }
            return value;

        }

       
        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            lblSuccess.Text = "";
            lblStatus.Text = "";
            string category = ddlCategory.SelectedValue.ToString();
            List<Stock> sL = replenishStockController.populateReplenishStock(category);
            StockDetailsGrid.DataSource = sL;
            StockDetailsGrid.DataBind();

        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
             bool check=validateTextBox();
            if(check==true)
            {
                lblStatus.Text = "";
                foreach (GridViewRow r in StockDetailsGrid.Rows)
                {                
                        sk.ItemCode = r.Cells[0].Text;
                        sk.ItemCategory = r.Cells[1].Text;
                        sk.ItemDescription = r.Cells[2].Text;
                        sk.UnitOfMeasure = r.Cells[3].Text;
                        sk.ReorderLevel = Convert.ToInt32((r.Cells[4].FindControl("txtReorderLevel") as TextBox).Text);
                        sk.MinReorderQty = Convert.ToInt32((r.Cells[5].FindControl("txtReorderQty")as TextBox).Text);
                        sk.AvailableQty = Convert.ToInt32((r.Cells[6].FindControl("txtQtyOnHand") as TextBox).Text);

                        addlst.Add(sk);
                        replenishStockController.updateStock(addlst);
                           
                 }
                lblSuccess.Text = "Updated Details Successfully!";  
            }
            else
            {
                lblStatus.Text = "Reorder Quantity or Reorder level cannot be zero or less than zero";
            }
            }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            // List<String> cg = replenishStockController.getItemCategory();
            //ddlCategory.DataSource = cg;
            //ddlCategory.DataBind();
            //string category = ddlCategory.SelectedValue.ToString();
            //List<Stock> sL = replenishStockController.populateReplenishStock(category);
            //StockDetailsGrid.DataSource = sL;
            //StockDetailsGrid.DataBind();
            //lblSuccess.Text = "";
            //lblStatus.Text = "";

            Response.Redirect("~/StoreClerk/ReplenishStockUI.aspx");
        }

        protected void StockDetailsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        }

          }
        
       
      
