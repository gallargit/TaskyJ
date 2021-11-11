using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public class TaskyJRepositorySTSDB : TaskyJRepositoryBaseSTSDB<DBTaskJ>
    {
        protected override string tablename { get; set; }

        public TaskyJRepositorySTSDB()
        {
            tablename = "DBTaskJ";
        }

        public TaskyJRepositorySTSDB(string BaseURL)
        {
            Baseurl = BaseURL;
            tablename = "DBTaskJ";
        }

        public override DBTaskJ GetItemInstance()
        {
            object o = new DBTaskJ();
            return (DBTaskJ)o;
        }
    }
}
