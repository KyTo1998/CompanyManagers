
namespace CompanyManagers.Models.ModelsAll
{
    public class Data_ResponAPI
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class Root_ResponAPI
    {
        public Data_ResponAPI data { get; set; }
        public object error { get; set; }
    }
}
