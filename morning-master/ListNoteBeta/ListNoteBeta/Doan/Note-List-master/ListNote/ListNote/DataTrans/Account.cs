using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListNote
{
    public class Account
    {
        public string query = @"Data Source=.;Initial Catalog=DTBEvent;Integrated Security=True";
        protected string user;
        protected List<Job> jobList;
        public Account()
        {
            user = "";
            jobList = new List<Job>();
        }

        public List<Job> JobList { get => jobList; set => jobList = value; }
        public string Username { get => user; set => user = value; }

    }
}
