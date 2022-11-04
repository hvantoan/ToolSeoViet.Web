using Blazored.LocalStorage;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolSeoViet.Web.Models;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Models.Seo.GetContent;
using ToolSeoViet.Web.Models.Seo.GetProject;
using ToolSeoViet.Web.Services.Common;
using ToolSeoViet.Web.Services.Project;

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

        public async Task<SearchContentDto> GetContent(GetContentRequest getContentRequest)
        {
            var response = await this.httpService.PostAsync<BaseResponse<SearchContentDto>>("/api/seo/content", getContentRequest);
            ValidateResponse(response);
            return response.Data;
        }

        public async Task<ProjectDetailDto> SearchPosition(SearchPositionRequest request)
        {
            var response = await this.httpService.PostAsync<BaseResponse<ProjectDetailDto>>("/api/seo/position", request);
            ValidateResponse(response);
            return response.Data;
        }
        public async Task<List<ProjectDto>> All()
        {
            var response = await this.httpService.GetAsync<BaseResponse<ListProjectResponse>>("/api/project/all");
            ValidateResponse(response);
            return response.Data?.Items ?? default;
        }
    }
}

