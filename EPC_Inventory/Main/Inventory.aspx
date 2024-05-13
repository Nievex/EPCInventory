<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="EPC_Inventory.Inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory</title>
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
        <div class="container">
            <div class="inventory-header">
                <h1>Inventory</h1>
                <div>
                    <asp:Button runat="server" ID="LogoutButton" OnClick="LogoutBtnClick" CssClass="logout-btn" Text="Logout" />
                    <a href="Add.aspx" class="add-btn"><i class="fa fa-plus-circle" aria-hidden="true"></i>Add Product</a>
                    <a href="ShopProfile.aspx" class="add-btn"><i class="fa fa-plus-circle" aria-hidden="true"></i>Update Profile</a>
                </div>
            </div>
            <div class="search-bar">
                <asp:TextBox ID="searchTextBox" runat="server" placeholder="Search..." CssClass="search-bar-fld" AutoPostBack="true" OnTextChanged="SearchTextBox_TextChanged"></asp:TextBox>
                <i class="fa fa-search" aria-hidden="true"></i>
            </div>
            <div class="header">
                <table>
                    <tr>
                        <th>
                            <asp:CheckBox runat="server" CssClass="check-bx" />
                        </th>
                        <th>Product ID</th>
                        <th>Image</th>
                        <th>Name</th>
                        <th>Category</th>
                        <th>Stock</th>
                        <th>Price</th>
                        <th>Actions</th>
                    </tr>
                    <asp:Repeater runat="server" ID="RepeaterInventory">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox runat="server" CssClass="check-bx" />
                                </td>
                                <td><%# Eval("ProductID") %></td>
                                <td>
                                    <asp:Image runat="server" ID="ImagePreview" ImageUrl='<%# GetBase64Image(Eval("ImageURL")) %>' CssClass="product-img" />
                                </td>
                                <td><%# Eval("Name") %></td>
                                <td><%# Eval("Category") %></td>
                                <td><%# Eval("Stocks") %></td>
                                <td><%# Eval("Price") %></td>
                                <td>
                                    <div class="dropdown">
                                        <button class="dropbtn">Action <i class="fa fa-chevron-down" aria-hidden="true"></i></button>
                                        
                                        <div class="dropdown-content">
                                            <a href='<%# "Edit.aspx?ProductID=" + Eval("ProductID") %>'>Edit</a>
                                            <asp:Button runat="server" Text=" Delete" CssClass="delete-btn" OnClick="DeleteProduct" CommandArgument='<%# Eval("ProductID") %>' />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>

        <div id></div>
    </form>

    <script>
        function previewImage() {
            var fileInput = document.getElementById('ImageFileUpload');
            var imagePreview = document.getElementById('ImagePreview');

            if (fileInput.files && fileInput.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    imagePreview.src = e.target.result;
                };

                reader.readAsDataURL(fileInput.files[0]);
            }
        }
    </script>
</body>
</html>
