using System;

namespace Todo.Common.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int StatusCode { get; set; }
        public Exception Exception { get; set; }
        public string Path { get; set; }
    }
}