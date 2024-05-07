<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PicturePage.aspx.cs" Inherits="WebApplication2.PicturePage" %>

<!DOCTYPE html>
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title></title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
</head>
<body>
    <header>
        <div style="font-size: 60px; font: bold; font-family: Arial">
            <asp:Literal ID="PicturePageTitle" runat="server"></asp:Literal>
        </div>
        <div class="homepage">
            <form runat="server">
                <input id="NewPictureName" runat="server" placeholder="New Picture Name" />
                <input id="NewUrlAddress" runat="server" placeholder="New Picture URL" />
                <asp:Button CssClass="album-picture-page-button" ID="UpdatePictureButton" runat="server" Text="Update Picture" OnClick="UpdatePictureButton_Click" />
                <asp:Button CssClass="album-picture-page-button" ID="DeletePictureButton" runat="server" Text="Delete Picture" OnClick="DeletePictureButton_Click" />
            </form>
        </div>
    </header>
    <div class="picture-page-picture">
        <asp:PlaceHolder id="PicturePlaceholder" runat="server"></asp:PlaceHolder>
    </div>
</body>
