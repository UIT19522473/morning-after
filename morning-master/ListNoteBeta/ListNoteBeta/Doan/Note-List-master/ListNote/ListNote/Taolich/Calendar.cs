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
    public partial class Calendar : Form
    {
        protected Account curAcc;
        #region Peoperties


        string InUser ="";

        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        public Account CurAcc { get => curAcc; set => curAcc = value; }
        

        private List<string> dateOfWeek = new List<string>() { "Monday"
        ,"Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};
        #endregion
        public Calendar()
        {
            CurAcc = new Account();
            InitializeComponent();
        }
        private void Calendar_Load(object sender, EventArgs e)
        {
            InUser = curAcc.Username;
            LoadMatrix();
        }
       // string InUser = curAcc.Username;

        void LoadMatrix()
        {
            Matrix = new List<List<Button>>();
            Button oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.Margin, 0) };
            for (int i = 0; i < Cons.DayOffColumn; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.DayOfweek; j++)
                {
                    Button a = new Button() { Height = Cons.Height, Width = Cons.Width };

                    a.FlatAppearance.BorderSize = 1;
                    a.FlatAppearance.BorderColor = Color.LightSkyBlue;
                    a.TextAlign = ContentAlignment.TopLeft;
                    a.FlatStyle = FlatStyle.Flat;

                    a.Location = new Point(oldBtn.Location.X + oldBtn.Width + Cons.Margin, oldBtn.Location.Y);

                    a.Click += A_Click;

                    pnMatrix.Controls.Add(a);
                    Matrix[i].Add(a);
                    oldBtn = a;

                }
                oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.Margin, oldBtn.Location.Y + Cons.Height) };
            }

            SetDefaultDate();
        }

        private void A_Click(object sender, EventArgs e)
        {
            if((sender as Button).Text!="")
            {

                ListEvent LEvent = new ListEvent(curAcc.Username, this.dtpkDate.Value, (sender as Button).Text);
                
                LEvent.Location = new Point((sender as Button).Location.X+150, (sender as Button).Location.Y);

                string s = "";
                DateTime test = new DateTime(this.dtpkDate.Value.Year,this.dtpkDate.Value.Month, Int32.Parse((sender as Button).Text.ToString()));
                s = s + test.DayOfWeek.ToString() + ", " + NameMonth(dtpkDate.Value.Month.ToString()) + " " + (sender as Button).Text.ToString() + ", " + dtpkDate.Value.Year.ToString();
                LEvent.TileYMD = s;
                LEvent.ShowDialog();
                SetDefaultDate();
                this.Show();
            }
        }

        
        void AddNumberIntoMatrixByDate(DateTime date)
        {
            Clear();
            DateTime useDate = new DateTime(date.Year, date.Month, 1);

            int line = 0;
            for (int i = 1; i <= DayofMonth(date); i++)
            {
                int column = dateOfWeek.IndexOf(useDate.DayOfWeek.ToString());
                Button btn = Matrix[line][column];
                btn.Text = i.ToString();

                if (column >= 6)
                    line++;
                if(checkEvent(useDate,InUser))
                {
                    btn.BackColor = Color.Red;
                }
                else if (isEqualDate(useDate, date))
                {
                    btn.BackColor = Color.Blue;
                }

                else if (isEqualDate(useDate, DateTime.Now))
                {
                    btn.BackColor = Color.Yellow;
                }

                useDate = useDate.AddDays(1);

            }

        }
        
        bool checkEvent(DateTime useDate,string InUser)
        {
            string s = "";
            s = s + useDate.Month.ToString() + "/" + useDate.Day.ToString() + "/" + useDate.Year.ToString();

            DataTable tb = new DataTable();
            SqlConnection sql = new SqlConnection(curAcc.query);
            sql.Open();
            SqlDataAdapter read = new SqlDataAdapter("select *from DailyEvent where InDTPK = '" + s + "' and InUser = '" + InUser + "'", sql);
            read.Fill(tb);
            for (int j = 0; j < tb.Rows.Count; j++)
            {
                if (tb.Rows[0][j].ToString() == s)
                {
                    sql.Close();

                    return true;
                }

            }

            sql.Close();

            return false;
        }

        void SetDefaultDate()
        {
            dtpkDate.Value = DateTime.Now;
        }

        void Clear()
        {
            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[i].Count; j++)
                {
                    Button x = Matrix[i][j];
                    x.Text = "";
                    x.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private string NameMonth(string s)
        {
            switch (s)
            {
                case "1":
                    s = "january";
                    break;
                case "2":
                    s = "February";
                    break;
                case "3":
                    s = "March";
                    break;
                case "4":
                    s = "April";
                    break;
                case "5":
                    s = "May";
                    break;
                case "6":
                    s = "June";
                    break;
                case "7":
                    s = "July";
                    break;
                case "8":
                    s = "August";
                    break;
                case "9":
                    s = "September";
                    break;
                case "10":
                    s = "October";
                    break;
                case "11":
                    s = "November";
                    break;
                case "12":
                    s = "December";
                    break;


                default:
                    break;
            }
            return s;
        }

        bool isEqualDate(DateTime a, DateTime b)
        {
            return a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;
        }
        private void dtpkDate_ValueChanged_1(object sender, EventArgs e)
        {
            AddNumberIntoMatrixByDate((sender as DateTimePicker).Value);
            this.lbDay.Text = NameMonth(dtpkDate.Value.Month.ToString())+" "+dtpkDate.Value.Year.ToString();
        }

        private void MonthBF_Click_1(object sender, EventArgs e)
        {
            dtpkDate.Value = dtpkDate.Value.AddMonths(-1);
        }

        private void MonthATer_Click_1(object sender, EventArgs e)
        {
            dtpkDate.Value = dtpkDate.Value.AddMonths(1);
        }

        private void btnDayNow_Click_1(object sender, EventArgs e)
        {
            SetDefaultDate();
        }
        int DayofMonth(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 2:
                    if ((date.Year % 4 == 0 && date.Year % 100 != 0) || date.Year % 400 == 0)
                        return 29;
                    else return 28;
                default:
                    return 30;
            }
        }

    }
}
