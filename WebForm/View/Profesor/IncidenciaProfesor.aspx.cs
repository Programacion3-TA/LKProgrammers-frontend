using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Profesor
{
    public partial class IncidenciaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        private usuario profesor;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            profesor = (usuario)Session["Usuario"];
            if (!IsPostBack)
            {
                int idsalon = (int)Session["idsalon"];
                cargarAlumnosSalon(idsalon);
            }
        }


        protected void RegistrarIncidenciaBtn_Click(object sender, EventArgs e)
        {
            //buscamos al alumno y llenamos info estatica
            //obtenemos dni del alumo
            Button btn = (Button)sender;
            string dniAlumno = btn.CommandArgument;

            alumno alumno = daoServicio.listarAlumnosFiltro(dniAlumno).ToList().FirstOrDefault();

            NombreProfesorTxt.Text = profesor.nombres + " " + profesor.apellidoPaterno + " " + profesor.apellidoMaterno;
            NombreAlumnoTxt.Text = alumno.nombres + " " + alumno.apellidoPaterno + " " + profesor.apellidoMaterno;
            DniAlumnoTxt.Text = dniAlumno;
            //abre modal
            CallJavascript("showModal('RegistroIncidenciaModalCenter')");
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
        private void cargarAlumnosSalon(int salon)
        {
            //cargamos
            List<alumno> alumnos = daoServicio.listarAlumnosxsalon(salon).ToList();

            foreach(alumno alu in alumnos)
            {
                alu.nombres += " " + alu.apellidoPaterno + " " + alu.apellidoMaterno;
            }

            GridAlumnosSalon.DataSource = alumnos;
            GridAlumnosSalon.DataBind();
        }

        protected void CerrarModalIncidenciaBtn_Click(object sender, EventArgs e)
        {
            CallJavascript("showModal('RegistroIncidenciaModalCenter')");
        }
    }
}