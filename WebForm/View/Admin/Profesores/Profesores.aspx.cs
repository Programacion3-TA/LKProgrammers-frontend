using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace WebForm.View.Admin.Profesores
{
    
    public partial class Profesores : System.Web.UI.Page
    {
        public static List<Profesor> ListaProfesor;
        static Profesores()
        {
            ListaProfesor = new List<Profesor>();
            ListaProfesor.Add(new Profesor() {
                Codigo= "001",
                Nombre="Juan",
                ApellidoPat="Perez",
                ApellidoMat="Dávila",
                Sexo='M',
                Direccion="Su casa",
                Correo="hola@gmail.com",
                Username="juanp",
                Telefono="960589039",
                Dni="72207476",
                Password="1234",
                Especialidad="Matematicas",

            });


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void CargarTabla()
        {
            GridProfesores.DataSource = ListaProfesor;
            GridProfesores.DataBind();
        }

        protected void EditRow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            Profesor profesor = ListaProfesor.Find(x => x.Codigo == code);
            TxtCode.Text = profesor.Codigo;
            TxtNombre.Text = profesor.Nombre;
            TxtApellidoPat.Text = profesor.ApellidoPat;
            TxtApellidoMat.Text = profesor.ApellidoMat;
            TxtEspecialidad.Text = profesor.Especialidad;
            CallJavascritp("showModalForm()");
        }
        protected void DelRow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            Profesor profesor = ListaProfesor.Find(x => x.Codigo == code);
            ListaProfesor.Remove(profesor);
            CargarTabla();
        }

        protected void ButGuardar_Click(object sender, EventArgs e)
        {
            Profesor profesor = new Profesor();
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                profesor.Codigo = "00" + (ListaProfesor.Count + 1);
                profesor.Nombre = TxtNombre.Text;
                profesor.ApellidoPat = TxtApellidoPat.Text;
                profesor.ApellidoMat = TxtApellidoMat.Text;
                profesor.Especialidad = TxtEspecialidad.Text;
                ListaProfesor.Add(profesor);
                CargarTabla();
            }
            else //actualizar
            {
                profesor = ListaProfesor.Find(x => x.Codigo == TxtCode.Text);
                profesor.Nombre = TxtNombre.Text;
                profesor.ApellidoMat= TxtApellidoMat.Text;
                profesor.ApellidoPat = TxtApellidoPat.Text;
                profesor.Especialidad = TxtEspecialidad.Text;
                CargarTabla();
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtNombre.Text = "";
            TxtApellidoMat.Text = "";
            TxtApellidoPat.Text = "";
            TxtEspecialidad.Text = "";
            CallJavascritp("showModalForm()");
        }
        private void CallJavascritp(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}