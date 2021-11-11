using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TaskyJ.Globals.Data.DataObjects
{
    public class DBTaskJ : BaseEntity
    {
        public enum TaskPriority : byte
        {
            No_Priority = 0,
            Idle = 1,
            Minor = 2,
            Normal = 3,
            Major = 4,
            Critical = 5
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; } = new DateTime((DateTime.Now.Ticks / 10000000) * 10000000, DateTimeKind.Unspecified);
        [DataMember]
        public DateTime? Deadline { get; set; } = null;
        [DataMember]
        public DateTime? FinishDate { get; set; } = null;
        [DataMember]
        [Range(0, 100, ErrorMessage = "Value must range from 0 to 100")]
        public byte Completed { get; set; } //0 to 100
        [DataMember]
        public TaskPriority Priority { get; set; } = 0;
        [DataMember]
        public bool Deleted { get; set; } = false;
        [DataMember]
        public int? IDCategory
        {
            get
            {
                return (Category == null ? (int?)null : Category.ID);
            }
            set
            {
                if (value != null)
                {
                    Category = new DBCategoryJ
                    {
                        ID = value.Value
                    };
                }
            }
        }
        [DataMember]
        public int? IDUser
        {
            get
            {
                return (User == null ? (int?)null : User.ID);
            }
            set
            {
                if (value != null)
                    User = new DBUserJ
                    {
                        ID = value.Value
                    };
            }
        }

        public DBCategoryJ Category { get; set; } = null;
        public DBUserJ User { get; set; } = null;

        public override string ToString()
        {
            return Name + (Deleted ? "   ✗" : (Completed < 100 ? "" : "   ✓"));
        }

        public void CheckFinishTask()
        {
            if (Completed >= 100)
            {
                Completed = 100;
                if (FinishDate == null)
                    FinishDate = new DateTime((DateTime.Now.Ticks / 10000000) * 10000000, DateTimeKind.Unspecified);
            }
            else
            {
                FinishDate = null;
            }
        }

        public override void CopyFrom(BaseEntity source)
        {
            if (source != null)
            {
                base.CopyFrom(source);
                if (source is DBTaskJ sourcej)
                {
                    Name = sourcej.Name;
                    Description = sourcej.Description;
                    CreationDate = sourcej.CreationDate;
                    FinishDate = sourcej.FinishDate;
                    Completed = sourcej.Completed;
                    Priority = sourcej.Priority;
                    Deadline = sourcej.Deadline;
                    Deleted = sourcej.Deleted;
                    Category = sourcej.Category;
                }
            }
        }

        public override bool Equals(BaseEntity source)
        {
            if (source is DBTaskJ sourcej)
            {
                return Name == sourcej.Name &&
                        Description == sourcej.Description &&
                        CreationDate == sourcej.CreationDate &&
                        FinishDate == sourcej.FinishDate &&
                        Completed == sourcej.Completed &&
                        Priority == sourcej.Priority &&
                        Deadline == sourcej.Deadline &&
                        base.Equals(source);
            }
            else
            {
                return base.Equals(source);
            }
        }

        public void SetPriority(int priority)
        {
            if (priority >= 0 && priority <= 5)
                Priority = (TaskPriority)priority;
        }
    }
}
