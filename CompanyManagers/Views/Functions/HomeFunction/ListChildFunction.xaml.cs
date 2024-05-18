using CompanyManagers.Models.HomeFunction;
using CompanyManagers.Views.Home;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace CompanyManagers.Views.Functions.HomeFunction
{
    /// <summary>
    /// Interaction logic for ListChildFunction.xaml
    /// </summary>
    public partial class ListChildFunction : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private List<DataChildFunction> _dataChildFunction;
        public List<DataChildFunction> dataChildFunction 
        {
            get { return _dataChildFunction; }
            set { _dataChildFunction = value; OnPropertyChanged("dataChildFunction");}
        }
        DataFunction dataFunction;
        ManagerHome ManagerHome { get; set; }
        public ListChildFunction(ManagerHome managerHome, DataFunction _dataFunction)
        {
            InitializeComponent();
            this.ManagerHome = managerHome;
            this.dataFunction = _dataFunction;
            //tb_TitleFunction.Text = _dataFunction.nameFunction;
            //dataChildFunction = _dataFunction.dataChildFunction;
        }
    }
}
