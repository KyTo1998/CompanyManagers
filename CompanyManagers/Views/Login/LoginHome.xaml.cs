using CompanyManagers.Controllers;
using CompanyManagers.Models.Logins;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
        Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
        Regex regexPhone = new Regex(@"^((09|03|07|08|05|02)+([0-9]{8})\b)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
        Regex regexPhone1 = new Regex(@"^((09|03|07|08|05|02)+([0-9]{9})\b)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
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
            checkRememberPass(Properties.Settings.Default.RememberMe);
            if (Properties.Settings.Default.RememberMe)
            {
                txtEmail.Text = Properties.Settings.Default.User;
                txtPasswordHide.Password = Properties.Settings.Default.Pass;
            }
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

        public void LoginStart(int typeLogin, string userName, string passWord)
        {
            using(WebClient client = new WebClient())
            {
                client.QueryString.Add("account", userName);
                client.QueryString.Add("password", passWord);
                client.QueryString.Add("type", typeLogin.ToString());
                client.UploadValuesCompleted += (sender, e) => 
                { 
                    RootLogin dataLogin = JsonConvert.DeserializeObject<RootLogin>(UnicodeEncoding.UTF8.GetString(e.Result));
                    if (dataLogin.data.data != null)
                    {
                        if (Properties.Settings.Default.RememberMe)
                        {
                            Properties.Settings.Default.User = userName;
                            Properties.Settings.Default.Pass = pass;
                            Properties.Settings.Default.Type365 = typeLogin.ToString();
                            Properties.Settings.Default.RememberMe = true;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.User = "";
                            Properties.Settings.Default.Pass = "";
                            Properties.Settings.Default.RememberMe = false;
                            Properties.Settings.Default.Save();
                        }
                    }
                };
                client.UploadValuesTaskAsync(UrlApi.apiLogin, "POST", client.QueryString);
            }
        }

        private void LoginEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtEmail.Text.Length != 0 && (regex.IsMatch(txtEmail.Text) || regexPhone.IsMatch(txtEmail.Text) || regexPhone1.IsMatch(txtEmail.Text)))
                {
                    btLogin.IsEnabled = false;
                    btLogin.Cursor = Cursors.Wait;
                    btLogin.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FF4C5BD4");
                    LoginStart(TypeLogin, txtEmail.Text, txtPasswordHide.Password);
                }
                else if ((pass.Length == 0))
                {
                    boderPassword.BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFF3333");
                    lbWrongPassword.Visibility = Visibility.Visible;
                    lbWrongPassword.Text = "Mật khẩu không được để trống";
                }
                else
                {

                    boderEmail.BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFF3333");
                    lbWrongEmail.Visibility = Visibility.Visible;
                    lbWrongEmail.Text = "Định dạng email không chính xác";
                }
            }
            
        }
        private void LoginClick(object sender, MouseButtonEventArgs e)
        {
            LoginStart(TypeLogin, txtEmail.Text, txtPasswordHide.Password);
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
            if (Properties.Settings.Default.RememberMe == false)
            {
                Properties.Settings.Default.RememberMe = true;
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
            }
            checkRememberPass(Properties.Settings.Default.RememberMe);
        }
        private void checkRememberPass(bool rememberPassword)
        {
            Properties.Settings.Default.RememberMe = rememberPassword;
            if (rememberPassword)
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

       

        private void LinkGuide_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RegisterClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion
    }
}
