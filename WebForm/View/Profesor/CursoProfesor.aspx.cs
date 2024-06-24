using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm.View.CursoProfesor
{
    public partial class CursoProfesor : System.Web.UI.Page
    {
        private int id_cursoP;
        private string nombre_s;
        private LKServicioWebClient servicio;
        private paginaCurso pag;
        protected void Page_Load(object sender, EventArgs e)
        {
            servicio = new LKServicioWebClient();
            int id_cursoP = int.Parse(Request.QueryString["cursoId"]);
            string nombre_s = Request.QueryString["cursoNombre"];
            paginaCurso pag = servicio.pagina_init(id_cursoP);
            PageTitle.Text = nombre_s;
            pag.secciones = servicio.listar_CONTENIDOS(id_cursoP) ?? new seccion[] { };
            renderizarSecciones(pag);
        }

        protected void renderizarSecciones(paginaCurso pagina)
        {
            foreach (seccion secc in pagina.secciones)
            {
                muestraSubtitulo(secc);
                if (secc.elementos == null) continue;
                foreach (elemento ele in secc.elementos)
                {
                    switch (ele.tipoElemento)
                    {
                        case tipoElemento.Heading:
                            // Código para manejar el caso "Heading"
                            muestraCabecera((heading)ele);
                            break;

                        case tipoElemento.Parrafo:
                            // Código para manejar el caso "Parrafo"
                            muestraParrafo((parrafo)ele);
                            break;

                        case tipoElemento.Enlace:
                            // Código para manejar el caso "Enlace"
                            muestraEnlace((enlace)ele);
                            break;

                        case tipoElemento.Imagen:
                            // Código para manejar el caso "Imagen"
                            muestraImagen((imagen)ele);
                            break;

                        default:
                            continue;
                            break;
                    }
                }
            }
        }
        //MUESTRAS DE LOS DATOS
        protected void muestraParrafo(parrafo ele)
        {
            SectionText.Text = ele.contenido;
        }
        protected void muestraCabecera(heading ele)
        {
            SectionTitle.Text = ele.contenido;
        }
        protected void muestraEnlace(enlace ele)
        {
            LinkExterno.Text = ele.contenido;
        }
        protected void muestraImagen(imagen ele)
        {
            string base64String = Convert.ToBase64String(ele.img);
            string imageSrc = "data:image/jpeg;base64," + base64String;
            Imagenes.ImageUrl = imageSrc;
        }
        protected void muestraSubtitulo(seccion secc)
        {
            SectionTitle.Text = secc.titulo + "  ";
        }

        protected System.Void BTN_AgregarSeccion_Click()
        {

        }

        protected System.Void AgregarContenido_Click()
        {

        }

        protected System.Void ModificarSeccion_Click()
        {

        }

        protected System.Void EliminaSeccion_Click()
        {

        }

        protected System.Void EditarParrafo_Click()
        {

        }

        protected System.Void EliminarParrafo_Click()
        {

        }

        protected System.Void EditarLink_Click()
        {

        }

        protected System.Void EliminarLink_Click()
        {

        }

        protected System.Void EditarImagen_Click()
        {

        }

        protected System.Void EliminarImagne_Click()
        {

        }
    }
}