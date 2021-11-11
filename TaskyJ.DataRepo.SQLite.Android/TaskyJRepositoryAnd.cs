using TaskyJ.DataObjects;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public class TaskyJRepositoryAnd : RepositoryBaseDapperAnd<DBTaskJ>
    {
        public TaskyJRepositoryAnd() : base("TaskyJ", "ID") { }

        public override DBTaskJ GetItemInstance()
        {
            object o = new DBTaskJ();
            return (DBTaskJ)o;
        }

    }
}
