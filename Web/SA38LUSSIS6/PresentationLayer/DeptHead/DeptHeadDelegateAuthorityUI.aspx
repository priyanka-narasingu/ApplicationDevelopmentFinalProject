<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="DeptHeadDelegateAuthorityUI.aspx.cs" Inherits="PresentationLayer.DeptHeadDelegateAuthorityUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript">
        $(function () {
            $(".dtp").datepicker({ minDate: 0, dateFormat: "dd/mm/yy" });
        });
        
    </script>
    <style type="text/css">
        .noBdr
        {
            border-top: 0px !important;
        }
        .txtlft
        {
            text-align: left;
        }
        
        .txtrgt
        {
            text-align: right;
        }
        .zeroMgn
        {
            margin: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Delegate Authority
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table">
                <tr class="">
                    <td class="txtlft">
                        <h4>
                            <label id="lblComments" runat="server" class="control-label zeroMgn">
                                Current Delegation:</label>
                            <asp:Label ID="lbldelegated" runat="server"></asp:Label></h4>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table">
                <tr class="">
                    <td class="col-sm-3">
                        <label class="control-label">
                            Choose Employee</label>
                    </td>
                    <td class="col-sm-3">
                        <asp:DropDownList ID="ddlemployee" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td class="col-sm-3">
                    </td>
                    <td class="col-sm-3">
                    </td>
                </tr>
                <tr class="">
                    <td class="col-sm-3 noBdr">
                        <label class="control-label">
                            Select Duration</label>
                    </td>
                    <td class="col-sm-3 noBdr">
                        <div class="form-inline">
                            <asp:TextBox ID="txtFromdate" placeholder="Start Date" CssClass="dtp mytexbox form-control"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                                SetFocusOnError="false" ControlToValidate="txtFromdate" Display="Static" ForeColor="Red"
                                ErrorMessage="Please choose a Start Date"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtFromdate"
                                ControlToValidate="txtEnddate" ErrorMessage="Invalid Date Range" ForeColor="Red"
                                Operator="GreaterThanEqual" Type="Date">*</asp:CompareValidator>
                        </div>
                    </td>
                    <td class="col-sm-3 noBdr txtlft">
                        <div class="form-inline">
                            <asp:TextBox ID="txtEnddate" placeholder="End Date" CssClass="dtp mytexbox form-control"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="false"
                                ControlToValidate="txtEnddate" Display="Static" Text="*" ForeColor="Red" ErrorMessage="Please choose an End Date "></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td class="col-sm-3 noBdr">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table txtrgt">
                <tr>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="#009900"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" CausesValidation="false" CssClass="btn btn-warning" runat="server"
                            Text="Cancel" OnClick="btnCancel_Click" />
                        <asp:Button ID="btnrevoke" CssClass="btn btn-danger" runat="server" Enabled="False"
                            OnClick="btnrevoke_Click" Text="Revoke" CausesValidation="false" />
                        <asp:Button ID="btnapprove" CssClass="btn btn-success" runat="server" OnClick="btnapprove_Click"
                            Text="Delegate" CausesValidation = "true" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--<div align="center">
        <asp:Panel ID="panel" runat="server" Width="500px" HorizontalAlign="Left">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <h2>
                Current Delegation:
            </h2>
            <br />
            Choose Employee
            <br />
            <br />
            <br />
            Start Date: End Date:
            <br />
            <asp:CalendarExtender ID="fromcdateextender" runat="server" TargetControlID="txtFromdate"
                OnClientDateSelectionChanged="CheckDateEalier" Format="dd/MM/yyyy">
            </asp:CalendarExtender>
            <asp:CalendarExtender ID="enddateextender" runat="server" TargetControlID="txtEnddate"
                Format="dd/MM/yyyy">
            </asp:CalendarExtender>
            <br />
            <br />
            <br />
        </asp:Panel>
    </div>--%>
</asp:Content>
