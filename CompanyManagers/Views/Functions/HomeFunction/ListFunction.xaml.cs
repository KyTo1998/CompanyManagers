using CompanyManagers.Models.HomeFunction;
using CompanyManagers.Models.Logins;
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

namespace CompanyManagers.Views.Functions.HomeFunction
{
    /// <summary>
    /// Interaction logic for ListFunction.xaml
    /// </summary>
    public partial class ListFunction : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private List<DataFunction> _dataFunction;
        public List<DataFunction> dataFunction 
        {
            get { return _dataFunction; }
            set { _dataFunction = value; OnPropertyChanged("dataFunction");}
        }
        ListChildFunction pageChildFunction { get; set; }
        ManagerHome ManagerHome { get; set; }
        public ListFunction( ManagerHome managerHome)
        {
            InitializeComponent();
            ManagerHome = managerHome;
            dataFunction = new List<DataFunction>();
            if (Properties.Settings.Default.Type365 == "1")
            {
                dataFunction.Add(new DataFunction() { idFunction = 1, nameFunction = "Quản lý công ty", colorFunction1 = "#FFA13B", colorFunction2 = "#E8811A"});
                dataFunction.Add(new DataFunction() { idFunction = 2, nameFunction = "Chấm công", colorFunction1 = "#97C25F", colorFunction2 = "#7DA047"});
                dataFunction.Add(new DataFunction() { idFunction = 3, nameFunction = "Cài đặt lương", colorFunction1 = "#8069FF", colorFunction2 = "#5E53C9"});
                dataFunction.Add(new DataFunction() { idFunction = 4, nameFunction = "Cài đặt đề xuất",colorFunction1 = "#FF5B4D", colorFunction2 = "#C1403A"});
            }
            else
            {
                dataFunction.Add(new DataFunction() { idFunction = 1, nameFunction = "Tạo đề xuất", colorFunction1 = "#FFA13B", colorFunction2 = "#E8811A"});
                dataFunction.Add(new DataFunction() { idFunction = 2, nameFunction = "Lịch sử", colorFunction1 = "#97C25F", colorFunction2 = "#7DA047"});
                dataFunction.Add(new DataFunction() { idFunction = 3, nameFunction = "Tính lương", colorFunction1 = "#FF5B4D", colorFunction2 = "#C1403A"});
            }
            dataFunction = dataFunction.ToList();
            DataFunction dataFunc = new DataFunction();
            ListChildFunction lstChildFunction = new ListChildFunction(this, null, null);
            ManagerHome.PageChildFunction.Content = lstChildFunction;
        }

        private void ShowListManager(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataFunction dataFunction = (DataFunction)(sender as Grid).DataContext;
                if (dataFunction != null)
                {
                    List<DataChildFunction> dataChildFunction = new List<DataChildFunction>();
                    if (Properties.Settings.Default.Type365 == "1")
                    {
                        switch (dataFunction.idFunction)
                        {
                            case 1:
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Thiết lập cơ cấu tổ chức"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Thiết lập vị trí"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 3, nameChildFunction = "Quản lý nhân viên"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 4, nameChildFunction = "Danh sách ứng viên"});
                                break;
                            case 2:
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Cấu hình chấm công"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Quản lý ca làm việc"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 3, nameChildFunction = "Quản lý Lịch làm việc"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 4, nameChildFunction = "Xuất công"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 5, nameChildFunction = "Lịch sử điểm danh"});
                                break;
                            case 3:
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Cài đặt lương cơ bản"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Cài đặt đi muộn - về sớm"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 3, nameChildFunction = "Cài đặt nghỉ phép"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 4, nameChildFunction = "Cài đặt bảo hiểm"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 5, nameChildFunction = "Cài đặt phụ cấp"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 6, nameChildFunction = "Cài đặt thuế"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 7, nameChildFunction = "Cộng công"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 8, nameChildFunction = "Thưởng phạt"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 9, nameChildFunction = "Hoa hồng"});
                                break;
                            case 4:
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Cài đặt đề xuất"});
                                break;
                        }
                    }
                    else
                    {
                         switch (dataFunction.idFunction)
                        {
                            case 1:
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Tạo đề xuất"});
                                break;
                            case 2:
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Chấm công"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Lương hiện tại"});
                                break;
                            case 3:
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Xem bảng công"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Xem bảng lương"});
                                dataChildFunction.Add(new DataChildFunction() { idChildFunction = 3, nameChildFunction = "Xem lịch làm việc"});
                                break;
                        }
                    }
                    ListChildFunction lstChildFunction = new ListChildFunction(this,dataFunction, dataChildFunction);
                    ManagerHome.PageChildFunction.Content = lstChildFunction;
                }
            }
            catch (Exception)
            {
            }
            

        }
    }
}
