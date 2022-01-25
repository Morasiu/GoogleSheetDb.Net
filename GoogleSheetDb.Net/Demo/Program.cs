using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using GoogleSheetDb.Net.Core;

namespace Demo {
	class Program {
		static string ApplicationName = "GoogleSheetDbTest";

		static void Main(string[] args) {
			var spreadsheetId = "1Nayfp89asArT9kHfg6LoHgfakrwvZFCuYpREXPtjaaM";
			var sheetDb = new SheetDb("credentials.json", "Test", spreadsheetId);
			var service = sheetDb.Service;

			// // INSERT
			// var data = new List<object>() { Guid.NewGuid(), "HEJ from c#" };
			// var valueRange = new ValueRange();
			// valueRange.Values = new IList<object>[] { data };
			//
			// var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, "A:F");
			// appendRequest.ValueInputOption =
			// 	SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
			//
			// var appendResponse = appendRequest.Execute();
			
			// Define request parameters.
			var range = "A:B";
			var request = service.Spreadsheets.Values.Get(spreadsheetId, range);

			// Prints the names and majors of students in a sample spreadsheet:
			// https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
			var response = request.Execute();
			var values = response.Values;
			
			if (values != null && values.Count > 0) {
				foreach (var row in values) {
					// Print columns A and E, which correspond to indices 0 and 4.
					Console.WriteLine($"{row[0]}, {row[1]}");
				}
			}
			else {
				Console.WriteLine("No data found.");
			}

			Console.Read();
		}
	}
}