using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.WPF.Model;

namespace TaskyJ.Interface.WPF.ViewModel
{
    public class WPFTaskListJViewModel : WPFViewModelBaseJ
    {
        public static ObservableCollection<WPFTaskJ> Tasks { get; set; } = new ObservableCollection<WPFTaskJ>();
        private readonly DBSessionJ CurrentSession = null;

        public WPFTaskListJViewModel(DBSessionJ session)
        {
            CurrentSession = session;
            Reload();
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        public void Reload(bool showDeleted = false)
        {
            Tasks.Clear();
            Business.TaskyJManager.RetrieveTasks(CurrentSession).ToList().ForEach(u => Tasks.Add(new WPFTaskJ(u)));
            if (showDeleted)
                Business.TaskyJManager.RetrieveDeletedTasks(CurrentSession).ToList().ForEach(u => Tasks.Add(new WPFTaskJ(u)));
        }

        private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Tasks");
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                return mUpdater ?? (mUpdater = new Updater(CurrentSession));
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            private readonly DBSessionJ CurrentSession = null;

            public Updater(DBSessionJ session)
            {
                CurrentSession = session;
            }

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                Tasks.ToList().ForEach(u => Business.TaskyJManager.PushTask(u.ToBaseEntity<DBTaskJ>() as DBTaskJ, CurrentSession));
            }

            #endregion
        }
    }
}
