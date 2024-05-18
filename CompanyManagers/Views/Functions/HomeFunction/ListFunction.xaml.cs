using CompanyManagers.Models.HomeFunction;
using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;


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
        private string _type365;
        public string type365
        {
            get { return _type365; }
            set { _type365 = value; OnPropertyChanged("type365");}
        }
        ManagerHome ManagerHome { get; set; }
         private List<DataChildFunction> _dataChildFunction;
        public List<DataChildFunction> dataChildFunction 
        {
            get { return _dataChildFunction; }
            set { _dataChildFunction = value; OnPropertyChanged("dataChildFunction");}
        }
        public ListFunction(ManagerHome managerHome)
        {
            InitializeComponent();
            ManagerHome = managerHome;
            dataFunction = managerHome.dataFunctionHome.ToList();
            type365 = Properties.Settings.Default.Type365;
            dataChildFunction = managerHome.dataFunctionHome.Find(x =>x.idFunction == 1).dataChildFunction;
            tb_TitleFunction.Text = managerHome.dataFunctionHome.Find(x =>x.idFunction == 1).nameFunction;
            /*ListChildFunction lstChildFunction = new ListChildFunction(ManagerHome,);
            ManagerHome.PageChildFunction.Content = lstChildFunction;*/
        }

        private void ShowListManager(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataFunction dataFunction = (DataFunction)(sender as Grid).DataContext;
                if (dataFunction != null)
                {
                    dataChildFunction = dataFunction.dataChildFunction;
                     tb_TitleFunction.Text = dataFunction.nameFunction;
                   /* ListChildFunction lstChildFunction = new ListChildFunction(ManagerHome,dataFunction);
                    ManagerHome.PageChildFunction.Content = lstChildFunction;*/
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
