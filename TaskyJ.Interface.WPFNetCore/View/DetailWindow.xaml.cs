using System;
using System.Linq;
using System.Windows;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.WPFNetCore.ViewModel;
using static TaskyJ.Globals.Data.DataObjects.DBTaskJ;

namespace TaskyJ.Interface.WPFNetCore.View
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private readonly DBSessionJ CurrentSession = null;
        private readonly int CurrentTaskID = 0;

        public DetailWindow(DBSessionJ session, int idTask)
        {
            InitializeComponent();
            CurrentSession = session;
            CurrentTaskID = idTask;
            DataContext = new WPFTaskJViewModel(CurrentSession, CurrentTaskID);
            if (CurrentTaskID == 0)
            {
                btnUndo.IsEnabled = btnDelete.IsEnabled = false;
                btnUpdate.Content = "OK";
            }
            txtPriority.ItemsSource = Enum.GetValues(typeof(TaskPriority)).Cast<TaskPriority>();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new WPFTaskJViewModel(CurrentSession, CurrentTaskID);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sure to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                WPFTaskJViewModel.DeleteEnabled = true;
                DialogResult = true;
                Close();
            }
            else
            {
                WPFTaskJViewModel.DeleteEnabled = false;
            }
        }
    }
}
