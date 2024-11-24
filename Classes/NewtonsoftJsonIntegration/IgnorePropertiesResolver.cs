using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Il2CppInterop.Runtime.InteropTypes;

namespace ClimeronToolsForPvZ.Classes.NewtonsoftJsonIntegration
{
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly List<string> _ignoreProps = new(){ nameof(Il2CppObjectBase.ObjectClass), nameof(Il2CppObjectBase.Pointer), nameof(Il2CppObjectBase.WasCollected) };
        
        public IgnorePropertiesResolver() { }
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore) =>
            _ignoreProps.AddRange(propNamesToIgnore);
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (_ignoreProps.Contains(property.PropertyName))
                property.ShouldSerialize = _ => false;
            return property;
        }
    }
}
