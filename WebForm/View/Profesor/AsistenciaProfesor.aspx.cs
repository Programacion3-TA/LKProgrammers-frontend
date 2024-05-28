using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.AsistenciaProfesor
{
    public partial class AsistenciaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            //solo ingresan los profesores que se asignaron a salones como Tutor
            if (!IsPostBack)
            {
                string tipo = (string)Session["Tipo"];
                // if (tipo != "Profesor")Response.Redirect(); //le indica que no tiene permisos;

                profesor profesor = (profesor)Session["Usuario"];
                //
                if (!daoServicio.esTutor(profesor.dni))
                {
                    Response.Redirect("/View/Profesor/ErroNoTutor.aspx");
                }

            }
        }

    }
}