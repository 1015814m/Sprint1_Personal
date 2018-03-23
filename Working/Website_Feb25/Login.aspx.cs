using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;



public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["databaseName"] = "Lab4";
    }

    protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            string username = employeeLogin.UserName;
            string password = employeeLogin.Password;

            e.Authenticated = false;

            SqlConnection conn = ProjectDB.connectToDB();

            if(conn !=  null)
            {
                string commandText = "Select Top 1 UserName, PasswordHash, LoginType from [dbo].[EmployeeLogin] where UserName = @UserName";

                SqlCommand select = new SqlCommand(commandText, conn);
                select.Parameters.AddWithValue("@UserName", username);
                SqlDataReader reader = select.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    String pwHash = reader["PasswordHash"].ToString();
                    String user = reader["UserName"].ToString();
                    int loginType = (int)reader["LoginType"];
                    Session["loggedInAs"] = user;

                    bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
                    if (verify)
                    {
                        switch (loginType)
                        {
                            case 1:
                                Session["login"] = 1;
                                break;
                            case 2:
                                Session["login"] = 2;
                                break;
                            case 3:
                                Session["login"] = 3;
                                break;
                            default:
                                Session["login"] = -1;
                                break;
                        }
                    }
                    e.Authenticated = verify;
                    if (e.Authenticated == true)
                    {
                        getUserInfo(getLoginID(username));

                    }

                }
                conn.Close();
                Session["employeeLoggedIn"] = e.Authenticated.ToString();

            }
            if (e.Authenticated == false)
            {
                employeeLogin.FailureText = "Incorrect Username or Password.";
            }
            
        }
        catch (Exception)
        {

        }
    }


    protected void employeeLogin_LoggedIn(object sender, EventArgs e)
    {
        string url = "Logout.aspx";
        switch ((int)Session["login"])
        {
            case 1:
                url = "HomePage.aspx";
                break;
            case 2:
                url = "Admin.aspx";
                break;
            case 3:
                url = "VendorHome.aspx";
                break;
            case -1:
                url = "Logout.aspx";
                break;
            default:
                break;
        }
        Response.Redirect(url);
    }

    protected void getUserInfo(int empLoginID)
    {
        try
        {
            int count = 0;
            string commandText = "SELECT TOP 1 EmployeeID, FirstName, LastName, Email, LastUpdated, LastUpdatedBy, Points " +
                "FROM [DBO].[EMPLOYEE] WHERE EmpLoginID = @EmpLoginID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);
            select.Parameters.AddWithValue("@EmpLoginID", empLoginID);

            SqlDataReader reader = select.ExecuteReader();


            //if there is data for the user read it
            if(reader.HasRows)
            {
                reader.Read();
                count++;
                int employeeID = (int)reader["EmployeeID"];
                String firstName = reader["FirstName"].ToString();
                String lastName = reader["LastName"].ToString();
                String email = reader["Email"].ToString();
                DateTime lastUpdated = (DateTime)reader["LastUpdated"];
                String lastUpdatedBy = reader["LastUpdatedBy"].ToString();
                Decimal points = (Decimal)reader["Points"];

                Employee user = new Employee(firstName, lastName, email, lastUpdated, lastUpdatedBy, empLoginID, false, points);
                Session["user"] = user;


                //Employee user2 = (Employee)Session["user"];
            }
            else
            {
                reader.Close();
            }
            if(count == 0) 
            {
                //if the user does not exist in the employee table check the administrator table for the employee
                commandText = "SELECT TOP 1 AdminID, FirstName, LastName, Email, LastUpdated, LastUpdatedBy" +
                    " FROM [DBO].[ADMINISTRATOR] WHERE EmpLoginID = @EmpLoginID";
                select = new SqlCommand(commandText, conn);
                select.Parameters.AddWithValue("@EmpLoginID", empLoginID);

                SqlDataReader adminReader = select.ExecuteReader();


                //if the administrator exists then read the data
                
                if (adminReader.HasRows)
                {
                    adminReader.Read();
                    int adminID = (int)adminReader["AdminID"];
                    String firstName = adminReader["FirstName"].ToString();
                    String lastName = adminReader["LastName"].ToString();
                    String email = adminReader["Email"].ToString();
                    DateTime lastUpdated = (DateTime)adminReader["LastUpdated"];
                    String lastUpdatedBy = adminReader["LastUpdatedBy"].ToString();

                    //create an employee object that is specific to the administrator i.e. the admin boolean is true
                    Employee user = new Employee(firstName, lastName, email, lastUpdated, lastUpdatedBy, empLoginID, true);

                    //create a session variable for the user logged in
                    Session["user"] = user;


                }
                else
                {
                    adminReader.Close();
                }
            }

            
            conn.Close();

        }
        catch (Exception ex)
        {
            
            errorMessage.Text += "\n" + ex;

        }
    }

    protected int getLoginID(string userName)
    {
        try
        {
            string commandText = "SELECT TOP 1 EmpLoginID FROM [DBO].[EMPLOYEELOGIN] WHERE UserName = @UserName";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);
            select.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                int empLoginID = (int)reader["EmpLoginID"];
                conn.Close();
                return empLoginID;
            }
            else
            {
                return -1;
            }


        }
        catch (Exception ex)
        {
            errorMessage.Text += "\n" + ex;
            return -1;
        }
    }



    protected void ForgotPass_Click(object sender, EventArgs e)
    {
        Response.Redirect("PasswordReset.aspx");
    }

    protected void getLoginInfo(int loginID, int loginType)
    {
        try
        {
            switch (loginType)
            {
                case 1:
                    string commandText = "select top 1 EmployeeID, FirstName, LastName, Email, LastUpdatedBy, LastUpdated, Points, Enabled, CompanyID, LandingPage, UseNickname, UseAnon FROM [dbo].[Employee] WHERE EmpLoginID = @EmpLoginID";
                    SqlConnection conn = ProjectDB.connectToDB();
                    SqlCommand select = new SqlCommand(commandText, conn);
                    select.Parameters.AddWithValue("@EmpLoginID", loginID);

                    SqlDataReader reader = select.ExecuteReader();

                    if(reader.HasRows)
                    {
                        reader.Read();
                        int id = (int)reader["EmployeeID"];
                        string fname = reader["FirstName"].ToString();
                        string lname = reader["LastName"].ToString();
                        string email = reader["Email"].ToString();
                        string updateBy = reader["LastUpdatedBy"].ToString();
                        DateTime update = (DateTime)reader["LastUpdated"];
                        Decimal points = (Decimal)reader["Points"];
                        
                        if ((int)reader["Enabled"] == 0)
                        {
                            Boolean enabled = false;
                        }
                        else
                        {
                            Boolean enabled = true;
                        }
                        int companyid = (int)reader["CompanyID"];
                        int landing = (int)reader["LandingPage"];
                        if((int)reader["UseNickname"] == 0)
                        {
                            Boolean nickname = false;
                        }
                        else
                        {
                            Boolean nickname = true;
                        }
                        if ((int)reader["UseAnon"] == 0)
                        {
                            Boolean anon = false;
                        }
                        else
                        {
                            Boolean anon = true;
                        }

                        
                    }
                    break;
            }

        }
        catch (Exception)
        {

        }
    }
}