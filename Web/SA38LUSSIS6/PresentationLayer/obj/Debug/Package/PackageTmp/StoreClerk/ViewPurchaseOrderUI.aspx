<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ViewPurchaseOrderUI.aspx.cs" Inherits="PresentationLayer.ViewPurchaseOrderUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
        /** Search Items modal window **/
        .Popup
        {
            background-color: whitesmoke;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
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
        .noBdr
        {
            border-top: 0px !important;
        }
        #ContentPlaceHolder1_PODetailGrid > tbody > tr > th
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".dtp").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h4 class="text-center">
                View Purchase Orders
            </h4>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-sm-12">
            <table class="table">
                <tr>
                    <td class="col-sm-2 txtrgt" style="padding-top: 15px;">
                        <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td class="col-sm-2 txtrgt" style="padding-top: 15px;">
                        <asp:Label ID="lblPODate" runat="server" Text="PO Date"></asp:Label>
                    </td>
                    <td class="col-sm-3">
                        <asp:TextBox ID="txtPODate" CssClass="dtp form-control" runat="server"></asp:TextBox>
                    </td>
                    <td class="col-sm-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                            CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:GridView ID="PODetailGrid" OnSelectedIndexChanged="PODetailGrid_SelectedIndexChanged"
                CssClass="table" runat="server" AutoGenerateColumns="False" CellPadding="1" BackColor="White"
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                GridLines="Vertical" RowStyle-HorizontalAlign="Center">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="PONumber" HeaderText="PO Number" />
                    <asp:BoundField DataField="Supplier" HeaderText="Supplier " />
                    <asp:BoundField DataField="DateRaised" HeaderText="PO Date" DataFormatString="{0:dd/MM/yyyy}"
                        HtmlEncode="False" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Amount" />
                    <asp:BoundField DataField="POStatus" HeaderText="PO Status" />
                    <asp:CommandField HeaderText="Details" ShowSelectButton="True" 
                        ControlStyle-ForeColor="Navy" SelectText="View" >
<ControlStyle ForeColor="Navy"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#507cd1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" Height="30px" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle ForeColor="Black" Height="" HorizontalAlign="Center" VerticalAlign="Middle"
                    BackColor="#EEEEEE" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
    </div>
    <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
</asp:Content>
