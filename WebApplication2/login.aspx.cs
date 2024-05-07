using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class login : Page
    {
        protected Label ErrorMessage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ErrorMessage"] != null)
            {
                ErrorMessage.Text = Session["ErrorMessage"].ToString();
                Session.Remove("ErrorMessage");
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            DatabaseAccess dbAccess = new DatabaseAccess();
            int userId = dbAccess.LoginUser(username, password);

            if (userId != -1)
            {
                Session["username"] = username;
                Session["userId"] = userId.ToString();
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                Session["ErrorMessage"] = "Invalid username or password.";
                Response.Redirect("login.aspx");
            }
        }

    }
}
