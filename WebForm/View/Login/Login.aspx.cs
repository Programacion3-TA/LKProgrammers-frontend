﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (Session["Usuario"] != null && Session["Tipo"] != null)
            {
               if (Session["Tipo"] as string == "Administrador")
                {
                    Response.Redirect("/View/Admin/AnioAcademico/AniosAcademicos.aspx");
                }
                Response.Redirect("/View/" + Session["Tipo"] + "/" + Session["Tipo"] + ".aspx");
            }
        }
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            usuario user = (usuario)daoServicio.verificarUsuario(TxtUsuario.Text, TxtContrasenia.Text);
            //redireccionamiento de paginas dependiendo del usuario
<<<<<<< HEAD
            if (t == 'N') //NO ES NADIE ASI QUE EMITE UN ERROR: 78 en ASCII
=======
            if (user == null) //NO ES NADIE ASI QUE EMITE UN ERROR
>>>>>>> master
            {
                //implementar estilos de errores con JS
                
                TxtUsuario.Text = "";
                TxtContrasenia.Text = "";
                Response.Redirect("/View/Login/Login.aspx");
            }
            Session["Usuario"] = user;
            if (user is profesor) //ES EL PROFESOR
            {
                Session["Tipo"] = "Profesor";
                Response.Redirect("/View/Profesor/CursoProfesor.aspx");
            }
<<<<<<< HEAD
            if (t == 'E')//ES EL ESTUDIANTE: 69 ASCII
=======
            else if (user is alumno)//ES EL ESTUDIANTE
>>>>>>> master
            {
                Session["Tipo"] = "Alumno";
                Response.Redirect("/View/Alumno/Alumno.aspx");
            }
            else if (user is personalAdministrativo) //ADMINISTRATIVO
            {
                Session["Tipo"] = "Administrador";
                Response.Redirect("/View/Admin/AnioAcademico/AniosAcademicos.aspx");
            }
        }
    }
}