using System;
using System.Windows;
using System.Windows.Resources;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.WPF.Model;
using TaskyJ.Interface.WPF.ViewModel;

namespace TaskyJ.Interface.WPF.View
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
                var originalCursor = Cursor;
                StreamResourceInfo sri = Application.GetResourceStream(new Uri("resources/busy.ani", UriKind.Relative));
                var customCursor = new System.Windows.Input.Cursor(sri.Stream);
                Cursor = customCursor;
                var nw = new DetailWindow(CurrentSession, ((WPFTaskJ)lstTasks.SelectedItem).ID);
                if (nw.ShowDialog().Value)
                    ((WPFTaskListJViewModel)DataContext).Reload(btnShowDeleted.IsChecked.Value);
                lstTasks.SelectedItem = null;
                Cursor = originalCursor;
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
