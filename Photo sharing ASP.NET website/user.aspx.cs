using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

public partial class user : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("~/homepage.aspx");
        else
            completeLabels();
    }
    
    protected void Edit_ServerClick(object sender, EventArgs e)
    {
        enableEditing(true);
        int userId = Convert.ToInt32(Session["userId"]);
        string conString = Session["conString"].ToString();
        UserNameTextBox.Text = Functions.getUserName(userId, conString);
        FirstNameTextBox.Text = Functions.getFirstName(userId, conString);
        LastNameTextBox.Text = Functions.getLastName(userId, conString);
        GenderButtonList.SelectedValue = Functions.getGender(userId, conString);
        EmailTextBox.Text = Functions.getEmail(userId, conString);
        DateTextBox.Text = (Functions.getBirthday(userId, conString) == default(DateTime)) ? "" : Functions.getBirthday(userId, conString).ToShortDateString();
    }

    protected void updateProfile(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["userId"]);
        string conString = Session["conString"].ToString();
        if (PasswordCheck.Text.Equals(""))
        {
            ErrorChange.Visible = true;
            ErrorChange.Text = "Please insert your password!";
        }
        else
        {
            string pass = Functions.getPassword(userId,conString);
            string toCheck = PasswordCheck.Text;
            if(pass.Equals(toCheck))
            {
                string userName = UserNameTextBox.Text;
                string email = EmailTextBox.Text;
                if (Functions.findInDatabase("user_name", userName,conString) || userName.Equals(Functions.getUserName(userId,conString)))
                {
                    if (Functions.findInDatabase("email", email, conString) || email.Equals(Functions.getEmail(userId, conString)))
                    {
                        string lName = (LastNameTextBox.Text.CompareTo("") == 0) ? null : LastNameTextBox.Text;
                        string fName = (FirstNameTextBox.Text.CompareTo("") == 0) ? null : FirstNameTextBox.Text;
                        string gender = GenderButtonList.SelectedValue;
                        string password = (PasswordTextBox.Text.Equals(""))?pass:PasswordTextBox.Text;
                        DateTime bDay = (DateTextBox.Text.CompareTo("") != 0) ? Convert.ToDateTime(DateTextBox.Text) : default(DateTime);
                        Functions.updateAccountprotected(userId, userName, password, email, gender, fName, lName,bDay, conString);
                        enableEditing(false);
                        completeLabels();
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
            else
            {
                ErrorChange.Visible = true;
                ErrorChange.Text = "Password do not match!";
            }
        }
    }

    protected void enableEditing(bool b)
    {
        if(!b)
            Edit.Attributes.Remove("disabled");
        else
            Edit.Attributes.Add("disabled", "true");
        Update.Visible = Cancel.Visible = b;
        UserNameLabel.Visible = !b;
        FirstNameLabel.Visible = !b;
        LastNameLabel.Visible = !b;
        GenderLabel.Visible = !b;
        EmailLabel.Visible = !b;
        DateLabel.Visible = !b;
        PasswordLabel.Visible = !b;
        UserNameTextBox.Visible = b;
        FirstNameTextBox.Visible = b;
        LastNameTextBox.Visible = b;
        PasswordTextBox.Visible = b;
        ConfPass.Visible = b;
        GenderButtonList.Visible = b;
        EmailTextBox.Visible = b;
        DateTextBox.Visible = b;
        PassCheck.Visible = b;
        PasswordCompare.Enabled = b;
        RegularEmail.Enabled = b;
        DateValidator.Enabled = b;
    }
    
    protected void Cancel_ServerClick(object sender, EventArgs e)
    {
        enableEditing(false);
    }

    private void cleanPassword()
    {
        PasswordTextBox.Text = "";
        ConfirmPasswordTextBox.Text = "";
    }

    private void completeLabels()
    {
        int userId = Convert.ToInt32(Session["userId"]);
        string conString = Session["conString"].ToString(),
               fName = Functions.getFirstName(userId, conString),
               lName = Functions.getLastName(userId, conString);
        UserNameLabel.Text = Functions.getUserName(userId, conString);
        FirstNameLabel.Text = (fName.Equals("")) ? "not specified" : fName;
        LastNameLabel.Text = (lName.Equals("")) ? "not specified" : lName;
        GenderLabel.Text = Functions.getGender(userId, conString);
        EmailLabel.Text = Functions.getEmail(userId, conString);
        DateLabel.Text = (Functions.getBirthday(userId, conString) == default(DateTime)) ? "not specified" : Functions.getBirthday(userId, conString).ToShortDateString();
        PasswordLabel.Text = "*********";
        ErrorChange.Visible = false;
    }
}