<%@ Page Language="C#" Title="Shop Profile" AutoEventWireup="true" MasterPageFile="~/Seller/SellerDashboard.Master" CodeFile="ShopProfile.aspx.cs" Inherits="EPC_Inventory.Seller.ShopProfile" %>

<asp:Content ID="Navigation" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <h2>Profile</h2>
        <asp:Label runat="server" CssClass="breadcrumb-label" ID="Breadcrumb">HOME / Profile</asp:Label>
    </div>

    <div class="shop-profile-container">
        <div class="upper-panel">
            <div>
                <div class="profile-image">
                    <label for="inputFile" class="profile-label">
                        <div class="profile-hover">
                            <div>
                                <i class="fa fa-upload" aria-hidden="true"></i>
                                <p>Upload new logo</p>
                            </div>
                        </div>
                        <img id="image_upload_preview" class="shop-profile-img" />
                        <asp:Image runat="server" ID="ShopLogoImagePreview" />
                    </label>
                </div>
                <input type="file" id="inputFile" name="inputFile" class="file-upload" />
            </div>

            <div>
                <asp:Label runat="server" ID="ShopNameLabel" CssClass="shop-name-label"></asp:Label><br />
                <p class="verified"><i class="fa fa-check-circle" aria-hidden="true"></i>Verified</p>
            </div>

            <div class="toggle-edit-container">
                <input type="button" id="editProfileToggle" onclick="toggleTextBoxes();" value="Edit Profile" class="toggle-edit" />
                <p>Click to toggle edit profile</p>
            </div>
        </div>

        <div class="lower-panel">
            <div>
                <label>Shop Name</label>
                <span>
                    <asp:RequiredFieldValidator ID="ShopNameRequiredValidator" runat="server" ControlToValidate="ShopName" ErrorMessage="Shop Name is required." /></span>
                <br />
                <asp:TextBox runat="server" ID="ShopName"></asp:TextBox>
            </div>
            <div>
                <label>Address</label>
                <span>
                    <asp:RequiredFieldValidator ID="ShopAddressRequiredValidator" runat="server" ControlToValidate="ShopAddress" ErrorMessage="Address is required." /></span>
                <br />
                <asp:TextBox runat="server" ID="ShopAddress"></asp:TextBox>
            </div>
            <div>
                <label>Contact Number</label>
                <span>
                    <asp:RegularExpressionValidator ID="ShopContactNumberFormatValidator" runat="server" ControlToValidate="ShopContactNumber" ErrorMessage="Invalid contact number." ValidationExpression="^\d{10}$" /></span>
                <br />
                <asp:TextBox runat="server" ID="ShopContactNumber"></asp:TextBox>
            </div>
            <div>
                <label>Email</label><span><asp:RegularExpressionValidator ID="ShopEmailFormatValidator" runat="server" ControlToValidate="ShopEmail" ErrorMessage="Invalid email format." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" /></span>
                <br />
                <asp:TextBox runat="server" ID="ShopEmail" TextMode="Email"></asp:TextBox>
            </div>
            <div>
                <label>Username</label><span><asp:RequiredFieldValidator ID="ShopUsernameRequiredValidator" runat="server" ControlToValidate="ShopUsername" ErrorMessage="Username is required." /></span>
                <br />
                <asp:TextBox runat="server" ID="ShopUsername"></asp:TextBox>
            </div>
            <div>
                <label>First Name</label><span><asp:RequiredFieldValidator ID="ShopOwnerFNRequiredValidator" runat="server" ControlToValidate="ShopOwnerFN" ErrorMessage="First Name is required." /></span>
                <br />
                <asp:TextBox runat="server" ID="ShopOwnerFN"></asp:TextBox>
            </div>
            <div>
                <label>Last Name</label><span><asp:RequiredFieldValidator ID="ShopOwnerLNRequiredValidator" runat="server" ControlToValidate="ShopOwnerLN" ErrorMessage="Last Name is required." /></span>
                <br />
                <asp:TextBox runat="server" ID="ShopOwnerLN"></asp:TextBox>
            </div>

        <asp:Button runat="server" ID="UpdateProfile" Text="Update Profile" OnClick="UpdateProfile_Click" CssClass="update-profile-btn" />
    </div>

        <div class="change-password-panel">
            <div>
                <label>Old Password</label><br />
                <asp:TextBox runat="server" ID="OldPassword" TextMode="Password"></asp:TextBox>
            </div>

            <div>
                <label>New Password</label><br />
                <asp:TextBox runat="server" ID="NewPassword" TextMode="Password"></asp:TextBox>
            </div>

            <div>
                <label>Confirm Password</label><span><asp:CompareValidator ID="PasswordMatchValidator" runat="server" ControlToValidate="ConfirmPassword" ControlToCompare="NewPassword" Operator="Equal" ErrorMessage="Passwords do not match." /></span>
                <br />
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password"></asp:TextBox>
            </div>

            <asp:Button runat="server" ID="ChangePasswordButton" Text="Change Password" OnClick="ChangePassword_Click" CssClass="change-password-btn" />
        </div>
    </div>

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

        function toggleTextBoxes() {
            var textboxes = document.querySelectorAll('input[type="text"]');
            textboxes.forEach(function (textbox) {
                textbox.readOnly = !textbox.readOnly;
            });
        }
    </script>
</asp:Content>
