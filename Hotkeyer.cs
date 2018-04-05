using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace hotkeyhelper
{
    public partial class Hotkeyer : Form
    {
        const string CFG_PATH = "hotkeyer.cfg";
        bool formInitialized = false;

        // Column IDs
        const int REMOVE_COLUMN_ID = 0;
        const int HOTKEY_COLUMN_ID = 1;
        const int COMMAND_COLUMN_ID = 2;
        
        public Hotkeyer()
        {
            InitializeComponent();
            HotkeyManager.HotkeyPressed += new EventHandler<HotkeyEventArgs>(HotkeyManager_HotkeyPressed);

            // Read the saved hotkeys file (make sure it exists first)
            File.Open(CFG_PATH, FileMode.Append).Close();
            using (StreamReader fs = new StreamReader(CFG_PATH, true))
            {
                string line;
                while ((line = fs.ReadLine()) != null)
                {
                    // New row
                    MainTable.Rows.Insert(MainTable.Rows.Count, 1);
                    var lastRow = MainTable.Rows.Cast<DataGridViewRow>().Last();

                    // Split the hotkey and shell command, fill new row
                    int first_space = line.IndexOf(' ');
                    lastRow.Cells[HOTKEY_COLUMN_ID].Value = line.Substring(0, first_space);
                    lastRow.Cells[COMMAND_COLUMN_ID].Value = line.Substring(first_space + 1);
                }
            }

            formInitialized = true;
            MainTable_ContentChanged(null, null);
        }

        private void HotkeyManager_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            // Find all matching hotkeys and execute their shell commands
            foreach (DataGridViewRow row in MainTable.Rows.Cast<DataGridViewRow>()
                .Where(r =>
                {
                    HotkeyDesc desc = new HotkeyDesc(r.Cells[HOTKEY_COLUMN_ID].Value.ToString());
                    return desc.mods == (e.Modifiers | KeyModifiers.NoRepeat) && desc.key == e.Key;
                }))
                System.Diagnostics.Process.Start(row.Cells[COMMAND_COLUMN_ID].Value.ToString());
        }

        private void MainTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Clicking the first column (red x) means deleting it
            if (e.ColumnIndex == REMOVE_COLUMN_ID)
                MainTable.Rows.RemoveAt(e.RowIndex);
        }

        private void MainTable_ContentChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Prevent this from firing at startup and deleting the config
            if (!formInitialized) return;

            List<HotkeyDesc> HotkeyList = new List<HotkeyDesc>();

            // Save the new hotkeys back to the file
            using (FileStream fs = File.Create(CFG_PATH))
            {
                foreach (DataGridViewRow row in MainTable.Rows)
                {
                    // Build line and write it to file
                    byte[] text = new UTF8Encoding(true).GetBytes(new StringBuilder()
                        .Append(row.Cells[HOTKEY_COLUMN_ID].Value)
                        .Append(' ')
                        .Append(row.Cells[COMMAND_COLUMN_ID].Value)
                        .Append('\n')
                        .ToString());
                    fs.Write(text, 0, text.Length);

                    // Add to list of hotkeys
                    if (row.Cells[HOTKEY_COLUMN_ID].Value is string) 
                        HotkeyList.Add(new HotkeyDesc(row.Cells[HOTKEY_COLUMN_ID].Value.ToString()));
                }
            }

            // Replace old hotkey bindings with new ones
            HotkeyManager.Hotkeys = HotkeyList;
        }

        private void MainTable_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                MainTable.Rows[i].Cells[REMOVE_COLUMN_ID].Value = Properties.Resources.RemoveBtn;
        }

        // Minimize to taskbar
        private void Hotkeyer_Resize(object sender, EventArgs e)
        {   
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        // Maximize from taskbar
        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ContextMenuQuit(object sender, EventArgs e)
        {
            Close();
        }

        // Add new row button
        private void AddHotkeyBtn_Click(object sender, EventArgs e)
        {
            MainTable.Rows.Insert(MainTable.Rows.Count, 1);
        }
    }
}
