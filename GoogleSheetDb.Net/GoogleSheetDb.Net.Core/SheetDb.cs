using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using GoogleSheetDb.Net.Core.Extensions;
using GoogleSheetDb.Net.Core.Queries;

namespace GoogleSheetDb.Net.Core;

public class SheetDb {
	private readonly SheetDbOptions _options;
	public bool IsAuthenticated { get; private set; }
	public SheetsService? Service { get; private set; }

	public Scheme.Scheme Scheme { get; private set; }

	public SheetDb(SheetDbOptions options) {
		if (options.SpreadsheetId.IsNullOrEmpty())
			throw new ArgumentNullException(options.SpreadsheetId);

		if (!File.Exists(options.CredentialsPath)) {
			throw new FileNotFoundException(options.CredentialsPath);
		}

		_options = options;
	}

	public IEnumerable<T> GetAll<T>() {
		if (!IsAuthenticated) Authenticate();

		// var sheets = Service!.Spreadsheets.Get(_options.SpreadsheetId).Execute().Sheets;
		// var sheet = sheets.SingleOrDefault(a => a.Properties.Title == nameof(T));
		var type = typeof(T);
		var properties = type.GetProperties();
		var query = new QueryBuilder().From(SheetColumn.A).To((SheetColumn) properties.Length).Build();
		var data = Service.Spreadsheets.Values.Get(_options.SpreadsheetId, query).Execute();
		var values = data.Values;
		var all = new List<T>();
		for (int i = 1; i < values.Count; i++) {
			var row = values[i];
			var o = Activator.CreateInstance<T>();
			for (int j = 0; j < row.Count; j++) {
				var value = row[j];
				var propertyInfo = properties[j];
				propertyInfo.SetValue(o, value);
			}
			all.Add(o);
		}
		
		return all;
	}

	public void Authenticate() {
		GoogleCredential credential;

		using (var stream = new FileStream(_options.CredentialsPath, FileMode.Open, FileAccess.Read)) {
			credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);
		}

		// Create Google Sheets API service.
		Service = new SheetsService(new BaseClientService.Initializer() {
			HttpClientInitializer = credential,
			ApplicationName = _options.ApplicationName
		});

		IsAuthenticated = true;
	}


	// public bool CheckScheme() {
	// 	if (!IsAuthenticated) Authenticate();
	//
	// 	var spreadsheet = Service!.Spreadsheets.Get(_options.SpreadsheetId).Execute();
	// 	var sheets = spreadsheet.Sheets;
	// 	
	// 	foreach (var entity in _options.Enitities) {
	// 		var sheet = sheets.SingleOrDefault(a => a.Properties.Title == entity.Type.Name);
	// 		if (sheet is null)
	// 			return false;
	// 		var query = new QueryBuilder().From(SheetColumn.A).To((SheetColumn) entity.BindValues.Count).Build();
	// 		var response = Service.Spreadsheets.Values.Get(_options.SpreadsheetId, query).Execute();
	// 		var row = response.Values.First();
	// 		foreach (var bind in entity.BindValues) {
	// 		}
	// 	}
	// 	
	// 	return true;
	// }
}