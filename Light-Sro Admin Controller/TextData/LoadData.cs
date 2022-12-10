using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Light_Sro_Admin_Controller.TextData
{
    public class LoadData
    {
        private static LoadData _loadTexts;
        public static LoadData getInstance()
        {
            if(_loadTexts == null)
                _loadTexts = new LoadData();
            
            return _loadTexts;
        }
        public static void LoadTextDataObject()
        {
            int i = 0;
            string path = "Data\\Txt\\textdata_object.txt";
            if (File.Exists(path))
            {


                
                var lines = File.ReadLines(path);
                foreach(string line in lines.Where(x => x != null))
                {
                    var Data = line.Split('\t');
                    if (Data[0].ToString() != "1" || line.StartsWith("//") || Data.Length < 10 ||  Data[1].Contains("DESC") || Data[1].Contains("SN_SKILL") )
                        continue;
                    string StrName128 = Data[1].ToString();
                    string StrName128Value;
                   
                    if (Data[5].ToString() != "0")
                        StrName128Value = Data[9].ToString();
                    else
                        StrName128Value = Data[8].ToString();


                    

                    if(!Main.TextDataObject.ContainsKey(StrName128))
                        Main.TextDataObject.Add(StrName128, StrName128Value);



                    //  string service = Data[0];
                    // string NameStr = Data[1];
                    //  string ValueStr = Data[2];
                    //   Main.Logger.Info(service);
                    // Main.Logger.Info(NameStr);
                    //  Main.Logger.Info(ValueStr);

                }
                Main.Logger.Info("TextData_Objects Has Been Loaded");
            }
        }
    }
}
