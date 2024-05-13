using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;

namespace EPC_Inventory
{
    public partial class RegistrationList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProcessRegistrationData();
            }
        }

        protected void ProcessRegistrationData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    string query = @"SELECT * FROM SHOP_REGISTRATION WHERE SHOP_STATUS = 'Pending'";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);
                                ShopRegistrationListRepeater.DataSource = dataTable;
                                ShopRegistrationListRepeater.DataBind();
                            }    
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        protected bool CheckIfEntryExists(int shopRegId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT COUNT(*) FROM SHOP_APPROVAL WHERE SHOP_REG_ID = :ShopRegId";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("ShopRegId", OracleDbType.Decimal).Value = shopRegId;
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                return false;
            }
        }

        protected string GetBase64Image(object imageData)
        {
            if (imageData == DBNull.Value)
            {
                return "";
            }

            byte[] bytes = (byte[])imageData;
            string base64String = Convert.ToBase64String(bytes);
            string imageUrl = "data:image/jpeg;base64," + base64String;
            return imageUrl;
        }
    }
}
