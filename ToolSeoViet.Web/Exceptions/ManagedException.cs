using MudBlazor;
using System;
using System.Runtime.Serialization;

namespace ToolSeoViet.Web.Exceptions {

    [Serializable]
    public class ManagedException : Exception {
        public Severity ErrorLevel { get; }

        public ManagedException() {
        }

        public ManagedException(string message, Severity errorLevel = Severity.Error) : base(message) {
            this.ErrorLevel = errorLevel;
        }

        public ManagedException(string message, Exception innerException) : base(message, innerException) {
        }

        protected ManagedException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}