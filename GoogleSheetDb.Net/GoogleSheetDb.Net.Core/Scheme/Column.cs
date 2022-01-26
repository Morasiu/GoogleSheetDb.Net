using System.Reflection;

namespace GoogleSheetDb.Net.Core.Scheme;

public class Column {
	public string Name { get; set; }
	public PropertyInfo Info { get; set; }
}