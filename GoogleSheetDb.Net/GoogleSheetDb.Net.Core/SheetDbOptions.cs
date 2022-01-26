namespace GoogleSheetDb.Net.Core; 

public class SheetDbOptions {
	public string CredentialsPath { get; set; } = string.Empty;
	public string ApplicationName { get; set; } = "GoogleSheetDb";
	public string SpreadsheetId { get; set; } = string.Empty;

	public List<Enitity> Enitities { get; set; } = new();
}

public class Enitity {
	public Type Type { get; set; }

	public Dictionary<string, SheetColumn> BindValues { get; set; } = new();

	public static Enitity FromType<T>() {
		var bindValues = new Dictionary<string, SheetColumn>();
		var type = typeof(T);
		for (var i = 0; i < type.GetProperties().Length; i++) {
			var property = type.GetProperties()[i];
			bindValues.Add(property.Name, (SheetColumn) i);
		}

		return new Enitity() { Type = type, BindValues = bindValues};
	}
}