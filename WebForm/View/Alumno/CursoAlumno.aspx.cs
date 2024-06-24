using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebForm.ServicioWS;
using System.Net;
using WebForm.Utils;

namespace WebForm.View.CursoAlumno
{
    public partial class CursoAlumno : System.Web.UI.Page
    {

        private int codigo_curso;
        private LKServicioWebClient serviciodao;
        protected void Page_Load(object sender, EventArgs e)
        {
            //String html = MyReact.createComponent("h1", null, "Hola mundo");
            //MyCont.Text = html;


            serviciodao = new LKServicioWebClient();
            paginaCurso pag = new paginaCurso();
            //pag.id = 1;
            //pag.secciones = new seccion[]{
            //    new seccion(){ id=1, titulo="Semana 1", orden=1, elementos=new elemento[]{
            //        new heading(){ id=1, contenido="Operaciones basicas", orden=1, nivel=1, tipoElemento=tipoElemento.Heading},
            //        new parrafo(){ id=2, contenido="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi ac placerat diam. Sed posuere turpis eget ante venenatis, elementum vestibulum velit eleifend. Donec fermentum, lorem quis blandit iaculis, sem risus hendrerit dui, at posuere velit urna quis diam. Nunc vitae suscipit turpis, eu porta nunc. Duis id ex non lorem auctor finibus. Ut venenatis eu tortor sed interdum. Vestibulum tristique nisi sed diam fringilla iaculis. Aenean tellus ligula, scelerisque at iaculis id, dictum vel quam", orden=2, tipoElemento=tipoElemento.Parrafo},
            //        new parrafo(){ id=3, contenido="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi ac placerat diam. Sed posuere turpis eget ante venenatis, elementum vestibulum velit eleifend. Donec fermentum, lorem quis blandit iaculis, sem risus hendrerit dui, at posuere velit urna quis diam. Nunc vitae suscipit turpis, eu porta nunc. Duis id ex non lorem auctor finibus. Ut venenatis eu tortor sed interdum. Vestibulum tristique nisi sed diam fringilla iaculis. Aenean tellus ligula, scelerisque at iaculis id, dictum vel quam", orden=3, tipoElemento=tipoElemento.Parrafo},
            //        new enlace(){ id=4, contenido="Enlace a google", orden=4, href="https://google.com", tipoElemento=tipoElemento.Enlace},
            //        new imagen(){ id=5, contenido="", orden=5, tipoElemento=tipoElemento.Imagen},
            //        new parrafo(){ id=6, contenido="Fuiste rickrolleado", orden=6, tipoElemento=tipoElemento.Parrafo}
            //    } },
            //    new seccion(){ id=2, titulo="Semana 2", orden=2 },
            //    new seccion(){ id=3, titulo="Semana 3", orden=3 },
            //    new seccion(){ id=4, titulo="Semana 4", orden=4 }
            //};


            codigo_curso = (int)Session["CURSO"];
            PageTitle.Text = (string)Session["Curname"];
            //insertar funcion para obtener el codigo del curso al que el usuario hizo click
            pag = serviciodao.pagina_init(codigo_curso);
            pag.secciones = serviciodao.listar_CONTENIDOS(codigo_curso) ?? new seccion[] {};

            List<String> badgesData = new List<String>();
            String badgesComp = "";

            foreach (seccion seccion in pag.secciones){
                badgesData.Add(seccion.titulo);
                badgesComp += MyReact.CreateComponent("span", new Dictionary<string, string> { { "class", "badge rounded-pill text-bg-primary p-2 text-truncate" }, { "style", "max-width: 60%;" } }, seccion.titulo);
            }
            BadgesContainer.Text = badgesComp;
            renderizarSecciones(pag);

        }
       

        protected void renderizarSecciones(paginaCurso pagina)

        {

            String seccComp = "";
            foreach (seccion secc in pagina.secciones)
            {
                String seccHtmlId = $"secc{secc.id}";
                seccComp += MyReact.CreateComponent(
                    "div", "class=\"accordion-item\"",
                        MyReact.CreateComponent("h2", "class=\"accordion-header\"",
                            MyReact.CreateComponent("button", $"class=\"accordion-button collapsed\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#{seccHtmlId}\" aria-expanded=\"false\" aria-controls=\"{seccHtmlId}\"", secc.titulo)
                        ) +
                        MyReact.CreateComponent("div", $"id=\"{seccHtmlId}\" class=\"accordion-collapse collapse\" data-bs-parent=\"#accordionFlushExample\"",
                            MyReact.CreateComponent("div", "class=\"accordion-body\"", secc.elementos == null ? "" : secc.elementos.Aggregate("",
                                (concatenacion, elem) => concatenacion +
                                    MyReact.CreateComponentByType(elem, "", elem.contenido)
                                )
                            )
                        )
                );
            }
            SeccionesContainer.Text = seccComp;
        }


}