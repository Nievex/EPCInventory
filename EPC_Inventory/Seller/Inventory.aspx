<%@ Page Title="Inventory" Language="C#" MasterPageFile="SellerDashboard.Master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="EPC_Inventory.MasterInventory" %>

<asp:Content ID="Navigation" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <h2>Inventory</h2>
        <asp:Label runat="server" CssClass="breadcrumb-label" ID="Breadcrumb">HOME / Inventory</asp:Label>
    </div>

    <div class="container">
        <div class="inventory-header">
            <h1>Inventory</h1>
            <div>
                <a href="Add.aspx" class="add-btn"><i class="fa fa-plus-circle" aria-hidden="true"></i>Add Product</a>
            </div>
        </div>
        <div class="search-bar">
            <asp:TextBox ID="searchTextBox" runat="server" placeholder="Search..." CssClass="search-bar-fld" AutoPostBack="true" OnTextChanged="SearchTextBox_TextChanged"></asp:TextBox>
            <i class="fa fa-search" aria-hidden="true"></i>
        </div>
        <div class="header">
            <table>
                <tr>
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
                            <td><%# Eval("Product_ID") %></td>
                            <td>
                                <asp:Image runat="server" ID="ImagePreview" ImageUrl='<%# GetBase64Image(Eval("ImageURL")) %>' CssClass="product-img" />
                            </td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Category") %></td>
                            <td><%# Eval("Stocks") %></td>
                            <td>&#8369;<%# Eval("Price", "{0:N2}") %></td>
                            <td>
                                <div class="dropdown">
                                    <button class="dropbtn" disabled>Action <i class="fa fa-chevron-down" aria-hidden="true"></i></button>

                                    <div class="dropdown-content">
                                        <a href='<%# "Edit.aspx?Product_ID=" + Eval("Product_ID") %>'>Edit</a>
                                        <asp:Button runat="server" Text=" Delete" CssClass="delete-btn" OnClick="DeleteProduct" CommandArgument='<%# Eval("Product_ID") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
