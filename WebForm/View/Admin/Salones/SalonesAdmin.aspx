<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAdministrador.master" AutoEventWireup="true" CodeFile="SalonesAdmin.aspx.cs" Inherits="WebForm.View.Admin.Salones.SalonesAdmin" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Vista de salones
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
        </ol>
    </nav>
    <div class="mx-auto d-flex flex-column justify-content-center">
        <h2 class="px-2">Salones</h2>
        <hr />
        <div class="container">
            <div class="container row">
                <div class="text-end p-3 mx-auto">
                    <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar Salón"
                        CssClass="btn btn-success" OnClick="BtnAgregar_Click"/>
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="GridSalones" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5"
                    CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="GridSalones_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Código" />
                        <asp:BoundField DataField="idAnioEscolar" HeaderText="Año Académico" />
                        <asp:BoundField DataField="gradoSalon" HeaderText="Grado del Salón" />
                        <asp:BoundField DataField="tutor.nombres" HeaderText="Tutor" />
                        <asp:BoundField DataField="tutor.apellidoPaterno" HeaderText="" />
                        <asp:BoundField DataField="capacidadMaxima" HeaderText="Capacidad Máxima" />
                        <asp:BoundField DataField="capacidadMinima" HeaderText="Capacidad Mínima" />                       

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div style="display:flex;gap:0.6em">
                                    <asp:Button ID="BtnVer" runat="server" Text="Ver" CssClass="btn btn-primary"
                                    CommandArgument='<%#Eval("id") %>' OnClick="BtnVer_Click" />
                                <asp:Button ID="BtnEditar" runat="server" Text="Editar" CssClass="btn btn-warning"
                                    CommandArgument='<%#Eval("id") %>' OnClick="BtnEditar_Click" />
                                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                    CommandArgument='<%#Eval("id") %>' OnClick="BtnEliminar_Click"  OnClientClick="return confirm('¿Desea eliminar?')" />
                                </div>
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

            
        <!-- Modal Agregar, ver, editar salón -->
        <div id="modalSalon" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Salón</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container p-3 ">                    
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblCode" runat="server" Text="Código" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                    
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblAnioEscolar" runat="server" Text="Año Escolar" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="DDAnioEscolar" runat="server" CssClass="form-select">                                
                                </asp:DropDownList>
                            </div>

                            <div >
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
                    
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblCapMaxima" runat="server" Text="Capacidad Máxima" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCapMaxima" runat="server" CssClass="form-control" Enabled="true" type="number" step="any"></asp:TextBox>
                            </div>
                    
                            <div class="col-md-12  mb-3">
                                <asp:Label ID="LblCapMinima" runat="server" Text="Capacidad Mínima" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtCapMinima" runat="server" CssClass="form-control" Enabled="true" type="number" step="any"></asp:TextBox>
                            </div>
                    
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="LblTutor" runat="server" Text="Tutor" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="DDTutor" runat="server" CssClass="form-select">                                
                                </asp:DropDownList>
                            </div>                        
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnGuardar_Click"/>
                    </div>
                </div>
            </div>
        </div>
    
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
    <script src="Salon.js"></script>
</asp:Content>
