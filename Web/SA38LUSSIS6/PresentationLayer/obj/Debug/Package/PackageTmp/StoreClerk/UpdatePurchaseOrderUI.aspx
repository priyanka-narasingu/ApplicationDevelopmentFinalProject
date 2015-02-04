<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="UpdatePurchaseOrderUI.aspx.cs" Inherits="PresentationLayer.UpdatePurchaseOrderUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
        .txtrgt
        {
            text-align: right;
        }
         #ContentPlaceHolder1_PODetailGrid > tbody > tr > th
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".dtp").datepicker({ minDate: 0 });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h4 class="text-center">
                Purchase Order Details
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-bordered">
                <tr>
                    <td class="col-sm-2">
                        <asp:Label ID="lblPONumber" runat="server" Text="PO Number"></asp:Label>
                    </td>
                    <td class="col-sm-4">
                        <asp:Label ID="lblDBPONumber" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-2">
                        <asp:Label ID="lblPODate" runat="server" Text="PO Date"></asp:Label>
                    </td>
                    <td class="col-sm-4">
                        <asp:Label ID="lblDBPODate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-2">
                        <asp:Label ID="lblSupplier" runat="server" Text="Supplier"></asp:Label>
                    </td>
                    <td class="col-sm-4">
                        <asp:Label ID="lblDBSupplier" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-2">
                        <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount"></asp:Label>
                    </td>
                    <td class="col-sm-4">
                        <asp:Label ID="lblDBTotalAmount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-2">
                        <asp:Label ID="lblDelveryOrderNo" runat="server" Text="Delivery Order Number"></asp:Label>
                    </td>
                    <td class="col-sm-4">
                        <asp:TextBox ID="txtDeliveryOrderNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqDelOrderNo" runat="server" ErrorMessage="Please enter Delivery Order No"
                            ControlToValidate="txtDeliveryOrderNumber" ValidationGroup="Delivered">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="col-sm-2">
                        <asp:Label ID="lblDeliveryDate" runat="server" Text="Delivery Date" ></asp:Label>
                    </td>
                    <td class="col-sm-4">
                        <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="dtp form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqDelDate" runat="server" ControlToValidate="txtDeliveryDate"
                            ErrorMessage="Please select the Delivery Date" ValidationGroup="Delivered">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-2">
                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                    </td>
                    <td class="col-sm-4">
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="col-sm-2">
                        Order Status
                    </td>
                    <td class="col-sm-4">
                        <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
     <asp:GridView ID="PODetailGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
            <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit Of Measure" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:BoundField DataField="Price" HeaderText="Amount" />
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
    <div align = "center">
    <asp:Label ID="lblStatus" runat="server" CssClass="noPrint" ForeColor="Red" Font-Bold="True"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="noPrint" ForeColor="Red" 
            ValidationGroup="Delivered" Font-Bold="True" />
    </div>
    <div class="row txtrgt">
        <div class="col-sm-12">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default noPrint" OnClientClick="JavaScript: window.history.back(1); return false;" />
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="window.print();" CssClass="btn btn-success noPrint" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel PO" CssClass="btn btn-warning noPrint"
                OnClick="btnCancel_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                CausesValidation="false" CssClass="btn btn-primary noPrint" />
        </div>
    </div>
</asp:Content>
