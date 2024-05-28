using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.View.Admin.Estudiantes
{
    public partial class Estudiantes : System.Web.UI.Page
    {
      
   
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }
        protected void CargarTabla()
        {
         /*   GridAlumnos.DataSource = alumnos;
            GridAlumnos.DataBind();*/
        }
        protected void EditRow_Click(object sender, EventArgs e)
        {
            /*
            LinkButton btn = (LinkButton)sender;
            int codigoBusca = int.Parse(btn.CommandArgument);

            AlumnoN alumno = alumnos.Find(alu => alu.Codigo == codigoBusca);
            TxtCode.Text = alumno.Codigo.ToString();
            TxtDNI.Text = alumno.Dni.ToString();
            TxtNombre.Text = alumno.Nombre;
            TxtTelefono.Text = alumno.Telefono;
            TxtApellidoPat.Text = alumno.ApellidoPat;
            TxtApellidoMat.Text = alumno.ApellidoMat;
            TxtCorreo.Text = alumno.Correo;
            SLGrado.SelectedValue = alumno.Grado;
            CallJavascritp("showModalForm()");*/

        }
        protected void DelRow_Click(object sender, EventArgs e)
        {
            /*
            LinkButton btn = ( LinkButton )sender;
            int codigo = int.Parse(btn.CommandArgument);
            AlumnoN alu = alumnos.Find(al => al.Codigo == codigo);
            alumnos.Remove(alu);
            CargarTabla();*/

        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtDNI.Text = "";
            TxtNombre.Text = "";
            TxtTelefono.Text = "";
            TxtApellidoPat.Text = "";
            TxtApellidoMat.Text = "";
            TxtCorreo.Text = "";
             
            CallJavascritp("showModalForm()");

        }
        protected void BntGuardar_Click(object sender, EventArgs e)
        {
            /*
            AlumnoN alumno = new AlumnoN();
            bool actualizaOcrea = string.IsNullOrEmpty(TxtCode.Text);
            if (actualizaOcrea)
            {
                alumno.Codigo = alumnos.Count + 1;
            }
            else
            {
                alumno = alumnos.Find(alu => alu.Codigo == int.Parse(TxtCode.Text));
            }
            alumno.Dni = TxtDNI.Text;
            alumno.Nombre = TxtNombre.Text;
            alumno.ApellidoPat = TxtApellidoPat.Text;
            alumno.ApellidoMat = TxtApellidoMat.Text;
            alumno.Correo = TxtCorreo.Text;
            alumno.Telefono = TxtTelefono.Text;
            alumno.Grado = SLGrado.SelectedValue;

            if (actualizaOcrea)
            {
                alumnos.Add(alumno);
            }
            CargarTabla() ;*/
        }
        private void CallJavascritp(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}