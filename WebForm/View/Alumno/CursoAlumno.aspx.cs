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
        private List<seccionCURSO> paginaCurso;

        protected void Page_Load(object sender, EventArgs e)
        {
            //String html = MyReact.createComponent("h1", null, "Hola mundo");
            //MyCont.Text = html;
            serviciodao = new LKServicioWebClient();
            codigo_curso = 1; //insertar funcion para obtener el codigo del curso al que el usuario hizo click
            var pag = serviciodao.listar_CONTENIDOS(codigo_curso);
            List<String> badgesData = new List<String>();
            String badgesComp = "";
            paginaCurso = new List<seccionCURSO>();
            if (pag != null) { 
                paginaCurso = pag.ToList();
                foreach (seccionCURSO section in paginaCurso){
                    String RAND = section.nombre_SECCION;
                    badgesData.Add(RAND);
                    badgesComp += MyReact.CreateComponent("span", new Dictionary<string, string> { { "class", "badge rounded-pill text-bg-primary p-2 text-truncate" }, { "style", "max-width: 60%;" } }, RAND);
                } //asigna nombres de seccion (titulo de contenidos para mostrar)
                BadgesContainer.Text = badgesComp;
                renderizarSecciones();
            }
        }

        protected void renderizarSecciones()
        {
            String seccComp = "";
            foreach (seccionCURSO secc in paginaCurso)
            {
                String seccHtmlId = $"secc{secc.id_curso}";
                seccComp += MyReact.CreateComponent(
                    "div", "class=\"accordion-item\"",
                        MyReact.CreateComponent("h2", "class=\"accordion-header\"",
                            MyReact.CreateComponent("button", $"class=\"accordion-button collapsed\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#{seccHtmlId}\" aria-expanded=\"false\" aria-controls=\"{seccHtmlId}\"", secc.nombre_SECCION)
                        ) +
                        MyReact.CreateComponent("div", $"id=\"{seccHtmlId}\" class=\"accordion-collapse collapse\" data-bs-parent=\"#accordionFlushExample\"",
                            MyReact.CreateComponent("div", "class=\"accordion-body\"", secc.CONTENIDOS_DE_SECCION == null? "" : secc.CONTENIDOS_DE_SECCION.Aggregate("",
                                (concatenacion, elem) => concatenacion +
                                    MyReact.CreateComponentByType(elem, "", elem.ID_CONTENIDO)
                                )
                            )
                        )
                );
            }

        //    SeccionesContainer.Text = seccComp;
        //}
    }
}