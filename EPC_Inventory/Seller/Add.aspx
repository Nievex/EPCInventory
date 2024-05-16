<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="SellerDashboard.Master" CodeFile="Add.aspx.cs" Inherits="EPC_Inventory.Add" %>

<asp:Content ID="Navigation" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <div class="breadcrumb">
        <h2>Add Product</h2>
        <asp:Label runat="server" ID="Breadcrumb">HOME / Add Product</asp:Label>
    </div>

    <div class="product-section">
        <div>
            <h1>Add Product</h1>
        </div>
        <div class="product-container">
            <div class="product-container-left">
                <div>
                    <label>Product ID</label><br />
                    <asp:TextBox runat="server" ID="ProductIdField"></asp:TextBox>
                </div>

                <div class="product-name">
                    <label>Product Name</label><br />
                    <asp:TextBox runat="server" ID="ProductNameField"></asp:TextBox>
                </div>

                <div>
                    <label>Category</label><br />
                    <asp:DropDownList
                        ID="CategoryList"
                        runat="server"
                        CssClass="category-dropdown">

                        <asp:ListItem Selected="True" Disabled="true">Choose here</asp:ListItem>
                        <asp:ListItem Value="Dog Food">Dog Food</asp:ListItem>
                        <asp:ListItem Value="Dog Treats">Dog Treats</asp:ListItem>
                        <asp:ListItem Value="Pet Grooming Supplies">Pet Grooming Supplies</asp:ListItem>
                        <asp:ListItem Value="Pet Health and Wellness">Pet Health and Wellness</asp:ListItem>
                        <asp:ListItem Value="Pet Supplies">Pet Supplies</asp:ListItem>
                        <asp:ListItem Value="Cat Food">Cat Food</asp:ListItem>
                        <asp:ListItem Value="Cat Treats">Cat Treats</asp:ListItem>
                        <asp:ListItem Value="Cat Litter and Accessories">Cat Litter and Accessories</asp:ListItem>
                        <asp:ListItem Value="Features">Features</asp:ListItem>

                    </asp:DropDownList>
                </div>

                <div>
                    <label>Stocks</label><br />
                    <asp:TextBox runat="server" ID="StocksField" TextMode="Number" CssClass="stocks-field" ></asp:TextBox>
                </div>

                <div>
                    <label>Price</label><br />
                    <asp:TextBox runat="server" ID="PriceField"></asp:TextBox>
                </div>

                <div class="btn-container">
                    <asp:Button runat="server" ID="AddProductBtn" Text="Add Product" OnClick="AddBtn" CssClass="add-product-btn" />
                    <a href="Inventory.aspx" class="return-btn">Return</a>
                </div>

            </div>
            <div class="product-container-right">
                <div class="image-preview">
                    <div class="image-container">
                        <img id="image_upload_preview" src="../images/empty-img.jpg" />
                    </div>
                    <label for="inputFile" class="custom-file-upload">
                        <i class="fa fa-upload" aria-hidden="true"></i>Upload an image
                    </label>
                    <input type="file" id="inputFile" name="inputFile" class="file-upload" />
                </div>
            </div>
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
    </script>
</asp:Content>

