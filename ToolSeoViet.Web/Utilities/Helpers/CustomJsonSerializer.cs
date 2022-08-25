using Blazored.LocalStorage.Serialization;
using Newtonsoft.Json;

namespace TuanVu.Management.Web.Utilities.Helpers {

    public class CustomJsonSerializer : IJsonSerializer {

        public T Deserialize<T>(string text)
            => JsonConvert.DeserializeObject<T>(text);

        public string Serialize<T>(T obj)
            => JsonConvert.SerializeObject(obj);
    }
}