<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopLogin.aspx.cs" Inherits="EPC_Inventory.Shop.ShopLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="../styles.css" />
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
        <main class="login-container">
            <section class="login-section">
                <div class="left-panel">
                    <img src="../images/login-img.jpg" />
                </div>
                <div class="right-panel">

                    <div class="login-header">
                        <div class="logo">
                            <img src="../images/logo.svg" />
                            <p class="logo-txt">Easy Pet Care</p>
                        </div>
                        <h1>Welcome back!</h1>
                    </div>


                    <div>
                        <div class="input-container">
                            <label>Shop Username</label><br />
                            <asp:TextBox runat="server" ID="UsernameText" CssClass="login-txtbx" placeholder="Enter your username"></asp:TextBox>
                        </div>
                        <div class="input-container">
                            <label>Password</label><br />
                            <asp:TextBox runat="server" ID="PasswordText" TextMode="Password" CssClass="login-txtbx" placeholder="Enter your password"></asp:TextBox>
                            <div class="forgot-password">
                                <a href="#">Forgot Password?</a>
                            </div>
                        </div>
                        <asp:Button runat="server" ID="LoginBtn" OnClick="LoginBtnClick" Text="Login" CssClass="login-btn" />
                        <asp:Label runat="server" ID="ValidationLabel" CssClass="validation-txt"></asp:Label>
                    </div>


                    <div class="register-cta">
                        <p>Don't have an account yet? <a href="/Seller/Register.aspx">Join now!</a></p>
                    </div>
                </div>
            </section>
        </main>
    </form>
</body>
</html>
