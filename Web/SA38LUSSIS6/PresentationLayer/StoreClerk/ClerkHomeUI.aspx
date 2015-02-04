<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ClerkHomeUI.aspx.cs" Inherits="PresentationLayer.ClerkHomeUI" %>
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
                                <asp:Label ID="lblRecenRequests" runat="server" Text=""></asp:Label></div>
                            <div class="smallTxt">
                                Outstanding Requests</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:HyperLink ID="viewRequisitionHistory" runat="server" NavigateUrl="~/StoreClerk/ClerkViewRequisitionHistoryUI.aspx">View Requistion History</asp:HyperLink></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-support fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblApprovedRequests" runat="server" Text=""></asp:Label></div>
                            <div class="smallTxt">
                                Approved Requests</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                         <asp:HyperLink ID="generatestionaryretrieval" runat="server" NavigateUrl="~/StoreClerk/StationeryRetrievalUI.aspx">Generate Stationary Retrieval</asp:HyperLink><span class="pull-right"><i class="fa fa-arrow-circle-right">
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
    <div class="row" style="margin-top: 30px;">
        <div class="col-sm-4">
            <div class="panel panel-red">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-comments fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblLowStockAlert" runat="server" Text=""></asp:Label></div>
                            <div class="smallTxt">
                                Low Stock Alert</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:HyperLink ID="replenishstock" runat="server" NavigateUrl="~/StoreClerk/CreatePurchaseOrderUI.aspx">Create Purchase Order</asp:HyperLink></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="panel panel-green">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-support fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblPendingDiscrepacies" runat="server" Text=""></asp:Label></div>
                            <div style="font-size:17px;">
                                Recently Raised Discrepancies</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:HyperLink ID="discrepancyHistory" runat="server" NavigateUrl="~/StoreClerk/ReportDiscrepancyUI.aspx">Raise Discrepancy</asp:HyperLink></span> <span class="pull-right">
                            <i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>
    <div class="col-sm-4">
    </div>
    <%--<div class="col-sm-3"></div>--%>
    </div>
</asp:Content>
