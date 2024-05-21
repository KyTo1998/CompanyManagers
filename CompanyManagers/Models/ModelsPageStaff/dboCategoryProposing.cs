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
        public string _id { get; set; }
        public string name_cate_dx { get; set; }

        public string mota_dx
        {
            get
            {
                switch (_id)
                {
                    case "1": return "Mẫu đề xuất xin nghỉ phép";
                    case "2": return "Mẫu đề xuất xin đổi ca";
                    case "3": return "Mẫu đề xuất xin tạm ứng";
                    case "4": return "Mẫu đề xuất xin cấp phép tài sản";
                    case "5": return "Đơn xin thôi việc";
                    case "6": return "Đề xuất tương lương";
                    case "7": return "Đề xuất bổ nhiệm";
                    case "8": return "Đề xuất luân chuyển công tác";
                    case "9": return "Đề xuất tham gia dự án";
                    case "10": return "Đề xuất xin tăng ca";
                    case "11": return "Đề xuất xin nghỉ chế độ thai sản";
                    case "12": return "Đề xuất đăng kí sử dụng phòng họp";
                    case "13": return "Đề xuất đăng kí sử dụng xe công";
                    case "14": return "Đề xuất sửa chữa tài sản cấp phát";
                    case "15": return "Đề xuất thanh toán";
                    case "16": return "Đề xuất khiếu nại";
                    case "17": return "Đề xuất cộng công";
                    case "18": return "Đề xuất thưởng phạt";
                    case "19": return "Đề xuất hoa hồng doanh thu";
                    case "20": return "Đề xuất đi muộn về sớm";
                    case "21": return "Đơn xin nghỉ phép ra ngoài";
                    case "22": return "Đề xuất xin tải tài liệu";
                    case "23": return "Đề xuất nhập ngày nhận lương";
                    case "24": return "Đề xuất cộng điểm";  
                    case "25": return "Đề xuất ghim tin";  
                    case "26": return "Đề xuất bảo lưu điểm";  
                    case "27": return "Đề xuất chuyển điểm";  
                    default: return "";

                }

            }

            set { _id = value; }
        }
    }
}
