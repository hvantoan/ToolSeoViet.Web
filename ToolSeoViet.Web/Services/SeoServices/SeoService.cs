using Blazored.LocalStorage;
using System.Threading.Tasks;
using ToolSeoViet.Web.Models;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Services.Common;

namespace ToolSeoViet.Web.Services.SeoServices
{
    public class SeoService : BaseService
    {
        public SeoService(HttpService httpService) : base(httpService){ }
        public async Task<CheckIndexDto> CheckIndex(CheckIndexRequest checkIndexRequest)
        {
            var response = await this.httpService.PostAsync<BaseResponse<CheckIndexDto>>("/api/seo/index", checkIndexRequest);
            ValidateResponse(response);
            return response.Data;
        }
    }
}

