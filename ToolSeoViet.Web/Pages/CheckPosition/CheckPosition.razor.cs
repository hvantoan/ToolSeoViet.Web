﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Models.Seo.GetProject;
using ToolSeoViet.Web.Models.Seo.SearchPositon;
using ToolSeoViet.Web.Services.ProjectService;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.CheckPosition {
    public partial class CheckPosition {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public ProjectService ProjectService { get; set; }
        [Inject] public SeoService SeoService { get; set; }
        [Inject] public IDialogService DialogService { get; set; }

        public ProjectDto item = new();

        private HashSet<ProjectDetailDto> selectedProjectDetails = new HashSet<ProjectDetailDto>();

        private bool success = false;
        private bool loading = false;
        private bool enable = false;
        private bool isAddProject = false;
        //two ref input
        private string key = "";

        public void GetDomains(MouseEventArgs args) {
            this.key = this.key?.Trim() ?? "";
            if (!string.IsNullOrEmpty(item.Domain) && !string.IsNullOrEmpty(this.key)) {
                var keywords = key.Split("\n").Distinct().ToList();
                if (keywords.Any()) { 
                    for (int i = 0; i < keywords.Count; i++) {
                        if (this.item.ProjectDetails.Any(o => o.Url == keywords[i]) || string.IsNullOrEmpty(keywords[i].Trim())) continue;
                        this.item.ProjectDetails.Add(new ProjectDetailDto() {
                            Key = keywords[i],
                            CurrentPosition = 0,
                            BestPosition = 0,
                        });
                        success = true;
                    }
                }
            }
            var index = 0;
            foreach (var projectDetail in this.item?.ProjectDetails) {
                index++;
                projectDetail.Stt = index;
            }
        }
        
        private async Task AddNew(MouseEventArgs args) {
            try {
                this.loading = true;
                StateHasChanged();
                await ProjectService.Save(this.item);
                this.Snackbar.Add("Thêm dự án thành công", Severity.Success);
                this.item = new();
                this.key = "";
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
        
        private async Task SendKeyword(MouseEventArgs args) {
            try {
                this.loading = true;
                StateHasChanged();
                if (string.IsNullOrEmpty(this.key.Trim()) || string.IsNullOrEmpty(this.item.Domain?.Trim() ?? ""))
                    throw new ManagedException("Từ khóa hoặc domain không được để trống");
                for (int index = 0; index < item.ProjectDetails.Count; index++) {

                    var data = await this.SeoService.SearchPosition(new SearchIndexRequest() {
                        Domain = this.item.ProjectDetails[index].Url,
                        Key = this.item.ProjectDetails[index].Name
                    });

                    this.item.ProjectDetails[index] = data ?? this.item.ProjectDetails[index];
                    this.item.ProjectDetails[index].Stt = index + 1;
                    StateHasChanged();
                    await Task.Delay(500);
                }

            } catch (System.Exception ex) {
                throw new ManagedException(ex.ToString());
            } finally {
                this.loading = false;
                StateHasChanged();
            }
        }
        
        public async Task OpenDialog(MouseEventArgs args) {
            var result = await this.DialogService.Show<Dialog>("").Result;
            if (result.Cancelled) return;

            try {
                this.loading = true;
                StateHasChanged();
                this.item = await this.ProjectService.Get(result.Data.ToString());
                var index = 0;
                foreach (var projectDetail in this.item?.ProjectDetails) {
                    index++;
                    projectDetail.Stt = index;
                }

            } catch (ManagedException e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } finally {
                this.loading = false;
                StateHasChanged();
            }
        }
    }
}
