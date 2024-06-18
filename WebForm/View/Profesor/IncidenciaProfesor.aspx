<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="IncidenciaProfesor.aspx.cs" Inherits="WebForm.View.Profesor.IncidenciaProfesor" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Incidencias del salon
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Incidencias del salon</h1>
    <div class="container">
        <nav class=" navbar bg-light navbar-light w-75">
            <div class="d-flex gap-3 w-100">
            <asp:TextBox ID="FiltrarAlumnosTxt" runat="server" CssClass="form-control mr-sm-2 w-75" placeholder="Buscar por Nombre de alumno o DNI"></asp:TextBox>
                <asp:LinkButton ID="FiltrarAlumnosBtn" runat="server" CssClass="btn btn-success my-2 my-sm-0" OnClick="FiltrarAlumnosBtn_Click">
                    <i class="fa-solid fa-magnifying-glass"></i>
                </asp:LinkButton>
            </div>
        </nav>
    </div>

    <div class="container">
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="GridAlumnosSalon" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" CssClass="table table-responsive table-hover table-striped" PageSize="5"
                    OnPageIndexChanging="GridAlumnosSalon_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="dni" HeaderText="Dni del Alumno" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre completo" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:BoundField DataField="correoElectronico" HeaderText="Correo Electrónico" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="RegistrarIncidenciaBtn" runat="server" Text="Registrar Incidencia" CssClass="btn btn-primary" 
                                    OnClick="RegistrarIncidenciaBtn_Click"
                                    CommandArgument='<%#Eval("dni")%>'/>
                                <asp:Button ID="MostrarInsidenciasBtn" runat="server" Text="Desplegar incidencias"  CssClass =" btn btn-success"
                                    CommandArgument='<%#Eval("dni") + "|"+Eval("nombres") %>' OnClick="MostrarInsidenciasBtn_Click"/>
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="450px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"/>
                </asp:GridView>
            </div>
        </div>
    </div>

    
    <hr />
    <!--Se despliega el grid de incidencias-->
    <div class="container">
        <div class="card">
            <div class="card-header">
                <asp:Label ID="IncidenciasAlumnoLbl" runat="server" Text="" Font-Size="X-Large"></asp:Label>
            </div>
            <div class="card-body">
             <asp:GridView ID="IncidenciasAlumnoGrid" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" CssClass="table table-responsive table-hover table-striped">
                 <Columns>
                     <asp:BoundField DataField="numIncidencia" HeaderText="Numero de la incidencia" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:BoundField DataField="fechaHora" HeaderText="Fecha y hora registrada" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:BoundField DataField="tipo" HeaderText="Gravedad de la incidencia" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:TemplateField HeaderText="Acciones">
                         <ItemTemplate>
                            <asp:Button ID="ModificarIncidenciaBtn" runat="server" Text="Modificar" CssClass="btn btn-success" 
                                CommandArgument='<%#Eval("numIncidencia") %>' OnClick="ModificarIncidenciaBtn_Click"/>
                             <asp:Button ID="EliminarIncidenciaBtn" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                 CommandArgument='<%#Eval("numIncidencia") %>' OnClick="EliminarIncidenciaBtn_Click"/>
                             <asp:Button ID="VerificarDescripcionBtn" runat="server" Text="Ver descripcion" CssClass="btn btn-secondary" 
                                 OnClick="VerificarDescripcionBtn_Click" CommandArgument='<%#Eval("descripcion") %>'/>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="450px" />
                     </asp:TemplateField>
                 </Columns>
                 <HeaderStyle HorizontalAlign="Center"/>
             </asp:GridView>
            </div>
        </div>
    </div>



 <!--Modal de no se tienen incidencias-->
   <div class="modal fade" id="NoHayIncidenciasModal" tabindex="-1" role="dialog" aria-labelledby="NoHayIncidenciasModalLabel" aria-hidden="true">
       <div class="modal-dialog" role="document">
           <div class="modal-content">
               <div class="modal-header">
                   <h5 class="modal-title" id="NoHayIncidenciasModalLabel">Aviso!</h5>
               </div>
               <div class="modal-body">
                     Este alumno no tiene incidencias registradas actualmente.
               </div>
               <div class="modal-footer">
                   <asp:Button ID="CerrarNoHayIncidenciasModalBtn" runat="server" Text="Cerrar" CssClass="btn btn-secondary" 
                       OnClick="CerrarNoHayIncidenciasModalBtn_Click"/>
               </div>
           </div>
       </div>
   </div>

    <!--Modal de mostrar la descripcion-->
<div class="modal fade" id="DescripcionShowModalCenter" tabindex="-1" role="dialog" aria-labelledby="DescripcionShowModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="DescripcionShowModalLongTitle">Descripción de la incidencia</h5>
      </div>
      <div class="modal-body">
          <asp:Label ID="DescripcionShowLbl" runat="server" Text=""></asp:Label>
      </div>
      <div class="modal-footer">
          <asp:Button ID="CerrarDescripcionModal" runat="server" Text="Cerrar" CssClass="btn btn-secondary" OnClick="CerrarDescripcionModal_Click" />
      </div>
    </div>
  </div>
</div>


    <!--Modal de registro de incidencias-->
    <div class="modal fade" id="RegistroIncidenciaModalCenter" tabindex="-1" role="dialog" aria-labelledby="RegistroIncidenciaModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="RegistroIncidenciaModalLongTitle">Registrar incidencias</h5>
          </div>
          <div class="modal-body d-flex  flex-column gap-4" >
              <div class="form-group">
                  <asp:Label ID="NumeroIncidenciaLbl" runat="server" Text="Numero de incidencia"></asp:Label>
                  <asp:TextBox ID="NumeroIncidenciaTxt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>
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
                  <asp:RadioButtonList ID="GravedadIncidenciaRbl" runat="server"  CssClass="form-check-input w-50"
                      RepeatDirection="Horizontal" CellPadding="10">
                      <asp:ListItem Value="Leve" Text="Leve" ></asp:ListItem>
                      <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                      <asp:ListItem Value="Grave" Text="Grave"></asp:ListItem>
                  </asp:RadioButtonList>
              </div>

          </div>
          <div class="modal-footer">
              <asp:Button ID="CerrarModalIncidenciaBtn" runat="server" Text="Cerrar" CssClass="btn btn-secondary" OnClick="CerrarModalIncidenciaBtn_Click" />
              <asp:Button ID="RegistrarIncidenciaBtn" runat="server" Text="Enviar y registrar" CssClass="btn btn-primary" OnClick="RegistrarIncidenciaBtn_Click1" />
          </div>
        </div>
      </div>
    </div>


   

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js"></script>
</asp:Content>
