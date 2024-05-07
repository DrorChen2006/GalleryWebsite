<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateUserInfo.aspx.cs" Inherits="WebApplication2.UpdateUserInfo" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Update User Info</title>
    <link rel="stylesheet" href="styles.css">
</head>
    <% if (Convert.ToInt32(Session["userId"]) == Convert.ToInt32(Request.QueryString["userId"])
      || Convert.ToInt32(Session["userId"]) == 1)
        { %>
<body>
    <header>
        <div class="container">
            <h1>Update Your Info</h1>
            <p>Here you can update your username, email, and password.</p>
        </div>
    </header>
    <div class="login-signup">
        <form id="updateUserInfoForm" runat="server" onsubmit="return validateUpdateInfoForm();">
            <label for="username">Username:</label>
            <input type="text"  placeholder="New Username" id="username" runat="server" name="username">
            <label for="email">Email:</label>
            <input type="email" placeholder="New Email" id="email" runat="server" name="email">
            <label for="password">Password:</label>
            <input type="password" placeholder="New Password" id="password" runat="server" name="password">
            <asp:Button CssClass="login-signup-button" id="updateUserInfoButton" runat="server" Text="Update Info" OnClick="updateUserInfoButton_Click" />
            <asp:Label id="ErrorMessage" runat="server" ForeColor="Red" />
        </form>
    </div>
    <script src="validation.js"></script>
</body>
    <% } %>
</html>
