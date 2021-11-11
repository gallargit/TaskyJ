using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Globals.Data.Helpers;

namespace TaskyJ.DataRepo
{
    public class UserJRepositorySTSDB : TaskyJRepositoryBaseSTSDB<DBUserJ>
    {
        protected override string tablename { get; set; }

        public UserJRepositorySTSDB()
        {
            tablename = "DBUserJ";
        }

        public UserJRepositorySTSDB(string BaseURL)
        {
            Baseurl = BaseURL;
            tablename = "DBUserJ";
        }

        public override DBUserJ GetItemInstance()
        {
            object o = new DBUserJ();
            return (DBUserJ)o;
        }

        public override void Add(DBUserJ objectToAdd)
        {
            //do not change password unless a new one is in specified in objectToAdd
            if (string.IsNullOrEmpty(objectToAdd.Password))
            {
                var existingUser = GetById(objectToAdd.ID);
                if (existingUser != null)
                    objectToAdd.Password = existingUser.Password;
            }
            else
            {
                objectToAdd.Password = JPasswordHasher.HashJ(objectToAdd.Password, JPasswordHasher.Salt);
            }
            base.Add(objectToAdd);
        }
    }
}
