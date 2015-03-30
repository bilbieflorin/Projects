using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Utilities;

public partial class albums : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
        {   
            int userId = Convert.ToInt32(Session["userId"].ToString());
            string conString = Session["conString"].ToString();
            if (Session["albumCreated"] != null)
            {
                Status.Visible = true;
                Status.Attributes["class"] = "alert alert-success text-center";
                Status.Attributes["role"] = "alert";
                Status.InnerText = "Album succesfully created!";
                Session["albumCreated"] = null;
            }
            if (Functions.numberOfAlbums(userId, conString) == 0)
            {
                Status.Attributes["class"] = "alert alert-info text-center";
                Status.Attributes["style"] = "margin-bottom:0px";
                Status.Visible = true;
                Status.InnerText = "No albums to show!";
            }
            else
            {
                List<Album> albums = Functions.getAlbums(userId, conString);
                foreach (Album album in albums)
                {
                    HtmlGenericControl panel = new HtmlGenericControl("div");
                    HtmlGenericControl heading = new HtmlGenericControl("div");
                    HtmlGenericControl body = new HtmlGenericControl("div");
                    HtmlGenericControl a = new HtmlGenericControl("a");
                    a.Attributes["href"] = "album.aspx?albumid="+album.getId();
                    a.Attributes["class"] = "no-dec";
                    panel.Attributes["class"] = "panel panel-info";
                    heading.Attributes["class"] = "panel-heading";
                    body.Attributes["class"] = "panel-body";
                    heading.InnerText = album.getName();
                    string desc = album.getDescription();
                    body.InnerText = desc.Substring(0,Math.Min(100,desc.Length))+"...";
                    a.Controls.Add(panel);
                    panel.Controls.Add(heading);
                    panel.Controls.Add(body);
                    Box.Controls.Add(a);
                }
            }
        }
    }
}