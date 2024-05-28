<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="AsistenciaProfesor.aspx.cs" Inherits="WebForm.View.AsistenciaProfesor.AsistenciaProfesor" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
   <!-- <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Asistencia"/>-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Asistencias del salon</h1>
    <div class="container">
        <div class="container row pb-2 pt-2">
            <div class="col">
                filtro
            </div>
            <div>
                <div class="text-end p-3">
                   
                </div>
            </div>
        </div>
    </div>
    <button type="button" class="btn btn-secondary" data-bs-toggle="modal" data-bs-target="#guardar--modal">
      <i class="fa-solid fa-gear"></i> Guardar
    </button>

    <table class="table table-striped">
      <thead>
        <tr>
          <th scope="col">Alumno</th>
          <th scope="col">Tipo asistencia</th>
          <th scope="col">Editar</th>
          <th scope="col">Eliminar</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <th scope="row">Cueva Paz, Guanira Erasmo</th>
          <td>Temprano</td>
          <td><input class="form-check-input" type="checkbox" value="" id="flexCheckDefault2"></td>
          <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
        </tr>
        <tr>
          <th scope="row">Phan Chau, Bernardo Julio</th>
          <td>Temprano</td>
          <td><input class="form-check-input" type="checkbox" value="" id="flexCheckDefault1"></td>
          <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
        </tr>
        <tr>
          <th scope="row">Villanueva Villalobos, Vanessa Veth</th>
          <td>Temprano</td>
          <td><input class="form-check-input" type="checkbox" value="" id="flexCheckDefault"></td>
          <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
        </tr>
      </tbody>
    </table>

    <div id="guardar--modal" class="modal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div  class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Guardar cambios</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="submit" class="btn btn-primary">Sí, aceptar</button>
                </div>
            </div>
        </div>
    </div>

    <div id="eliminar--modal" class="modal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">¿Está seguro que quiere eliminar "${elemento}"?</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="submit" class="btn btn-primary">Eliminar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
