using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class UpdateUserInfo : System.Web.UI.Page
    {
        protected Label ErrorMessage;
        private DatabaseAccess dbAccess = new DatabaseAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("HomePage.aspx");
            }
            if (Session["ErrorMessage"] != null)
            {
                ErrorMessage.Text = Session["ErrorMessage"].ToString();
                Session.Remove("ErrorMessage");
            }
        }

        protected void updateUserInfoButton_Click(object sender, EventArgs e)
        {
            List<UpdateInfoParameter> updateParameters = new List<UpdateInfoParameter>();

            updateParameters.Add(new UpdateInfoParameter("USERNAME", Request.Form["username"]));
            updateParameters.Add(new UpdateInfoParameter("EMAIL", Request.Form["email"]));
            updateParameters.Add(new UpdateInfoParameter("HASHED_PASSWORD", string.IsNullOrEmpty(Request.Form["password"]) ? "" : dbAccess.ToMd5Hash(Request.Form["password"])));

            if (dbAccess.UpdateInfo(Convert.ToInt32(Request.QueryString["userId"]), "USERS", updateParameters))
            {
                if (!string.IsNullOrEmpty(Request.Form["username"]) &&
                    Convert.ToInt32(Request.QueryString["userId"]) == Convert.ToInt32(Session["userId"]))
                {
                    Session["username"] = Request.Form["username"];
                }
            }
            else
            {
                Session["ErrorMessage"] = "Invalid username, email or password.";
                Response.Redirect("UpdateUserInfo.aspx");
            }

            Response.Redirect("HomePage.aspx");
        }

    }
}
