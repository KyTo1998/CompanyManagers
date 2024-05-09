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
using System.Windows.Shapes;

namespace CompanyManagers.Views.Login
{
    /// <summary>
    /// Interaction logic for LoginHome.xaml
    /// </summary>
    public partial class LoginHome : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        public static int TypeLogin { get; set; }
        public bool RememberPassword { get; set; }

        string pass;
        public string Pass
        {
            get => pass;
            set
            {
                pass = value;
                OnPropertyChanged("Pass");
            }
        }
        public LoginHome()
        {
            InitializeComponent();
            checkRememberPass(true);
            Pass = "";
        }

        private void openPagelogin(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Border).Name.Equals("btnCompany"))
            {
                TypeLogin = 1;
            }
            else if ((sender as Border).Name.Equals("btnEmployee"))
            {
                TypeLogin = 2;
            }
            stpSelectLogin.Visibility = Visibility.Collapsed;
            grFormLogin.Visibility = Visibility.Visible;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Minimimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Maximimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void CloseWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void returnSelectLogin(object sender, MouseButtonEventArgs e)
        {
            stpSelectLogin.Visibility = Visibility.Visible;
            grFormLogin.Visibility = Visibility.Collapsed;
        }
        #region TextBoxLogin
        private void txtPasswordHide_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lbWrongPassword.Visibility = Visibility.Collapsed;
            Pass = txtPasswordHide.Password;
        }
        private void txtPasswordShow_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbWrongPassword.Visibility = Visibility.Collapsed;
            Pass = txtPasswordHide.Password;
        }
        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbWrongEmail.Visibility = Visibility.Collapsed;
            boderEmail.BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFB1A9A9");
        }

        private void LoginEnter(object sender, KeyEventArgs e)
        {

        }

        private void hideShowPassword(object sender, MouseButtonEventArgs e)
        {
            if (txtPasswordHide.Visibility == Visibility.Collapsed)
            {
                HiddenPassword.Visibility = Visibility.Visible;
                showPassword.Visibility = Visibility.Collapsed;
                txtPasswordHide.Visibility = Visibility.Visible;
                txtPasswordShow.Visibility = Visibility.Collapsed;
                txtPasswordHide.Password = Pass;
                txtPasswordHide.Focus();
            }
            else
            {
                HiddenPassword.Visibility = Visibility.Collapsed;
                showPassword.Visibility = Visibility.Visible;
                txtPasswordHide.Visibility = Visibility.Collapsed;
                txtPasswordShow.Visibility = Visibility.Visible;
                txtPasswordShow.Text = Pass;
                txtPasswordShow.Focus();
                txtPasswordShow.CaretIndex = txtPasswordShow.Text.Length;
            }
        }

        private void ChangeRememberPassword(object sender, MouseButtonEventArgs e)
        {
            checkRememberPass(RememberPassword);
        }
        private void checkRememberPass(bool flag)
        {
            RememberPassword = flag;
            if (flag)
            {
                checkBoxFalse.Visibility = Visibility.Hidden;
                checkBoxTrue.Visibility = Visibility.Visible;
            }
            else
            {
                checkBoxFalse.Visibility = Visibility.Visible;
                checkBoxTrue.Visibility = Visibility.Hidden;
            }
        }
        private void forgetPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void LoginClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void LinkGuide_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RegisterClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion
    }
}
