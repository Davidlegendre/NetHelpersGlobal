using Helpers.Red.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Red.Helpers
{
    internal static class RedHelpers
    {
        internal static string ConvertToQuery(List<ParametrosModel> parametros) {
            string result = "?";
            foreach (var param in parametros)
            {
                //?param=value&param=value
                result += param.Name + "=" + param.Value + "&";
            }
            result.Remove(result.Length - 1);   
            return result;  
        }

        internal static string ConvertToRoute(List<ParametrosModel> parametros)
        {
            string result = "";
            foreach (var param in parametros)
            {
                ///value/value/vlau
                result += "/" + param.Value;
            }
            return result;
        }

        internal static async Task<RetornoSandbox> GetRetornoSandbox(HttpResponseMessage result)
        {
            if (!result.IsSuccessStatusCode)
            {
                return new RetornoSandbox()
                {
                    IsServerError = true,
                    ErrorMessage = result.ReasonPhrase,
                    HttpResponse = result.StatusCode,
                    JsonResult = null
                };
            }
            var content = await result.Content.ReadAsStringAsync();
            return new RetornoSandbox()
            {
                IsServerError = false,
                ErrorMessage = "",
                HttpResponse = result.StatusCode,
                JsonResult = content
            };
        }

        internal static string PrepareParametrosURL(string BaseURL, TypeParams typeParams, List<ParametrosModel> parametros)
        {
            BaseURL = (BaseURL.EndsWith("/") || BaseURL.EndsWith("?") ? BaseURL.Remove(BaseURL.Length - 1) : BaseURL);
            switch (typeParams)
            {
                case TypeParams.Query:
                    BaseURL += RedHelpers.ConvertToQuery(parametros);
                    break;
                case TypeParams.Route:
                    BaseURL += RedHelpers.ConvertToRoute(parametros);
                    break;
                default:
                    break;
            }
            return BaseURL;
        }
    }
    public enum TypeParams
    {
        Query, Route, None
    }
}
