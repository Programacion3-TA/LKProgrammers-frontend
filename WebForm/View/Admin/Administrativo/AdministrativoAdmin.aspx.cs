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

            daoservicio.eliminarAdministrativo(codigoBusca);
            var elementoAEliminar = personal.FirstOrDefault(p => p.codigoPersonal == codigoBusca);
            if (elementoAEliminar != null)
            {
                personal.Remove(elementoAEliminar);
            }

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            personalAdministrativo personalNuevo = new personalAdministrativo();

            if (string.IsNullOrEmpty(TxtCode.Text)) // Si no hay codigo, creamos uno
            {
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
                    /*
                   try
                    {
                        personalNuevo.fechaNac = DateTime.Parse(TxtFechaNac.Text);
                        personalNuevo.fechaNacSpecified = true;
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("Error en el parse");
                    }*/
                }
                System.Diagnostics.Debug.WriteLine(personalNuevo.fechaNac);
                if (!string.IsNullOrEmpty(TxtPuesto.Text))
                    personalNuevo.puestoEjecutivo = TxtPuesto.Text;

                int result = daoservicio.insertarAdministrativo(personalNuevo);

                System.Diagnostics.Debug.WriteLine("result= " + result);
                if (result != 0)
                {
                    // se logró insertar
                    personal.Add(personalNuevo); 
                    CargarTabla();                    
                }
            }
            else
            {
                // Modificamos


            }


            
        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}