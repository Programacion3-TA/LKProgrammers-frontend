<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.master" AutoEventWireup="true" CodeBehind="Historial.aspx.cs" Inherits="WebForm.View.Admin.Historial.Historial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ImagenPerfil" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="container-fluid mt-2 ms-4 me-4 ">
        <h2>Estudiantes eliminados</h2>

        <div>
            <a href="/View/Admin/Estudiantes/Estudiantes.aspx" class="btn btn-primary">Ver lista de estudiantes</a>
        </div>

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
                <div class="col-md-2 d-flex align-items-center gap-2">
                    <asp:LinkButton ID="LkBtnBuscar" runat="server" Text=" Buscar"
                        CssClass="btn btn-primary" OnClick="LkBtnBuscar_Click"/>
                    <asp:LinkButton ID="BtnRestaurar" runat="server" Text="Mostrar todos"
                        CssClass="btn btn-outline-success" OnClick="BtnMostrarTodo_Click"/>
                </div>
            </div>
        </div>
        <hr />

        <asp:Label ID="LblNoAlumnos" runat="server"></asp:Label>
        <asp:GridView ID="GridAlumnosEliminados" runat="server" AutoGenerateColumns="false"
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
                        <asp:Button runat="server" Text="Restaurar" CssClass="btn btn-success"
                        CommandArgument='<%# Eval("dni") %>' OnClick="BtnRestaurar_Click"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
