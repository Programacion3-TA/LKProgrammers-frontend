﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="AsistenciaProfesor.aspx.cs" Inherits="WebForm.View.AsistenciaProfesor.AsistenciaProfesor" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
   <!-- <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Asistencia"/>-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Asistencias del salon</h1>
    <div class="container">
        <div class="container row pb-2 pt-2">
            <div class="col">
                    <div class="input-group mb-3">
                    <asp:TextBox ID="TxtBuscarDias" runat="server" CssClass="form-control" placeholder="Ingrese día de la semana"></asp:TextBox>
                    <div class="input-group-append">
                       <asp:Button ID="BtnBuscarDias" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary"
                           OnClick="BtnBuscarDias_Click"/>
                    </div>
                    </div>
            </div>
            <div>
                <div class="text-end p-3">
                   <asp:LinkButton ID="BtnRegistrarAsistencia" runat="server" CssClass="btn btn-dark"
                       Text="<i class='fa-solid fa-clipboard-user'> </i> Agregar" OnClick="BtnRegistrarAsistencia_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class ="container">
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="GridAsistenciasFechas" runat="server" AutoGenerateColumns="false"
                    AllowPaging ="true" CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="FechaHora" HeaderText ="Fecha de asistencias" />
                        <asp:TemplateField HeaderText="Actualización de asistencias">
                            <ItemTemplate>
                                <asp:Button runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                </asp:GridView>
            </div>
        </div>
    </div>





    <div id="guardar--modal" class="modal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div  class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Guardar cambios</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="submit" class="btn btn-primary">Sí, aceptar</button>
                </div>
            </div>
        </div>
    </div>

    <div id="eliminar--modal" class="modal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">¿Está seguro que quiere eliminar "${elemento}"?</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="submit" class="btn btn-primary">Eliminar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
