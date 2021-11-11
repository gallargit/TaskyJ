using System.Windows;
using TaskyJ.Interface.WPF.View;

namespace TaskyJ.Interface.WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            if (!Business.TaskyJManager.IsAlive())
            {
                MessageBox.Show("Service is not reachable, exiting", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public LoginWindow(string username, string password) : this()
        {
            txtUserName.Text = username;
            txtPassword.Password = password;
            btnOK_Click(null, null);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var session = Business.TaskyJManager.Authenticate(txtUserName.Text, txtPassword.Password, "127.0.0.1");
            if (session == null)
            {
                MessageBox.Show("Wrong user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (!IsVisible)
                    Show();
            }
            else
            {
                new MainWindow(session).Show();
                Close();
            }
        }
    }
}
