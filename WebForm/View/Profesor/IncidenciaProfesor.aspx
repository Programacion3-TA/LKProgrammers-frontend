<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="IncidenciaProfesor.aspx.cs" Inherits="WebForm.View.Profesor.IncidenciaProfesor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registro de Incidencias
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Registro de Incidencias</h1>
    <div class="container">
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="GridAlumnosSalon" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" CssClass="table table-responsive table-hover table-striped">
                    <Columns>
                        <asp:BoundField DataField="dni" HeaderText="Dni del Alumno" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre completo" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:BoundField DataField="correoElectronico" HeaderText="Correo Electrónico" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="RegistrarIncidenciaBtn" runat="server" Text="Registrar Asistencia" CssClass="btn btn-warning"
                                    OnClick="RegistrarIncidenciaBtn_Click"
                                    CommandArgument='<%#Eval("dni")%>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                 
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="modal fade" id="RegistroIncidenciaModalCenter" tabindex="-1" role="dialog" aria-labelledby="RegistroIncidenciaModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="RegistroIncidenciaModalLongTitle">Registrar incidencias</h5>
           
          </div>
          <div class="modal-body d-flex  flex-column gap-4" >
              <div class="form-group">
                  <asp:Label ID="NombreProfesorLbl" runat="server" Text="Nombre del Tutor: "></asp:Label>
                  <asp:TextBox ID="NombreProfesorTxt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group">
                  <asp:Label ID="DniAlumnoLbl" runat="server" Text="Dni del Alumno"></asp:Label>
                  <asp:TextBox ID="DniAlumnoTxt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group">
                <asp:Label ID="NombreAlumnoLbl" runat="server" Text="Nombre del alumno: " ></asp:Label>
                <asp:TextBox ID="NombreAlumnoTxt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group">
                  <asp:Label ID="DescripIncidenicaLbl" runat="server" Text="Ingrese descripción de la incidencia: "></asp:Label>
                  <asp:TextBox ID="DescripIncidenciaTxt" runat="server" TextMode="MultiLine" Rows="5" Columns="40"
                      placeholder="El alumno realizo..." CssClass="form-control"></asp:TextBox>
              </div>
              <div class="form-group">
                  <asp:Label ID="GravedadIncidenicaLbl" runat="server" Text="Seleccione la gravedad:"></asp:Label>
                  <asp:RadioButtonList ID="GravedadIncidenciaRbl" runat="server" AutoPostBack="true" CssClass="form-check-input w-50">
                      <asp:ListItem Value="Leve" Text="Leve" ></asp:ListItem>
                      <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                      <asp:ListItem Value="Grave" Text="Grave"></asp:ListItem>
                  </asp:RadioButtonList>
              </div>

          </div>
          <div class="modal-footer">
              <asp:Button ID="CerrarModalIncidenciaBtn" runat="server" Text="Cerrar" CssClass="btn btn-secondary" OnClick="CerrarModalIncidenciaBtn_Click" />
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            <button type="button" class="btn btn-primary">Enviar y registrar</button>
          </div>
        </div>
      </div>
</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js"></script>
</asp:Content>
