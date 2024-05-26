using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using WebForm.ServicioWS;
namespace WebForm.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        public List<Usuario> usuarios; 
        private ServicioWS.LKServicioWebClient daoServicio;
        public Login() {
            usuarios = new List<Usuario>();

            usuarios.Add(new Profesor()
            {
                Codigo = "001",
                Nombre = "Juan",
                ApellidoPat = "Perez",
                ApellidoMat = "Dávila",
                Sexo = 'M',
                Direccion = "Su casa",
                Correo = "hola@gmail.com",
                Username = "juanp",
                Telefono = "960589039",
                Dni = "72207476",
                Password = "1234",
                Especialidad = "Matematicas",

            });

            usuarios.Add(new AlumnoN()
            {
                Codigo = 1,
                Dni = "76835856",
                Grado = "PRIM1",
                Nombre = "Fidel",
                ApellidoPat = "Apari",
                ApellidoMat = "Sanchez",
                Username="FidelApari",
                Password="1234",
                Sexo = '?',
                Correo = "Elapari@uWuntu.com",
                Telefono = "+91 222 111 222"

            });

            usuarios.Add(new Administrador() {
                Codigo = 2,
                Dni = "33333",
                Nombre = "Administrador1",
                ApellidoPat = "Admin",
                ApellidoMat = "Admin2",
                Username = "myadmin",
                Password = "1234",
                Sexo = 'M',
                Correo = "admin@gmail.com",
                Telefono="+93333333",
            });

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new ServicioWS.LKServicioWebClient();

            if (Session["Usuario"] != null && Session["Tipo"] != null)
            {
                Response.Redirect("/View/" + Session["Tipo"] + "/" + Session["Tipo"] + ".aspx");
            }
        }
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
          //  Usuario usuario = usuarios.Find(pro => (pro.Username == TxtUsuario.Text || pro.Correo == TxtUsuario.Text) && pro.Password == TxtContrasenia.Text);
            List<usuario> usuarios = daoServicio.listarUsuarios().ToList();
            usuario user = usuarios.Find(x => (x.usuario1 == TxtUsuario.Text || x.correoElectronico == TxtUsuario.Text) && x.contrasenia == TxtContrasenia.Text);

            //redireccionamiento de paginas dependiendo del usuario
            if(user == null)
            {

                //implementar estilos de errores con JS
                TxtUsuario.Text = "";
                TxtContrasenia.Text = "";
                Response.Redirect("/View/Login/Login.aspx");
            }

            Session["Usuario"] = user;
            //Session["AnimacionInicio"] = true; webada de fidel
            if(user is profesor)
            {
                Session["Tipo"] = "Profesor";
                Response.Redirect("/View/Profesor/CursoProfesor.aspx");
            }

            if (user is alumno)
            {
                Session["Tipo"] = "Alumno";
                Response.Redirect("/View/Alumno/Alumno.aspx");
            }
            if(user is personalAdministrativo)
            {
                Session["Tipo"] = "Administrador";
                Response.Redirect("/View/Admin/Profesores/Profesores.aspx");
            }
        }
    }
}