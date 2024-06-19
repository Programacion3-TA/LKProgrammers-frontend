using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.NotaProfesor
{   
    
    public partial class NotaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack)
            {
                cursoHorario[] cursosHorario = daoServicio.listarCursosPorProfesor(((profesor)Session["Usuario"]).dni);
                CargarCursos(cursosHorario);
            }
        }

        protected void CargarCursos(cursoHorario[] cursoHorarios)
        {
            if(cursoHorarios != null)
            {
                List<cursoHorario> cursoHorarioList = cursoHorarios.ToList();   
                foreach(cursoHorario cur in cursoHorarioList)
                {
                    
                }
            }
        }
    }
}