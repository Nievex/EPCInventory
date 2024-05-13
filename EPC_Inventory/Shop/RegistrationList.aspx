<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationList.aspx.cs" Inherits="EPC_Inventory.RegistrationList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approval</title>
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
        <table>
            <tr>
                <th>Shop Reg ID</th>
                <th>Name</th>
                <th>Username</th>
                <th>Shop Logo</th>
                <th>Shop Status</th>
                <th>Date Registered</th>
                <th>Review</th>
            </tr>
            <asp:Repeater runat="server" ID="ShopRegistrationListRepeater">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("SHOP_REG_ID") %></td>
                        <td><%# Eval("SHOP_NAME") %></td>
                        <td><%# Eval("SHOP_USERNAME") %></td>
                        <td>
                            <asp:Image runat="server" ID="ShopLogoPreview" ImageUrl='<%# GetBase64Image(Eval("SHOP_LOGO")) %>' CssClass="product-img"></asp:Image></td>
                        <td><%# Eval("SHOP_STATUS") %></td>
                        <td><%# Eval("CREATION_DATE") %></td>
                        <td>
                            <a href='<%# "Review.aspx?Shop_Reg_ID=" + Eval("Shop_Reg_ID") %>'>Review</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>
        <asp:Label runat="server" ID="ErrorMessageLabel" ForeColor="Red" />
    </form>
</body>
</html>
