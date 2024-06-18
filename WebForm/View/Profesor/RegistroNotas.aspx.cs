using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Profesor
{
    public partial class RegistroNotas : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack)
            {
                string idsalon = Request.QueryString["idsalon"];
                alumno[] alumnos = daoServicio.listarAlumnosxsalon(int.Parse(idsalon));
                if (alumnos != null)
                {
                   List<alumno> aluList =  TransformarNombres(alumnos);
                   CargarAlumnos(aluList);
                }
            }
        }
        protected List<alumno> TransformarNombres(alumno[] alus) {
            foreach (alumno alumno in alus)
            {
                alumno.nombres += " " + alumno.apellidoPaterno + " " + alumno.apellidoMaterno;
            }
            return alus.ToList();
        }
        protected void CargarAlumnos(List<alumno> alumnos)
        {
            GridAlumnos.DataSource = alumnos;
            GridAlumnos.DataBind();
        }

        protected void NotaAlumno_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
            txtbox.Text = "2";
        }
    }
}