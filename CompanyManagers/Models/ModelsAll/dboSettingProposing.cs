using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelsAll
{
    public class Root_SettingPropose
    {
        public SettingPropose settingPropose { get; set; }
    }

    public class SettingPropose
    {
        public string _id { get; set; }
        public int id { get; set; }
        public int comId { get; set; }
        public int dexuat_id { get; set; }
        public string dexuat_name { get; set; }
        public int confirm_level { get; set; }
        public int confirm_type { get; set; }
        public int confirm_time { get; set; }
        public int created_time { get; set; }
        public int update_time { get; set; }
    }
}
