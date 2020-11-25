using System;
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

namespace ListNote
{
    public partial class AddNewQueryDocument : Form
    {
        Account acc = new Account();
        private string username = "";
        private string LinkDocument = "";
        public string Username { get => username; set => username = value; }
        public AddNewQueryDocument()
        {
            InitializeComponent();
           
        }
        
        public void update()
        {
            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Width = 400;
            dataGridView1.Columns[0].Name = "File path";
        }
        private void AddNewQueryDocument_Load(object sender, EventArgs e)
        {
            Screen scr = Screen.PrimaryScreen; //đi lấy màn hình chính
            this.Left = (scr.WorkingArea.Width - this.Width) / 2;
            this.Top = (scr.WorkingArea.Height - this.Height) / 2;
            update();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".pdf,.doc,.xls|*.pdf;*.doc;*.xls| All file (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.dataGridView1.Rows.Add( ofd.FileName);
                LinkDocument = ofd.FileName;
            }
        }
         
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter the document card name", "List Note");
            }
            else
            {
                try
                {
                    SqlConnection Connect = new SqlConnection(acc.query);
                    Connect.Open();
                    string ContainerPath = @".\Document\" + username;
                    if (!Directory.Exists(ContainerPath))
                    {
                        Directory.CreateDirectory(ContainerPath);
                    }
                    File.Copy(LinkDocument, Path.Combine(ContainerPath, textBox1.Text + Path.GetExtension(LinkDocument)), true);
                    string query = "INSERT INTO Tailieu VALUES('" + username + "','" + Path.Combine(ContainerPath, textBox1.Text + Path.GetExtension(LinkDocument)) + "','" + textBox1.Text + Path.GetExtension(LinkDocument) + "');";
                    SqlCommand cmd = new SqlCommand(query, Connect);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Created and added documents successfully !");
                }
                catch
                {
                    MessageBox.Show("This name has existed !!!", "List Note");
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Documents dc = new Documents();
            dc.Username = this.Username;
            this.Close();
            dc.ShowDialog();
        }
    }
 }

