<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ViewDiscrepancyUI.aspx.cs" Inherits="PresentationLayer.ViewDescrepancyUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 72%;
        }
        .style2
        {
            width: 74px;
        }
        .style3
        {
            width: 139px;
        }
        .style4
        {
            width: 70px;
        }
        .style5
        {
            width: 58px;
        }
        .style6
        {
            width: 160px;
        }
        .Popup
        {
            background-color: white;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Larger" Text="View Descrepancy History"></asp:Label>
        <br />
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <table class="style1">
            <tr>
                <td class="style2">
                    <asp:Label ID="lblDate" runat="server" Text="By Date"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td class="style4">
                    <asp:Label ID="lblStatus" runat="server" Text="By Status"></asp:Label>
                </td>
                <td class="style6">
                    <asp:DropDownList ID="ddlStatus" runat="server" Height="16px" Width="149px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Style="margin-left: 16px" Text="Search" />
                </td>
            </tr>
        </table>
        <hr />
        <br />
        <asp:GridView ID="DescrepancyHistoryGrid" runat="server" AutoGenerateColumns="False"
            Style="margin-top: 0px" Width="380px">
            <Columns>
                <asp:BoundField DataField="DateRaised" HeaderText="Date Raised" />
                <asp:BoundField DataField="DeptName" HeaderText="Department" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Amount" />
                <asp:BoundField DataField="DescrepancyStatus" HeaderText="Status" />
                <asp:CommandField HeaderText="Details" SelectText="View" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        <div align="right" style="padding-right: 10%">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="margin-right: 20px;" />
            <asp:Button ID="btnPrint" runat="server" Text="Print" />
            <br />
        </div>
      </div>
    <asp:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="Panel1" TargetControlID="btnSearch"
        CancelControlID="btnPopUpCancel" BackgroundCssClass="Background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Style="display: none;
        width: 900px; height: 400px;">
        <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblPopupTitle" runat="server" Font-Bold="True" Font-Size="Medium" Text="View Descrepancy Details"></asp:Label>
                <table class="style1">
                    <tr>
                        <td class="style3">
                            <asp:Label ID="lblPopupVoucher" runat="server" Text="Voucher"></asp:Label>
                        </td>
                        <td class="style2">
                             <asp:Label ID="lblDBDescrepancyID" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="style2">
                              <asp:Label ID="lblPopupDateIssued" runat="server" Text="Date Issued"></asp:Label>
                        </td>
                        <td class="style2">
                              <asp:Label ID="lblDBDateRaised" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td class="style3">
                            <asp:Label ID="lblPopupRaisedBy" runat="server" Text="Raised By"></asp:Label>
                        </td>
                        <td class="style2">
                             <asp:Label ID="lblDBRaisedBy" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="style2">
                              <asp:Label ID="lblPopupStatus" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="style2">
                              <asp:Label ID="lblDBDescrepancyStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td class="style2">
                              <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label>
                        </td>
                         <td class="style2">
                              <asp:Label ID="lblDBApprovedBy" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="style2">
                              <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount"></asp:Label>
                        </td>
                         <td class="style2">
                              <asp:Label ID="lblDBTotalAmount" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                </table>
                <asp:GridView ID="DescrepancyDetailsGrid" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        
                        <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
                        <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                         <asp:BoundField DataField="Quantity" HeaderText="Qty Adjusted" />
                     <asp:TemplateField HeaderText="Deduct">
                    <ItemTemplate>
                        <asp:TextBox ID="txtReorderLevel" runat="server" Text='<%# Eval("IsAdded") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Price1" HeaderText="Unit Price" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnPopUpCancel" runat="server" Text="Cancel" CausesValidation="false" />
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
