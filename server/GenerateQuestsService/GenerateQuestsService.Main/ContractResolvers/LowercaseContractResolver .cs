using Newtonsoft.Json.Serialization;

namespace GenerateQuestsService.Main.ContractResolvers
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
