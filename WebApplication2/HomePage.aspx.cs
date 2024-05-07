using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class HomePage : System.Web.UI.Page
    {
        private DatabaseAccess dbAccess = new DatabaseAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["logout"] == "true")
            {
                Session.Abandon();
                Response.Redirect("HomePage.aspx");
            }

            List<Album> albums = dbAccess.GetAllAlbums();

            AlbumSliderPlaceholder.Controls.Add(new LiteralControl(GenerateAlbumSlider(albums)));
        }

        private string GenerateAlbumSlider(List<Album> albums)
        {
            StringBuilder sliderHtml = new StringBuilder();

            foreach (var album in albums)
            {
                string firstPicturePath = album.GetPictures().Count > 0 ? album.GetPictures()[0].GetLocation() : "default-image-path";
                string albumName = album.GetName();

                sliderHtml.Append($"<div class='slider-item'><a href=\"AlbumPage.aspx?albumId={album.GetAlbumId()}\" draggable=\"false\" ><img class=\"image\" src=\"{firstPicturePath}\" draggable=\"false\" alt='{albumName}'></a><div class='overlay'>{album}</div></div>");
            }

            return sliderHtml.ToString();
        }

        protected void createAlbumButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["AlbumName"]))
            {
                dbAccess.CreateAlbum(Convert.ToInt32(Session["userId"]), Request.Form["AlbumName"]);
                Response.Redirect("HomePage.aspx");
            }
        }
    }
}