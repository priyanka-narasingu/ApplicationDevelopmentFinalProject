<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="DisbursementListUI.aspx.cs" Inherits="PresentationLayer.DisbursemetListUI" %>

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
            padding-top: 7px;
        }
        .noBdr
        {
            border-top: 0px !important;
        }
        #ContentPlaceHolder1_DisbursementDetailGrid > tbody > tr > th
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Disbursement List
            </h3>
        </div>
    </div>
    <asp:Label ID="lblDBRetrievalID" runat="server" Visible="False"></asp:Label>
    <table class="table">
        <tr>
            <td class="col-sm-4 txtrgt" style="padding-top: 14px;">
                <asp:Label ID="lblDeptName" runat="server" Text="Department Name:" Font-Bold="True"></asp:Label>
            </td>
            <td class="col-sm-4 txtlft">
                <asp:DropDownList ID="ddlDeptName" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </td>
            <td class="col-sm-4 txtlft">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    CausesValidation="false" CssClass="btn btn-primary noPrint" />
            </td>
        </tr>
    </table>
    <table id="disbursementTable" runat="server" class="table table-bordered">
        <tr>
            <td class="col-sm-3">
                <asp:Label ID="lblDisbursementID" runat="server" Text="Disbursement ID:" Font-Bold="True"></asp:Label>
            </td>
            <td class="col-sm-3">
                <asp:Label ID="lblDBDisbursementID" runat="server"></asp:Label>
            </td>
            <td class="col-sm-3">
                <asp:Label ID="lblDisbursementStatus" runat="server" Text="Disbursement Status:"
                    Font-Bold="True"></asp:Label>
            </td>
            <td class="col-sm-3">
                <asp:Label ID="lblDBDisbursementStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="col-sm-3">
                <asp:Label ID="lblRepName" runat="server" Text="Representative:" Font-Bold="True"></asp:Label>
            </td>
            <td class="col-sm-3">
                <asp:Label ID="lblDBRepName" runat="server"></asp:Label>
            </td>
            <td class="col-sm-3">
                <asp:Label ID="lblCollectionPoint" runat="server" Text="Collection Point:" Font-Bold="True"></asp:Label>
            </td>
            <td class="col-sm-3">
                <asp:Label ID="lblDBCollectionPoint" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="col-sm-3">
                <asp:Label ID="lblDateofDisbursement" runat="server" Text="Date of Disbursement"
                    Font-Bold="True"></asp:Label>
            </td>
            <td class="col-sm-3">
                <asp:TextBox ID="txtDate" runat="server" CssClass="dtp form-control "></asp:TextBox>
            </td>
            <td class="col-sm-3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server"
                    ControlToValidate="txtDate" ErrorMessage="Please select the Disbursement date">*</asp:RequiredFieldValidator>
               
            </td>
            <td class="col-sm-3">
            </td>
        </tr>
    </table>
    
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="DisbursementDetailGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item No" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested Qty" />
                            <asp:TemplateField HeaderText="Actual Qty">
                                <ItemTemplate>
                                    <div class="form-inline">
                                        <asp:TextBox ID="txtActualQty" CssClass="form-control" runat="server" Text='<%# Eval("ActualQty") %>'
                                            Style="text-align: right"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="txtActualQty"
                                            ErrorMessage="Actual quantity cannot be blank" Font-Bold="True">*</asp:RequiredFieldValidator></div>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid number" ControlToValidate="txtActualQty" Font-Bold="True" ValidationExpression="^[0-9]+$">*</asp:RegularExpressionValidator>
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
                    <div align="center">
                        <asp:ValidationSummary ID="ValidationSummary" CssClass="noPrint" runat="server" ForeColor="Red" 
                            DisplayMode="List" Font-Bold="True" />
                        <asp:Label ID="lblStatus" runat="server" CssClass="noPrint" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                </div>
            </div>
            <div id="FormButtons" runat="server" class="row txtrgt" style="margin-bottom: 20px;">
                <div class="col-sm-12">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="window.print();" CssClass="btn btn-primary noPrint" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning noPrint"
                        OnClick="btnCancel_Click" CausesValidation = "false" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success noPrint" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
