using System;
using System.Linq;
using System.Windows;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.WPF.ViewModel;
using static TaskyJ.Globals.Data.DataObjects.DBTaskJ;

namespace TaskyJ.Interface.WPF.View
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private bool dirty = false;

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
            dirty = false;
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            dirty = false;
            DialogResult = false;
            btnUndo_Click(sender, e);
            Close();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            dirty = false;
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

        private void textbox_KeyUp(object sender, RoutedEventArgs e)
        {
            dirty = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dirty)
            {
                switch (MessageBox.Show("Do you want to save changes?", "Unsaved data", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        btnUpdate_Click(sender, null);
                        break;
                    case MessageBoxResult.No:
                        btnUndo_Click(sender, null);
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
