using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace hotkeyhelper
{
    class DataGridViewHotkeyCell : DataGridViewTextBoxCell
    {
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            // Don't allow non-letter main keys
            if (e.KeyCode < Keys.A || e.KeyCode > Keys.Z) return;

            // Don't allow non-modified hotkeys
            if (!e.Shift && !e.Alt && !e.Control) { MessageBox.Show("Must have at least one modifier!", "Invalid combo"); return; }

            // Build the descriptive string
            StringBuilder shortcutText = new StringBuilder();
            if (e.Control)
                shortcutText.Append("Ctrl+");
            if (e.Shift)
                shortcutText.Append("Shift+");
            if (e.Alt)
                shortcutText.Append("Alt+");

            shortcutText.Append(e.KeyCode.ToString());

            // Disallow duplicate hotkeys
            if (DataGridView.Rows.Cast<DataGridViewRow>()
                .Where(
                    r => r.Cells[ColumnIndex].Value is string &&
                    r.Cells[ColumnIndex].Value.ToString() == shortcutText.ToString())
                .Count() > 0)
            {
                MessageBox.Show("This hotkey is already bound.", "Error");
                return;
            }

            Value = shortcutText.ToString();
        }
    }
}
