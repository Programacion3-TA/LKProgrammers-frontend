<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.Master" AutoEventWireup="true" CodeBehind="Estudiantes.aspx.cs" Inherits="WebForm.View.Admin.Estudiantes.Estudiantes" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registro de Estudiantes
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <nav style="--bs-breadcrumb-divider: '>'; font-size: 14px" class="p-2">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <i class="fa-solid fa-house"></i>
            </li>            
            <li class="breadcrumb-item">Estudiantes</li>
        </ol>
    </nav>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="px-2">Estudiantes</h2>

        <hr />

        <div class="container row">
            <div class="col-md-3">
                <!-- Columna -->
                <asp:Label ID="LblBuscar" runat="server" Text="Buscar estudiante: " CssClass="form-label" style="font-size: 20px;"></asp:Label>
            </div>
            <div class="col-md-6">
                <!-- Columna -->                    
                <asp:TextBox ID="TxtCriterioBusqueda" runat="server" CssClass="form-control" Enabled="true" Placeholder="Ingresar nombres, apellidos, DNI o código de estudiante..."></asp:TextBox>
        
            </div>
            <div class="col-md-2">
                <!-- Columna -->                    
                    <asp:LinkButton ID="LkBtnBuscar" runat="server" Text=" Buscar"
                        CssClass="btn btn-primary" OnClick="LkBtnBuscar_Click"/>
            </div>
        </div>
        <hr />

        <div class="container">
            
            <div class="container row">

                <div class="p-3 mx-auto col-md-6">
                    <!-- Columna -->
                    <asp:LinkButton ID="BtnRestaurar" runat="server" Text="Mostrar todos"
                        CssClass="btn btn-outline-success" Visible="false" OnClick="BtnRestaurar_Click"/>                                
                </div>

                <div class="text-end p-3 mx-auto col-md-6">
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Nuevo Estudiante"
                        CssClass="btn btn-success"
                        OnClick="BtnNuevo_Click"
                         />
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridAlumnos" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped"
                    OnPageIndexChanging="GridAlumnos_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="codigoAlumno" HeaderText="Codigo"/>
                        <asp:BoundField DataField="dni" HeaderText="Dni" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellidoPaterno" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="apellidoMaterno" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="correoElectronico" HeaderText ="Correo electrónico" />                        
                        <asp:BoundField DataField="grado" HeaderText="Grado" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text="Editar" CssClass="btn btn-warning"
                                CommandArgument='<%# Eval("codigoAlumno") %>' OnClick="EditRow_Click"/>
                                <asp:LinkButton runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                CommandArgument='<%# Eval("codigoAlumno") %>' OnClick="DelRow_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="LblNoAlumnos" runat="server" Text="No se han encontrado alumnos." CssClass="form-label" style="font-size: 20px;" Visible= "false"></asp:Label>
            </div>
        </div>

        <!--Modal Alumno: Agregar, edita, ver -->
        <div id="modalAgregarEstudiante" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Alumno</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container p-3 ">
                            <div class="col-md-12  mb-3">
                                <h6>Información Personal</h6>
                            </div>
                            
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblCode" runat="server" Text="Código" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblDNI" runat="server" Text="DNI" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtDNI" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblApellidoPat" runat="server" Text="Apellido paterno" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtApellidoPat" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblApellidoMat" runat="server" Text="Apellido materno" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtApellidoMat" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblGenero" runat="server" Text="Genero" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="DDGenero" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div  class="col-md-12  mb-3">
                                <asp:Label ID="LblDireccion" runat="server" Text="Dirección" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control" ></asp:TextBox>    
                            </div>

                            <div  class="col-md-12  mb-3">
                                <asp:Label ID="LblCorreo" runat="server" Text="Correo electrónico" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                
                            </div>

                            <div  class="col-md-12  mb-3">
                                <asp:Label ID="LblUsuario" runat="server" Text="Usuario de Intranet" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtUsuario" runat="server" CssClass="form-control" ></asp:TextBox>    
                            </div>

                             <div  class="col-md-12  mb-3">
                                 <asp:Label ID="LblContrasenia" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                                 <asp:TextBox ID="TxtContrasenia" runat="server" CssClass="form-control" ></asp:TextBox>    
                             </div>

                            <div  class="col-md-12  mb-3">
                                <asp:Label ID="LblTelefono" runat="server" Text="Telefono" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control" ></asp:TextBox>
                            </div>

                            <div  class="col-md-12  mb-3">
                                <asp:Label ID="Lbl" runat="server" Text="Fecha de nacimiento" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtFechaNac" runat="server" CssClass="form-control"  TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-12  mb-3">
                                <h6>Información Académica</h6>
                            </div>
                            
                            <div >
                                <asp:Label ID="LblGrado" runat="server" Text="Grado" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="SLGrado" runat="server" CssClass="form-select" AutoPostBack="false">
                                    <asp:ListItem Value="INI2">2do-Inicial</asp:ListItem>
                                    <asp:ListItem Value="INI3">3ro-Inicial</asp:ListItem>
                                    <asp:ListItem Value="INI4">4to-Inicial</asp:ListItem>
                                    <asp:ListItem Value="INI5">5to-Inicial</asp:ListItem>
                                    <asp:ListItem Value="PRIM1">1ro-Primaria</asp:ListItem>
                                    <asp:ListItem Value="PRIM2">2do-Primaria</asp:ListItem>
                                    <asp:ListItem Value="PRIM3">3ro-Primaria</asp:ListItem>
                                    <asp:ListItem Value="PRIM4">4to-Primaria</asp:ListItem>
                                    <asp:ListItem Value="PRIM5">5to-Primaria</asp:ListItem>
                                    <asp:ListItem Value="PRIM6">6to-Primaria</asp:ListItem>
                                </asp:DropDownList>
                            </div> 
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-primary"
                            OnClick="BntGuardar_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal (Avisos) -->
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
                        OnClick="BtnAceptarEliminar_Click"/>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
   <script src="EstudianteAgregar.js"></script>
</asp:Content>

