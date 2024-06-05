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
        <h2 class="px-2">Salon <asp:Literal ID="LitSalonId" runat="server" /> </h2>
        <div class="container">
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Alumno"
                        CssClass="btn btn-success" />
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridSalones" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
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
                                <div style="display: flex; gap: 0.6em">
                                    <asp:Button ID="BtnVer" runat="server" Text="Ver" CssClass="btn btn-primary"
                                        CommandArgument='<%#Eval("codigoAlumno") %>' />
                                </div>

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
