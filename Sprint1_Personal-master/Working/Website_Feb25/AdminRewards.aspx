﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminRewards.aspx.cs" Inherits="AdminRewards" %>

<!DOCTYPE html>
<html>
    <head>
        <title>Add Reward Items</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins">
        <style>
        body,h1,h2,h3,h4,h5 {font-family: "Poppins", sans-serif}
        .button
        {
            background-color: #F44336;
            color: white;
            padding: 8px 10px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .button:hover{
            background-color: #bf342a;
        }
        input[type=text], .ddl, textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
            resize: vertical;
        }
        body {font-size:16px;}
        .w3-half img{margin-bottom:-6px;margin-top:16px;opacity:0.8;cursor:pointer}
        .w3-half img:hover{opacity:1}
        </style>
    </head>
    <body>

    <!-- Sidebar/menu -->
    <nav class="w3-sidebar w3-red w3-collapse w3-top w3-large w3-padding" style="z-index:3;width:300px;font-weight:bold;" id="mySidebar"><br>
      <a href="javascript:void(0)" onclick="w3_close()" class="w3-button w3-hide-large w3-display-topleft" style="width:100%;font-size:22px">Close Menu</a>
      <div class="w3-container">
        <h3 class="w3-padding-64"><b><br>KUDOS</b></h3>
      </div>
      <div class="w3-bar-block">
        <a href="Admin.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Analytics</a>  
        <a href="AdminRewards.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Add/Edit Rewards</a> 
        <a href="AdminCreate.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Create/Edit Users</a>   
        <a href="AdminAddFunds.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Add Funds</a>
        <a href="Logout.aspx" onclick="w3_close()" class="w3-bar-item w3-button w3-hover-white">Logout</a>
      </div>
    </nav>

    <!-- Top menu on small screens -->
    <header class="w3-container w3-top w3-hide-large w3-red w3-xlarge w3-padding">
      <a href="javascript:void(0)" class="w3-button w3-red w3-margin-right" onclick="w3_open()">☰</a>
      <span>KUDOS</span>
    </header>

    <!-- Overlay effect when opening sidebar on small screens -->
    <div class="w3-overlay w3-hide-large" onclick="w3_close()" style="cursor:pointer" title="close side menu" id="myOverlay"></div>

    <!-- !PAGE CONTENT! -->
    <div class="w3-main" style="margin-left:340px;margin-right:40px">

      <!-- Header -->
      <div class="w3-container" style="margin-top:80px" id="showcase">
        <h1 class="w3-jumbo"><b>Administration</b></h1>
        <h1 class="w3-xxxlarge w3-text-red"><b>Add a Reward</b></h1>
          <hr style="width:50px;border:5px solid red; float: left;" class="w3-round">
          <br /><br />
        
        <form id="feed" runat="server">
            &nbsp;<br /><br />
            <asp:Label ID="lblName" runat="server" Text="Reward Name"></asp:Label>
            <asp:Label ID="lblErrorName" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox>
            <br /><br />
            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
            <asp:Label ID="lblErrorDescription" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescription" runat="server" MaxLength="50"></asp:TextBox>
            <br /><br />
            <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
            <asp:Label ID="lblErrorPrice" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" ErrorMessage="Invalid Price" Type="Currency" ControlToValidate="txtPrice" ForeColor="Red"></asp:CompareValidator>
            <br /><br />
            <asp:Label ID="lblStart"  runat="server" Text="Start Date"></asp:Label>
            <asp:Label ID="lblErrorStart" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtStartDate" placeholder="YYYY-MM-DD" runat="server"></asp:TextBox>

            <br /><br />
            <asp:Label ID="lblEnd" runat="server" Text="End Date"></asp:Label>
            <asp:Label ID="lblErrorEnd" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtEndDate" placeholder="YYYY-MM-DD" runat="server"></asp:TextBox>

            <br /><br />
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
            <asp:Label ID="lblErrorQuantity" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                ControlToValidate="txtQuantity" runat="server" BackColor ="White"
                ErrorMessage="Only Numbers allowed"
                ValidationExpression="\d+" ForeColor="Red"></asp:RegularExpressionValidator>
            <br /><br />
            <asp:Label ID="lblProvider" runat="server" Text="Reward Provider"></asp:Label>
            <asp:Label ID="lblErrorProvider" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:DropDownList CssClass="ddl" ID="txtProvider" runat="server">
            </asp:DropDownList>
            <br /><br />
            <asp:Label ID="lblCategory" runat="server" Text="Reward Category"></asp:Label>
            <asp:Label ID="lblErrorCategory" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <br />
            <asp:DropDownList CssClass="ddl" ID="txtCategory" runat="server">
            </asp:DropDownList>
            <br /><br />
            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save Reward" OnClick="btnSave_Click" />
            <br /><asp:Label ID="lblError" runat="server" Text="Label" BackColor="White" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <div class="w3-container" style="margin-top:80px" id="editshowcase">       
                    <h1 class="w3-xxxlarge w3-text-red">Edit and Delete Rewards</h1>
                    <hr style="width:50px;border:5px solid red; float: left;" class="w3-round">
                </div>
            <br />
            <asp:GridView CssClass="ddl" ID="rewardGrid" runat="server" AutoGenerateEditButton="True" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="RewardID" DataSourceID="SqlDataSource2" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="RewardID" HeaderText="RewardID" InsertVisible="False" ReadOnly="True" SortExpression="RewardID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="w3-red" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="w3-red" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Lab4ConnectionString %>"  InsertCommand="INSERT INTO [RewardItem] ([Name], [Description], [Price], [StartDate], [EndDate], [Quantity]) VALUES (@Name, @Description, @Price, @StartDate, @EndDate, @Quantity)" SelectCommand="SELECT [RewardID], [Name], [Description], [Price], [StartDate], [EndDate], [Quantity] FROM [RewardItem]" UpdateCommand="UPDATE [RewardItem] SET [Name] = @Name, [Description] = @Description, [Price] = @Price, [StartDate] = @StartDate, [EndDate] = @EndDate, [Quantity] = @Quantity WHERE [RewardID] = @RewardID">
                
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String"  />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Price" Type="Decimal" />
                    <asp:Parameter Name="StartDate" Type="DateTime" />
                    <asp:Parameter Name="EndDate" Type="DateTime" />
                    <asp:Parameter Name="Quantity" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" DefaultValue="DEFAULT" />
                    <asp:Parameter Name="Description" Type="String" DefaultValue="DEFAULT" />
                    <asp:Parameter Name="Price" Type="Decimal" DefaultValue="0" />
                    <asp:Parameter Name="StartDate" Type="DateTime" DefaultValue="2018/01/01" />
                    <asp:Parameter Name="EndDate" Type="DateTime" DefaultValue="2018/01/01" />
                    <asp:Parameter Name="Quantity" Type="Int32" DefaultValue="0"  />
                    <asp:Parameter Name="RewardID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </form>
      </div>

    <div class="w3-container" id="administration" style="margin-top: 75px;">
    </div>
  

    <!-- End page content -->
    </div>

    <!-- W3.CSS Container -->
    <div class="w3-light-grey w3-container w3-padding-32" style="margin-top:75px;padding-right:58px"><p class="w3-right">Powered by <a href="https://www.w3schools.com/w3css/default.asp" title="W3.CSS" target="_blank" class="w3-hover-opacity">w3.css</a></p></div>

    <script>
        // Script to open and close sidebar
        function w3_open() {
            document.getElementById("mySidebar").style.display = "block";
            document.getElementById("myOverlay").style.display = "block";
        }

        function w3_close() {
            document.getElementById("mySidebar").style.display = "none";
            document.getElementById("myOverlay").style.display = "none";
        }

        // Modal Image Gallery
        function onClick(element) {
            document.getElementById("img01").src = element.src;
            document.getElementById("modal01").style.display = "block";
            var captionText = document.getElementById("caption");
            captionText.innerHTML = element.alt;
        }
    </script>

    </body>
</html>