using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListNote
{
    public partial class CldWeek : Form
    {
        private List<Button> TbHead;
        public List<Button> TbHead1 { get => TbHead; set => TbHead = value; }


        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        

        public CldWeek()
        {
            InitializeComponent();
        }


        void LoadTime()
        {
            DateTime check = dtpkDate.Value;

            Button oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.Margin, 0) };
            TbHead = new List<Button>();
            for (int i = 0; i < 7; i++)
            {
                
                Button a = new Button() { Height = Cons.Height, Width = Cons.Width - 10 };
                a.FlatAppearance.BorderSize = 1;
                a.FlatAppearance.BorderColor = Color.LightSkyBlue;
                a.TextAlign = ContentAlignment.TopLeft;
                a.FlatStyle = FlatStyle.Flat;

                a.Location = new Point(oldBtn.Location.X + oldBtn.Width + Cons.Margin, oldBtn.Location.Y);

                this.pnDayOfMonth.Controls.Add(a);

                
                TbHead.Add(a);
                oldBtn = a;
            }

            LoadMatrix();
            SetDefaultDate();
        }

        // ham add ngay cua cac thu trong tuan
        void AddNumberTbHead(DateTime date)
        {
            ClearTbhead();

            DateTime check = date;

            if(check.DayOfWeek.ToString()!="Monday")
            {
                bool bl = true;
                while (bl)
                {

                    check = check.AddDays(-1);
                    if (check.DayOfWeek.ToString() == "Monday")
                    {
                        bl = false;
                    }
                }
            }
            

            for (int i = 0; i < TbHead.Count; i++)
            {
                Button btn = TbHead[i];
                btn.Text = check.Month.ToString() + " / " + check.Day.ToString();
                check = check.AddDays(1);
            }


            //DateTime useDate = new DateTime(date.Year, date.Month, 1);

            //int line = 0;
            //for (int i = 1; i <= DayofMonth(date); i++)
            //{
            //    int column = dateOfWeek.IndexOf(useDate.DayOfWeek.ToString());
            //    Button btn = Matrix[line][column];
            //    btn.Text = i.ToString();

            //    if (column >= 6)
            //        line++;
            //    if (checkEvent(useDate, InUser))
            //    {
            //        btn.BackColor = Color.Red;
            //    }
            //    else if (isEqualDate(useDate, date))
            //    {
            //        btn.BackColor = Color.Blue;
            //    }

            //    else if (isEqualDate(useDate, DateTime.Now))
            //    {
            //        btn.BackColor = Color.Yellow;
            //    }

            //    useDate = useDate.AddDays(1);

            //}

        }

        void ClearTbhead()
        {
          
                for (int i = 0; i < TbHead.Count; i++)
                {
                    Button x = TbHead[i];
                    x.Text = "";
                }
          
        }


        void ClearMatrix()
        {
            for(int i = 0; i<matrix.Count;i++)
            {
                for(int j = 0; j<matrix[i].Count; j++)
                {
                    Button x = Matrix[i][j];
                    x.Text = "";
                    //x.BackColor = Color.WhiteSmoke;
                }
            }
        }



        void LoadMatrix()
        {
            Matrix = new List<List<Button>>();
            Button oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.Margin+55, 0) };
            for (int i = 0; i < Cons.CountTimeofDay*2; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.DayOfweek; j++)
                {
                    Button a = new Button() { Height = Cons.Height/3, Width = Cons.Width-10 };

                    a.FlatAppearance.BorderSize = 1;
                    a.FlatAppearance.BorderColor = Color.LightSkyBlue;
                    a.TextAlign = ContentAlignment.TopLeft;
                    a.FlatStyle = FlatStyle.Flat;
                    a.Location = new Point(oldBtn.Location.X + oldBtn.Width + Cons.Margin, oldBtn.Location.Y);
                    a.Text = i.ToString();
                    a.Click += A_Click;

                    pnMatrix.Controls.Add(a);
                    Matrix[i].Add(a);
                    oldBtn = a;

                }
                oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.Margin+55, oldBtn.Location.Y + Cons.Height/3) };
            }

            SetDefaultDate();
        }


        void AddNumberMatrix(DateTime date)
        {
            ClearMatrix();
            DateTime check = date;

            if (check.DayOfWeek.ToString() != "Monday")
            {
                bool bl = true;
                while (bl)
                {

                    check = check.AddDays(-1);
                    if (check.DayOfWeek.ToString() == "Monday")
                    {
                        bl = false;
                    }
                }
            }
           
            for(int i = 0;i<matrix.Count; i++)
            {
                DateTime temp = check;
                for(int j = 0; j<matrix[i].Count; j++)
                {
                    Button x = matrix[i][j];
                    x.Text = temp.Month.ToString() + "//" + temp.Day.ToString();
                    temp = temp.AddDays(1);
                }
            }

        }

        private void A_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void SetDefaultDate()
        {
            dtpkDate.Value = DateTime.Now;
        }

        private void CldWeek_Load(object sender, EventArgs e)
        {
            LoadTime();
            
            
        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void MonthATer_Click(object sender, EventArgs e)
        {
            dtpkDate.Value = dtpkDate.Value.AddDays(7);
        }

        private void MonthBF_Click(object sender, EventArgs e)
        {
            dtpkDate.Value = dtpkDate.Value.AddDays(-7);
        }

        private void dtpkDate_ValueChanged(object sender, EventArgs e)
        {
            AddNumberTbHead((sender as DateTimePicker).Value);
            AddNumberMatrix((sender as DateTimePicker).Value);
        }

        private void btnDayNow_Click(object sender, EventArgs e)
        {
            SetDefaultDate();
        }
    }
}
