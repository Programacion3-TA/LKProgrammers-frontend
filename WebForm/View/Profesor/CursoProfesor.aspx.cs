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
            Session["idCurso"] = $"{id_cursoP}";
            string nombre_s = Request.QueryString["cursoNombre"];
            paginaCurso pag = (Session[$"Pag{id_cursoP}"] == null)? servicio.pagina_init(id_cursoP) : (paginaCurso)Session[$"Pag{id_cursoP}"];
            pag.secciones = pag.secciones ?? new seccion[] { };
            Session[$"Pag{id_cursoP}"] = Session[$"Pag{id_cursoP}"] ?? pag;
            PageTitle.Text = nombre_s;
            pag.secciones = servicio.listar_CONTENIDOS(id_cursoP) ?? new seccion[] { };
            RenderizarPagina(id_cursoP);
        }

        protected void RenderizarPagina(int id_curso)

        {
            paginaCurso pagina = (paginaCurso) Session[$"Pag{id_curso}"];
            foreach (seccion secc in pagina.secciones ?? new seccion[] { })
            {
                String seccHtmlId = $"secc{secc.id}";
                Panel seccHtml = new Panel { };

                elemento[] elementos = secc.elementos ?? new elemento[] { };

                seccHtml.Controls.Add(new LiteralControl("<div class=\"accordion-item\">"));
                seccHtml.Controls.Add(new LiteralControl("<h2 class=\"accordion-header\">"));
                seccHtml.Controls.Add(new LiteralControl($"<button class=\"accordion-button collapsed\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#{seccHtmlId}\" aria-expanded=\"false\" aria-controls=\"{seccHtmlId}\">"));
                seccHtml.Controls.Add(new LiteralControl($"{secc.titulo}"));
                seccHtml.Controls.Add(new LiteralControl("</button>"));
                seccHtml.Controls.Add(new LiteralControl("</h2>"));
                seccHtml.Controls.Add(new LiteralControl($"<div id=\"{seccHtmlId}\" class=\"accordion-collapse collapse\" data-bs-parent=\"#accordionFlushExample\">"));
                foreach (elemento elem in elementos)
                {
                    seccHtml.Controls.Add(new LiteralControl("<div class=\"d-flex justify-content-between\">"));
                    seccHtml.Controls.Add(new LiteralControl(MyReact.CreateComponentByType(elem, "", elem.contenido)));

                    seccHtml.Controls.Add(new LiteralControl("<div class=\"dropdown\">"));
                    seccHtml.Controls.Add(new LiteralControl("   <button class=\"btn btn-secondary dropdown-toggle\" type=\"button\" data-bs-toggle=\"dropdown\" aria-expanded=\"false\">Acciones</button>"));
                    seccHtml.Controls.Add(new LiteralControl("   <ul class=\"dropdown-menu\">"));

                    seccHtml.Controls.Add(new LiteralControl("       <li>"));
                    seccHtml.Controls.Add(new LinkButton
                    {
                        Text= "<i class=\"fa fa-arrow-up\" aria-hidden=\"true\"></i> Mover arriba",
                        CssClass= "dropdown-item"
                    });
                    seccHtml.Controls.Add(new LiteralControl("       </li>"));

                    seccHtml.Controls.Add(new LiteralControl("       <li>"));
                    seccHtml.Controls.Add(new LinkButton
                    {
                        Text = "<i class=\"fa fa-arrow-down\" aria-hidden=\"true\"></i> Mover abajo",
                        CssClass = "dropdown-item"
                    });
                    seccHtml.Controls.Add(new LiteralControl("       </li>"));

                    seccHtml.Controls.Add(new LiteralControl("       <li>"));
                    seccHtml.Controls.Add(new LinkButton
                    {
                        Text = "<i class=\"fa fa-pencil\" aria-hidden=\"true\"></i> Editar",
                        CssClass = "dropdown-item"
                    });
                    seccHtml.Controls.Add(new LiteralControl("       </li>"));

                    seccHtml.Controls.Add(new LiteralControl("       <li>"));
                    seccHtml.Controls.Add(new LinkButton
                    {
                        Text = "<i class=\"fa fa-trash\" aria-hidden=\"true\"></i> Eliminar",
                        CssClass = "dropdown-item"
                    });
                    seccHtml.Controls.Add(new LiteralControl("       </li>"));
                    
                    seccHtml.Controls.Add(new LiteralControl("</ul>"));
                    seccHtml.Controls.Add(new LiteralControl("</div>"));

                    seccHtml.Controls.Add(new LiteralControl("</div>"));
                }
                LinkButton lnkBtn = new LinkButton
                {
                    Text = "<i class='fa-solid fa-plus'></i> Agregar elemento",
                    CssClass = "btn btn-success mt-2"
                };
                lnkBtn.Click += new EventHandler(BTN_AgregarSeccion_Click);
                seccHtml.Controls.Add(lnkBtn);

                seccHtml.Controls.Add(new LiteralControl("</div>"));
                seccHtml.Controls.Add(new LiteralControl("</div>"));

                SeccionesContainer.Controls.Add(seccHtml);
                //seccComp += MyReact.CreateComponent(
                //    "div", "class=\"accordion-item\"",
                //        MyReact.CreateComponent("h2", "class=\"accordion-header\"",
                //            MyReact.CreateComponent("button", $"class=\"accordion-button collapsed\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#{seccHtmlId}\" aria-expanded=\"false\" aria-controls=\"{seccHtmlId}\"", secc.titulo)
                //        ) +
                //        MyReact.CreateComponent("div", $" class=\"accordion-collapse collapse\" data-bs-parent=\"#accordionFlushExample\"",
                //            MyReact.CreateComponent("div", "class=\"accordion-body\"", secc.elementos == null ? "" : secc.elementos.Aggregate("",
                //                (concatenacion, elem) => concatenacion +
                                    
                //                )
                //            )
                //        )
                //);
            }
        }

        //protected void renderizarSecciones(paginaCurso pagina)
        //{
        //    foreach (seccion secc in pagina.secciones)
        //    {
        //        muestraSubtitulo(secc);
        //        if (secc.elementos == null) continue;
        //        foreach (elemento ele in secc.elementos)
        //        {
        //            switch (ele.tipoElemento)
        //            {
        //                case tipoElemento.Heading:
        //                    // Código para manejar el caso "Heading"
        //                    muestraCabecera((heading)ele);
        //                    break;

        //                case tipoElemento.Parrafo:
        //                    // Código para manejar el caso "Parrafo"
        //                    muestraParrafo((parrafo)ele);
        //                    break;

        //                case tipoElemento.Enlace:
        //                    // Código para manejar el caso "Enlace"
        //                    muestraEnlace((enlace)ele);
        //                    break;

        //                case tipoElemento.Imagen:
        //                    // Código para manejar el caso "Imagen"
        //                    muestraImagen((imagen)ele);
        //                    break;

        //                default:
        //                    continue;
        //                    break;
        //            }
        //        }
        //    }
        //}
        //MUESTRAS DE LOS DATOS
        //protected void muestraParrafo(parrafo ele)
        //{
        //    SectionText.Text = ele.contenido;
        //}
        //protected void muestraCabecera(heading ele)
        //{
        //    SectionTitle.Text = ele.contenido;
        //}
        //protected void muestraEnlace(enlace ele)
        //{
        //    LinkExterno.Text = ele.contenido;
        //}
        //protected void muestraImagen(imagen ele)
        //{
        //    string base64String = Convert.ToBase64String(ele.img);
        //    string imageSrc = "data:image/jpeg;base64," + base64String;
        //    Imagenes.ImageUrl = imageSrc;
        //}
        //protected void muestraSubtitulo(seccion secc)
        //{
        //    SectionTitle.Text = secc.titulo + "  ";
        //}

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BTN_AgregarSeccion_Click(object sender, EventArgs e)
        {
            int id_curso = int.Parse((string)Session["idCurso"]);
            paginaCurso pag = (paginaCurso) Session[$"Pag{id_curso}"];
            List<seccion> seccs = (pag.secciones ?? new seccion[] { }).ToList();
            seccs.Add(new seccion { titulo = "Unnamed" });
            pag.secciones = seccs.ToArray();
            Session[$"Pag{id_cursoP}"] = pag;
        }

        protected void AgregarContenido_Click(object sender, EventArgs e)
        {
            CallJavascript("var modalForm = new bootstrap.Modal(document.getElementById('EditarSeccion'));modalForm.show();");
        }

        protected void ModificarSeccion_Click(object sender, EventArgs e)
        {

        }

        protected void EliminaSeccion_Click(object sender, EventArgs e)
        {

        }

        protected void EditarParrafo_Click(object sender, EventArgs e)
        {

        }

        protected void EliminarParrafo_Click(object sender, EventArgs e)
        {

        }

        protected void EditarLink_Click(object sender, EventArgs e)
        {

        }

        protected void EliminarLink_Click(object sender, EventArgs e)
        {

        }

        protected void EditarImagen_Click(object sender, EventArgs e)
        {

        }

        protected void EliminarImagne_Click(object sender, EventArgs e)
        {

        }
    }
}