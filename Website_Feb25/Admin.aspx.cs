using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin : System.Web.UI.Page
{
    int loginType;
    Administrator user;
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
        ReceivingTop();
        GivingTop();
    }

    protected void ReceivingTop()
    {
        try
        {

            SqlConnection conn = ProjectDB.connectToDB();
            string toprec = "  Select TOP 5(SUM(PointsAmount)) as TotalPoints, RecEmployee FROM Achievement WHERE CompanyID = @CompanyID GROUP BY RecEmployee ORDER BY TotalPoints DESC;";
            System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand(toprec, conn);
            select.Parameters.AddWithValue("@CompanyID", user.CompanyID);

            SqlDataReader reader = select.ExecuteReader();
            int points = 0;
            int recEmployee = 0;
            while (reader.Read())
            {

                points = (int)reader["TotalPoints"];
                recEmployee = (int)reader["RecEmployee"];
                TopRecieving.Text += "Employee Name: " + findEmployeeName(recEmployee) + Environment.NewLine + "Total Points Recieved:" + points + Environment.NewLine + Environment.NewLine;
            }
            conn.Close();
        }
        catch (Exception)
        {

        }
    }

    protected void GivingTop()
    {
        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            string toprec = "  Select TOP 5(SUM(PointsAmount)) as TotalPoints, EmployeeID FROM Achievement WHERE CompanyID = @CompanyID GROUP BY EmployeeID ORDER BY TotalPoints DESC;";
            System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand(toprec, conn);
            select.Parameters.AddWithValue("@CompanyID", user.CompanyID);

            SqlDataReader reader = select.ExecuteReader();
            int points = 0;
            int givEmployee = 0;
            while (reader.Read())
            {

                points = (int)reader["TotalPoints"];
                givEmployee = (int)reader["EmployeeID"];
                TopGiving.Text += "Employee Name: " + findEmployeeName(givEmployee) + Environment.NewLine + "Total Points Given:" + points + Environment.NewLine + Environment.NewLine;
            }
            conn.Close();
        }
        catch (Exception)
        {

        }
    }

    protected string findEmployeeName(int id)
    {
        string name = "";
        try
        {
            string commandText = "SELECT TOP 1 [FirstName],[LastName] FROM [dbo].[Employee] WHERE [EmployeeID] = @EmployeeID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@EmployeeID", id);

            SqlDataReader reader = select.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                string firstName = reader["FirstName"].ToString();
                string lastName = reader["LastName"].ToString();
                name = firstName + " " + lastName;
            }
            conn.Close();
            return name;
        }
        catch (Exception ex)
        {
            TopRecieving.Text += " " + ex;
            return name;
        }
    }


}