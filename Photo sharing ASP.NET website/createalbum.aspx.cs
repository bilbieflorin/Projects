using System;
using System.Collections;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

public partial class createalbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
        {
            if (Session["noAlbums"] != null)
            {
                NoAlbums.Visible = true;
                Session["noAlbums"] = null;
            }
        }
    }

    protected void create_album(object sender,EventArgs e)
    {
        string name = AlbumNameTextBox.Text;
        string description = DescriptionTextArea.Text;
        createAlbum(name, description);
        Session["albumCreated"] = true;
        Response.Redirect("~/albums.aspx");
    }

    protected void createAlbum(string albumName, string description)
    {
        int id = Functions.nextId("albums","album_id", Session["conString"].ToString());
        using (SqlConnection connect = new SqlConnection(Session["conString"].ToString()))
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(@"insert into albums(album_id, album_name, description, owner_id, creation_day)
                                                          values(@id, @name, @desc, @user, @date)", connect);
            cmd.Parameters.Add(new SqlParameter("@id",id));
            cmd.Parameters.Add(new SqlParameter("@name",albumName));
            cmd.Parameters.Add(new SqlParameter("@desc", description));
            cmd.Parameters.Add(new SqlParameter("@user",Session["userID"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@date",DateTime.Now));
            cmd.ExecuteNonQuery();
        }
    }
}