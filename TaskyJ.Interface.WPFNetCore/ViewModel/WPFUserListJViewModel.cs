using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.WPFNetCore.Model;

namespace TaskyJ.Interface.WPFNetCore.ViewModel
{
    public class WPFUserListJViewModel : WPFViewModelBaseJ
    {
        public static ObservableCollection<WPFUserJ> Users { get; set; } = new ObservableCollection<WPFUserJ>();

        public WPFUserListJViewModel()
        {
            Business.TaskyJManager.RetrieveUsers().ToList().ForEach(u => Users.Add(new WPFUserJ(u)));
            Users.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Users_CollectionChanged);
        }

        void Users_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Users");
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                Users.ToList().ForEach(u => Business.TaskyJManager.PushUser(u.ToBaseEntity<DBUserJ>() as DBUserJ));
            }

            #endregion
        }
    }
}
