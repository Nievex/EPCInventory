<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="EPC_Inventory.UserSide.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Login</h2>
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            <div>
                <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>