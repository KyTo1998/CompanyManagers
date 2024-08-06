using CompanyManagers.Models.ModelsPageStaff;
using System.Collections.Generic;

namespace CompanyManagers.Models.ModelsAll
{
    public class Data_ComfrimAndFollow
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<LanhDaoDuyet> listUsersDuyet { get; set; }
        public List<ListUsersTheoDoi> listUsersTheoDoi { get; set; }
    }

    public class ListUsersDuyet
    {
        public int _id { get; set; }
        public int idQLC { get; set; }
        public string userName { get; set; }
        public object avatarUser { get; set; }
    }

    public class ListUsersTheoDoi
    {
        public int _id { get; set; }
        public string userName { get; set; }
        public object avatarUser { get; set; }
        public int idQLC { get; set; }
    }

    public class Root_ComfrimAndFollow
    {
        public Data_ComfrimAndFollow data { get; set; }
        public object error { get; set; }
    }


}
