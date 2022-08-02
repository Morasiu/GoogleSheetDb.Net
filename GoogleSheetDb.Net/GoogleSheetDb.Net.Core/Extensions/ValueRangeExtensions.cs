using System.Collections.ObjectModel;
using Google.Apis.Sheets.v4.Data;

namespace GoogleSheetDb.Net.Core.Extensions; 

public static class ValueRangeExtensions {
	public static ValueRange FromObject<T>(T entity) {
		var valueRange = new ValueRange();

		var properties = typeof(T).GetProperties();
		var values = new List<object>(properties.Length) { };

		foreach (var propertyInfo in properties) {
			values.Add(propertyInfo.GetValue(entity)?.ToString() ?? throw new InvalidOperationException());
		}
		
		valueRange.Values = new List<IList<object>>() {
			values
		};
		
		return valueRange;
	}
}