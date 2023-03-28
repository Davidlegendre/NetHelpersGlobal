using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Red.Models
{
    public class RetornoSandbox
    {
        public bool IsServerError { get; set; }
        public string ErrorMessage { get; set; }

        public HttpStatusCode HttpResponse { get; set; }

        public object JsonResult { get; set; }

    }
}
