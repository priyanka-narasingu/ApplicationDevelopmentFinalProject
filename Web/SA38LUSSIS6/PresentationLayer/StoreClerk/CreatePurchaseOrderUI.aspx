<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="CreatePurchaseOrderUI.aspx.cs" Inherits="PresentationLayer.CreatePurchaseOrderUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /** Search Items modal window ***/
        .Popup
        {
            background-color: white;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
        .textPD
        {
            margin-bottom: -12px;
        }
        #ContentPlaceHolder1_PODetailGrid > tbody > tr > th
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
        .topPnlbl
        {
            padding-top: 7px;
        }
        .zeroPD
        {
            padding-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Create Purchase Order
            </h3>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!--Search-->
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
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    CausesValidation="false" CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>
    <!--Modal window for Item Serach-->
    <asp:Button ID="btnFake" runat="server" Text="" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="Panel1" TargetControlID="btnFake"
        CancelControlID="btnPopUpCancel" BackgroundCssClass="Background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Style="width: 750px;
        height: 550px;">
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
                            <asp:Button ID="btnPopupSearch" CausesValidation="false" CssClass="btn btn-primary"
                                runat="server" Text="Search" OnClick="btnPopupSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div style="width: 700px; height: 350px; overflow: auto; overflow-x: hidden;">
                    <asp:GridView ID="ItemDetailsGrid" CssClass="table" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAdd" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                            <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" />
                            <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" />
                            <asp:BoundField DataField="MinReorderQty" HeaderText="Min Order Qty" />
                            <asp:BoundField DataField="AvailableQty" HeaderText="Availabler Qty" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="btnPopupCancel" CssClass="btn btn-warning" runat="server" Text="Cancel"
                    CausesValidation="false" OnClick="btnPopupCancel_Click" />
                <asp:Button ID="btnPopupAddItems" CssClass="btn btn-info" runat="server" Text="Add Items"
                    CausesValidation="false" OnClick="btnPopupAddItems_Click" />
            </div>
        </div>
    </asp:Panel>
    <div class="row">
        <div class="col-sm-12" style="overflow: inherit; overflow-x: hidden;">
            <asp:GridView ID="PODetailGrid" runat="server" DataKeyNames="key" OnSelectedIndexChanged="PODetailGrid_SelectedIndexChanged"
                OnRowDeleting="PODetailGrid_RowDeleting" CssClass="table" AutoGenerateColumns="False"
                CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
                    <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                    <asp:BoundField DataField="ItemDescription" HeaderText="Description" HtmlEncode="False" />
                    <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" />
                    <asp:BoundField DataField="MinReorderQty" HeaderText="Quantity" />
                    <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                    <asp:BoundField DataField="Price" HeaderText="Unit Price" />
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Convert.ToDouble(Eval("Price"))* Convert.ToDouble(Eval("MinReorderQty")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="" ShowSelectButton="True" SelectText="Edit" ControlStyle-ForeColor="Navy" />
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="OrangeRed" DeleteText="Remove" />
                    <asp:BoundField DataField="key" HeaderText="Key" Visible="False" />
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
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-bordered">
                <tr>
                    <td class="col-sm-3">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblDBDescription" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-3">
                        <asp:Label ID="lblReorderLevel" runat="server" Text="Reorder level"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblDBReorderLevel" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="Label2" runat="server" Text="Unit Price"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDBPrice" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-3">
                        <asp:Label ID="lblMinReorderQty" runat="server" Text="Min Reorder Quantity"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblDBMinReorderQty" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblAvailable" runat="server" Text="Available Quantity"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblDBAvailableQty" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-3">
                        <asp:Label ID="Label1" runat="server" Text="Order Quantity"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:TextBox ID="txtReorderQty" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblKey" runat="server" Visible="False"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server"
                            ControlToValidate="txtReorderQty" ErrorMessage="The quantity cannot be blank" Font-Bold="True">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="Please enter a valid number" ControlToValidate="txtReorderQty" 
                            Font-Bold="True" ValidationExpression="^[0-9]+$" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                    <td class="col-sm-3">
                        <asp:Label ID="lblDBItemCode" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div align="center">
        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"
            Font-Bold="True" DisplayMode="List" />
    </div>
    <div class="row txtrgt" style="margin-bottom: 20px;">
        <div class="col-sm-12">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                CssClass="btn btn-warning" CausesValidation="false" onclick="btnCancel_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                CssClass="btn btn-success" />
            <asp:Button ID="btnGeneratePO" runat="server" Text="Generate PO" CssClass="btn btn-primary"
                OnClick="btnGeneratePO_Click" />
        </div>
    </div>
</asp:Content>
