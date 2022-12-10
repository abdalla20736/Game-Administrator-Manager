using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Light_Sro_Admin_Controller.Classes;
namespace Light_Sro_Admin_Controller.MSSQL
{

    internal class SQLInitializer
    {
        
        public static void LoadData()
        {
            if (!Main._queries.LoadItemData())
            {
                Main.Logger.Error("Failed to Load ItemData");
               // throw new Exception("Failed to Load ItemData");
            }
            if (!Main._queries.LoadInventoryInfo())
            {
                Main.Logger.Error("Failed to Load Player's Inventories");
                return;
            }
            if (!Main._queries.LoadSocketData())
            {
                Main.Logger.Error("Failed to Load Player's Inventories");
                return;
            }

        }

        public static void ExecuteQuery(string queryString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Main.connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();

                }
            }catch (Exception ex)
            {
               
                
                 Main.Logger.Error($"SQL Server Query Faulty.\n Exception : {ex}");
                 
            }
        }
      

        public static void Connection()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(Main.connectionString))
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        Main.Logger.Error($"Connection is closed");
                        return;
                    }
                    else
                    {
                        Main.Logger.Success("SQL Has Been Connected Successfully");
                    }
                        
                }
            }
            catch (Exception ex)
            {


                Main.Logger.Error($"SQL Server Connection Faulty.\n Exception : {ex}");

            }

        }


    }
}
