<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.Master" AutoEventWireup="true" CodeBehind="SalonDetalle.aspx.cs" Inherits="WebForm.View.Admin.Salones.SalonDetalle" %>

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
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
    <script src="SalonDetalle.js"></script>
</asp:Content>
