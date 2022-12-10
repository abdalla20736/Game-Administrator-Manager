using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light_Sro_Admin_Controller.Classes
{

    public class GUI
    {
        private static GUI GUI_instance;
        private Panel InventoryPanel;
        Point location;
        public static int Page = 1;
        Panel ItemData = new Panel();
        private int Y;
        public static string CharName;

        public static GUI getInstance()
        {
            if(GUI_instance == null)
            {
                GUI_instance = new GUI();
            }

            return GUI_instance;
        }

        public void InventoryPageSetter(int page)
        {
            Page = page;
        }
        public void InventoryPanelSetter(Panel panel1)
        {
            InventoryPanel = panel1;
        }
        public void GetSlot_Inv(int slot)
        {
            if (slot == 45 || slot == 77)
            {
                location.X = 0;
                location.Y = -25;
            }
                
            // 0 % 4 = 1
            Y = ((slot-1) % 4);

            if (Y == 0)
            {
                location.Y += 36;
                location.X  = 18;
            }
            else
                location.X += 35; 

        }
        void Item_MouseClick(object sender, EventArgs e)
        {
            var Item = (PictureBox)sender;
            Main.ItemMenu.SetID(Item.Name);
            Main.ItemMenu.Show();
        }
        void Item_MouseEnter(object sender, EventArgs e)
        {
            ItemData = new();
            var Item = (PictureBox)sender;
            var Data = Item.Name.Split('\t');
            ItemData.BackColor = Color.FromArgb(24, 30, 57);
            ItemData.Size = new Size(200, 200);
            ItemData.ForeColor = Color.White;
            
            if (Data[3].ToString() == "2")
               ItemData.ForeColor = Color.Yellow;
            ItemData.Location = new Point(Item.Location.X + 650, Item.Location.Y + 80);
            Label ItemName = new Label();
            string ItemNameText;
            Main.TextDataObject.TryGetValue(Data[2], out ItemNameText);
            ItemName.Text = ItemNameText;
            //ItemName.Font = new System.Drawing.Font("Snap ITC", 9F);
            ItemName.Location = new Point(ItemName.Location.X+5,ItemName.Location.Y+10);
            ItemName.Text = ItemNameText;
            if (Convert.ToInt32(Data[4]) > 0)
                ItemName.Text += $" + {Data[4]}"; 
            //ItemName.Hide();
            ItemData.Controls.Add(ItemName);
            Main.form1.Controls.Add(ItemData);
            ItemData.Show();
            ItemData.BringToFront();
        }
        void Item_MouseLeave(object sender, EventArgs e)
        {
            var Item = (PictureBox)sender;
            
            Main.form1.Controls.Remove(ItemData);
            ItemData.Dispose();
        }
        public Panel InventoryViewGetter()
        {
            return InventoryPanel;
        }
        public void InventoryResetLocation()
        {
            location = new Point(-10, -133);
        }
        #region ReloadItemsInInventory
        public void ReloadImages_Inv(string Character_name)
        {
            InventoryPanel.Controls.Clear();
            int minimum = 0,maximum = 0;

            switch (Page)
            {
                case 1:
                    minimum = 13;
                    maximum = 44;
                    break;
                case 2:
                    minimum = 45;
                    maximum = 76;
                    break;
                case 3:
                    minimum = 77;
                    maximum = 108;
                    break;

            }

            if (Main.InventoryInfoList.ContainsKey(Character_name))
            {
                foreach (Structures.Slot Value in Main.InventoryInfoList[Character_name])
                {


                    if (Value.slot < minimum)
                        continue;
                    if (Value.slot == maximum)
                        break;
                    
             

                    if (Value.itemID != 0)
                    {
                        Value.Data.Name = $"{Value.slot}\t{Character_name}\t{Value.NameStrID128}\t{Value.Rarity}\t{Value.OptLevel}";

                        // Value.Data = null;

                        string path = $"Data\\icon\\{Value.AssocFileIcon128.Replace(".ddj", "")}.jpg";
                       
                        Value.Data.MouseDoubleClick += new MouseEventHandler(Item_MouseClick);
                        //    Value.Data.MouseLeave += new EventHandler(Item_MouseLeave);
                        Value.Data.Width = 32;
                        Value.Data.Height = 32;
                        Value.Data.Location = Value.location;
                        // Value.Data.FlatStyle = FlatStyle.Flat;1
                        if (File.Exists(path))
                            Value.Data.Image = Image.FromFile(path);
                        else
                            Value.Data.Image = Image.FromFile("Data\\icon\\icon_default.jpg");
                        InventoryPanel.Controls.Add(Value.Data);
                        Value.Data.BringToFront();

                    }
                }
            }
            else
                MessageBox.Show($"{Character_name} Not Found");
        }
        #endregion
        public void LoadImages_Inv(Panel Inventory, string Character_name)
        {
            InventoryPanel = Inventory;
            Inventory.Controls.Clear();
            if (Main.InventoryInfoList.ContainsKey(Character_name))
            {
                foreach (Structures.Slot Value in Main.InventoryInfoList[Character_name])
                {
                   
                    //if (Value.slot >= 32)
                      //  continue;

                    Value.Data = new PictureBox();


                    GetSlot_Inv(Value.slot);


                    Value.Data.Location = location;

                    if (Value.itemID != 0)
                    {
                        Value.Data.Name = $"{Value.slot}\t{Character_name}\t{Value.NameStrID128}\t{Value.Rarity}\t{Value.OptLevel}";

                        // Value.Data = null;

                        string path = $"Data\\icon\\{Value.AssocFileIcon128.Replace(".ddj", "")}.jpg";
                        Value.Data.MouseDoubleClick += new MouseEventHandler(Item_MouseClick);
                        Value.Data.MouseEnter += new EventHandler(Item_MouseEnter);

                        Value.Data.MouseLeave += new EventHandler(Item_MouseLeave);
                        Value.Data.Width = 32;
                        Value.Data.Height = 32;
                        Value.location = location;
                        // Value.Data.FlatStyle = FlatStyle.Flat;1
                        if (File.Exists(path))
                            Value.Data.Image = Image.FromFile(path);
                        else
                            Value.Data.Image = Image.FromFile("Data\\icon\\icon_default.jpg");
                        // Inventory.Controls.Add(Value.Data);
                        // Value.Data.BringToFront();
                        

                    }

                }
                Main.Logger.Success($"{Character_name} Inventory Loaded Sucessfully");
                CharName = Character_name;
            }
            else
                MessageBox.Show($"{Character_name} Not Found");
        }

    }
}
