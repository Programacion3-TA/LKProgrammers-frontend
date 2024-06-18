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
                CargarAlumnosSalon(idsalon);
            }
        }


        protected void RegistrarIncidenciaBtn_Click(object sender, EventArgs e)
        {
            //buscamos al alumno y llenamos info estatica
            //obtenemos dni del alumo
            Button btn = (Button)sender;
            string dniAlumno = btn.CommandArgument;

            alumno alumno = (daoServicio.listarAlumnosFiltro(dniAlumno) ?? new alumno[] { }).ToList().FirstOrDefault();

            NombreProfesorTxt.Text = profesor.nombres + " " + profesor.apellidoPaterno + " " + profesor.apellidoMaterno;
            NombreAlumnoTxt.Text = alumno.nombres + " " + alumno.apellidoPaterno + " " + profesor.apellidoMaterno;
            DniAlumnoTxt.Text = dniAlumno;
            DescripIncidenciaTxt.Text = "";
            GravedadIncidenciaRbl.SelectedValue = "";
            NumeroIncidenciaTxt.Text = "";
            //abre modal
            CallJavascript("showModal('RegistroIncidenciaModalCenter')");
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
        private void CargarAlumnosSalon(int salon)
        {
            //cargamos
            List<alumno> alumnos = (daoServicio.listarAlumnosxsalon(salon) ?? new alumno[] { }).ToList();
            TransformaNombre(alumnos);
            CargarAlumnos(alumnos);
        }

        protected void CerrarModalIncidenciaBtn_Click(object sender, EventArgs e)
        {
            CallJavascript("closeModal('RegistroIncidenciaModalCenter')");
        }

        protected void FiltrarAlumnosBtn_Click(object sender, EventArgs e)
        {
            string filtro = FiltrarAlumnosTxt.Text;
            List<alumno> alumnos = (daoServicio.listarAlumnosFiltro(filtro) ?? new alumno[]{ }).ToList();
            TransformaNombre(alumnos);
            CargarAlumnos(alumnos);
        }
        private void TransformaNombre(List<alumno> alumnos)
        {
            foreach (alumno alu in alumnos)
            {
                alu.nombres += " " + alu.apellidoPaterno + " " + alu.apellidoMaterno;
            }
        }
        private void CargarAlumnos(List<alumno> alumnos)
        {
            GridAlumnosSalon.DataSource = alumnos;
            GridAlumnosSalon.DataBind();
        }

        protected void RegistrarIncidenciaBtn_Click1(object sender, EventArgs e)
        {
            string numIndi = NumeroIncidenciaTxt.Text;
            bool esNuevo = string.IsNullOrEmpty(numIndi);
            incidencia incidenciaAlumno;
            if (esNuevo)
            {
                incidenciaAlumno = new incidencia();
                incidenciaAlumno.fechaHora = DateTime.Now.Date;
                incidenciaAlumno.fechaHoraSpecified = true;
                incidenciaAlumno.dniProfesor = ((usuario)Session["Usuario"]).dni;
                incidenciaAlumno.dniAlumno = DniAlumnoTxt.Text;
            }
            else
            {
                incidenciaAlumno = (incidencia)Session["incidencia"];

            }
            incidenciaAlumno.descripcion = DescripIncidenciaTxt.Text;
            incidenciaAlumno.tipo = GravedadIncidenciaRbl.SelectedValue;


            if (esNuevo)
            {
                daoServicio.insertarIncidencia(incidenciaAlumno);
            }
            else
            {
                daoServicio.actualizaIncidencia(incidenciaAlumno);
                List<incidencia> incidencias = (daoServicio.listarIncidencias(DniAlumnoTxt.Text) ?? new incidencia[] { }).ToList();
                Session["incidencias"] = incidencias;
                IncidenciasAlumnoGrid.DataSource = incidencias;
                IncidenciasAlumnoGrid.DataBind();
            }
            CallJavascript("closeModal('RegistroIncidenciaModalCenter')");
        }

        protected void MostrarInsidenciasBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[]args = btn.CommandArgument.Split('|'); //separo el string en partes de array por "|"
            string Dni = args[0];
            string nombres= args[1];


            incidencia[] IncidenciaArray =daoServicio.listarIncidencias(Dni);

            if(IncidenciaArray != null)
            {
                List<incidencia> incidencias = IncidenciaArray.ToList();
                IncidenciasAlumnoLbl.Text = "Incidencias del alumno " + nombres;
                Session["incidencias"] = incidencias; //guardamos las incidencias
                Session["dataAlumno"] = args;
                IncidenciasAlumnoGrid.DataSource = incidencias;
                IncidenciasAlumnoGrid.DataBind();
            }
            else
            {
                //muestra modal que no tiene incidencias actuales
                IncidenciasAlumnoLbl.Text = "";
                IncidenciasAlumnoGrid.DataSource = null;
                IncidenciasAlumnoGrid.DataBind();
                CallJavascript("showModal('NoHayIncidenciasModal')");

            }
        }
        protected void CerrarNoHayIncidenciasModalBtn_Click(object sender, EventArgs e)
        {
            CallJavascript("closeModal('NoHayIncidenciasModal')");
        }

        protected void VerificarDescripcionBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string descripcion = btn.CommandArgument;
            DescripcionShowLbl.Text = descripcion; 
            CallJavascript("showModal('DescripcionShowModalCenter')");
        }

        protected void CerrarDescripcionModal_Click(object sender, EventArgs e)
        {
            CallJavascript("closeModal('DescripcionShowModalCenter')");
        }

        protected void ModificarIncidenciaBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int numIncidencia = int.Parse(btn.CommandArgument);
            string[] args = (string[])Session["dataAlumno"];
            incidencia incidencia = ((List<incidencia>)Session["incidencias"]).Find(x => x.numIncidencia == numIncidencia);
            Session["incidencia"] = incidencia;

            NumeroIncidenciaTxt.Text = incidencia.numIncidencia.ToString();
            NombreProfesorTxt.Text = profesor.nombres + " " + profesor.apellidoPaterno + " " + profesor.apellidoMaterno;
            DniAlumnoTxt.Text = args[0];
            NombreAlumnoTxt.Text = args[1];
            DescripIncidenciaTxt.Text = incidencia.descripcion;
            GravedadIncidenciaRbl.SelectedValue = incidencia.tipo;

            CallJavascript("showModal('RegistroIncidenciaModalCenter')");
        }

        protected void EliminarIncidenciaBtn_Click(object sender, EventArgs e)
        {
           Button btn = (Button)sender;
            int numIncidencia = int.Parse(btn.CommandArgument);
            incidencia incidencia = ((List<incidencia>)Session["incidencias"]).Find(x => x.numIncidencia == numIncidencia);

            daoServicio.eliminarIncidencia(incidencia);
            incidencia[] incidenciasArray = daoServicio.listarIncidencias(incidencia.dniAlumno);
            if(incidenciasArray == null)
            {
                IncidenciasAlumnoGrid.DataSource = null;
                IncidenciasAlumnoGrid.DataBind();
                IncidenciasAlumnoLbl.Text = "";
            }
            else
            {
                List<incidencia> incidencias = incidenciasArray.ToList();
                Session["incidencias"] = incidencias;
                IncidenciasAlumnoGrid.DataSource = incidencias;
                IncidenciasAlumnoGrid.DataBind();
            }
        }

        protected void GridAlumnosSalon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int salon = (int)Session["idsalon"];
            CargarAlumnosSalon(salon);
            GridAlumnosSalon.PageIndex = e.NewPageIndex;
            GridAlumnosSalon.DataBind();
        }
    }
}