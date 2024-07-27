using System.Collections.Generic;

namespace CompanyManagers.Models.ModelsAll
{
    public class Data_StaffAll
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Info_StaffAll> items { get; set; }
    }

    public class Info_StaffAll
    {
        public int _id { get; set; }
        public int ep_id { get; set; }
        public string ep_email { get; set; }
        public string ep_phone { get; set; }
        public string ep_name { get; set; }
        public string ep_image { get; set; }
        public int role_id { get; set; }
        public int allow_update_face { get; set; }
        public int? ep_education { get; set; }
        public int? ep_exp { get; set; }
        public string ep_address { get; set; }
        public int? ep_gender { get; set; }
        public int? ep_married { get; set; }
        public double? ep_birth_day { get; set; }
        public int? group_id { get; set; }
        public string start_working_time { get; set; }
        public int? position_id { get; set; }
        public int com_id { get; set; }
        public int dep_id { get; set; }
    }

    public class Root_StaffAll
    {
        public Data_StaffAll data { get; set; }
        public object error { get; set; }
    }


}
