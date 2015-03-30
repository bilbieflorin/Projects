using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Utilities;

public partial class photos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
        {   
            int userId = Convert.ToInt32(Session["userId"]);
            string conString = Session["conString"].ToString();
            if (Session["uploaded"] != null)
            {
                Status.Visible = true;
                Status.Attributes["class"] = "alert alert-success text-center";
                Status.Attributes["role"] = "alert";
                Status.InnerText = "Picture succesfully uploaded!";
                Session["uploaded"] = null;
            }
            if (Session["deleted"] != null)
            {
                Status.Visible = true;
                Status.Attributes["class"] = "alert alert-success text-center";
                Status.Attributes["role"] = "alert";
                Status.InnerText = "Picture succesfully deleted!";
                Session["deleted"] = null;
            }

            List<Photo> photos = Functions.getPhotosByUser(userId, conString);
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
}