<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.master" AutoEventWireup="true" CodeBehind="CompetenciasCurso.aspx.cs" Inherits="WebForm.View.Admin.Cursos.CompetenciasCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Competencias del Curso
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
            <li class="breadcrumb-item">Competencias del Curso</li>
        </ol>
    </nav>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2>Competencias de
            <asp:Literal ID="LtCurso" runat="server" /></h2>
        <div class="container">
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnAgregarCompetencia" runat="server" CssClass="btn btn-primary" Text="Agregar Competencia" OnClick="BtnAgregar_Click" />
                </div>
            </div>
            <div class="container row">
                <asp:GridView ID="GridTutor" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="container row">
                <asp:GridView ID="GridCompetenciasCurso" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Codigo" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                        <asp:BoundField DataField="peso" HeaderText="Peso" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnEdit" runat="server" Text="Editar" CssClass="btn btn-warning" OnClick="BtnEditar_Click" CommandArgument='<%#Eval("id") %>'/>
                                <asp:LinkButton ID="BtnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger" OnClick="BtnQuitar_Click" OnClientClick="return confirm('¿Desea eliminar?')" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="modalCompetencia" class="modal" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Agregar Competencia</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <!-- Add your modal content here -->
                <!-- Example: -->.
                <asp:Label ID="LblCode" runat="server" Text="ID" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtID" runat="server" placeholder="ID" CssClass="form-control mb-2" Enabled="false"></asp:TextBox>

                <asp:Label ID="LblDesc" runat="server" Text="Descripción" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtDescripcion" runat="server" placeholder="Descripción" CssClass="form-control mb-2"></asp:TextBox>
                <asp:Label ID="LlbPeso" runat="server" Text="Peso" CssClass="form-label"  ></asp:Label>
                <asp:TextBox ID="TxtPeso" runat="server" placeholder="Peso" CssClass="form-control mb-2"></asp:TextBox>
                <asp:Button ID="BtnGuardarCompetencia" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnGuardarCompetencia_Click" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
    <script src="./CompetenciasCurso.js"></script>
</asp:Content>
