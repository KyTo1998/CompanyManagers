using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsShift;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using System.Linq;
using CompanyManagers.Models.ModelRose;
using System.Globalization;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelsPageStaff
{
    public class BaoLuuTin
    {
        public int new_id { get; set; }
        public string new_title { get; set; }
        public string type_pin { get; set; }
        public string endtime_pin { get; set; }
        public int site { get; set; }
        public int days { get; set; }
        public int active { get; set; }
    }

    public class Data
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<Detail_Proposet> detailDeXuat { get; set; }
    }

    public class Detail_Proposet
    {
        public string id_de_xuat { get; set; }
        public string ten_de_xuat { get; set; }
        public string nguoi_tao { get; set; }
        public int id_nguoi_tao { get; set; }
        public int nhom_de_xuat { get; set; }
        public long thoi_gian_tao { get; set; }
        public string thoi_gian_tao_string
        {
            set
            {
                thoi_gian_tao = int.Parse(value);
            }
            get
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(thoi_gian_tao).ToLocalTime().ToString("dd/MM/yyyy - HH:mm tt ");
            }
        }
        public int loai_de_xuat { get; set; }
        public int cap_nhat { get; set; }
        public ThongTinChung thong_tin_chung { get; set; }
        public string kieu_phe_duyet { get; set; }
        public string kieu_phe_duyet_format
        {
            get
            {
                switch (kieu_phe_duyet)
                {
                    case "0": return "Phê duyệt đồng thời";
                    case "1": return "Phê duyệt lần lượt";
                    default: return "";
                }

            }

            set { kieu_phe_duyet = value; }
        }
        public List<LanhDaoDuyet> lanh_dao_duyet { get; set; }
        public List<NguoiTheoDoi> nguoi_theo_doi { get; set; }
        public List<object> file_kem { get; set; }
        public int type_duyet { get; set; }
        public long thoi_gian_duyet { get; set; }
        public long thoi_gian_tiep_nhan { get; set; }
        public List<LichSuDuyet> lich_su_duyet { get; set; }
        public int del_type { get; set; }
        public bool qua_han_duyet { get; set; }
        public int tam_ung_status { get; set; }
        public int thanh_toan_status { get; set; }
        public bool confirm_status { get; set; }
        public string ly_do_tu_choi { get; set; }
        public bool edited { get; set; }
    }
    public class LanhDaoDuyet
    {
        public string userName { get; set; }

        private string LinkAvatarComfirm;

        public string avatarUser 
        {
            get
            {
                GetShaftAll(); return LinkAvatarComfirm;
            }
            set { LinkAvatarComfirm = value; }
        }
        public int idQLC { get; set; }
        public void GetShaftAll()
        {
            if (idQLC.ToString() != Properties.Settings.Default.IdCompany && idQLC.ToString() == Properties.Settings.Default.IdUser)
            {
                var data = new
                {
                    com_id = Properties.Settings.Default.IdCompany,
                    idQLC = idQLC.ToString()
                };
                string jsondata = JsonConvert.SerializeObject(data);
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                    var resData = webClient.UploadString(UrlApi.Url_Api_Staff + UrlApi.Name_Api_SearchInforStaff, "POST", jsondata);
                    byte[] bytesData = Encoding.Default.GetBytes(resData);
                    Root_InforStaff dataInforStaff = JsonConvert.DeserializeObject<Root_InforStaff>(Encoding.UTF8.GetString(bytesData));
                    if (dataInforStaff != null)
                    {
                        string fileName = GetFileNameFromUrl(dataInforStaff.data.data.avatarUser);
                        LinkAvatarComfirm = $"http://210.245.108.202:9002/avatarUser/{dataInforStaff.data.data._id}/{fileName}";
                    }
                }
            }
        }
        public static string GetFileNameFromUrl(string url)
        {
            Uri uri = new Uri(url);
            return Path.GetFileName(uri.LocalPath);
        }
    }

    public class LichSuDuyet
    {
        public string ng_duyet { get; set; }
        public string type_duyet { get; set; }
        public string status_duyet
        {
            get
            {
                switch (type_duyet)
                {
                    case "1": return "đã tiếp nhận đề xuất";
                    case "2": return "đã duyệt đề xuất";
                    case "3": return "đã Từ chối đề xuất";
                    case "4": return "đã hủy duyệt đề xuất";
                    default: return "";
                }
            }
            set { type_duyet = value; }
        }
        public DateTime time { get; set; }

        public string time_string
        {
            get
            {
                var dateTimeOffsetUtc = new DateTimeOffset(time, TimeSpan.Zero);
                var dateTimeOffsetVn = dateTimeOffsetUtc.ToOffset(TimeSpan.FromHours(7));
                return dateTimeOffsetVn.ToString("dd-MM-yyyy HH:mm:ss");
            }
            set { }
        }
       
    }

    public class NguoiTheoDoi
    {
        public string userName { get; set; }
        public string avatarUser { get; set; }
        public int idQLC { get; set; }
    }

    public class Root_DetailPropose
    {
        public Data data { get; set; }
        public object error { get; set; }
    }

    public class ThongTinChung
    {
        public BaoLuuTin bao_luu_tin { get; set; }
        public ChuyenDiem chuyen_diem { get; set; }
        public BaoLuuDiem bao_luu_diem { get; set; }
        public GhimTin ghim_tin { get; set; }
        public CongDiemNtd cong_diem_ntd { get; set; }
        public NghiPhep_DetailPropose nghi_phep { get; set; }
        public DoiCa doi_ca { get; set; }
        public TamUng tam_ung { get; set; }
        public CapPhatTaiSan cap_phat_tai_san { get; set; }
        public ThoiViec thoi_viec { get; set; }
        public TangLuong tang_luong { get; set; }
        public BoNhiem bo_nhiem { get; set; }
        public LuanChuyenCongTac luan_chuyen_cong_tac { get; set; }
        public ThamGiaDuAn tham_gia_du_an { get; set; }
        public TangCa tang_ca { get; set; }
        public NghiThaiSan nghi_thai_san { get; set; }
        public SuDungPhongHop su_dung_phong_hop { get; set; }
        public SuDungXeCong su_dung_xe_cong { get; set; }
        public SuaChuaCoSoVatChat sua_chua_co_so_vat_chat { get; set; }
        public XacNhanCong xac_nhan_cong { get; set; }
        public LichLamViec_DetailPropose lich_lam_viec { get; set; }
        public HoaHong_DetailPropose hoa_hong { get; set; }
        public ThanhToan thanh_toan { get; set; }
        public ThuongPhat thuong_phat { get; set; }
        public DiMuonVeSom di_muon_ve_som { get; set; }
        public NghiPhepRaNgoai nghi_phep_ra_ngoai { get; set; }
        public NhapNgayNhanLuong nhap_ngay_nhan_luong { get; set; }
        public XinTaiTaiLieu xin_tai_tai_lieu { get; set; }
    }
    public class Nd
    {
        public int Order { get; set; }
        public string bd_nghi { get; set; }
        public string kt_nghi { get; set; }
        public string ca_nghi { get; set; }

        private string shiftName;
        public string ca_nghi_string
        {
            get 
            { 
                if(ca_nghi == null)
                {
                    return "Nghỉ cả ngày(tất cả các ca)";
                }
                else
                {
                    UpdateShiftName(); return shiftName;
                }
            }
            set { shiftName = value; }
        }
        public string _id { get; set; }

        public void UpdateShiftName()
        {
            var data = new
            {
                day = bd_nghi
            };
            string jsonData = JsonConvert.SerializeObject(data);
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                var resData = webClient.UploadString(UrlApi.Url_Api_Shift + UrlApi.Name_Api_ListShiftForDay, "POST", jsonData);
                byte[] bytesData = Encoding.Default.GetBytes(resData);
                Root_StaffShiftInDay dataStaffShiftInDay = JsonConvert.DeserializeObject<Root_StaffShiftInDay>(Encoding.UTF8.GetString(bytesData));
                if (dataStaffShiftInDay != null)
                {
                    shiftName = dataStaffShiftInDay.list.Find(x => x.shift_id == ca_nghi || x.shift_id == (Int32.Parse(ca_nghi) + 1).ToString() || x.shift_id == (Int32.Parse(ca_nghi) - 1).ToString())?.shift_name;
                }
            }
        }

        public static void UpdateOrder(List<Nd> dataList)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                dataList[i].Order = i + 1;
            }
        }
    }

    public class NghiPhep_DetailPropose
    {
        public List<Nd> nd { get; set; }
        public int? ng_ban_giao_CRM { get; set; }

        private string NameUserCRM;
        public string ng_ban_giao_string_CRM 
        {
            get
            {
                GetShaftAll(); return NameUserCRM;
            }
            set { NameUserCRM = value; }
        }
        public void GetShaftAll()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                var resData = webClient.UploadString(UrlApi.Url_Api_Staff + UrlApi.Name_Api_Staff, "POST", "");
                byte[] bytesData = Encoding.Default.GetBytes(resData);
                Root_StaffAll dataInforStaff = JsonConvert.DeserializeObject<Root_StaffAll>(Encoding.UTF8.GetString(bytesData));
                if (dataInforStaff != null)
                {
                    if (ng_ban_giao_CRM != null && dataInforStaff.data.items.Any(x => x.ep_id == ng_ban_giao_CRM))
                    {
                        NameUserCRM = dataInforStaff.data.items.Find(x => x.ep_id == ng_ban_giao_CRM).ep_name;
                    }
                    else
                    {
                        NameUserCRM = "Không có";
                    }
                }
            }
        }
        public int? loai_np { get; set; }
        public string ly_do { get; set; }
    }

    public class HoaHong_DetailPropose
    {
        public string chu_ky { get; set; }
        public string time_hh { get; set; }
        public string item_mdt_date { get; set; }
        public long? dt_money { get; set; }
        public string dt_money_formatted
        {
            get 
            {
                var cultureInfo = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                cultureInfo.NumberFormat.NumberGroupSeparator = ".";
                return dt_money?.ToString("N0", cultureInfo); 
            }
            set
            {
                if (int.TryParse(value.Replace(",", ""), out int parsedLuong))
                {
                    dt_money = parsedLuong;
                }
            }
        }
        public string name_dt { get; set; }

        public string _lever_revernue;
        public string lever_revernue 
        {
            get
            {
                GetListLevelRevernue(); return _lever_revernue;
            }
            set { _lever_revernue = value; }
        }
        public string ly_do { get; set; }
        public void GetListLevelRevernue()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                var resData = webClient.UploadString(UrlApi.Url_Api_Rose + UrlApi.Name_Api_ListLeverlRevernue, "POST", "");
                byte[] bytesData = Encoding.Default.GetBytes(resData);
                Root_LevelRevenue dataListLeverlRevernue = JsonConvert.DeserializeObject<Root_LevelRevenue>(Encoding.UTF8.GetString(bytesData));
                if (dataListLeverlRevernue != null)
                {
                    foreach (var item in dataListLeverlRevernue.data.danhthuList)
                    {
                        if (item.tl_id.ToString() == name_dt)
                        {
                            _lever_revernue = item.tl_name;
                        }
                    }
                }
            }
        }
    }
    public class LichLamViec_DetailPropose
    {
        public string lich_lam_viec { get; set; }
        public string lich_lam_viec_display
        {
            get
            {
                switch (lich_lam_viec)
                {
                    case "1": return "Thứ 2 - Thứ 6";
                    case "2": return "Thứ 2 - Thứ 7";
                    case "3": return "Thứ 2 - CN";
                    default: return "";
                }
            }
            set { lich_lam_viec = value; }
        }
        public string thang_ap_dung { get; set; }
        public string thang_ap_dung_display 
        {
            get
            {
                if (thang_ap_dung != null)
                {
                    return UnixTimestampToDateTime(Int32.Parse(thang_ap_dung)).ToString("MM-yyyy");
                }
                else
                {
                    return null;
                }
            }
            set { thang_ap_dung = value; }
        }
        public string ngay_bat_dau { get; set; }
        public string ngay_bat_dau_display 
        {
            get
            {
                if (ngay_bat_dau != null)
                {
                    return UnixTimestampToDateTime(Int32.Parse(ngay_bat_dau)).ToString("dd-MM-yyyy");
                }
                else
                {
                    return null ;
                }
            }
            set { ngay_bat_dau = value; }
        }
        public string ca_la_viec { get; set; }

        public string ngay_lam_viec { get; set; }
     
        public string ly_do { get; set; }
        public static DateTime UnixTimestampToDateTime(long unixTimestamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTimestamp);
        }
    }
}
