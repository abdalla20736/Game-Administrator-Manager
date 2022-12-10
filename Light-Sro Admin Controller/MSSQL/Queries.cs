using Light_Sro_Admin_Controller.Structures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light_Sro_Admin_Controller.Structures;
namespace Light_Sro_Admin_Controller.MSSQL
{
    public class Queries
    {
        private static Queries _Queries = null; 
        public static Queries getInstance()
        {
            if(_Queries == null)
                _Queries = new Queries();
            
            return _Queries;
        }
        #region
        public void DeleteItem(int slot, string CharName)
        {
            using (SqlConnection connection = new SqlConnection(Main.connectionString))
            {
                SqlCommand command = new SqlCommand($"UPDATE SRO_VT_SHARD.._Inventory Set ItemID = 0 Where Slot = {slot} and CharID in (Select CharID FROM SRO_VT_SHARD.._Char Where CharName16 = '{CharName}')", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Donate
        public static void Donate(string CharName, string MainBuild, string SecondaryBuild, int isDevil, int OptLevel, int isShield, int FullBlue, string SetType)
        {
                
                SQLInitializer.ExecuteQuery($"EXEC {Main.ShardDB}..DonationGiver @CharName16 = '{CharName}', @MainBuild = '{MainBuild}',  @SecondaryBuild = '{SecondaryBuild}',@SetType = '{SetType}',    @OptLevel = {OptLevel}, @FullBlue = {FullBlue},	 @IsDevil  = {isDevil},@IS_SHIELD = {isShield}");
                Main.Logger.Success($"The Donation Items Has Been Sent to {CharName} Successfully.");
            
        }
        #endregion

        #region LoadDataIntoLists
        public bool LoadInventoryInfo()
        {
            
            try { 
           // Main.InventoryInfoList = null;
            using (SqlConnection connection = new SqlConnection(Main.connectionString))
            {
                    Main.InventoryInfoList.Clear();
                SqlCommand command = new SqlCommand($"Select CharName16, ID64, RefItemID, Slot, ItemID, AssocFileIcon128, NameStrID128, Rarity, OptLevel from SRO_VT_SHARD.._Char inner join SRO_VT_SHARD.._Inventory on _Inventory.CharID = _Char.CharID inner join SRO_VT_SHARD.._Items on _Items.ID64 = _Inventory.ItemID Inner join SRO_VT_SHARD.._refobjCommon ON _refobjcommon.ID = _items.RefItemID Where Slot < 100 Order by  SRO_VT_SHARD.._Char.CharID,SRO_VT_SHARd.._Inventory.Slot", connection);
                command.Connection.Open();
                    using (SqlDataReader adapter = command.ExecuteReader())
                    {
                        while (adapter.Read())
                        {
                            Slot slot = new();
                            slot.slot = Convert.ToInt32(adapter["Slot"]);
                            slot.RefItemID = Convert.ToInt32(adapter["RefItemID"]);
                            slot.itemID = Convert.ToInt32(adapter["ItemID"]);
                            slot.AssocFileIcon128 = adapter["AssocFileIcon128"].ToString();
                            slot.NameStrID128 = adapter["NameStrID128"].ToString();
                            slot.Rarity = Convert.ToInt32(adapter["Rarity"]);
                            slot.OptLevel = adapter["OptLevel"].ToString();
                            string? CharName16 = adapter["CharName16"]?.ToString();

                            if (!Main.InventoryInfoList.ContainsKey(CharName16))
                            {
                                  Main.InventoryInfoList.Add(CharName16, new List<Slot>());
                            }

                            if (!Main.InventoryInfoList[CharName16].Contains(slot))
                                    Main.InventoryInfoList[CharName16].Add(slot);
                           

                 
                        }
                        
                        
                        return true;
                }
            }
        } catch(Exception ex) {
                Main.Logger.Error($"Loading Database Tables Error : {ex}");
                return false;
           }
}

        public bool LoadSocketData()
        {
         //   Main.PlayerSocketList = null;
            try {

                using (SqlConnection connection = new SqlConnection(Main.connectionString))
                {
                    SqlCommand command = new SqlCommand($"SELECT UserName, AssignedChar, CurIp FROM {Main.FilterDB}.._PlayersStatus Order By CurIp", connection);
                    command.Connection.Open();
                    using (SqlDataReader adapter = command.ExecuteReader())
                    {
                        while (adapter.Read())
                        {
                            PlayerSocketData PlayerSocket = new PlayerSocketData();
                            PlayerSocket.Charname = adapter["AssignedChar"].ToString();
                            PlayerSocket.UserJID = adapter["UserName"].ToString();
                            PlayerSocket.IP_Address = adapter["CurIp"].ToString();
                            Main.PlayerSocketList.Add(PlayerSocket);
                        }
                        return true;
                    }
                }



            } catch(Exception ex) {
                Main.Logger.Error($"Loading Database Tables Error : {ex}");
                return false;
            }
            
        }
        public bool LoadItemData()
        {
           // Main.ItemDataList = null;
            try
            {
                    using (SqlConnection connection = new SqlConnection(Main.connectionString))
                {
                    SqlCommand command = new SqlCommand($"SELECT * FROM {Main.ShardDB}.._RefObjCommon", connection);
                    command.Connection.Open();
                    using (SqlDataReader adapter = command.ExecuteReader())
                    {
                        while (adapter.Read())
                        {
                            ItemData itemData = new ItemData();
            itemData.ID = Convert.ToInt32(adapter["ID"]);       
                                itemData.CodeName128 = adapter["CodeName128"].ToString();
            itemData.Icon = adapter["AssocFileIcon128"].ToString();
                            Main.ItemDataList.Add(itemData);
                        }
                        return true;
                    }
                }

           } catch(Exception ex) {
                Main.Logger.Error($"Loading Database Tables Error : {ex}");
                return false;
           }

        }
        #endregion
    }
}

