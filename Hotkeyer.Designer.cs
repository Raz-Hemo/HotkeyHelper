namespace hotkeyhelper
{
    partial class Hotkeyer
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hotkeyer));
            this.MainTable = new System.Windows.Forms.DataGridView();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.Hotkey = new hotkeyhelper.DataGridViewHotkeyColumn();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskbarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuitem1 = new System.Windows.Forms.MenuItem();
            this.AddHotkeyBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTable
            // 
            this.MainTable.AllowUserToAddRows = false;
            this.MainTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.MainTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Remove,
            this.Hotkey,
            this.Command});
            this.MainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTable.Location = new System.Drawing.Point(0, 0);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowHeadersVisible = false;
            this.MainTable.Size = new System.Drawing.Size(386, 259);
            this.MainTable.TabIndex = 1;
            this.MainTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainTable_CellClick);
            this.MainTable.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.MainTable_RowAdded);
            this.MainTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainTable_ContentChanged);
            // 
            // Remove
            // 
            this.Remove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Remove.Image = global::hotkeyhelper.Properties.Resources.RemoveBtn;
            this.Remove.Name = "X";
            this.Remove.Width = 53;
            // 
            // Hotkey
            // 
            this.Hotkey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Hotkey.HeaderText = "Hotkey";
            this.Hotkey.Name = "Hotkey";
            this.Hotkey.Width = 66;
            // 
            // Command
            // 
            this.Command.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Command.DefaultCellStyle = dataGridViewCellStyle1;
            this.Command.HeaderText = "Command";
            this.Command.Name = "Command";
            // 
            // TaskbarIcon
            // 
            this.TaskbarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskbarIcon.Icon")));
            this.TaskbarIcon.Text = "Hotkeyer";
            this.TaskbarIcon.Visible = true;
            this.TaskbarIcon.Click += new System.EventHandler(this.NotifyIcon_Click);
            // 
            // menuitem1
            // 
            this.menuitem1.Index = 0;
            this.menuitem1.Text = "Exit";
            this.menuitem1.Click += new System.EventHandler(this.ContextMenuQuit);
            // 
            // AddHotkeyBtn
            // 
            this.AddHotkeyBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AddHotkeyBtn.Location = new System.Drawing.Point(0, 236);
            this.AddHotkeyBtn.Name = "AddHotkeyBtn";
            this.AddHotkeyBtn.Size = new System.Drawing.Size(386, 23);
            this.AddHotkeyBtn.TabIndex = 2;
            this.AddHotkeyBtn.Text = "Add New Hotkey";
            this.AddHotkeyBtn.UseVisualStyleBackColor = true;
            this.AddHotkeyBtn.Click += new System.EventHandler(this.AddHotkeyBtn_Click);
            // 
            // Hotkeyer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 259);
            this.Controls.Add(this.AddHotkeyBtn);
            this.Controls.Add(this.MainTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Hotkeyer";
            this.Text = "Hotkeyer";
            this.Resize += new System.EventHandler(this.Hotkeyer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView MainTable;
        private System.Windows.Forms.NotifyIcon TaskbarIcon;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
        private DataGridViewHotkeyColumn Hotkey;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
        private System.Windows.Forms.MenuItem menuitem1;
        private System.Windows.Forms.Button AddHotkeyBtn;
    }
}

