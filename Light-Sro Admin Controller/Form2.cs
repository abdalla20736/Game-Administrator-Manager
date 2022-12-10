using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Sro_Admin_Controller
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(60,60,60);
            this.Font = new System.Drawing.Font("Snap ITC", 9F);
            this.BringToFront();
            this.Location = new System.Drawing.Point(100, 100);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            int slot = Int32.Parse(RemoveItem.Name.Split('\t')[0]);
            string CharName = RemoveItem.Name.Split('\t')[1].ToString();
            Main._queries.DeleteItem(slot, CharName);
            Main.InventoryInfoList[CharName].RemoveAt(slot);
            Main._gui.ReloadImages_Inv(CharName);
            RemoveItem.Hide();
            
        }
        public string GetID()
        {
            return RemoveItem.Name;
        }
        public void SetID(string Data)
        {
            RemoveItem.Name = Data;
        }

        private void ItemMenuExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
