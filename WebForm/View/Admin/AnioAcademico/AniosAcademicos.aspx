<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.master" AutoEventWireup="true" CodeFile="AniosAcademicos.aspx.cs" Inherits="WebForm.View.Admin.AnioAcademico.AniosAcademicos" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Vista de Años Académicos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ImagenPerfil" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <nav style="--bs-breadcrumb-divider: '>'; font-size: 14px" class="p-2">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <i class="fa-solid fa-house"></i>
            </li>
            <li class="breadcrumb-item">Año Academico</li>
        </ol>
    </nav>

    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="form-title">
            Año Escolar Vigente
            <br />
            <asp:Label ID="LblAnioVigente" runat="server" CssClass="form-title-text"></asp:Label>
        </h2>

        <!-- Datos del año academico vigente-->

         <div class="container row">
             <asp:GridView ID="GVAnioVigente" runat="server" AutoGenerateColumns="false"
                 AllowPaging="true" PageSize="5"
                 CssClass="table table-hover table-responsive table-striped">
                 <Columns>
                     <asp:BoundField DataField="id" HeaderText="Código" />
                     <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                     <asp:BoundField DataField="fechaInicio" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                     <asp:BoundField DataField="fechaFin" HeaderText="Fecha de Fin" DataFormatString="{0:dd/MM/yyyy}"/>                     
                     <asp:TemplateField HeaderText="Acciones">
                         <ItemTemplate>
                             <asp:Button runat="server" Text="Editar" CssClass="btn btn-warning"
                                 CommandArgument='<%# Eval("id") %>' OnClick="EditRowClick" />
                             <asp:Button runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                 CommandArgument='<%# Eval("id") %>' OnClick="DelRow_Click" />
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
         </div>


        <hr />

        <h2 class="form-title">Años Académicos                  
        </h2>

        <hr />

        <div class="container">
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnAgregarAnio" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Nuevo Año Académico"
                        CssClass="btn btn-success"
                        OnClick="BtnAgregarAnio_Click" />
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GVAnios" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Código" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="fechaInicio" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField DataField="fechaFin" HeaderText="Fecha de Fin" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField DataField="fechaCerrado" HeaderText="Fecha de Cerrado" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" Text="Editar" CssClass="btn btn-warning"
                                    CommandArgument='<%# Eval("id") %>' OnClick="EditRowClick" />
                                <asp:Button runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                    CommandArgument='<%# Eval("id") %>' OnClick="DelRow_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    
    <!-- Modal Editar -->
    <div id="modalAnioAcademico" class="modal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Año Académico</h5>
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
                            <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                        </div>
                                                
                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha de Inicio" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtFechaInicio" runat="server" CssClass="form-control" Enabled="true" type="date"></asp:TextBox>
                        </div>

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblFechaFin" runat="server" Text="Fecha de Fin Programada" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Enabled="true" type="date"></asp:TextBox>
                        </div>

                        <div class="col-md-12  mb-3">
                            <asp:Label ID="LblFechaCerrado" runat="server" Text="Fecha del Cierre" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="TxtFechaCerrado" runat="server" CssClass="form-control" Enabled="true" type="date"></asp:TextBox>
                        </div>
                          
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"
                        OnClick="BtnGuardar_Click"/>
                </div>
            </div>
        </div>
    </div>
    
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
    <script src="AnioAcademico.js"></script>
</asp:Content>
