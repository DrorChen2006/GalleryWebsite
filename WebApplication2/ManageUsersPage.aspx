<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsersPage.aspx.cs" Inherits="WebApplication2.ManageUsersPage" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Manage Users</title>
    <link rel="stylesheet" href="styles.css">
</head>
<%if (Convert.ToInt32(Session["userId"]) == 1)
    { %>
<body>
    <header>
        <div class="container">
            <h1>Manage Users</h1>
        </div>
    </header>
    <main>
        <div>
            <form runat="server">
                <asp:GridView id="UsersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="userId" OnRowCommand="UsersGridView_RowCommand" CssClass="user-grid">
                    <Columns>
                        <asp:BoundField DataField="userId" HeaderText="User Id" ReadOnly="True" />
                        <asp:BoundField DataField="username" HeaderText="Name" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="hashedPassword" HeaderText="Hashed Password" />
                        <asp:ButtonField CommandName="DeleteUser" Text="Delete User"/>
                        <asp:ButtonField CommandName="UpdateUserInfo" Text="Update User Info"/>
                    </Columns>
                </asp:GridView>
            </form>
        </div>
    </main>
</body>

<% } %>

</html>
