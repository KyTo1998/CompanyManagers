using CompanyManagers.Views.Login;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace CompanyManagers.Views.Register
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterAccount : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private string pass;
        public string Pass
        {
            get => pass;
            set
            {
                pass = value;
                OnPropertyChanged("Pass");
            }
        }

        private string rePass;
        public string RePass
        {
            get => rePass;
            set
            {
                rePass = value;
                OnPropertyChanged("RePass");
            }
        }
        private int _typeRegister;
        public int typeRegister
        {
            get { return _typeRegister; }
            set { _typeRegister = value; OnPropertyChanged("typeRegister"); }
        }
        LoginHome loginHome;
        public RegisterAccount(LoginHome _loginHome, int type)
        {
            InitializeComponent();
            this.DataContext = this;
            this.loginHome = _loginHome;
            this.typeRegister = type;
            Pass = "";
            RePass = "";
        }

        private void ShowClick(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BackToLoginForm(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            loginHome.Show();
        }

        private void RegisterCompany(object sender, MouseButtonEventArgs e)
        {
            typeRegister = 1;
        }

        private void RegisterEmployee(object sender, MouseButtonEventArgs e)
        {
            typeRegister = 2;
        }

        private void RegisterIndividual(object sender, MouseButtonEventArgs e)
        {
            typeRegister = 3;
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtPasswordHide_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Pass = txtPasswordHide.Password;
        }

        private void LoginEnter(object sender, KeyEventArgs e)
        {
            Pass = txtPasswordHide.Password;
        }

        private void txtPasswordShow_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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

        private void LoginClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtRePasswordHide_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RePass = txtRePasswordHide.Password;
        }

        private void txtRePasswordShow_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            RePass = txtRePasswordHide.Password;
        }

        private void hideShowRePassword(object sender, MouseButtonEventArgs e)
        {
            if (txtRePasswordHide.Visibility == Visibility.Collapsed)
            {
                HiddenRePassword.Visibility = Visibility.Visible;
                showRePassword.Visibility = Visibility.Collapsed;
                txtRePasswordHide.Visibility = Visibility.Visible;
                txtRePasswordShow.Visibility = Visibility.Collapsed;
                txtRePasswordHide.Password = RePass;
                txtRePasswordHide.Focus();
            }
            else
            {
                HiddenRePassword.Visibility = Visibility.Collapsed;
                showRePassword.Visibility = Visibility.Visible;
                txtRePasswordHide.Visibility = Visibility.Collapsed;
                txtRePasswordShow.Visibility = Visibility.Visible;
                txtRePasswordShow.Text = RePass;
                txtRePasswordShow.Focus();
                txtRePasswordShow.CaretIndex = txtRePasswordShow.Text.Length;
            }
        }
    }
}
