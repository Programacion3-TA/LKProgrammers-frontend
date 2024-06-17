using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Admin.Salones
{
    public partial class SalonDetalle : System.Web.UI.Page
    {
        private int salonId;
        private LKServicioWebClient serviciodao;
        private BindingList<alumno> alumnosSalon;

        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();
            salonId = Convert.ToInt32(Request.QueryString["salonId"]);
            if (!IsPostBack)
            {
                cargarTabla();
                LoadCursos();
                LitSalonId.Text = salonId.ToString();
            }
        }

        private void cargarTabla()
        {
            var list = serviciodao.listarAlumnosxsalon(salonId);
            profesor tutor = serviciodao.listarSalones().ToList().Find(s => s.id == salonId).tutor;
            if (list != null)
            {
                alumnosSalon = new BindingList<alumno>(list);
            }
            GridTutor.DataSource = new List<profesor> { tutor };
            GridAlumnosSalon.DataSource = alumnosSalon;
            GridAlumnosSalon.DataBind();
            GridTutor.DataBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            CallJavascript("showModalFormSalon()");
        }
        protected void BtnQuitar_Click(object sender, EventArgs e)
        {
            string alumnoId = ((LinkButton)sender).CommandArgument;
            serviciodao.eliminarSalonAlumno(salonId,alumnoId);
            cargarTabla();

        }
        protected void BtnSeleccionarAlumno_Click(object sender, EventArgs e)
        {
            string alumnoId = ((LinkButton)sender).CommandArgument;
            serviciodao.insertarSalonAlumno(salonId, alumnoId);
            cargarTabla();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }
        protected void lbBuscarAlumno_Click(object sender, EventArgs e)
        {
            string nombre = TxtFiltroAlumno.Text;
            alumnosSalon = new BindingList<alumno>(serviciodao.listarAlumnosFiltro(nombre).ToList());
            gvAlumnosResult.DataSource = alumnosSalon;
            gvAlumnosResult.DataBind();
        }   

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
        private void LoadCursos()
        {
            //salonId
            var C = serviciodao.listar_curso_salon(salonId);
            List<curso> j = new List<curso>();
            DataTable dtCursos = new DataTable();
            // Simulamos la carga de datos desde una base de datos
            dtCursos.Columns.Add("idCurso");
            dtCursos.Columns.Add("nombreCurso");
            if (C != null)
            {
                j = C.ToList();
                // Añadir filas de ejemplo
                foreach (curso cur_ingresado in j) { dtCursos.Rows.Add(cur_ingresado.id, cur_ingresado.nombre); }
            }
            GridCursos.DataSource = dtCursos;
            GridCursos.DataBind();
        }

        protected void BtnAgregarCurso_Click(object sender, EventArgs e)
        {
            // Aquí se maneja la lógica para agregar un nuevo curso.
            // Ejemplo: Mostrar un formulario modal para ingresar los detalles del curso.

            // Después de agregar, recargar la lista de cursos

            CallJavascript("showModalAgregarCurso()");
            //LoadCursos();
        }

        protected void BtnEliminarCurso_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idCurso = int.Parse(btn.CommandArgument);
            serviciodao.eliminar_curso_salon(salonId,idCurso);
            LoadCursos();
        }
    }
}