using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace ToolSeoViet.Web.Shared.Components {
    public partial class DateTimePicker {
        [Parameter] public string Label { get; set; }
        [Parameter] public DateTimeOffset? Value { get; set; }
        [Parameter] public EventCallback<DateTimeOffset?> ValueChanged { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool ReadOnly { get; set; }
        [Parameter] public bool NotNull { get; set; }

        private MudDatePicker picker;

        private DateTime? date;
        private int hour = 0;
        private int minute = 0;

        protected override void OnParametersSet() {
            this.date = (this.Value ?? DateTimeOffset.UtcNow).ToLocalTime().DateTime;
            this.hour = this.date?.Hour ?? 0;
            this.minute = this.date?.Minute ?? 0;
            StateHasChanged();
        }

        private async Task OnClean() {
            this.picker.Close();
            if (this.ReadOnly) return;

            this.Value = null;
            await SetValue();
        }

        private async Task OnClick() {
            this.picker.Close();
            if (this.ReadOnly) return;

            if (this.date == null) {
                this.Value = null;
            } else {
                this.Value = new DateTimeOffset(this.date.Value.Year, this.date.Value.Month, this.date.Value.Day, this.hour, this.minute, 0, DateTimeOffset.Now.Offset);
            }

            await SetValue();
        }

        private async Task SetValue() {
            await this.ValueChanged.InvokeAsync(this.Value);
            this.date = this.Value?.ToLocalTime().DateTime;
            this.hour = this.date?.Hour ?? 0;
            this.minute = this.date?.Minute ?? 0;
            StateHasChanged();
        }
    }
}