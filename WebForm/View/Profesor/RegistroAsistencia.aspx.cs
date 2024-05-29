using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.Profesor
{
    public partial class RegistroAsistencia : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack) {
                string idsalon = Request.QueryString["idsalon"];
                if (!string.IsNullOrEmpty(idsalon))
                {
                    mostrarAlumnosSalon(idsalon);
                }
            }

        }
        protected void mostrarAlumnosSalon(string idsalon)
        {
            
            List<alumno> alumnos = daoServicio.listarAlumnosxsalon(int.Parse(idsalon)).ToList();
            
            //por ahora para mostrar el nombre completo
            foreach(alumno alu in alumnos)
            {
                alu.nombres = alu.nombres + " " + alu.apellidoPaterno + " " + alu.apellidoMaterno;
                
            }

            GridAlumnos.DataSource = alumnos;
            GridAlumnos.DataBind();

        }

        protected void RadAsistencia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Profesor/AsistenciaProfesor.aspx");
        }

        protected void BtnGuardarAsistencia_Click(object sender, EventArgs e)
        {

        }
    }

}