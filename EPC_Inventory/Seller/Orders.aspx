<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Seller/SellerDashboard.Master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="EPC_Inventory.Seller.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <h2>Orders</h2>
        <asp:Label runat="server" ID="Breadcrumb">HOME / Order List</asp:Label>
    </div>

    <div class="status-container">
        <asp:LinkButton runat="server" CssClass="status-anchor" OnClick="StatusAnchor_Click">Pending</asp:LinkButton>
        <asp:LinkButton runat="server" CssClass="status-anchor" OnClick="StatusAnchor_Click">Approved</asp:LinkButton>
        <asp:LinkButton runat="server" CssClass="status-anchor" OnClick="StatusAnchor_Click">Declined</asp:LinkButton>
        <asp:LinkButton runat="server" CssClass="status-anchor" OnClick="StatusAnchor_Click">Cancelled</asp:LinkButton>
    </div>

    <div class="orders-container">
        <div class="order-grid">
            <asp:Repeater runat="server" ID="OrderRepeater" OnItemCommand="OrderRepeater_ItemCommand" OnItemDataBound="OrderRepeater_ItemDataBound">
                <ItemTemplate>
                    <div class="order-card">
                        <div class="order-details">
                            <asp:Image runat="server" ID="ProductImagePreview" />
                            <asp:Label runat="server" ID="ProductName" Text='<%# Eval("PRODUCT_NAME") %>'></asp:Label>
                        </div>

                        <div class="customer-panel">
                            <div class="simple-flx">
                                <p>Quantity:</p>
                                <asp:Label runat="server" ID="OrderQuantity" Text='<%# Eval("QTY") %>' CssClass="qty"></asp:Label>
                            </div>
                            <div class="order-price">
                                <p>Subtotal:</p>
                                <asp:Label runat="server" ID="OrderTotalPrice" CssClass="price" Text='<%# "₱"+Eval("SUBTOTAL", "{0:N2}") %>'></asp:Label>
                            </div>

                            <div class="payment-method">
                                <p>Payment Method:</p>
                                <asp:Label runat="server" ID="PaymentMethod" Text='<%# Eval("PAYMENT_METHOD") %>' CssClass="payment-mthd"></asp:Label>
                            </div>

                            <div class="customer-details">
                                <div class="simple-flx">
                                    <p>Name:</p>
                                    <asp:Label runat="server" ID="CustomerName" Text='<%# Eval("CUSTOMER_NAME") %>'></asp:Label>
                                </div>
                                <div class="simple-flx">
                                    <p>Address:</p>
                                    <asp:Label runat="server" ID="CustomerAddress" Text='<%# Eval("CUSTOMER_ADDRESS") %>'></asp:Label>
                                </div>
                                <div class="simple-flx">
                                    <p>Contact No.:</p>
                                    <asp:Label runat="server" ID="CustomerContactNumber" Text='<%# Eval("CUSTOMER_CONTACT_NUMBER") %>'></asp:Label>
                                </div>
                                <div class="simple-flx">
                                    <p>Date ordered:</p>
                                    <asp:Label runat="server" ID="OrderDate" Text='<%# Eval("ORDER_DATE", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                </div>
                            </div>

                            <asp:DropDownList runat="server" ID="StatusDropdown" CssClass="status-dropdown" SelectedValue='<%# Eval("ORDER_STATUS") %>'>
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Approved">Approve</asp:ListItem>
                                <asp:ListItem Value="Declined">Decline</asp:ListItem>
                                <asp:ListItem Value="Cancelled">Cancel</asp:ListItem>
                            </asp:DropDownList>

                            <asp:Button runat="server" Text="Submit" CssClass="order-update-btn" CommandName="UpdateStatus" CommandArgument='<%# Eval("ORDER_ID") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
