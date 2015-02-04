<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="DeptHeadHomeUI.aspx.cs" Inherits="PresentationLayer.DeptHeadHomeUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 30px;">
        <div class="col-sm-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-comments fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblPendingRequests" runat="server" Text="Label"></asp:Label></div>
                            <div class="smallTxt">
                                Requests Pending Approval</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Approve</asp:LinkButton></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
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
                                <asp:Label ID="lblApproved" runat="server" Text="Label"></asp:Label></div>
                            <div class="smallTxt">
                                Recent Approved Requests</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">View History</asp:LinkButton></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
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
            <div class="panel panel-yellow">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-comments fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblCurrentDelegation" runat="server" Text="Label"></asp:Label></div>
                            <div class="smallTxt">
                                Current Delegations</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Change Delegation</asp:LinkButton>   </span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
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
                                <asp:Label ID="lblCollectionPt" runat="server" Text="Label"></asp:Label></div>
                            <div class="smallTxt">
                                Current Collection Point</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Change Collection Point Details</asp:LinkButton></span> <span class="pull-right">
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
