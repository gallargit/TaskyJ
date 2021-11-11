using System;
using TaskyJ.Globals.Data.DataObjects;
using static TaskyJ.Globals.Data.DataObjects.DBTaskJ;

namespace TaskyJ.Interface.WPFNetCore.Model
{
    public class WPFTaskJ : WPFBaseEntityJ
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }


        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private DateTime _creationdate;
        public DateTime CreationDate
        {
            get
            {
                return _creationdate;
            }
            set
            {
                _creationdate = value;
                OnPropertyChanged("CreationDate");
            }
        }

        private DateTime? _deadline;
        public DateTime? Deadline
        {
            get
            {
                return _deadline;
            }
            set
            {
                _deadline = value;
                OnPropertyChanged("Deadline");
            }
        }

        private DateTime? _finishdate;
        public DateTime? FinishDate
        {
            get
            {
                return _finishdate;
            }
            set
            {
                _finishdate = value;
                OnPropertyChanged("FinishDate");
            }
        }
        private byte _completed;
        public byte Completed
        {
            get
            {
                return _completed;
            }
            set
            {
                _completed = value;
                OnPropertyChanged("Completed");
            }
        }

        private TaskPriority _priority;
        public TaskPriority Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }


        private bool _deleted;
        public bool Deleted
        {
            get
            {
                return _deleted;
            }
            set
            {
                _deleted = value;
                OnPropertyChanged("Deleted");
            }
        }

        private int _idcategory;
        public int IDCategory
        {
            get
            {
                return _idcategory;
            }
            set
            {
                _idcategory = value;
                OnPropertyChanged("IDCategory");
            }
        }

        private int _iduser;
        public int IDUser
        {
            get
            {
                return _iduser;
            }
            set
            {
                _iduser = value;
                OnPropertyChanged("IDUser");
            }
        }

        public WPFTaskJ()
        {
        }

        public WPFTaskJ(BaseEntity originalObject) : base(originalObject)
        {
        }

        public override string ToString()
        {
            return Name + (Deleted ? "   ✗" : (Completed < 100 ? "" : "   ✓"));
        }
    }
}
