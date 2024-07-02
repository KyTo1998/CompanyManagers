using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelRose
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RevenueList
    {
        public int tl_ep_id { get; set; }
        public string _id { get; set; }
        public int tl_id { get; set; }
        public int tl_id_com { get; set; }
        public int tl_id_rose { get; set; }
        public string tl_name { get; set; }
        public int tl_money_min { get; set; }
        public int tl_money_max { get; set; }
        public RevenuePhanTram tl_phan_tram { get; set; }
        public int tl_chiphi { get; set; }
        public int tl_hoahong { get; set; }
        public int tl_kpi_yes { get; set; }
        public int tl_kpi_no { get; set; }
        public string tl_time_created { get; set; }
    }

    public class Data_LevelRevenue
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<RevenueList> danhthuList { get; set; }
    }

    public class Root_LevelRevenue
    {
        public Data_LevelRevenue data { get; set; }
        public object error { get; set; }
    }

    public class RevenuePhanTram
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }


}
