using System;
using Microsoft.Extensions.Logging;

namespace Todo.Common.Models
{
    public class RequestResponseLog
    {
        public long Id { get; set; }
        public string RequestPath { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public string RequestHost { get; set; }
        public string RequestQueryString { get; set; }
        
        public int ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; }

        public string LogRequest()
        {
            return $"";
        }

        public string LogResponse()
        {
            return $"";
        }
    }
}
