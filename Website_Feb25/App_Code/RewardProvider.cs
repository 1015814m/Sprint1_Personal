using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RewardProvider
/// </summary>
public class RewardProvider
{
    private int providerID;
    private String name;
    private String fname;
    private String lname;
    private String phone;
    private String email;
    private DateTime lastUpdated;
    private String lastUpdatedBy;
    private int companyID;
    private int loginID;
    public RewardProvider(int providerID, String name, String fname, String lname, String phone, String email, DateTime lastUpdated, String lastUpdatedBy)
    {
        ProviderID = providerID;
        Name = name;
        Fname = fname;
        Lname = lname;
        Phone = phone;
        Email = email;
        LastUpdated = lastUpdated;
        LastUpdatedBy = lastUpdatedBy;
    }
    public RewardProvider(int providerID, String name, String fname, String lname, String phone, String email, DateTime lastUpdated, String lastUpdatedBy, int companyID, int loginID)
    {
        ProviderID = providerID;
        Name = name;
        Fname = fname;
        Lname = lname;
        Phone = phone;
        Email = email;
        LastUpdated = lastUpdated;
        LastUpdatedBy = lastUpdatedBy;
        CompanyID = companyID;
        LoginID = loginID;
    }
    public int ProviderID
    {
        get
        {
            return providerID;
        }
        private set
        {
            providerID = value;
        }
    }
    public String Name
    {
        get
        {
            return name;
        }
        private set
        {
            name = value;
        }
    }
    public String Fname
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
    public String Lname
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
    public String Phone
    {
        get
        {
            return phone;
        }
        private set
        {
            phone = value;
        }
    }
    public String Email
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
    public String LastUpdatedBy
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
}