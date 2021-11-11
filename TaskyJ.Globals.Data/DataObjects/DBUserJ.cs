using System.Runtime.Serialization;

namespace TaskyJ.Globals.Data.DataObjects
{
    public class DBUserJ : BaseEntity
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }

        public override string ToString()
        {
            return UserName;
        }

        public override bool Equals(BaseEntity source)
        {
            if (source is DBUserJ sourcej)
            {
                return UserName == sourcej.UserName && base.Equals(source);
            }
            else
            {
                return base.Equals(source);
            }
        }

        public DBUserJ() : base() { }

        public DBUserJ(DBUserJ sourcej)
        {
            UserName = sourcej.UserName;
            Password = sourcej.Password;
        }

        public override void CopyFrom(BaseEntity source)
        {
            if (source != null)
            {
                base.CopyFrom(source);
                if (source is DBUserJ sourcej)
                {
                    UserName = sourcej.UserName;
                    Password = sourcej.Password;
                }
            }
        }
    }
}
