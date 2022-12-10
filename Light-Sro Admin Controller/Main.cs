using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light_Sro_Admin_Controller.Classes;
using Light_Sro_Admin_Controller.MSSQL;
using Light_Sro_Admin_Controller.Structures;
using Light_Sro_Admin_Controller.TextData;

namespace Light_Sro_Admin_Controller
{
    public sealed class Main
    {
        public static Form1 form1 = null;
        public static Form2 ItemMenu = new Form2();
        public static Logger Logger = Logger.getInstance();
        //Data Source=WIN-2GUR2MNP4GH\\SQLEXPRESS;Initial Catalog = SRO_VT_WAXY;uid = dallin;pwd = Root@dallin
        //Data Source=UNKOWN-USER\\SQLEXPRESS1;Initial Catalog = SRO_VT_WAXY;uid = sa;pwd = 3199462
        public static string connectionString = "Data Source=UNKOWN-USER\\SQLEXPRESS1;Initial Catalog = SRO_VT_WAXY;uid = sa;pwd = 3199462";
        public static Queries _queries = Queries.getInstance();
        public static LoadData _LoadTexts = LoadData.getInstance();
        public static GUI _gui = GUI.getInstance();
        public static string ShardDB = "SRO_VT_SHARD";
        public static string WAXYDB  = "SRO_VT_WAXY";
        public static string FilterDB = "wFilter";
        public static Extra _extra = new Extra();
        public static List<ItemData> ItemDataList = new List<ItemData>();
        public static Dictionary<string, List<Slot>> InventoryInfoList = new();
        public static List<PlayerSocketData> PlayerSocketList = new List<PlayerSocketData>();
        public static Dictionary<string,string> TextDataObject = new Dictionary<string,string>();


    }
}
