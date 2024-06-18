using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //no deja entrar a menos que sea miembro
            if (Session["Usuario"] == null || Session["Tipo"] == null)
                Response.Redirect("/View/Login/Login.aspx");
            
            if (Session["AnimacionInicio"] != null)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "A", "<script>alert('A');</script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "A", "<script type=text/javascript> alert('Hello World!') </script>");
                Session["AnimacionInicio"] = null;
            }

            //if (Session["MyNotification"] != null)
            //{
            //    MyNotification notf = (MyNotification)Session["MyNotification"];
            //    Dictionary<NotificationStates, string> StadoClase = new Dictionary<NotificationStates, string>
            //    {
            //        {NotificationStates.Ok, "alert alert-success" },
            //        {NotificationStates.BadRequest, "alert alert-danger" },
            //        {NotificationStates.Primary, "alert alert-primary" }
            //    };
            //    ErrorAlert.Text =
            //        $"<div class=\"{StadoClase[notf.Estado]} my__notification position-fixed \" style=\"z-index:999;\" role=\"alert\">" +
            //        "   <i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\"></i>" +
            //        $"  {notf.Mensaje}" +
            //        "</div>";
            //    Session["MyNotification"] = null;
            //}

            usuario _usuario = (usuario)Session["Usuario"];
            string _tipo_usuario = (string)Session["Tipo"];
            nombreUsuarioLbl.Text = $"{_usuario.nombres} {_usuario.apellidoPaterno} <span class=\"badge text-bg-primary\">{_tipo_usuario}</span>";
        }

        protected void CerrarSesionBtn_Click(object sender, EventArgs e)
        {
            Session["Tipo"] = null;
            Session["Usuario"] = null;
            Response.Redirect("/View/Login/Login.aspx"); 
        }
    }
}