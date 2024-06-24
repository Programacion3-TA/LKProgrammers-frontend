using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.CursoProfesor
{
    public partial class CursoProfesor : System.Web.UI.Page
    {
        private int id_cursoP;
        private string nombre_s;
        private LKServicioWebClient servicio;
        private paginaCurso pag;
        protected void Page_Load(object sender, EventArgs e)
        {
            id_cursoP = (int)Session["CURSO"];
            nombre_s = (string)Session["Curname"];
            pag = servicio.pagina_init(id_cursoP);
            PageTitle.Text = nombre_s;
        }
    }
}