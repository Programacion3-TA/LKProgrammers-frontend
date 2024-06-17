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

        public static String CreateComponentByType(elemento elem, String props, String children)
        {
            String html = "";

            switch (elem.tipoElemento)
            {
                case tipoElemento.Heading:
                    html = CreateComponent($"h{((heading)elem).nivel}", props, children);
                    break;
                case tipoElemento.Parrafo:
                    html = CreateComponent($"p", props, children);
                    break;
                case tipoElemento.Enlace:
                    html = CreateComponent($"a", $"{props} href=\"{( (enlace)elem ).href}\"", children);
                    break;
                case tipoElemento.Imagen:
                    html = CreateComponent($"img", $"{props} src=\"{((imagen)elem).source}\"", children);
                    break;
            }

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