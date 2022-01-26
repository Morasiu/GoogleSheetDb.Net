namespace GoogleSheetDb.Net.Core.Scheme;

public class Table {
	public string Name { get; set; }

	public Type Type { get; set; }

	public IEnumerable<Column> Columns { get; set; }
}