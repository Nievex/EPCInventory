<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Seller/SellerDashboard.Master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="EPC_Inventory.Seller.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <h2>Orders</h2>
        <asp:Label runat="server" ID="Breadcrumb">HOME / Order List</asp:Label>
    </div>

    <div class="status-container">
        <a href="#" class="status-anchor">Pending</a>
        <a href="#" class="status-anchor">Approved</a>
        <a href="#" class="status-anchor">Declined</a>
        <a href="#" class="status-anchor">Cancelled</a>
    </div>

    <div class="orders-container">
        <div class="order-grid">
            <div class="order-card">
                <div class="order-details">
                    <asp:Image runat="server" ID="ProductImagePreview" />
                    <asp:Label runat="server" ID="ProductName"></asp:Label>
                </div>

                <div class="customer-panel">
                    <div class="order-quantity">
                        <p>Quantity:</p>
                        <asp:Label runat="server" ID="OrderQuantity"></asp:Label>
                    </div>
                    <div class="order-price">
                        <p>Subtotal:</p>
                        <asp:Label runat="server" ID="OrderTotalPrice" CssClass="price"></asp:Label>
                    </div>

                    <div class="payment-method">
                        <p>Payment Method:</p>
                        <asp:Label runat="server" ID="PaymentMethod"></asp:Label>
                    </div>

                    <div class="customer-details">
                        <asp:Label runat="server" ID="CustomerName"></asp:Label>
                        <asp:Label runat="server" ID="CustomerAddress"></asp:Label>
                        <asp:Label runat="server" ID="CustomerContactNumber"></asp:Label>
                        <asp:Label runat="server" ID="OrderDate"></asp:Label>
                    </div>

                    <asp:DropDownList runat="server" CssClass="status-dropdown">
                        <asp:ListItem Selected="True" Value="Pending">Pending</asp:ListItem>
                        <asp:ListItem Value="Approved">Approve</asp:ListItem>
                        <asp:ListItem Value="Declined">Decline</asp:ListItem>
                        <asp:ListItem Value="Cancelled">Cancel</asp:ListItem>
                    </asp:DropDownList>

                    <asp:Button runat="server" Text="Submit" CssClass="order-update-btn" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
