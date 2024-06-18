<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="RegistroNotas.aspx.cs" Inherits="WebForm.View.Profesor.RegistroNotas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registrar Notas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container">
     <div class="card">
         <div class="card-header">
             <h1>Registrar notas</h1>
         </div>
         <div class="card-body">
             <asp:GridView ID="GridAlumnos" runat="server" AutoGenerateColumns="false"
                 AllowPaging="true" CssClass="table table-hover table-responsive table-striped" >
                 <Columns>
                     <asp:BoundField DataField="dni" HeaderText="Código del alumno" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:BoundField DataField="nombres" HeaderText="Nombre Completo del alumno"  ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:TemplateField HeaderText="Asignar nota">
                         <ItemTemplate>
                              <asp:TextBox ID="NotaAlumno" runat="server" TextMode="Number" CssClass="form-control"
                                  onkeyup="mostrarErrorInput(textbox)" ></asp:TextBox>
                               <p>Elemento hermano</p>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="150px" />
                     </asp:TemplateField>
                 </Columns>
                 <HeaderStyle HorizontalAlign="Center"/>
             </asp:GridView>
         </div>
         <div class="card-footer d-flex justify-content-between">
             <asp:Button ID="CancelaRegistroBtn" runat="server" Text="Cancelar" />
             <asp:Button ID="GuardarNotasBtn" runat="server" Text="Guardar notas" />
         </div>
     </div>
 </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script>
        function mostrarErrorInput(textbox) {
            const input = textbox
            const spanInput = textbox.nextElementSibling;
            const expReg = /[\.\-\+\e]/gi;
            spanInput.innerText = "cambia"
            if (!expReg.test(input.value)) {
                spanInput.innerText = "si   ";
                alert(input.value);
            }
            else {
                alert("NO")
                spanInput.innerText = "Ingrese una nota válida";
            }

        }
    </script>
</asp:Content>
