using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
            alumno[] alumnos= serviciodao.listarAlumnosxsalon(salonId) ?? new alumno[] { };
            alumnosSalon = new BindingList<alumno>(alumnos);
            profesor tutor = (serviciodao.listarSalones() ?? new salon[] { })
                .ToList()
                .Find(s => s.id == salonId)
                ?.tutor
                ?? new profesor { }; // Rayita futuro inge de software
            GridTutor.DataSource = new BindingList<profesor> { tutor };
            GridTutor.DataBind();
            GridAlumnosSalon.DataSource = alumnosSalon;
            GridAlumnosSalon.DataBind();
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
            ScriptManager.RegisterStartupScript(this, GetType(), "", $"__doPostBack('{BtnAgregar.ClientID}','');", true);
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
            curso[] cursos = serviciodao.listar_curso_salon(salonId) ?? new curso[] { };
            List<curso> listaCursos = cursos.ToList();
            DataTable dtCursos = new DataTable();
            // Simulamos la carga de datos desde una base de datos
            dtCursos.Columns.Add("idCurso");
            dtCursos.Columns.Add("nombreCurso");
            
            foreach (curso cur_ingresado in listaCursos) {
                dtCursos.Rows.Add(cur_ingresado.id, cur_ingresado.nombre);
            }
            GridCursos.DataSource = dtCursos;
            GridCursos.DataBind();
        }
        protected void BtnAgregarCurso_Click(object sender, EventArgs e)
        {
            CallJavascript("showModalAgregarCurso()");
        }
        protected void BtnBuscarCurso_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = TxtCriterioBusquedaCurso.Text; //Nombre
            // Lógica para8 buscar cursos por el criterio ingresado
            DataTable cursos = BuscarCursos(criterioBusqueda);
            if (cursos != null && cursos.Rows.Count > 0)
            {
                GVCursos.DataSource = cursos;
                GVCursos.DataBind();
            }
        }
        public static string NormalizarTexto(string texto)
        {
            string textoNormalizado = texto.ToLower();
            textoNormalizado = Regex.Replace(textoNormalizado, @"\s+", ""); // Eliminar espacios en blanco
            textoNormalizado = Regex.Replace(textoNormalizado, @"[áàäâ]", "a");
            textoNormalizado = Regex.Replace(textoNormalizado, @"[éèëê]", "e");
            textoNormalizado = Regex.Replace(textoNormalizado, @"[íìïî]", "i");
            textoNormalizado = Regex.Replace(textoNormalizado, @"[óòöô]", "o");
            textoNormalizado = Regex.Replace(textoNormalizado, @"[úùüû]", "u");
            textoNormalizado = Regex.Replace(textoNormalizado, @"[ñ]", "n");
            return textoNormalizado;
        }
        // Método para buscar cursos en la base de datos
        private DataTable BuscarCursos(string criterio)
        {
            // Aquí debes implementar la lógica para buscar cursos en la base de datos
            // y devolver un DataTable con los resultados.
            curso[] cursos = serviciodao.CURSOS_LIBRES_DEL_ANIO() ?? new curso[] { };
            string cs = NormalizarTexto(criterio);
            List<curso> cursos_list = cursos.ToList();
            DataTable dt = new DataTable();

            cursos_list = cursos_list
                .FindAll(S => NormalizarTexto(S.nombre).Contains(cs)); //uso de contains para hacer una mejor busqueda
            dt.Columns.Add("idCurso");
            dt.Columns.Add("nombreCurso");
            dt.Columns.Add("descripcion");
            foreach (curso j in cursos_list){dt.Rows.Add(j.id,j.nombre,j.descripcion);}

            return dt;
        }
        
        protected void BtnBuscarHorario_Click(object sender, EventArgs e)
        {
            string dia = DDDía.SelectedValue;
            int horasAReservar = int.Parse(TxtHoras.Text);

            // Lógica para buscar horarios disponibles según el día y las horas a reservar
            DataTable horariosDisponibles = BuscarHorarios(dia, horasAReservar);

            if (horariosDisponibles != null && horariosDisponibles.Rows.Count > 0)
            {
                DDHorariosDisponibles.DataSource = horariosDisponibles;
                DDHorariosDisponibles.DataTextField = "Horario"; // Campo a mostrar
                DDHorariosDisponibles.DataValueField = "HorarioId"; // Campo de valor
                DDHorariosDisponibles.DataBind();

                LblHoariosDisp.Visible = true;
                DDHorariosDisponibles.Visible = true;
            }
        }

        // Método para buscar horarios disponibles en la base de datos
        private DataTable BuscarHorarios(string dia, int horas)
        {
            DataTable dt = new DataTable();
            return dt;
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            int cursoId = int.Parse(TxtCursoID.Text);
            string dia = DDDía.SelectedValue;
            int horarioId = int.Parse(DDHorariosDisponibles.SelectedValue);

            // Lógica para guardar la asignación del curso con el horario seleccionado

            serviciodao.insertar_curso_salon(salonId, horarioId, cursoId);
            LoadCursos();
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