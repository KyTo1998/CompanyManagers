using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private void ClickSelectTypeComfirm(object sender, SelectionChangedEventArgs e)
        {
            
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

        private void SelectYesPlan(object sender, RoutedEventArgs e)
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

        private void CloseBox(object sender, MouseButtonEventArgs e)
        {
             borSelectedShift.Visibility = Visibility.Collapsed;
        }
       
    }
}
