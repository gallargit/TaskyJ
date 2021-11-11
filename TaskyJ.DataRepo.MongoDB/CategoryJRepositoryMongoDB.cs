using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public class CategoryJRepositoryMongoDB : TaskyJRepositoryBaseMongoDB<DBCategoryJ>
    {
        protected override string tablename { get; set; }

        public CategoryJRepositoryMongoDB()
        {
            tablename = "DBCategoryJ";
        }

        public override DBCategoryJ GetItemInstance()
        {
            object o = new DBCategoryJ();
            return (DBCategoryJ)o;
        }
    }
}
