using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Common.helpers
{
    public class ApiResponse
    {
        public ApiResponse() { }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public virtual IList<T> Data { get; set; }
    }
    public class ApiPostResponse<T> : ApiResponse
    {
        public virtual T Data { get; set; }
    }
    public class Response : ApiResponse 
    {
        public long TAID { get; set; }
    }
}
