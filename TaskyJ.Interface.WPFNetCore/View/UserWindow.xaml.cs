using System.Windows;
using TaskyJ.Interface.WPFNetCore.ViewModel;

namespace TaskyJ.Interface.WPFNetCore.View
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();

            DataContext = new WPFUserListJViewModel();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
