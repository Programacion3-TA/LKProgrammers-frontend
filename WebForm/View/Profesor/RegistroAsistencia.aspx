<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="RegistroAsistencia.aspx.cs" Inherits="WebForm.View.Profesor.RegistroAsistencia"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registro de Asistencia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
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
                    AllowPaging="true" CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="dni" HeaderText="Código del alumno" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre Completo del alumno"  ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                        <asp:TemplateField HeaderText="Marcar Asistencia">
                            <ItemTemplate>
                                <asp:HiddenField ID="HiddenDniAlumno" runat="server" Value='<%#Eval("dni") %>' />

                                <asp:RadioButtonList ID="RadAsistencia" runat="server" RepeatDirection="Horizontal" CellPadding="10"
                                    CssClass=" d-flex justify-content-center " OnSelectedIndexChanged="RadAsistencia_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="P" Text="Presente" />
                                    <asp:ListItem Value="T" Text="Tardanza" />
                                    <asp:ListItem Value="A" Text="Falta"/>
                                </asp:RadioButtonList>
                                <!--Asegura que se elija almenos uno-->
                                <asp:RequiredFieldValidator ID="rfvRadAsistencia" runat="server" ControlToValidate="RadAsistencia"
                                InitialValue="" ErrorMessage="Seleccione una opción" CssClass="text-danger" Display="Dynamic" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="350px" />
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
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
