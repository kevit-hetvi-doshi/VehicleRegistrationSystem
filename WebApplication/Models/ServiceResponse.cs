using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public Boolean Success { get; set; } = true;

        public String message { get; set; } = null;
    }
}