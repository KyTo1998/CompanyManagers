using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelsShift
{
   // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class StaffShiftInDay
    {
        public string _id { get; set; }
        public string shift_id  { get; set; }
        public int com_id { get; set; }
        public string shift_name { get; set; }
        public string start_time { get; set; }
        public object start_time_latest { get; set; }
        public string end_time { get; set; }
        public object end_time_earliest { get; set; }
        public int over_night { get; set; }
        public int shift_type { get; set; }
        public NumToCalculate num_to_calculate { get; set; }
        public int num_to_money { get; set; }
        public int money_per_hour { get; set; }
        public int is_overtime { get; set; }
        public int status { get; set; }
        public List<RelaxTime_StaffShiftInDay> relaxTime { get; set; }
        public int flex { get; set; }
        public DateTime create_time { get; set; }
    }

    public class NumToCalculate
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }

    public class RelaxTime_StaffShiftInDay
    {
        public object start_time_relax { get; set; }
        public object end_time_relax { get; set; }
        public string _id { get; set; }
    }

    public class Root_StaffShiftInDay
    {
        public List<StaffShiftInDay> list { get; set; }
    }


}
