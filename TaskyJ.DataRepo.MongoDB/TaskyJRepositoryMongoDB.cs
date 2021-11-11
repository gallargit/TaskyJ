using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public class TaskyJRepositoryMongoDB : TaskyJRepositoryBaseMongoDB<DBTaskJ>
    {
        protected override string tablename { get; set; }

        public TaskyJRepositoryMongoDB()
        {
            tablename = "DBTaskJ";
        }

        public override DBTaskJ GetItemInstance()
        {
            object o = new DBTaskJ();
            return (DBTaskJ)o;
        }
    }
}
