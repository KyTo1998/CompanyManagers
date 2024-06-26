﻿using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

        int startDay, selectedDay; string date;
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
        }
        
        public void LoadDataCalendarWork(int monthSelected, int yearSelected, typeConfirm selectedStartToEnd, List<Item_ShiftAll> _listShift)
        {
            try
            {
                startDay = (int)new DateTime(yearSelected, monthSelected, 1).DayOfWeek;
                listLichProposing = new List<lichlamviec>();
                // Lấy số ngày dầu tiên trong tuần của tháng trước
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
                if (selectedStartToEnd != null)
                {
                    if (selectedStartToEnd.id_Custom == 1)
                    {
                        for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                        {
                            
                            if (lstSundays.Contains(i) || lstSaturdays.Contains(i))
                            {
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 0, listShiftSelectedAll = _listShift };
                                listLichProposing.Add(d);
                            }
                            else
                            {
                                if (lstPastDays.Contains(i))
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift };
                                    listLichProposing.Add(d);
                                }
                                else
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1,statusPast = 0, listShiftSelectedAll = _listShift };
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
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 0, listShiftSelectedAll = _listShift };
                                listLichProposing.Add(d);
                            }
                            else
                            {
                                if (lstPastDays.Contains(i))
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift };
                                    listLichProposing.Add(d);
                                }
                                else
                                {
                                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift };
                                    listLichProposing.Add(d);
                                }
                            }
                        }
                    
                    }
                    else if (selectedStartToEnd.id_Custom == 3)
                    {
                        for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                        {
                            
                            if (lstPastDays.Contains(i))
                            {
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift };
                                listLichProposing.Add(d);
                            }
                            else
                            {
                                var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift };
                                listLichProposing.Add(d);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                    {
                       
                        if (lstPastDays.Contains(i))
                        {
                            var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift };
                            listLichProposing.Add(d);
                        }
                        else
                        {
                            var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift };
                            listLichProposing.Add(d);
                        }
                    }
                }

                int n = 42 - listLichProposing.Count;
                for (int i = 1; i <= n; i++)
                {
                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = 0, statusClick = 0 };
                    listLichProposing.Add(d);
                }

                date = StartDateOnLeave.SelectedDate + "";
                //for (int i = 1; i <= DateTime.DaysInMonth(yearSelected, monthSelected); i++)
                //{
                //    string ngayString = "";
                   
                //    int x = (int)new DateTime(yearSelected, monthSelected, listLichProposing[i + startDay - 1].DayInCalendar).DayOfWeek;
                //    if (DateTime.Parse(date).Day <= listLichProposing[i + startDay - 1].DayInCalendar)
                //    {

                //        ngayString = yearSelected + "-" + monthSelected + "-" + listLichProposing[i + startDay - 1].DayInCalendar;
                //        if (selectedStartToEnd != null)
                //        {
                //            if (selectedStartToEnd.id_Custom == 1)
                //            {
                //                if (x >= 1 && x < 6)
                //                {
                //                    //foreach (var item in listShiftAll)
                //                    //{
                //                    //    _listShift.Add(item);
                //                    //}
                //                }
                //                if (lstSundays.Contains(i) || lstSaturdays.Contains(i))
                //                {
                //                    var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 0, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                    listLichProposing[i + startDay - 1] = d;
                //                }
                //                else
                //                {
                //                    if (lstPastDays.Contains(i))
                //                    {
                //                        var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                        listLichProposing[i + startDay - 1] = d;
                //                    }
                //                    else
                //                    {
                //                        var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                        listLichProposing[i + startDay - 1] = d;
                //                    }
                //                }
                                
                //            }
                //            if (selectedStartToEnd.id_Custom == 2)
                //            {
                //                if (x >= 1 && x < 7) 
                //                {
                //                    //foreach (var item in listShiftAll)
                //                    //{
                //                    //    _listShift.Add(item);
                //                    //}
                //                }
                //                if (lstSundays.Contains(i))
                //                {
                //                    var d = new lichlamviec() { id = listLichProposing.Count, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 0, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                    listLichProposing[i + startDay - 1] = d;
                //                }
                //                else
                //                {
                //                    if (lstPastDays.Contains(i))
                //                    {
                //                        var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                        listLichProposing[i + startDay - 1] = d;
                //                    }
                //                    else
                //                    {
                //                        var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                        listLichProposing[i + startDay - 1] = d;
                //                    }
                //                }
                //            }
                //            if (selectedStartToEnd.id_Custom == 3)
                //            {
                //                //foreach (var item in listShiftAll)
                //                //{
                //                //    _listShift.Add(item);
                //                //}
                //                if (lstPastDays.Contains(i))
                //                {
                //                    var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                    listLichProposing[i + startDay - 1] = d;
                //                }
                //                else
                //                {
                //                    var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                    listLichProposing[i + startDay - 1] = d;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (lstPastDays.Contains(i))
                //            {
                //                var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 1, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                listLichProposing[i + startDay - 1] = d;
                //            }
                //            else
                //            {
                //                var d = new lichlamviec() { id = i + startDay - 1, DayInCalendar = i, shiftSelected = _listShift.Count, statusClick = 1, statusPast = 0, listShiftSelectedAll = _listShift, ngayString = ngayString };
                //                listLichProposing[i + startDay - 1] = d;
                //            }
                //        }
                //    }
                //}
                listLichProposing = listLichProposing.ToList();
            }
            catch (System.Exception)
            {
            }
        }

        static List<int> GetAllSaturdaysInMonth(int month, int year)
        {
            List<int> saturdays = new List<int>();

            // Tìm ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Duyệt qua các ngày trong tháng
            for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
            {
                // Kiểm tra xem ngày đó có phải là thứ Bảy không
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    saturdays.Add(date.Day);
                }
            }

            return saturdays;
        }

        static List<int> GetAllSundaysInMonth(int month, int year)
        {
            List<int> sundays = new List<int>();

            // Tìm ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Duyệt qua các ngày trong tháng
            for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
            {
                // Kiểm tra xem ngày đó có phải là Chủ Nhật không
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    sundays.Add(date.Day);
                }
            }

            return sundays;
        }
        static List<int> GetAllPastDaysInMonth(int month, int year)
        {
            List<int> pastDays = new List<int>();

            // Tìm ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Lấy ngày hiện tại
            DateTime today = DateTime.Today;

            // Duyệt qua các ngày trong tháng
            for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
            {
                // Kiểm tra xem ngày đó có trước ngày hiện tại không
                if (date < today)
                {
                    pastDays.Add(date.Day);
                }
            }

            return pastDays;
        }
        private void ClickSelectTypeComfirm(object sender, SelectionChangedEventArgs e)
        {
            typeConfirm selectedStartToEnd = new typeConfirm();
            selectedStartToEnd = SelectCalendarWork.SelectedItemSelected as typeConfirm;
            LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, selectedStartToEnd, listShift);

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
            MonthCalendar.Text = StartDateOnLeave.SelectedDate.Value.ToString("MM-yyyy");
            LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, null, listShift);
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

        private void SelectShiftPlan(object sender, RoutedEventArgs e)
        {
           
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
                
                dataShiftSelected.isSeleced = dataShiftSelected.isSeleced == "True" ? "False" : "True";
                if (dataShiftSelected.isSeleced == "True")
                {
                    listShift.Add(dataShiftSelected);
                    LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, null, listShift);
                }
                else
                {
                    listShift.Remove(dataShiftSelected);
                    LoadDataCalendarWork(StartDateOnLeave.SelectedDate.Value.Month, StartDateOnLeave.SelectedDate.Value.Year, null, listShift);
                }
            }
        }

        private void CloseBox(object sender, MouseButtonEventArgs e)
        {
             borSelectedShift.Visibility = Visibility.Collapsed;
        }
    }
}
