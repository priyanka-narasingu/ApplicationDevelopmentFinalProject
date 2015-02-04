using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Web.Security;

namespace PresentationLayer
{
    public partial class RepViewRequisitionHistoryUI : System.Web.UI.Page
    {
        RepMaintainCollectionPointController rp = new RepMaintainCollectionPointController();
         
        MembershipUser user;
        string deptcode;
        Department dept = new Department();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Membership.GetUser();
            deptcode = rp.getDeptCode(user.UserName.ToString());
            
            dept = rp.getDepartment(deptcode);

            if (!IsPostBack)
            {
                if (dept.CollectionPoint.ToString() == "SSA") {rdbStationery.Checked = true;}
                if (dept.CollectionPoint.ToString() == "MGM") { rdbmanagement.Checked = true; }
                if (dept.CollectionPoint.ToString() == "SCI") { rdbscience.Checked = true; }
                if (dept.CollectionPoint.ToString() == "MDS") { rdbmedical.Checked = true; }
                if (dept.CollectionPoint.ToString() == "ENS") { rdbengineering.Checked = true; }
                if (dept.CollectionPoint.ToString() == "UHC") { rdbuhc.Checked = true; }
                txtpinNo.Text = dept.DeptCollectionPin.ToString();
            }
        }
        protected void btnsavechange_Click(object sender, EventArgs e)
        {
            if (rdbStationery.Checked)
            {
                rp.updateDepartment(deptcode, "SSA");
            }
            if (rdbmanagement.Checked)
            {
                rp.updateDepartment(deptcode, "MGM");

            }
            if (rdbengineering.Checked)
            {
                rp.updateDepartment(deptcode, "ENS");
            }
            if (rdbmedical.Checked)
            {
                rp.updateDepartment(deptcode, "MDS");
            }
            if (rdbscience.Checked)
            {
                rp.updateDepartment(deptcode, "SCI");
            }
            if (rdbuhc.Checked)
            {
                rp.updateDepartment(deptcode, "UHC");
            }
            
            
                int pinnumber = Convert.ToInt32(txtpinNo.Text);
                
                 rp.updatePin(deptcode, pinnumber);

                 successMsg();
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptRep/RepHomeUI.aspx");
        }


        public void successMsg()
        {
            string message = "Changes Saved Successfully !!";
            string url = "RepHomeUI.aspx";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

    }
}