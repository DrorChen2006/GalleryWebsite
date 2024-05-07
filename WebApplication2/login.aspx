<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication2.login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <header>
        <div class="container">
            <h1>Login</h1>
        </div>
    </header>
    <main>
        <div class="login-signup">
            <form id="loginForm" runat="server">
                <label for="username">Username:</label>
                <input type="text" id="username" name="username" required>
                <label for="password">Password:</label>
                <input type="password" id="password" name="password" required>
                <asp:Button CssClass="login-signup-button" id="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
                <asp:Label id="ErrorMessage" runat="server" ForeColor="Red" />
            </form>
        </div>
    </main>
</body>
</html>
