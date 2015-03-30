using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using Utilities;


public partial class upload : System.Web.UI.Page
{
    private Hashtable categories;
    private Hashtable albums;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
        {
            if (Functions.numberOfAlbums(Convert.ToInt32(Session["userId"].ToString()), Session["conString"].ToString()) == 0)
            {
                Session["noAlbums"] = true;
                Response.Redirect("~/createalbum.aspx");
            }
            else
            {
                albums = Functions.getAlbums(Session["conString"].ToString(), Convert.ToInt32(Session["userId"].ToString()));
                if (AlbumDropDownList.Items.Count == 1)
                {
                    foreach (DictionaryEntry album in albums)
                    {
                        ListItem option = new ListItem();
                        option.Value = album.Key.ToString();
                        AlbumDropDownList.Items.Insert(AlbumDropDownList.Items.Count, option);
                    }
                }
            }
            categories = Functions.getCategories(Session["conString"].ToString());
            if (CategoryDropdownList.Items.Count == 1)
            {
                foreach (DictionaryEntry category in categories)
                {
                    ListItem option = new ListItem();
                    option.Value = category.Key.ToString();
                    if (option.Value.Equals("Category..."))
                        option.Selected = true;
                    CategoryDropdownList.Items.Insert(CategoryDropdownList.Items.Count, option);
                }
            }
        }
    }

    protected void fileUpload(object sender, EventArgs e)
    {
        string desc = DescriptionTextArea.Text;
        int id = Functions.nextId("photos","photo_id",Session["conString"].ToString());
        string src = "photos\\"+id+PhotoUpload.FileName;
        PhotoUpload.SaveAs(Server.MapPath(src));
        int album = (int)albums[AlbumDropDownList.SelectedValue];
        int category = (int)categories[CategoryDropdownList.SelectedValue];
        DateTime date = DateTime.Now;
        int owner = (int)Session["userId"];
        insertImage(id, src, desc, date, category, album, owner);
        Session["uploaded"] = true;
        Response.Redirect("~/photos.aspx");
    }

    protected void insertImage(int id, string src, string desc, DateTime date, int category, int album, int owner)
    {
        using (SqlConnection connect = new SqlConnection(Session["conString"].ToString()))
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(@"insert into photos values(@id, @src, @desc, @owner, @date, @category, @album)", connect);
            cmd.Parameters.Add(new SqlParameter("@id",id));
            cmd.Parameters.Add(new SqlParameter("@src",src));
            cmd.Parameters.Add(new SqlParameter("@desc",desc));
            cmd.Parameters.Add(new SqlParameter("@owner",owner));
            cmd.Parameters.Add(new SqlParameter("@date",date));
            cmd.Parameters.Add(new SqlParameter("@category", category));
            cmd.Parameters.Add(new SqlParameter("@album",album));
            cmd.ExecuteNonQuery();
        }
    }
}