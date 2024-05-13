<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="EPC_Inventory.Shop.Review" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Shop Reg ID</label>
            <asp:Label runat="server" ID="ShopRegID"></asp:Label>
        </div>
        <div>
            <label>Shop Name</label>
            <asp:Label runat="server" ID="ShopName"></asp:Label>
        </div>
        <div>
            <label>Shop Address</label>
            <asp:Label runat="server" ID="ShopAddress"></asp:Label>
        </div>
        <div>
            <label>Contact Number</label>
            <asp:Label runat="server" ID="ShopContactNumber"></asp:Label>
        </div>
        <div>
            <label>Email</label>
            <asp:Label runat="server" ID="ShopEmail"></asp:Label>
        </div>
        <div>
            <label>First Name</label>
            <asp:Label runat="server" ID="ShopFirstName"></asp:Label>
        </div>
        <div>
            <label>Last Name</label>
            <asp:Label runat="server" ID="ShopLastName"></asp:Label>
        </div>
        <div>
            <label>Shop Requirements</label>
            <asp:Image runat="server" ID="ShopRequirementsPreview" />
        </div>
        <div>
            <label>Username</label>
            <asp:Label runat="server" ID="ShopUsername"></asp:Label>
        </div>
        <div>
            <label>Password</label>
            <asp:Label runat="server" ID="ShopPassword"></asp:Label>
        </div>
        <div>
            <label>Shop Logo</label>
            <asp:Image runat="server" ID="ShopLogoPreview" />
        </div>
        <div>
            <label>Shop Status</label>
            <asp:DropDownList ID="ShopStatusSelect" runat="server" DataTextField="Pending" AutoPostBack="True">
                  <asp:ListItem Value="Approved">Approve</asp:ListItem>
                  <asp:ListItem Value="Declined">Decline</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <label>Date Registered</label>
            <asp:Label runat="server" ID="ShopDateRegistered"></asp:Label>
        </div>
        <asp:Button runat="server" ID="SubmitButton" Text="Submit" OnClick="SubmitBtn" />
    </form>
</body>
</html>
