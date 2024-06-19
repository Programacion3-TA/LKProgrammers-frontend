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
            CallJavascript("showModalAgregarCurso()");
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
        private int curso_id_glob;
        protected void SeleccionarCurso_OnClick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idCurso = int.Parse(btn.CommandArgument);
            curso_id_glob = idCurso;
            Response.Write("Curso seleccionado: " + idCurso);
        }

        // Método para buscar horarios disponibles en la base de datos
        
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            int cursoId = int.Parse(TxtCursoID.Text);
            string dia = DDDía.SelectedValue;
            int horarioId = int.Parse(DDHorariosDisponibles.SelectedValue);
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

        private DataTable BuscarHorarios(string dia)
        {
            DataTable dt = new DataTable();
            List<horario> hor = serviciodao.Buscar_horarios_libres(dia, salonId)?.ToList() ?? new List<horario>();
            dt.Columns.Add("id");
            dt.Columns.Add("dia");
            dt.Columns.Add("horaInicio");
            dt.Columns.Add("horaFin");
            foreach (horario j in hor)
            {
                string hini = $"{j.horaInicio.hora:00}" + ":" + $"{j.horaInicio.minuto:00}" + ":" + $"{j.horaInicio.segundo:00}";
                string hfim = $"{j.horaFin.hora:00}" + ":" + $"{j.horaFin.minuto:00}" + ":" + $"{j.horaFin.segundo:00}";
                dt.Rows.Add(j.id, j.dia, hini, hfim);
            }
            return dt;
        }

        protected void BtnBuscarHorario_Click1(object sender, EventArgs e)
        {
            string dia = DDDía.SelectedValue;
            // Lógica para buscar horarios disponibles según el día y las horas a reservar
            DataTable horariosDisponibles = BuscarHorarios(dia);
            if (horariosDisponibles != null && horariosDisponibles.Rows.Count > 0)
            {
                // Vincula los datos al GridView de horarios disponibles
                GridHorario.DataSource = horariosDisponibles;
                GridHorario.DataBind();

                // Mostrar el GridView
                GridHorario.Visible = true;
            }
            else
            {
                // En caso de no encontrar horarios disponibles, ocultar el GridView
                GridHorario.Visible = false;
            }

            // Mostrar el modal de agregar curso después de buscar los horarios
            CallJavascript("showModalAgregarCurso()");
        }

    }
}