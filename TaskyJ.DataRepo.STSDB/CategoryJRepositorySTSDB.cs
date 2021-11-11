using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
	public class CategoryJRepositorySTSDB : TaskyJRepositoryBaseSTSDB<DBCategoryJ>
	{
		protected override string tablename { get; set; }

		public CategoryJRepositorySTSDB()
		{
			tablename = "DBCategoryJ";
		}

		public CategoryJRepositorySTSDB(string BaseURL)
		{
			Baseurl = BaseURL;
			tablename = "DBCategoryJ";
		}

		public override DBCategoryJ GetItemInstance()
		{
			object o = new DBCategoryJ();
			return (DBCategoryJ)o;
		}
	}
}
