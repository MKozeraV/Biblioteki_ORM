//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : Dyplomowa
	/// Data Source    : .
	/// Server Version : 12.00.2269
	/// </summary>
	public partial class DyplomowaDB : LinqToDB.Data.DataConnection
	{
		public ITable<Book> Books { get { return this.GetTable<Book>(); } }

		public DyplomowaDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public DyplomowaDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public DyplomowaDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public DyplomowaDB(DataOptions<DyplomowaDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="Books")]
	public partial class Book
	{
		[Column("id_k"),    PrimaryKey, NotNull] public int    IdK     { get; set; } // int
		[Column("nazwa"),               NotNull] public string Nazwa   { get; set; } // nvarchar(100)
		[Column("autor"),               NotNull] public string Autor   { get; set; } // nvarchar(100)
		[Column("gatunek"),             NotNull] public string Gatunek { get; set; } // nvarchar(100)
	}

	public static partial class TableExtensions
	{
		public static Book Find(this ITable<Book> table, int IdK)
		{
			return table.FirstOrDefault(t =>
				t.IdK == IdK);
		}
	}
}
