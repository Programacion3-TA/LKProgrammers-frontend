using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebForm.ServicioWS;

namespace WebForm.Utils
{
    public class MyReact
    {
        public struct htmlElementStandar
        {
            public String tag;
            public String props;
        };

        public static Dictionary<tipoElemento, htmlElementStandar> standarTypes = new Dictionary<tipoElemento, htmlElementStandar>
        {
            { tipoElemento.Heading, new htmlElementStandar {tag="h{}", props= "" } },
            { tipoElemento.Parrafo, new htmlElementStandar { tag ="p", props= "" } },
            { tipoElemento.Enlace, new htmlElementStandar { tag ="a", props= "href={}" } },
            { tipoElemento.Imagen, new htmlElementStandar { tag ="img", props= "src={}" } }
        };

        public static String CreateComponent(String tag, Dictionary<String, String> props, String children)
        {
            String html = $"<{tag}";

            if(props != null)
            {
                foreach (KeyValuePair<String,String> entry in props)
                {
                    html += $" {entry.Key}=\"{entry.Value}\"";
                }
            }
            html += ">";
            html += $"{children}";
            html += $"</{tag}>";
            return html;
        }
        
        public static String CreateComponent(String tag, String props, String children)
        {
            String html = $"<{tag} {props}";
            html += ">";
            html += $"{children}";
            html += $"</{tag}>";
            return html;
        }

        public static string CreateComponentByType(elemento elem, string props, string children, bool editable = false)
        {
            string html = "", childHtml = "";

            switch (elem.tipoElemento)
            {
                case tipoElemento.Heading:
                    childHtml = CreateComponent($"h{((heading)elem).nivel}", props, children);
                    break;
                case tipoElemento.Parrafo:
                    childHtml = CreateComponent($"p", props, children);
                    break;
                case tipoElemento.Enlace:
                    childHtml = CreateComponent($"a", $"{props} href=\"{((enlace)elem).href}\"", children);
                    break;
                case tipoElemento.Imagen:
                    childHtml = CreateComponent($"img", $"{props}\"", children);
                    break;
            }

            if (editable)
            {
                childHtml += "" +
                    "<div class=\"dropdown\">" +
                    "   <button class=\"btn btn-secondary dropdown-toggle\" type=\"button\" data-bs-toggle=\"dropdown\" aria-expanded=\"false\">" +
                    "       Acciones" +
                    "   </button>" +
                    "   <ul class=\"dropdown-menu\">" +
                    "       <li><a class=\"dropdown-item\" href=\"#\">" +
                    "           <i class=\"fa fa-arrow-up\" aria-hidden=\"true\"></i> Mover arriba" +
                    "       </a></li>" +
                    "       <li><a class=\"dropdown-item\" href=\"#\">" +
                    "           <i class=\"fa fa-arrow-down\" aria-hidden=\"true\"></i> Mover abajo" +
                    "       </a></li>" +
                    "       <li><a class=\"dropdown-item\" href=\"#\">" +
                    "           <i class=\"fa fa-pencil\" aria-hidden=\"true\"></i> Editar" +
                    "       </a></li>" +
                    "       <li><a class=\"dropdown-item\" href=\"#\">" +
                    "           <i class=\"fa fa-trash\" aria-hidden=\"true\"></i> Eliminar" +
                    "       </a></li>" +
                    "   </ul>" +
                    "</div>";
            }
            html += CreateComponent("div", "class=\"d-flex justify-content-between\"", childHtml);

            return html;
        }

        //public static String createComponentsList(Dictionary<String, String> keys, String type, Dictionary<String, String> props, List<String> childrens)
        //{
        //    String html = "";
        //    int i = 0;
        //    foreach (KeyValuePair<String, String> entry in keys)
        //    {
        //        props[entry.Key] = entry.Value;
        //        html += MyReact.createComponent(type, props, childrens[i++]);
        //        props.Remove(entry.Key);
        //    }
        //    return html;
        //}

    }
}