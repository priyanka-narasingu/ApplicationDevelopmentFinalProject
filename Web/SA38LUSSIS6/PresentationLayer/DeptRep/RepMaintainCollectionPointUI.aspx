<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="RepMaintainCollectionPointUI.aspx.cs" Inherits="PresentationLayer.RepViewRequisitionHistoryUI" %>

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
                            Change Collection PIN</h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="form-inline">
                            <label class="control-label">
                                PIN:</label>
                            <asp:TextBox ID="txtpinNo" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ForeColor="Red"
                                runat="server" ControlToValidate="txtpinNo" ErrorMessage="PIN number is mandatory"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtpinNo"
                                runat="server" Display="Static" ErrorMessage="PIN should be four digits" Text="*"
                                ForeColor="Red" ValidationExpression="^[1-9]+"></asp:RegularExpressionValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="noBdr">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                            Display="Static" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td class="txtrgt">
                        <asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" 
                            Text="Cancel" onclick="btnCancel_Click"/>
                        <asp:Button ID="btnsavechange" CssClass="btn btn-primary" runat="server" Text="Save Changes"
                            OnClick="btnsavechange_Click" />
                            
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
