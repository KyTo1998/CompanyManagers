using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace CompanyManagers.Common.Popups
{
    /// <summary>
    /// Interaction logic for PopupNoticationAll.xaml
    /// </summary>
    public partial class PopupNoticationAll : UserControl
    {
        private int countdownValue = 3;
        private DispatcherTimer countdownTimer;
        public event EventHandler CountdownFinished;
        BrushConverter br = new BrushConverter();
        ManagerHome managerHome;
        string TienTB;
        string TB;
        string HauTB;
        public PopupNoticationAll(ManagerHome _managerHome, string tienTB, string tB, string hauTB)
        {
            InitializeComponent();
            StartCountdown();
            if (tB.Contains("thành công"))
            {
                icon_ThanhCong.Visibility = Visibility.Visible;
                tb_TextThongBaoAll.Foreground = (Brush)br.ConvertFrom("#0086DA");
            }
            else if (tB.Contains("lỗi hệ thống") || tB.Contains("không") || tB.Contains("thất bại"))
            {
                icon_ThatBai.Visibility = Visibility.Visible;
                tb_TextThongBaoAll.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
            }
            this.managerHome = _managerHome;
            TienTB = tienTB;
            TB = tB;
            HauTB = hauTB;
            tb_TienToThongBao.Text = tienTB;
            tb_ThongBao.Text = tB;
            tb_HauToThongBao.Text = hauTB;
        }
        private void btnDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void StartCountdown()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();

        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            progressBar.Value = countdownValue;

            countdownValue--;

            if (countdownValue < 0)
            {
                countdownTimer.Stop();
                this.Visibility = Visibility.Collapsed;
                CountdownFinished?.Invoke(this, EventArgs.Empty);
            }

        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                // Mở URL được chỉ định khi hyperlink được nhấp
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
