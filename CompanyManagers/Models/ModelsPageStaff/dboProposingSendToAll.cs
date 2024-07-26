using CompanyManagers.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CompanyManagers.Models.ModelsPageStaff
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BaoLuuDiem
    {
        public int id_ntd { get; set; }
        public string name_ntd { get; set; }
        public int points { get; set; }
        public int days { get; set; }
        public int site { get; set; }
    }

    public class BoNhiem
    {
        public object thanhviendc_bn { get; set; }
        public object chucvu_hientai { get; set; }
        public object chucvu_dx_bn { get; set; }
        public object organizeDetailId { get; set; }
        public object new_organizeDetailId { get; set; }
        public object ly_do { get; set; }
    }

    public class CapPhatTaiSan
    {
        public object cap_phat_taisan { get; set; }
        public object ly_do { get; set; }
    }

    public class ChuyenDiem
    {
        public int id_ntd_to { get; set; }
        public string name_ntd_to { get; set; }
        public int id_ntd_from { get; set; }
        public string name_ntd_from { get; set; }
        public int points { get; set; }
        public int site { get; set; }
        public int site_to { get; set; }
    }

    public class CongDiemNtd
    {
        public int id_ntd { get; set; }
        public int ngay_reset { get; set; }
        public int diem { get; set; }
    }

    public class Data_ProposingSendAll
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<ListProposingSendAll> data { get; set; }
        public int totalPages { get; set; }
    }
    public class ListProposingSendAll
    {
       public int Order { get; set; }

        // Phương thức tĩnh để cập nhật thứ tự trong danh sách
        public static void UpdateOrder(List<ListProposingSendAll> dataList)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                dataList[i].Order = i + 1;
            }
        }
        public NoiDung noi_dung { get; set; }
        public int _id { get; set; }
        public string name_dx { get; set; }
        public int type_dx { get; set; }

        private string nameCategotyPropose;
        public string name_type_dx 
        {
            get
            {
                GetCategoryPropose(); return nameCategotyPropose;
            }
            set { nameCategotyPropose = value; }
        }

        public void GetCategoryPropose()
        {
            List<Result_CategoryProposing> listCategoyPropose = new List<Result_CategoryProposing>();
            for (int i = 1; i < 4 ; i++)
            {
                var data = new
                {
                    page = i
                };
                string jsondata = JsonConvert.SerializeObject(data);
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                    var resData = webClient.UploadString(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_CategoryProposing, "POST", jsondata);
                    byte[] bytesData = Encoding.Default.GetBytes(resData);
                    Root_CategoryProposing dataCategoryPropose = JsonConvert.DeserializeObject<Root_CategoryProposing>(Encoding.UTF8.GetString(bytesData));
                    if (dataCategoryPropose != null)
                    {
                        foreach (var item in dataCategoryPropose.data.result)
                        {
                            listCategoyPropose.Add(item);
                        }
                    }
                }
            }
            nameCategotyPropose = listCategoyPropose.Find(x => x.cate_dx == type_dx)?.name_cate_dx;
        }
        public string name_user { get; set; }
        public int id_user { get; set; }
        public int com_id { get; set; }
        public int kieu_duyet { get; set; }
        public string id_user_duyet { get; set; }
        public string id_user_theo_doi { get; set; }
        public List<object> file_kem { get; set; }
        public string type_duyet { get; set; }

         public string status_duyet
        {
            get
            {
                switch (type_duyet)
                {
                    
                    case "0": return "Tiếp nhận";
                    case "3": return "Từ chối";
                    case "5": return "Chấp thuận";
                    case "6": return "Bắt buộc đi làm";
                    case "7": return "Đã được tiếp nhận";
                    case "10": return "Chờ lãnh đạo còn lại duyệt";
                    case "11": return "Chờ công ty duyệt";
                    default: return "";

                }

            }

            set { type_duyet = value; }
        }
        public int type_time { get; set; }
        public int time_create { get; set; }
        public string time_create_display
        {
            set
            {
                time_create = int.Parse(value);
            }
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(time_create).ToLocalTime().ToString("HH:mm tt dd/MM/yyyy");
            }
        }
        public object time_tiep_nhan { get; set; }
        public object time_duyet { get; set; }
        public int active { get; set; }
        public int del_type { get; set; }
        public int status_cancel { get; set; }
        public int tam_ung_status { get; set; }
        public int thanh_toan_status { get; set; }
        public string organizeDetailName { get; set; }
        public string refuse_reason { get; set; }
        public bool edited { get; set; }
        public int __v { get; set; }
        public bool confirmed { get; set; }
    }

    public class DiMuonVeSom
    {
        public object ngay_di_muon_ve_som { get; set; }
        public object time_batdau { get; set; }
        public object time_ketthuc { get; set; }
        public object ca_lam_viec { get; set; }
        public object ly_do { get; set; }
    }

    public class DoiCa
    {
        public object ngay_can_doi { get; set; }
        public object ca_can_doi { get; set; }
        public object ngay_muon_doi { get; set; }
        public object ca_muon_doi { get; set; }
        public object ly_do { get; set; }
    }

    public class GhimTin
    {
        public int id_ntd { get; set; }
        public int id_tin { get; set; }
        public int week { get; set; }
        public int diem { get; set; }
        public int ngay_reset { get; set; }
    }

    public class HoaHong
    {
        public string chu_ky { get; set; }
        public object time_hh { get; set; }
        public string item_mdt_date { get; set; }
        public string dt_money { get; set; }
        public string name_dt { get; set; }
        public string ly_do { get; set; }
    }

    public class LichLamViec
    {
        public object lich_lam_viec { get; set; }
        public object thang_ap_dung { get; set; }
        public object ngay_bat_dau { get; set; }
        public object ca_la_viec { get; set; }
        public object ngay_lam_viec { get; set; }
        public object ly_do { get; set; }
    }

    public class LuanChuyenCongTac
    {
        public object cv_nguoi_lc { get; set; }
        public object pb_nguoi_lc { get; set; }
        public object noi_cong_tac { get; set; }
        public object noi_chuyen_den { get; set; }
        public object ly_do { get; set; }
    }

    public class NghiPhep
    {
        public List<object> nd { get; set; }
        public object ng_ban_giao_CRM { get; set; }
        public object loai_np { get; set; }
        public object ly_do { get; set; }
    }

    public class NghiPhepRaNgoai
    {
        public object type_nghi { get; set; }
        public object bd_nghi { get; set; }
        public object ca_nghi { get; set; }
        public object time_bd_nghi { get; set; }
        public object time_kt_nghi { get; set; }
        public object ly_do { get; set; }
    }

    public class NghiThaiSan
    {
        public object ngaybatdau_nghi_ts { get; set; }
        public object ngayketthuc_nghi_ts { get; set; }
        public object ly_do { get; set; }
    }

    public class NhapNgayNhanLuong
    {
        public object thang_ap_dung { get; set; }
        public object ngay_bat_dau { get; set; }
        public object ngay_ket_thuc { get; set; }
        public object ly_do { get; set; }
    }

    public class NoiDung
    {
        public ChuyenDiem chuyen_diem { get; set; }
        public BaoLuuDiem bao_luu_diem { get; set; }
        public GhimTin ghim_tin { get; set; }
        public CongDiemNtd cong_diem_ntd { get; set; }
        public NghiPhep nghi_phep { get; set; }
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
        public LichLamViec lich_lam_viec { get; set; }
        public HoaHong hoa_hong { get; set; }
        public ThanhToan thanh_toan { get; set; }
        public ThuongPhat thuong_phat { get; set; }
        public DiMuonVeSom di_muon_ve_som { get; set; }
        public NghiPhepRaNgoai nghi_phep_ra_ngoai { get; set; }
        public NhapNgayNhanLuong nhap_ngay_nhan_luong { get; set; }
        public XinTaiTaiLieu xin_tai_tai_lieu { get; set; }
    }

    public class Root_ProposingSendAll
    {
        public Data_ProposingSendAll data { get; set; }
        public object error { get; set; }
    }

    public class SuaChuaCoSoVatChat
    {
        public object tai_san { get; set; }
        public object so_luong { get; set; }
        public object ngay_sc { get; set; }
        public object so_tien { get; set; }
    }

    public class SuDungPhongHop
    {
        public object bd_hop { get; set; }
        public object end_hop { get; set; }
        public object phong_hop { get; set; }
        public object ly_do { get; set; }
    }

    public class SuDungXeCong
    {
        public object bd_xe { get; set; }
        public object end_xe { get; set; }
        public object soluong_xe { get; set; }
        public object local_di { get; set; }
        public object local_den { get; set; }
    }

    public class TamUng
    {
        public object ngay_tam_ung { get; set; }
        public object sotien_tam_ung { get; set; }
        public object ly_do { get; set; }
    }

    public class TangCa
    {
        public object time_tc { get; set; }
        public object ly_do { get; set; }
    }

    public class TangLuong
    {
        public object mucluong_ht { get; set; }
        public object mucluong_tang { get; set; }
        public object date_tang_luong { get; set; }
        public object ly_do { get; set; }
    }

    public class ThamGiaDuAn
    {
        public object cv_nguoi_da { get; set; }
        public object pb_nguoi_da { get; set; }
        public object dx_da { get; set; }
        public object ly_do { get; set; }
    }

    public class ThanhToan
    {
        public object so_tien_tt { get; set; }
        public object ly_do { get; set; }
    }

    public class ThoiViec
    {
        public object ngaybatdau_tv { get; set; }
        public object ca_bdnghi { get; set; }
        public object ly_do { get; set; }
    }

    public class ThuongPhat
    {
        public object so_tien_tp { get; set; }
        public object time_tp { get; set; }
        public object nguoi_tp { get; set; }
        public object type_tp { get; set; }
        public object ly_do { get; set; }
    }

    public class XacNhanCong
    {
        public DateTime? time_xnc { get; set; }
        public string ca_xnc { get; set; }
        public int? id_ca_xnc { get; set; }
        public string ly_do { get; set; }
        public string time_vao_ca { get; set; }
        public string time_het_ca { get; set; }
    }

    public class XinTaiTaiLieu
    {
        public object ten_tai_lieu { get; set; }
        public object ly_do { get; set; }
    }


}
