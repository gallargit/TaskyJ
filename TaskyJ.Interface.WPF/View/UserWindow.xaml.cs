using System.Windows;
using TaskyJ.Interface.WPF.ViewModel;

namespace TaskyJ.Interface.WPF.View
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
