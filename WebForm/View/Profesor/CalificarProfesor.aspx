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
    
    <h1>Calificar cursos</h1>
    <div class="card">
        <div class="card-header">
            <label>Seleccione un curso dictado:</label>
            <asp:DropDownList ID="CursoDictadoDrpL" runat="server" AutoPostBack="true"
                CssClass="form-select" DataTextField="cursoDescrip" DataValueField="cursoIdent"
                OnSelectedIndexChanged="CursoDictadoDrpL_SelectedIndexChanged" OnLoad="CursoDictadoDrpL_SelectedIndexChanged"></asp:DropDownList>
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

        <asp:Label ID="FormulaPesos" runat="server" Text="Hubo un error al mostrar la formula"></asp:Label>
    </div>

    

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js"></script>
</asp:Content>
 