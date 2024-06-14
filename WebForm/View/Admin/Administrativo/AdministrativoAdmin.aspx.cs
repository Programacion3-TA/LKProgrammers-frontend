using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.View.Admin.Estudiantes;
using WebForm.View.Admin.Profesores;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WebForm.View.Admin.Administrativo
{
    public partial class AdministrativoAdmin : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient daoservicio;
        private BindingList<personalAdministrativo> personal;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoservicio = new ServicioWS.LKServicioWebClient();
            personal = new BindingList<personalAdministrativo>(daoservicio.listarAdministradores().ToList());

            CargarTabla();
        }

        protected void CargarTabla()
        {
            GridAdministrativo.DataSource = personal;
            GridAdministrativo.DataBind();
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
            DDGenero.SelectedValue = "";
            TxtDireccion.Text = "";
            TxtUsuario.Text = "";
            TxtContrasenia.Text = "";
            TxtFechaNac.Text = "";
            TxtPuesto.Text = "";
            CallJavascript("showModalFormAdministrativo()");
        }
        protected void EditRow_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int codigoBusca = int.Parse(btn.CommandArgument); // Recibe el codigo del trabajador

            personalAdministrativo trabajador = personal.ToList().Find(p => p.codigoPersonal == codigoBusca);
            TxtCode.Text = trabajador.codigoPersonal.ToString();
            TxtDNI.Text = trabajador.dni.ToString();
            TxtNombre.Text = trabajador.nombres;
            TxtTelefono.Text = trabajador.telefono;
            TxtApellidoPat.Text = trabajador.apellidoPaterno;
            TxtApellidoMat.Text = trabajador.apellidoMaterno;
            TxtCorreo.Text = trabajador.correoElectronico;
            TxtPuesto.Text = trabajador.puestoEjecutivo;
            DDGenero.SelectedValue = trabajador.genero.ToString();
            TxtDireccion.Text = trabajador.direccion;
            TxtUsuario.Text = trabajador.usuario1;
            TxtContrasenia.Text = trabajador.contrasenia;

            TxtFechaNac.Text = trabajador.fechaNac.ToString("yyyy-MM-dd");
            CallJavascript("showModalFormAdministrativo()");
        }
        protected void DelRow_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int codigoBusca = int.Parse(btn.CommandArgument); // Recibe el codigo del trabajador

            usuario user_actual = Session["Usuario"] as usuario;

            if (user_actual.dni != daoservicio.obtenerDniDeCodigoAdmin(codigoBusca) ) // Si no es el usuario actual
            {
                 // Preguntar si se quiere eliminar de verdad
                var elementoAEliminar = personal.FirstOrDefault(p => p.codigoPersonal == codigoBusca);
                if (elementoAEliminar == null) return;
                personalAdministrativo per = elementoAEliminar as personalAdministrativo;
                Session["personalAEliminar"] = per;
                LblWarning.Text = "¡Cuidado!";
                LblMensaje.Text = "¿Está seguro de que quiere eliminar al administrador " + per.nombres + " " + per.apellidoPaterno +
                    " con código " + per.codigoPersonal + " y DNI " + per.dni + "?";
                BtnAceptarEliminar.Visible = true;
                CallJavascript("showModalFormWarning()");                
            }
            else
            {
                // Indicar que se trata del usuario actual
                LblWarning.Text = "¡Atención!";
                LblMensaje.Text = "No se puede eliminar el usuario de la sesión actual.";
                BtnAceptarEliminar.Visible = false;

                CallJavascript("showModalFormWarning()");
            }

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            personalAdministrativo personalNuevo = new personalAdministrativo();


            if (!string.IsNullOrEmpty(TxtDNI.Text))
                personalNuevo.dni = TxtDNI.Text;
            if (!string.IsNullOrEmpty(TxtNombre.Text))
                personalNuevo.nombres = TxtNombre.Text;
            if (!string.IsNullOrEmpty(TxtApellidoPat.Text))
                personalNuevo.apellidoPaterno = TxtApellidoPat.Text;
            if (!string.IsNullOrEmpty(TxtApellidoMat.Text))
                personalNuevo.apellidoMaterno = TxtApellidoMat.Text;
            if (!string.IsNullOrEmpty(DDGenero.SelectedValue))
                personalNuevo.genero = DDGenero.SelectedValue[0];
            if (!string.IsNullOrEmpty(TxtDireccion.Text))
                personalNuevo.direccion = TxtDireccion.Text;
            if (!string.IsNullOrEmpty(TxtCorreo.Text))
                personalNuevo.correoElectronico = TxtCorreo.Text;
            if (!string.IsNullOrEmpty(TxtUsuario.Text))
                personalNuevo.usuario1 = TxtUsuario.Text;
            if (!string.IsNullOrEmpty(TxtContrasenia.Text))
                personalNuevo.contrasenia = TxtContrasenia.Text;
            if (!string.IsNullOrEmpty(TxtTelefono.Text))
                personalNuevo.telefono = TxtTelefono.Text;

            if (!string.IsNullOrEmpty(TxtFechaNac.Text))
            {
                try
                {
                    personalNuevo.fechaNac = DateTime.Parse(TxtFechaNac.Text);
                    personalNuevo.fechaNacSpecified = true;
                }
                catch (System.Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Error en el parse");
                }
            }
            System.Diagnostics.Debug.WriteLine(personalNuevo.fechaNac);
            if (!string.IsNullOrEmpty(TxtPuesto.Text))
                personalNuevo.puestoEjecutivo = TxtPuesto.Text;

            if (string.IsNullOrEmpty(TxtCode.Text)) // Si no hay codigo, creamos uno
            {
                int result = daoservicio.insertarAdministrativo(personalNuevo);

                System.Diagnostics.Debug.WriteLine("result= " + result);
                if (result != 0)
                {
                    // se logró insertar
                    personal = new BindingList<personalAdministrativo>(daoservicio.listarAdministradores().ToList());
                    CargarTabla();                    
                }
                else
                {
                    // No se logró insertar. Puede deverse al UNIQUE constraint
                    LblWarning.Text = "¡Atención!";
                    LblMensaje.Text = "No se puede crear un administrador cuyo DNI o correo electrónico ya haya sido registrado o cuyo nombre de usuario de intranet ya exista.";
                    BtnAceptarEliminar.Visible = false;

                    CallJavascript("showModalFormWarning()");
                }
            }
            else
            {
                // Modificamos
                int result = daoservicio.modificarAdministrativo(personalNuevo);
                System.Diagnostics.Debug.WriteLine("result= " + result);
                if (result != 0)
                {
                    // se logró modificar
                    personal = new BindingList<personalAdministrativo>(daoservicio.listarAdministradores().ToList());
                    CargarTabla();
                }

            }
            
        }

        // Evento de cambio de página
        protected void GridAdministrativo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridAdministrativo.PageIndex = e.NewPageIndex;
            GridAdministrativo.DataBind();
        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnAceptarEliminar_Click(object sender, EventArgs e)
        {
            personalAdministrativo personalAEliminar = Session["personalAEliminar"] as personalAdministrativo;
            int resultado = daoservicio.eliminarAdministrativo(personalAEliminar.codigoPersonal);
            personal = new BindingList<personalAdministrativo>(daoservicio.listarAdministradores().ToList());
            CargarTabla();
        }

        protected void LkBtnBuscar_Click(object sender, EventArgs e)
        {
            // Se buscan administradores por nombres, apellidos, dni o código
            if (!string.IsNullOrEmpty(TxtCriterioBusqueda.Text))
            {
                var resultado = daoservicio.buscarAdminPorTodosCriterios(TxtCriterioBusqueda.Text);
                
                if (resultado != null)
                {
                    // Se ha encontrado personal
                    BindingList<personalAdministrativo> personalRecuperado = new BindingList<personalAdministrativo>(resultado.ToList());
                    personal = personalRecuperado;
                    CargarTabla();
                }
                else
                {
                    // No se ha encontrado personal
                    LblWarning.Text = "¡Atención!";
                    LblMensaje.Text = "No se han encontrado administradores con los criterios de búsqueda ingresados.";
                    BtnAceptarEliminar.Visible = false;

                    CallJavascript("showModalFormWarning()");
                }
            }
            else
            {
                // No se ha ingresado un criterio de búsqueda
                LblWarning.Text = "¡Atención!";
                LblMensaje.Text = "Es necesario ingresar un criterio de búsqueda.";
                BtnAceptarEliminar.Visible = false;

                CallJavascript("showModalFormWarning()");
            }
            

            BtnRestaurar.Visible = true;
        }

        protected void BtnRestaurar_Click(object sender, EventArgs e)
        {
            // Se muestran todos los administradores
            personal = new BindingList<personalAdministrativo>(daoservicio.listarAdministradores().ToList());

            CargarTabla();
        }
    }
}