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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static CompanyManagers.Common.Tool.DatePicker;
using static CompanyManagers.Views.Home.ManagerHome;


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

        public class JsonOnLeave
        {
            public string nameShif {  get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public List<object> nghi_phep { get; set; }
        }
        public class lstJsonOnLeave
        {
            public List<List<object>> nghi_phep { get; set; } = new List<List<object>>();
        }
        private List<JsonOnLeave> _listShiftSelect;
        public List<JsonOnLeave> listShiftSelect
        {
            get { return _listShiftSelect; }
            set { _listShiftSelect = value; OnPropertyChanged("listShiftSelect"); }
        }
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

        private int _typeCategoryProposing;
        public int typeCategoryProposing
        {
            get { return _typeCategoryProposing; }
            set { _typeCategoryProposing = value;OnPropertyChanged("typeCategoryProposing");}
        }
        ManagerHome managerHome { set; get; }
        int typePlan {  get; set; }
        bool statusValidate {  get; set; }
        Result_CategoryProposing dataCategoryProposing;
        public pageCreateNewProposing(ManagerHome _managerHome, Result_CategoryProposing _dataCategoryProposing)
        {
            InitializeComponent();
            managerHome = _managerHome;
            dataCategoryProposing = _dataCategoryProposing;
            typeCategoryProposing = _dataCategoryProposing.cate_dx;
            tb_UserNameCreate.Text = _managerHome.UserCurrent.user_info.ep_name;
            tb_CategoryProposingCreate.Text = _dataCategoryProposing.name_cate_dx;
            lstTypeConfirms.Add(new typeConfirm() { id_Confirm = 0, name_Confirm = "Duyệt đồng thời" });
            lstTypeConfirms.Add(new typeConfirm() { id_Confirm = 1, name_Confirm = "Duyệt lần lượt" });
            SelectTypeComfirm.ItemsSourceSelected = lstTypeConfirms.ToList();
            SelectUserComfirm.ItemsSource = _managerHome.dataListUserComfrim.ToList();
            SelectUserFollow.ItemsSource = _managerHome.dataListUserFollow.ToList();
        }
        #region Nghỉ Phép
        public async void CreateProposingOnLeave()
        {
            try
            {
                if (cbSelectYesPlan.IsChecked == false && cbSelectNoPlan.IsChecked == false)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Chưa chọn loại đề xuất";
                    statusValidate = false;
                }
                else
                {
                    statusValidate = true;
                }
                if (StartDateOnLeave.SelectedDate == null || EndDateOnLeave.SelectedDate == null)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Chưa chọn ngày bắt đầu hoặc kết thúc";
                    statusValidate = false;
                }
                else
                {
                    statusValidate = true;
                }
                if (ShiftOnLeave.SelectedIndexSelected < 0)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Chưa ca nghỉ";
                    statusValidate = false;
                }
                else
                {
                    statusValidate = true;
                }
                if (statusValidate)
                {
                    var data = new lstJsonOnLeave();
                    foreach (var item in listShiftSelect)
                    {
                        data.nghi_phep.Add(item.nghi_phep);
                    }
                    string jsonString = JsonConvert.SerializeObject(data);

                    List<string> listUserConfirm = new List<string>();
                    foreach (var item in dataListUserComfrim)
                    {
                        listUserConfirm.Add(item.idQLC.ToString());
                    }
                    string userConfirm = String.Join(",", listUserConfirm);
                    var client = HttpClientSingleton.Instance;
                    var request = new HttpRequestMessage(HttpMethod.Post, UrlApi.apiCreateProposingOnLeave);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(""), "fileKem");
                    content.Add(new StringContent(jsonString), "noi_dung");
                    content.Add(new StringContent(tb_InputNameProposing.Text), "name_dx");
                    content.Add(new StringContent(((typeConfirm)SelectTypeComfirm.SelectedItemSelected).id_Confirm.ToString()), "kieu_duyet");
                    content.Add(new StringContent(typePlan.ToString()), "loai_np");
                    content.Add(new StringContent(tb_InputReasonCreateProposing.Text), "ly_do");
                    content.Add(new StringContent(userConfirm), "id_user_duyet");
                    content.Add(new StringContent(((ListUsersTheoDoi)SelectUserFollow.SelectedItem).idQLC.ToString()), "id_user_theo_doi");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        dataListUserComfrim.Clear();
                        listShiftSelect.Clear();
                        managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất xin nghỉ phép thành công", ""));
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
        private void ClickSelectTypeComfirm(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void ClickShiftOnLeave(object sender, SelectionChangedEventArgs e)
        {
            StaffShiftInDay dataShift = (StaffShiftInDay)ShiftOnLeave.SelectedItemSelected;
            if (dataShift != null)
            {
                JsonOnLeave OnLeave = new JsonOnLeave();
                OnLeave.nameShif = dataShift.shift_name;
                OnLeave.startDate = StartDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd");
                OnLeave.endDate = EndDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd");
                OnLeave.nghi_phep = new List<object>{ $"{StartDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd")}", $"{EndDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd")}", dataShift.shift_id }; 
                if (listShiftSelect == null) listShiftSelect = new List<JsonOnLeave>();
                listShiftSelect.Add(OnLeave);
                listShiftSelect = listShiftSelect.ToList();
                if (listShiftSelect.Count > 0)
                {
                    lsvListShifForDay.Visibility = Visibility.Visible;
                } 
            }
        }
        private void SelectYesPlan(object sender, RoutedEventArgs e)
        {
            cbSelectNoPlan.IsChecked = false;
            typePlan = 1;
        }

        private void SelectNoPlan(object sender, RoutedEventArgs e)
        {
            cbSelectYesPlan.IsChecked= false;
            typePlan = 2;
        }

        private void DeleteShiftOnLeave(object sender, MouseButtonEventArgs e)
        {
            JsonOnLeave dataShiftOnLeave = (JsonOnLeave)(sender as Border).DataContext;
            if (dataShiftOnLeave != null)
            {
                listShiftSelect.Remove(dataShiftOnLeave);
                listShiftSelect = listShiftSelect.ToList();
                if (listShiftSelect.Count == 0)
                {
                    lsvListShifForDay.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void LoadNumRowShifForDay(object sender, DataGridRowEventArgs e)
        {
             e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void SelectedStartDateOnLeave(object sender, SelectionChangedEventArgs e)
        {
            EndDateOnLeave.IsEnabled = true;
            managerHome.GetShiftForDay(StartDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd"), managerHome.UserCurrent.user_info.ep_id.ToString());
        }
        private void SelectedEndDateOnLeave(object sender, SelectionChangedEventArgs e)
        {
            ShiftOnLeave.ItemsSourceSelected = managerHome.dataListStaffShiftInDay;
        }
        #endregion
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

        private void ClickCancel(object sender, MouseButtonEventArgs e)
        {
            pagePopupGrayColor = new PagePopupGrayColor(managerHome);
            pagePopupGrayColor.Popup.NavigationService.Navigate(null);
            managerHome.PagePopup.NavigationService.Navigate(null);
        }
        private void CheckValidateProposing()
        {
            try
            {
                if (string.IsNullOrEmpty(tb_InputNameProposing.Text))
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Tên đề xuất không được để trống";
                    statusValidate = false;
                }
                else if (SelectTypeComfirm.SelectedIndexSelected < 0)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Chưa chọn kiểu duyệt";
                    statusValidate = false;
                }
                else if (string.IsNullOrEmpty(tb_InputReasonCreateProposing.Text))
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Lý do không được để trống";
                    statusValidate = false;
                }
                else if (SelectUserFollow.SelectedIndex < 0)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Chưa chọn người theo dõi";
                    statusValidate = false;
                }
                else if (dataListUserComfrim == null)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    tb_ValidateProposing.Text = "Chưa chọn người xét duyệt";
                    statusValidate = false;
                }
                else
                {
                    statusValidate = true;
                }

            }
            catch (System.Exception)
            {
            }
        }

        private void ClickCreateProposing(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CheckValidateProposing();
                if (statusValidate)
                {
                    switch (dataCategoryProposing.cate_dx)
                    {
                        case 1:
                            CreateProposingOnLeave();
                            break;
                    }

                }
            }
            catch (System.Exception)
            {
            }
        }

        private void ScollCreateProposing(object sender, MouseWheelEventArgs e)
        {
            scoll.ScrollToVerticalOffset(scoll.VerticalOffset - e.Delta);
        }
    }
}
