using CompanyManagers.Controllers;
using CompanyManagers.Models.Logins;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
        public static string TypeLogin { get; set; }
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
        ManagerHome ManagerHome { get; set; }
        public LoginHome()
        {
            InitializeComponent();
            this.DataContext = this;
            checkRememberPass(Properties.Settings.Default.RememberMe);
            Pass = "";
        }
        public LoginHome(string statusLogout, string userName)
        {
            InitializeComponent();
            this.DataContext = this;
            checkRememberPass(Properties.Settings.Default.RememberMe);
            Pass = "";
            this.Show();
            txtEmail.Text = userName;
            stpSelectLogin.Visibility = Visibility.Collapsed;
            grFormLogin.Visibility = Visibility.Visible;
            if (Properties.Settings.Default.Type365 == "1")
                tb_TitelLogin.Text = "Tài khoản công ty";
            else tb_TitelLogin.Text = "Tài khoản nhân viên";
            
        }
        private void openPagelogin(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Border).Name.Equals("btnCompany"))
            {
                TypeLogin = "1";
                tb_TitelLogin.Text = "Tài khoản công ty";
                if (Properties.Settings.Default.RememberMe)
                {
                    txtEmail.Text = Properties.Settings.Default.UserCom;
                    txtPasswordHide.Password = Properties.Settings.Default.PassCom;
                }
                else
                {
                    txtEmail.Text = "";
                    txtPasswordHide.Password = "";
                }
            }
            else if ((sender as Border).Name.Equals("btnEmployee"))
            {
                TypeLogin = "2";
                tb_TitelLogin.Text = "Tài khoản nhân viên";
                if (Properties.Settings.Default.RememberMe)
                {
                    txtEmail.Text = Properties.Settings.Default.UserEp;
                    txtPasswordHide.Password = Properties.Settings.Default.PassEp;
                }
                else
                {
                    txtEmail.Text = "";
                    txtPasswordHide.Password = "";
                }
            }
            stpSelectLogin.Visibility = Visibility.Collapsed;
            grFormLogin.Visibility = Visibility.Visible;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Minimimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Maximimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void CloseWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            this.Close();
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

        public async  void LoginStart(string typeLogin, string userName, string passWord)
        {
            try
            {
                var options = new RestClientOptions("https://api.timviec365.vn")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/qlc/employee/login", Method.Post);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("account", userName);
                request.AddParameter("password", passWord);
                request.AddParameter("type", typeLogin);
                RestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    RootLogin dataLogin = JsonConvert.DeserializeObject<RootLogin>(response.Content);
                    if (dataLogin.data.data.type.ToString() != TypeLogin)
                    {
                        if (dataLogin.data.data.type == 1)
                        {
                            lbWrongUserPass.Visibility = Visibility.Visible;
                            lbWrongUserPass.Text = "Tài khoản đang đăng nhập là tài khoản công ty!";
                        }
                        else
                        {
                            lbWrongUserPass.Visibility = Visibility.Visible;
                            lbWrongUserPass.Text = "Tài khoản đang đăng nhập là tài khoản nhân viên!";
                        }
                    }
                    else
                    {
                        if (dataLogin.data != null)
                        {
                            this.Hide();
                            CheckTypeSaveAcount(typeLogin, userName, passWord, dataLogin.data.data.access_token);
                            ManagerHome = new ManagerHome(dataLogin.data.data, this);
                            try
                            {
                                await Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                                {
                                    var workingArea = System.Windows.SystemParameters.WorkArea;
                                    ManagerHome.Width = workingArea.Right - 300;
                                    ManagerHome.Height = workingArea.Bottom - 100;
                                    ManagerHome.Show();
                                }));
                            }
                            catch
                            {
                                ManagerHome.Show();
                            }
                        }
                    } 
                }
                else
                {
                    lbWrongUserPass.Visibility = Visibility.Visible;
                    lbWrongUserPass.Text = "Tài khoản hoặc mật khẩu không chính xác!";
                }
            }
            catch (Exception)
            {
                lbWrongUserPass.Visibility = Visibility.Visible;
                lbWrongUserPass.Text = "Lỗi hệ thống vui lòng thử lại sau!";
            }
        }
        public void CheckTypeSaveAcount(string typeLogin, string userName, string passWord, string token)
        {
            try
            {
                switch (typeLogin)
                    {
                    case "1":
                        if (Properties.Settings.Default.RememberMe)
                        {
                            Properties.Settings.Default.UserCom = userName;
                            Properties.Settings.Default.PassCom = pass;
                            Properties.Settings.Default.Type365 = typeLogin.ToString();
                            Properties.Settings.Default.Token = token;
                            Properties.Settings.Default.RememberMe = true;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.UserCom = "";
                            Properties.Settings.Default.PassCom = "";
                            Properties.Settings.Default.RememberMe = false;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    case "2":
                        if (Properties.Settings.Default.RememberMe)
                        {
                            Properties.Settings.Default.UserEp = userName;
                            Properties.Settings.Default.PassEp = pass;
                            Properties.Settings.Default.Type365 = typeLogin.ToString();
                            Properties.Settings.Default.Token = token;
                            Properties.Settings.Default.RememberMe = true;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.UserEp = "";
                            Properties.Settings.Default.PassEp = "";
                            Properties.Settings.Default.RememberMe = false;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
            }
            catch (Exception)
            {
            }
        }
        public void CheckLogin()
        {
            try
            {
                if (txtEmail.Text.Length != 0 && (regex.IsMatch(txtEmail.Text) || regexPhone.IsMatch(txtEmail.Text) || regexPhone1.IsMatch(txtEmail.Text)))
                {
                    btLogin.Cursor = Cursors.Hand;
                    LoginStart(TypeLogin, txtEmail.Text, txtPasswordHide.Password);
                }
                else if ((pass.Length == 0))
                {
                    boderPassword.BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFF3333");
                    lbWrongPassword.Visibility = Visibility.Visible;
                    lbWrongPassword.Text = "Mật khẩu không được để trống!";

                }
                else
                {
                    boderEmail.BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFF3333");
                    lbWrongEmail.Visibility = Visibility.Visible;
                    lbWrongEmail.Text = "Sai định dạng tài khoản!";
                }
            }
            catch (Exception)
            {

            }
        }
        private void LoginEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckLogin();
            }
        }
        private void LoginClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CheckLogin();
            }
            catch (Exception)
            {
            }
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
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
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
