using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Alumno
{
    public partial class NotaAlumno : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient serviciodao;
        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();
            RenderizarCursos();
        }
        protected void RenderizarCursos()
        {
            alumno alum = (alumno)Session["Usuario"];
            string dni = alum.dni;
            List<curso> cursos = (serviciodao.listarCursosPorAlumno(dni) ?? new curso[] { }).ToList();
            GridCursosNotas.DataSource = cursos;
            GridCursosNotas.DataBind();
        }

        protected void BtnVerNotas__Click(object sender, EventArgs e)
        {
            alumno alum = (alumno)Session["Usuario"];
            string dni = alum.dni;
            int cursoId = int.Parse(((Button)sender).CommandArgument);
            List<nota> notas = (serviciodao.listarNotasAlumnoCurso(dni, cursoId) ?? new nota[] { }).ToList();
            GridViewNotasAlumno.DataSource = notas;
            GridViewNotasAlumno.DataBind();

            double sumaParcial = 0, sumaFaltante = 0, pesos = 0;
            foreach(nota n in notas)
            {
                pesos += n.competencia.peso;
                sumaParcial += n.calificacion * n.competencia.peso;
                sumaFaltante += (20 - n.calificacion) * n.competencia.peso;
            }
            CallJavaScript("graficarPie([" + (sumaParcial/pesos)  + ", " + (sumaFaltante/pesos) + "]); mostrarModal('NotasCursoModal')");
        }

        protected void BtnGenerarPdf__Click(object sender, EventArgs e)
        {
            // Diego
            salon salon = (salon)Session["salonAlumno"];
            alumno alumno = (alumno)Session["Usuario"];
            string nombre = alumno.nombres;
            nombre += " " + alumno.apellidoPaterno + " " + alumno.apellidoMaterno;
            string idsalon = salon.id.ToString();
            string grado = TransformarGrado(alumno.grado.ToString());
            string nombreProfesor = salon.tutor.nombres + " " + salon.tutor.apellidoPaterno + " " + salon.tutor.apellidoMaterno;
            Byte[] FileBuffer = serviciodao.reportePDFNotas(alumno.dni, nombre, grado, alumno.telefono, nombreProfesor,idsalon);
            if (FileBuffer != null)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }

        protected void CallJavaScript(string func)
        {
            string script = "window.onload = function() {" + func + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
        protected string TransformarGrado(string grado)
        {
            switch (grado)
            {
                case "INI2":
                    return "2 años";
                case "INI3":
                    return "3 años";
                case "INI4":
                    return "4 años";
                case "INI5":
                    return "5 años";
                case "PRIM1":
                    return "Primero de Primaria";
                case "PRIM2":
                    return "Segundo de Primaria";
                case "PRIM3":
                    return "Tercero de Primaria";
                case "PRIM4":
                    return "Cuarto de Primaria";
                case "PRIM5":
                    return "Quinto de Primaria";
                case "PRIM6":
                    return "Sexto de Primaria";
                default:
                    return "Fallo en el grado";
            }
        }
    }
}