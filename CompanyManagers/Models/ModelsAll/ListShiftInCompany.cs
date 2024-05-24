using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelsAll
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_ShiftAll
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Item_ShiftAll> items { get; set; }
    }

    public class Item_ShiftAll
    {
        public string _id { get; set; }
        public int shift_id { get; set; }
        public int parentId { get; set; }
        public int com_id { get; set; }
        public string shift_name { get; set; }
        public string start_time { get; set; }
        public string start_time_latest { get; set; }
        public string end_time { get; set; }
        public string end_time_earliest { get; set; }
        public string start_time_earliest { get; set; }
        public object end_time_latest { get; set; }
        public int over_night { get; set; }
        public int? nums_day { get; set; }
        public int shift_type { get; set; }
        public double num_to_calculate { get; set; }
        public int num_to_money { get; set; }
        public int money_per_hour { get; set; }
        public int is_overtime { get; set; }
        public int status { get; set; }
        public List<RelaxTime_ShiftAll> relaxTime { get; set; }
        public int type_money_flex { get; set; }
        public int? money_allowances { get; set; }
        public int flex { get; set; }
        public int type_end_date { get; set; }
        public object start_date { get; set; }
        public object end_date { get; set; }
        public DateTime create_time { get; set; }
    }

    public class RelaxTime_ShiftAll
    {
        public object start_time_relax { get; set; }
        public object end_time_relax { get; set; }
        public string _id { get; set; }
    }

    public class Root_ShiftAll
    {
        public Data_ShiftAll data { get; set; }
        public object error { get; set; }
    }


}
