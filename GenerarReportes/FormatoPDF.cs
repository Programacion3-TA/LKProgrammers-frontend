using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winnovative;

namespace GenerarReportes
{
    public class FormatoPDF
    {
        public static byte[] GenerarBytes(String titulo, String[] cabeceras, String[][] cuerpo)
        {
            String contenido = $"<html><body><h1>{titulo}</h1><table><tr>";
            foreach(String cab in cabeceras)
            {
                contenido += $"<th>{cab}</th>";
            }
            contenido += $"</tr>";
            foreach (String[] fila in cuerpo)
            {
                contenido += "<tr>";
                foreach (String data in fila)
                {
                    contenido += $"<td>{data}</td>";
                }
                contenido += "</tr>";
            }
            contenido += "</table></body></html>";
            PdfConverter pdfConverter = new PdfConverter();
            byte[] pdfBytes = pdfConverter.GetPdfBytesFromHtmlString(contenido);
            return pdfBytes;
        }
    }
}
