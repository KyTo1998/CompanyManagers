﻿using CompanyManagers.Common.Popups;
using CompanyManagers.Controllers;
using CompanyManagers.Models.HomeFunction;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static CompanyManagers.Views.Home.ManagerHome;

namespace CompanyManagers.Views.PageStaff.Proposing
{
    /// <summary>
    /// Interaction logic for pageCreateProposedWorkSchedule.xaml
    /// </summary>
    public partial class pageCreateProposedWorkSchedule : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        public class ListBodyCalendarWork
        {
            public string date { get; set; }
            public string shift_id { get; set; }
        }
        private int _typeCategoryProposing;
        public int typeCategoryProposing
        {
            get { return _typeCategoryProposing; }
            set { _typeCategoryProposing = value;OnPropertyChanged("typeCategoryProposing");}
        }
        public class CalendarWork
        {
            public int id_CalendarWork { get; set; }
            public string name_CalendarWork { get; set; }
        }
        List<typeConfirm> lstCalendarWork = new List<typeConfirm>();

        private List<Item_ShiftAll> _listShiftAll;
        public List<Item_ShiftAll> listShiftAll
        {
            get { return _listShiftAll; }
            set { _listShiftAll = value; OnPropertyChanged("listShiftAll"); }
        }

        private List<Item_ShiftAll> _listShiftAllSelected;
        public List<Item_ShiftAll> listShiftAllSelected
        {
            get { return _listShiftAllSelected; }
            set { _listShiftAllSelected = value; OnPropertyChanged("listShiftAllSelected"); }
        }
        private List<ListUsersDuyet> _dataListUserComfrim;
        public List<ListUsersDuyet> dataListUserComfrim
        {
            get { return _dataListUserComfrim; }
            set { _dataListUserComfrim = value; OnPropertyChanged("dataListUserComfrim"); }
        }
        private bool _statusScoll;
        public bool statusScoll
        {
            get { return _statusScoll; }
            set { _statusScoll = value; OnPropertyChanged("statusScoll"); }
        }

        private bool _statutSelectDay;
        public bool statutSelectDay
        {
            get { return _statutSelectDay; }
            set { _statutSelectDay = value; OnPropertyChanged("statutSelectDay"); }
        }

        private bool _statusValidate = true;
        public bool statusValidate
        {
            get { return _statusValidate; }
            set { _statusValidate = value; OnPropertyChanged("statusValidate"); }
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

        private string _dateSelectedForDay;
        public string dateSelectedForDay
        {
            get { return _dateSelectedForDay; }
            set
            {
                _dateSelectedForDay = value;
                OnPropertyChanged("dateSelectedForDay");
            }
        }
        int startDay;
        PagePopupGrayColor pagePopupGrayColor { get; set; }

        Result_CategoryProposing dataCategoryProposing;
        ManagerHome managerHome { set; get; }
        public pageCreateProposedWorkSchedule(ManagerHome _managerHome, Result_CategoryProposing _dataCategoryProposing)
        {
            InitializeComponent();
            managerHome = _managerHome;
            dataCategoryProposing = _dataCategoryProposing;
            typeCategoryProposing = _dataCategoryProposing.cate_dx;
            tb_UserNameCreate.Text = _managerHome.UserCurrent.user_info.ep_name;
            tb_CategoryProposingCreate.Text = _dataCategoryProposing.name_cate_dx;
            SelectTypeComfirm.ItemsSourceSelected = _managerHome.lstTypeConfirms.ToList();
            lstCalendarWork.Add(new typeConfirm() { id_Custom = 1, name_Custom = "Thứ 2 - Thứ 6" });
            lstCalendarWork.Add(new typeConfirm() {id_Custom = 2, name_Custom = "Thứ 2 - Thứ 7" });
            lstCalendarWork.Add(new typeConfirm() {id_Custom = 3, name_Custom = "Thứ 2 - CN" });
            SelectCalendarWork.ItemsSourceSelected = lstCalendarWork.ToList();
            SelectUserComfirm.ItemsSource = _managerHome.dataListUserComfrim.ToList();
            SelectUserFollow.ItemsSource = _managerHome.dataListUserFollow.ToList();
            listShiftAll = _managerHome.dataListShiftAll.ToList();
            listShiftAllSelected = _managerHome.dataListShiftAll.ToList();
            tb_InputNameProposing.Text = $"Lịch làm việc cá nhân của {managerHome.UserName}";
            tb_InputReasonCreateProposing.Text = "Tạo lịch làm việc theo tuần";
            SelectUserComfirm.PlaceHolder = $"Bạn cần chọn {managerHome.userNumberConfirm} người duyệt";
        }
        
        public async void CreateProposing()
        {
            try
            {
                List<string> listStringUserComfirm = new List<string>();
                string listStringCalendarForDay = "";
                List< ListBodyCalendarWork> lstCalendarWork = new List< ListBodyCalendarWork >();
                foreach (var item in dataListUserComfrim)
                {
                    listStringUserComfirm.Add(item.idQLC.ToString());
                }
                string StringUserComfirm = string.Join(",", listStringUserComfirm);
                long MonthApply = ConvertToEpochTime(StartDateOnLeave.SelectedDate.Value.ToString("dd/MM/yyyy"));
                foreach (var item in listLichProposing)
                {
                    if (item.listShiftSelectedAll != null)
                    {
                        if (item.listShiftSelectedAll.Count > 0)
                        {
                            listStringCalendarForDay = string.Join(",", item.listShiftSelectedAll.Select(x => x.shift_id));
                            lstCalendarWork.Add(new ListBodyCalendarWork() { date = DateTime.Parse(item.dayString).ToString("yyyy-MM-dd"), shift_id = listStringCalendarForDay });
                        }
                    }
                }
                var ObjectCalendarWork = new
                {
                    type = 1,
                    data = lstCalendarWork
                };
                string JsonCalendarWork = JsonConvert.SerializeObject(ObjectCalendarWork);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, UrlApi.Url_Api_Proposing + UrlApi.Name_Api_CreateProposeCalendarWork);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_InputNameProposing.Text), "name_dx");
                content.Add(new StringContent(((typeConfirm)SelectTypeComfirm.SelectedItemSelected).id_Custom.ToString()), "kieu_duyet");
                content.Add(new StringContent(StringUserComfirm), "id_user_duyet");
                content.Add(new StringContent(((ListUsersTheoDoi)SelectUserFollow.SelectedItem).idQLC.ToString()), "id_user_theo_doi");
                content.Add(new StringContent(tb_InputReasonCreateProposing.Text), "ly_do");
                content.Add(new StringContent(MonthApply.ToString()), "thang_ap_dung");
                content.Add(new StringContent(MonthApply.ToString()), "ngay_bat_dau");
                content.Add(new StringContent(""), "ca_lam_viec");
                content.Add(new StringContent(((typeConfirm)SelectCalendarWork.SelectedItemSelected).id_Custom.ToString()), "lich_lam_viec");
                int numberFile = 0;
                if (managerHome.lstInfoFileCreateProposing != null)
                {
                    foreach (var item in managerHome.lstInfoFileCreateProposing)
                    {
                        content.Add(new StreamContent(File.OpenRead(item.FullName)), $"fileKem[{numberFile}]", item.FullName);
                        numberFile++;
                    }
                }
                content.Add(new StringContent("[" + JsonCalendarWork + "]"), "ngay_lam_viec");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responsContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất xin nghỉ phép thành công", ""));
                }
            }
            catch (Exception)
            {
                managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất thất bại, lỗi hệ thống", ""));
            }
        } 

        public void LoadDataCalendarWork(int monthSelected, int yearSelected, typeConfirm selectedStartToEnd, List<Item_ShiftAll> _listShift)
        {
            try
            {
                startDay = (int)new DateTime(yearSelected, monthSelected, 1).DayOfWeek;
                listLichProposing = new List<lichlamviec>();
                // Lấy ngày dầu tiên trong tuần của tháng trước
                if (monthSelected - 1 > 0)
                {
                    for (int i = 0; i < startDay; i++)
                    {
                        var x = DateTime.DaysInMonth(yearSelected, monthSelected - 1);
                        listLichProposing.Add(
                            new lichlamviec() { id = listLichProposing.Count, DayInCalendar = x - i, shiftSelected = 0, statusClick = 0 });
                    }
                    listLichProposing.Reverse();
                }
                else if (monthSelected - 1 == 0)
                {
                    for (int i = 0; i < startDay; i++)
                    {
                        var x = DateTime.DaysInMonth(yearSelected - 1, 12);
                        listLichProposing.Add(
                            new lichlamviec() { id = listLichProposing.Count, DayInCalendar = x - i, });
                    }
                    listLichProposing.Reverse();
                }
                List<int> lstPastDays = GetAllPastDaysInMonth(monthSelected, yearSelected);
                List<int> lstSaturdays = GetAllSaturdaysInMonth(monthSelected, yearSelected);
                List<int> lstSundays = GetAllSundaysInMonth(monthSelected, yearSelected);
                List<int> lstPastDaySeleced = GetAllPastDaysInMonth(monthSelected, yearSelected, StartDateOnLeave.SelectedDate.Value);
                //Lấy ngày của tháng hiện tại
                if (selectedStartToEnd != null)
                {
                    if (selectedStartToEnd.id_Custom == 1)
                    {
                        for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                        {
                            if (lstSundays.Contains(i) || lstSaturdays.Contains(i))
                            {
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 0 };
                                listLichProposing.Add(d);
                            }
                            else
                            {
                                if (lstPastDays.Contains(i) || lstPastDaySeleced.Contains(i))
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1};
                                    listLichProposing.Add(d);
                                }
                                else
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1,statusPast = 0, listShiftSelectedAll = _listShift , dayString  = $"{yearSelected}-{monthSelected}-{i}"};
                                    listLichProposing.Add(d);
                                }
                            }
                        }
                    }
                    else if (selectedStartToEnd.id_Custom == 2)
                    {
                        for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                        {
                            if (lstSundays.Contains(i))
                            {
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 0};
                                listLichProposing.Add(d);
                            }
                            else
                            {
                                if (lstPastDays.Contains(i) || lstPastDaySeleced.Contains(i))
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1};
                                    listLichProposing.Add(d);
                                }
                                else
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift, dayString = $"{yearSelected}-{monthSelected}-{i}" };
                                    listLichProposing.Add(d);
                                }
                            }
                        }
                    }
                    else if (selectedStartToEnd.id_Custom == 3)
                    {
                        for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                        {
                            if (lstPastDays.Contains(i) || lstPastDaySeleced.Contains(i))
                            {
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1};
                                listLichProposing.Add(d);
                            }
                            else
                            {
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift, dayString = $"{yearSelected}-{monthSelected}-{i}" };
                                listLichProposing.Add(d);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                    {
                        if (lstPastDays.Contains(i) || lstPastDaySeleced.Contains(i))
                        {
                            var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1};
                            listLichProposing.Add(d);
                        }
                        else
                        {
                            var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift, dayString = $"{yearSelected}-{monthSelected}-{i}" };
                            listLichProposing.Add(d);
                        }
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
        
        private void ValidateCreatePropose(string type)
        {
            if (dataListUserComfrim == null) { dataListUserComfrim = new List<ListUsersDuyet>(); }
            if (SelectCalendarWork.SelectedIndexSelected < 0 && type == "SelectedStartDateOnLeave")
            {
                statusValidate = false;
                tb_Notication.Text = "Bạn chưa chọn lịch làm việc";
            }
            else if (tb_InputNameProposing.Text == "")
            {
                statusValidate = false;
                tb_Notication.Text = "Bạn chưa nhập tên đề xuất";
            }
            else if (tb_InputReasonCreateProposing.Text == "")
            {
                statusValidate = false;
                tb_Notication.Text = "Bạn chưa nhập lý do tạo đề xuất";
            }
            else if (SelectTypeComfirm.SelectedIndexSelected < 0)
            {
                statusValidate = false;
                tb_Notication.Text = "Bạn chưa chọn kiểu duyệt";
            }
            else if (dataListUserComfrim.Count == 0)
            {
                statusValidate = false;
                tb_Notication.Text = $"Bạn chưa chọn người duyệt, số người duyệt là {managerHome.userNumberConfirm}";
            }
            else if (SelectUserFollow.SelectedIndex < 0)
            {
                statusValidate = false;
                tb_Notication.Text = "Bạn chưa chọn người theo dõi";
            }
            else
            {
                statusValidate = true;
            }
        }
        private void ClickSelectTypeComfirm(object sender, SelectionChangedEventArgs e)
        {
           
        }
        typeConfirm selectedStartToEnd = new typeConfirm();
        private void ClickSelectCalendarWork(object sender, SelectionChangedEventArgs e)
        {
            selectedStartToEnd = SelectCalendarWork.SelectedItemSelected as typeConfirm;
            ValidateCreatePropose("ClickSelectCalendarWork");
            if (statusValidate == true && StartDateOnLeave.SelectedDate != null)
            {
                LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, selectedStartToEnd, listShift);
            }
        }
        private void SelectionChangeUserComfirm(object sender, SelectionChangedEventArgs e)
        {
            if (SelectUserComfirm.Text != null)
            {
                ListUsersDuyet dataUserComfirm = new ListUsersDuyet();
                dataUserComfirm = SelectUserComfirm.SelectedItem as ListUsersDuyet;
                lsvUserComfirmSelected.Visibility = Visibility.Visible;
                if (dataListUserComfrim == null)
                {
                    dataListUserComfrim = new List<ListUsersDuyet>();
                }
                if (!dataListUserComfrim.Contains(dataUserComfirm))
                {
                    dataListUserComfrim.Add(dataUserComfirm);
                    if (dataListUserComfrim.Count > 1)
                    {
                        btnDeleteUserConfirmAll.Visibility = Visibility.Visible;
                    }
                }
                dataListUserComfrim = dataListUserComfrim.ToList();
                SelectUserComfirm.PlaceHolder = null;
                SelectUserComfirm.Text = null;
                SelectUserComfirm.VerticalAlignment = VerticalAlignment.Center;
            }
        }
        private void SelectionChangeUserFollow(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void SelectedStartDateOnLeave(object sender, SelectionChangedEventArgs e)
        {
            ValidateCreatePropose("SelectedStartDateOnLeave");
            if (statusValidate == true)
            {
                MonthCalendar.Text = StartDateOnLeave.SelectedDate.Value.ToString("MM-yyyy");
                LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, selectedStartToEnd, listShift);
            }
        }
      
        private void DeleteUserConfirmAll(object sender, MouseButtonEventArgs e)
        {
            if (dataListUserComfrim.Count > 0)
            {
                dataListUserComfrim.Clear();
                btnDeleteUserConfirmAll.Visibility = Visibility.Collapsed;
                lsvUserComfirmSelected.Visibility = Visibility.Collapsed;
                SelectUserComfirm.PlaceHolder = "Chọn người duyệt";
                dataListUserComfrim = dataListUserComfrim.ToList();
                if (dataListUserComfrim.Count == 0)
                {
                    statusValidate = false;
                    tb_Notication.Text = $"Bạn cần chọn {managerHome.userNumberConfirm} người duyệt";
                }
            }
        }

        private void DeleteFileGim(object sender, MouseButtonEventArgs e)
        {
            InfoFile infoFile = (InfoFile)(sender as Border).DataContext;
            if (infoFile != null)
            {
                lsvFileGim.Items.Remove(infoFile);
                lsvFileGim.Items.Refresh();
                TextHidenFileGim.Visibility = Visibility.Visible;
            }
        }

        private void AddFileGimForProposing(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog selectFile = new OpenFileDialog();
            selectFile.Filter = "All files (*.*)|*.*";
            selectFile.Multiselect = true;
            if (selectFile.ShowDialog() == true)
            {
               managerHome.selectionFile(selectFile.FileNames, lsvFileGim);
               lsvFileGim.Visibility = Visibility.Visible;
               TextHidenFileGim.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteUserConfirm(object sender, MouseButtonEventArgs e)
        {
            ListUsersDuyet dataUserComfirm = (ListUsersDuyet)(sender as Border).DataContext;
            if (dataUserComfirm != null)
            {
                dataListUserComfrim.Remove(dataUserComfirm);
                if (dataListUserComfrim.Count == 0)
                {
                    lsvUserComfirmSelected.Visibility = Visibility.Collapsed;
                    SelectUserComfirm.PlaceHolder = "Chọn người duyệt";
                }
                if (dataListUserComfrim.Count == 1)
                {
                    btnDeleteUserConfirmAll.Visibility = Visibility.Collapsed;
                }
                if (dataListUserComfrim.Count == 0)
                {
                    statusValidate = false;
                    tb_Notication.Text = $"Bạn cần chọn {managerHome.userNumberConfirm} người duyệt";
                }
                dataListUserComfrim = dataListUserComfrim.ToList();
            }
        }

        private void ScollCreateProposing(object sender, MouseWheelEventArgs e)
        {
            if (statusScoll == false)
            {
                scoll.ScrollToVerticalOffset(scoll.VerticalOffset - e.Delta);
            }
        }

       

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void SelectedShiftHover(object sender, MouseEventArgs e)
        {
            statusScoll = true;
        }

        private void SelectedShiftOutHover(object sender, MouseEventArgs e)
        {
            statusScoll = false;
        }

        private void ShowBoxSelectShift(object sender, MouseButtonEventArgs e)
        {
            if (borSelectedShift.Visibility == Visibility.Collapsed)
            {
                borSelectedShift.Visibility = Visibility.Visible;
            }
            else
            {
                borSelectedShift.Visibility = Visibility.Collapsed;
            }
        }
        List<Item_ShiftAll> listShift = new List<Item_ShiftAll>();
        private void SelectShiftPlan(object sender, MouseButtonEventArgs e)
        {
            Item_ShiftAll dataShiftSelected = (Item_ShiftAll)(sender as DockPanel).DataContext;
            if (dataShiftSelected != null)
            {
                statutSelectDay = false;
                dataShiftSelected.isSeleced = dataShiftSelected.isSeleced == "True" ? "False" : "True";
                if (dataShiftSelected.isSeleced == "True")
                {
                    listShift.Add(dataShiftSelected);
                    LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, selectedStartToEnd, listShift);
                }
                else
                {
                    listShift.Remove(dataShiftSelected);
                    LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, selectedStartToEnd, listShift);
                }
            }
        }

        int idSelectedForDay;
        private void SelectShiftForDay(object sender, MouseButtonEventArgs e)
        {
            lichlamviec dataSelected = (lichlamviec)(sender as Border).DataContext;
            statutSelectDay = true;
            shiftChange = new List<Item_ShiftAll>();
            if (dataSelected.listShiftSelectedAll.Count > 0)
            {
                shiftChange.AddRange(dataSelected.listShiftSelectedAll);
            }
            if (dataSelected != null)
            {
                dateSelectedForDay = $"{dataSelected.DayInCalendar}-{StartDateOnLeave.SelectedDate.Value.ToString("MM-yyyy")}";
                if (dataSelected.listShiftSelectedAll.Count > 0)
                {
                    foreach (var item in listShiftAllSelected)
                    {
                        if (dataSelected.listShiftSelectedAll.Contains(item))
                        {
                            item.isSelecedForDay = "True";
                        }
                        else
                        {
                            item.isSelecedForDay = "False";
                        }
                    }
                }
                else
                {
                    foreach(var item in listShiftAllSelected)
                    {
                        item.isSelecedForDay = "False";
                    }
                }
                idSelectedForDay = dataSelected.id;
                foreach (var item in listLichProposing)
                {
                    if (item.id == dataSelected.id && item.statusClick == 1)
                    {
                        item.statusClick = 2;
                    }
                    else
                    {
                        if (item.statusClick == 2)
                        {
                            item.statusClick = 1;
                        }
                    }
                }
            }
            
        }
        List<Item_ShiftAll> shiftChange { get ; set; }
        private void SelectedShiftForDayCheckbox(object sender, MouseButtonEventArgs e)
        {
            Item_ShiftAll dataShiftSelectedForDay = (Item_ShiftAll)(sender as DockPanel).DataContext;
            if (dataShiftSelectedForDay != null)
            {
                dataShiftSelectedForDay.isSelecedForDay = dataShiftSelectedForDay.isSelecedForDay == "True" ? "False" : "True";
                if (dataShiftSelectedForDay.isSelecedForDay == "True")
                {
                    if (listLichProposing != null)
                    {
                        shiftChange.Add(dataShiftSelectedForDay);
                        foreach (var listLichForDay in listLichProposing)
                        {
                            if (listLichForDay.id == idSelectedForDay)
                            {
                                listLichForDay.listShiftSelectedAll = shiftChange;
                            }
                        }
                        listLichProposing.Find(x => x.id == idSelectedForDay).shiftSelected++;
                    }
                }
                else
                {
                    if (listLichProposing != null)
                    {
                        shiftChange.Remove(dataShiftSelectedForDay);
                        foreach (var listLichForDay in listLichProposing)
                        {
                            if (listLichForDay.id == idSelectedForDay)
                            {
                                listLichForDay.listShiftSelectedAll = shiftChange;
                            }
                        }
                        listLichProposing.Find(x => x.id == idSelectedForDay).shiftSelected--;
                    }
                }
                listLichProposing = listLichProposing.ToList();
            }
        }
        private void SelectShiftPlan(object sender, RoutedEventArgs e)
        {

        }
        private void CLickCloseNotication(object sender, MouseButtonEventArgs e)
        {
            statusValidate = true;
        }

        private void CencalCalendar(object sender, MouseButtonEventArgs e)
        {
            if (listLichProposing != null)
            {
                foreach (var item in listLichProposing)
                {
                    if (item.listShiftSelectedAll != null)
                    {
                        item.listShiftSelectedAll.Clear();
                        item.shiftSelected = 0;
                    }
                }
            }
            statutSelectDay = false;
            listLichProposing = listLichProposing.ToList();
        }

        private void CloseBox(object sender, MouseButtonEventArgs e)
        {
             borSelectedShift.Visibility = Visibility.Collapsed;
        }

        private void ClosePopupCreateProposing(object sender, MouseButtonEventArgs e)
        {
            pagePopupGrayColor = new PagePopupGrayColor(managerHome);
            pagePopupGrayColor.Popup.NavigationService.Navigate(null);
            managerHome.PagePopup.NavigationService.Navigate(null);
        }

        private void CreateProposing(object sender, MouseButtonEventArgs e)
        {
            ValidateCreatePropose("CreateProposing");
            if (statusValidate == true)
            {
                CreateProposing();
            }
        }
    }
}
