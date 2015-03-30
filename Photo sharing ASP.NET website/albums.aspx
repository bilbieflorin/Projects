<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Albums" AutoEventWireup="true" CodeFile="albums.aspx.cs" Inherits="albums" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="login_content" ContentPlaceHolderID="Body" runat="server">
    <div class="container down">
        <div class="leftAside">
            <asp:HyperLink ID="CreateAlbumHyperLink" runat="server" NavigateUrl="~/createalbum.aspx">Create album</asp:HyperLink>
        </div>
        <div class="main">
            <div class="box" id="Box" runat="server">
                <div style="width: 100%" runat="server" id="Status" visible="false"></div>
            </div>
        </div>
    </div>
</asp:Content>
