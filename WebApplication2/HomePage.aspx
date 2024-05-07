<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebApplication2.HomePage" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Gallery</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <header>
        <div class="container">
            <h1>Welcome to My Gallery Site</h1>
            <p>Where you can post pictures and create albums!</p>
        </div>
        <div class="homepage">
            <% if (Session["username"] != null)
                { %>
            <form action="HomePage.aspx" method="post">
                <p>Hello <span style="color: green; font-family: 'Arial';"><%= Session["username"] %></span>!</p>
                <input type="hidden" name="logout" value="true" />
                <button type="submit">Logout</button>
            </form>
            <% if (Convert.ToInt32(Session["userId"]) == 1)
                { %>
            <form action="ManageUsersPage.aspx">
                <button type="submit">Manage Users</button>
            </form>
            <% } %>
            <form action='<%="UpdateUserInfo.aspx?userId=" + Session["userId"].ToString()%>' method="post">
                <button type="submit" id="UpdateUserInfoButton">Update User Info</button>
            </form>
            <form runat="server">
                <input id="AlbumName" runat="server" placeholder="Album Name"></>
                <asp:Button CssClass="create-album-button" ID="createAlbumButton" runat="server" Text="Create new album" OnClick="createAlbumButton_Click" />
            </form>
            <% }
                else
                { %>
            <form action="login.aspx" method="post">
                <button type="submit">Login</button>
            </form>
            <form action="signup.aspx" method="post">
                <button type="submit">Signup</button>
            </form>
            <% } %>
        </div>
    </header>
    <div id="image-track" data-mouse-down-at="0" data-prev-percentage="0">
        <asp:PlaceHolder id="AlbumSliderPlaceholder" runat="server"></asp:PlaceHolder>
    </div>
    <script src="slider.js"></script>
</body>
</html>
