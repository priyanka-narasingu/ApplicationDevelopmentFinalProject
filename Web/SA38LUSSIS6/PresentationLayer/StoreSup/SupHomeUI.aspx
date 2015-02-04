<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="SupHomeUI.aspx.cs" Inherits="PresentationLayer.SupHomeUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 30px;">
        <div class="col-sm-4">
            <div class="panel panel-yellow">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-comments fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblRecentRequest" runat="server" Text=""></asp:Label></div>
                            <div  class="smallTxt">
                                Ourtstanding Requests</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left"><asp:HyperLink ID="viewRequisitionHistory" runat="server" NavigateUrl="~/StoreClerk/ClerkViewRequisitionHistoryUI.aspx">View Requisition History</asp:HyperLink></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="panel panel-red">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-support fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblPendingDiscrepencies" runat="server" Text=""></asp:Label></div>
                            <div  class="smallTxt">
                                Pending Discrepencies</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left"><asp:HyperLink ID="viewStockAdjustment" runat="server" NavigateUrl="~/StoreSup/SupViewStockAdjustmentUI.aspx">View Stock Adjustments</asp:HyperLink></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-sm-4">
        </div>
        <%--<div class="col-sm-3"></div>--%>
    </div>
</asp:Content>
