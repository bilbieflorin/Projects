<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Sign Up" AutoEventWireup="true" CodeFile="signUp.aspx.cs" Inherits="register" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="register_content" ContentPlaceHolderID="Body" runat="server">
    <div class="container down">
        <div class="leftAside"></div>
        <div class="main">
            <div class="box">
                <form runat="server" class="form-horizontal form" role="form">
                    <fieldset>
                        <legend class="text-center">Sign Up</legend>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label one-line" for="UserNameTextBox">User Name*</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="UserNameTextBox" CssClass="form-control" runat="server" Placeholder="Insert your user name here" Height="40px" Width="325px" />
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please insert your user name!" ForeColor="Red" ControlToValidate="UserNameTextBox" Display="Dynamic" />
                                <asp:Label runat="server" ForeColor="Red" ID="UserExistentLabel" Text="User name already taken!" Visible="false" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="FirstNameTextBox">First Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="FirstNameTextBox" CssClass="form-control" runat="server" Placeholder="Insert your first name here" Height="40px" Width="325px" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="LastNameTextBox">Last Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="LastNameTextBox" CssClass="form-control" runat="server" Placeholder="Insert your last name here" Height="40px" Width="325px" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="PasswordTextBox">Password*</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="PasswordTextBox" Placeholder="Insert your password" CssClass="form-control" runat="server" TextMode="Password" Height="40px" Width="325px" />
                                <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Please insert your password" Display="Dynamic" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="ConfirmPasswordTextBox">Confirm Password</label>
                            <div class="col-sm-2" style="width: 300px">
                                <asp:TextBox ID="ConfirmPasswordTextBox" CssClass="form-control" Placeholder="Confirm your password" runat="server" TextMode="Password" Height="40px" Width="325px" ForeColor="Black" />
                                <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ErrorMessage="Password do not match" ControlToCompare="ConfirmPasswordTextBox" ControlToValidate="PasswordTextBox" Display="Dynamic" ForeColor="Red" Width="320px" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="EmailTextBox">Email*</label>
                            <div class="col-sm-10">
                                <asp:TextBox runat="server" ID="EmailTextBox" CssClass="form-control" Placeholder="Insert your email" TextMode="Email" Height="40px" Width="325px" ForeColor="Black" />
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please insert your email" Display="Dynamic" ControlToValidate="EmailTextBox" ForeColor="Red" />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ForeColor="Red" ErrorMessage="Please insert a valid email adress" />
                                <asp:Label runat="server" ID="EmailExitentLabel" Visible="false" ForeColor="Red" Text="Email already existent! Please insert another one!" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="GenderButtonList">Gender</label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="GenderButtonList" runat="server">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                    <asp:ListItem Selected="True">Other</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="DateTextBox">BirthDay</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="DateTextBox" CssClass="form-control" Placeholder="Insert your birthday DD/MM/YYYY" runat="server" Height="40px" Width="325px" ForeColor="Black" />
                                <asp:CompareValidator ID="dateValidator" Display="Dynamic" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="DateTextBox" ErrorMessage="Please enter a valid date." />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <div class="col-sm-offset-2 col-sm-10">
                                <label>Fields marked with '*' are requiered!</label>
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:LinkButton runat="server" CssClass="btn btn-primary" OnClick="signUp">Sing Up</asp:LinkButton>
                            </div>
                        </div>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>
    <div class="footer"></div>
</asp:Content>
