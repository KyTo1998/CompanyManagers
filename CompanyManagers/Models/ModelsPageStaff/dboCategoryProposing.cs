using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelsPageStaff
{
    public class Data_CategoryProposing
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<Showcatedx_CategoryProposing> showcatedx { get; set; }
    }

    public class Root_CategoryProposing
    {
        public Data_CategoryProposing data { get; set; }
        public object error { get; set; }
    }

    public class Showcatedx_CategoryProposing
    {
        public int _id { get; set; }
        public string name_cate_dx { get; set; }
    }
}
