using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Employee
/// </summary>
/// 

public class Employee
{
    private int employeeID;
    private string firstName;
    private string lastName;
    private string email;
    private DateTime lastUpdated;
    private string lastUpdatedBy;
    private int empLoginID;
    private DateTime lastLogin;
    private Boolean admin;
    private Decimal points;
    private int loginID;
    private Boolean enabled;
    private int companyID;
    private int landing;
    private string nickname;
    private Boolean usenick;
    private Boolean anon;
    /// <summary>
    /// This constructor is used for initializing employees at login (NOT CREATION OF EMPLOYEES)
    /// This includes a lastLogin
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="mi"></param>
    /// <param name="DOB"></param>
    /// <param name="email"></param>
    /// <param name="lastUpdatedBy">The administrators name</param>
    public Employee(string firstName, string lastName, string email, DateTime lastUpdated, string LastUpdatedBy, int empLoginID, Boolean admin, Decimal points)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        LastUpdated = lastUpdated;
        LastUpdatedBy = lastUpdatedBy;
        EmpLoginID = empLoginID;
        LastLogin = DateTime.Now;
        Admin = false;
        Points = points;
    }
    /// <summary>
    /// This constructor is used for creating admins at login
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="lastUpdated"></param>
    /// <param name="LastUpdatedBy"></param>
    /// <param name="empLoginID"></param>
    /// <param name="admin">For anything using regular employees make this false</param>
    public Employee(string firstName, string lastName, string email, DateTime lastUpdated, string LastUpdatedBy, int empLoginID, Boolean admin)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        LastUpdated = lastUpdated;
        LastUpdatedBy = lastUpdatedBy;
        EmpLoginID = empLoginID;
        LastLogin = DateTime.Now;
        Admin = admin;
    }

    public Employee(string firstName, string lastName, string email, DateTime lastUpdated, string lastUpdatedBy)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        LastUpdated = lastUpdated;
        LastUpdatedBy = lastUpdatedBy;
    }

    public Employee(int employeeID, string fname, string lname, string email, string lastUpdatedBy, DateTime lastUpdated, int loginID, Decimal points, Boolean enabled, int companyID, int landing,
        Boolean usenick, string nickname, Boolean anon)
    {
        EmployeeID = employeeID;
        FirstName = fname;
        LastName = lname;
        Email = email;
        LastUpdatedBy = lastUpdatedBy;
        LastUpdated = lastUpdated;
        Login = loginID;
        Points = points;
        Enabled = enabled;
        CompanyID = companyID;
        Landing = landing;
        Nickname = nickname;
        UseNickname = usenick;
        Anon = anon;
    }

    public Employee()
    {

    }
    public int EmployeeID
    {
        get
        {
            return employeeID;
        }
        private set
        {
            employeeID = value;
        }
    }
    public string FirstName
    {
        get
        {
            return firstName;
        }
        private set
        {
            firstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return lastName;
        }
        private set
        {
            lastName = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }
        private set
        {
            email = value;
        }
    }

    public DateTime LastUpdated
    {
        get
        {
            return lastUpdated;
        }
        private set
        {
            lastUpdated = value;
        }
    }

    public string LastUpdatedBy
    {
        get
        {
            return lastUpdatedBy;
        }
        private set
        {
            lastUpdatedBy = value;
        }
    }

    public int EmpLoginID
    {
        get
        {
            return empLoginID;
        }
        private set
        {
            empLoginID = value;
        }
    }

    public DateTime LastLogin
    {
        get
        {
            return lastLogin;
        }
        private set
        {
            lastLogin = value;
        }
    }

    public Boolean Admin
    {
        get
        {
            return admin;
        }
        private set
        {
            admin = value;
        }
    }

    public Decimal Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }

    public Boolean Enabled
    {
        get
        {
            return enabled;
        }
        private set
        {
            enabled = value;
        }
    }

    public int CompanyID
    {
        get
        {
            return companyID;
        }
        private set
        {
            companyID = value;
        }
    }

    public int Landing
    {
        get
        {
            return landing;
        }
        private set
        {
            landing = value;
        }
    }

    public string Nickname
    {
        get
        {
            return nickname;
        }
        private set
        {
            nickname = value;
        }
    }

    public Boolean UseNickname
    {
        get
        {
            return usenick;
        }
        private set
        {
            usenick = value;
        }
    }

    public Boolean Anon
    {
        get
        {
            return anon;
        }
        private set
        {
            anon = value;
        }
    }

    public int Login
    {
        get
        {
            return loginID;
        }
        private set
        {
            loginID = value;
        }
    }




        





}