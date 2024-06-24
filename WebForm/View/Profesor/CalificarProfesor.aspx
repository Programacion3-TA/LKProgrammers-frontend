<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="CalificarProfesor.aspx.cs" Inherits="WebForm.View.Profesor.CalificarProfesor" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Calificar cursos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://polyfill.io/v3/polyfill.min.js?features=es6"></script>
    <script id="MathJax-script" async src="https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js"></script>
    
    <h1>Calificar notas de cursos</h1>
    <div class="card">
        <div class="card-header">
            <div class="col">
                <h2>Ver notas de alumno del curso</h2>
                <div>
                    <label>Seleccione alumno:</label>
                    <asp:DropDownList ID="AlumnosDelCursoDrpl" runat="server" AutoPostBack="true"
                       CssClass="form-select w-50" DataTextField="nombres" DataValueField="dni" OnSelectedIndexChanged="AlumnosDelCursoDrpl_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <asp:Button ID="ReporteNotasAlumnoBtn" runat="server" Text="Obtener notas actuales" CssClass="btn btn-primary"  
                    OnClick="ReporteNotasAlumnoBtn_Click" OnClientClick="openinNewTab()"/>
            </div>
            <hr />
            <div class="col">
                <label>Seleccione un curso dictado:</label>
                <asp:DropDownList ID="CursoDictadoDrpL" runat="server" AutoPostBack="true"
                CssClass="form-select w-50" DataTextField="cursoDescrip" DataValueField="cursoIdent"
                OnSelectedIndexChanged="CursoDictadoDrpL_SelectedIndexChanged" ></asp:DropDownList>
            </div>
        </div>
        <div class="card-body">
            <div>
                <asp:GridView ID="CompetenciaCursosGrid" runat="server"
                    AutoGenerateColumns="false" 
                    CssClass="table table-hover table-responsive table-striped"
                    AllowPaging="true"
                    PageSize="5"
                    OnPageIndexChanging="CompetenciaCursosGrid_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="ID de Competencia" DataField="id" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField HeaderText="Descripcion de Competencia" DataField="descripcion" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField HeaderText="Peso" DataField="peso" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="AsignarNotaBtn" CssClass="btn btn-primary"
                                    Text="Asignar nota"
                                    OnClick="AsignarNotaBtn_Click"
                                    CommandArgument='<%#Eval("id") %>'
                                    />
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:TemplateField>
                    </Columns>
                        <HeaderStyle HorizontalAlign="Center"/>
                </asp:GridView>

            </div>
        </div>

        <!--<asp:Label ID="FormulaPesos" runat="server" Text="Hubo un error al mostrar la formula"></asp:Label>-->
    </div>

    

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js"></script>
    <script>
        function openinNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () {
                window.document.forms[0].target= '';
            }, 0);
        }
    </script>
</asp:Content>
 