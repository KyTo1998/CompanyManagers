using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelsPageStaff
{
    // Root_DetailPropose myDeserializedClass = JsonConvert.DeserializeObject<Root_DetailPropose>(myJsonResponse);

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
                return DateTimeOffset.FromUnixTimeSeconds(thoi_gian_tao).ToLocalTime().ToString("dd-MM/yyyy HH:mm tt ");
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
        public string avatarUser { get; set; }
        public int idQLC { get; set; }
    }

    public class LichSuDuyet
    {
        public string ng_duyet { get; set; }
        public int type_duyet { get; set; }
        public DateTime time { get; set; }
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
}
