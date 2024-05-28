<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="CompetenciaProfesor.aspx.cs" Inherits="WebForm.View.CompetenciaProfesor.CompetenciaProfesor" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
   <!-- <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Curso/Competencia"/>-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<h1>
    Competencias del curso
</h1>
<table class="table table-striped">
  <thead>
    <tr>
      <th scope="col">Id</th>
      <th scope="col">Descripción</th>
      <th scope="col">Editar</th>
      <th scope="col">Eliminar</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">1</th>
      <td>Descripción1</td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#competencia--modal">Editar <i class="fa-solid fa-pencil" style="color:yellow;"></i></a></td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
    </tr>
    <tr>
      <th scope="row">2</th>
      <td>Descripción2</td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#competencia--modal">Editar <i class="fa-solid fa-pencil" style="color:yellow;"></i></a></td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
    </tr>
    <tr>
      <th scope="row">3</th>
      <td>Descripción3</td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#competencia--modal">Editar <i class="fa-solid fa-pencil" style="color:yellow;"></i></a></td>
      <td><a class="link-opacity-100" style="cursor:pointer;" data-bs-toggle="modal" data-bs-target="#eliminar--modal">Eliminar <i class="fa-solid fa-trash" style="color:red;"></i></a></td>
    </tr>
  </tbody>
</table>

<button type="button" class="btn btn-success">Añadir +</button>

<div id="competencia--modal" class="modal" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered">
        <div  class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Ingresar/Actualizar competencia</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <div class="mb-3">
                    <label for="competencia--peso" class="form-label">Peso:</label>
                    <input type="number" class="form-control" id="competencia--peso" name="peso_competencia"
                        placeholder="Ingrese el peso aquí...">
                </div>
                <div class="mb-3">
                    <label for="competencia--textarea" class="form-label">Descripción:</label>
                    <textarea class="form-control" id="competencia--textarea" name="competencia_textarea" rows="3"
                        placeholder="Ingrese la descripción aquí..."></textarea>
                </div>
            </div>
            <div class="modal-footer">
                <button type="submit" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                <button type="submit" class="btn btn-primary">Guardar cambios</button>
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
