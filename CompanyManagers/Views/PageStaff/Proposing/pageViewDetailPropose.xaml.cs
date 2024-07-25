using CompanyManagers.Models.ModelRose;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using static CompanyManagers.Views.Home.ManagerHome;

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

        private List<HoaHong_DetailPropose> _listRoseRevenue = new List<HoaHong_DetailPropose>();
        public List<HoaHong_DetailPropose> listRoseRevenue
        {
            get { return _listRoseRevenue; }
            set { _listRoseRevenue = value; OnPropertyChanged("listRoseRevenue"); }
        }
        
        private List<LichLamViec_DetailPropose> _listCalendaWork = new List<LichLamViec_DetailPropose>();
        public List<LichLamViec_DetailPropose> listCalendaWork
        {
            get { return _listCalendaWork; }
            set { _listCalendaWork = value; OnPropertyChanged("listCalendaWork"); }
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

        private bool _confirmOverdue;
        public bool confirmOverdue
        {
            get { return _confirmOverdue; }
            set { _confirmOverdue = value; OnPropertyChanged("confirmOverdue"); }
        }
        
        private bool _viewCalendarWork;
        public bool viewCalendarWork
        {
            get { return _viewCalendarWork; }
            set { _viewCalendarWork = value; OnPropertyChanged("viewCalendarWork"); }
        }
        private string _userHandOverCRM;
        public string userHandOverCRM
        {
            get { return _userHandOverCRM; }
            set { _userHandOverCRM = value; OnPropertyChanged("userHandOverCRM"); }
        }
        private int _deletePropose;
        public int deletePropose
        {
            get { return _deletePropose; }
            set { _deletePropose = value; OnPropertyChanged("deletePropose"); }
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
            listRoseRevenue.Add(_detailPropose.thong_tin_chung.hoa_hong);
            listRoseRevenue = listRoseRevenue.ToList();
            listCalendaWork.Add(_detailPropose.thong_tin_chung.lich_lam_viec);
            listCalendaWork = listCalendaWork.ToList();
            tb_ReasonCreatePropse.Text = _detailPropose.thong_tin_chung.nghi_phep.ly_do;
            userHandOverCRM = _detailPropose.thong_tin_chung.nghi_phep.ng_ban_giao_string_CRM;
            type_duyet = _detailPropose.type_duyet;
            nhom_de_xuat = _detailPropose.nhom_de_xuat;
            confirmOverdue = _detailPropose.qua_han_duyet;
            confirm_status = _detailPropose.confirm_status;
            deletePropose = _detailPropose.del_type;
            tb_NamePropose.Text = _detailPropose.ten_de_xuat.ToString();
            tb_TimeCreatePropose.Text = _detailPropose.thoi_gian_tao_string;
            TimeUpdatePropose.Text = $"{_detailPropose.cap_nhat} Ngày trước";
            NameUserCreatePropose.Text = _detailPropose.nguoi_tao;
            tb_InforTimeCreatePropose.Text = _detailPropose.thoi_gian_tao_string;
            tb_UserCreatePropose.Text = _detailPropose.nguoi_tao;
            tb_UserCreate.Text = _detailPropose.nguoi_tao;
            tb_TypeComfirm.Text = _detailPropose.kieu_phe_duyet_format;
            LoadDataCalendarWork(DateTime.Now.Month, DateTime.Now.Year);
        }
        int startDay;
        private List<lichlamviec> _listLichProposing;

        public List<lichlamviec> listLichProposing
        {
            get { return _listLichProposing; }
            set
            {
                _listLichProposing = value;
                OnPropertyChanged("listLichProposing");
            }
        }
        public void LoadDataCalendarWork(int monthSelected, int yearSelected)
        {
            try
            {
                startDay = (int)new DateTime(yearSelected, monthSelected, 1).DayOfWeek;
                listLichProposing = new List<lichlamviec>();
                // Lấy ngày dầu tiên trong tuần của tháng trước
                for (int i = 0; i < startDay; i++)
                {
                    var x = DateTime.DaysInMonth(yearSelected, monthSelected - 1);
                    listLichProposing.Add(
                        new lichlamviec() { id = listLichProposing.Count, DayInCalendar = x - i, shiftSelected = 0, statusClick = 0 });
                }
                listLichProposing.Reverse();
                
                //Lấy ngày của tháng hiện tại
                for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                {
                   var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, /*shiftSelected = _listShift.Count,*/ statusClick = 1 };
                   listLichProposing.Add(d);
                }
                //Lấy ngày đầu tiên trong tuần của tháng tiếp theo
                int n = 42 - listLichProposing.Count;
                for (int i = 1; i <= n; i++)
                {
                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = 0, statusClick = 0 };
                    listLichProposing.Add(d);
                }
                listLichProposing = listLichProposing.ToList();
            }
            catch (System.Exception)
            {
            }
        }


        private void ScollCreateProposing(object sender, MouseWheelEventArgs e)
        {
            scoll.ScrollToVerticalOffset(scoll.VerticalOffset - e.Delta);
        }

        private void ShowDetailCalendaWork(object sender, MouseButtonEventArgs e)
        {
            LichLamViec_DetailPropose dataCalendaWork = (LichLamViec_DetailPropose)(sender as Border).DataContext;
            if (dataCalendaWork != null)
            {
                viewCalendarWork = true;
            }
        }

        private void ClosePopup(object sender, MouseButtonEventArgs e)
        {
            viewCalendarWork = false;
        }
    }
}
