<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ClerkViewRequisitionHistoryUI.aspx.cs" Inherits="PresentationLayer.ClerkViewRequisitionHistoryUI"
    Culture="en-US" %>

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
            width: 100%;
            height: 32px;
            margin-bottom: 0px;
        }
        .style3
        {
            height: 28px;
            width: 131px;
        }
        .style4
        {
            height: 28px;
            width: 164px;
        }
        .style5
        {
            height: 28px;
            width: 143px;
        }
        .style6
        {
            height: 28px;
            width: 67px;
        }
        .style7
        {
            height: 28px;
            width: 85px;
        }
        .style8
        {
            height: 28px;
        }
        /** Search Items modal window **/
        
        .PopupBG
        {
            background-color: Gray;
        }
        .Popup
        {
            background-color: #fcfcfc;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
        }
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
        .txtlft
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
            $(".dtp").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                    <td style="padding-top: 15px;">
                        <asp:Label ID="lblFromDate" runat="server" Font-Bold="true" Text="Request Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromdate" placeholder="From Date" runat="server" CssClass="dtp form-control "></asp:TextBox>
                    </td>
                    <td style="padding-top: 15px;">
                        <asp:Label ID="lblToDate" runat="server" Text="To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="dtp form-control "></asp:TextBox>
                    </td>
                    <td class="txtlft">
                        <asp:CompareValidator ID="CompareValidator1" ForeColor="Red" runat="server" ControlToCompare="txtFromdate"
                            ControlToValidate="txtToDate" ErrorMessage="To date cannot be earlier than the from date"
                            Operator="GreaterThanEqual" Type="Date" SetFocusOnError="False">*</asp:CompareValidator>
                    </td>
                    <td>
                        <label class="control-label topPnlbl">
                            Status</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control dropdown">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                            OnClick="btnSearch_Click" CausesValidation="true" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:GridView ID="requestDetailGrid" CssClass="table" AutoGenerateColumns="False"
        runat="server" CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center"
        OnSelectedIndexChanged="requestDetailGrid_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="RequestID" HeaderText="Request ID" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="DeptName" HeaderText="Department Name" />
            <asp:BoundField DataField="DateCreated" HeaderText="Date Requested" DataFormatString="{0:dd/MM/yyyy}"
                HtmlEncode="False" />
            <asp:BoundField DataField="RequestStatus" HeaderText="Status" />
            <asp:CommandField HeaderText="Details" SelectText="View" ShowSelectButton="True" />
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" Font-Bold="True" />
    <div align="right">
        <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
            CssClass="btn btn-warning" />--%>
    </div>
    <br />
    <div align="center">
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
    </div>
    <!--modal popup-->
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
                <table class="table">
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblrID" runat="server" Text="Request ID: "></asp:Label>--%>
                            <label id="lblrID" class="control-label">
                                Request ID:</label>
                            <asp:Label ID="lblDBRequestID" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblrDate" runat="server" Text="Request Date: "></asp:Label>--%>
                            <label id="lblrDate" class="control-label">
                                Request Date:</label>
                            <asp:Label ID="lblDBRequestDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblrStatus" runat="server" Text="Status: "></asp:Label>--%>
                            <label id="lblDepartmentName" class="control-label">
                                Department:</label>
                            <asp:Label ID="lblDBDeptName" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblrStatus" runat="server" Text="Status: "></asp:Label>--%>
                            <label id="lblrStatus" class="control-label">
                                Status:</label>
                            <asp:Label ID="lblDBRequestStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblrCmt" runat="server" Text="Comments: "></asp:Label>--%>
                            <label id="lblrCmt" class="control-label">
                                Comments:</label>
                            <asp:Label ID="lblDBComments" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table">
                    <tr>
                        <td class="noBdr">
                            <div style="height: 220px; overflow: auto; overflow-x: hidden;">
                                <asp:GridView ID="requestPopupGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Code" />
                                        <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="btnPopupCancel" CssClass="btn btn-warning" runat="server" Text="Cancel"
                    CausesValidation="false" OnClick="btnPopupCancel_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="btnFake" runat="server" Text="" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="plViewDetails" TargetControlID="btnFake"
        CancelControlID="btnPopUpCancel" BackgroundCssClass="Background">
    </asp:ModalPopupExtender>
</asp:Content>
