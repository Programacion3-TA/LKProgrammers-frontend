<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.master" AutoEventWireup="true" CodeBehind="AdministrativoAdmin.aspx.cs" Inherits="WebForm.View.Admin.Administrativo.AdministrativoAdmin" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Vista de personal administrativo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <nav style="--bs-breadcrumb-divider: '>'; font-size: 14px" class="p-2">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <i class="fa-solid fa-house"></i>
            </li>
            <li class="breadcrumb-item">Personal Administrativo</li>
        </ol>
    </nav>

    <div class="container">
        <div class="mx-auto d-flex flex-column justify-content-center">
            <h2 class="px-2">Personal Administrativo</h2>
            <hr />
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnNuevo" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Nuevo Personal"
                        CssClass="btn btn-success" OnClick="BtnNuevo_Click" />
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridAdministrativo" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="codigoPersonal" HeaderText="Código" />                        
                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellidoPaterno" HeaderText="Apellido Paterno" />                        
                        <asp:BoundField DataField="correoElectronico" HeaderText="Correo electrónico" />
                        <asp:BoundField DataField="puestoEjecutivo" HeaderText="Puesto de trabajo" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text="Editar" CssClass="btn btn-warning"
                                    CommandArgument='<%# Eval("codigoPersonal") %>' OnClick="EditRow_Click" />
                                <asp:LinkButton runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                    CommandArgument='<%# Eval("codigoPersonal") %>' OnClick="DelRow_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Modal Personal Administrativo -->
    <div id="modalAdministrativo" class="modal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Personal Administrativo</h5>
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

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblDireccion" runat="server" Text="Dirección" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblCorreo" runat="server" Text="Correo electrónico" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtCorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>

                        </div>

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblUsuario" runat="server" Text="Usuario de Intranet" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblContrasenia" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtContrasenia" runat="server" CssClass="form-control"  TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblTelefono" runat="server" Text="Telefono" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="Lbl" runat="server" Text="Fecha de nacimiento" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtFechaNac" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-12  mb-3">
                            <h6>Información Profesional</h6>
                        </div>

                        <div>
                           <asp:Label ID="LblPuesto" runat="server" Text="Puesto de Trabajo" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtPuesto" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"
                        OnClick="BtnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
     <script src="Administrativo.js"></script>
</asp:Content>
