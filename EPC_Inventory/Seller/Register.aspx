<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="EPC_Inventory.Seller.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" href="../styles.css" />
    <link
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css"
        rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
        href="https://fonts.googleapis.com/css2?family=Raleway:ital,wght@0,100..900;1,100..900&display=swap"
        rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="flex-center">
            <div class="register-container">
                <div class="register-header">
                    <div class="register-logo">
                        <img src="../images/logo.svg" />
                        <p>Easy Pet Care</p>
                    </div>
                    <h1>Sign up to Easy Pet Care</h1>
                </div>

                <div class="register-flex">
                    <div class="register-left-panel">
                        <div class="image-preview">
                            <div class="image-container">
                                <img id="image_upload_preview" src="../images/empty-img.jpg" />
                            </div>
                            <label for="inputFile" class="custom-file-upload">
                                <i class="fa fa-upload" aria-hidden="true"></i> Upload your shop logo
                            </label>
                            <input type="file" id="inputFile" name="inputFile" class="file-upload" />
                        </div>
                        <div>
                            Already have an account? <a href="../ShopLogin.aspx">Login here!</a>
                        </div>
                    </div>
                    <div class="register-right-panel">
                        <div>
                            <label>Shop Name</label><br />
                            <asp:TextBox runat="server" ID="ShopNameField" CssClass="register-fld"></asp:TextBox>
                        </div>
                        <div>
                            <label>Shop Username</label><br />
                            <asp:TextBox runat="server" ID="ShopUsernameField" CssClass="register-fld"></asp:TextBox>
                        </div>
                        <div class="email-grid">
                            <label>Email</label><br />
                            <asp:TextBox runat="server" TextMode="Email" ID="EmailField" CssClass="register-fld"></asp:TextBox>
                        </div>
                        <div>
                            <label>Password</label><br />
                            <asp:TextBox runat="server" TextMode="Password" ID="PasswordField" CssClass="register-fld"></asp:TextBox>
                        </div>

                        <div>
                            <label>Confirm Password</label><br />
                            <asp:TextBox runat="server" TextMode="Password" ID="ConfirmPasswordField" CssClass="register-fld"></asp:TextBox>
                            <span><asp:CompareValidator runat="server" ControlToValidate="ConfirmPasswordField" ControlToCompare="PasswordField" ErrorMessage="Passwords do not match" /></span>
                        </div>
                        <div class="address-grid">
                            <label>Address</label><br />
                            <asp:TextBox runat="server" ID="AddressField" CssClass="register-fld"></asp:TextBox>
                        </div>
                        <div>
                            <label>Contact No.</label><br />
                            <asp:TextBox runat="server" ID="ContactNoField" CssClass="register-fld"></asp:TextBox>
                        </div>
                        <div>
                            <label>First Name</label><br />
                            <asp:TextBox runat="server" ID="FirstNameField" CssClass="register-fld"></asp:TextBox>
                        </div>
                        <div>
                            <label>Last Name</label><br />
                            <asp:TextBox runat="server" ID="LastNameField" CssClass="register-fld"></asp:TextBox>
                        </div>
                        <div>
                            <label>Shop Requirements</label><br />
                            <label for="shopRequirementFile" class="shop-requirement">
                                <i class="fa fa-file" aria-hidden="true"></i>
                                <asp:Label runat="server" ID="FileUploaded" CssClass="file-upload-txt"> Upload your file</asp:Label>
                            </label>
                            <input type="file" id="shopRequirementFile" name="shopRequirementFile" class="file-upload" />
                        </div>

                        <asp:Button runat="server" ID="RegisterShopBtn" Text="Register" OnClick="RegisterShopBtn_Click" CssClass="register-btn" />
                        <asp:Label runat="server" ID="ErrorMessageLabel" ForeColor="Red" CssClass="error-message-label" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#image_upload_preview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        $("#inputFile").change(function () {
            readURL(this);
        });
    </script>
</body>
</html>
