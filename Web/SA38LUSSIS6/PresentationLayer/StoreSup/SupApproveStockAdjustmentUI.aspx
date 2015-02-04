<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="SupApproveStockAdjustmentUI.aspx.cs" Inherits="PresentationLayer.ApproveStockAdjustmentUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 137px;
        }
        .style3
        {
            width: 165px;
        }
        .style4
        {
            width: 181px;
        }
        .style5
        {
            width: 116px;
        }
        #ContentPlaceHolder1_DiscrepancyDetailsGrid > tbody > tr > th
        {
            text-align: center;
        }
        
        .topPnlbl
        {
            padding-top: 7px;
        }
        .lnkbtnClr
        {
            color: Navy;
        }
        .txtrgt
        {
            text-align: right;
        }
        .txtlft
        {
            text-align: left;
        }
        .noBdr
        {
            border-top: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Approve Stock Adjustment
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-bordered">
                <tr>
                    <td class="col-sm-3 txtrgt">
                        <asp:Label ID="lblDiscrepancyID" runat="server" Text="Discrepancy ID"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:Label ID="lblDBDiscrepancyID" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtrgt">
                        <asp:Label ID="lblDateRaised" runat="server" Text="Date Raised"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:Label ID="lblDBDateRaised" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-3 txtrgt">
                        <asp:Label ID="lblRaisedBY" runat="server" Text="Raised By"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:Label ID="lblDBRaisedBy" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtrgt">
                    </td>
                    <td class="col-sm-3 txtlft">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:GridView ID="DiscrepancyDetailsGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
            <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity Adjusted" />
            <asp:TemplateField HeaderText="IsAdded">
                <ItemTemplate>
                    <asp:CheckBox ID="chkDeduct" runat="server" Checked='<%# Eval("IsAdded") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:BoundField DataField="Reason" HeaderText="Reason" />
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
    <div align="left">
        <asp:Label ID="lblReasonRejection" runat="server" Text="Reason for rejection"></asp:Label>
        <asp:TextBox ID="txtReasonRejection" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="row txtrgt" style="margin-top:20px; margin-bottom:20px;">
        <div class="col-sm-12">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default noPrint"
                OnClientClick="JavaScript: window.history.back(1); return false;" />
            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-warning"
                OnClick="btnReject_Click" />
            <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success" Text="Approve"
                OnClick="btnApprove_Click" />
        </div>
    </div>
</asp:Content>
