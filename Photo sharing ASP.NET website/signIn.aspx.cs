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
using System.Collections.Generic;
using System.IO;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] != null)
            Response.Redirect("~/homepage.aspx");
        else
        {
            HyperLink signIn = (HyperLink)Master.FindControl("SignInHyperLink");
            signIn.Visible = false;
            ErrorLabel.Visible = false;
        }
    }
  
    protected void singIn(object sender,EventArgs e)
    {   string user = UserNameTextBox.Text;
        string password = PasswordTextBox.Text;
        if (check(user,password))
        {
            Session["loggedIn"] = true;
            Session["userName"] = user;
            Response.Redirect("~/homepage.aspx");
        }
        else
            ErrorLabel.Visible = true;
    }
    
    protected bool check(string user, string password)
    {
        using(SqlConnection connect = new SqlConnection((String)Session["conString"]))
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(@"select user_id, user_type, birthday, first_name
                                              from users
                                              where user_name = @user and password = @password",connect);
            cmd.Parameters.Add(new SqlParameter("@user", user.ToLower()));
            cmd.Parameters.Add(new SqlParameter("@password",password));
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Session["userType"] = reader.GetString(1);
                    Session["userId"] = reader.GetInt32(0);
                    if(!reader.IsDBNull(2))
                        Session["birthday"] = reader.GetDateTime(2);
                    if (!reader.IsDBNull(3))
                        Session["firstName"] = reader.GetString(3);
                    return true;
                }
                return false;
            }
        }

    }
}