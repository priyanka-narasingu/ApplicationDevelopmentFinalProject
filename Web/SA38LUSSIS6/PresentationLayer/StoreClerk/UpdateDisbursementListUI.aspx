<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="UpdateDisbursementListUI.aspx.cs" Inherits="PresentationLayer.UpdateDisbursementListUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /** Search Items modal window ***/
        .Popup
        {
            background-color: whitesmoke;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            width: 400px;
            height: 350px;
        }
        #ContentPlaceHolder1_StationeryDisbursementGrid > tbody > tr > th
        {
            text-align: center;
        }
        #ContentPlaceHolder1_DisbursementDetailsGrid > tbody > tr > th
        {
            text-align: center;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                View Disbursement List
            </h3>
        </div>
    </div>
    <table class="table zeroMgn tblpdng">
        <tr>
            <td class="txtrgt">
                <label class="control-label topPnlbl">
                    Department Name</label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartmentName" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                    CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <div class="row">
        <div class="col-sm-12">
            <asp:GridView ID="StationeryDisbursementGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
                CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center"
                OnSelectedIndexChanged="StationeryDisbursementGrid_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="DisbursementID" HeaderText="Disbursement ID" />
                    <asp:BoundField DataField="DateUpdated" HeaderText="Date Raised" DataFormatString="{0:dd/MM/yyyy}"
                        HtmlEncode="False" />
                    <asp:BoundField DataField="CollectionPointName" HeaderText="Collection Point" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Representative" />
                    <asp:BoundField DataField="DisbursementStatus" HeaderText="Status" />
                    <asp:CommandField HeaderText="Details" SelectText="View" ShowSelectButton="True"
                        ControlStyle-ForeColor="Navy" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#507cd1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" Height="30px" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle ForeColor="Black" Height="" HorizontalAlign="Center" VerticalAlign="Middle"
                    BackColor="#EEEEEE" />
                <SelectedRowStyle BackColor="#99cccc" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
    </div>
    <div align="center">
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
    </div>
    <div class="row txtrgt">
        <div class="col-sm-12">
          <%--  <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                CssClass="btn btn-warning" />--%>
            <%--<asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" />--%>
        </div>
    </div>
    <!--Modal PopUp-->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <input id="btn_Hid" type="hidden" runat="server" />
    <asp:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="Panel1" TargetControlID="btn_Hid"
        CancelControlID="btnPopUpCancel" BackgroundCssClass="Background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Style="width: 900px;
        height: 600px;">
        <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="text-center">
                            Update Disbursement Details
                        </h4>
                    </div>
                </div>
                <table class="table table-bordered" style="background-color: White;">
                    <tr>
                        <td class="col-sm-3">
                            <asp:Label ID="lblDisbursementID" runat="server" Text="Disbursement ID"></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:Label ID="lblDBDisbursementID" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:Label ID="lblPopupDepartmentName" runat="server" Text="Department Name"></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:Label ID="lblDBDepartmentName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col-sm-3">
                            <asp:Label ID="lblRepresentative" runat="server" Text="Representative"></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:Label ID="lblDBRepresentative" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:Label ID="lblCollectionPoint" runat="server" Text="Collection Point"></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:Label ID="lblDBCollectionPoint" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col-sm-3">
                            <asp:Label ID="lblDisbursementStatus" runat="server" Text="Disbursement Status"></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:DropDownList ID="ddlDisbursementStatus" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="col-sm-3">
                            <asp:Label ID="lblCollectionPin" runat="server" Text="Collection Pin"></asp:Label>
                        </td>
                        <td class="col-sm-3">
                            <asp:TextBox ID="txtCollectionPin" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="height: 300px; overflow: auto; overflow-x: hidden;">
                    <asp:GridView ID="DisbursementDetailsGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested Qty" />
                            <asp:TemplateField HeaderText="Actual Qty">
                                <ItemTemplate>
                                    <div class="form-inline">
                                        <asp:TextBox ID="txtActualQty" CssClass="form-control" runat="server" Text='<%# Eval("ActualQty") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorActualQty" ForeColor="Red"
                                            runat="server" ControlToValidate="txtActualQty" ErrorMessage="The Actual quantity cannot be blank"
                                            Text="*" Font-Bold="True"></asp:RequiredFieldValidator></div>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtActualQty"
                                        ErrorMessage="Please enter a valid number" Font-Bold="True" ValidationExpression="[0-9]+$">*</asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Label ID="lblPopupStatus" runat="server" ForeColor="Red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" DisplayMode="List" />
                <div class="row" style="margin-top: 10px;">
                    <div class="col-sm-12">
                        <asp:Button ID="btnPopupCancel" runat="server" Text="Cancel" CausesValidation="false"
                            CssClass="btn btn-warning" OnClick="btnPopupCancel_Click" />
                        <asp:Button ID="btnPopupUpdate" runat="server" Text="Update" OnClick="btnPopupUpdate_Click"
                            CssClass="btn btn-success" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
