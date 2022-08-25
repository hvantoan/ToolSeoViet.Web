using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System;

namespace ToolSeoViet.Web.Shared.Components {
    public partial class DateNullPicker {
        [Parameter] public string Label { get; set; }
        [Parameter] public DateTimeOffset? Value { get; set; }
        [Parameter] public EventCallback<DateTimeOffset?> ValueChanged { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool ReadOnly { get; set; }
        [Parameter] public TimeSpan FixTime { get; set; } = TimeSpan.Zero;

        private DateTime? date;

        protected override void OnInitialized() {
            this.date = this.Value?.ToLocalTime().Date;
            StateHasChanged();
        }

        private async Task DateChanged(DateTime? value) {
            this.Value = value.HasValue ? new DateTimeOffset(value.Value.Date).Add(this.FixTime) : null;
            await this.ValueChanged.InvokeAsync(this.Value);
            this.date = this.Value?.ToLocalTime().Date;
        }
    }
}
