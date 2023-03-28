using Helpers.Errores;
using Helpers.Red.Helpers;
using Helpers.Red.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Red
{
    public class FetchPost<T> where T : class
    {
        public static async Task<RetornoSandbox> PostAsync(string BaseURL, T Obj, List<T> Objs = null,
            TypeParams typeParams = TypeParams.None, List<ParametrosModel> parametros = null)
        {
            return await SandBox.ExecAsync(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                   BaseURL = RedHelpers.PrepareParametrosURL(BaseURL, typeParams, parametros);
                    
                    var result = await client.PostAsync(BaseURL, 
                        new StringContent((Objs != null) 
                            ? JsonConvert.SerializeObject(Objs)
                            : JsonConvert.SerializeObject(Obj), Encoding.UTF8, "application/json"));

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
