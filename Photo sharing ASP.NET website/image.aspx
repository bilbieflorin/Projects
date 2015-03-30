<%@ Page Language="C#" AutoEventWireup="true" Title="Photo" MasterPageFile="~/MasterPage.master" CodeFile="image.aspx.cs" Inherits="redirect" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral,
 PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="container-fluid down">
        <form id="Form1" runat="server">
            <div class="leftAside" id="abc" runat="server">
                <button runat="server" class="btn btn-default" role="button" id="Delete" onserverclick="Delete_ServerClick" visible="false">Remove&nbsp;<span class="glyphicon glyphicon-remove"></span></button>
            </div>
            <div class="main" id="ContentPlace" runat="server">
                <div class="panel fb">
                    <asp:Image runat="server" CssClass="full" ID="image" />
                    <div class="panel-body fb">
                        <div runat="server" id="Info"></div>
                        <div>
                            <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="updatePanel">
                                <ContentTemplate>
                                    <asp:Panel ID="Comments" runat="server">
                                    </asp:Panel>
                                    <asp:Panel ID="NewComment" runat="server">
                                        <table class="full">
                                            <tr>
                                                <td class="w90" id="text">
                                                    <asp:TextBox runat="server" CssClass="form-control ta" TextMode="MultiLine" Rows="4" ID="CommentText"></asp:TextBox>
                                                </td>
                                                <td id="button">
                                                    <asp:Button Style="float: left" CssClass="btn btn-primary" runat="server" OnClick="AddComment_ServerClick" Text="Post" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
