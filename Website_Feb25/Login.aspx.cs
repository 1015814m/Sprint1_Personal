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
                string commandText = "Select Top 1 UserName, PasswordHash, LoginType, EmpLoginID from [dbo].[EmployeeLogin] where UserName = @UserName";

                SqlCommand select = new SqlCommand(commandText, conn);
                select.Parameters.AddWithValue("@UserName", username);
                SqlDataReader reader = select.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    String pwHash = reader["PasswordHash"].ToString();
                    String user = reader["UserName"].ToString();
                    int loginType = (int)reader["LoginType"];
                    int loginID = (int)reader["EmpLoginID"];
                    
                    

                    bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
                    if (verify)
                    {
                        switch (loginType)
                        {
                            case 1:
                                Session["login"] = 1;
                                if (!enabledEmployee(loginID))
                                {
                                    verify = false;
                                }
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
                        getLoginInfo(loginID, loginType);
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


    protected void ForgotPass_Click(object sender, EventArgs e)
    {
        Response.Redirect("PasswordReset.aspx");
    }

    protected void getLoginInfo(int loginID, int loginType)
    {
        try
        {
            SqlConnection conn = ProjectDB.connectToDB();
            switch (loginType)
            {
                case 1:
                    string empCommand = "select top 1 EmployeeID, FirstName, LastName, Email, LastUpdatedBy, LastUpdated, Points, Enabled, CompanyID, LandingPage, Nickname, " +
                        "UseNickname, UseAnon FROM [dbo].[Employee] WHERE EmpLoginID = @EmpLoginID";
                    SqlCommand select = new SqlCommand(empCommand, conn);
                    select.Parameters.AddWithValue("@EmpLoginID", loginID);
                    SqlDataReader empReader = select.ExecuteReader();

                    if (empReader.HasRows)
                    {
                        empReader.Read();
                        int id = (int)empReader["EmployeeID"];
                        string fname = empReader["FirstName"].ToString();
                        string lname = empReader["LastName"].ToString();
                        string email = empReader["Email"].ToString();
                        string updateBy = empReader["LastUpdatedBy"].ToString();
                        DateTime update = (DateTime)empReader["LastUpdated"];
                        Decimal points = (Decimal)empReader["Points"];
                        string nickname = " ";
                        if (empReader["Nickname"] == DBNull.Value)
                        {
                            nickname = " ";
                        }
                        else
                        {
                            nickname = empReader["Nickname"].ToString();
                        }

                        Boolean enabled = (Boolean)empReader["Enabled"];

                        int companyid = (int)empReader["CompanyID"];
                        int landing = (int)empReader["LandingPage"];
                        Boolean usenick = (Boolean)empReader["UseNickname"];

                        Boolean anon = (Boolean)empReader["UseAnon"];


                        Employee user = new Employee(id, fname, lname, email, updateBy, update, loginID, points, enabled, companyid, landing, usenick, nickname, anon);
                        Session["user"] = user;
                    }
                    break;

                case 2:
                    string adminCommand = "select top 1 AdminID, FirstName, LastName, Email, LastUpdatedBy, LastUpdated, CompanyID from [dbo].[Administrator] where EmpLoginID = @EmpLoginID";
                    SqlCommand adminSelect = new SqlCommand(adminCommand, conn);
                    adminSelect.Parameters.AddWithValue("@EmpLoginID", loginID);
                    SqlDataReader adminReader = adminSelect.ExecuteReader();

                    if(adminReader.HasRows)
                    {
                        adminReader.Read();
                        int id = (int)adminReader["AdminID"];
                        string fname = adminReader["FirstName"].ToString();
                        string lname = adminReader["LastName"].ToString();
                        string email = adminReader["Email"].ToString();
                        string updatedBy = adminReader["LastUpdatedBy"].ToString();
                        DateTime update = (DateTime)adminReader["LastUpdated"];
                        int companyID = (int)adminReader["CompanyID"];

                        Administrator user = new Administrator(id, fname, lname, email, updatedBy, update, loginID, companyID);
                        Session["user"] = user;
                    }
                    break;

                case 3:
                    string vendorCommand = "select top 1 ProviderID, ProviderName, ContactFirstName, ContactLastName, ContactPhoneNum, ContactEmail, LastUpdatedBy, LastUpdated, CompanyID" +
                        " from [dbo].[RewardProvider] where EmpLoginID = @EmpLoginID";
                    SqlCommand vendorSelect = new SqlCommand(vendorCommand, conn);
                    vendorSelect.Parameters.AddWithValue("@EmpLoginID", loginID);
                    SqlDataReader vendorReader = vendorSelect.ExecuteReader();

                    if(vendorReader.HasRows)
                    {
                        vendorReader.Read();
                        int id = (int)vendorReader["ProviderID"];
                        String providerName = vendorReader["ProviderName"].ToString();
                        String fname = vendorReader["ContactFirstName"].ToString();
                        String lname = vendorReader["ContactLastName"].ToString();
                        String phone = vendorReader["ContactPhoneNum"].ToString();
                        String email = vendorReader["ContactEmail"].ToString();
                        String lastUpdatedBy = vendorReader["LastUpdatedBy"].ToString();
                        DateTime lastUpdated = (DateTime)vendorReader["LastUpdated"];
                        int companyID = (int)vendorReader["CompanyID"];

                        RewardProvider user = new RewardProvider(id, providerName, fname, lname, phone, email, lastUpdated, lastUpdatedBy, companyID, loginID);
                        Session["user"] = user;
                    }
                    break;
                default:

                    break;
            }
            conn.Close();

        }
        catch (Exception ex)
        {
            
        }
    }

    protected Boolean enabledEmployee(int id)
    {
        Boolean isEnabled = false;
        try
        {
            string commandText = "select top 1 [Enabled] as Result from [dbo].[Employee] where EmpLoginID = @EmpLoginID";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);
            select.Parameters.AddWithValue("@EmpLoginID", id);

            SqlDataReader reader = select.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                isEnabled = (Boolean)reader["Result"];

            }
            conn.Close();
            
        }
        catch (Exception ex)
        {
            employeeLogin.FailureText = "" + ex;
        }
        return isEnabled;
    }
}