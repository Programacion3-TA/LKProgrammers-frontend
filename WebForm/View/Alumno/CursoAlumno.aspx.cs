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

        private int codigo_curso;
        private LKServicioWebClient serviciodao;
        private paginaCurso pagina;
        protected void Page_Load(object sender, EventArgs e)
        {
            //String html = MyReact.createComponent("h1", null, "Hola mundo");
            //MyCont.Text = html;
            serviciodao = new LKServicioWebClient();
            codigo_curso = (int)Session["CURSO"];
            PageTitle.Text = (string)Session["Curname"];
            pagina = serviciodao.pagina_init(codigo_curso);
            //insertar funcion para obtener el codigo del curso al que el usuario hizo click
            var pag = serviciodao.listar_CONTENIDOS(codigo_curso);
            List<String> badgesData = new List<String>();
            String badgesComp = "";
            if (pag != null) {
                pagina.secciones = pag;
                foreach (seccion seccion in pagina.secciones){
                    badgesData.Add(seccion.titulo);
                    badgesComp += MyReact.CreateComponent("span", new Dictionary<string, string> { { "class", "badge rounded-pill text-bg-primary p-2 text-truncate" }, { "style", "max-width: 60%;" } }, seccion.titulo);
                }
                BadgesContainer.Text = badgesComp;
                renderizarSecciones(pagina);
            }
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
}