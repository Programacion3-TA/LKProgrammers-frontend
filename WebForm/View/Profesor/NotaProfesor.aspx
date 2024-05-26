<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="NotaProfesor.aspx.cs" Inherits="WebForm.View.NotaProfesor.NotaProfesor" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Curso/Nota"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<h1>
    Notas de alumnos
</h1>
<table class="table table-striped">
  <thead>
    <tr>
      <th scope="col">Alumno</th>
      <th scope="col">Nota</th>
      <th scope="col">Editar</th>
      <th scope="col">Eliminar</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Cueva Paz, Guanira Erasmo</th>
      <td>20</td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#competencia--modal">Editar <i class="fa-solid fa-pencil" style="color:yellow;"></i></a></td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
    </tr>
    <tr>
      <th scope="row">Phan Chau, Bernardo Julio</th>
      <td>14</td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#competencia--modal">Editar <i class="fa-solid fa-pencil" style="color:yellow;"></i></a></td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
    </tr>
    <tr>
      <th scope="row">Villanueva Villalobos, Vanessa Veth</th>
      <td>17</td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#competencia--modal">Editar <i class="fa-solid fa-pencil" style="color:yellow;"></i></a></td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
    </tr>
  </tbody>
</table>

<div id="competencia--modal" class="modal" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered">
        <form action="" method="post" class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Actualizar Notar</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <div class="mb-3">
                    <label for="nota--alumno" class="form-label">Nota:</label>
                    <input type="number" class="form-control" id="nota--alumno" name="nota_alumno"
                        placeholder="Ingrese la nota aquí...">
                </div>
            </div>
            <div class="modal-footer">
                <button type="submit" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                <button type="submit" class="btn btn-primary">Guardar cambios</button>
            </div>
        </form>
    </div>
</div>

<div id="eliminar--modal" class="modal" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered">
        <form action="" class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">¿Está seguro que quiere eliminar "${elemento}"?</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                <button type="submit" class="btn btn-primary">Eliminar</button>
            </div>
        </form>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
