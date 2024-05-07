using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class ManageUsersPage : System.Web.UI.Page
    {
        private DatabaseAccess dbAccess = new DatabaseAccess();
        List<User> users;
        protected GridView UsersGridView;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindUsersGridView();
        }

        private void BindUsersGridView()
        {
            users = dbAccess.GetAllUsers();
            UsersGridView.DataSource = users;
            UsersGridView.DataBind();
        }

        protected void UsersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                int userId = users[Convert.ToInt32(e.CommandArgument)].UserId;
                if (userId != 1)
                {
                    if (dbAccess.DeleteUser(userId))
                    {
                        Page_Load(sender, EventArgs.Empty);
                    }
                }
            }
            if (e.CommandName == "UpdateUserInfo")
            {
                int userId = users[Convert.ToInt32(e.CommandArgument)].UserId;
                Response.Redirect($"UpdateUserInfo.aspx?userId={userId}");
            }
        }

    }
}