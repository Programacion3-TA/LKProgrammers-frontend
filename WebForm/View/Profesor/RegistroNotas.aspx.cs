using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
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
            PanelError.Visible= false;
            if (!IsPostBack)
            {
                string idsalon = Request.QueryString["idsalon"];
                alumno[] alumnos = daoServicio.listarAlumnosxsalon(int.Parse(idsalon));
                if (alumnos != null)
                {
                   List<alumno> aluList =  TransformarNombres(alumnos);
                    Session["AlumnosCurso"] = aluList;
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
            List<nota> notas = new List<nota>();
            Session["notanuevas"] = false;
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
                        Session["notanuevas"] = false; //si encontramos notas, significa que es nuevo el registro de notas
                        notaAlumno = notasAlumno.FirstOrDefault();
                    }
                    else
                    {
                        notaAlumno  = new nota();
                        competencia competencia = new competencia();
                        notaAlumno.dniAlumno = dniAlumno;
                        notaAlumno.calificacion = -1;
                    }
                    notas.Add(notaAlumno);
                }
            }
            Session["notanuevas"] = notas.All(nota => nota.calificacion == -1); //si todas las calificaciones fueron -1 porque no existian entonces son notaas nuevas

            Session["notasCargadas"] = notas;



        }
        protected void CargarNotas()
        {
            //solo se carga cuando las notas no son nuevas
            if (!(bool)Session["notanuevas"]) {

                foreach (GridViewRow fila in GridAlumnos.Rows)
                {
                    //evaluamos las filas de datos
                    if (fila.RowType == DataControlRowType.DataRow)
                    {
                        string dniAlumno = fila.Cells[0].Text;
                        TextBox notaTxt = (TextBox)fila.FindControl("NotaAlumno");
                        nota nota = ((List<nota>)Session["notasCargadas"]).Find(x => x.dniAlumno.Equals(dniAlumno));
                        
                        if(nota.calificacion != -1)
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
                if (fila.RowType == DataControlRowType.DataRow)
                {
                    TextBox notaTxt = (TextBox)fila.FindControl("NotaAlumno");

                    if (notaTxt != null)
                    {
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
                            CallJavascript("showModal('NotasRegistradasErrorModal')");
                            return;
                            //no se realiza 
                        }


                    }
                }
            }

            if ((bool)Session["notanuevas"])
                foreach (nota notaAlu in notas)
                    daoServicio.insertarNota(notaAlu);
            else
                foreach (nota notaAlu in notas) 
                    daoServicio.modificarNota(notaAlu);
                

            CallJavascript("showModal('NotasRegistradasModal')");
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}