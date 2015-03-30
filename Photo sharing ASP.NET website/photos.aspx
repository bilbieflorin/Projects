<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Photos" AutoEventWireup="true" CodeFile="photos.aspx.cs" Inherits="photos" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="login_content" ContentPlaceHolderID="Body" runat="server">
    <div class="container-fluid down">
        <div class="leftAside"></div>
        <div class="main">
            <div class="box">
                <div style="width: 100%" runat="server" id="Status" visible="false"></div>
                <div class="row" id="Row" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
