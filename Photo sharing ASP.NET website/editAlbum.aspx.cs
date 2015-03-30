using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

public partial class editAlbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
        {
            int albumId = Convert.ToInt32(Request["id"]);
            string conString = Session["conString"].ToString();
            Album a = Functions.getDetails(albumId,conString);
            if(AlbumNameTextBox.Text.Equals(""))
                AlbumNameTextBox.Text = a.getName();
            if(DescriptionTextArea.Text.Equals(""))
                DescriptionTextArea.Text = a.getDescription(); 
        }
    }
  
    protected void Update_ServerClick(object sender, EventArgs e)
    {
        int albumId = Convert.ToInt32(Request["id"]);
        string conString = Session["conString"].ToString();
        string name = AlbumNameTextBox.Text;
        string desc = DescriptionTextArea.Text;
        Functions.updateAlbum(albumId, name, desc, conString);
        Session["updated"] = true;
        Response.Redirect("album.aspx?albumid="+albumId);
    }
}