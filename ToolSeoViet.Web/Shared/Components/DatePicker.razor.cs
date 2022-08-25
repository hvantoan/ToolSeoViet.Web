using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ToolSeoViet.Web.Shared.Components {
    public partial class DatePicker {
        [Parameter] public string Label { get; set; }
        [Parameter] public DateTimeOffset Value { get; set; }
        [Parameter] public EventCallback<DateTimeOffset> ValueChanged { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool ReadOnly { get; set; }
        [Parameter] public TimeSpan FixTime { get; set; } = TimeSpan.Zero;

        private DateTime? date;

        protected override void OnInitialized() {
            this.date = this.Value.ToLocalTime().Date;
            StateHasChanged();
        }

        private async Task DateChanged(DateTime? value) {
            this.Value = new DateTimeOffset(value.Value.Date).Add(this.FixTime);
            await this.ValueChanged.InvokeAsync(this.Value);
            this.date = this.Value.ToLocalTime().Date;
        }
    }
}