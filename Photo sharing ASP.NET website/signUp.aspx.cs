using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Utilities;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
            HyperLink signUp = (HyperLink)Master.FindControl("SignUpHyperLink");
            signUp.Visible = false;
            UserExistentLabel.Visible = false;
   
    }

    protected void signUp(object sender, EventArgs e)
    {
        string lName = (LastNameTextBox.Text.CompareTo("") == 0) ? null : LastNameTextBox.Text.ToLower();
        string fName = (FirstNameTextBox.Text.CompareTo("") == 0) ? null : FirstNameTextBox.Text.ToLower();
        string gender = GenderButtonList.SelectedValue;
        string userName = UserNameTextBox.Text;
        string password = PasswordTextBox.Text;
        string email = EmailTextBox.Text;
        DateTime bDay = (DateTextBox.Text.CompareTo("") != 0) ? Convert.ToDateTime(DateTextBox.Text) : default(DateTime);
        if (Functions.findInDatabase("user_name", UserNameTextBox.Text,Session["conString"].ToString()))
                {
                    if (Functions.findInDatabase("email", EmailTextBox.Text, Session["conString"].ToString()))
                    {
                        Session["loggedIn"] = true;
                        Session["userName"] = UserNameTextBox.Text;
                        Session["userType"] = "regular";
                        Session["userId"] = createAccount(userName, password, email,gender, fName, lName, bDay);
                        if (DateTextBox.Text.Equals(""))
                            Session["birthday"] = null;
                        else
                            Session["birthday"] = DateTextBox.Text;
                        Response.Redirect("~/homepage.aspx");
                     }
                    else
                    {
                        EmailExitentLabel.Visible = true;
                        cleanPassword();
                    }
                }
                else
                {
                    UserExistentLabel.Visible = true;
                    cleanPassword();
                }
    }

    protected int createAccount(string userName,string password,string email,string gender,string fName,string lName,DateTime bDay)
    {
        int id = Functions.nextId("users", "user_id", Session["conString"].ToString());
        using (SqlConnection connect = new SqlConnection((String)Session["conString"]))
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(@"insert into USERS(USER_ID,USER_NAME,PASSWORD, EMAIL, JOIN_DATE,GENDER, FIRST_NAME, LAST_NAME, BIRTHDAY)
                                                         values(@id, @user    ,@pass   ,@email,@joindate,@gender,@first_name,@last_name,@birthday)", connect);
            
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@user", userName.ToLower()));
            cmd.Parameters.Add(new SqlParameter("@pass", password));
            cleanPassword();
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("@joindate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@gender", gender));
            if (fName != null)
                cmd.Parameters.Add(new SqlParameter("@first_name", fName));
            else
                cmd.Parameters.Add(new SqlParameter("@first_name", (object)DBNull.Value));
            if (lName != null)
                cmd.Parameters.Add(new SqlParameter("@last_name", lName));
            else
                cmd.Parameters.Add(new SqlParameter("@last_name", (object)DBNull.Value));
            if (bDay == default(DateTime))
                cmd.Parameters.Add(new SqlParameter("@birthday", (object)DBNull.Value));
            else
                cmd.Parameters.Add(new SqlParameter("@birthday", bDay));
            cmd.ExecuteNonQuery();
        }
        return id;
    }

    private void cleanPassword()
    {
        PasswordTextBox.Text = "";
        ConfirmPasswordTextBox.Text = "";
    }
}