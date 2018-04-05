namespace hotkeyhelper
{
    class DataGridViewHotkeyColumn : System.Windows.Forms.DataGridViewTextBoxColumn
    {
        public DataGridViewHotkeyColumn()
        {
            CellTemplate = new DataGridViewHotkeyCell();
        }
    }
}
