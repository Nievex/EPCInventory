<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopProfile.aspx.cs" Inherits="EPC_Inventory.Products.ShopProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" ID="EditProfileToggle" Text="Edit Profile" OnClientClick="toggleTextBoxes();" />
        <div>
            <label>Shop Name</label>
            <asp:TextBox runat="server" ID="ShopName"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ShopNameRequiredValidator" runat="server" ControlToValidate="ShopName" ErrorMessage="Shop Name is required." />
        </div>
        <div>
            <label>Address</label>
            <asp:TextBox runat="server" ID="ShopAddress"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ShopAddressRequiredValidator" runat="server" ControlToValidate="ShopAddress" ErrorMessage="Address is required." />
        </div>
        <div>
            <label>Contact Number</label>
            <asp:TextBox runat="server" ID="ShopContactNumber"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ShopContactNumberRequiredValidator" runat="server" ControlToValidate="ShopContactNumber" ErrorMessage="Contact Number is required." />
            <asp:RegularExpressionValidator ID="ShopContactNumberFormatValidator" runat="server" ControlToValidate="ShopContactNumber" ErrorMessage="Invalid contact number format." ValidationExpression="^\d{10}$" />
        </div>
        <div>
            <label>Email</label>
            <asp:TextBox runat="server" ID="ShopEmail" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ShopEmailRequiredValidator" runat="server" ControlToValidate="ShopEmail" ErrorMessage="Email is required." />
            <asp:RegularExpressionValidator ID="ShopEmailFormatValidator" runat="server" ControlToValidate="ShopEmail" ErrorMessage="Invalid email format." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
        </div>
        <div>
            <label>Username</label>
            <asp:TextBox runat="server" ID="ShopUsername"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ShopUsernameRequiredValidator" runat="server" ControlToValidate="ShopUsername" ErrorMessage="Username is required." />
        </div>
        <div>
            <label>First Name</label>
            <asp:TextBox runat="server" ID="ShopOwnerFN"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ShopOwnerFNRequiredValidator" runat="server" ControlToValidate="ShopOwnerFN" ErrorMessage="First Name is required." />
        </div>
        <div>
            <label>Last Name</label>
            <asp:TextBox runat="server" ID="ShopOwnerLN"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ShopOwnerLNRequiredValidator" runat="server" ControlToValidate="ShopOwnerLN" ErrorMessage="Last Name is required." />
        </div>
        <div>
            <asp:Image runat="server" ID="ShopLogoImagePreview" />
            <asp:FileUpload runat="server" ID="NewShopLogo" onchange="previewImage()" />
        </div>
        <!-- Trigger/Open The Modal -->
        <button type="button" id="myBtn">Open Modal</button>

        <!-- The Modal -->
        <div id="myModal" class="modal">

            <!-- Modal content -->
            <div class="modal-content">
                <div>
                    <label>Old Password</label>
                    <asp:TextBox runat="server" ID="OldPassword" TextMode="Password"></asp:TextBox>
                </div>
                <div>
                    <label>New Password</label>
                    <asp:TextBox runat="server" ID="NewPassword" TextMode="Password"></asp:TextBox>
                </div>
                <div>
                    <label>Confirm Password</label>
                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="PasswordMatchValidator" runat="server" ControlToValidate="ConfirmPassword" ControlToCompare="NewPassword" Operator="Equal" ErrorMessage="Passwords do not match." />
                </div>

                <asp:Button runat="server" ID="ChangePasswordButton" Text="Change Password" OnClick="ChangePassword_Click" />
            </div>

        </div>

        <asp:Button runat="server" ID="UpdateProfile" Text="Update Profile" OnClick="UpdateProfile_Click" />
    </form>

    <script>
        function toggleTextBoxes() {
            var textboxes = document.querySelectorAll('input[type="text"]');
            textboxes.forEach(function (textbox) {
                textbox.readOnly = !textbox.readOnly;
            });
        }

        function previewImage() {
            var shopLogoImagePreview = document.getElementById('ShopLogoImagePreview');
            var fileInput = document.getElementById('NewShopLogo');

            if (fileInput.files && fileInput.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    shopLogoImagePreview.src = e.target.result;
                };

                reader.readAsDataURL(fileInput.files[0]);
            }
        }

        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>

</body>
</html>
