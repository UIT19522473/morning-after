using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ListNote
{
    public partial class ListEvent : Form
    {
        Account acc = new Account();
        string Admin = "";
       // DateTime dayTime;
        string InDTPK = "";

        public string TileYMD;
        public string TileYMD1 { get => TileYMD; set => TileYMD = value; }

        string CurDay, CurMonth, CurYear;

        DataTable tb = new DataTable();

        

        public ListEvent()
        {
            InitializeComponent();
        }
        //DailyEvent dlev = new DailyEvent("tuan", dtpkDate.Value, (sender as Button).Text);

        public ListEvent(string User,DateTime dayUser, string day)
        {
            InitializeComponent();
            Admin = User;
            InDTPK = InDTPK + dayUser.Month.ToString() + "/" + day + "/" + dayUser.Year.ToString();

            CurDay = day;   CurMonth = dayUser.Month.ToString();    CurYear = dayUser.Year.ToString();

        }

        public void LoadData()
        {
            
            string query = acc.query;
            SqlConnection connection = new SqlConnection(query);
            connection.Open();
            string cmdquery = "SELECT * FROM DailyEvent WHERE InUser = '" + Admin + "' and InDTPK = '"+ InDTPK +"'" ;
            //MessageBox.Show(cmdquery);
            SqlCommand cmd = new SqlCommand(cmdquery, connection);
            
            try
            {
                SqlDataAdapter read = new SqlDataAdapter(cmdquery, connection);
                read.Fill(tb);

                connection.Close();
                //this.dataGridView1.Columns
               // MessageBox.Show(tb.Rows.Count.ToString());
                for(int i = 0; i<tb.Rows.Count; i++)
                {
                    DataGridViewRow row =(DataGridViewRow)dgvShow.Rows[i].Clone();

                    //dgvShow.Rows[i].Cells[0].Value = tb.Rows[i][0];
                    //dgvShow.Rows[i].Cells[1].Value = tb.Rows[i][3];
                    row.Cells[0].Value = tb.Rows[i][0];
                    row.Cells[1].Value = tb.Rows[i][3];
                    dgvShow.Rows.Add(row);
                }
                

                //DataGridViewRow row = (DataGridViewRow)yourDataGridView.Rows[0].Clone();
                //row.Cells[0].Value = "XYZ";
                //row.Cells[1].Value = 50.2;
                //yourDataGridView.Rows.Add(row);

            }
            catch
            {

            }
            //SqlDataReader r = cmd.ExecuteReader();

           
            //return false;
            //if (r.Read() == true)
            //    return true;
            //return false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ListEvent_Load(object sender, EventArgs e)
        {
            LoadData();
            this.lbTile.Text = TileYMD;
        }

        private void dgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int curIndex = dgvShow.CurrentRow.Index;
                DailyEvent DlEv = new DailyEvent(Admin, InDTPK, tb.Rows[curIndex][11].ToString());
                this.Hide();
                DlEv.ShowDialog();
               // Calendar Cldar = new Calendar();
                //Cldar.ShowDialog();
                this.Close();
            }
            catch
            {

            }
            
           // MessageBox.Show(curIndex.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DailyEvent DlEv = new DailyEvent(Admin,InDTPK);
            DlEv.CurYear = CurYear;
            DlEv.CurDay = CurDay;
            DlEv.CurMonth = CurMonth;
            this.Hide();
            DlEv.ShowDialog();
            this.Close();
        }

        /*
         * void LoadSql()
        {
            try
            {
                DataTable tb = new DataTable();
                SqlConnection sql = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DTBEvent;Integrated Security=True");
                sql.Open();
                MessageBox.Show("select *from DailyEvent where InDTPK = '" + inDTPK + "' and InUser = '" + InUser + "'");
                SqlDataAdapter read = new SqlDataAdapter("select *from DailyEvent where InDTPK = '"+inDTPK+"' and InUser = '"+InUser+"'", sql);
                read.Fill(tb);
                sql.Close();


                cmbReminder.Text = tb.Rows[0][2].ToString();
                txtEventName.Text = tb.Rows[0][3].ToString();
                txtlocation.Text = tb.Rows[0][4].ToString();
                txtStartEvent.Text = tb.Rows[0][5].ToString();
                txtEndEvent.Text = tb.Rows[0][6].ToString();
                cmbTimeStart.Text = tb.Rows[0][7].ToString();
                cmbTimeEnd.Text = tb.Rows[0][8].ToString();
                txtEventDcrt.Text = tb.Rows[0][10].ToString();

                if (tb.Rows[0][9].ToString() == "1")
                {
                    ckbAllDay.Checked = true;
                }
                else
                {
                    ckbAllDay.Checked = false;
                }
            }
            catch
            {
                MessageBox.Show("Chua Co Du lieu");
            }

        }
         */

    }
}
