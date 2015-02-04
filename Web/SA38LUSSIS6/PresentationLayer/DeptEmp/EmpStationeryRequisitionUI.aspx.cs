/* Request Stationery Use Case */
/* Developer: Vignesh Sridharan */

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
    public partial class StationeryRequisitionUI : System.Web.UI.Page
    {
        //object instantiations

        RequestStationeryController rs = new RequestStationeryController();
        SearchStationeryController st = new SearchStationeryController();
        NotifyDeptHeadController nd = new NotifyDeptHeadController();
        List<Stock> addlst = new List<Stock>();
        List<RequestDetail> rdlist = new List<RequestDetail>();
        DataTable dt = new DataTable();
        int key = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populating Initial values

                lblReqID.Text = rs.getCurrentReqID().ToString();
                lblReqDate.Text = System.DateTime.Today.Date.ToShortDateString();

                List<String> cg = rs.getItemCategory();
                cg.Insert(0, "All");
                ddlCategory.DataSource = cg;
                ddlCategory.DataBind();

                List<Stock> l = st.getStock();
                ItemDetailsGrid.DataSource = l;
                ItemDetailsGrid.DataBind();

                //Datatable definition

                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ItemCode", typeof(String)),
                                new DataColumn("ItemCategory", typeof(string)),
                                new DataColumn("ItemDescription",typeof(string)),
                                new DataColumn("Quantity",typeof(int)),
                                new DataColumn("key",typeof(int))
                               });

                Session["data"] = dt;

                lblComments.Visible = false;
                txtComments.Visible = false;
                btnCancel.Visible = false;
                btnSave.Visible = false;


            }
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
                mpe1.Hide();
        }

        protected void btnPopupAddItems_Click(object sender, EventArgs e)
        {
            //Add selected items from the popup into the request items grid

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

                    DataRow row = dt.NewRow();
                    row["key"] = key;
                    row["ItemCode"] = itemCode;
                    row["ItemCategory"] = itemCategory;
                    row["ItemDescription"] = itemDescription;
                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                }
                dgvRequests.DataSource = dt;
                dgvRequests.DataBind();
                Session["data"] = dt;
            }

            dgvRequests.Visible = true;

            lblComments.Visible = true;
            txtComments.Visible = true;
            btnCancel.Visible = true;
            btnSave.Visible = true;

            mpe1.Hide();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Search and display items in the popup

            dt = (DataTable)Session["data"];
            if (dt.Rows.Count != 0)
            {
                saveGridValues();

            }

            List<String> cg = rs.getItemCategory();
            cg.Insert(0, "All");
            ddlPopupCategory.DataSource = cg;
            ddlPopupCategory.DataBind();


            String sCat = ddlCategory.SelectedValue.ToString();
            if (sCat == "All")
            {

                List<Stock> la = st.getStock();
                ItemDetailsGrid.DataSource = la;
                ItemDetailsGrid.DataBind();

            }
            else
            {
                List<Stock> lc = st.getStockByCategory(sCat);
                ItemDetailsGrid.DataSource = lc;
                ItemDetailsGrid.DataBind();

            }
            ddlPopupCategory.Text = ddlCategory.SelectedValue;
            mpe1.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Save the request and request details

            try
            {
                saveStationeryRequest();
                saveStationeryRequestDetail();
                sendEmail();
                successMsg();
            }
            catch
            { }

        }

        public void saveStationeryRequest()
        {
            String eName = User.Identity.Name;
            String empRaised = rs.getEmpID(eName);
            DateTime dCreated = System.DateTime.Now;
            String rStatus = "Pending Approval".ToString();
            String rComments = txtComments.Text;
            DateTime dUpdated = System.DateTime.Now;

            rs.saveRequestInfo(empRaised, dCreated, rStatus, rComments, dUpdated);
        }

        public void saveStationeryRequestDetail()
        {
            List<RequestDetail> lrd = setSelectedStationery();
            rs.saveRequestDetailInfo(lrd);
        }

        public List<RequestDetail> setSelectedStationery()
        {
            foreach (GridViewRow gdr in dgvRequests.Rows)
            {
                RequestDetail rDetail = new RequestDetail();
                rDetail.RequestID = rs.getReqID();
                rDetail.ItemCode = gdr.Cells[0].Text;
                TextBox tq = (TextBox)gdr.FindControl("txtQty");
                rDetail.Quantity = Convert.ToInt32(tq.Text);
                rDetail.DeletedFlag = false;
                rdlist.Add(rDetail);
            }
            return rdlist;
        }

        public void saveGridValues()
        {

            dt = (DataTable)Session["data"];

            foreach (GridViewRow gr in dgvRequests.Rows)
            {
                int row_index = gr.RowIndex;
                foreach (DataRow row in dt.Rows)
                {
                    int dtKey = Convert.ToInt32(row["key"]);
                    int grdKey = Convert.ToInt32(dgvRequests.DataKeys[row_index]["key"]);

                    if (dtKey == grdKey)
                    {
                        TextBox txtQtyAdusted = (TextBox)gr.FindControl("txtQty");


                        if (txtQtyAdusted.Text != "")
                        {
                            row["Quantity"] = Convert.ToInt32(txtQtyAdusted.Text);
                        }
                        else
                        {
                            row["Quantity"] = 0;
                        }
                        row.EndEdit();
                        dt.AcceptChanges();
                    }
                }
            }
            Session["data"] = dt;
        }

        protected void btnPopupSearch_Click(object sender, EventArgs e)
        {
            String sCat = ddlPopupCategory.SelectedValue.ToString();
            if (sCat == "All")
            {
                List<Stock> la = st.getStock();
                ItemDetailsGrid.DataSource = la;
                ItemDetailsGrid.DataBind();
            }
            else
            {
                List<Stock> lc = st.getStockByCategory(sCat);
                ItemDetailsGrid.DataSource = lc;
                ItemDetailsGrid.DataBind();
            }
        }


        protected void dgvRequests_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            saveGridValues();
            dt = (DataTable)Session["data"];
            int index = e.RowIndex;
            dt.Rows.RemoveAt(index);
            dgvRequests.DataSource = dt;
            dgvRequests.DataBind();
        }

        public void sendEmail()
        {
            String eName = User.Identity.Name;
            String reqID = rs.getReqID().ToString();
            String reqDate = lblReqDate.Text;
            String empName = rs.getEmpName(eName);
            String empID = rs.getEmpID(eName);
            String depName = rs.getDeptName(empID);
            String depCode = rs.getDeptCode(empID);
            String depHead = rs.getDeptHeadName(depCode);
            String dhEmail = rs.getDepHeadEmail(depCode);
            String bodyContent = "<div><div>Dear " + depHead + "</div><br><div>A new Stationery Request with Request ID: <b>" + reqID + "</b> has been raised.</div><br><div><br><div>Please log in to the LU Stationery System to Approve</div></div>";
            try
            {
                nd.emailDeptHead(empName, dhEmail, bodyContent);
            }
            catch
            {

            }

        }

        public void successMsg()
        {
            string message = "Request Raised Successfully.";
            string url = "EmpHomeUI.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptEmp/EmpHomeUI.aspx");
        }
    }
}