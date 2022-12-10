using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light_Sro_Admin_Controller.Structures
{
    public class Slot
    {
        public int slot { get; set; }
        public int itemID { get; set; }
        public string NameStrID128 { get; set; }
        public int RefItemID { get; set; }
        public string OptLevel { get; set; }
        public int Rarity { get; set; }
        public Point location { get; set; }
        public string AssocFileIcon128 { get; set; }
        public bool Is_Selected = false;
        public PictureBox Data = null;
        
    }
}
