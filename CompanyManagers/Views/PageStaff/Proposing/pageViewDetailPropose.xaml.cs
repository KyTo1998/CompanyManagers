using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyManagers.Views.PageStaff.Proposing
{
    /// <summary>
    /// Interaction logic for pageViewDetailPropose.xaml
    /// </summary>
    public partial class pageViewDetailPropose : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private List<LanhDaoDuyet> _listLeaderComfirm;
        public List<LanhDaoDuyet> listLeaderComfirm
        {
            get { return _listLeaderComfirm; }
            set { _listLeaderComfirm = value; OnPropertyChanged("listLeaderComfirm"); }
        }
        private List<NguoiTheoDoi> _listUserFollow;
        public List<NguoiTheoDoi> listUserFollow
        {
            get { return _listUserFollow; }
            set { _listUserFollow = value; OnPropertyChanged("listUserFollow"); }
        }
        private List<LichSuDuyet> _listStatuComfirm;
        public List<LichSuDuyet> listStatuComfirm
        {
            get { return _listStatuComfirm; }
            set { _listStatuComfirm = value; OnPropertyChanged("listStatuComfirm"); }
        }

        private List<Nd> _listCalendarOnLeave;
        public List<Nd> listCalendarOnLeave
        {
            get { return _listCalendarOnLeave; }
            set { _listCalendarOnLeave = value; OnPropertyChanged("listCalendarOnLeave"); }
        }
        private int _type_duyet;
        public int type_duyet
        {
            get { return _type_duyet; }
            set { _type_duyet = value; OnPropertyChanged("type_duyet"); }
        }

        private int _nhom_de_xuat;
        public int nhom_de_xuat
        {
            get { return _nhom_de_xuat; }
            set { _nhom_de_xuat = value; OnPropertyChanged("nhom_de_xuat"); }
        }
        
        private bool _confirm_status;
        public bool confirm_status
        {
            get { return _confirm_status; }
            set { _confirm_status = value; OnPropertyChanged("confirm_status"); }
        }

        private string _userHandOverCRM;
        public string userHandOverCRM
        {
            get { return _userHandOverCRM; }
            set { _userHandOverCRM = value; OnPropertyChanged("userHandOverCRM"); }
        }
        private Detail_Proposet detailPropose;
        InforDx_Proposing dataProposeHome;
        ListProposingSendAll dataProposeMine;
        ManagerHome managerHome;
        public pageViewDetailPropose(ManagerHome _managerHome, Detail_Proposet _detailPropose, InforDx_Proposing _dataProposeHome, ListProposingSendAll _dataProposeMine)
        {
            InitializeComponent();
            managerHome = _managerHome;
            detailPropose = _detailPropose;
            if (_dataProposeHome != null)
            {
                tb_GroundPropose.Text = _dataProposeHome.type_dx_string;
                dataProposeHome = _dataProposeHome;
            }
            else
            {
                tb_GroundPropose.Text = _dataProposeMine.name_type_dx;
                dataProposeMine = _dataProposeMine;
            }
            listLeaderComfirm = _detailPropose.lanh_dao_duyet.ToList();
            listUserFollow = _detailPropose.nguoi_theo_doi.ToList();
            listStatuComfirm = _detailPropose.lich_su_duyet.ToList();
            Nd.UpdateOrder(_detailPropose.thong_tin_chung.nghi_phep.nd);
            listCalendarOnLeave = _detailPropose.thong_tin_chung.nghi_phep.nd.ToList();
            tb_ReasonCreatePropse.Text = _detailPropose.thong_tin_chung.nghi_phep.ly_do;
            userHandOverCRM = _detailPropose.thong_tin_chung.nghi_phep.ng_ban_giao_string_CRM;
            type_duyet = _detailPropose.type_duyet;
            nhom_de_xuat = _detailPropose.nhom_de_xuat;
            confirm_status = _detailPropose.confirm_status;
            tb_NamePropose.Text = _detailPropose.ten_de_xuat.ToString();
            tb_TimeCreatePropose.Text = _detailPropose.thoi_gian_tao_string;
            TimeUpdatePropose.Text = $"{_detailPropose.cap_nhat} Ngày trước";
            NameUserCreatePropose.Text = _detailPropose.nguoi_tao;
            tb_UserCreate.Text = _detailPropose.nguoi_tao;
            tb_TypeComfirm.Text = _detailPropose.kieu_phe_duyet_format;
            
            
        }

        private void ScollCreateProposing(object sender, MouseWheelEventArgs e)
        {
            scoll.ScrollToVerticalOffset(scoll.VerticalOffset - e.Delta);
        }
    }
}
