using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Administrator
/// </summary>
public class Administrator
{
    private int adminID;
    private string fname;
    private string lname;
    private string email;
    private string lastUpdatedBy;
    private DateTime lastUpdated;
    private int loginID;
    private int companyID;

    public Administrator(int adminID, string fname, string lname, string Email, string lastUpdatedBy, DateTime lastUpdated, int loginID, int companyID)
    {
        AdminID = adminID;
        Fname = fname;
        Lname = lname;
        Email = email;
        LastUpdatedBy = lastUpdatedBy;
        LastUpdated = lastUpdated;
        LoginID = loginID;
        CompanyID = companyID;
    }

    public int AdminID
    {
        get
        {
            return adminID;
        }
        private set
        {
            adminID = value;
        }
    }

    public string Fname
    {
        get
        {
            return fname;
        }
        private set
        {
            fname = value;
        }
    }

    public string Lname
    {
        get
        {
            return lname;
        }
        private set
        {
            lname = value;
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

    public int LoginID
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
}