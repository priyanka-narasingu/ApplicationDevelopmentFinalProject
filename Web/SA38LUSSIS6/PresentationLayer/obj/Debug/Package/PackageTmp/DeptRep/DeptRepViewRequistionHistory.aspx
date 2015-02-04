<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="DeptRepViewRequistionHistory.aspx.cs" Inherits="PresentationLayer.DeptRep.DeptRepViewRequistionHistory" %>

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
        .Popup
        {
            background-color: white;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
        }
        /*#ContentPlaceHolder1_dgvEmpReqHistory > tbody
        {
            height:100px;
            overflow-y:auto;
        }*/
        #ContentPlaceHolder1_dgvEmpReqHistory > tbody > tr > th
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
        .noBdr
        {
            border-top: 0px !important;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".dtp").datepicker({ dateFormat: 'dd/mm/yy' });
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Stationery Requisition History
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table text-center">
                <tr>
                    <td>
                        <label for="drpFrom" class="control-label topPnlbl">
                            Request Date</label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="dtp form-control" ID="dtpFrom" placeholder="From Date" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <h5>
                            To</h5>
                    </td>
                    <td>
                        <asp:TextBox CssClass="dtp form-control" ID="dtpTo" placeholder="To Date" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpFrom"
                            ControlToValidate="dtpTo" ForeColor="Red" Operator="GreaterThanEqual" SetFocusOnError="True"
                            EnableTheming="True" Type="Date">*</asp:CompareValidator>
                    </td>
                    <td>
                        <label for="drpFrom" class="control-label topPnlbl">
                            Status</label>
                    </td>
                    <td>
                        <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server">
                            <asp:ListItem Selected="True">All</asp:ListItem>
                            <asp:ListItem>Pending Approval</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>
                            <asp:ListItem>Rejected</asp:ListItem>
                            <asp:ListItem>Request Accepted</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass=" btn btn-primary" runat="server" Text="Search"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12" style="overflow: inherit; overflow-x: hidden;">
            <asp:GridView CssClass="table" ID="dgvEmpReqHistory" runat="server" HorizontalAlign="Center"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="DateCreated" HeaderText="Request Date" DataFormatString="{0:dd/MM/yyyy}"
                        HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Raised By" />
                    <asp:BoundField DataField="Comments" HeaderText="Comments" />
                    <asp:BoundField DataField="RequestStatus" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnView" CssClass="lnkbtnClr" runat="server" OnClick="btnView_Click">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#507cd1" Font-Bold="True" ForeColor="White" Height="25px"
                    HorizontalAlign="Center" VerticalAlign="Middle" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" Height="25px" HorizontalAlign="Center"
                    Width="700px" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
    </div>
    <asp:Panel ID="plViewDetails" runat="server" CssClass="Popup" align="center" Style="width: 700px;
        height: 520px;">
        <asp:UpdatePanel ID="udpDetailsUpdatePanel" runat="Server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="text-center">
                            Requistion Details
                        </h4>
                    </div>
                </div>
                <table class="table" style="margin-bottom: 10px;">
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblrID" runat="server" Text="Request ID: "></asp:Label>--%>
                            <label id="lblrID" class="control-label">
                                Request ID:</label>
                            <asp:Label ID="lblrIDCt" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblrDate" runat="server" Text="Request Date: "></asp:Label>--%>
                            <label id="Label1" class="control-label">
                                Request Date:</label>
                            <asp:Label ID="lblrDateCt" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblrStatus" runat="server" Text="Status: "></asp:Label>--%>
                            <label id="Label2" class="control-label">
                                Status:</label>
                            <asp:Label ID="lblrStatusCt" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblrCmt" runat="server" Text="Comments: "></asp:Label>--%>
                            <label id="Label3" class="control-label">
                                Comments:</label>
                            <asp:Label ID="lblrCmtCt" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table">
                    <tr>
                        <td class="noBdr">
                            <div style="height: 250px; overflow: auto; overflow-x: hidden;">
                                <asp:GridView ID="dgvRqDetails" CssClass="table" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Code" />
                                        <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    </Columns>
                                    <HeaderStyle BackColor="#cccccc" Font-Bold="True" Height="25px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="btnPopupCancel" CssClass="btn btn-warning" runat="server" Text="Close"
                    CausesValidation="false" OnClick="btnPopupCancel_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="btnFake" runat="server" Text="" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpeViewDetails" runat="server" PopupControlID="plViewDetails"
        TargetControlID="btnFake" CancelControlID="btnPopUpCancel" BackgroundCssClass="Background">
    </asp:ModalPopupExtender>
</asp:Content>
