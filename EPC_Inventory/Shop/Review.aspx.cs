using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace EPC_Inventory.Shop
{
    public partial class Review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Shop_Reg_ID"]))
                {
                    string shopRegID = Request.QueryString["Shop_Reg_ID"];
                    PopulateShopReviewData(shopRegID);
                }
            }
        }

        protected void PopulateShopReviewData(string shopRegID)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                string query = @"SELECT * FROM SHOP_REGISTRATION WHERE SHOP_REG_ID = :ShopRegID";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.BindByName = true;
                        command.Parameters.Add("ShopRegID", OracleDbType.Decimal).Value = Convert.ToDecimal(shopRegID);
                        connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the data to the corresponding labels
                                ShopRegID.Text = reader["SHOP_REG_ID"].ToString();
                                ShopName.Text = reader["SHOP_NAME"].ToString();
                                ShopAddress.Text = reader["SHOP_ADDRESS"].ToString();
                                ShopContactNumber.Text = reader["SHOP_CONTACT_NUMBER"].ToString();
                                ShopEmail.Text = reader["SHOP_EMAIL"].ToString();
                                ShopFirstName.Text = reader["SHOP_OWNER_FN"].ToString();
                                ShopLastName.Text = reader["SHOP_OWNER_LN"].ToString();
                                ShopUsername.Text = reader["SHOP_USERNAME"].ToString();
                                ShopPassword.Text = reader["SHOP_PASSWORD"].ToString();
                                ShopStatusSelect.SelectedValue = reader["SHOP_STATUS"].ToString();
                                ShopDateRegistered.Text = reader["CREATION_DATE"].ToString();

                                LoadAndDisplayImages(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void LoadAndDisplayImages(OracleDataReader reader)
        {
            byte[] requirementsData = reader["SHOP_REQUIREMENTS"] as byte[];
            if (requirementsData != null)
            {
                string requirementsBase64String = Convert.ToBase64String(requirementsData);
                ShopRequirementsPreview.ImageUrl = "data:image;base64," + requirementsBase64String;
            }

            byte[] logoData = reader["SHOP_LOGO"] as byte[];
            if (logoData != null)
            {
                string logoBase64String = Convert.ToBase64String(logoData);
                ShopLogoPreview.ImageUrl = "data:image;base64," + logoBase64String;
            }
        }

        protected void SubmitBtn(object sender, EventArgs e)
        {
            try
            {
                string shopRegID = ShopRegID.Text;
                string shopStatus = ShopStatusSelect.SelectedValue;

                UpdateShopRegistrationStatus(shopRegID, shopStatus);

                if (shopStatus == "Approved")
                {
                    byte[] shopLogoData = GetShopLogoData(shopRegID);
                    InsertShopApproval(shopRegID, shopLogoData);
                    InsertShopProfile(shopLogoData, ShopContactNumber.Text); // Pass ShopContactNumber here
                }

                Response.Redirect("RegistrationList.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void UpdateShopRegistrationStatus(string shopRegID, string shopStatus)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string updateQuery = "UPDATE SHOP_REGISTRATION SET SHOP_STATUS = :ShopStatus WHERE SHOP_REG_ID = :ShopRegID";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.Add("ShopStatus", OracleDbType.Varchar2).Value = shopStatus;
                    updateCommand.Parameters.Add("ShopRegID", OracleDbType.Decimal).Value = Convert.ToDecimal(shopRegID);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        protected byte[] GetShopLogoData(string shopRegID)
        {
            byte[] shopLogoData = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string getLogoQuery = @"SELECT SHOP_LOGO FROM SHOP_REGISTRATION WHERE SHOP_REG_ID = :ShopRegID";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand getLogoCommand = new OracleCommand(getLogoQuery, connection))
                {
                    connection.Open();
                    getLogoCommand.Parameters.Add("ShopRegID", OracleDbType.Decimal).Value = Convert.ToDecimal(shopRegID);
                    object logoResult = getLogoCommand.ExecuteScalar();
                    if (logoResult != DBNull.Value)
                    {
                        shopLogoData = (byte[])logoResult;
                    }
                }
            }
            return shopLogoData;
        }

        protected void InsertShopApproval(string shopRegID, byte[] shopLogoData)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                // Get the next value from the shop_id_seq sequence
                decimal shopID;
                string getNextShopIDQuery = "SELECT shop_id_seq.NEXTVAL FROM DUAL";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand getShopIDCommand = new OracleCommand(getNextShopIDQuery, connection))
                    {
                        object result = getShopIDCommand.ExecuteScalar();
                        if (result != null)
                        {
                            shopID = Convert.ToDecimal(result);
                        }
                        else
                        {
                            // Handle the case when the sequence value is null
                            throw new Exception("Failed to retrieve the next value from the shop_id_seq sequence.");
                        }
                    }
                }

                // Get the next value from the shop_approval_seq sequence
                Random random = new Random();
                int approvalNumber = random.Next(10000000, 99999999);

                // Insert into SHOP_APPROVAL table
                string insertQueryApproval = @"INSERT INTO SHOP_APPROVAL 
                (SHOP_ID, SHOP_APPROVAL_NUMBER, SHOP_REG_ID, SHOP_NAME, 
                SHOP_ADDRESS, SHOP_CONTACT_NUMBER, SHOP_EMAIL, SHOP_OWNER_FN, 
                SHOP_OWNER_LN, SHOP_USERNAME, SHOP_PASSWORD, DATE_UPDATED, 
                SHOP_STATUS, SHOP_LOGO) 
                VALUES 
                (:ShopID, :ShopApprovalNumber, :ShopRegID, :ShopName, 
                :ShopAddress, :ShopContactNumber, :ShopEmail, :ShopOwnerFn, 
                :ShopOwnerLn, :ShopUsername, :ShopPassword, SYSDATE, 
                :ShopStatus, :ShopLogo)";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand insertCommandApproval = new OracleCommand(insertQueryApproval, connection))
                    {
                        insertCommandApproval.Parameters.Add("ShopID", OracleDbType.Decimal).Value = shopID;
                        insertCommandApproval.Parameters.Add("ShopApprovalNumber", OracleDbType.Decimal).Value = approvalNumber;
                        insertCommandApproval.Parameters.Add("ShopRegID", OracleDbType.Decimal).Value = Convert.ToDecimal(shopRegID);
                        insertCommandApproval.Parameters.Add("ShopName", OracleDbType.Varchar2).Value = ShopName.Text;
                        insertCommandApproval.Parameters.Add("ShopAddress", OracleDbType.Varchar2).Value = ShopAddress.Text;
                        insertCommandApproval.Parameters.Add("ShopContactNumber", OracleDbType.Decimal).Value = Convert.ToDecimal(ShopContactNumber.Text);
                        insertCommandApproval.Parameters.Add("ShopEmail", OracleDbType.Varchar2).Value = ShopEmail.Text;
                        insertCommandApproval.Parameters.Add("ShopOwnerFn", OracleDbType.Varchar2).Value = ShopFirstName.Text;
                        insertCommandApproval.Parameters.Add("ShopOwnerLn", OracleDbType.Varchar2).Value = ShopLastName.Text;
                        insertCommandApproval.Parameters.Add("ShopUsername", OracleDbType.Varchar2).Value = ShopUsername.Text;
                        insertCommandApproval.Parameters.Add("ShopPassword", OracleDbType.Varchar2).Value = ShopPassword.Text;
                        insertCommandApproval.Parameters.Add("ShopStatus", OracleDbType.Varchar2).Value = ShopStatusSelect.SelectedValue;
                        insertCommandApproval.Parameters.Add("ShopLogo", OracleDbType.Blob).Value = shopLogoData;

                        insertCommandApproval.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void InsertShopProfile(byte[] shopLogoData, string shopContactNumber)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                // Get the next value from the shop_id_seq sequence
                decimal shopID;
                string getNextShopIDQuery = "SELECT shop_id_seq.NEXTVAL FROM DUAL";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand getShopIDCommand = new OracleCommand(getNextShopIDQuery, connection))
                    {
                        object result = getShopIDCommand.ExecuteScalar();
                        if (result != null)
                        {
                            shopID = Convert.ToDecimal(result);
                        }
                        else
                        {
                            // Handle the case when the sequence value is null
                            throw new Exception("Failed to retrieve the next value from the shop_id_seq sequence.");
                        }
                    }
                }

                // Insert into SHOP_PROFILE table
                string insertProfileQuery = @"INSERT INTO SHOP_PROFILE 
                 (SHOP_ID, SHOP_NAME, SHOP_ADDRESS, SHOP_CONTACT_NUMBER, SHOP_EMAIL, SHOP_PASSWORD, 
                 SHOP_OWNER_FN, SHOP_OWNER_LN, SHOP_USERNAME, SHOP_LOGO) 
                 VALUES 
                 (:ShopID, :ShopName, :ShopAddress, :ShopContactNumber, :ShopEmail, :ShopPassword, 
                 :ShopOwnerFn, :ShopOwnerLn, :ShopUsername, :ShopLogo)";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand insertCommandProfile = new OracleCommand(insertProfileQuery, connection))
                    {
                        insertCommandProfile.Parameters.Add("ShopID", OracleDbType.Decimal).Value = shopID;
                        insertCommandProfile.Parameters.Add("ShopName", OracleDbType.Varchar2).Value = ShopName.Text;
                        insertCommandProfile.Parameters.Add("ShopAddress", OracleDbType.Varchar2).Value = ShopAddress.Text;
                        insertCommandProfile.Parameters.Add("ShopContactNumber", OracleDbType.Decimal).Value = Convert.ToDecimal(ShopContactNumber.Text); // Use passed ShopContactNumber here
                        insertCommandProfile.Parameters.Add("ShopEmail", OracleDbType.Varchar2).Value = ShopEmail.Text;
                        insertCommandProfile.Parameters.Add("ShopPassword", OracleDbType.Varchar2).Value = ShopPassword.Text;
                        insertCommandProfile.Parameters.Add("ShopOwnerFn", OracleDbType.Varchar2).Value = ShopFirstName.Text;
                        insertCommandProfile.Parameters.Add("ShopOwnerLn", OracleDbType.Varchar2).Value = ShopLastName.Text;
                        insertCommandProfile.Parameters.Add("ShopUsername", OracleDbType.Varchar2).Value = ShopUsername.Text;
                        insertCommandProfile.Parameters.Add("ShopLogo", OracleDbType.Blob).Value = shopLogoData;

                        insertCommandProfile.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
