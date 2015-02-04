<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="SupViewStockAdjustmentUI.aspx.cs" Inherits="PresentationLayer.SupViewStockAdjustmentUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
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
        .topPnlbl
        {
            padding-top: 15px !important;
        }
        .noBdr
        {
            border-top: 0px !important;
        }
        #ContentPlaceHolder1_GridView1 > tbody > tr > th
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".dtp").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                View Stock Adjustment List
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-bordered">
                <tr>
                    <td class="col-sm-2 txtrgt topPnlbl">
                        <asp:Label ID="lblDate" CssClass="control-label" Font-Bold="true" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:TextBox ID="txtCalendar" runat="server" CssClass="dtp form-control"></asp:TextBox>
                    </td>
                    <td class="col-sm-2 txtrgt topPnlbl">
                        <asp:Label ID="lblStatus" CssClass="control-label" Font-Bold="true" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdown form-control"
                            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem>Pending Approval</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>
                            <asp:ListItem>Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="col-sm-2 txtrgt">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Label ID="lblnoData" runat="server" ForeColor="Red"></asp:Label>
    <div class="row">
        <div class="col-sm-12">
            <asp:GridView ID="GridView1" CssClass="table" runat="server" AutoGenerateColumns="False"
                CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="DiscrepancyID" HeaderText="Discrepancy ID" />
                    <asp:BoundField DataField="DateRaised" HeaderText="Date Raised" />
                    <asp:BoundField DataField="RaisedBy" HeaderText="Raised By" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Amount" />
                    <asp:BoundField DataField="DiscrepancyStatus" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnView" runat="server" OnClick="btnView_Click">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#507cd1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" Height="30px" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle ForeColor="Black" Height="" HorizontalAlign="Center" VerticalAlign="Middle"
                    BackColor="#EEEEEE" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
