<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.Master" AutoEventWireup="true" CodeBehind="Profesores.aspx.cs" Inherits="WebForm.View.Admin.Profesores.Profesores" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registro de profesores
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <nav style="--bs-breadcrumb-divider: '>'; font-size: 14px" class="p-2">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <i class="fa-solid fa-house"></i>
            </li>
            <li class="breadcrumb-item">Profesores</li>
            <li class="breadcrumb-item">Listar</li>
        </ol>
    </nav>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="px-2">Listado de Profesores </h2>
        <div class="container">
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Profesor"
                        CssClass="btn btn-success"
                        OnClick="BtnNuevo_Click"/>
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridProfesores" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="codigoProfesor" HeaderText="Código" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellidoPaterno" HeaderText="Apellido Paterno" />
                        <asp:BoundField DataField="apellidoMaterno" HeaderText="Apellido Materno" />
                        <asp:BoundField DataField="especialidad" HeaderText="Especialidad" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="BtnEditar" runat="server" Text="Editar" CssClass="btn btn-warning"
                                    CommandArgument='<%#Eval("codigoProfesor") %>' OnClick="EditRow_Click" />
                                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                    CommandArgument='<%#Eval("codigoProfesor") %>' OnClick="DelRow_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="modalForm" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Profesor</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container p-3 ">
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblCode" runat="server" Text="Código:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                <asp:Label ID="LblEspecialidad" runat="server" Text="Especialidad:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtEspecialidad" runat="server" CssClass="form-control"></asp:TextBox>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="Profesores.js"></script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Navusuarios" runat="server">
    <script src="Profesores.js"></script>
</asp:Content>
