namespace Light_Sro_Admin_Controller
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RemoveItem = new System.Windows.Forms.Button();
            this.ItemMenuExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RemoveItem
            // 
            this.RemoveItem.Location = new System.Drawing.Point(12, 12);
            this.RemoveItem.Name = "RemoveItem";
            this.RemoveItem.Size = new System.Drawing.Size(75, 23);
            this.RemoveItem.TabIndex = 0;
            this.RemoveItem.Text = "Remove";
            this.RemoveItem.UseVisualStyleBackColor = true;
            this.RemoveItem.Click += new System.EventHandler(this.RemoveItem_Click);
            // 
            // ItemMenuExit
            // 
            this.ItemMenuExit.Location = new System.Drawing.Point(93, 12);
            this.ItemMenuExit.Name = "ItemMenuExit";
            this.ItemMenuExit.Size = new System.Drawing.Size(75, 23);
            this.ItemMenuExit.TabIndex = 1;
            this.ItemMenuExit.Text = "Exit";
            this.ItemMenuExit.UseVisualStyleBackColor = true;
            this.ItemMenuExit.Click += new System.EventHandler(this.ItemMenuExit_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 47);
            this.ControlBox = false;
            this.Controls.Add(this.ItemMenuExit);
            this.Controls.Add(this.RemoveItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Item";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button RemoveItem;
        private Button ItemMenuExit;
    }
}