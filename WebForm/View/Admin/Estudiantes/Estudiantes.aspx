<%@ Page EnableEventValidation="true" Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.Master" AutoEventWireup="true" CodeBehind="Estudiantes.aspx.cs" Inherits="WebForm.View.Admin.Estudiantes.Estudiantes" %>
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
            <li class="breadcrumb-item">Alumnos</li>
            <li class="breadcrumb-item">Listar</li>
        </ol>
    </nav>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="px-2">Listado de Estudiantes </h2>

        <div class="container">
            <!--Boton de agregar un alumno al colegio-->
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
                        CssClass="btn btn-success"
                        OnClick="BtnNuevo_Click"
                         />
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridAlumnos" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo"/>
                        <asp:BoundField DataField="Dni" HeaderText="Dni" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="ApellidoPat" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="ApellidoMat" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="Correo" HeaderText ="Correo electrónico" />
                        <asp:BoundField DataField="Telefono" HeaderText ="Telefono" />
                        <asp:BoundField DataField="Grado" HeaderText="Grado" />
                        <asp:TemplateField HeaderText=" ">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text="Editar" CssClass="btn btn-warning"
                                CommandArgument='<%# Eval("Codigo") %>' OnClick="EditRow_Click"/>
                                <asp:LinkButton runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                CommandArgument='<%# Eval("Codigo") %>' OnClick="DelRow_Click"/>
                                                        
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!--Modal-->
        <div id="modalForm" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Alumno</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container p-3 ">
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblCode" runat="server" Text="Código:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblDNI" runat="server" Text="DNI:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtDNI" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblApellidoPat" runat="server" Text="Apellido paterno:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtApellidoPat" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblApellidoMat" runat="server" Text="Apellido materno:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtApellidoMat" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="LblCorreo" runat="server" Text="Correo electrónico:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCorreo" runat="server" CssClass="form-control" ></asp:TextBox>
                                
                            </div>
                            <div>
                                <asp:Label ID="LblTelefono" runat="server" Text="Telefono:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control" ></asp:TextBox>
                            </div>
                            <div>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script>
        function showModalForm() {
            var modalForm = new bootstrap.Modal(document.getElementById('modalForm'));
            modalForm.toggle();
        }     
    </script>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="Navusuarios" runat="server">
  
</asp:Content>













