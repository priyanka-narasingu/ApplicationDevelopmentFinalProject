<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="RepHomeUI.aspx.cs" Inherits="PresentationLayer.DeptRepHomeUI" %>
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
                                <asp:Label ID="lblPendingRequests" runat="server" Text="Label"></asp:Label></div>
                            <div class="smallTxt">
                                Recent Raised Requests</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Raise Request</asp:LinkButton></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
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
                                <asp:Label ID="lblAppEmp" runat="server" Text="Label"></asp:Label></div>
                            <div class="smallTxt">
                                Requests Pending Approval </div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">
                            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">View Request History</asp:LinkButton></span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
         <div class="col-sm-4"></div>
         <%--<div class="col-sm-3"></div>--%>
    </div>
</asp:Content>
