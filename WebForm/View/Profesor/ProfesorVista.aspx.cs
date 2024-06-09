using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.ProfesorVista
{
    public partial class ProfesorVista : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            if (!IsPostBack)
            {
                
                cursoHorario[] cursosHorarios = daoServicio.listarCursosPorProfesor(((profesor)Session["Usuario"]).dni);
                MostrarCursos(cursosHorarios);
            }
        }

        private void MostrarCursos(cursoHorario[] cursoHorarios)
        {
            StringBuilder strBuild = new StringBuilder();
            string nombres = ((profesor)Session["Usuario"]).nombres + ((profesor)Session["Usuario"]).apellidoPaterno;

            foreach(cursoHorario cursoHor in cursoHorarios)
            {
                strBuild.Append("" +
                   $"<a href=\"/View/CursoAlumno/CursoAlumno.aspx\" class=\"d-flex flex-column cursoCaja\" style=\"text-decoration:none;\">" +
                   $"   <div class=\"h-50\" style=\"background-color:black" + "\"></div>" +
                   $"   <div class=\"p-2 infoCaja\">" +
                   $"       <p>{cursoHor.curso.nombre}</p>" +
                   $"       <div class=\"line\"></div>" +
                   $"       <p>Profesor: {nombres}</p>" +
                   $"       <p>Código curso: {cursoHor.curso.id}</p>" +
                   $"       <p>Salón: {cursoHor.idsalon}"+ //debe ser el nombre del salon
                   $"   </div>" +
                   $"</a>");
            }
        }
    }
}