<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.master" AutoEventWireup="true" CodeBehind="CursosAdmin.aspx.cs" Inherits="WebForm.View.Admin.Cursos.CursosAdmin" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Vista de Cursos Vigentes
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
            <li class="breadcrumb-item">Cursos Vigentes</li>
        </ol>
    </nav>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="px-2">Cursos</h2>
        <hr />
        <div class="container">
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnNuevo" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Nuevo Curso"
                        CssClass="btn btn-success"
                        OnClick="BtnNuevo_Click" />
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridCursos" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Código" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="d-flex gap-2">
                                    <asp:Button runat="server" Text="Editar/Ver" CssClass="btn btn-warning"
                                        CommandArgument='<%# Eval("id") %>' OnClick="EditRow_Click" />
                                    <asp:Button runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                        CommandArgument='<%# Eval("id") %>' OnClick="DelRow_Click" OnClientClick="return confirm('¿Está seguro de eliminar este curso?');" />
                                    <asp:Button ID="BtnVerCompetencias" runat="server" Text="Editar competencias" CssClass="btn btn-success"
                                        CommandArgument='<%#Eval("id") %>' OnClick="BtnCompetencias_Click" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div id="modalCurso" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Curso</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container p-3 ">
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblCode" runat="server" Text="Código" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblDescripcion" runat="server" Text="Descripción" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtDescripción" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnGuardar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
    <script src="Curso.js"></script>
</asp:Content>
