<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAlumno.Master" AutoEventWireup="true" CodeBehind="AsistenciaAlumno.aspx.cs" Inherits="WebForm.View.AsistenciaAlumno.AsistenciaAlumno" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Asistencias
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>
    .row-style{
        text-align:center;
    }
   .JustifyButton{
       text-decoration:none;
       background-color:#0D2F37;
       color:white;
       border-radius:15px;
   }
   .JustifyButton:hover{
       background-color:#0d2f37bf;
   }
  


        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Asistencia"/>        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <form runat="server" class="AsistenciaForm">
        <div class="container-fluid mt-2 ms-4 me-4 ">
          <h2 class="mb-4 text-center text-md-start ">Asistencias por día</h2>
            <asp:GridView ID="GriAsistenciasAlumnos" runat="server"
            AutoGenerateColumns ="false" AllowPaging="true"
            PageSize="30" CssClass="table table-hover table-responsive table-striped "
                HeaderStyle-HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="fecha" HeaderText="Fecha Asistencia" ItemStyle-CssClass="col-1" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="estado" HeaderText="Asistencia" ItemStyle-CssClass="col-1"/>
                <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-1 " ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#justificar--modal">
                            Justificar tardanza
                        </button>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"  />
                </asp:TemplateField>
            </Columns>
             <RowStyle CssClass="row-style" />
             <AlternatingRowStyle CssClass="row-style" />
            </asp:GridView>
        </div>
    </form>

    <!--Modal-->
    <div id="justificar--modal" class="modal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <form action="" method="post" class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Justificar tardanza</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="justificar--textarea" class="form-label">Descripción:</label>
                        <textarea class="form-control" id="justificar--textarea" name="descripcion_tardanza" rows="3"
                            placeholder="Ingrese la descripción aquí..."></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="submit" class="btn btn-primary">Guardar cambios</button>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
