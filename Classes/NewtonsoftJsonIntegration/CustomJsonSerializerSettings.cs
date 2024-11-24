using Newtonsoft.Json;

namespace ClimeronToolsForPvZ.Classes.NewtonsoftJsonIntegration
{
    public class CustomJsonSerializerSettings : JsonSerializerSettings
    {
        public CustomJsonSerializerSettings(bool prettyPrint)
        {
            ContractResolver = new IgnorePropertiesResolver();
            Formatting = prettyPrint ? Formatting.Indented : Formatting.None;
        }
    }
}
