using System;
using System.Linq;
using System.Windows.Input;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.WPFNetCore.Model;

namespace TaskyJ.Interface.WPFNetCore.ViewModel
{
    public class WPFTaskJViewModel : WPFViewModelBaseJ
    {
        public static WPFTaskJ CurrentTask { get; set; }
        private DBSessionJ CurrentSession = null;

        public WPFTaskJViewModel(DBSessionJ session, int idTask)
        {
            CurrentSession = session;
            if (idTask == 0)
                CurrentTask = new WPFTaskJ();
            else
            {
                var dbtask = Business.TaskyJManager.RetrieveTasks(session).FirstOrDefault(t => t.ID == idTask);
                if (dbtask == null)
                    dbtask = Business.TaskyJManager.RetrieveDeletedTasks(session).FirstOrDefault(t => t.ID == idTask);
                CurrentTask = new WPFTaskJ(dbtask);
            }
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater(CurrentSession);
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private ICommand mUndoer;
        public ICommand UndoCommand
        {
            get
            {
                if (mUndoer == null)
                    mUndoer = new Undoer(CurrentSession);
                return mUndoer;
            }
            set
            {
                mUndoer = value;
            }
        }

        public static bool DeleteEnabled { get; set; } = true;

        private ICommand mDeleter;
        public ICommand DeleteCommand
        {
            get
            {
                if (mDeleter == null)
                    mDeleter = new Deleter(CurrentSession);
                return mDeleter;
            }
            set
            {
                mDeleter = value;
            }
        }

        private class Updater : ICommand
        {
            private DBSessionJ CurrentSession = null;

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
                Business.TaskyJManager.PushTask(CurrentTask.ToBaseEntity<DBTaskJ>() as DBTaskJ, CurrentSession);
            }

            #endregion
        }

        private class Undoer : ICommand
        {
            private DBSessionJ CurrentSession = null;

            public Undoer(DBSessionJ session)
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
                CurrentTask = new WPFTaskJ(Business.TaskyJManager.RetrieveTasks(CurrentSession).FirstOrDefault(t => t.ID == CurrentTask.ID));
            }

            #endregion
        }

        private class Deleter : ICommand
        {
            private DBSessionJ CurrentSession = null;

            public Deleter(DBSessionJ session)
            {
                CurrentSession = session;
            }

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return DeleteEnabled;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                Business.TaskyJManager.RemoveTask(CurrentTask.ID);
            }

            #endregion
        }
    }
}
