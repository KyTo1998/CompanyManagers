using System.Collections.Generic;
using System.ComponentModel;

namespace CompanyManagers.Models.ModelsPageStaff
{
    public class Data_CategoryProposing
    {
        public List<Result_CategoryProposing> result { get; set; }
        public string message { get; set; }
        public List<object> idHideCateDX { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
    }

    public class Result_CategoryProposing: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        public int _id { get; set; }
        public int cate_dx { get; set; }
        public string name_cate_dx { get; set; }
        public int com_id { get; set; }
        public string mieuta_maudon { get; set; }
        public int date_cate_dx { get; set; }
        public int money_cate_dx { get; set; }
        public int hieu_luc_cate { get; set; }
        public int kieu_duyet_cate { get; set; }
        public string user_duyet_cate { get; set; }
        public string ghi_chu_cate { get; set; }
        public int created_date { get; set; }
        public int update_time { get; set; }
        public int time_limit { get; set; }
        public int time_limit_l { get; set; }
        public int trang_thai_dx { get; set; }
        public int __v { get; set; }

        private bool _statusHoverProposing;
        public bool statusHoverProposing
        {
            get { return _statusHoverProposing; }
            set { _statusHoverProposing = value; OnPropertyChanged("statusHoverProposing"); }
        }
    }

    public class Root_CategoryProposing
    {
        public Data_CategoryProposing data { get; set; }
        public object error { get; set; }
    }
}
