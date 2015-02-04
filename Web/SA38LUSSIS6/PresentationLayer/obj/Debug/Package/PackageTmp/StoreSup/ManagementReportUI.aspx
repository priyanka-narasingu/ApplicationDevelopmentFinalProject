<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ManagementReportUI.aspx.cs" Inherits="PresentationLayer.StoreSup.ManagementReportsUI" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
        .PDRt
        {
            padding-right: 10px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".dtp").datepicker({ dateFormat: 'dd/mm/yy' });
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Management Reports
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-bordered text-center">
                <tr>
                    <td class="col-sm-3">
                        <label>
                            Report Name:</label>
                    </td>
                    <td class="col-sm-3">
                        <asp:DropDownList ID="ddlReport" CssClass="form-control" runat="server">
                            <asp:ListItem Value="OBC">Orders By Category</asp:ListItem>
                            <asp:ListItem Value="OBD">Orders By Department</asp:ListItem>
                            <asp:ListItem Value="OBS">Orders By Suppliers</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="col-sm-3">
                        <label>
                            Display Type:</label>
                    </td>
                    <td class="col-sm-3">
                        <div class="form-inline">
                            <asp:RadioButton ID="optTable" runat="server" CssClass="PDRt" Text="Table" GroupName="displayType"
                                Checked="true" />
                            <asp:RadioButton ID="optChart" runat="server" Text="Chart" GroupName="displayType" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-3">
                        <label>
                            Date Range:</label>
                    </td>
                    <td class="col-sm-3">
                        <div class="form-inline">
                            <asp:TextBox ID="dtpFrom" placeholder="From" CssClass="dtp form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter the Start Date"
                                ControlToValidate="dtpFrom" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td class="col-sm-3">
                        <div class="form-inline">
                            <asp:TextBox ID="dtpTo" placeholder="To" CssClass="dtp form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter the End Date"
                                ControlToValidate="dtpTo" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Invalid Date Range"
                                ControlToCompare="dtpFrom" ControlToValidate="dtpTo" ForeColor="Red" Operator="GreaterThan"
                                Type="Date">*</asp:CompareValidator>
                        </div>
                    </td>
                    <td class="col-sm-3">
                        <asp:Button ID="btnView" CssClass="btn btn-primary" runat="server" Text="View" OnClick="btnView_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row text-center">
                <div class="col-sm-12 text-center">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="OBCtblView" runat="server">
                            <CR:CrystalReportViewer ID="crvOBCtbl" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        </asp:View>
                        <asp:View ID="OBCchtView" runat="server">
                            <CR:CrystalReportViewer ID="crvOBCCht" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        </asp:View>
                        <asp:View ID="OBDtblView" runat="server">
                            <CR:CrystalReportViewer ID="crvOBDtbl" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        </asp:View>
                        <asp:View ID="OBDchtView" runat="server">
                            <CR:CrystalReportViewer ID="crvOBDcht" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        </asp:View>
                        <asp:View ID="OBStblView" runat="server">
                            <CR:CrystalReportViewer ID="crvOBStbl" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        </asp:View>
                        <asp:View ID="OBSchtView" runat="server">
                            <CR:CrystalReportViewer ID="crvOBScht" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
