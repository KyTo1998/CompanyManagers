﻿using CompanyManagers.Models.Logins;
using CompanyManagers.Views.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace CompanyManagers.Views.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagerHome : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private int _IsFull = 0;
        public int IsFull
        {
            get { return _IsFull; }
            set
            {
                _IsFull = value;
                var workingArea = System.Windows.SystemParameters.WorkArea;
                switch (IsFull)
                {
                    case 0:
                        /*gridMain.Margin = new Thickness(10);*/
                        this.WindowState = WindowState.Normal;
                        Width = oldWidth;
                        Height = oldHeight;
                        Left = oldLeft;
                        Top = oldTop;
                        this.ResizeMode = ResizeMode.CanResize;
                        NormalSize.Visibility = Visibility.Collapsed;
                        Maximimize.Visibility = Visibility.Visible;
                        logoInTitle.Margin = new Thickness(0, 0, 0, 0);
                        pageTitle.Height = 40;
                        /*pageDisplay.Margin = new Thickness(0, 40, 0, 0);
                        pageLoading.Margin = new Thickness(0, 40, 0, 0);*/
                        break;
                    case 1:
                        oldWidth = this.ActualWidth;
                        oldHeight = this.ActualHeight;
                        oldTop = this.Top;
                        oldLeft = this.Left;
                        /*gridMain.Margin = new Thickness(0);*/
                        this.WindowState = WindowState.Normal;
                        Left = workingArea.TopLeft.X;
                        Top = workingArea.TopLeft.Y;
                        Width = workingArea.Width;
                        Height = workingArea.Height;
                        this.ResizeMode = ResizeMode.CanMinimize;
                        NormalSize.Visibility = Visibility.Visible;
                        Maximimize.Visibility = Visibility.Collapsed;
                        pageTitle.Height = 40;
                        logoInTitle.Margin = new Thickness(0, 0, 0, 0);
                        /*pageDisplay.Margin = new Thickness(0, 40, 0, 0);
                        pageLoading.Margin = new Thickness(0, 40, 0, 0);*/
                        break;
                    case 2:
                        oldWidth = workingArea.Right - 440;
                        oldHeight = workingArea.Bottom - 180;
                        oldLeft = (workingArea.Right / 2) - (this.oldWidth / 2);
                        oldTop = (workingArea.Bottom / 2) - (this.oldHeight / 2);
                        /*gridMain.Margin = new Thickness(0);*/
                        this.WindowState = WindowState.Normal;
                        Left = workingArea.TopLeft.X;
                        Top = workingArea.TopLeft.Y;
                        Width = workingArea.Width;
                        Height = workingArea.Height;
                        this.ResizeMode = ResizeMode.NoResize;
                        NormalSize.Visibility = Visibility.Visible;
                        Maximimize.Visibility = Visibility.Collapsed;
                        pageTitle.Height = 40;
                        logoInTitle.Margin = new Thickness(0, 0, 0, 0);
                        /*pageDisplay.Margin = new Thickness(0, 40, 0, 0);
                        pageLoading.Margin = new Thickness(0, 40, 0, 0);*/
                        break;
                    default:
                        break;
                }
                OnPropertyChanged("IsFull");
            }
        }

        double oldWidth = 824;
        double oldHeight = 690;
        double oldTop = 690;
        double oldLeft = 690;

        private DataUserLogin _UserCurrent;
        public DataUserLogin UserCurrent 
        {
            get { return _UserCurrent; }
            set { _UserCurrent = value; OnPropertyChanged("UserCurrent");}
        }

        LoginHome loginHome { get; set; }
        public ManagerHome(DataUserLogin userCurrent, LoginHome _loginHome)
        {
            InitializeComponent();
            this.DataContext = this;
            UserCurrent = userCurrent;
            loginHome = _loginHome;
            switch (userCurrent.type)
            {
                case 1:
                    tb_NameUser.Text = userCurrent.user_info.com_name;
                    tb_IdUser.Text = userCurrent.user_info.com_id.ToString();
                    break;
                case 2:
                    tb_NameUser.Text = userCurrent.user_info.ep_name;
                    tb_IdUser.Text = userCurrent.user_info.ep_id.ToString();
                    break;
            }
            
            
        }

        private void pageTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Minimimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void NormalSize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsFull = 0;
        }

        private void Maximimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsFull = 1;
        }

        private void CloseWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            this.Close();
        }

        private void ShowDetailAcount(object sender, MouseButtonEventArgs e)
        {
            if (bor_DetailAcount.Visibility == Visibility.Collapsed)
            {
                bor_DetailAcount.Visibility = Visibility.Visible;
            }
            else 
            {
                bor_DetailAcount.Visibility = Visibility.Collapsed;
            }
        }
    }
}
