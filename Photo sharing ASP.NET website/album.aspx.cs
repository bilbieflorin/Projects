using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Utilities;

public partial class album : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
        {
            if (Session["updated"] != null)
            {
                Status.Visible = true;
                Status.InnerText = "Album succesfully updated!";
                Session["updated"] = null;
            }
            int albumId = Convert.ToInt32(Request["albumid"]);
            string conString = Session["conString"].ToString();
            Album al = Functions.getDetails(albumId, conString);
            AlbumName.InnerText = al.getName();
            AlbumDesc.InnerText = al.getDescription();
            List<Photo> photos = Functions.getPhotosByAlbum(albumId, conString);
            if (photos.Count != 0)
            {
                foreach (Photo photo in photos)
                {
                    HtmlGenericControl col = new HtmlGenericControl("div");
                    HtmlGenericControl a = new HtmlGenericControl("a");
                    HtmlGenericControl img = new HtmlGenericControl("img");
                    col.Attributes["class"] = "thumb";
                    a.Attributes["class"] = "img";
                    a.Attributes["href"] = "image.aspx?photoid=" + photo.getId();
                    img.Attributes["src"] = photo.getSource();
                    img.Attributes["class"] = "img";
                    a.Controls.Add(img);
                    col.Controls.Add(a);
                    Row.Controls.Add(col);
                }
            }
            else
            {
                Status.InnerText = "No photos to show!";
                Status.Visible = true;
                Status.Attributes["class"] = "alert alert-info text-center";
                Status.Attributes["style"] = "margin-bottom:0px";
            }
        }
    }
    
    protected void Edit_ServerClick(object sender, EventArgs e)
    {
        int albumId = Convert.ToInt32(Request["albumid"]);
        Response.Redirect("editalbum.aspx?id="+albumId);
    }
}