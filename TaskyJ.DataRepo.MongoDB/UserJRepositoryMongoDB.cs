using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public class UserJRepositoryMongoDB : TaskyJRepositoryBaseMongoDB<DBUserJ>
    {
        protected override string tablename { get; set; }

        public UserJRepositoryMongoDB()
        {
            tablename = "DBUserJ";
        }

        public override DBUserJ GetItemInstance()
        {
            object o = new DBUserJ();
            return (DBUserJ)o;
        }
    }
}
