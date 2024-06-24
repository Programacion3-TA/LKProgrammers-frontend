<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.master" AutoEventWireup="true" CodeBehind="HistorialProfesor.aspx.cs" Inherits="WebForm.View.Admin.Profesores.Historial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mx-auto d-flex flex-column justify-content-center">
        <div class="container">
            <h2 class="px-2">Profesores Eliminados</h2>

            <div>
                <a href="/View/Admin/Profesores/Profesores.aspx" class="btn btn-primary">Ver lista de profesores</a>
            </div>

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
                <div class="col-md-2 d-flex align-items-center gap-2">
                    <!-- Columna -->                    
                    <asp:LinkButton ID="LkBtnBuscar" runat="server" Text=" Buscar"
                        CssClass="btn btn-primary" OnClick="LkBtnBuscar_Click" />
                    <asp:LinkButton ID="BtnRestaurar" runat="server" Text="Mostrar todos"
                        CssClass="btn btn-outline-success" OnClick="BtnMostrarTodo_Click"/>  
                </div>
            </div>
            <hr />

            <div class="container row">
                <asp:Label ID="LblNoProfesor" runat="server"></asp:Label>
                <asp:GridView ID="GridProfesoresEliminados" runat="server" AutoGenerateColumns="false"
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
                                <asp:Button ID="BtnRestaurarEliminado" runat="server" Text="Restaurar" CssClass="btn btn-success"
                                    CommandArgument='<%#Eval("dni") %>' OnClick="BtnRestaurar_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
