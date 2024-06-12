using iTextSharp.tool.xml.html.head;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using WebForm.Utils;
using WebForm.ServicioWS;

namespace WebForm.View.CursoAlumno
{
    public partial class CursoAlumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //String html = MyReact.createComponent("h1", null, "Hola mundo");
            //MyCont.Text = html;

            paginaCurso pag = new paginaCurso();
            pag.id = 1;
            pag.secciones = new seccion[]{
                new seccion(){ id=1, titulo="Semana 1", orden=1, elementos=new elemento[]{
                    new heading(){ id=1, contenido="Operaciones basicas", orden=1, nivel=1, tipoElemento=tipoElemento.Heading},
                    new parrafo(){ id=2, contenido="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi ac placerat diam. Sed posuere turpis eget ante venenatis, elementum vestibulum velit eleifend. Donec fermentum, lorem quis blandit iaculis, sem risus hendrerit dui, at posuere velit urna quis diam. Nunc vitae suscipit turpis, eu porta nunc. Duis id ex non lorem auctor finibus. Ut venenatis eu tortor sed interdum. Vestibulum tristique nisi sed diam fringilla iaculis. Aenean tellus ligula, scelerisque at iaculis id, dictum vel quam", orden=2, tipoElemento=tipoElemento.Parrafo},
                    new parrafo(){ id=3, contenido="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi ac placerat diam. Sed posuere turpis eget ante venenatis, elementum vestibulum velit eleifend. Donec fermentum, lorem quis blandit iaculis, sem risus hendrerit dui, at posuere velit urna quis diam. Nunc vitae suscipit turpis, eu porta nunc. Duis id ex non lorem auctor finibus. Ut venenatis eu tortor sed interdum. Vestibulum tristique nisi sed diam fringilla iaculis. Aenean tellus ligula, scelerisque at iaculis id, dictum vel quam", orden=3, tipoElemento=tipoElemento.Parrafo},
                    new enlace(){ id=4, contenido="Enlace a google", orden=4, href="https://google.com", tipoElemento=tipoElemento.Enlace},
                    new imagen(){ id=5, contenido="", orden=5, source="https://media.giphy.com/media/LXONhtCmN32YU/giphy.gif", width=100, height=100, tipoElemento=tipoElemento.Imagen},
                    new parrafo(){ id=6, contenido="Fuiste rickrolleado", orden=6, tipoElemento=tipoElemento.Parrafo}
                } },
                new seccion(){ id=2, titulo="Semana 2", orden=2 },
                new seccion(){ id=3, titulo="Semana 3", orden=3 },
                new seccion(){ id=4, titulo="Semana 4", orden=4 }
            };

            String badgesComp = "";
            List<String> badgesData = new List<String>
            {
                "Reconocimiento de números: Identificar y escribir números del 1 al 100.",
                "Secuencia numérica: Contar hacia adelante y hacia atrás desde cualquier número dentro del rango de 1 a 100.",
                "Comparación de números: Comprender los conceptos de mayor que(>), menor que(<) e igual que(=) en relación con números hasta el 100.",
                "Operaciones básicas: Suma y resta dentro del rango de 0 a 20, utilizando objetos físicos, imágenes o mentalmente.",
                "Resolución de problemas: Entender y resolver problemas matemáticos simples que impliquen sumar y restar cantidades pequeñas.",
                "Formas y figuras geométricas: Identificar y nombrar formas geométricas básicas como círculos, cuadrados, triángulos y rectángulos.",
                "Medición: Comprender conceptos de tamaño y longitud utilizando términos como grande, pequeño, largo y corto. También introducir unidades de medida básicas como centímetros y metros.",
                "Patrones: Identificar y crear patrones simples utilizando formas, colores o números.",
                "Organización de datos: Clasificar y organizar objetos en categorías simples según atributos como forma, tamaño o color.",
                "Conceptos de tiempo: Entender conceptos básicos de tiempo, como el día, la noche, el hoy, el ayer y el mañana.También empezar a leer y usar un reloj analógico básico para decir la hora en horas en punto y medias horas."
            };

            foreach (String competencia in badgesData)
            {
                badgesComp += MyReact.createComponent("span", new Dictionary<string, string> { { "class", "badge rounded-pill text-bg-primary p-2 text-truncate" }, { "style", "max-width: 60%;" } }, competencia);
            }
            BadgesContainer.Text = badgesComp;
            renderizarSecciones(pag);
        }

        protected void renderizarSecciones(paginaCurso pag)
        {
            String seccComp = "";
            foreach (seccion secc in pag.secciones)
            {
                String seccHtmlId = $"secc{secc.id}";
                seccComp += MyReact.createComponent(
                    "div", "class=\"accordion-item\"",
                        MyReact.createComponent("h2", "class=\"accordion-header\"",
                            MyReact.createComponent("button", $"class=\"accordion-button collapsed\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#{seccHtmlId}\" aria-expanded=\"false\" aria-controls=\"{seccHtmlId}\"", secc.titulo)
                        ) +
                        MyReact.createComponent("div", $"id=\"{seccHtmlId}\" class=\"accordion-collapse collapse\" data-bs-parent=\"#accordionFlushExample\"",
                            MyReact.createComponent("div", "class=\"accordion-body\"", secc.elementos == null? "" : secc.elementos.Aggregate("",
                                (concatenacion, elem) => concatenacion +
                                    MyReact.createComponentByType(elem, "", elem.contenido)
                                )
                            )
                        )
                );
            }

            SeccionesContainer.Text = seccComp;
        }
    }
}