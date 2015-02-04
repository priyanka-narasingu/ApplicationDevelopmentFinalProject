<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="DeptHeadViewRequisitionHistoryUI.aspx.cs" Inherits="PresentationLayer.DeptViewRequisitionHistoryUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
        .style1
        {
            width: 226px;
        }
        .style2
        {
            width: 221px;
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
        #ContentPlaceHolder1_dgvEmpRequest > tbody > tr > th
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
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                View Requisition History
            </h3>
        </div>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table class="table col-lg-12">
        <tr>
            <td align="right" style="padding-top: 15px;">
                <label>
                    Request Date</label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtCalendar" runat="server" CssClass="dtp form-control" placeholder="Date"></asp:TextBox>
            </td>
            <td align="right" style="padding-top: 15px; padding-left: 0px; width: 75px;">
                <label>
                    Status</label>
            </td>
            <td class="style1">
                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control dropdown">
                    <asp:ListItem>Approved</asp:ListItem>
                    <asp:ListItem>Pending Approval</asp:ListItem>
                    <asp:ListItem>Rejected</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="generatebtn" runat="server" OnClick="generatebtn_Click" CssClass="btn btn-primary"
                    Text="Generate" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblnotfound" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lbldatte" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbltestdate" runat="server" Visible="false"></asp:Label>
    <div align="center">
        <asp:GridView ID="dgvRequest" CssClass="table" runat="server" AutoGenerateColumns="False"
            CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
            HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="S.NO" HeaderText="S.NO" />
                <asp:BoundField DataField="RequistionID" HeaderText="RequistionID" />
                <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" />
                <asp:BoundField DataField="DateRequested" HeaderText="DateRequested" />
                <asp:BoundField DataField="Comment" HeaderText="Comment" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
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
</asp:Content>
