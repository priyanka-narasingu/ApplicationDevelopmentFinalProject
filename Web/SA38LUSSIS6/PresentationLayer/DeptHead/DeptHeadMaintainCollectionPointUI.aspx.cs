/* Development of Maintain Collection Point Page*/
/* Developer: Vignesh Sridharan*/

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
    public partial class DeptMaintainCollectionPointUI : System.Web.UI.Page
    {
        // Initializations

        DeptHeadMaintainCollectionController control = new DeptHeadMaintainCollectionController();
        MembershipUser user;
        Department dept = new Department();
        string deptcode;
        List<Employee> emplist;
        string currentrepresentavtive;
        Employee eRep = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Membership.GetUser();
            deptcode = control.getDeptCode(user.UserName.ToString());

            List<Employee> emp = new List<Employee>();
            List<String> eName = new List<String>();
            emp = control.getEmployee(deptcode);
            if (!IsPostBack)
            {

                for (int i = 0; i < emp.Count; i++)
                {

                    if ("DR" == control.getEmpRole(emp[i].UserName))
                    {
                        lblrepresentativename.Text = emp[i].EmployeeName.ToString();
                    }

                }
                eName = control.getDeptEmpList(deptcode);
                eName.Insert(0, "");

                ddlemployee.DataSource = eName;
                ddlemployee.DataBind();

                Employee eRep = control.getRepresentativedEmployee(deptcode);

                currentrepresentavtive = eRep.UserName;
                Session["cRep"] = eRep.UserName.ToString();

                Department dept = new Department();
                dept = control.getDepartment(deptcode);


                if (dept.CollectionPoint.ToString() == "SSA")
                {
                    rdbStationery.Checked = true;
                }
                if (dept.CollectionPoint.ToString() == "MGM")
                {
                    rdbmanagement.Checked = true;
                }
                if (dept.CollectionPoint.ToString() == "ENS")
                {
                    rdbengineering.Checked = true;
                }
                if (dept.CollectionPoint.ToString() == "MDS")
                {
                    rdbmedical.Checked = true;
                }
                if (dept.CollectionPoint.ToString() == "SCI")
                {
                    rdbscience.Checked = true;
                }
                if (dept.CollectionPoint.ToString() == "UHC")
                {
                    rdbuhc.Checked = true;
                }
            }

        }

        protected void btnsavechange_Click(object sender, EventArgs e)
        {
            if (rdbStationery.Checked)
            {
                control.updateDepartment(deptcode, "SSA");
            }
            if (rdbmanagement.Checked)
            {
                control.updateDepartment(deptcode, "MGM");

            }
            if (rdbengineering.Checked)
            {
                control.updateDepartment(deptcode, "ENS");
            }
            if (rdbmedical.Checked)
            {
                control.updateDepartment(deptcode, "MDS");
            }
            if (rdbscience.Checked)
            {
                control.updateDepartment(deptcode, "SCI");
            }
            if (rdbuhc.Checked)
            {
                control.updateDepartment(deptcode, "UHC");
            }

            currentrepresentavtive = Session["cRep"].ToString();
            if (ddlemployee.SelectedItem.Text != "")
            {
                string username = control.getUserName(ddlemployee.SelectedItem.Text);
                try
                {
                    Roles.RemoveUserFromRole(currentrepresentavtive, "DeptRep");
                    Roles.AddUserToRole(currentrepresentavtive, "DeptEmp");
                    control.updateRole(currentrepresentavtive, "DE");
                    Roles.AddUserToRole(username, "DeptRep");
                    Roles.RemoveUserFromRole(username, "DeptEmp");
                    control.updateRole(username, "DR");

                    String emplID = control.getEmployeeID(username);
                    control.updateDepartmentRep(deptcode, emplID);

                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
                finally
                {


                }
            }
            successMsg();

        }

        public void successMsg()
        {
            string message = "Changes Saved Successfully !!";
            string url = "DeptHeadHomeUI.aspx";
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
            Response.Redirect("~/DeptHead/DeptHeadHomeUI.aspx");
        }
    }
}