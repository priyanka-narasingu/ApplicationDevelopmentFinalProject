<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="DeptHeadApproveRequestUI.aspx.cs" Inherits="PresentationLayer.DeptApproveRequestUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .txtrgt
        {
            text-align: right;
        }
        .txtlft
        {
            text-align: left;
        }
        #ContentPlaceHolder1_dgvReqDetails > tbody > tr > th
        {
            text-align: center;
        }
        .noBdr
        {
            border-top: 0px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Request Details
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-bordered text-center">
                <tr>
                    <td class="col-sm-3 txtrgt">
                        <label>
                            Request ID</label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:Label ID="lblReqID" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtrgt">
                        <label>
                            Request Date</label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:Label ID="lblReqDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-3 txtrgt">
                        <label>
                            Raised By</label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:Label ID="lblemployee" runat="server"></asp:Label>
                    </td>
                    <td class="col-sm-3 txtrgt">
                        <label>
                            Status</label>
                    </td>
                    <td class="col-sm-3 txtlft">
                        <asp:Label ID="lblReqStat" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div align="center">
        <asp:GridView ID="dgvReqDetails" CssClass="table" runat="server" AutoGenerateColumns="False"
            CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
            HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="S.No" HeaderText="S.No" />
                <asp:BoundField DataField="CategoryItem" HeaderText="CategoryItem" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
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
    <asp:Panel ID="mypanel" runat="server">
        <table class="table">
            <tr>
                <td align="left">
                    Comments:
                </td>
            </tr>
            <tr>
                <td class="noBdr">
                    <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="noBdr">
                    <asp:Label ID="lblshow" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="row txtrgt">
            <div class="col-sm-12">
                <asp:Button ID="btnback" runat="server" Text="Back" OnClick="btnback_Click1" CssClass="btn btn-warning" />
                <asp:Button ID="btnreject" runat="server" CssClass="btn btn-danger" OnClick="btnreject_Click"
                    Text="Reject" />
                <asp:Button ID="btnapprove" runat="server" CssClass="btn btn-success" OnClick="btnapprove_Click"
                    Text="Approve" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
