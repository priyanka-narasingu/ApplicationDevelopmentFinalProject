<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ReportDiscrepancyUI.aspx.cs" Inherits="PresentationLayer.ReportDescrepancyUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 46%;
        }
        .style2
        {
            width: 86px;
        }
        .style3
        {
            width: 213px;
        }
        /** Search Items modal***/
        .Popup
        {
            background-color: white;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: auto;
            height: auto;
        }
        .style4
        {
            width: 242px;
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
        .topPnlbl
        {
            padding-top: 7px;
        }
        .noBdr
        {
            border-top: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Report Discrepancy
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 column">
            <table class="table text-center zeroMgn">
                <tr>
                    <td>
                        <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label>
                        <asp:Label ID="lblDisplayDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <table class="table">
        <tr>
            <td class="col-sm-4 txtrgt" style="padding-top: 15px;">
                <asp:Label ID="lblCategory" CssClass="control-label" runat="server" Text="Category"></asp:Label>
            </td>
            <td class="col-sm-4 txtlft">
                <asp:DropDownList ID="ddlCategory" CssClass=" dropdown form-control " runat="server">
                </asp:DropDownList>
            </td>
            <td class="col-sm-4 txtlft">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>
    <div class="row">
        <div class="col-sm-12" style="overflow: inherit; overflow-x: hidden;">
            <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:GridView ID="discrepancyGrid" runat="server" DataKeyNames="key" CssClass="table"
                        AutoGenerateColumns="False" CellPadding="1" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" GridLines="Vertical"
                        RowStyle-HorizontalAlign="Center" OnRowDeleting="discrepancyGrid_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" HtmlEncode="False"
                                HtmlEncodeFormatString="False" />
                            <asp:TemplateField HeaderText="Quantity Adjusted">
                                <ItemTemplate>
                                    <div class="form-inline">
                                        <asp:TextBox ID="txtQtyAdjusted" runat="server" CssClass="form-control" Text='<%# Eval("QtyAdjusted") %>'
                                            AutoPostBack="True" OnTextChanged="txtQtyAdjusted_TextChanged" CausesValidation="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Please enter the adjusted quantity"
                                            Font-Bold="True" Text="*" ControlToValidate="txtQtyAdjusted"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegExpValidator" runat="server" ErrorMessage="Please enter a valid number"
                                            ValidationExpression="^[0-9]+$" Text="*" ControlToValidate="txtQtyAdjusted"></asp:RegularExpressionValidator>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deduct">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDeduct" runat="server" Checked='<%# Eval("Deduct") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Price" HeaderText="Unit Price" />
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:UpdatePanel runat="server" ID="UpId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:Label ID="lblAmount" runat="server" CssClass="form-control" OnTextChanged="txtQtyAdjusted_TextChanged"
                                                Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReason" CssClass="form-control" runat="server" Text='<%# Eval("Reason") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="Remove" ControlStyle-ForeColor="OrangeRed">
                                <ControlStyle ForeColor="OrangeRed" />
                            </asp:CommandField>
                            <asp:BoundField DataField="key" HeaderText="Key" Visible="False" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div align="center">
        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Bold="True" ForeColor="Red" />
    </div>
    <div class="row txtrgt" style="margin-bottom: 15px;">
        <div class="col-sm-12 txtrgt">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                OnClick="btnCancel_Click" CausesValidation = "false" />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                CssClass="btn btn-success" />
        </div>
    </div>
    <!--Modal popup-->
    <asp:Button ID="btnFake" runat="server" Text="" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="Panel1" TargetControlID="btnFake"
        CancelControlID="btnPopUpCancel" BackgroundCssClass="Background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Height="550px"
        Width="700px" BorderStyle="Groove">
        <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="text-center">
                            Search Stationery
                        </h4>
                    </div>
                </div>
                <table class="table zeroMgn tblpdng">
                    <tr>
                        <td class="txtrgt">
                            <label class="control-label topPnlbl">
                                Category</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPopupCategory" runat="server" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnPopupSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                CausesValidation="false" OnClick="btnPopupSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div style="width: 600px; height: 350px; overflow: auto; overflow-x: hidden;">
                    <asp:GridView ID="ItemDetailsGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAdd" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
                            <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="Price1" HeaderText="Unit Price" />
                        </Columns>
                        <HeaderStyle BackColor="#cccccc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center"
                            VerticalAlign="Middle" Height="25px" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblPopupStatus" runat="server" ForeColor="Red"></asp:Label>
        <div style="margin-top: 20px; margin-bottom: 10px;">
            <asp:Button ID="btnPopupCancel" runat="server" Text="Cancel" CausesValidation="false"
                CssClass="btn btn-warning" OnClick="btnPopupCancel_Click" />
            <asp:Button ID="btnPopupAddItems" runat="server" Text="Add Items" CausesValidation="false"
                CssClass="btn btn-primary" OnClick="btnPopupAddItems_Click" />
        </div>
    </asp:Panel>
</asp:Content>
