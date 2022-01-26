using GoogleSheetDb.Net.Core;

namespace Demo {
	class Program {
		static string ApplicationName = "GoogleSheetDbTest";

		static void Main(string[] args) {
			var spreadsheetId = "1Nayfp89asArT9kHfg6LoHgfakrwvZFCuYpREXPtjaaM";
			var sheetDbOptions = new SheetDbOptions() {
				SpreadsheetId = spreadsheetId,
				CredentialsPath = "credentials.json"
			};
			var sheetDb = new SheetDb(sheetDbOptions);
			var productEntities = sheetDb.GetAll<ProductEntity>();
		}
	}
}