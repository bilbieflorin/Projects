<%@ Page Language="C#" Title="Edit Album" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editalbum.aspx.cs" Inherits="editAlbum" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
    <div class="container down">
        <div class="leftAside"></div>
        <div class="main">
            <div class="box">
                <p style="width: 100%" class="text-center" runat="server" id="NoAlbums" visible="false"></p>
                <form id="Form1" class="form-horizontal form" role="form" runat="server">
                    <fieldset>
                        <legend class="text-center">
                            Edit album
                        </legend>
                        <div class="form-group div-left2">
                            <label for="DescriptionTextArea" class="col-sm-2 control-label" style="width: 150px; left: -30px;">Album Name</label>
                            <div class="col-sm-2 div">
                                <asp:TextBox ID="AlbumNameTextBox" placeholder="Insert a name for your album" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AlbumNameTextBox" ErrorMessage="Please insert a name for your album!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group div-left2">
                            <label for="DescriptionTextArea" class="col-sm-2 control-label">Description</label>
                            <div class="col-sm-2 div">
                                <asp:TextBox ID="DescriptionTextArea" MaxLength="300" placeholder="Insert a desciption for your album" CssClass="form-control ta" TextMode="MultiLine" Rows="6" Wrap="true" runat="server" Height="114px" Width="446px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DescriptionTexTArea" ErrorMessage="Please insert a description for your album!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="valInput" ControlToValidate="DescriptionTextArea" ValidationExpression="^[\s\S]{0,300}$" ForeColor="Red" ErrorMessage="Please enter a maximum of 300 characters!" Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group div-left2">
                            <div class="col-sm-2">
                                <asp:Button id="Button1" runat="server" class="btn btn-primary" OnClick="Update_ServerClick" Text="Edit album"/>
                            </div>
                        </div>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
