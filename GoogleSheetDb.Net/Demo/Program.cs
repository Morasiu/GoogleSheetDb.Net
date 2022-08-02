using System.Globalization;
using GoogleSheetDb.Net.Core;

namespace Demo {
	class Program {
		static string ApplicationName = "GoogleSheetDbTest";

		static async Task Main() {
			var spreadsheetId = "1Nayfp89asArT9kHfg6LoHgfakrwvZFCuYpREXPtjaaM";
			var sheetDbOptions = new SheetDbOptions() {
				SpreadsheetId = spreadsheetId,
				CredentialsPath = "credentials.json"
			};
			var sheetDb = new SheetDb(sheetDbOptions);
			
			sheetDb.Authenticate();
			
			var productEntities = await sheetDb.GetAllAsync<ProductEntity>();

			var newProduct = new ProductEntity() {
				Id = Guid.NewGuid().ToString(),
				Name = "Orange",
				Price = 1.23m.ToString(CultureInfo.InvariantCulture)
			};

			await sheetDb.AddAsync(newProduct);
		}
	}
}