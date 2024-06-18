<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.Master" AutoEventWireup="true" CodeBehind="SalonDetalle.aspx.cs" Inherits="WebForm.View.Admin.Salones.SalonDetalle" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Vista de salon
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ImagenPerfil" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <nav style="--bs-breadcrumb-divider: '>'; font-size: 14px" class="p-2">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <i class="fa-solid fa-house"></i>
            </li>
            <li class="breadcrumb-item">Salones</li>
            <li class="breadcrumb-item">Detalle</li>
        </ol>
    </nav>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="px-2">Salon
            <asp:Literal ID="LitSalonId" runat="server" />
        </h2>
        <div class="container">
            <div class="container row">
                <h2>Tutor
                </h2>
                <asp:GridView ID="GridTutor" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="codigoProfesor" HeaderText="Código" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellidoPaterno" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="apellidoMaterno" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="especialidad" HeaderText="Especialidad" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="container row">
                <h2>Alumnos
                </h2>
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Alumno"
                        CssClass="btn btn-success" OnClick="BtnAgregar_Click" />
                </div>
            </div>
            <div class="container row">
                <asp:GridView ID="GridAlumnosSalon" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="codigoAlumno" HeaderText="Codigo" />
                        <asp:BoundField DataField="dni" HeaderText="Dni" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellidoPaterno" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="apellidoMaterno" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="correoElectronico" HeaderText="Correo electrónico" />
                        <asp:BoundField DataField="grado" HeaderText="Grado" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div style="display: flex; gap: 0.6em">
                                    <asp:LinkButton ID="BtnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger"
                                        CommandArgument='<%#Eval("dni") %>' OnClick="BtnQuitar_Click" />
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Sección de Cursos -->
    <div class="container">
        <div class="container row">
            <h2>Cursos</h2>
            <div class="text-end p-3 mx-auto">
                <asp:LinkButton ID="BtnAgregarCurso" runat="server" Text="<i class='fas fa-plus pe-2'></i> Agregar Curso"
                    CssClass="btn btn-success" OnClick="BtnAgregarCurso_Click" />
            </div>
            <asp:GridView ID="GridCursos" runat="server" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="5"
                CssClass="table table-hover table-responsive table-striped">
                <Columns> 
                    <asp:BoundField DataField="idCurso" HeaderText="ID" />
                    <asp:BoundField DataField="nombreCurso" HeaderText="Nombre" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div style="display: flex; gap: 0.6em">
                                <asp:LinkButton ID="BtnEliminarCurso" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                    CommandArgument='<%#Eval("idCurso") %>' OnClick="BtnEliminarCurso_Click" />
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <!-- Fin de la Sección de Cursos -->
    <div id="modalSalonDetalle" class="modal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Salón</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="TxtFiltroAlumno" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lbBuscarAlumnoSalon" OnClick="lbBuscarAlumno_Click" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" UseSubmitBehavior="false" />
                            <div class="container">
                                <asp:GridView ID="gvAlumnosResult" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped">
                                    <Columns>
                                        <asp:BoundField DataField="grado" HeaderText="Grado" />
                                        <asp:BoundField DataField="dni" HeaderText="Dni" />
                                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-check'></i> Seleccionar" CommandArgument='<%# Eval("dni") %>' OnClick="BtnSeleccionarAlumno_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal para agregar salon -->
     <!-- Modal Personal Administrativo (Editar, Ver, Agregar)-->
    <div id="modalAgregarCurso" class="modal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar curso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body container p-3">
                    
                        <div class="col-md-12  mb-3">
                            <h6>Seleccionar curso</h6>
                        </div>


                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblBuscarCurso" runat="server" Text="Buscar curso" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtCriterioBusquedaCurso" runat="server" CssClass="form-control" Enabled="true" Placeholder="Ingresar nombre del curso..."></asp:TextBox>
                            <br />
                            <asp:Button ID="BtnBuscarCurso" runat="server" Text="Buscar Curso" CssClass="btn btn-primary" OnClick="BtnBuscarCurso_Click"/>
                        </div>

                        
                        <div class="col-md-12  mb-3">
                            <asp:GridView ID="GVCursos" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped">
                                <Columns>  
                                    <asp:BoundField DataField="idCurso" HeaderText="ID" />
                                    <asp:BoundField DataField="nombreCurso" HeaderText="Nombre" />                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-check'></i> Seleccionar" CommandArgument='<%# Eval("idCurso") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                         <div class="col-md-12  mb-3">
                             <asp:Label ID="LblCursoID" runat="server" Text="Curso" CssClass="form-label" Visible="false"></asp:Label>
                             <asp:TextBox ID="TxtCursoID" runat="server" CssClass="form-control" Enabled="false" Visible="false" ></asp:TextBox>
                             <asp:Label ID="LblNombreCurso" runat="server" Text="Nombre" CssClass="form-label" Visible="false"></asp:Label>
                            <asp:TextBox ID="TxtNombreCurso" runat="server" CssClass="form-control" Enabled="false" Visible="false" ></asp:TextBox>
                         </div>
                        

                        <div class="col-md-12  mb-3">
                            <h6>Seleccionar horario</h6>
                        </div>

                    <div class="col-md-12  mb-3">
                        <asp:Label ID="LblDia" runat="server" Text="Día" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="DDDía" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Lunes">Lunes</asp:ListItem>
                            <asp:ListItem Value="Martes">Martes</asp:ListItem>
                            <asp:ListItem Value="Miercoles">Miércoles</asp:ListItem>
                            <asp:ListItem Value="Jueves">Jueves</asp:ListItem>
                            <asp:ListItem Value="Viernes">Viernes</asp:ListItem>                            
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-12  mb-3">
                    <asp:Label ID="LblHoras" runat="server" Text="Horas a reservar" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TxtHoras" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    <br />
                    <asp:Button ID="BtnBuscarHorario" runat="server" Text="Buscar Horarios" CssClass="btn btn-primary"/>
                    </div>
                    <asp:Label ID="LblHoariosDisp" runat="server" Text="HorariosDisponibles" CssClass="form-label" Visible="false"></asp:Label>
                    <asp:DropDownList ID="DDHorariosDisponibles" runat="server" CssClass="form-select" Visible="false">                                               
                    </asp:DropDownList>

                    <asp:Label ID="LblInicio" runat="server" Text="Hora Inicio" CssClass="form-label" Visible="false"></asp:Label>
                    <asp:TextBox ID="TxtHoraInicio" runat="server" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>

                    <asp:Label ID="LblFin" runat="server" Text="Hora Fin" CssClass="form-label" Visible="false"></asp:Label>
                    <asp:TextBox ID="TxtHoraFin" runat="server" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"/>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

                </div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
    <script src="SalonDetalle.js"></script>
</asp:Content>
