using Helpers.Errores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helpers.Errores
{
    public class SandBox
    {
        public static void Exec(Action accion, Action<Exception> OutExec = null)
        {
			try
			{
                accion.Invoke();
			}
			catch (Exception ex)
			{
                ExcepcionAction(ex, OutExec);
            }
        }

        public static T Exec<T>(Func<T> accion, Func<Exception, T> OutExec = null)
        {
            try { 
               return accion.Invoke();
            }
            catch (Exception ex)
            {
                ExcepcionFunc(ex, OutExec);
                return default(T);
            }
        }



        public static async Task ExecAsync(Action accion, Action<Exception> OutExec = null)
        {
            try
            {
                await Task.FromResult(accion);
            }
            catch (Exception ex)
            {
                ExcepcionAction(ex, OutExec);   
            }
        }

        public static async Task<T> ExecAsync<T>(Func<Task<T>> accion, Func<Exception, T> OutExec = null)
        {
            try
            {
                return await accion.Invoke();
            }
            catch (Exception ex)
            {
                ExcepcionFunc(ex, OutExec);
                return default(T);
            }
        }

        private static void ExcepcionFunc<T>(Exception ex, Func<Exception, T> OutExec = null)
        {
            if (OutExec != null)
                OutExec(ex);
            else
                MessageBox.Show(ex.Message);
        }
        private static void ExcepcionAction(Exception ex, Action<Exception> OutExec = null)
        {
            if (OutExec != null)
                OutExec(ex);
            else
                MessageBox.Show(ex.Message);
        }
    }
}
