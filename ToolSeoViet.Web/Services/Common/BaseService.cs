using System.Collections.Generic;
using System.Linq;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models;

namespace ToolSeoViet.Web.Services.Common {

    public abstract class BaseService {
        protected readonly HttpService httpService;

        protected BaseService(HttpService httpService) {
            this.httpService = httpService;
        }

        protected static void ValidateResponse(BaseResponse response) {
            if (!response.Success)
                throw new ManagedException(response.Message);
        }

        protected string GetQueryString(Dictionary<string, string> queryParams) {
            if (!queryParams.Any()) return "";

            var queryParamString = queryParams.Select(o => $"{o.Key}={o.Value}").ToList();
            return $"?{string.Join('&', queryParamString)}";
        }
    }
}