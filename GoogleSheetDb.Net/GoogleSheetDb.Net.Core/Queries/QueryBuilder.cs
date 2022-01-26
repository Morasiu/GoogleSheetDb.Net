using System.Text;

namespace GoogleSheetDb.Net.Core.Queries; 

public class QueryBuilder {
	private string _from = $"{SheetColumn.A}{0}";
	private string _to = $"{SheetColumn.A}{0}";

	public QueryBuilder From(SheetColumn sheetColumn = SheetColumn.A, int row = 0) {
		var rowValue = row != 0 ? row.ToString() : string.Empty;
		_from = $"{sheetColumn}{rowValue}";
		return this;
	}
	
	public QueryBuilder To(SheetColumn sheetColumn = SheetColumn.A, int row = 0) {
		var rowValue = row != 0 ? row.ToString() : string.Empty;
		_to = $"{sheetColumn}{rowValue}";
		return this;
	}

	public string Build() {
		return $"{_from}:{_to}";
	}
}