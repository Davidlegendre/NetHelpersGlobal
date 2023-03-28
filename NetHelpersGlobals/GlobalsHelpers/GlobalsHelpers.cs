using Helpers.Errores;
using Helpers.GlobalsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.GlobalsHelpers
{
    internal class GlobalsHelpers
    {
        public static bool IsEqualsToHeaderToList<T>(List<Headers> headers, List<T> lista) where T : class
        {
            return SandBox.Exec(() => {
                bool result = true;
                foreach(var header in headers)
                {
                    if (lista.GetType().GetProperty(header.Name) == null)
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            });
        }
    }
}
