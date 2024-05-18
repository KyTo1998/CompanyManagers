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
        ManagerHome ManagerHome { get; set; }
        public ListFunction(ManagerHome managerHome)
        {
            InitializeComponent();
            ManagerHome = managerHome;
            dataFunction = managerHome.dataFunctionHome.ToList();
            ListChildFunction lstChildFunction = new ListChildFunction(ManagerHome,managerHome.dataFunctionHome.Find(x =>x.idFunction == 1));
            ManagerHome.PageChildFunction.Content = lstChildFunction;
        }

        private void ShowListManager(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataFunction dataFunction = (DataFunction)(sender as Grid).DataContext;
                if (dataFunction != null)
                {
                    ListChildFunction lstChildFunction = new ListChildFunction(ManagerHome,dataFunction);
                    ManagerHome.PageChildFunction.Content = lstChildFunction;
                }
            }
            catch (Exception)
            {
            }
            

        }
    }
}
