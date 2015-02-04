<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="EmpStationeryRequisitionUI.aspx.cs" Inherits="PresentationLayer.StationeryRequisitionUI" %>

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
        /** Search Items modal window **/
        
        .PopupBG
        {
            background-color: Gray;
        }
        .Popup
        {
            background-color: white;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
        }
        .tblBG
        {
            background-color: white;
        }
        #ContentPlaceHolder1_dgvRequests > tbody > tr > th
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
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            padding: 4px !important;
            vertical-align: middle;
        }
        .tblpdng > thead > tr > th, .tblpdng > tbody > tr > th, .tblpdng > tfoot > tr > th, .tblpdng > thead > tr > td, .tblpdng > tbody > tr > td, .tblpdng > tfoot > tr > td
        {
            padding: 8px !important;
        }
        .noBdr
        {
            border-top: 0px !important;
        }
    </style>
    <script type="text/javascript">
        //        function validateQty() {
        //            $("#txtQty").addClass("has-error");
        //        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Request Stationery
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 column">
            <table class="table text-center zeroMgn">
                <tr>
                    <td>
                        <label class="control-label">
                            Request ID:</label>
                        <asp:Label ID="lblReqID" CssClass="control-label" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <label class="control-label">
                            Request Date:</label>
                        <asp:Label ID="lblReqDate" CssClass="control-label" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                    <td class="txtlft">
                        <label class="control-label">
                            Search Stationery</label>
                    </td>
                </tr>--%>
            </table>
            <table class="table zeroMgn tblpdng">
                <tr>
                    <td class="txtrgt">
                        <label class="control-label topPnlbl">
                            Category</label>
                    </td>
                    <td>
                        <asp:DropDownList CssClass="form-control" ID="ddlCategory" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                            Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12" style="overflow: inherit; overflow-x: hidden;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="dgvRequests" DataKeyNames="key" CssClass="table" runat="server"
                        AutoGenerateColumns="False" CellPadding="1" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" GridLines="Vertical"
                        RowStyle-HorizontalAlign="Center" OnRowDeleting="dgvRequests_RowDeleting">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <%--<asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRemove" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemCategory" HeaderText="Category" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%" HtmlEncode="False" HtmlEncodeFormatString="False">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="30%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <span class="form-group"><span class="col-sm-11" style="padding-right: 0px;">
                                        <asp:TextBox CssClass="form-control" ID="txtQty" runat="server" 
                                        Text='<%# Eval("Quantity") %>'></asp:TextBox>
                                    </span><span class="col-sm-1 txtlft" style="padding: 0px; padding-left: 4px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty"
                                            ErrorMessage="Quantity cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[1-9]+[0-9]*$"
                                            ControlToValidate="txtQty" ForeColor="Red" runat="server" ErrorMessage="Please Enter only digits">*</asp:RegularExpressionValidator></span>
                                        <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="" ControlToValidate="txtQty" ClientValidationFunction="validateQty()"></asp:CustomValidator>--%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" 
                                ControlStyle-ForeColor="OrangeRed" >
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
    <div class="row">
        <div class="col-sm-12 column">
            <table id="tblComm" class="table text-center zeroMgn">
                <tr class="">
                    <td class="txtlft">
                        <label id="lblComments" runat="server" class="control-label zeroMgn">
                            Requistion Comments</label>
                    </td>
                </tr>
                <tr>
                    <%--<td class="txtrgt">
                        <label class="control-label topPnlbl">
                            Requistion Comments</label>
                    </td>--%>
                    <td class="noBdr">
                        <asp:TextBox ID="txtComments" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            DisplayMode="SingleParagraph" ForeColor="Red" />
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table id="tblbtns" class="table txtrgt">
                <tr>
                    <td class="noBdr topPnlbl">
                        <asp:Button ID="btnCancel" CausesValidation="false" CssClass="btn btn-warning" runat="server"
                            Text="Cancel" OnClick="btnCancel_Click" />
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Panel ID="plSearchItem" runat="server" CssClass="Popup" align="center" Style="width: 700px;
        height: 510px;">
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
                            <asp:DropDownList CssClass="form-control" ID="ddlPopupCategory" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnPopupSearch" CssClass="btn btn-primary" runat="server" Text="Search"
                                OnClick="btnPopupSearch_Click" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
                <div id="ppdiv" runat="server" style="width: 500px; height: 350px; overflow: auto;
                    overflow-x: hidden;">
                    <asp:GridView ID="ItemDetailsGrid" CssClass="table tblBG" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAdd" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
                            <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                        </Columns>
                        <HeaderStyle BackColor="#cccccc" Font-Bold="True" ForeColor="Black" Height="25px"
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row" style="margin-top: 15px;">
            <div class="col-sm-12">
                <asp:Button ID="btnPopupCancel" CssClass="btn btn-warning" runat="server" Text="Cancel"
                    CausesValidation="false" OnClick="btnPopupCancel_Click" />
                <asp:Button ID="btnPopupAddItems" CssClass="btn btn-primary" runat="server" Text="Add Items"
                    CausesValidation="false" OnClick="btnPopupAddItems_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="btnFake" runat="server" Text="" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="plSearchItem" TargetControlID="btnFake"
        CancelControlID="btnPopUpCancel" BackgroundCssClass="popupBG">
    </asp:ModalPopupExtender>
</asp:Content>
