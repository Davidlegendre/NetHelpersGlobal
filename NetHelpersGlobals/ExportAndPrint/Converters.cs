using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Helpers.ExportAndPrint
{
    public class Converters
    {
        public void ExportToExcel<T>(List<T> Data,string OutFileExcel) where T : class
        {
            SLDocument oSLDocument = new SLDocument();
            
            System.Data.DataTable dt = new System.Data.DataTable();

            var propiedades = Data.Select(x => x.GetType().GetProperties());
            //columnas
            foreach(var h in propiedades.First())
            {
                //columna   
                dt.Columns.Add(h.Name);
            }

            foreach (var d in Data)
            {
                var obj = new List<object>();
                foreach (var dd in d.GetType().GetProperties())
                { 
                    var value = dd.GetValue(d);
                    obj.Add(value);
                }
                dt.Rows.Add(obj.ToArray());
            }
            

            oSLDocument.ImportDataTable(1, 1, dt, true);
            SLStyle style4 = oSLDocument.CreateStyle();
            style4.Border.LeftBorder.BorderStyle = BorderStyleValues.Double;
            style4.Border.LeftBorder.SetBorderThemeColor(SLThemeColorIndexValues.Accent5Color);
            style4.Border.RightBorder.BorderStyle = BorderStyleValues.Double;
            style4.Border.RightBorder.SetBorderThemeColor(SLThemeColorIndexValues.Accent5Color);

            oSLDocument.SetColumnStyle(1, propiedades.First().Count(), style4);
            oSLDocument.SaveAs(OutFileExcel);
        }
    }
}
