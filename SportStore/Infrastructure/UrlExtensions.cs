using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest req)
        {
            var result = req.QueryString.HasValue ? $"{req.Path}{req.QueryString}" : req.Path.ToString();
            return result;
        }
    }
}
