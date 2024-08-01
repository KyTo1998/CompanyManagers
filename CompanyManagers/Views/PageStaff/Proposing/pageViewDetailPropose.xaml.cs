using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Input;
using static CompanyManagers.Views.Home.ManagerHome;
using System.Globalization;
using System.Net;
using System.Text;
using System.Windows.Media.Animation;
using System.Diagnostics;

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
        
        private List<FileKem_DetailPropose> _listFileAndImg = new List<FileKem_DetailPropose>();
        public List<FileKem_DetailPropose> listFileAndImg
        {
            get { return _listFileAndImg; }
            set { _listFileAndImg = value; OnPropertyChanged("listFileAndImg"); }
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

        private bool _viewInputRefuseComfirm;
        public bool viewInputRefuseComfirm
        {
            get { return _viewInputRefuseComfirm; }
            set { _viewInputRefuseComfirm = value; OnPropertyChanged("viewInputRefuseComfirm"); }
        } 
        
        private bool _viewForwardefuseComfirm;
        public bool viewForwardefuseComfirm
        {
            get { return _viewForwardefuseComfirm; }
            set { _viewForwardefuseComfirm = value; OnPropertyChanged("viewForwardefuseComfirm"); }
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

        private int _NumberProposeInList = 0;
        public int NumberProposeInList
        {
            get { return _NumberProposeInList; }
            set { _NumberProposeInList = value; OnPropertyChanged("NumberProposeInList"); }
        }

        private bool _RoseUserComfirm;
        public bool RoseUserComfirm
        {
            get { return _RoseUserComfirm; }
            set { _RoseUserComfirm = value; OnPropertyChanged("RoseUserComfirm"); }
        }
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
        private List<list_ngay_lam_viec> _listShiftForDay = new List<list_ngay_lam_viec>();
        public List<list_ngay_lam_viec> listShiftForDay
        {
            get { return _listShiftForDay; }
            set
            {
                _listShiftForDay = value;
                OnPropertyChanged("listShiftForDay");
            }
        }
        public class list_ngay_lam_viec
        {
            public string date { get; set; }
            public DateTime datetime 
            {
                get { return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture); }
                set { } 
            }

            public string shift_id { get; set; }
            public List<string> list_shift_id 
            {
                get 
                {
                    string[] stringArray = shift_id.Split(',');
                    List<string> stringList = new List<string>(stringArray);
                    return stringList;
                }
                set { }
            } 
            private List<string> _list_shift_name = new List<string>();
            public List<string> list_shift_name 
            {
                get { return _list_shift_name; }
                set { _list_shift_name = value; } 
            }
        }
        public class Root_ngaylamviec
        {
            public int type { get; set; }
            public List<list_ngay_lam_viec> data { get; set; }
        }
        private List<InforDx_Proposing> listProposingHome;
        private List<ListProposingSendAll> listProposingSendAll;
        private Detail_Proposet detailPropose;
        InforDx_Proposing dataProposeHome;
        ListProposingSendAll dataProposeMine;
        ManagerHome managerHome;
        int startDay;

        public pageViewDetailPropose(ManagerHome _managerHome, Detail_Proposet _detailPropose, InforDx_Proposing _dataProposeHome, ListProposingSendAll _dataProposeMine, List<InforDx_Proposing> _listProposingHome, List<ListProposingSendAll> _listProposingSendAll)
        {
            InitializeComponent();
            managerHome = _managerHome;
            detailPropose = _detailPropose;
            listProposingHome = _listProposingHome;
            listProposingSendAll = _listProposingSendAll;
            if (_detailPropose.nguoi_tao != _managerHome.UserName)
            {
                RoseUserComfirm = true;
            }
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
            listFileAndImg = _detailPropose.file_kem.ToList();
            listLeaderComfirm = _detailPropose.lanh_dao_duyet.ToList();
            SelectUserNext.ItemsSourceSelected = listLeaderComfirm;
            listUserFollow = _detailPropose.nguoi_theo_doi.ToList();
            listStatuComfirm = _detailPropose.lich_su_duyet.ToList();
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
            tb_RefuseComfirm.Text = _detailPropose.ly_do_tu_choi;
            if (_detailPropose.nhom_de_xuat == 20)
            {
                listRoseRevenue.Add(_detailPropose.thong_tin_chung.hoa_hong);
                listRoseRevenue = listRoseRevenue.ToList();
            }
            if (_detailPropose.nhom_de_xuat == 1)
            {
                Nd.UpdateOrder(_detailPropose.thong_tin_chung.nghi_phep.nd);
                listCalendarOnLeave = _detailPropose.thong_tin_chung.nghi_phep.nd.ToList();
                tb_ReasonCreatePropse.Text = _detailPropose.thong_tin_chung.nghi_phep.ly_do;
                userHandOverCRM = _detailPropose.thong_tin_chung.nghi_phep.ng_ban_giao_string_CRM;
            }
            if (_detailPropose.nhom_de_xuat == 18)
            {
                listCalendaWork.Add(_detailPropose.thong_tin_chung.lich_lam_viec);
                listCalendaWork = listCalendaWork.ToList();
                string jsonString = _detailPropose.thong_tin_chung.lich_lam_viec.ngay_lam_viec.Substring(1, _detailPropose.thong_tin_chung.lich_lam_viec.ngay_lam_viec.Length - 2);
                Root_ngaylamviec dataNgayLamViec = JsonConvert.DeserializeObject<Root_ngaylamviec>(jsonString);
                GetListShiftAll(dataNgayLamViec.data);
            }
            if (_detailPropose.nhom_de_xuat == 17)
            {
                tb_ReasonCreatePropse.Text = _detailPropose.thong_tin_chung.xac_nhan_cong.ly_do;
                TimeComfirmCC.Text = _detailPropose.thong_tin_chung.xac_nhan_cong.time_xnc_display;
                ShiftNameCC.Text = _detailPropose.thong_tin_chung.xac_nhan_cong.ca_xnc;
                TimeInShiftCC.Text = _detailPropose.thong_tin_chung.xac_nhan_cong.time_vao_ca;
                TimeOutShiftCC.Text = _detailPropose.thong_tin_chung.xac_nhan_cong.time_het_ca;
                ResuelCC.Text = _detailPropose.thong_tin_chung.xac_nhan_cong.ly_do;
            }
        }
        public async void GetListShiftAll(List<list_ngay_lam_viec> _list_ngay_lam_viec)
        {
            var client = HttpClientSingleton.Instance;
            var request = new HttpRequestMessage(HttpMethod.Get, UrlApi.Url_Api_Shift + UrlApi.Name_Api_ListShiftAll);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var returl = await response.Content.ReadAsStringAsync();
                Root_ShiftAll dataShiftAll = JsonConvert.DeserializeObject<Root_ShiftAll>(returl);
                if (dataShiftAll != null)
                {
                    foreach (var itemDayWork in _list_ngay_lam_viec)
                    {
                        foreach (var itemDayAll in dataShiftAll.data.items)
                        {
                            if (itemDayWork.list_shift_id.Contains(itemDayAll.shift_id.ToString()))
                            {
                                itemDayWork.list_shift_name.Add(itemDayAll.shift_name);
                            }
                        }
                    }
                    listShiftForDay = _list_ngay_lam_viec.ToList();
                }
                LoadDataCalendarWork(_list_ngay_lam_viec.Find(x => x.datetime != null).datetime.Month, _list_ngay_lam_viec.Find(x => x.datetime != null).datetime.Year, listShiftForDay);
            }
        }
        int sttAdd;
        public void LoadDataCalendarWork(int monthSelected, int yearSelected, List<list_ngay_lam_viec> _listShiftForDay)
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
                    foreach (var itemForDay in _listShiftForDay)
                    {
                        if (itemForDay.datetime.Day == i)
                        {
                            var d1 = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, list_shift_name_picker = itemForDay.list_shift_name , statusClick = 1 };
                            listLichProposing.Add(d1);
                            sttAdd = i;
                        }
                    }
                    if (sttAdd != i)
                    {
                        var d2 = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, statusClick = 1 };
                        listLichProposing.Add(d2);
                    }
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
                tb_MonthCalendarWork.Text = detailPropose.thong_tin_chung.lich_lam_viec.thang_ap_dung_display;
            }
        }

        private void ClosePopup(object sender, MouseButtonEventArgs e)
        {
            if (viewCalendarWork)
            {
                viewCalendarWork = false;
            }
            if (viewInputRefuseComfirm)
            {
                viewInputRefuseComfirm = false;
            }
            if (viewForwardefuseComfirm)
            {
                viewForwardefuseComfirm = false;
            }
        }
        InforDx_Proposing dataProposeHomeNext;
        ListProposingSendAll dataProposeMineNext;
        private void NextDetailProposeToLeft(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataProposeHome != null)
                {
                    if (dataProposeHomeNext == null)
                    {
                        dataProposeHomeNext = dataProposeHome;
                    }
                    if (listProposingHome.IndexOf(dataProposeHomeNext) - 1 >= 0)
                    {
                        dataProposeHomeNext = listProposingHome[listProposingHome.IndexOf(dataProposeHomeNext) - 1];
                        managerHome.GetDetailPropose(dataProposeHomeNext, null, listProposingHome, listProposingSendAll);
                    }
                }
                else
                {
                    if (dataProposeMineNext == null)
                    {
                        dataProposeMineNext = dataProposeMine;
                    }
                    if (listProposingSendAll.IndexOf(dataProposeMineNext) - 1 >= 0)
                    {
                        NumberProposeInList--;
                        dataProposeMineNext = listProposingSendAll[listProposingSendAll.IndexOf(dataProposeMineNext) - 1];
                        managerHome.GetDetailPropose(null, dataProposeMineNext, listProposingHome, listProposingSendAll);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void NextDetailProposeToRight(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataProposeHome != null)
                {
                    if (dataProposeHomeNext == null)
                    {
                        dataProposeHomeNext = dataProposeHome;
                    }
                    if (listProposingHome.IndexOf(dataProposeHomeNext) + 1 <= listProposingHome.Count - 1)
                    {
                        dataProposeHomeNext = listProposingHome[listProposingHome.IndexOf(dataProposeHomeNext) + 1];
                        managerHome.GetDetailPropose(dataProposeHomeNext, null, listProposingHome, listProposingSendAll);
                    }
                }
                else
                {
                    if (dataProposeMineNext == null)
                    {
                        dataProposeMineNext = dataProposeMine;
                    }
                    if (listProposingSendAll.IndexOf(dataProposeMineNext) + 1 <= listProposingSendAll.Count - 1)
                    {
                        dataProposeMineNext = listProposingSendAll[listProposingSendAll.IndexOf(dataProposeMineNext) + 1];
                        managerHome.GetDetailPropose(null, dataProposeMineNext, listProposingHome, listProposingSendAll);
                    }
                }
            }
            catch (Exception)
            {
            }   
        }
        private void ClickSelectUserNext(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ClickDeletePropose(object sender, MouseButtonEventArgs e)
        {
            //type = 0 xóa đề xuất
            managerHome.DeletePropose(detailPropose.id_de_xuat, 0);
        }

        private void ShowInputRefuse(object sender, MouseButtonEventArgs e)
        {
            viewInputRefuseComfirm = true;
        }

        private void ClickRefuseComfirm(object sender, MouseButtonEventArgs e)
        {
            //type = 2 từ trối đề xuất+
            managerHome.RefuseComfirm(detailPropose.id_de_xuat, tb_InputRefuseComfirm.Text, 2, 0);
            managerHome.GetDetailPropose(dataProposeHome, dataProposeMine, listProposingHome, listProposingSendAll);
        }

        private void ClickReceivePropose(object sender, MouseButtonEventArgs e)
        {
            // type = 6 tiếp nhận đề xuất
            managerHome.RefuseComfirm(detailPropose.id_de_xuat, null, 6, 0);
            managerHome.GetDetailPropose(dataProposeHome, dataProposeMine, listProposingHome, listProposingSendAll);
        }

        private void ClickAcceptComfirm(object sender, MouseButtonEventArgs e)
        {
            // type = 1 chấp thuận đề xuất
            managerHome.RefuseComfirm(detailPropose.id_de_xuat, null, 1, 0);
            managerHome.GetDetailPropose(dataProposeHome, dataProposeMine, listProposingHome, listProposingSendAll);
        }

        private void ClickAbortComfirm(object sender, MouseButtonEventArgs e)
        {
            // type = 5 Hủy duyệt đề xuất
            managerHome.RefuseComfirm(detailPropose.id_de_xuat, null, 5, 0);
            managerHome.GetDetailPropose(dataProposeHome, dataProposeMine, listProposingHome, listProposingSendAll);
        }

        private void SelectUserComfirmNext(object sender, MouseButtonEventArgs e)
        {
            viewForwardefuseComfirm = true;
        }

        private void ClickComfirmNext(object sender, MouseButtonEventArgs e)
        {
            // type = 4 duyệt chuyển tiếp đề xuất
            managerHome.RefuseComfirm(detailPropose.id_de_xuat, null, 4, ((LanhDaoDuyet)SelectUserNext.SelectedItemSelected).idQLC);
            managerHome.GetDetailPropose(dataProposeHome, dataProposeMine, listProposingHome, listProposingSendAll);
        }

        private void ClickForcedToDo(object sender, MouseButtonEventArgs e)
        {
            // type = 3 buộc đi làm cho đề xuát nghỉ phép
            managerHome.RefuseComfirm(detailPropose.id_de_xuat, null, 3, 0);
            managerHome.GetDetailPropose(dataProposeHome, dataProposeMine, listProposingHome, listProposingSendAll);
        }

        private void ShowFileImage(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            if (grid != null)
            {
                grid.BeginAnimation(Grid.WidthProperty, new DoubleAnimation(200, TimeSpan.FromSeconds(1)));
                grid.BeginAnimation(Grid.HeightProperty, new DoubleAnimation(200, TimeSpan.FromSeconds(1)));
            }
        }

        private void DowloadFile(object sender, MouseButtonEventArgs e)
        {
            FileKem_DetailPropose dataFile = (FileKem_DetailPropose)(sender as Grid).DataContext;
            if (dataFile != null)
            {
                if (dataFile.type_file == "file")
                {
                    Process process = new Process();
                    process.StartInfo.FileName = dataFile.file;
                    process.StartInfo.Arguments = "";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.Start();
                }
            }
        }
    }
}
