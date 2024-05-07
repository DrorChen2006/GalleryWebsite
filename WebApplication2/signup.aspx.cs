using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class signup : Page
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

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            string email = Request.Form["email"];
            DatabaseAccess dbAccess = new DatabaseAccess();
            int userId = dbAccess.SignupUser(username, email, password);

            if (userId != -1)
            {
                Session["username"] = username;
                Session["userId"] = userId.ToString();
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                Session["ErrorMessage"] = "Invalid username or password.";
                Response.Redirect("signup.aspx");
            }
        }
    }
}
