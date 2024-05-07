using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class AlbumPage : System.Web.UI.Page
    {
        private DatabaseAccess dbAccess = new DatabaseAccess();
        private int albumId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["albumId"]))
            {
                this.albumId = Convert.ToInt32(Request.QueryString["albumId"]);
                Album album = dbAccess.GetAlbumFromId(albumId);
                Page.Title = album.GetName();
                AlbumPageTitle.Text = album.GetName();
                int ownerId = album.GetOwnerId();
                if (Session["userId"] != null && Convert.ToInt32(Session["userId"]) == ownerId)
                {
                    AddPictureButton.Visible = true;
                    DeleteAlbumButton.Visible = true;
                    PictureName.Visible = true;
                    UrlAddress.Visible = true;
                }
                else
                {
                    AddPictureButton.Visible = false;
                    DeleteAlbumButton.Visible = false;
                    PictureName.Visible = false;
                    UrlAddress.Visible = false;
                }
                if (!IsPostBack)
                {
                    PictureSliderPlaceholder.Controls.Add(new LiteralControl(GeneratePictureSlider(album)));
                }
            }
        }

        private string GeneratePictureSlider(Album album)
        {
            StringBuilder sliderHtml = new StringBuilder();

            foreach (var picture in album.GetPictures())
            {
                sliderHtml.Append($"<div class='slider-item'><a href=\"PicturePage.aspx?pictureId={picture.GetId()}\" draggable=\"false\" ><img class=\"image\" src=\"{picture.GetLocation()}\" draggable=\"false\" alt='{picture.GetName()}'></a><div class='overlay'>{picture.GetName()}</div></div>");
            }

            return sliderHtml.ToString();
        }

        protected void AddPictureButton_Click(object sender, EventArgs e)
        {
            string pictureName = Request.Form["PictureName"];
            string pictureUrl = Request.Form["UrlAddress"];

            if (pictureName != "" &&  pictureUrl != "")
            {
                dbAccess.CreatePicture(albumId, pictureName, pictureUrl);
                Response.Redirect("HomePage.aspx");
            }
        }

        protected void DeleteAlbumButton_Click(object sender, EventArgs e)
        {
            dbAccess.DeleteAlbum(albumId);
            Response.Redirect("HomePage.aspx");
        }
    }
}
