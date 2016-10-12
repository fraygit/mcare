using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace mcare.API.Helpers
{
    public static class HttpRequestMessageExtensions
    {
        public static Dictionary<string, string> GetQueryStrings(
            this HttpRequestMessage request)
        {
            return request.GetQueryNameValuePairs()
                          .ToDictionary(kv => kv.Key, kv => kv.Value,
                               StringComparer.OrdinalIgnoreCase);
        }

    }
}