<%@ Page Language="C#" AutoEventWireup="true" Title="Album" CodeFile="album.aspx.cs" Inherits="album"  MasterPageFile="~/MasterPage.master"%>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="login_content" ContentPlaceHolderID="Body" runat="server">
    <div class="container-fluid down">
        <div class="leftAside"></div>
        <div class="main">
            <div class="box">
                <div style="width: 100%" class="alert alert-success text-center" runat="server" id="Status" visible="false"></div>
                <div class="panel panel-info no-margin">
                    <div class="panel-heading" >
                        <span id="AlbumName" runat="server"></span>
                        <form runat="server">
                            <asp:LinkButton CssClass="pull-right btn btn-default no-margin a up" OnClick="Edit_ServerClick" runat="server" >Edit <span class="glyphicon glyphicon-wrench"></span></asp:LinkButton>
                        </form>
                    </div>
                    <div class="panel-body">
                        <p id="AlbumDesc" runat="server"></p>
                        <br />
                        <div class="row" id="Row" runat="server"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>