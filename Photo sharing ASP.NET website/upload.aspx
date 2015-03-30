<%@ Page Language="C#" AutoEventWireup="true" Title="Upload" MasterPageFile="~/MasterPage.master" CodeFile="upload.aspx.cs" Inherits="upload" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="login_content" ContentPlaceHolderID="Body" runat="server">
    <div class="container down">
        <div class="leftAside"></div>
        <div class="main">
            <div class="box">
                <form class="form-horizontal form" role="form" runat="server">
                    <fieldset>
                        <legend>
                            <p class="text-center">Upload your image</p>
                        </legend>
                        <div class="form-group div-left2">
                            <label class="col-sm-2 control-label space" for="PhotoUpload">Chose a file for upload</label>
                            <div class="col-sm-10">
                                <asp:FileUpload ID="PhotoUpload" CssClass="form-control" runat="server" Height="39px" Width="309px" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="PhotoUpload" ErrorMessage="Please choose an image!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Only jpg/jpeg/png/bmp file is allowed!" ValidationExpression="^.+(.jpg|.jpeg|.png|.bmp)$" ControlToValidate="PhotoUpload" Display="Dynamic" ForeColor="Red"> </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group div-left2">
                            <label for="DescriptionTextArea" class="col-sm-2 control-label">Description</label>
                            <div class="col-sm-2 div">
                                <asp:TextBox ID="DescriptionTextArea" MaxLength="300" placeholder="Insert a desciption for your image" CssClass="form-control ta" TextMode="MultiLine" Rows="6" Wrap="true" runat="server" Height="114px" Width="446px" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="DescriptionTexTArea" ErrorMessage="Please insert a description for your image!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="valInput" ControlToValidate="DescriptionTextArea" ValidationExpression="^[\s\S]{0,300}$" ForeColor="Red" ErrorMessage="Please enter a maximum of 300 characters!" Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group div-left2">
                            <label class="col-sm-2 control-label space left" for="CategoryDropdownList">Choose a category</label>
                            <div class="col-sm-2 div">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="CategoryDropdownList" Height="39px" Width="241px">
                                    <asp:ListItem Selected="true">Choose a category...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator runat="server" ControlToValidate="CategoryDropdownList" ErrorMessage="Please choose a category!" Operator="NotEqual" Display="Dynamic" ForeColor="Red" ValueToCompare="Choose a category..." />
                            </div>
                        </div>
                        <div class="form-group div-left2">
                            <label class="col-sm-2 control-label space left" style="left: -40px" for="AlbumDropDownList">Choose an album</label>
                            <div class="col-sm-2 div">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="AlbumDropDownList" Height="37px" Width="241px">
                                    <asp:ListItem Selected="True">Album...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator runat="server" ControlToValidate="AlbumDropDownList" ErrorMessage="Please choose an album!" Operator="NotEqual" CssClass="validator" Display="Dynamic" ForeColor="Red" ValueToCompare="Album..."></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="form-group div-left2">
                            <div class="col-sm-2">
                                <button id="UploadLinkButton" runat="server" class="btn btn-primary" onserverclick="fileUpload">Upload</button>
                            </div>
                        </div>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
