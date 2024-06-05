<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="AsistenciaProfesor.aspx.cs" Inherits="WebForm.View.AsistenciaProfesor.AsistenciaProfesor" EnableEventValidation="false" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Asistencias del salon
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
                    <div class="input-group mb-3 w-50 d-flex gap-3 align-items-center fw-bolder">
                        <asp:Label ID="FiltroLbl" runat="server" Text="Filtrar por mes: "></asp:Label>
                        <asp:DropDownList ID="MesesDropDown" runat="server" CssClass="form-select">
                            <asp:ListItem Value="">Seleccione un mes</asp:ListItem>
                            <asp:ListItem Value="enero"  >Enero</asp:ListItem>
                            <asp:ListItem Value="febrero">Febrero</asp:ListItem>
                            <asp:ListItem Value="marzo">Marzo</asp:ListItem>
                            <asp:ListItem Value="abril">Abril</asp:ListItem>
                            <asp:ListItem Value="mayo">Mayo</asp:ListItem>
                            <asp:ListItem Value="junio">Junio</asp:ListItem>
                            <asp:ListItem Value="julio">Julio</asp:ListItem>
                            <asp:ListItem Value="agosto">Agosto</asp:ListItem>
                            <asp:ListItem Value="septiembre">Septiembre</asp:ListItem>
                            <asp:ListItem Value="octubre">Octubre</asp:ListItem>
                            <asp:ListItem Value="noviembre">Noviembre</asp:ListItem>
                            <asp:ListItem Value="diciembre">Diciembre</asp:ListItem>
                        </asp:DropDownList>
                        <div class="input-group-append">
                           <asp:Button ID="FiltrarMesBtn" runat="server" Text="Filtrar" CssClass="btn btn-outline-secondary"
                               OnClick="FiltrarMesBtn_Click"/>
                        </div>
                    </div>
            </div>
            <div>
                <div class="text-end p-3 d-flex flex-row-reverse justify-content-between">
                   <asp:LinkButton ID="BtnRegistrarAsistencia" runat="server" CssClass="btn btn-dark d-flex gap-2 end-0 align-items-center"
                       Text="<i class='fa-solid fa-clipboard-user'> </i> Agregar" OnClick="BtnRegistrarAsistencia_Click">
                   </asp:LinkButton>
                    <div class="d-flex gap-3">
                        <asp:DropDownList ID="AlumnosDrpDown" runat="server" CssClass="form-select" DataTextField="nombres" DataValueField="dni">
                        </asp:DropDownList>
                        <asp:Button ID="AsistenciasAlumnoBtn" runat="server" Text="Obtener Asistencias" CssClass="btn btn-success" OnClick="AsistenciasAlumnoBtn_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class ="container">
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="GridAsistenciasFechas" runat="server" AutoGenerateColumns="false"
                    AllowPaging ="true" CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="FechaFormato" HeaderText ="Fecha de asistencias" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:TemplateField HeaderText="Actualización de asistencias">
                            <ItemTemplate>
                                <asp:Button runat="server" CssClass="btn btn-primary" Text="Editar" ID="editarAsistencia"
                                    CommandArgument='<%#Eval("Fecha") %>' OnClick="editarAsistencia_Click"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="300px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"/>
                </asp:GridView>
            </div>
        </div>
    </div>


    <div class="modal fade" id="bloqueoRegistroModal" tabindex="-1" role="dialog" aria-labelledby="bloqueoRegistroModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="bloqueoRegistroModalLabel">Aviso!</h5>
                </div>
                <div class="modal-body">
                      Ya se realizaron los registros de los alumnos el día de hoy.
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="BtnCerrarModal" runat="server" CssClass="btn btn-primary" OnClick="BtnCerrarModal_Click">Volver</asp:LinkButton>
                    
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="AsistenciaAlumnoModalCenter" tabindex="-1" role="dialog" aria-labelledby="AsistenciaAlumnoModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="AsistenciaAlumnoModalLongTitle">
                <asp:Label ID="AsistenciaAlumnoLbl" runat="server" Text="Reporte de asistencias de"></asp:Label>
            </h5>
          </div>
          <div class="modal-body d-flex  flex-column gap-4" >
              <asp:GridView ID="AsistenciaAlumnoGrid" runat="server" AutoGenerateColumns="false"
                    AllowPaging ="true" CssClass="table table-hover table-responsive table-striped">
                  <Columns>
                      <asp:BoundField DataField="fechaFormato" HeaderText="Fecha y Hora registrada" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                       <asp:BoundField DataField="estado" HeaderText="Estado de Asistencia" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                  </Columns>
                  <HeaderStyle HorizontalAlign="Center"/>
              </asp:GridView>

          </div>
          <div class="modal-footer">
              <asp:Button ID="CerrarModalIncidenciaBtn" runat="server" Text="Cerrar" CssClass="btn btn-primary" OnClick="CerrarModalIncidenciaBtn_Click"/>
            <!--  <asp:Button ID="GenerarPdfBtn" runat="server" Text="Enviar y registrar" CssClass="btn btn-primary" />-->
          </div>
        </div>
      </div>
    </div>
    <!--<div id="guardar--modal" class="modal" tabindex="-1">
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
    </div>-->
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js"></script>
</asp:Content>
