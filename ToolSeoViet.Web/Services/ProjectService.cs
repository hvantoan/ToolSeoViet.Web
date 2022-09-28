using System.Collections.Generic;
using System.Threading.Tasks;
using ToolSeoViet.Web.Models;
using ToolSeoViet.Web.Models.Project;
using ToolSeoViet.Web.Models.Seo.GetProject;
using ToolSeoViet.Web.Services.Common;
using ToolSeoViet.Web.Services.Project;

namespace ToolSeoViet.Web.Services.ProjectService {
    public class ProjectService : BaseService {
        public ProjectService(HttpService httpService) : base(httpService) { }

        public async Task<List<ProjectDto>> All() {
            var response = await this.httpService.GetAsync<BaseResponse<ListProjectResponse>>("/api/project/all");
            ValidateResponse(response);
            return response.Data?.Items ?? default;
        }

        public async Task<ProjectDto> Get(string id) {
            var request = new GetProjectRequest() { Id = id };
            var response = await this.httpService.PostAsync<BaseResponse<ProjectDto>>("/api/project/get", request);
            ValidateResponse(response);
            return response.Data;
        }

        public async Task Save(ProjectDto projectDto) {
            await this.httpService.PostAsync<BaseResponse<ProjectDto>>("/api/project/save", projectDto);
        }

        public async Task Delete(string id) {
            var request = new DeleteProjectRequest() { Id = id };
            await this.httpService.PostAsync<BaseResponse<ProjectDto>>("/api/project/Delete", request);
        }
    }
}

