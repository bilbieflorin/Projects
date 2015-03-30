<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Profile" AutoEventWireup="true" CodeFile="~/user.aspx.cs" Inherits="user" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="register_content" ContentPlaceHolderID="Body" runat="server">
    <div class="container down">
        <div class="leftAside"></div>
        <div class="main">
            <div class="box">
                <form id="Form1" runat="server" class="form-horizontal form" role="form">
                    <fieldset>
                        <legend class="text-center">Profile Info
                            <button id="Edit" onserverclick="Edit_ServerClick" runat="server" style="position: relative; top: -7px;" class="pull-right btn btn-default">Edit <span class="glyphicon glyphicon-wrench"></span></button>
                        </legend>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label one-line" for="UserNameTextBox">User Name</label>
                            <div class="col-sm-10">
                                <asp:Label CssClass="col-sm-2 control-label txt-left" ID="UserNameLabel" runat="server" Width="150px"></asp:Label>
                                <asp:TextBox ID="UserNameTextBox" Visible="false" onkeydown="javascript: return false" CssClass="form-control" runat="server" Placeholder="Insert your user name here" Height="40px" Width="325px" />
                                <asp:Label runat="server" ForeColor="Red" ID="UserExistentLabel" Text="User name already taken!" Visible="false" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="FirstNameTextBox">First Name</label>
                            <div class="col-sm-10">
                                <asp:Label CssClass="col-sm-2 control-label txt-left" ID="FirstNameLabel" runat="server" Width="150px"></asp:Label>
                                <asp:TextBox ID="FirstNameTextBox" Visible="false" CssClass="form-control" runat="server" Placeholder="Insert your first name here" Height="40px" Width="325px" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="LastNameTextBox">Last Name</label>
                            <div class="col-sm-10">
                                <asp:Label CssClass="col-sm-2 control-label txt-left" ID="LastNameLabel" runat="server" Width="150px"></asp:Label>
                                <asp:TextBox ID="LastNameTextBox" Visible="false" CssClass="form-control" runat="server" Placeholder="Insert your last name here" Height="40px" Width="325px" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="PasswordTextBox">Password</label>
                            <div class="col-sm-10">
                                <asp:Label CssClass="col-sm-2 control-label txt-left" ID="PasswordLabel" runat="server" Width="150px"></asp:Label>
                                <asp:TextBox ID="PasswordTextBox" Visible="false" Placeholder="Insert your new password" CssClass="form-control" runat="server" TextMode="Password" Height="40px" Width="325px" />
                            </div>
                        </div>
                        <div class="form-group div-left" id="ConfPass" runat="server" visible="false">
                            <label class="col-sm-2 control-label" for="ConfirmPasswordTextBox">Confirm Password</label>
                            <div class="col-sm-2" style="width: 300px">
                                <asp:TextBox ID="ConfirmPasswordTextBox" CssClass="form-control" Placeholder="Confirm your password" runat="server" TextMode="Password" Height="40px" Width="325px" ForeColor="Black" />
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ErrorMessage="Password do not match" ControlToCompare="ConfirmPasswordTextBox" ControlToValidate="PasswordTextBox" Display="Dynamic" ForeColor="Red" Width="320px" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="EmailTextBox">Email</label>
                            <div class="col-sm-10">
                                <asp:Label CssClass="col-sm-2 control-label txt-left" ID="EmailLabel" runat="server" Width="150px"></asp:Label>
                                <asp:TextBox runat="server" Visible="false" ID="EmailTextBox" CssClass="form-control" Placeholder="Insert your email" TextMode="Email" Height="40px" Width="325px" ForeColor="Black" />
                                <asp:RegularExpressionValidator Enabled="false" ID="RegularEmail" runat="server" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ForeColor="Red" ErrorMessage="Please insert a valid email adress" />
                                <asp:Label runat="server" ID="EmailExitentLabel" Visible="false" ForeColor="Red" Text="Email already existent! Please insert another one!" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="GenderButtonList">Gender</label>
                            <div class="col-sm-10">
                                <asp:Label CssClass="col-sm-2 control-label txt-left" ID="GenderLabel" runat="server" Width="150px"></asp:Label>
                                <asp:RadioButtonList Visible="false" ID="GenderButtonList" runat="server">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                    <asp:ListItem Selected="True">Other</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <label class="col-sm-2 control-label" for="DateTextBox">BirthDay</label>
                            <div class="col-sm-10">
                                <asp:Label CssClass="col-sm-2 control-label txt-left" ID="DateLabel" runat="server" Width="150px"></asp:Label>
                                <asp:TextBox ID="DateTextBox" Visible="false" CssClass="form-control" Placeholder="Insert your birthday DD/MM/YYYY" runat="server" Height="40px" Width="325px" ForeColor="Black" />
                                <asp:CompareValidator ID="DateValidator" Enabled="false" Display="Dynamic" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="DateTextBox" ErrorMessage="Please enter a valid date." />
                            </div>
                        </div>
                        <div class="form-group" style="margin-left:50px" runat="server" id="PassCheck" visible="false">
                            <label class="col-sm-2 control-label txt-left" style="width:160px;top:-7px" for="PasswordCheck">Enter password to apply changes</label>
                            <div class="col-sm-8">
                                <asp:TextBox runat="server" TextMode="Password" class="form-control" id="PasswordCheck" placeholder="Current password" Height="40px" Width="325px"/>
                                <asp:Label runat="server" ID="ErrorChange" Visible="false" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="form-group div-left">
                            <div class="col-sm-offset-2 col-sm-10">
                                <button id="Update" visible="false" runat="server" class="btn btn-primary" onserverclick="updateProfile">Update <span class="glyphicon glyphicon-pencil"></span></button>
                                <button id="Cancel" visible="false" runat="server" class="btn btn-default" onserverclick="Cancel_ServerClick">Cancel <span class="glyphicon glyphicon-remove"></span></button>
                            </div>
                        </div>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>
    <div class="footer"></div>
</asp:Content>
