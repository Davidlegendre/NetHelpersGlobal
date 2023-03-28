using Helpers.Errores;
using Helpers.Red.Helpers;
using Helpers.Red.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Red.Get
{
    public class FetchGet
    {
        public static async Task<RetornoSandbox> GetAsync(string BaseURL, TypeParams typeParams = TypeParams.None, List<ParametrosModel> parametros = null)
        {
            return await SandBox.ExecAsync(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    BaseURL = RedHelpers.PrepareParametrosURL(BaseURL, typeParams, parametros); 
                    var result = await client.GetAsync(BaseURL);

                    return await RedHelpers.GetRetornoSandbox(result);
                }
            }, (ex) =>
            {
                return new RetornoSandbox()
                {
                    IsServerError = true,
                    ErrorMessage = ex.Message,
                    JsonResult = null,
                    HttpResponse = System.Net.HttpStatusCode.ExpectationFailed
                };
            });
        }        
    }
}
