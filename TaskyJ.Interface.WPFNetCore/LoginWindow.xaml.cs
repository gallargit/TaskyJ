using System;
using System.Windows;
using TaskyJ.Interface.WPFNetCore.View;

namespace TaskyJ.Interface.WPFNetCore
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
                Environment.Exit(0);
            }
            //autologin
            //btnOK_Click(null, null);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var session = Business.TaskyJManager.Authenticate(txtUserName.Text, txtPassword.Password, "127.0.0.1");
            if (session == null)
            {
                MessageBox.Show("Wrong user");
            }
            else
            {
                new MainWindow(session).Show();
                Close();
            }
        }
    }
}
