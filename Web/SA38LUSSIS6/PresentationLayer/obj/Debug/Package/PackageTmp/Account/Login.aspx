<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="PresentationLayer.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loginModal" class="modal show" tabindex="-1" role="dialog" aria-hidden="true"
        style="margin-top: 100px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="text-center">
                        Login</h2>
                </div>
                <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
                    OnLoggedIn="LoginUser_LoggedIn">
                    <LayoutTemplate>
                        <div class="modal-body">
                            <div class="accountInfo">
                                <fieldset class="login">
                                    <p>
                                        <div class="form-group">
                                            <asp:Label ID="UserNameLabel" runat="server" CssClass="control-label" AssociatedControlID="UserName">Username</asp:Label>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                CssClass="failureNotification" ForeColor="Red" ErrorMessage="User Name is required."
                                                ToolTip="User Name is required." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </div>
                                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control input-lg textEntry"></asp:TextBox>
                                    </p>
                                    <p>
                                        <div class="form-group">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ForeColor="Red"
                                                ControlToValidate="Password" CssClass="failureNotification" ErrorMessage="Password is required."
                                                ToolTip="Password is required." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </div>
                                        <asp:TextBox ID="Password" runat="server"  CssClass="form-control input-lg passwordEntry"
                                            TextMode="Password"></asp:TextBox>
                                    </p>
                                    <p>
                                        <span class="failureNotification" style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                        </span>
                                        <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" ForeColor="Red"
                                            DisplayMode="SingleParagraph" CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup" />
                                    </p>
                                </fieldset>
                                <p class="submitButton">
                                    <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-primary btn-lg btn-block"
                                        CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                                </p>
                            </div>
                        </div>
                    </LayoutTemplate>
                </asp:Login>
                &nbsp;
            </div>
        </div>
    </div>
</asp:Content>
