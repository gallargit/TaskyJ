using System;
using System.Data;
using System.Data.Common;
using System.IO;
using Mono.Data.Sqlite;

namespace TaskyJ.DataRepo
{
	static class SQLiteDBFactoryAnd
	{
		static string dbFile = string.Empty;
		static SqliteFactory factory = null;
		static string connString = string.Empty;

		static SQLiteDBFactoryAnd() //init
		{
			dbFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			dbFile = Path.Combine(dbFile, "TaskyJ.db");

			if (File.Exists(dbFile))
				File.Delete(dbFile);

			connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);
			factory = new SqliteFactory();

			//test using System.Data.Common and SQLiteFactory
			InitializeDB();
		}

		public static DbConnection GetConnection()
		{
			DbConnection cx = factory.CreateConnection();
			cx.ConnectionString = connString;
			cx.Open();
			return cx;
		}

		static void InitializeDB()
		{
			using (DbConnection dbConn = factory.CreateConnection())
			{
				dbConn.ConnectionString = connString;
				dbConn.Open();
				using (DbCommand cmd = dbConn.CreateCommand())
				{
					cmd.CommandText = @"CREATE TABLE IF NOT EXISTS CategoryJ (ID integer primary key, Name text, IconBase64 text);";
					cmd.ExecuteNonQuery();

					cmd.CommandText = @"CREATE TABLE IF NOT EXISTS TaskyJ (ID integer primary key, Name text, Description text, Priority byte, Completed byte, Deleted boolean, CreationDate Datetime, FinishDate DateTime, Deadline DateTime, IdCategory integer);";
					cmd.ExecuteNonQuery();

					//to-ado add foreign keys

					//parameterized insert
					cmd.CommandText = @"INSERT INTO CategoryJ (ID,Name,IconBase64) VALUES(@id,@n,@ic)";
					for (int i = 1; i <= 3; i++)
					{
						var p1 = cmd.CreateParameter();
						p1.ParameterName = "@id";
						p1.Value = i;

						var p2 = cmd.CreateParameter();
						p2.ParameterName = "@n";
						p2.Value = "cat " + i;

						var p3 = cmd.CreateParameter();
						p3.ParameterName = "@ic";
						if (i == 1)
							p3.Value = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADmsAAA5rAVT+DQYAAAUwSURBVGhD7ZlbbFRVFIbXmZlDO20p01JanEs7mV70AamIjiAoYIpACjEiF/XFIqkpGnwQEtAHWk0UY7kEEVtppahR1BiNkdJWiBbFGAoP8qRBatBe0qkJ4dILpZ2zXWvP6tRpezrT6UytZL5m56z177P37P9cV04hRowY/y3HMzNd1DiNGgbeRoVau/0Bo6ado0Yxy1EhakbqrdYCnPwUhqnUKCZNdkaBqBips9vXCoPhOIZJPkWSRBr1cR5RIm6k3mbbgJtj2OIoV9RpsjGkHauz2db70sih8DYi1Dsc24QQ5RjKeY0zLJBzsBoE/jVvLQbvtaskEwIUZfuqlpZ9nEvOnO3YrBiUFZz6ENC4yJ3xLme6ROyM1Nvtb6KJPRhKE+qsdMh7/xNImDMXEufky5g0RgEh9tIYzn0oyjxc+PqABnC/r3NsJmykDOfAS+WQANjBEsQ5siCv5lOId+WwAjImLc6eyQodbNhBY2kOlsJmQhOcnz9fddvtH+GRfJ4lMOfeCblHjsE0q52VIUijPtrHD46lOWguVsIibCOf2+3mvz2er/A6epolSJw7D3KrPgZ1ZhorI1HTZsl9aN9BaI5Oj+dL0X3DxNK4CcvISZdrRjLeFhgW+hSA5AWLIaeiBozJ2BME2of2pTGDoJnVvTuL1oieLlbGx7iNNGRkpA/093+H1/fDLIFl+SpwHXgPDOYEVoJD+9IYS8FKVgC8bZetPaUlIK5dYSV0xmXkhMORranqz/jEuZclSFu7AZxv7MN3xfgvcRrj3L0fZj4+9FrxNv8K3TuLQPO0sRIa8lEZClgr3Y2uGzC8w6cApD9TDLYXt9MNy4qPm/0DcOZiJ2eBLM5Lh3h12K0gBLQdKIfOD6tZwClT0iBh1yEwZuUcxffIJpZ1CcnICZttgaIotRhS3SQXbt26DTKKnpPpcFqudIG79GvOAml69TFwpP67chnCc/QwtB/cK40RSlIymF8orV2ydeNqKYxB0EurITNzOZo4iaE0oZhMkFW2W9fERKA5HS+XgWLwLUt0XYfe/a88SmuQwhiMaQTfvE9omvYNhvIQUs3kfOttSF0TlbpPkrbuKch6fa88YIS41afSGmgtUtBB1whWqZvxBH+GoSz+jIlJkH2wCixLo1aJ+0lZUQjZ71SDISGRFYijteDD5lnORzCqERyAdzBUYTNSLou/iqMw3b2Q0klhuvtByK38QP42Y1SEqKbClPMARhihHXGAv4I1paRCzqEjsvgLFQMONU8zjdqoL1ToN+m3TZYUVgCXJsrrHI6XOPczYlY08hDu/C2G8VT80Rt4tLppMrnV3gqXtmyCvpY/Ke3RAB4pbG09KzuZEWdkZUvLj/h43RjvdN3QK/4mm8Fik9YkNO3J4SYI3fPc/teV04b4eH8ZMhXQbt78wZqZuoTTAHSfWmgCz+DUYqw16Rr5v6F7aXV09nyPm6W+LDje5gvQXUlP7dBJLNkDxux8zkKicXZ6wjKOA4jYGRF9PaC1XhxXozGR4ra5tGJGphoxI1ONmJFwcS5cBqrZzFnkmHQjBlUFgzHs73C6TLoRoXlB83o5ixy6Jcpvv1+tEgKGvkIHQblwygKVW+7hVBdFMYAQXPuVVPwi8gv8/2sIhqLApbtyLcWcBqBr5KemjkbsHrVkHg31j3OQXBX081MA14troN8V0n8NGHF6kXv2qPVf7Kk11bhtjIxxj3hqsNPNaVBMl8+bE7/YZeU0JLrXvdY+4Lyvl9OgCICmUL4Dx4gRYzgA/wDveIb8nGrHxwAAAABJRU5ErkJggg==";
						else if (i == 2)
							p3.Value = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADmsAAA5rAVT+DQYAAASwSURBVGhD7ZlraBxVGIbfs5ckm8tuklKqVgs2bRrT2DaGVIykKiqm4AVRi/6wILUgGANW6Q/FOk1K+6cGkVZREH+VgNgoRm2wpbUXi80NsZFYBAUVWkxSqW1Mspvd4zszJzWXvcxkZ3YD7gPfnO98M5s575nvnDNnghw5cvw/EKrMOPKcVgkvdrEFyyDRi1D5brG6ZVKdtk1WhMi+tgoI2Y/gHSHk3whc+wkY+7lb1Gub1SW28agys4joayiuCaG0AQjcCixtAgpuaJL9rY3qCttkR4hELfxLVEWHieErZzy2VgVs43pqyXtRjCieo1vJuw0ihqN4W+sEvOuRv4xFAIhcpv0FRKdasHPPIIXeQyukHRdn0G3+peS4KkRuwi1szDd0V5oRgxiqKq5i8/0hrLjJjESjQM/3QNexYYyPLzWDComDFNOsaglxV0gjPmPxGO0CG9TBu91F/0Gax7hz/QaguhLoPgFcGmbAYITXHuYVkyxfYD2P5SMU84V5Oj6uCZFPcXK9hCt0i3iXanEKQ0a8EdWoqTqDHy+UQUo9ZFJcyORb2Ymhwa3ia4zpIV77FosdtAPiNF7SY4lwb7APG5003VFTqvyPuV04Q9N1BCKqTNlO156IDsfI53pa0I2fWnXrgDUVwNHTwJ8jDBgsrtTScWSwC7zLtHxR1RLiqhAdeTdK2LvP062lDXAq7sI72mH66/Xzs9Cn31f26GOpiS0rpOQu8S2OmCeT47qQeMg+jd0fR4gQzaLuzYOqZovsrOwu4JgQKTWP7G1bIwf2zs5xIr/Tbpa92sxx4ji2U0ue0HwI+ZYjf+qiWKuFjVhPaz08sQ66nIIMDiEc3I6iv0s48R7igH/AiAq+rsfwNEu+ojibWraEsFe38hcH6JbQuHcQryMg38c4p1dATUHXaee1qyjiUVWfpp/mo2VnjMgebQMb9iFdXYROPqP7MeFroT9XBE/hSVq8/UUdjcu4s1gfIx48waPek7ORU88obzYCK3j0m5V5rFalY1gXIuXDyptLjSqzSm76dYPwyOjuiS+b9fcr2ywqITISXRIevfzeP53b++THL3PraJ1FmVpT18bqImXynPxhX5kKpWRRChE+L/yh4O0IT56VA9r8qT0O1oUIYazimcBfGmLLjKZV8U3gU+NtIgU2hOAP5TlAvO2gQgjklZeqisFGBMWzyk+IdSESvytPMd2YJI1KSOI3I18Jt/i+OQ9AygblJSSNMTLdGGe3NHllccf3RVUmJA0hzuMpyIO3aN6sG0bU+5HyE2JnjCz4S7lVfMGg8mYg8IG4841fVS0hdp5IysebLsIzL03PI1j+qvKTYuOJiPPKyxQRxDzbrP7PxLqQK/Ikj6NmJROINrFxV6+qpMSyEHGfpn8tXNDubQF8hV9u26t8S9gZI8BEYD+PDi6McelAAI+LLVuiqm4J24uA7G2thYidoltsRhxDX1nbuRHeKYQWM0PWWdBqpr6afEJX3846wW+UsU3Ua8dU3Tb2UkthDMKw8Qm0nTZhBBfGVdo+oGBdOiJ00n6/kGe1cvjFQxByE3t1Obsm+YZIGik0xDufxGTwiGjYMW6eyJEjR46UAP8C+QRI91hz35AAAAAASUVORK5CYII=";
						else if (i == 3)
							p3.Value = "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";

						cmd.Parameters.Add(p1);
						cmd.Parameters.Add(p2);
						cmd.Parameters.Add(p3);

						cmd.ExecuteNonQuery();
					}

					cmd.CommandText = @"INSERT INTO TaskyJ(ID,Name,Description,CreationDate,Priority,Completed,Deleted,IdCategory) VALUES(@id,@n,@d,@cd,@pr,@cm,@dl,@ic)";
					for (int i = 1; i <= 11; i++)
					{
						var p1 = cmd.CreateParameter();
						p1.ParameterName = "@id";
						p1.Value = i;

						var p2 = cmd.CreateParameter();
						p2.ParameterName = "@n";
						p2.Value = "test" + i + " SL_AND";

						var p3 = cmd.CreateParameter();
						p3.ParameterName = "@d";
						p3.Value = p2.Value + " descr";

						var p4 = cmd.CreateParameter();
						p4.ParameterName = "@cd";
						p4.Value = DateTime.Now;

						var p5 = cmd.CreateParameter();
						p5.ParameterName = "@pr";
						p5.Value = i;
						if (i > 5)
							p5.Value = 5;

						var p6 = cmd.CreateParameter();
						p6.ParameterName = "@cm";
						p6.Value = i * 10;
						if (i >= 10)
							p6.Value = 100;

						var p7 = cmd.CreateParameter();
						p7.ParameterName = "@dl";
						p7.Value = false;

						var p8 = cmd.CreateParameter();
						p8.ParameterName = "@ic";
						p8.Value = (i > 3 ? 3 : i);

						cmd.Parameters.Add(p1);
						cmd.Parameters.Add(p2);
						cmd.Parameters.Add(p3);
						cmd.Parameters.Add(p4);
						cmd.Parameters.Add(p5);
						cmd.Parameters.Add(p6);
						cmd.Parameters.Add(p7);
						cmd.Parameters.Add(p8);

						cmd.ExecuteNonQuery();
					}
					cmd.CommandText = "UPDATE TaskyJ SET FinishDate=Date('now') WHERE Completed>=100";
					cmd.ExecuteNonQuery();
					cmd.Dispose();
				}
				if (dbConn.State != ConnectionState.Closed)
					dbConn.Close();
				dbConn.Dispose();
				//factory.Dispose();
			}
		}
	}
}
