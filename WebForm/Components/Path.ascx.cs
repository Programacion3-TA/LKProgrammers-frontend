using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Components
{
    public partial class Path : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string TiposURL { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            List<string> Tipos = TiposURL.Split('/').ToList();

            Dictionary<string, string> mapaTipoClase = new Dictionary<string, string> {
                {"Curso", "fa-book" },
                {"Nota", "fa-star" },
                {"Asistencia", "fa-user"},
                { "Calendario", "fa-calendar"},
                { "Competencia", "fa-newspaper"}
            };

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "fa-solid fa-house fa-5xs me-2");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "#000000");
            writer.Write("");
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("");
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("Principal");
            writer.RenderEndTag();

            if (TiposURL.CompareTo("") == 0) return;

            foreach(string tipo in Tipos)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write("/");
                writer.RenderEndTag();

                string seleccion = mapaTipoClase[tipo];
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "fa-solid " + seleccion + " fa-5xs me-2");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "#000000");
                writer.Write("");
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write("");
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(tipo);
                writer.RenderEndTag();
            }
        }
    }
}