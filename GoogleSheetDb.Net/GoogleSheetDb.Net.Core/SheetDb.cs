using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace GoogleSheetDb.Net.Core;

public class SheetDb {
	public SheetDb(string credentialsPath, string applicationName, string spreadsheetId) {
		GoogleCredential credential;

		using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read)) {
			credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);
		}
			
		// Create Google Sheets API service.
		Service = new SheetsService(new BaseClientService.Initializer() {
			HttpClientInitializer = credential,
			ApplicationName = applicationName
		});
	}

	public SheetsService Service { get; }
}