<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="CursoProfesor.aspx.cs" Inherits="WebForm.View.CursoProfesor.CursoProfesor" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>
        .custom-title {
            font-size: 30px;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
            font-weight: bold;
        }
        .custom-subtitle {
            font-size: 22x;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <%--<uc:Path ID="MyCustomControl1" runat="server" TiposURL="Curso" />--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="container d-flex align-items-start flex-column gap-5">
        <div>
            <h1>
                <asp:Label ID="PageTitle" runat="server" Text="" CssClass="" style="float: left;"></asp:Label>
            </h1>
        </div>

        <div class="accordion accordion-flush w-100" id="accordionFlushExample">
            <asp:PlaceHolder ID="SeccionesContainer" runat="server"></asp:PlaceHolder>
        </div>

        <%--<div>
            <h2>
                <asp:Label ID="SectionTitle" runat="server" Text="" CssClass=""></asp:Label>
            </h2>
            <div style="float: right; margin-right: 15px;">
                <asp:LinkButton ID="AgregarContenido" runat="server" Text="Agregar Contenido" CssClass="btn btn-success" OnClick="AgregarContenido_Click"></asp:LinkButton>
                <asp:LinkButton ID="ModificarSeccion" runat="server" Text="Editar" CssClass="btn btn-success" OnClick="ModificarSeccion_Click"></asp:LinkButton>
                <asp:LinkButton ID="EliminaSeccion" runat="server" Text="Eliminar" CssClass="btn btn-success" OnClick="EliminaSeccion_Click"></asp:LinkButton>
            </div>
        </div>
        <div>
            <asp:Label ID="SectionText" runat="server" Text="" ></asp:Label>
            <div style="float: right; margin-right: 15px;">
                <asp:LinkButton ID="EditarParrafo" runat="server" Text="Editar" CssClass="btn btn-success" OnClick="EditarParrafo_Click"></asp:LinkButton>
                <asp:LinkButton ID="EliminarParrafo" runat="server" Text="Editar" CssClass="btn btn-success" OnClick="EliminarParrafo_Click"></asp:LinkButton>
            </div>
        </div>
        <div>
            <asp:Label ID="LinkExterno" runat="server" Text="Label"></asp:Label>
            <div style="float: right; margin-right: 15px;">
                <asp:LinkButton ID="EditarLink" runat="server" Text="Editar" CssClass="btn btn-success" OnClick="EditarLink_Click"></asp:LinkButton>
                <asp:LinkButton ID="EliminarLink" runat="server" CssClass="btn btn-success" OnClick="EliminarLink_Click"></asp:LinkButton>
            </div>
        </div>
        <div>
            <asp:Image ID="Imagenes" runat="server" />
            <div style="float: right; margin-right: 15px;">
                <asp:LinkButton ID="EditarImagen" runat="server" CssClass="btn btn-success" OnClick="EditarImagen_Click"></asp:LinkButton>
                <asp:LinkButton ID="EliminarImagne" runat="server" CssClass="btn btn-success" OnClick="EliminarImagne_Click"></asp:LinkButton>
            </div>
        </div>--%>

        <asp:LinkButton ID="BTN_AgregarSeccion" runat="server" Text="<i class='fa-solid fa-plus'></i> Agregar Seccion" CssClass="btn btn-success" OnClick="BTN_AgregarSeccion_Click"></asp:LinkButton>
    </div>

    <div class="modal fade" id="EditarSeccion" tabindex="-1" aria-labelledby="EditarSeccion" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Editar Sección</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h2>
                        Titulo de sección
                    </h2>
                    <asp:TextBox ID="TituloSeccion" runat="server" placeholder="Ingrese el titulo aquí..." CssClass="form-control"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Guardar cambios</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="EditarParrafo--modal" tabindex="-1" aria-labelledby="EditarParrafo--modal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Editar Párrafo</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h2>
                        Contenido de párrafo
                    </h2>
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Ingrese el contenido aquí..." CssClass="form-control"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Guardar cambios</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="EditarImagen--modal" tabindex="-1" aria-labelledby="EditarParrafo--modal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Editar Imagen</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h2>
                        Imagen
                    </h2>
                    <asp:FileUpload ID="ImagenFuente" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Guardar cambios</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>

