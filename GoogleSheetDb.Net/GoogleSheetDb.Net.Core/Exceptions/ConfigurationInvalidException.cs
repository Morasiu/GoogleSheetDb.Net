namespace GoogleSheetDb.Net.Core.Exceptions; 

public class ConfigurationInvalidException : Exception {
	public ConfigurationInvalidException(string entityName) : base($"Configuration for entity: {entityName} was invalid") {
		
	}
}