using CompanyManagers.Models.HomeFunction;
using CompanyManagers.Views.Home;
using CompanyManagers.Views.PageStaff.Proposing;
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
        private List<DataFunction> _dataListFunction;
        public List<DataFunction> dataListFunction 
        {
            get { return _dataListFunction; }
            set { _dataListFunction = value; OnPropertyChanged("dataListFunction");}
        }
        private string _type365;
        public string type365
        {
            get { return _type365; }
            set { _type365 = value; OnPropertyChanged("type365");}
        }
        ManagerHome ManagerHome { get; set; }
        private List<DataChildFunction> _dataListChildFunction;
        public List<DataChildFunction> dataListChildFunction
        {
            get { return _dataListChildFunction; }
            set { _dataListChildFunction = value; OnPropertyChanged("dataListChildFunction");}
        }
        public ListFunction(ManagerHome managerHome)
        {
            InitializeComponent();
            ManagerHome = managerHome;
            dataListFunction = managerHome.dataFunctionHome.ToList();
            type365 = Properties.Settings.Default.Type365;
            dataListChildFunction = managerHome.dataFunctionHome.Find(x =>x.idFunction == 1).dataChildFunction;
            tb_TitleFunction.Text = managerHome.dataFunctionHome.Find(x =>x.idFunction == 1).nameFunction;
            managerHome.dataFunctionHome.Find(x => x.idFunction == 1).statusClickFunction = true;
        }

        private void ShowListManager(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataFunction dataFunction = (DataFunction)(sender as Grid).DataContext;
                dataFunction.statusClickFunction = false;
                if (dataFunction != null)
                {
                    dataListChildFunction = dataFunction.dataChildFunction;
                    tb_TitleFunction.Text = dataFunction.nameFunction;
                    foreach (var item in dataListFunction)
                    {
                        if (dataFunction.idFunction == item.idFunction)
                        {
                            item.statusClickFunction = true;
                        }
                        else
                        {
                            item.statusClickFunction = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ShowLisChildManager(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataChildFunction dataChildFunction = (DataChildFunction)(sender as Grid).DataContext;
                if (dataChildFunction != null)
                {
                    if (Properties.Settings.Default.Type365 == "2")
                    {
                        if (dataChildFunction.nameChildFunction == "Loại đề xuất" || dataChildFunction.nameChildFunction == "Proposal Type")
                        {
                            ProposingHome proposingHome = new ProposingHome(ManagerHome);
                            ManagerHome.PageFunction.Content = proposingHome;
                            ManagerHome.backToBack = "BackToManagerHome";
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
