using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Utilities;

public partial class redirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
        {
            int id = Convert.ToInt32(Request["photoid"]);
            Photo p = Functions.getPhoto(id, Session["conString"].ToString());
            image.ImageUrl = p.getSource();
            Info.InnerHtml = "<span class='navy'>" + p.getOwner() + "</span><br/><small class='grey'>" + p.getDate().ToShortDateString() + "</small><br/>"
                             + p.getDescription() + "<hr/>";
            ShowComments(id);
            if (Convert.ToInt32(Session["userId"]) == Functions.getOwnerId(id,Session["conString"].ToString()) || Session["userType"].ToString().Equals("admin"))
                Delete.Visible = true;
            Type cstype = this.GetType();
            ScriptManager.RegisterStartupScript(Comments, cstype, "scriptname", "function toltip(){ $('[data-toggle=\"tooltip\"]').tooltip({placement : 'top'});}toltip();", true);
        }
    }

    protected void DeleteComment_ServerClick(object sender, EventArgs e)
    {
        LinkButton b = (LinkButton)sender;
        int id = Convert.ToInt32(b.ID);
        Functions.deleteComment(id, Session["conString"].ToString());
        Comments.Controls.Clear();
        int photoId = Convert.ToInt32(Request["photoid"]);
        ShowComments(photoId);
    }

    protected void AddComment_ServerClick(object sender, EventArgs e)
    {
        string text = CommentText.Text;
        if (!text.Equals(""))
        {
            int id = (int)Session["userID"];
            int photoId = Convert.ToInt32(Request["photoid"]);
            Functions.inserComment(text, id, photoId, Session["conString"].ToString());
            CommentText.Text = "";
            Comments.Controls.Clear();
            ShowComments(photoId);
        }
    }

    private void ShowComments(int photoId)
    {
        List<Comment> comments = Functions.getComments(photoId, Session["conString"].ToString());
        foreach (Comment c in comments)
        {
            HtmlGenericControl box = new HtmlGenericControl("div");
            HtmlGenericControl com = new HtmlGenericControl("div");
            box.Attributes["class"] = "box-content";
            com.Attributes["class"] = "w95";
            com.InnerHtml = "<span class='navy'>" + c.getUserName() + "</span>" + "&nbsp;&nbsp;&nbsp;&nbsp;" + c.getText() + "<br/><small class='grey'>" + c.getDate().ToShortDateString() + "</small>";
            if (Convert.ToInt32(Session["userId"]) == c.getUserId() ||
                Session["userType"].ToString() == "admin" ||
                Convert.ToInt32(Session["userId"]) == Functions.getOwnerId(photoId, Session["conString"].ToString()))
            {
                LinkButton remove = new LinkButton();
                remove.CssClass = "btn glyphicon glyphicon-remove pull-right little a";
                //data-toggle="tooltip" data-placement="bottom" data-original-title="Edit profile"
                remove.Click += DeleteComment_ServerClick;
                remove.Attributes["data-toggle"] = "tooltip";
                remove.Attributes["data-placement"] = "top";
                remove.Attributes["data-original-title"] = "Remove";
                remove.Visible = true;
                remove.ID = c.getCommId() + "";
                box.Controls.Add(remove);
            }
            box.Controls.Add(com);
            Comments.Controls.Add(box);
        }
        if (comments.Capacity == 0)
            NewComment.Attributes["style"] = "margin-top: 30px";
    }
   
    protected void Delete_ServerClick(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request["photoid"]);
        Photo p = Functions.getPhoto(id,Session["conString"].ToString());
        Functions.deletePhoto(id,Session["conString"].ToString());
       
            File.Delete(Server.MapPath(p.getSource()));
        Session["deleted"] = true;
        Response.Redirect("~/photos.aspx");
    }
}
