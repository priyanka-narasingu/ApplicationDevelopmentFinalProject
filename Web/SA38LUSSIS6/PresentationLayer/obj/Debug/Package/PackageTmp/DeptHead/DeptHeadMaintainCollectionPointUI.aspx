<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="DeptHeadMaintainCollectionPointUI.aspx.cs" Inherits="PresentationLayer.DeptMaintainCollectionPointUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .noBdr
        {
            border-top: 0px !important;
        }
        .txtrgt
        {
            text-align: right;
        }
        .collPtTbl > label
        {
            padding-left: 7px;
            vertical-align: inherit;
        }
        .txtlftrr
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="text-center">
                Maintain Collection Point Details
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 column">
            <table id="tblCCpt" class="table text-center zeroMgn">
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table collPtTbl">
                <tr class="">
                    <td class="txtlft noBdr">
                        <h4>
                            Change Collection Point</h4>
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-4 collPtTbl">
                        <asp:RadioButton ID="rdbStationery" runat="server" GroupName="1" Text="Stationery Store (9:30AM)" />
                    </td>
                    <td class="col-sm-4 collPtTbl">
                        <asp:RadioButton ID="rdbmanagement" runat="server" GroupName="1" Text="ManagementSchool (11:00AM)" />
                    </td>
                    <td class="col-sm-4 collPtTbl">
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-4 noBdr collPtTbl">
                        <asp:RadioButton ID="rdbmedical" runat="server" GroupName="1" Text="Medical School (9:30AM)" />
                    </td>
                    <td class="col-sm-4 noBdr collPtTbl">
                        <asp:RadioButton ID="rdbengineering" runat="server" GroupName="1" Text="Engineering School (11:00AM)" />
                    </td>
                    <td class="col-sm-4 noBdr collPtTbl">
                    </td>
                </tr>
                <tr>
                    <td class="col-sm-4 noBdr collPtTbl">
                        <asp:RadioButton ID="rdbscience" runat="server" GroupName="1" Text="Science School (9:30AM)" />
                    </td>
                    <td class="col-sm-4 noBdr collPtTbl">
                        <asp:RadioButton ID="rdbuhc" runat="server" GroupName="1" Text="University Health Centre (11:30AM)" />
                    </td>
                    <td class="col-sm-4 noBdr collPtTbl">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table">
                <tr class="">
                    <td class="txtlft noBdr">
                        <h4>
                            Change Department Representative</h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="form-inline">
                            <label class="control-label">
                                Current Representative:</label>
                            <asp:Label ID="lblrepresentativename" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="form-inline">
                            <label class="control-label">
                                Choose Representative:</label>
                            <asp:DropDownList ID="ddlemployee" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="noBdr">
                    </td>
                </tr>
                <tr>
                    <td class="txtrgt">
                        <asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" Text="Cancel"
                            OnClick="btnCancel_Click" />
                        <asp:Button ID="btnsavechange" CssClass="btn btn-primary" runat="server" Text="Save Changes"
                            OnClick="btnsavechange_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
