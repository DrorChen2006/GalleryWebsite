<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumPage.aspx.cs" Inherits="WebApplication2.AlbumPage" %>

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
            <asp:Literal ID="AlbumPageTitle" runat="server"></asp:Literal>
        </div>
        <div class="homepage">
            <form runat="server">
                <input id="PictureName" runat="server" placeholder="Picture Name" />
                <input id="UrlAddress" runat="server" placeholder="Picture URL" />
                <asp:Button CssClass="album-picture-page-button" ID="AddPictureButton" runat="server" Text="Add Picture" OnClick="AddPictureButton_Click" />
                <asp:Button CssClass="album-picture-page-button" ID="DeleteAlbumButton" runat="server" Text="Delete Album" OnClick="DeleteAlbumButton_Click" />
            </form>
        </div>
    </header>
    <div id="image-track" data-mouse-down-at="0" data-prev-percentage="0">
        <asp:PlaceHolder id="PictureSliderPlaceholder" runat="server"></asp:PlaceHolder>
    </div>
    <script src="slider.js"></script>
</body>
