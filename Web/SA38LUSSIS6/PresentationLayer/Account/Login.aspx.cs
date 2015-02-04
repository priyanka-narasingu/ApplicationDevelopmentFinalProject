using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace PresentationLayer.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            
           if (Roles.IsUserInRole(LoginUser.UserName, "DeptHead"))
                Response.Redirect("~/DeptHead/DeptHeadHomeUI.aspx");
            else if (Roles.IsUserInRole(LoginUser.UserName, "DeptRep"))
                Response.Redirect("~/DeptRep/RepHomeUI.aspx");
            else if (Roles.IsUserInRole(LoginUser.UserName, "DeptEmp"))
                Response.Redirect("~/DeptEmp/EmpHomeUI.aspx");
            else if (Roles.IsUserInRole(LoginUser.UserName, "DeptDel"))
                Response.Redirect("~/DeptHead/DeptHeadHomeUI.aspx");
            else if (Roles.IsUserInRole(LoginUser.UserName, "StoreClerk"))
                Response.Redirect("~/StoreClerk/ClerkHomeUI.aspx");
            else if (Roles.IsUserInRole(LoginUser.UserName, "StoreSup"))
                Response.Redirect("~/StoreSup/SupHomeUI.aspx");
            else if (Roles.IsUserInRole(LoginUser.UserName, "StoreMgr"))
                Response.Redirect("~/StoreMgr/MgrHomeUI.aspx");
        }

    }
}
