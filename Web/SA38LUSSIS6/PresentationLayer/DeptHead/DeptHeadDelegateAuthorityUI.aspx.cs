using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BusinessLogicLayer;
using System.Data;

namespace PresentationLayer
{
    public partial class DeptHeadDelegateAuthorityUI : System.Web.UI.Page
    {
        MembershipUser user;
        DelegateAuthorityController dg = new DelegateAuthorityController();
        Boolean flag;
        String employeename;
        DateTime delegatedtime;
        DateTime startDate;
        DateTime endDate;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                lblStatus.Text = "";
                user = Membership.GetUser();
                Department dp = new Department();

                DateTime date = DateTime.Today.Date;

                Delegation del = new Delegation();
                
                
                Employee emp = new Employee();
                emp = dg.getEmployee(user.UserName.ToString());
                List<Employee> emplist = dg.getEmployeeList(emp.DeptCode.ToString());

                for (int i = 0; i < emplist.Count; i++)
                {
                    if (Roles.IsUserInRole(emplist[i].UserName, "DeptRep"))
                    {
                        
                        emplist.RemoveAt(i);
                        

                    }
                }

                List<Delegation> dele = new List<Delegation>();

                dele = dg.getDelegatedEmployee(emp.DeptCode.ToString());
                if (dele.Count > 0)
                {
                    for (int i = 0; i < dele.Count; i++)
                    {
                         flag = (Boolean)dele[i].DelegatedFlag;
                        employeename = dele[i].Employee.EmployeeName;
                        delegatedtime = Convert.ToDateTime(dele[i].EndDate);
                        startDate = Convert.ToDateTime(dele[i].StartDate);
                        endDate = Convert.ToDateTime(dele[i].EndDate);
                    }
                    btnrevoke.Enabled = true;
                    //DateTime comparedate = DateTime.ParseExact(delegatedtime,"dd/MM/yyyy",null);
                    lbldelegated.Text = employeename;
                    txtEnddate.Enabled = false;
                    txtFromdate.Enabled = false;
                    if (date >= delegatedtime)
                    {
                        string username = dg.getUserName(employeename);
                        Employee ep = new Employee();
                        ep = dg.getEmployeeID(employeename);
                        if (Roles.IsUserInRole(username, "DeptDel"))
                        {

                            Roles.RemoveUserFromRole(username, "DeptDel");
                        }
                        dg.reVoke(employeename);

                        btnrevoke.Enabled = false;
                    }
                    // if (date <= startDate)
                    //{
                    //    string username = dg.getUserName(employeename);
                    //    Roles.AddUserToRole(username, "DeptDel");
                    //    dg.Delegate(employeename);

                    //}
                    btnrevoke.Enabled = true;
                    btnapprove.Enabled = false;
                }
                
                
             else
                {

                    lbldelegated.Text = "No current delegation";
                    ddlemployee.DataSource = emplist;
                    ddlemployee.DataMember = "EmployeeID";
                    ddlemployee.DataValueField = "EmployeeName";
                    ddlemployee.DataBind();
                    txtFromdate.Enabled = true;
                    txtEnddate.Enabled = true;
                    btnapprove.Enabled = true;

                }
                


            }


        }
        MembershipUser delegateuser;

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startdate = DateTime.ParseExact(txtFromdate.Text, "dd/MM/yyyy", null);
                DateTime enddate = DateTime.ParseExact(txtEnddate.Text, "dd/MM/yyyy", null);

                string empname = ddlemployee.SelectedValue;
                dg.Delegate(empname, startdate, enddate);
                ////dg.updateEmployee(empname, "delegate");
                ////lblapprove.Text = "You have delegated to " + empname;
                lbldelegated.Text = empname;
                String username = dg.getUserName(empname);
                if(Roles.IsUserInRole(username,"DeptEmp"))
                {
                    Roles.RemoveUserFromRole(username, "DeptEmp");
                    Roles.AddUserToRole(username, "DeptDel");
                }

                   
            }
            catch (Exception ex)
            { }
            btnapprove.Enabled = false;
            btnrevoke.Enabled = true;
            txtEnddate.Enabled = false;
            txtFromdate.Enabled = false;
            ddlemployee.Enabled = false;
            lblStatus.Text = "Delegation successful!";

        }

        protected void btnrevoke_Click(object sender, EventArgs e)
        {
            ddlemployee.Enabled = true;
            string username=dg.getUserName(lbldelegated.Text);
            try
            {
                if (Roles.IsUserInRole(username, "DeptDel"))
                {
                    Roles.RemoveUserFromRole(username, "DeptDel");
                    if (!Roles.IsUserInRole(username, "DeptEmp"))
                    {
                        Roles.AddUserToRole(username, "DeptEmp");
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Failed to revoke delegation";
            }
            dg.reVoke(lbldelegated.Text);
            btnrevoke.Enabled = false;
            btnapprove.Enabled = true;

            user = Membership.GetUser();
            Department dp = new Department();

            DateTime date = DateTime.Today.Date;

            Delegation del = new Delegation();


            Employee emp = new Employee();
            emp = dg.getEmployee(user.UserName.ToString());
            List<Employee> emplist = dg.getEmployeeList(emp.DeptCode.ToString());

            for (int i = 0; i < emplist.Count; i++)
            {
                if (Roles.IsUserInRole(emplist[i].UserName, "DeptRep"))
                {

                    emplist.RemoveAt(i);


                }
            }

            lbldelegated.Text = "No current delegation";
            ddlemployee.DataSource = emplist;
            ddlemployee.DataMember = "EmployeeID";
            ddlemployee.DataValueField = "EmployeeName";
            ddlemployee.DataBind();
            btnapprove.Enabled = true;
            txtFromdate.Enabled = true;
            txtEnddate.Enabled = true;
            txtFromdate.Text = "";
            txtEnddate.Text = "";
            lblStatus.Text = "Revoked!";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeptHead/DeptHeadHomeUI.aspx");
        }

       


    }
}