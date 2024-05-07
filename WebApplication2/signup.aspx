<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="WebApplication2.signup" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Signup</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <header>
        <div class="container">
            <h1>Signup</h1>
        </div>
    </header>
    <div class="login-signup">
        <form id="signupForm" runat="server" onsubmit="return validateSignupForm();">
            <label for="email">Username:</label>
            <input type="text" id="username" runat="server" required />
            <label for="email">Email:</label>
            <input type="email" id="email" runat="server" required />
            <label for="password">Password:</label>
            <input type="password" id="password" runat="server" required />
            <asp:Button CssClass="login-signup-button" id="SignupButton" runat="server" Text="Signup" OnClick="SignupButton_Click"/>
            <asp:Label id="ErrorMessage" runat="server" ForeColor="Red" />
        </form>
    </div>
    <script src="validation.js"></script>
</body>
</html>
