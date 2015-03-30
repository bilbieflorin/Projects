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
using System.Collections.Generic;
using System.IO;
using Utilities;

public partial class homepage : System.Web.UI.Page
{
     Hashtable categories;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] != null)
        {
            jumbotron.Visible = false;
            SeachForm.Visible = true;
            categories = Functions.getCategories(Session["conString"].ToString());
            if(CategoryDropdownList.Items.Count == 1)
                foreach (DictionaryEntry category in categories)
                {
                    ListItem option = new ListItem();
                    option.Value = category.Key.ToString();
                    CategoryDropdownList.Items.Insert(CategoryDropdownList.Items.Count, option);
                }
            if (!Page.IsPostBack)
            {
                CategoryDropdownList.SelectedValue = Request["category"];
                SearchTextBox.Value = Request["query"];
            }
            Session["search"] = Request["search"];
            if (Session["search"] == null)
            {
                List<Photo> photos = Functions.getPhotos(Session["conString"].ToString());
                ShowPhotos(photos);
            }
            else
            {
                ShowPhotos((List<Photo>)Session["result"]);
            }
        }
    }

    protected void Search_ServerClick(object sender, EventArgs e)
    {
        string query = SearchTextBox.Value;
        int categoryId = (CategoryDropdownList.SelectedValue.Equals("Choose a category..."))?0:(int)categories[CategoryDropdownList.SelectedValue];
        string category = CategoryDropdownList.SelectedValue;
        SearchTextBox.Value = query + categoryId;
        List<Photo> l;
        if(categoryId == 0)
            l =  Functions.search(query, Session["conString"].ToString());
        else
            l = Functions.search(query, categoryId, Session["conString"].ToString());
        Session["result"] = l;
        Response.Redirect("homepage.aspx?search=true&query=" + query + "&category=" + category);
    }

    protected void ShowPhotos(List<Photo> photos)
    {
        if (photos.Count > 0)
        {
            foreach (Photo photo in photos)
            {
                HtmlGenericControl div = new HtmlGenericControl("div"),
                                   panel = new HtmlGenericControl("div"),
                                   head = new HtmlGenericControl("div"),
                                   small = new HtmlGenericControl("small"),
                                   thumbnail = new HtmlGenericControl("div"),
                                   image = new HtmlGenericControl("img"),
                                   link = new HtmlGenericControl("a");
                div.Attributes["class"] = "box container";
                panel.Attributes["class"] = "panel no-margin";
                head.Attributes["class"] = "panel-heading";
                head.InnerHtml = "<span class='navy'>" + photo.getOwner() + "</span><br/>";
                small.Attributes["class"] = "grey";
                small.InnerText = photo.getDate().ToShortDateString();
                thumbnail.Attributes["class"] = "panel-thumbnail";
                image.Attributes["src"] = photo.getSource();
                image.Attributes["class"] = "img-responsive";
                link.Attributes["href"] = "image.aspx?photoid=" + photo.getId();
                link.Controls.Add(image);
                thumbnail.Controls.Add(link);
                head.Controls.Add(small);
                panel.Controls.Add(head);
                panel.Controls.Add(thumbnail);
                div.Controls.Add(panel);
                MainContainer.Controls.Add(div);
            }
        }
        else
            Status.Visible = true;
    }
    protected void addLike(object sender, EventArgs e)
    {
        HtmlGenericControl span = (HtmlGenericControl)sender;
        span.InnerText += 1; 
    }
}