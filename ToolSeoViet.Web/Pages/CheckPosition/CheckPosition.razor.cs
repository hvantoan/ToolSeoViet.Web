using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Models.Seo.GetProject;
using ToolSeoViet.Web.Services.ProjectService;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.CheckPosition {
    public partial class CheckPosition {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public ProjectService ProjectService { get; set; }
        [Inject] public SeoService SeoService { get; set; }
        [Inject] public IDialogService DialogService { get; set; }

        private ProjectDto item = new();

        private HashSet<ProjectDetailDto> selectedProjectDetails = new HashSet<ProjectDetailDto>();

        private bool success = false;
        private bool loading = false;
        private bool enable = false;
        private bool isAddProject = false;

        private string key = string.Empty;

        public void GetDomains(MouseEventArgs args) {
            this.key = this.key?.Trim() ?? "";
            if (!string.IsNullOrEmpty(item.Domain) && !string.IsNullOrEmpty(this.key)) {
                var keywords = key.Split("\n").Distinct().ToList();
                if (keywords.Any()) {
                    int max = 0;
                    if (item.ProjectDetails.Any()) max = item.ProjectDetails.Max(o => o.Ordinal);
                    for (int i = 0; i < keywords.Count; i++) {
                        if (this.item.ProjectDetails.Any(o => o.Key == keywords[i]) || string.IsNullOrEmpty(keywords[i].Trim())) continue;
                        this.item.ProjectDetails.Add(new ProjectDetailDto() {
                            Key = keywords[i],
                            Ordinal = ++max,
                            CurrentPosition = 0,
                            BestPosition = 0,
                        });
                        success = true;
                    }
                }
            }
        }

        private void AddNew(MouseEventArgs args) {
            try {
                this.loading = true;
                StateHasChanged();
                this.Snackbar.Add("Thêm dự án thành công", Severity.Success);
                this.item = new();
                this.key = string.Empty;
            } catch (ManagedException e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } finally {
                this.loading = false;
                this.isAddProject = false;
                StateHasChanged();
            }
        }

        private async Task Delete(MouseEventArgs args) {
            try {
                this.loading = true;
                StateHasChanged();
                await this.ProjectService.Delete(this.item.Id);
                this.Snackbar.Add($"Xóa dự án {this.item.Name} thành công", Severity.Success);
            } catch (ManagedException e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } catch (Exception e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } finally {
                this.loading = false;
                this.item = new();
                StateHasChanged();
            }
        }

        private async Task Search(MouseEventArgs args) {
            try {
                this.loading = true;
                StateHasChanged();
                var projectDetails = selectedProjectDetails.ToList();
                for (int index = 0; index < projectDetails.Count; index++) {
                    var data = await this.SeoService.SearchPosition(new SearchPositionRequest { Domain = this.item.Domain, ProjectDetail = projectDetails[index]});
                    
                    var projectDetail = this.item.ProjectDetails.FirstOrDefault(o => o.Id == projectDetails[index].Id);
                    if (projectDetail == null) continue;

                    projectDetail.Name = data.Name;
                    projectDetail.CurrentPosition = data.CurrentPosition;
                    projectDetail.BestPosition = data.BestPosition;
                    projectDetail.Url = data.Url;

                    StateHasChanged();
                }

            } catch (ManagedException e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } catch (Exception e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } finally {
                this.loading = false;
                StateHasChanged();
            }
        }

        public async Task OpenDialog(MouseEventArgs args) {
            var result = await this.DialogService.Show<Dialog>("").Result;
            if (result.Cancelled) return;
            await LoadData(result.Data?.ToString() ?? "");

        }

        private async Task LoadData(string id) {
            try {
                this.loading = true;
                StateHasChanged();
                this.item = await this.ProjectService.Get(id);

            } catch (ManagedException e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } catch (Exception e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } finally {
                this.loading = false;
                StateHasChanged();
            }
        }
    }
}
