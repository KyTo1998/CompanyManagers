using CompanyManagers.Common.Popups;
using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Models.ModelsShift;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for pageCreateNewProposing.xaml
    /// </summary>
    public partial class pageCreateNewProposing : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        PagePopupGrayColor pagePopupGrayColor { get; set; }
        public class typeConfirm
        {
            public int id_Confirm { get; set; }
            public string name_Confirm { get; set; }
        }
        List<typeConfirm> lstTypeConfirms = new List<typeConfirm>();

        private List<ListUsersDuyet> _dataListUserComfrim;
        public List<ListUsersDuyet> dataListUserComfrim
        {
            get { return _dataListUserComfrim; }
            set { _dataListUserComfrim = value; OnPropertyChanged("dataListUserComfrim"); }
        }
        private List<StaffShiftInDay> _dataListStaffShiftInDay;
        public List<StaffShiftInDay> dataListStaffShiftInDay
        {
            get { return _dataListStaffShiftInDay; }
            set { _dataListStaffShiftInDay = value; OnPropertyChanged("dataListStaffShiftInDay"); }
        }
        ManagerHome managerHome { set; get; }
        public pageCreateNewProposing(ManagerHome _managerHome, Result_CategoryProposing _dataCategoryProposing)
        {
            InitializeComponent();
            managerHome = _managerHome;
            tb_UserNameCreate.Text = _managerHome.UserCurrent.user_info.ep_name;
            tb_CategoryProposingCreate.Text = _dataCategoryProposing.name_cate_dx;
            lstTypeConfirms.Add(new typeConfirm() { id_Confirm = 1, name_Confirm = "Duyệt đồng thời" });
            lstTypeConfirms.Add(new typeConfirm() { id_Confirm = 2, name_Confirm = "Duyệt lần lượt" });
            SelectTypeComfirm.ItemsSourceSelected = lstTypeConfirms.ToList();
            SelectUserComfirm.ItemsSource = _managerHome.dataListUserComfrim.ToList();
            SelectUserFollow.ItemsSource = _managerHome.dataListUserFollow.ToList();
        }

        private void ClickSelectTypeComfirm(object sender, SelectionChangedEventArgs e)
        {
            
        } 
        private void ClickShiftOnLeave(object sender, SelectionChangedEventArgs e)
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
        private void DeleteUserConfirmAll(object sender, MouseButtonEventArgs e)
        {
            if (dataListUserComfrim.Count > 0)
            {
                dataListUserComfrim.Clear();
                btnDeleteUserConfirmAll.Visibility= Visibility.Collapsed;
                lsvUserComfirmSelected.Visibility = Visibility.Collapsed;
                SelectUserComfirm.PlaceHolder = "Chọn người duyệt";
                dataListUserComfrim = dataListUserComfrim.ToList();
            }
        }
        
        private void SelectedStartDateOnLeave(object sender, SelectionChangedEventArgs e)
        {
           EndDateOnLeave.IsEnabled = true;
           GetShiftForDay();
        }
        private void SelectedEndDateOnLeave(object sender, SelectionChangedEventArgs e)
        {

        }
        List<int> Days = new List<int>();
        public async void GetShiftForDay()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, UrlApi.apiListShiftForDay);
                if (Properties.Settings.Default.Type365 == "1")
                {
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.TokenCom);
                }
                else
                {
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.TokenEp);
                }
                var content = new MultipartFormDataContent();
                content.Add(new StringContent( StartDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd")), "day");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var returl = await response.Content.ReadAsStringAsync();
                    Root_StaffShiftInDay dataStaffShiftInDay = JsonConvert.DeserializeObject<Root_StaffShiftInDay>(returl);
                   if (dataStaffShiftInDay.list != null)
                    {
                        dataListStaffShiftInDay = dataStaffShiftInDay.list;
                        StaffShiftInDay dataShift = new StaffShiftInDay();
                        dataShift.shift_name = "Nghỉ cả ngày(tất cả các ca)";
                        dataShift.shift_id = 0;
                        dataListStaffShiftInDay.Insert(0, dataShift);
                        ShiftOnLeave.ItemsSourceSelected = dataListStaffShiftInDay.ToList();
                    }
                }



                //using (WebClient request = new WebClient())
                //{
                //    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.TokenEp);
                //    request.QueryString.Add("day",);
                //    request.UploadValuesCompleted += (s, e) =>
                //    {
                //        Root_StaffShiftInDay dataStaffShiftInDay = JsonConvert.DeserializeObject<Root_StaffShiftInDay>(UnicodeEncoding.UTF8.GetString(e.Result));
                //        if (dataStaffShiftInDay.list != null)
                //        {
                //            dataListStaffShiftInDay = dataStaffShiftInDay.list;
                //            StaffShiftInDay dataShift = new StaffShiftInDay();
                //            dataShift.shift_name = "Nghỉ cả ngày(tất cả các ca)";
                //            dataShift.shift_id = 0;
                //            dataListStaffShiftInDay.Insert(0, dataShift);
                //            ShiftOnLeave.ItemsSourceSelected = dataListStaffShiftInDay.ToList();
                //        }
                //    };
                //    await request.UploadValuesTaskAsync(UrlApi.apiListShiftForDay, request.QueryString);
                //}
            }
            catch (Exception)
            {
            }
        }
        private void SelectionChangeUserFollow(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DeleteFileGim(object sender, MouseButtonEventArgs e)
        {

        }

        private void ClosePopupCreateProposing(object sender, MouseButtonEventArgs e)
        {
            pagePopupGrayColor = new PagePopupGrayColor(managerHome);
            pagePopupGrayColor.Popup.NavigationService.Navigate(null);
            managerHome.PagePopup.NavigationService.Navigate(null);
        }
    }
}
