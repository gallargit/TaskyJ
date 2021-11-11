using TaskyJ.DataObjects;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public class CategoryJRepositoryAnd : RepositoryBaseDapperAnd<DBCategoryJ>
    {
        public CategoryJRepositoryAnd() : base("CategoryJ", "ID") { }

        public override DBCategoryJ GetItemInstance()
        {
            return new DBCategoryJ();
        }

    }
}
