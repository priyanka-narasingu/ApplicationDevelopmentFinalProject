<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="StationeryRetrievalUI.aspx.cs" Inherits="PresentationLayer.RetrieveStationeryUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
        #ContentPlaceHolder1_StationeryRetrievalGrid > tbody > tr > th
        {
            text-align: center;
        }
        #ContentPlaceHolder1_DepartmentWiseDetailsGrid > tbody > tr > th
        {
            text-align: center;
        }
        .txtlft
        {
            text-align: left;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="text-center">
                        Stationery Retrieval Form
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <table id="RetrievalTable" class="table" runat="server">
                        <tr>
                            <td class="txtrgt">
                                <asp:Label ID="lblRetrievalID" runat="server" Text="Retrieval ID:" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="txtlft">
                                <asp:Label ID="lblDBRetrievalID" runat="server"></asp:Label>
                            </td>
                            <td class="txtrgt">
                                <asp:Label ID="lblDate" runat="server" Text="Date:" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="txtlft">
                                <asp:Label ID="lblDBDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="StationeryRetrievalGrid" runat="server" CssClass="table" AutoGenerateColumns="False"
                        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center"
                        OnSelectedIndexChanged="StationeryRetrievalGrid_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Number" />
                            <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="BinNumber" HeaderText="Bin Number" />
                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested Qty" />
                            <asp:BoundField DataField="AvailableQty" HeaderText="Available Qty" />
                            <asp:CommandField HeaderText="Details" ShowSelectButton="True" ControlStyle-ForeColor="Navy"
                                SelectText="View" />
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
            <h4 class="text-center">
                <asp:Label ID="lblTitle2" runat="server" Font-Bold="True" Font-Size="Medium" Text="Department wise Breakdown"></asp:Label></h4>
            <div class="row">
                <div class="col-sm-12">
                    <table id="RetrievalDetailTable" class="table" runat="server">
                        <tr>
                            <td class="txtrgt">
                                <asp:Label ID="lblItemNo" runat="server" Text="Item Number:" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="txtlft">
                                <asp:Label ID="lblDBItemNo" runat="server"></asp:Label>
                            </td>
                            <td class="txtrgt">
                                <asp:Label ID="lblDescription" runat="server" Text="Description:" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="txtlft">
                                <asp:Label ID="lblDBDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-10">
                    <asp:GridView ID="DepartmentWiseDetailsGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <Columns>
                            <asp:BoundField DataField="DeptName" HeaderText="Departent Name" ItemStyle-Width="50%" />
                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested Qty" ItemStyle-Width="25%" />
                            <asp:TemplateField HeaderText="Actual Qty" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="form-inline">
                                        <asp:TextBox ID="txtActualQty" CssClass="form-control" runat="server" Text='<%# Eval("ActualQty") %>'
                                            Style="text-align: right"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server"
                                            ControlToValidate="txtActualQty" ErrorMessage="The Actual quantity cannot be blank">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid number" ControlToValidate="txtActualQty" ForeColor="Red" ValidationExpression="^[0-9]*$">*</asp:RegularExpressionValidator>
                                            </div>
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
            </div>
            </div>
            <br />
            <br />
          
        </ContentTemplate>
    </asp:UpdatePanel>
      <div align="center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                <div class="row txtrgt" style="margin-bottom: 20px;">
                    <div class="col-sm-12">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" 
                            CssClass="btn btn-primary" onclick="btnPrint_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                            OnClick="btnCancel_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success" />
                    </div>
                </div>
</asp:Content>
