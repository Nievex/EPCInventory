using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace EPC_Inventory.Seller
{
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SHOP_ID"] != null)
                {
                    int shopId = Convert.ToInt32(Session["SHOP_ID"]);
                    LoadSalesReport(shopId);
                    LoadTransactionHistory(shopId);
                }
                else
                {
                    Response.Redirect("../ShopLogin.aspx");
                }
            }
        }

        private void LoadSalesReport(int shopId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT SUM(CASE WHEN ORDER_STATUS = 'Approved' THEN QTY ELSE 0 END) AS TOTAL_PRODUCTS_SOLD, SUM(CASE WHEN ORDER_STATUS = 'Approved' THEN SUBTOTAL ELSE 0 END) AS TOTAL_PROFIT " +
                           "FROM ORDERS WHERE SHOP_ID = :ShopId";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int totalProductsSold = Convert.ToInt32(reader["TOTAL_PRODUCTS_SOLD"]);
                        decimal totalProfit = Convert.ToDecimal(reader["TOTAL_PROFIT"]);

                        decimal monthlyProfit = CalculateMonthlyProfit(shopId);
                        decimal yearlyProfit = CalculateYearlyProfit(shopId);

                        // Update the sales card values
                        ProductsSoldValue.Text = totalProductsSold.ToString();
                        MonthlySalesValue.Text = monthlyProfit.ToString("₱0.00");
                        YearlySalesValue.Text = yearlyProfit.ToString("₱0.00");
                    }

                    reader.Close();
                }
            }
        }

        private decimal CalculateMonthlyProfit(int shopId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT SUM(CASE WHEN ORDER_STATUS = 'Approved' AND TRUNC(ORDER_DATE, 'MM') = TRUNC(SYSDATE, 'MM') THEN SUBTOTAL ELSE 0 END) AS MONTHLY_PROFIT " +
                           "FROM ORDERS WHERE SHOP_ID = :ShopId";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        private decimal CalculateYearlyProfit(int shopId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT SUM(CASE WHEN ORDER_STATUS = 'Approved' AND TRUNC(ORDER_DATE, 'YYYY') = TRUNC(SYSDATE, 'YYYY') THEN SUBTOTAL ELSE 0 END) AS YEARLY_PROFIT " +
                           "FROM ORDERS WHERE SHOP_ID = :ShopId";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        private void LoadTransactionHistory(int shopId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT ORDER_ID, PRODUCT_NAME, QTY, SUBTOTAL, CUSTOMER_NAME, ORDER_DATE, ORDER_STATUS " +
                           "FROM ORDERS WHERE SHOP_ID = :ShopId";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    TransactionHistoryRepeater.DataSource = dataTable;
                    TransactionHistoryRepeater.DataBind();
                }
            }
        }
    }
}