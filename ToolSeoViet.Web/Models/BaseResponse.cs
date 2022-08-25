using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToolSeoViet.Web.Models {

    public class BaseResponse {
        public bool Success { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class BaseResponse<T> : BaseResponse {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }

    public class BaseListData<T> {
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }
}