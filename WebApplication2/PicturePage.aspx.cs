using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class PicturePage : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.Literal PicturePageTitle;
        protected global::System.Web.UI.HtmlControls.HtmlInputText NewPictureName;
        protected global::System.Web.UI.HtmlControls.HtmlInputText NewUrlAddress;
        protected global::System.Web.UI.WebControls.Button UpdatePictureButton;
        protected global::System.Web.UI.WebControls.Button DeletePictureButton;

        private DatabaseAccess dbAccess = new DatabaseAccess();
        private int pictureId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pictureId"]))
            {
                pictureId = Convert.ToInt32(Request.QueryString["pictureId"]);
                Picture picture = dbAccess.GetPictureFromId(pictureId);
                Page.Title = picture.GetName();
                PicturePageTitle.Text = picture.GetName();
                int ownerId = picture.GetOwnerId();
                if (Session["userId"] != null && Session["userId"].ToString() == ownerId.ToString())
                {
                    UpdatePictureButton.Visible = true;
                    DeletePictureButton.Visible = true;
                    NewPictureName.Visible = true;
                    NewUrlAddress.Visible = true;
                }
                else
                {
                    UpdatePictureButton.Visible = false;
                    DeletePictureButton.Visible = false;
                    NewPictureName.Visible = false;
                    NewUrlAddress.Visible = false;
                }

                PicturePlaceholder.Controls.Add(new LiteralControl($"<img src='{picture.GetLocation()}' alt='{picture.GetName()}'><div class='overlay'>{picture.GetName()}</div>\n"));
            }
        }

        protected void UpdatePictureButton_Click(Object sender, EventArgs e)
        {
            List<UpdateInfoParameter> updateParameters = new List<UpdateInfoParameter>();

            updateParameters.Add(new UpdateInfoParameter("PICTURE_NAME", Request.Form["NewPictureName"]));
            updateParameters.Add(new UpdateInfoParameter("LOCATION", Request.Form["NewUrlAddress"]));

            dbAccess.UpdateInfo(pictureId, "PICTURES", updateParameters);
            Response.Redirect("HomePage.aspx");
        }

        protected void DeletePictureButton_Click(Object sender, EventArgs e)
        {
            dbAccess.DeletePicture(pictureId);
            Response.Redirect("HomePage.aspx");
        }
    }
}
