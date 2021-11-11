using System.Windows;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.WPFNetCore.Model;
using TaskyJ.Interface.WPFNetCore.ViewModel;

namespace TaskyJ.Interface.WPFNetCore.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DBSessionJ CurrentSession = null;

        public MainWindow(DBSessionJ session)
        {
            InitializeComponent();
            CurrentSession = session;
            DataContext = new WPFTaskListJViewModel(CurrentSession);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ((WPFTaskListJViewModel)DataContext).Reload(btnShowDeleted.IsChecked.Value);
        }

        private void lstTasks_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                var nw = new DetailWindow(CurrentSession, ((WPFTaskJ)lstTasks.SelectedItem).ID);
                if (nw.ShowDialog().Value)
                    ((WPFTaskListJViewModel)DataContext).Reload(btnShowDeleted.IsChecked.Value);
                lstTasks.SelectedItem = null;
            }
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            var nw = new UserWindow();
            nw.ShowDialog();
        }

        private void btnShowDeleted_Click(object sender, RoutedEventArgs e)
        {
            ((WPFTaskListJViewModel)DataContext).Reload(btnShowDeleted.IsChecked.Value);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var nw = new DetailWindow(CurrentSession, 0);
            if (nw.ShowDialog().Value)
                ((WPFTaskListJViewModel)DataContext).Reload(btnShowDeleted.IsChecked.Value);
        }
    }
}
