<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="EPC_Inventory.Seller.ForgotPassword" %>

<!DOCTYPE html>

<html>

<head runat="server">
    <title>Forgot Password</title>
    <link rel="stylesheet" href="../styles.css" runat="server" />
    <link
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css"
        rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
        href="https://fonts.googleapis.com/css2?family=Raleway:ital,wght@0,100..900;1,100..900&display=swap"
        rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="fpw-flex">
            <div class="forgot-pw-container">
                <h2>Forgot Password</h2>
                <asp:Label runat="server" Text="Enter your username:" /><br />
                <asp:TextBox runat="server" ID="UsernameTextBox" /><br />
                <asp:Button runat="server" Text="Submit" OnClick="Submit_Click" CssClass="forgot-btn" />
                <asp:Label runat="server" ID="MessageLabel" ForeColor="Red" />
            </div>
        </div>
    </form>
</body>

</html>