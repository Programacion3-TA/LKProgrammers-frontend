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
            servicio = new LKServicioWebClient();
            int id_cursoP = int.Parse(Request.QueryString["cursoId"]);
            string nombre_s = Request.QueryString["cursoNombre"];
            paginaCurso pag = servicio.pagina_init(id_cursoP);
            PageTitle.Text = nombre_s;
        }
    }
}