using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListNote
{
    public partial class DeletecardForm : Form
    {
        Account acc = new Account();
        public DeletecardForm()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        List<string> list = new List<string>();
        private void DeletecardForm_Load(object sender, EventArgs e)
        {
            Screen scr = Screen.PrimaryScreen; //đi lấy màn hình chính
            this.Left = (scr.WorkingArea.Width - this.Width) / 2;
            this.Top = (scr.WorkingArea.Height - this.Height) / 2;
            update();
            string dirPath = Application.StartupPath;
            IEnumerable<string> enums = Directory.EnumerateDirectories(dirPath);
            List<string> dirs = new List<string>(enums);
            list = new List<string>(enums);
            foreach (var dir in dirs)
            {
                string d = Path.GetFileName(dir);
                this.dataGridView1.Rows.Add(d);
            }
        }
        public void update()
        {
            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[0].Name = "List of document cards";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
      
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Are you sure to want to delete this document card ?", "?", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                string va = dataGridView1.Rows[rowIndex].Cells["List of document cards"].Value.ToString().Trim();
                string directoryPath = Application.StartupPath + @"\" + va;
                DirectoryInfo directory = new DirectoryInfo(directoryPath);
                directory.Delete(true);
                MessageBox.Show("Deleted successfully");
                dataGridView1.Rows.RemoveAt(rowIndex);
            }
            else
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
