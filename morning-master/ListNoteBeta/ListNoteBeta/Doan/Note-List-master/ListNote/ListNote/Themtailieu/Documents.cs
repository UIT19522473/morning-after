using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
namespace ListNote
{
    public partial class Documents : Form
    {
        Account acc = new Account();
        private string username = "";

        public string Username { get => username; set => username = value; }

        public Documents()
        {
            InitializeComponent();
        }
        private void LoadDocumentList()
        {
            dataGridView1.Rows.Clear();
            SqlConnection Connect = new SqlConnection(acc.query);
            Connect.Open();
            string query = "SELECT DOCUMENTNAME FROM Tailieu WHERE USERNAME = '" + this.username + "';";
            SqlCommand cmd = new SqlCommand(query, Connect);
            SqlDataReader r = cmd.ExecuteReader();
            while(r.HasRows)
            {
                if (r.Read() == false) break;
                dataGridView1.Rows.Add(r.GetString(0));
            }
            Connect.Close();
        }
       

  
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Documents_Load(object sender, EventArgs e)
        {
            Screen scr = Screen.PrimaryScreen; //đi lấy màn hình chính
            this.Left = (scr.WorkingArea.Width - this.Width) / 2;
            this.Top = (scr.WorkingArea.Height - this.Height) / 2;
            LoadDocumentList();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Connect = new SqlConnection(acc.query);
                Connect.Open();
                string query = "SELECT Link FROM Tailieu WHERE USERNAME = '" + this.username + "' AND DOCUMENTNAME = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "';";
                SqlCommand cmd = new SqlCommand(query, Connect);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Process.Start(r.GetString(0));
                }
                Connect.Close();
            }
            catch
            {
                MessageBox.Show("There is no document to open !","List Note");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Connect = new SqlConnection(acc.query);
                Connect.Open();
                string query2 = "SELECT Link FROM Tailieu WHERE USERNAME = '" + this.username + "' AND DOCUMENTNAME = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "';";
                SqlCommand cmd2 = new SqlCommand(query2, Connect);
                SqlDataReader r2 = cmd2.ExecuteReader();
                while (r2.Read())
                {
                    File.Delete(r2.GetString(0));
                }
                r2.Close();
                string query = "DELETE FROM Tailieu WHERE USERNAME = '" + this.username + "' AND DOCUMENTNAME = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "';";
                SqlCommand cmd = new SqlCommand(query, Connect);
                int r = cmd.ExecuteNonQuery();

                LoadDocumentList();
            }
            catch
            {
                MessageBox.Show("There is no document to delete !", "List Note");
            }

        }
    }
}
