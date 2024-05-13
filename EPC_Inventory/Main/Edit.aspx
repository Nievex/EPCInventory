<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="EPC_Inventory.Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Product ID</label>
            <asp:TextBox runat="server" ID="ProductIdField"></asp:TextBox>
        </div>

        <div>
            <label>Product Name</label>
            <asp:TextBox runat="server" ID="ProductNameField"></asp:TextBox>
        </div>

        <div>
            <label>Category</label>
            <asp:TextBox runat="server" ID="CategoryField"></asp:TextBox>
        </div>

        <div>
            <label>Stocks</label>
            <asp:TextBox runat="server" ID="StocksField"></asp:TextBox>
        </div>

        <div>
            <label>Price</label>
            <asp:TextBox runat="server" ID="PriceField"></asp:TextBox>
        </div>

        <div>
            <label>Image</label><br />
            <asp:FileUpload runat="server" ID="ImageFileUpload" onchange="previewImage()"></asp:FileUpload>
            <asp:Image runat="server" ID="ImagePreview" />
        </div>

        <asp:Button runat="server" ID="UpdateProductBtn" Text="Update" OnClick="UpdateBtn" />
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
