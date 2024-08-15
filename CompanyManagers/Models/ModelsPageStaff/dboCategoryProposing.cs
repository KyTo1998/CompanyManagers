using System.Collections.Generic;
using System.ComponentModel;
using CompanyManagers.Controllers;
using static System.Net.Mime.MediaTypeNames;

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
        public string name_cate_dx_display
        {
            get
            {
                if (Properties.Settings.Default.Language == "VN")
                {
                    return name_cate_dx;
                }
                else
                {
                    return Translate(name_cate_dx);
                }
            }
            set { }
        }
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
        public static Dictionary<string, string> translationDictionary = new Dictionary<string, string>
        {
            { "Đơn xin nghỉ phép", "Leave Request Form" },
            { "Đơn xin đổi ca", "Shift Change Request Form" },
            { "Đơn tạm ứng", "Advance Request Form" },
             { "Đơn xin cấp phát tài sản", "Asset Allocation Request Form" },
             { "Đơn xin thôi việc", "Resignation Letter" },
             { "Đề xuất tăng lương", "Salary Increase Proposal" },
             { "Đề xuất bổ nhiệm", "Appointment Proposal" },
             { "Đề xuất luân chuyển công tác", "Job Rotation Proposal" },
             { "Đề xuất tham gia dự án", "Project Participation Proposal" },
             { "Đề xuất xin tăng ca", "Overtime Request Proposal" },
             { "Đề xuất xin nghỉ chế độ thai sản", "Maternity Leave Request Proposal" },
             { "Đề xuất đăng ký sử dụng phòng họp", "Meeting Room Booking Request" },
             { "Đề xuất đăng ký sử dụng xe công", "Company Vehicle Request" },
             { "Đề xuất sửa chữa tài sản cấp phát", "Proposal for Asset Repair" },
             { "Đề xuất thanh toán", "Payment Request Proposal" },
             { "Đề xuất khiếu nại", "Complaint Proposal" },
             { "Đề xuất cộng công", "Proposal for Adding Hours" },
             { "Đề xuất lịch làm việc", "Work Schedule Proposal" },
             { "Đề xuất thưởng phạt", "Rewards and Penalties Proposal" },
             { "Đề xuất hoa hồng doanh thu", "Revenue Commission Proposal" },
             { "Đơn xin nghỉ phép ra ngoài", "External Leave Request Form" },
             { "Đề xuất nhập ngày nhận lương", "Proposal to Enter Salary Payment Date" },
             { "Đề xuất xin tải tài liệu", "Request for Document Download" },
             { "Đề xuất đi muộn về sớm", "Request for Late Arrival and Early Departure" },
        };

        public static string Translate(string text)
        {
            if (translationDictionary.TryGetValue(text, out string translation))
            {
                return translation;
            }
            else
            {
                return "Translation not found";
            }
        }
    }

    public class Root_CategoryProposing
    {
        public Data_CategoryProposing data { get; set; }
        public object error { get; set; }
    }
}
