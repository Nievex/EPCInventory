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
                <p class="value">232</p>
            </div>

            <div class="sales-card">
                <h2>Monthly Sales</h2>
                <p class="value">P49,000</p>
            </div>

            <div class="sales-card">
                <h2>Yearly Sales</h2>
                <p class="value">P123,345</p>
            </div>
        </div>

        <h3>Transaction History</h3>
        <table>
            <tr>
                <th>Transaction ID</th>
                <th>Reference Number</th>
                <th>Product Name</th>
                <th>QTY</th>
                <th>Price</th>
                <th>Customer Name</th>
                <th>Date</th>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
            <tr>
                <td>2309405</td>
                <td>10129393994</td>
                <td>Paws & Fur</td>
                <td>2</td>
                <td>500.00</td>
                <td>John Smith</td>
                <td>29-MAY-24</td>
            </tr>
        </table>
    </div>
</asp:Content>
