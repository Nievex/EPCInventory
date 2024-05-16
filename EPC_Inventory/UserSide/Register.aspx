<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="EPC_Inventory.UserSide.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Register</h2>
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            <div>
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                <asp:TextBox ID="CustomerName" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="Age"></asp:Label>
                <asp:TextBox ID="Age" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label3" runat="server" Text="Birthday"></asp:Label>
                <asp:TextBox ID="Birthday" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label4" runat="server" Text="Address"></asp:Label>
                <asp:TextBox ID="Address" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label5" runat="server" Text="Contact Number"></asp:Label>
                <asp:TextBox ID="ContactNumber" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label7" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick="RegisterButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
