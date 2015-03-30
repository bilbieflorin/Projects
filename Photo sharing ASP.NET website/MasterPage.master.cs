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

public partial class MyMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Session["conString"] = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        if (Session["loggedIn"] != null)
        {
            PhotosHyperLink.Visible = true;
            AlbumsHyperLink.Visible = true;
            UploadHyperLink.Visible = true;
            SignUpHyperLink.Visible = false;
            SignInHyperLink.Visible = false;
            LogOutHyperLink.Visible = true;
            UserLabel.Visible = true;
            if(Session["firstName"]==null)
                UserHyperLink.Text =  Session["userName"].ToString();
            else
                UserHyperLink.Text =  Session["firstName"].ToString();
            Type cstype = this.GetType();
            Page.ClientScript.RegisterStartupScript(cstype, "Data-toogle", "<script type='text/javascript'>$(document).ready(function(){$('[data-toggle=\"tooltip\"]').tooltip();});</script>");
            if (Session["birthday"] != null)
                if ((DateTime)Session["birthday"] == DateTime.Now)
                    BDayLabel.Visible = true;
        }
        else
        {
            PhotosHyperLink.Visible = false;
            AlbumsHyperLink.Visible = false;
            UploadHyperLink.Visible = false;
            SignUpHyperLink.Visible = true;
            SignInHyperLink.Visible = true;
            LogOutHyperLink.Visible = false;
            UserLabel.Visible = false;
            BDayLabel.Visible = false;
        }
    }

}