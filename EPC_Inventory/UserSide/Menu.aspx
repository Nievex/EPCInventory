<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Product Menu</title>
    <style>
        .product-card {
            border: 1px solid #ddd;
            padding: 10px;
            margin: 10px;
            display: inline-block;
            width: 200px;
        }
        .product-card img {
            width: 100%;
            height: auto;
        }
        .product-card button {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 10px;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Product Menu</h2>
            <asp:Repeater ID="ProductRepeater" runat="server">
                <ItemTemplate>
                    <div class="product-card">
                        <asp:Image ID="ProductImage" runat="server" ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("IMAGEURL")) %>' />
                        <h3><%# Eval("NAME") %></h3>
                        <p>Price: $<%# Eval("PRICE") %></p>
                        <p>
                            Quantity: 
                            <asp:TextBox ID="QuantityTextBox" runat="server" Text="1" Width="40px" />
                        </p>
                        <p>
                            Payment Method:
                            <asp:DropDownList ID="PaymentMethodDropDown" runat="server">
                                <asp:ListItem Text="Cash on Delivery" Value="Cash on Delivery"></asp:ListItem>
                                <asp:ListItem Text="GCash" Value="GCash"></asp:ListItem>
                                <asp:ListItem Text="Credit Card" Value="Credit Card"></asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <asp:Button ID="BuyButton" runat="server" Text="Buy" CommandArgument='<%# Eval("PRODUCT_ID") %>' OnCommand="BuyButton_Command" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
