using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminApprove : System.Web.UI.Page
{
    private int loginType;
    private Administrator user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeLoggedIn"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Session["employeeLoggedIn"].ToString() != "True")
        {
            Response.Redirect("Login.aspx");
        }
        loginType = (int)Session["login"];
        if (loginType == 1)
        {
            Response.Redirect("HomePage.aspx");
        }
        else if (loginType == 3)
        {
            Response.Redirect("VendorHome.aspx");
        }
        else if (loginType == -1)
        {
            Response.Redirect("Logout.aspx");
        }
        else
        {
            user = (Administrator)Session["user"];
        }
    }
}