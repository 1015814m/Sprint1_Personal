﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using database;
using System.Data.SqlClient;

public partial class AdminRewards : System.Web.UI.Page
{
    static Employee user;
    RewardItem item;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        if (Session["employeeLoggedIn"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Session["employeeLoggedIn"].ToString() != "True")
        {
            Response.Redirect("Login.aspx");
        }

        user = (Employee)Session["user"];

        if (user.Admin != true)
        {
            Response.Redirect("HomePage.aspx");
        }

        //Label9.Text += user.FirstName + " " + user.LastName;

        if (!IsPostBack)
        {
            //Load the Provider Names into the dropdown on page load
            try
            {
                SqlConnection conn = ProjectDB.connectToDB();
                string commandText = "select ProviderName from [dbo].[RewardProvider]";
                System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand(commandText, conn);
                txtProvider.DataSource = insert.ExecuteReader();
                txtProvider.DataTextField = "ProviderName";
                txtProvider.DataBind();
                txtProvider.Items.Insert(0, "Select");
                conn.Close();
            }
            //Shows an error message if there is a problem connecting to the database
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not load Provider Names from the Database')", true);
            }
            //Load the categories into the drop down on page load
            try
            {
                SqlConnection conn = ProjectDB.connectToDB();
                string commandText = "select Description from [dbo].[RewardCategory]";
                System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand(commandText, conn);
                txtCategory.DataSource = insert.ExecuteReader();
                txtCategory.DataTextField = "Description";
                txtCategory.DataBind();
                txtCategory.Items.Insert(0, "Select");
                conn.Close();
            }
            //Shows an error message if there is a problem connecting to the database
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not load Categories from the Database')", true);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean check = true;
            //validation that all entries are filled in
            if (txtCategory.SelectedValue == "Select")
            {
                check = false;
                lblError.Visible = true;
                lblErrorCategory.Visible = true;
                lblError.Text = "*Please select a Reward Category";
                txtCategory.Focus();
            }
            if (txtProvider.SelectedValue == "Select")
            {
                check = false;
                lblError.Visible = true;
                lblErrorProvider.Visible = true;
                lblError.Text = "*Please select a Reward Provider";
                txtProvider.Focus();
            }
            if (txtQuantity.Text == "")
            {
                check = false;
                lblError.Visible = true;
                lblErrorQuantity.Visible = true;
                lblError.Text = "*Please enter a Reward Quantity";
                txtQuantity.Focus();
            }
            if (txtEndDate.Text == "")
            {
                check = false;
                lblError.Visible = true;
                lblErrorEnd.Visible = true;
                lblError.Text = "*Please enter a valid Reward End Date";
                txtEndDate.Focus();
            }
            if (txtStartDate.Text == "")
            {
                check = false;
                lblError.Visible = true;
                lblErrorStart.Visible = true;
                lblError.Text = "*Please enter a valid Reward Start Date";
                txtStartDate.Focus();
            }
            if (txtPrice.Text == "")
            {
                check = false;
                lblError.Visible = true;
                lblErrorPrice.Visible = true;
                lblError.Text = "*Please enter a Reward Price";
                txtPrice.Focus();
            }
            if (txtDescription.Text == "")
            {
                check = false;
                lblError.Visible = true;
                lblErrorDescription.Visible = true;
                lblError.Text = "*Please enter a Reward Description";
                txtDescription.Focus();
            }
            if (txtName.Text == "")
            {
                check = false;
                lblError.Visible = true;
                lblErrorName.Visible = true;
                lblError.Text = "*Please enter a Reward Name";
                txtName.Focus();
            }

            if (txtStartDate.Text != "")
            {
                if (checkDate(txtStartDate.Text))
                {
                    
                }
                else
                {
                    check = false;
                    lblError.Visible = true;
                    lblErrorName.Visible = true;
                    lblError.Text = "Please enter a valid start date.";
                }
            }


            if (txtEndDate.Text != "")
            {
                if (checkDate(txtEndDate.Text))
                {
                    if (checkDate(txtStartDate.Text))
                    {
                        if((DateTime.Parse(txtStartDate.Text) >= DateTime.Now))
                        { 
                            if (!(DateTime.Parse(txtEndDate.Text) <= DateTime.Now))
                            {
                                if (!(DateTime.Parse(txtStartDate.Text) >= DateTime.Parse(txtEndDate.Text)))
                                {

                                }
                                else
                                {
                                    check = false;
                                    lblError.Visible = true;
                                    lblErrorStart.Visible = true;
                                    lblErrorEnd.Visible = false;
                                    lblError.Text = "The start date must be before the end date.";
                                }
                            }
                            else
                            {
                                check = false;
                                lblError.Visible = true;
                                lblErrorEnd.Visible = false;
                                lblError.Text = "The end date must be some date in the future.";
                            }
                        }
                        else
                        {
                            check = false;
                            lblError.Visible = true;
                            lblErrorStart.Visible = true;
                            lblError.Text = "The start date must be today or in the future.";
                        }
                    }
                    else
                    {
                        check = false;
                        lblError.Visible = true;
                        lblErrorStart.Visible = true;
                        lblError.Text = "Please enter a valid value for start date.";
                    }
                }
                else
                {
                    check = false;
                    lblError.Visible = true;
                    lblErrorEnd.Visible = true;
                    lblError.Text = "Please enter a valid value for end date.";
                }
            }
            else
            {
                check = false;
                lblError.Visible = true;
                lblErrorName.Visible = true;
                lblError.Text = "Please enter a date.";
            }

            if (check)
            {
                //calls the method to send it to the database
                sendItem();
                lblError.Visible = true;
                lblError.Text = "Reward added successfully!";
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reward was not successfully added. Please ensure all fields are valid')", true);
        }
    }

    public void createItem()
    {
        String name = txtName.Text;
        String description = txtDescription.Text;
        Decimal price = Decimal.Parse(txtPrice.Text);
        DateTime start = Convert.ToDateTime(txtStartDate.Text);
        DateTime end = Convert.ToDateTime(txtEndDate.Text);
        int quantity = int.Parse(txtQuantity.Text);
        DateTime updated = DateTime.Now;
        String updatedBy = user.FirstName + " " + user.LastName;
        item = new RewardItem(name, description, price, start, end, quantity, updated, updatedBy);
        //Label9.Text += item.Name + " " + item.Description + " " + item.Price + " " + item.StartDate + " " + item.EndDate + " " + item.Quantity + " " + item.LastUpdated + " " + item.LastUpdatedBy + "<br/>";
    }
     
    public void sendItem()
    {
        try
        {
            //calls the method to create the item
            createItem();
            SqlConnection conn = ProjectDB.connectToDB();
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
            insert.Connection = conn;

            insert.CommandText = "insert into [dbo].[RewardItem] values (@name, @description, @price, @startdate, @enddate, @quantity, @lastupdatedby, @lastupdated, @providerid, @categoryid)";
            insert.Parameters.AddWithValue("@name", item.Name);
            insert.Parameters.AddWithValue("@description", item.Description);
            insert.Parameters.AddWithValue("@price", item.Price);
            insert.Parameters.AddWithValue("@startdate", item.StartDate);
            insert.Parameters.AddWithValue("@enddate", item.EndDate);
            insert.Parameters.AddWithValue("@quantity", item.Quantity);
            insert.Parameters.AddWithValue("@lastupdatedby", item.LastUpdatedBy);
            insert.Parameters.AddWithValue("@lastupdated", item.LastUpdated);
            insert.Parameters.AddWithValue("@providerid", findProviderID(txtProvider.SelectedValue));
            insert.Parameters.AddWithValue("@categoryid", findCategoryID(txtCategory.SelectedValue));
            insert.ExecuteNonQuery();
            conn.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reward Added Successfully')", true);
            clearFields();
        }
        //Shows an error message if there is a problem connecting to the database
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('data connection error')", true);
            //Label9.Text += " " + ex;
        }
    }

    //finds the ID of the provider that was selected
    public int findProviderID(String providerName)
    {
        try
        {
            String commandText = "Select ProviderID from [dbo].[RewardProvider] WHERE ProviderName = @providername";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@providername", providerName);
            SqlDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int ProviderID = (int)reader["ProviderID"];
                conn.Close();
                return ProviderID;
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('error finding providerID')", true);
            return -1;
        }
        return -1;
    }

    //finds the id of the category that was selected
    public int findCategoryID(String categoryName)
    {
        try
        {
            String commandText = "Select CategoryID from [dbo].[RewardCategory] WHERE Description = @categoryname";
            SqlConnection conn = ProjectDB.connectToDB();
            SqlCommand select = new SqlCommand(commandText, conn);

            select.Parameters.AddWithValue("@categoryname", categoryName);
            SqlDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int categoryID = (int)reader["CategoryID"];
                conn.Close();
                return categoryID;
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('error finding categoryID')", true);
            return -1;
        }
        return -1;
    }

    //clears all the text fields
    public void clearFields()
    {
        txtName.Text = String.Empty;
        txtDescription.Text = String.Empty;
        txtPrice.Text = String.Empty;
        txtQuantity.Text = String.Empty;
        txtStartDate.Text = String.Empty;
        txtEndDate.Text = String.Empty;
        txtProvider.ClearSelection();
        txtCategory.ClearSelection();
    }


    protected Boolean checkDate(string date)
    {
        try
        {
            DateTime.Parse(date);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected Boolean checkInt(string num)
    {
        try
        {
            int.Parse(num);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected Boolean checkDecimal(string dec)
    {
        try
        {
            Decimal.Parse(dec);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}