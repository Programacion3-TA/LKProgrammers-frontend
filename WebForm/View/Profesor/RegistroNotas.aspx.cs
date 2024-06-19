using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm.View.Profesor
{
    public partial class RegistroNotas : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            PanelError.Visible= false;
            if (!IsPostBack)
            {
                string idsalon = Request.QueryString["idsalon"];
                //alumno[] alumnos = daoServicio.listarAlumnosxsalon(int.Parse(idsalon));
                List<alumno> aluList = (List<alumno>)Session["AlumnosCurso"];
                if (aluList != null)
                {
                  // List<alumno> aluList =  TransformarNombres(alumnos);
                    //Session["AlumnosCurso"] = aluList;
                    CargarAlumnos((List<alumno>)Session["AlumnosCurso"]);
                    ObtenerNotas(aluList);
                    CargarNotas();
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

        protected void ObtenerNotas(List<alumno> alumnos)
        {
            string idCompetencia = Request.QueryString["idcompetencia"];
            int idCompetenciaInt = int.Parse(idCompetencia);
            List<nota> notasNuevas = new List<nota>(); //por si hay nuevas notas
            List<nota> notasRegistradas = new List<nota>(); //por si ya fueron registrados
            foreach (GridViewRow fila in GridAlumnos.Rows)
            {
                //evaluamos las filas de datos
                if (fila.RowType == DataControlRowType.DataRow)
                {
                    string dniAlumno = fila.Cells[0].Text;
                    nota[] notasAlumno = daoServicio.obtenerNota(dniAlumno, idCompetenciaInt);
                    nota notaAlumno;
                    if(notasAlumno != null)
                    {
                        notaAlumno = notasAlumno.FirstOrDefault();
                        notasRegistradas.Add(notaAlumno);
                    }
                    else
                    {
                        notaAlumno  = new nota();
                        competencia competencia = new competencia();
                        notaAlumno.dniAlumno = dniAlumno;
                        notaAlumno.calificacion = -1;
                        notasNuevas.Add(notaAlumno);
                    }
                }
            }

            Session["haynotanuevas"] = (notasNuevas.Count != 0); //si todas las calificaciones fueron -1 porque no existian entonces son notaas nuevas

            Session["notasNuevas"] = notasNuevas;
            Session["notasRegistradas"] = notasRegistradas;



        }
        protected void CargarNotas()
        {
            //solo se carga cuando hay notas registradas
            if (((List<nota>)Session["notasRegistradas"]).Count != 0) {

                //junta todo
                List<nota> todoNotas = (List<nota>)Session["notasRegistradas"];

                foreach (GridViewRow fila in GridAlumnos.Rows)
                {
                    //evaluamos las filas de datos
                    if (fila.RowType == DataControlRowType.DataRow)
                    {
                        string dniAlumno = fila.Cells[0].Text;
                        TextBox notaTxt = (TextBox)fila.FindControl("NotaAlumno");
                        nota nota = todoNotas.Find(x => x.dniAlumno.Equals(dniAlumno));
                        
                        if(nota != null)
                        {
                            notaTxt.Text = nota.calificacion.ToString();
                        }
                    }
                }
            }
        }


        protected void NotaAlumno_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
        }

        protected void CancelaRegistroBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Profesor/CalificarProfesor.aspx");
        }

        protected void GuardarNotasBtn_Click(object sender, EventArgs e)
        {

            string idCompetencia = Request.QueryString["idcompetencia"];
            int idCompetenciaInt = int.Parse(idCompetencia);

            List<nota> notas = new List<nota>();

            foreach (GridViewRow fila in GridAlumnos.Rows)
            {
                //evaluamos las filas de datos
                if (fila.RowType != DataControlRowType.DataRow) continue;

                TextBox notaTxt = (TextBox)fila.FindControl("NotaAlumno");

                if (notaTxt == null) continue;

                try
                {
                    int nota = int.Parse(notaTxt.Text);
                    if (nota < 0 || nota > 20) throw new FormatException("El valor no es valido") ;
                    nota notaAlumno  = new nota();
                    competencia competencia = new competencia();
                    notaAlumno.competencia = competencia;
                    notaAlumno.calificacion = nota;
                    notaAlumno.competencia.id = idCompetenciaInt;
                    notaAlumno.dniAlumno = fila.Cells[0].Text;

                    notas.Add(notaAlumno);
                }
                catch (FormatException ex)
                {
                    CallJavascript("showNotification('Bad', mensaje='Error de formato de ingreso de notas: Las notas deben estar en el rango de 0 y 20. Además, no deben contener caracteres especiales')");
                    return;
                    //no se realiza 
                }
            }
            /*
            if ((bool)Session["notanuevas"])
                foreach (nota notaAlu in notas)
                    daoServicio.insertarNota(notaAlu);
            else
                foreach (nota notaAlu in notas) 
                    daoServicio.modificarNota(notaAlu);*/
            List<nota> notasRegistradas = (List<nota>)Session["notasRegistradas"];
            foreach (nota notaAlu in notas)
            {
               if(notasRegistradas.Find(x => x.dniAlumno.Equals(notaAlu.dniAlumno)) != null)
                {
                    //si lo encuentra se actualiza
                    daoServicio.modificarNota(notaAlu);
                }
                else
                {
                    daoServicio.insertarNota(notaAlu);
                }
            }

            //CallJavascript("showModal('NotasRegistradasModal')");
            Session["MyNotification"] = new MyNotification { Tipo="Ok", Mensaje= "Se actualizaron las notas con éxito", Titulo= "Notas registrada!" };
            Response.Redirect("/View/Profesor/CalificarProfesor.aspx");
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}