<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.Master" AutoEventWireup="true" CodeBehind="Profesores.aspx.cs" Inherits="WebForm.View.Admin.Profesores.Profesores" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registro de profesores
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <%--<nav style="--bs-breadcrumb-divider: '>'; font-size: 14px" class="p-2">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <i class="fa-solid fa-house"></i>
            </li>
            <li class="breadcrumb-item">Profesores</li>            
        </ol>
    </nav>--%>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="px-2">Profesores </h2>

        <div>
            <a href="/View/Admin/Profesores/HistorialProfesor.aspx" class="btn btn-primary">Ver lista de profesores eliminados</a>
        </div>

        <div class="container">
            <hr />

            <div class="container row">
                <div class="col-md-3">
                    <!-- Columna -->
                    <asp:Label ID="LblBuscar" runat="server" Text="Buscar profesor: " CssClass="form-label" style="font-size: 20px;"></asp:Label>
                </div>
                <div class="col-md-6">
                    <!-- Columna -->                    
                    <asp:TextBox ID="TxtCriterioBusqueda" runat="server" CssClass="form-control" Enabled="true" Placeholder="Ingresar nombres, apellidos, DNI o código del profesor..."></asp:TextBox>
        
                </div>
                <div class="col-md-2">
                    <!-- Columna -->                    
                        <asp:LinkButton ID="LkBtnBuscar" runat="server" Text=" Buscar"
                            CssClass="btn btn-primary" OnClick="LkBtnBuscar_Click" />
                </div>
            </div>
            <hr />


            <div class="container row">

                <div class="p-3 mx-auto col-md-6">
                    <!-- Columna -->
                    <asp:LinkButton ID="BtnRestaurar" runat="server" Text="Mostrar todos"
                        CssClass="btn btn-outline-success" OnClick="BtnRestaurar_Click" Visible="false"/>                                
                </div>

                <div class="text-end p-3 mx-auto col-md-6">
                    <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
                        CssClass="btn btn-success"
                        OnClick="BtnNuevo_Click"/>
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridProfesores" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="GridProfesores_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="codigoProfesor" HeaderText="Código" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellidoPaterno" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="apellidoMaterno" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="especialidad" HeaderText="Especialidad" />
                        
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="BtnCursos" runat="server" Text="Cursos" CssClass="btn btn-primary"
                                    CommandArgument='<%#Eval("codigoProfesor") %>' OnClick="BtnCursos_Click" />
                                <asp:Button ID="BtnEditar" runat="server" Text="Editar" CssClass="btn btn-warning"
                                    CommandArgument='<%#Eval("codigoProfesor") %>' OnClick="EditRow_Click" />
                                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                    CommandArgument='<%#Eval("codigoProfesor") %>' OnClick="DelRow_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <!-- Modal insertar, editar profesor -->
        <div id="modalForm" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Profesor</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container p-3">
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblCode" runat="server" Text="Código:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblApellidoPat" runat="server" Text="Apellido paterno:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtApellidoPat" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblApellidoMat" runat="server" Text="Apellido materno:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtApellidoMat" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-12 mb-3">
                                    <asp:Label ID="LblDireccion" runat="server" Text="Dirección:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-6 mb-3">
                                    <asp:Label ID="LblGenero" runat="server" Text="Género:" CssClass="form-label"></asp:Label>
                                    <asp:DropDownList ID="DDGenero" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblCorreo" runat="server" Text="Correo electrónico:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtCorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblUsername" runat="server" Text="Nombre de usuario:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblPassword" runat="server" Text="Contraseña:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblTelefono" runat="server" Text="Teléfono:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblFechaNacimiento" runat="server" Text="Fecha de Nacimiento:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblDNI" runat="server" Text="DNI del Profesor:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtDNI" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="LblEspecialidad" runat="server" Text="Especialidad:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="TxtEspecialidad" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>                            
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="ButGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"
                            OnClick="ButGuardar_Click" OnClientClick="return validarFormulario();" />
                    </div>
                </div>
            </div>
        </div>

    </div>
    <!-- modal de cursos dictados -->
    <div id="modalCursosDictados" class="modal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <asp:Label ID="LblCursosProfe" runat="server" CssClass="form-label" Text="Cursos"></asp:Label>
                    </h5>                    
                </div>
                <div class="modal-body">
                    <div class="container p-3">
                        <div class="row">
                           <h5>Cursos Actualmente Dictando</h5>
                            <asp:GridView ID="GVCursosDictados" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="5"
                                CssClass="table table-hover table-responsive table-striped">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Código" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEliminarCurso" runat="server" Text="Eliminar" CssClass="btn btn-warning"
                                                CommandArgument='<%#Eval("id") %>' OnClick="BtnEliminarCurso_Click"  />                                                
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="LblNoCursosDictados" runat="server" Text="El profesor no dicta ningún curso." CssClass="form-label" Visible="false"></asp:Label>
                            
                            <hr />

                            <h5>Agregar Cursos</h5>
                            <asp:Label ID="LblBuscarCurso" runat="server" Text="Buscar Curso" CssClass="form-label"></asp:Label>
                            <div class="row">
                                <div class="col-sm-10">
                                    <asp:TextBox ID="TxtCriterioCursos" runat="server" CssClass="form-control" Placeholder="Ingrese el nombre o código del curso..."></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscar_Click" UseSubmitBehavior="false" />
                                </div>
                            </div>
                            <br />
                            <asp:GridView ID="GVCursosEncontrados" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="5"
                                CssClass="table table-hover table-responsive table-striped">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Código" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnAgregarCurso" runat="server" Text="Agregar" CssClass="btn btn-warning"
                                                CommandArgument='<%#Eval("id") %>' OnClick="BtnAgregarCurso_Click"  />                                                
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="LblNoCursosDisp" runat="server" Text="No existen cursos disponibles." CssClass="form-label" Visible="false"></asp:Label>
                        </div>
                     </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>                    
                </div>
            </div>
        </div>
    </div>


    <!-- modal de warning -->
    <div id="modalWarning" class="modal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <asp:Label ID="LblWarning" runat="server" CssClass="form-label"></asp:Label>
                    </h5>                    
                </div>
                <div class="modal-body">
                    <div class="container p-3 ">
                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblMensaje" runat="server" CssClass="form-label"></asp:Label>
                        </div>
                     </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="BtnAceptarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary"
                        OnClick="BtnAceptarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="Profesores.js"></script>
</asp:Content>
