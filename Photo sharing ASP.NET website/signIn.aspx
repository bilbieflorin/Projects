<%@ Page Language="C#" AutoEventWireup="true" Title="Sign In" MasterPageFile="~/MasterPage.master" CodeFile="signIn.aspx.cs" Inherits="login" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="login_content" ContentPlaceHolderID="Body" runat="server">
    <div class="container down">
            <div class="leftAside"></div>
            <div class="main">
                <div class="box">
                    <form runat="server" class="form-horizontal form" role="form">
                        <fieldset>
                            <legend class="text-center">Sing In</legend>
                            <div id="ErrorLabel" class="alert alert-danger text-center" Visible="false" runat="server">User Name or/and password wrong!</div>
                            <div class="form-group div-left">
                                <label class="col-sm-2 control-label" for="UserNameTextBox">User Name</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="UserNameTextBox" CssClass="form-control" runat="server" Placeholder="Insert your user name here" Height="40px" Width="325px" />
                                    <asp:RequiredFieldValidator ID="UserNameRequiredValidator" runat="server" ControlToValidate="UserNameTextBox" Display="Dynamic" ForeColor="Red" ErrorMessage="Please insert your user name" />
                                </div>
                            </div>
                            <div class="form-group div-left">
                                <label class="col-sm-2 control-label" for="PasswordTextBox">Password</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="PasswordTextBox" Placeholder="Insert your password" CssClass="form-control" runat="server" type="password" Height="40px" Width="325px" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox" Display="Dynamic" ForeColor="Red" ErrorMessage="Please insert your password" />
                                </div>
                            </div>
                            <div class="form-group div-left">
                                <div class="col-sm-offset-2 col-sm-10">
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary" OnClick="singIn">Sing In</asp:LinkButton>
                                </div>
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
</asp:Content>
