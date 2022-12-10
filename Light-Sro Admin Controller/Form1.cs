using Light_Sro_Admin_Controller.Classes;
using Light_Sro_Admin_Controller.MSSQL;
using Light_Sro_Admin_Controller.Structures;
using Light_Sro_Admin_Controller.TextData;
using System.Text;
using System.Windows.Media.Imaging;

namespace Light_Sro_Admin_Controller
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Main.form1 = this;
            Main.Logger.LogSetter(LoggerView);
            Main._gui.InventoryPanelSetter(InventoryView);
            SQLInitializer.Connection();
            SQLInitializer.LoadData();
            LoadData.LoadTextDataObject();
            Main.Logger.Info("Welcome to Light-Sro Controller");
            ItemsComboBox.ValueMember = "CodeName128";
            ItemsComboBox.DisplayMember = "CodeName128";
            ItemsComboBox.DataSource = Main.ItemDataList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(41, 44, 51);
            tabPage1.ForeColor = System.Drawing.Color.FromArgb(41, 44, 51);
            tabPage2.ForeColor = System.Drawing.Color.FromArgb(41, 44, 51);
            tabControl1.TabPages[2].BackColor = System.Drawing.Color.FromArgb(41, 44, 51);
            LoggerView.View = View.Details;
            LoggerView.Columns[0].Width = 150;
            LoggerView.Columns[1].Width = 1000;
            LoggerView.FullRowSelect = true;
            SocketDataView.View = View.Details;
            SocketDataView.MultiSelect = true;

            SocketDataView.Columns[0].Width = 50;
            SocketDataView.Columns[1].Width = 290;
            SocketDataView.Columns[2].Width = 290;
            SocketDataView.Columns[3].Width = 290;
            SocketDataView.FullRowSelect = true;
            SocketDataView.ContextMenuStrip = ListviewRMenu;
            ToolStripMenuItem CopyOption = new ToolStripMenuItem();
            CopyOption.Name = "Copybtn";
            CopyOption.Text = "Copy";
            CopyOption.Click += CopyOption_ItemClicked;
            ListviewRMenu.Items.Add(CopyOption);

        }

        private void CopyOption_ItemClicked(object sender, EventArgs e)
        {
            if(SocketDataView.SelectedItems.Count > 0)
             Main._extra.CopySelectedValuesToClipboard(SocketDataView);
        }

        

     

        private void button1_Click(object sender, EventArgs e)
        {
  
          
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        
        public int StringToInteger(ComboBox combobox)
        {
            return Convert.ToInt32(bool.Parse(combobox.Text));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
             
            if(comboBox1.SelectedItem == null )
            { 
                Main.Logger.Warning("Please, Select Main Build");
                return;
            }
            if (comboBox2.SelectedItem == null)
            {
                Main.Logger.Warning("Please, Select Secondary Build");
                return;
            }
            if (comboBox3.SelectedItem == null)
            {
                Main.Logger.Warning("Please, Select Devil Value");
                return;
            }
            if (comboBox4.SelectedItem == null)
            {
                Main.Logger.Warning("Please, Select Plus Value");
                return;
            }
            if (comboBox5.SelectedItem == null)
            {
                Main.Logger.Warning("Please, Select Shield Value");
                return;
            }
            if (comboBox6.SelectedItem == null)
            {
                Main.Logger.Warning("Please, Select Blue Value");
                return;
            }
            if (comboBox7.SelectedItem == null)
            {
                Main.Logger.Warning("Please, Select Set Value");
                return;
            }
            DialogResult dialog = MessageBox.Show($"The Donation Items Will be sent to {textBox1.Text} ", "Are You Sure ?", MessageBoxButtons.YesNo);


            if (dialog == DialogResult.Yes)
                Queries.Donate(textBox1.Text, comboBox1.Text, comboBox2.Text, StringToInteger(comboBox3), Convert.ToInt32(comboBox4.Text), StringToInteger(comboBox5), StringToInteger(comboBox6), comboBox7.Text);
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main._gui.InventoryResetLocation();
            Main._gui.LoadImages_Inv(InventoryView, getinv_namebox.Text);
         //   Queries.FindInventory(textBox2.Text);
        }

        private void LoadCharactersbtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            LoadCharactersbtn.Text = "Reload Characters";

            if (SocketDataView.Items.Count > 0)
                SocketDataView.Items.Clear();

            foreach (var PlayerSocketData in Main.PlayerSocketList)
            {
                i++;
                if (PlayerSocketData.Charname.Contains(IP_FieldBox.Text) && IPorName.Text == "Character Name")
                    SocketDataView.Items.Add(new ListViewItem(new[] { i.ToString(), PlayerSocketData.IP_Address, PlayerSocketData.UserJID, PlayerSocketData.Charname }));
                if (PlayerSocketData.IP_Address.Contains(IP_FieldBox.Text) && IPorName.Text == "IP")
                    SocketDataView.Items.Add(new ListViewItem(new[] { i.ToString(), PlayerSocketData.IP_Address, PlayerSocketData.UserJID, PlayerSocketData.Charname }));

            }

        }

        private void SocketDataView_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void SendRewardBtn_Click(object sender, EventArgs e)
        {

            Main._queries.LoadInventoryInfo();
        
            if (SocketDataView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem Char in SocketDataView.SelectedItems)
                {
                    string? Name = Char.SubItems[3]?.Text;

                    switch (RewardCombo.Text)
                    {
                        case "Gold":
                            if (Name == string.Empty)
                                break;

                            SQLInitializer.ExecuteQuery($"EXEC {Main.WAXYDB}.._RewardPerIP '{Name}','0',1,{RewardAmmo.Value}");
                            Main.Logger.Warning($"{RewardAmmo.Value} Gold Has Been Added to {Name}");

                            break;
                        case "Silk":
                            string? UserName = Char.SubItems[2]?.Text;
                            if (Name == string.Empty)
                                break;

                            SQLInitializer.ExecuteQuery($"EXEC {Main.WAXYDB}.._RewardPerIP '{UserName}','0',2,{RewardAmmo.Value}");
                            Main.Logger.Warning($"{RewardAmmo.Value} Silk Has Been Added to {Name}");

                            break;
                        default:
                            if (Name == string.Empty)
                                break;

                            var PlayerInvInfo = Main.InventoryInfoList.TryGetValue(Name, out var records);
                            if (PlayerInvInfo && records != null)
                            {
                                bool Is_empty_slot = false;
                                foreach (var slot in records)
                                {
                                    if (slot.itemID == 0)
                                    {
                                        Is_empty_slot = true;
                                        break;
                                    }
                                }
                                if (Is_empty_slot)
                                {
                                    SQLInitializer.ExecuteQuery($"EXEC {Main.WAXYDB}.._RewardPerIP '{Name}','{ItemsComboBox.Text}',3,{RewardAmmo.Value}");
                                    Main.Logger.Warning($"The Selected Item Has Been Added to {Name} ");
                                }
                                else
                                    Main.Logger.Warning($"{Name}'s inventory is Full!!");

                            }
                            else
                                Main.Logger.Warning($"Couldn't find The Character Info");


                            break;
                    }
                }

            }
            
          //  SQLInitializer.ExecuteQuery("EXEC ");
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void RewardCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RewardCombo.SelectedItem.ToString() == "Item")
            {
                ItemsComboBox.Visible = true;
                item_codeLabel.Visible = true;
            }
            else
            {
                ItemsComboBox.Visible = false;
                item_codeLabel.Visible = false;
            }
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Main._gui.InventoryPageSetter(1);
            Main._gui.ReloadImages_Inv(GUI.CharName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main._gui.InventoryPageSetter(2);
            Main._gui.ReloadImages_Inv(GUI.CharName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main._gui.InventoryPageSetter(3);
            Main._gui.ReloadImages_Inv(GUI.CharName);
        }
    }
}