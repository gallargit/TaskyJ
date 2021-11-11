using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Interface.WPFNetCore.Model
{
    public class WPFUserJ : WPFBaseEntityJ
    {
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public WPFUserJ()
        {
        }

        public WPFUserJ(BaseEntity originalObject) : base(originalObject)
        {
        }
    }
}

