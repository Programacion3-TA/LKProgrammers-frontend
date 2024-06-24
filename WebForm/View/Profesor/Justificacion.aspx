<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="Justificacion.aspx.cs" Inherits="WebForm.View.Profesor.Justificacion" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="card">
        <div class="card-header">
            <h1>Justificación de Asistencias</h1>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridAsistencias" runat="server" AutoGenerateColumns="false" AllowPaging="true"  CssClass="table table-hover table-responsive table-striped">
                <Columns>
                      <asp:BoundField DataField="dniAlumno" HeaderText="DNI"/>
                       <asp:TemplateField>
                           <ItemTemplate>
                               <asp:Label ID="Label1" runat="server" Text='<%#ParsearFecha((DateTime)Eval("fechaHora"))%>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField> 
                        <asp:BoundField DataField="justificacion" HeaderText="Justificacion"/>
                       <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="AprobarJustiBtn" runat="server" Text="Aprobar"  CssClass="btn btn-primary"  OnClick="AprobarJustiBtn_Click"
                               CommandArgument='<%#Eval("dniAlumno")+"|"+Eval("fechaHora") %>'/>
                             <asp:Button ID="NegarJutiBtn" runat="server" Text="Rechazar"  CssClass="btn btn-danger" OnClick="NegarJutiBtn_Click"
                               CommandArgument='<%#Eval("dniAlumno")+"|"+Eval("fechaHora") %>'/>
                         </ItemTemplate>
                      </asp:TemplateField>
                </Columns>

            </asp:GridView>     
        </div>
        <div class="card-footer d-flex justify-content-between">
            <div class="text-end">
                <asp:Button ID="BtnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary" OnClick="BtnRegresar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
