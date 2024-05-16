<%@ Page Title="Sales Report" Language="C#" MasterPageFile="~/Seller/SellerDashboard.Master" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="EPC_Inventory.Seller.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <h2>Sales Report</h2>
        <asp:Label runat="server" ID="Breadcrumb">HOME / Sales Report</asp:Label>
    </div>

    <div class="sales-container">
        <div class="sales-card-grid">
            <div class="sales-card">
                <h2>Products Sold</h2>
                <p class="value">
                    <asp:Label runat="server" ID="ProductsSoldValue"></asp:Label>
                </p>
            </div>
            <div class="sales-card">
                <h2>Monthly Sales</h2>
                <p class="value">
                    <asp:Label runat="server" ID="MonthlySalesValue"></asp:Label>
                </p>
            </div>
            <div class="sales-card">
                <h2>Yearly Sales</h2>
                <p class="value">
                    <asp:Label runat="server" ID="YearlySalesValue"></asp:Label>
                </p>
            </div>
        </div>

        <h3>Transaction History</h3>
        <table>
            <tr>
                <th>Order ID</th>
                <th>Product Name</th>
                <th>QTY</th>
                <th>Sold</th>
                <th>Customer Name</th>
                <th>Date</th>
                <th>Status</th>
            </tr>
            <asp:Repeater runat="server" ID="TransactionHistoryRepeater">
                <itemtemplate>
                    <tr>
                        <td><%# Eval("ORDER_ID") %></td>
                        <td><%# Eval("PRODUCT_NAME") %></td>
                        <td><%# Eval("QTY") %></td>
                        <td><%# "₱"+Eval("SUBTOTAL", "{0:N2}") %></td>
                        <td><%# Eval("CUSTOMER_NAME") %></td>
                        <td><%# Eval("ORDER_DATE", "{0:dd-MMM-yy}") %></td>
                        <td><%# Eval("ORDER_STATUS") %></td>
                    </tr>
                </itemtemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
