<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="AsistenciaProfesor.aspx.cs" Inherits="WebForm.View.AsistenciaProfesor.AsistenciaProfesor" EnableEventValidation="false" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Asistencias del salon
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>
        .erroMsg{
            background-color:#c90606ce;
            height:40px;
            color:aliceblue;
            font-weight:bold
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
   <!-- <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Asistencia"/>-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Asistencias del salon</h1>
    <div class="container">
        <div class="container row pb-2 pt-2">
            <div class="card mb-4">
                
                    <h2 class="card-header">Asistencias por alumno</h2>
                    <div class="d-flex gap-3 border-2 flex-column card-body">
                        <div class="d-flex gap-3 w-75" >
                            <div>
                                <!--Falta agregar advertencias de no ingresar correcto las fechas-->
                              <asp:Label ID="FechaIniSelecLbl" runat="server" Text="Label" CssClass="form-check-label" Font-Bold="true">Fecha Inicial:</asp:Label>
                              <asp:TextBox ID="FechaIniTxt" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

                            </div>
                            <div>
                                <asp:Label ID="FechFinSelectLbl" runat="server" Text="Label" CssClass="form-check-label" Font-Bold="true">Fecha Final:</asp:Label>
                                <asp:TextBox ID="FechaFinalTxt" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <asp:Button ID="AsignarFechasBtn" runat="server" Text="Seleccionar Fechas" CssClass="btn btn-warning h-50 align-self-center" OnClick="AsignarFechasBtn_Click" />
                        </div>
                        <div class="d-flex gap-4">
                            <asp:DropDownList ID="AlumnosDrpDown" runat="server" CssClass="form-select" DataTextField="nombres" DataValueField="dni">
                            </asp:DropDownList>
                            <asp:Button ID="AsistenciasAlumnoBtn" runat="server" Text="Obtener Asistencias" CssClass="btn btn-success" OnClick="AsistenciasAlumnoBtn_Click"/>
                        </div>
                        
                    </div>
            </div>
            <hr />
            <div>
                <div class="text-end p-3 d-flex flex-row-reverse justify-content-between">
                   <asp:LinkButton ID="BtnRegistrarAsistencia" runat="server" CssClass="btn btn-dark d-flex gap-2 end-0 align-items-center h-75"
                       Text="<i class='fa-solid fa-clipboard-user'> </i> Agregar Asistencia " OnClick="BtnRegistrarAsistencia_Click">
                   </asp:LinkButton>
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
            </div>
        </div>
    </div>

    <div class ="container">
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="GridAsistenciasFechas" runat="server" AutoGenerateColumns="false"
                    AllowPaging ="true" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="GridAsistenciasFechas_PageIndexChanging"
                    PageSize="5">
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


    <!--Modal para el ingreso de fechas-->
    <div class="modal fade" id="fechasReporteModal" tabindex="-1" role="dialog" aria-labelledby="fechasReporteModalLabel" aria-hidden="true"
        >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background:#333">
                    <h5 class="modal-title" id="fechasReporteModalLabel" style="color:#fff">Seleccione las fechas</h5>
                       
                </div>
                <asp:Label ID="msgErrorFechas" runat="server" Text="Label" CssClass="erroMsg">¡Las fecha inicial es posterior a la fecha final!</asp:Label>
                <div class="modal-body d-flex gap-5" id="modalBody" style="overflow-x:scroll">
                      <div class="d-flex flex-column gap-1">
                          <asp:Label ID="fechaIniLbl" runat="server" Text="Label" Font-Bold="true">Seleccione la fecha inicial:</asp:Label>
                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                                <asp:Calendar ID="fechaIniCalen" runat="server"></asp:Calendar>
                              </ContentTemplate>
                          </asp:UpdatePanel>
                      </div>
                      <div class="d-flex flex-column gap-1">
                          <asp:Label ID="fechaFinLbl" runat="server" Text="Label" Font-Bold="true">Selecione la fecha final:</asp:Label>
                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                              <ContentTemplate>
                                <asp:Calendar ID="fechaFinCalen" runat="server"></asp:Calendar>
                              </ContentTemplate>
                          </asp:UpdatePanel>
                      </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="SalirFechasBtn" runat="server" Text="Salir" CssClass="btn btn-secondary" OnClick="SalirFechasBtn_Click"/>

                        <asp:Button ID="SelectFechasBtn" runat="server" Text="Seleccionar Fechas" CssClass="btn btn-success" OnClick="SelectFechasBtn_Click"/>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js"></script>
</asp:Content>
