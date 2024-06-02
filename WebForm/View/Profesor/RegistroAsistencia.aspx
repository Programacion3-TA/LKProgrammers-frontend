<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="RegistroAsistencia.aspx.cs" Inherits="WebForm.View.Profesor.RegistroAsistencia" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registro de Asistencia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>
        body{
            min-height:100vh;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h1>Registro de Asistencia</h1>
            </div>
            <div class="card-body">
                <asp:GridView ID="GridAlumnos" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" CssClass="table table-hover table-responsive table-striped" OnRowDataBound="GridAlumnos_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="dni" HeaderText="Código del alumno" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre Completo del alumno"  ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:TemplateField HeaderText="Marcar Asistencia">
                            <ItemTemplate>
                                <asp:RadioButtonList ID="RadAsistencia" runat="server" RepeatDirection="Horizontal" CellPadding="10"
                                    CssClass=" d-flex justify-content-center " OnSelectedIndexChanged="RadAsistencia_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="P" Text="Presente" Selected="True" />
                                    <asp:ListItem Value="T" Text="Tardanza"/>
                                    <asp:ListItem Value="A" Text="Ausente" />
                                </asp:RadioButtonList>
                                <asp:HiddenField ID="HiddenDniAlumno" runat="server" Value='<%#Eval("dni") %>'/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"/>
                </asp:GridView>
            </div>
            <div class="card-footer d-flex justify-content-between">
                <div class="text-end">
                    <asp:Button ID="BtnRegresar" runat="server" Text="Regresar" CssClass="btn btn-primary" OnClick="BtnRegresar_Click" />
                </div>
                <div class="text-end">
                    <asp:Button ID="BtnGuardarAsistencia" runat="server" Text="Guardar Asistencias" CssClass="btn btn-primary" OnClick="BtnGuardarAsistencia_Click" />
                </div>
            </div>
        </div>
    </div>

    <!--Modal para afirmar o negar que realizó el registro asistencias-->
    <!--<div id="modalGuardarAsistencias" class="modal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title">
                          Asistencias registradas
                      </h5>
                  </div>
                  <div class="modal-body">
                      Se registraron exitosamente las asistencias para el dia de hoy!.
                  </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="BtnRegresarAsistencias1" runat="server" CssClass="btn btn-secondary" OnClick="BtnRegresar_Click">Volver</asp:LinkButton>
                </div>                    
            </div>

            </div>
        </div>-->
    
    <div class="modal fade" id="asistenRegistradasModal" tabindex="-1" role="dialog" aria-labelledby="asistenRegistradasModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="asistenRegistradasModalLabel">Asistencias registrada</h5>
                       
                </div>
                <div class="modal-body" id="modalBody">
                      Se registraron exitosamente las asistencias para el dia de hoy!.
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="BtnRegresarAsistencias" runat="server" CssClass="btn btn-primary" OnClick="BtnRegresar_Click">Volver</asp:LinkButton>
                    
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js">
    </script>
</asp:Content>
