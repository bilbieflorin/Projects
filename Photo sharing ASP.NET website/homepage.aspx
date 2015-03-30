<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="homepage.aspx.cs" Inherits="homepage" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="container down">
        <div class="leftAside"></div>
        <div class="main" id="MainContainer" runat="server">
            <div class="jumbotron box" runat="server" visible="true" id="jumbotron">
                <h1>Welcome to PhotoSharing!</h1>
                <p>Create your account today and share your pictures with others!</p>
                <p><a class="btn btn-primary btn-lg" href="signup.aspx" role="button">Sing up!</a>&nbsp;&nbsp;&nbsp;&nbsp;If already have an account &nbsp;&nbsp;&nbsp;<a class="btn btn-primary btn-lg" href="signin.aspx" role="button">Sing in!</a></p>
            </div>
            <div class="container box" id="SeachForm" visible="false" runat="server">
                <form runat="server" class="form-inline" style="margin-left: 30px" role="search">
                    <div class="form-group full">
                        <input type="text" id="SearchTextBox" style="width: 40%" class="form-control" runat="server" placeholder="Search" />
                        <asp:DropDownList runat="server" Style="width: 45%" CssClass="form-control" ID="CategoryDropdownList">
                            <asp:ListItem>Choose a category...</asp:ListItem>
                        </asp:DropDownList>
                        <button class="btn btn-primary" runat="server" onserverclick="Search_ServerClick"><span class="glyphicon glyphicon-search"></span>Search</button>
                    </div>
                </form>
                <div class="alert alert-info text-center full nup-down" visible="false" runat="server" id ="Status">No photos to show!</div>
            </div>
        </div>
    </div>
</asp:Content>
