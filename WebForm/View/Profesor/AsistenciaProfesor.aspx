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
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"
                        CssClass="btn btn-dark" Text="<i class='fa-solid fa-clipboard-user'></i> Marcar asistencia"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
