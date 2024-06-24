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
    <asp:Label ID="Pruebas" runat="server"></asp:Label>
    <h1 class="pl-4">Asistencias del salon</h1>
    <div class="container ">
        <div class="container row pb-2 pt-2">
            <div class="card mb-4 p-0">
                
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
                        </div>
                        <div class="d-flex flex-column gap-2">
                            <asp:Label ID="AlumnoLbl" runat="server" Text="Label" CssClass="form-check-label ms-5" Font-Bold="true">Seleccionar Alumno:</asp:Label>
                            <div class="d-flex  justify-content-evenly ">
                                <asp:DropDownList ID="AlumnosDrpDown" runat="server" CssClass="form-select w-75" DataTextField="nombres" DataValueField="dni">
                                </asp:DropDownList>
                                <asp:Button ID="AsistenciasAlumnoBtn" runat="server" Text="Obtener Asistencias" CssClass="btn btn-success" OnClick="AsistenciasAlumnoBtn_Click"/>
                            </div>
                        </div>
                        
                    </div>
            </div>
            <hr />
            <div>
                <div class="text-end p-3 d-flex flex-row-reverse justify-content-between">
                    <div class="d-flex gap-3" >
                        <asp:LinkButton ID="JustificacionesBtn" runat="server" CssClass="btn btn-primary h-75" OnClick="JustificacionesBtn_Click">Justificaciones
                            <asp:Label ID="CantidadJusitficacionesLbl" runat="server" Text="" CssClass="badge text-bg-danger" ></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="BtnRegistrarAsistencia" runat="server" CssClass="btn btn-dark d-flex gap-2 end-0 align-items-center h-75"
                            Text="<i class='fa-solid fa-clipboard-user'> </i> Agregar Asistencia " OnClick="BtnRegistrarAsistencia_Click">
                        </asp:LinkButton>
                    </div>
                    
                    <div class="input-group mb-3 w-50 d-flex gap-3 align-items-center fw-bolder">
                        <asp:Label ID="FiltroLbl" runat="server" Text="Filtrar por mes: "></asp:Label>
                        <asp:DropDownList ID="MesesDropDown" runat="server" CssClass="form-select">
                            <asp:ListItem Value="">Seleccione un mes</asp:ListItem>
                            <asp:ListItem Value="1"  >Enero</asp:ListItem>
                            <asp:ListItem Value="2">Febrero</asp:ListItem>
                            <asp:ListItem Value="3">Marzo</asp:ListItem>
                            <asp:ListItem Value="4">Abril</asp:ListItem>
                            <asp:ListItem Value="5">Mayo</asp:ListItem>
                            <asp:ListItem Value="6">Junio</asp:ListItem>
                            <asp:ListItem Value="7">Julio</asp:ListItem>
                            <asp:ListItem Value="8">Agosto</asp:ListItem>
                            <asp:ListItem Value="9">Septiembre</asp:ListItem>
                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
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
                        <%--<asp:BoundField DataField="FechaFormato" HeaderText ="Fecha de asistencias" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>--%>
                        <asp:TemplateField HeaderText ="Fecha de asistencias" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# ParsearFecha((DateTime)Eval("Date")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" CssClass="btn btn-primary" Text="Editar" ID="editarAsistencia"
                                    CommandArgument='<%#Eval("Date") %>' OnClick="editarAsistencia_Click"/>

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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!--Modal para el ingreso de fechas-->
    <div class="modal fade" id="JustificacionModal" tabindex="-1" role="dialog" aria-labelledby="JustificacionModalTitle" aria-hidden="true">
    <div class="modal-dialog" role="document">
    <div class="modal-content">
        <div class="modal-header">
        <h5 class="modal-title" id="JustificacionModalTitle">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
        </div>
        <div class="modal-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="AsistenciasSinJustificarGrid" runat="server">
                        <Columns>
                            <asp:BoundField DataField="dniAlumno" HeaderText="DNI"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# ParsearFecha((DateTime)Eval("fechaHora")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="justificacion" HeaderText="Justificacion"/>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="Button2" runat="server" Text="Aprobar" />
                                    <asp:Button ID="Button1" runat="server" Text="Rechazar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
        </div>
    </div>
    </div>
    </div>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js"></script>
</asp:Content>
