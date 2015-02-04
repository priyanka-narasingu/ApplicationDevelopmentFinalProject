<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ReplenishStockUI.aspx.cs" Inherits="PresentationLayer.ReplenishStockUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Replenish Stock
            </h3>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search"
                            CausesValidation="False" OnClick="btnSearch_Click1" />
                    </td>
                </tr>
            </table>
            <div>
            </div>
            <div class="row">
                <div class="col-sm-12" >
                    <asp:GridView ID="StockDetailsGrid" CssClass="table" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Center" GridLines="Vertical" RowStyle-HorizontalAlign="Center"
                        Height="83px">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item No" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                               
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemCategory" HeaderText="Category" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                               
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                               
                            </asp:BoundField>
                            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Reorder Level">
                                <ItemTemplate>
                                    <span class="form-group"><span class="col-sm-10" style="padding-right: 0px;">
                                        <asp:TextBox ID="txtReorderLevel" runat="server" CssClass="form-control" Text='<%# Eval("ReorderLevel") %>'></asp:TextBox></span>
                                        <span class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="rfvReorderLevel" runat="server" ControlToValidate="txtReorderLevel"
                                                 ErrorMessage="Reorder Level cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
                                                         ControlToValidate="txtReorderLevel"
                                                         ValidationExpression="^\d+"
                                                         Display="Static"
                                                         ErrorMessage="Please enter numeric value"
                                                         Text="*"
                                                         ForeColor="Red"
                                                         EnableClientScript="True" 
                                                         runat="server"></asp:RegularExpressionValidator>
                                        </span></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reorder Qty">
                                <ItemTemplate>
                                    <span class="form-group"><span class="col-sm-10" style="padding-right: 0px;">
                                        <asp:TextBox ID="txtReorderQty" runat="server" CssClass="form-control" Text='<%# Eval("MinReorderQty") %>'></asp:TextBox></span>
                                        <span class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="rfvReorderQty" runat="server" ControlToValidate="txtReorderQty"
                                                ErrorMessage="Reorder Quantity cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator id="RegularExpressionValidator2" 
                                                         ControlToValidate="txtReorderQty"
                                                         ValidationExpression="^\d+"
                                                         Display="Static"
                                                         ErrorMessage="Please enter numeric value"
                                                         Text="*"
                                                         ForeColor="Red"
                                                         EnableClientScript="True" 
                                                         runat="server"></asp:RegularExpressionValidator></span>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty On Hand">
                                <ItemTemplate>
                                    <span class="form-group"><span class="col-sm-10" style="padding-right: 0px;">
                                        <asp:TextBox ID="txtQtyOnHand" runat="server" CssClass="form-control" Text='<%# Eval("AvailableQty") %>'></asp:TextBox></span>
                                        <span class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="rfvQtyOnHand" runat="server" ControlToValidate="txtQtyOnHand"
                                                ErrorMessage="Quantity cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                               <asp:RegularExpressionValidator id="RegularExpressionValidator3" 
                                                         ControlToValidate="txtQtyOnHand"
                                                         ValidationExpression="^\d+"
                                                         Display="Static"
                                                         ErrorMessage="Please enter numeric value"
                                                         Text="*"
                                                         ForeColor="Red"
                                                         EnableClientScript="True" 
                                                         runat="server"></asp:RegularExpressionValidator> </span>
                                    </span>
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
            <div>
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                    <asp:Label ID="lblSuccess" runat="server" ForeColor="#009933" Font-Bold="true"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1"
                  
                        EnableClientScript="true"
                        ForeColor="Red"
                        runat="server"/>
                        </div>
            <div class="row txtrgt" style="margin-bottom:20px;">
                
                    <span>
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel"
                            OnClick="btnCancel_Click1" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success"
                            OnClick="btnUpdate_Click1" /></span>
                
                
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
