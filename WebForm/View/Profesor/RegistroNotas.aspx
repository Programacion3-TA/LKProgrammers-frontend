<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="RegistroNotas.aspx.cs" Inherits="WebForm.View.Profesor.RegistroNotas"
    EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registrar Notas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>
    .errorBack{
        background-color: #f8d7da;
        color:#58151c;
        border-color:#f1aeb5;
        border-width:1px;
        border-style:solid;
    }
    .successModal{
        background-color: #d1e7dd;
        color:#0a3622;
        border-color:#a3cfbb;
        border-width:1px;
        border-style:solid;
    }
    </style>
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
             <asp:Panel ID="PanelError" runat="server" Visible="false">
                 <div>
                     Se ingresaron datos incorrectos
                 </div>
             </asp:Panel>

             <asp:GridView ID="GridAlumnos" runat="server" AutoGenerateColumns="false"
                 AllowPaging="true" CssClass="table table-hover table-responsive table-striped" >
                 <Columns>
                     <asp:BoundField DataField="dni" HeaderText="Código del alumno" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:BoundField DataField="nombres" HeaderText="Nombre Completo del alumno"  ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:TemplateField HeaderText="Asignar nota">
                         <ItemTemplate>
                              <asp:TextBox ID="NotaAlumno" runat="server" TextMode="Number" CssClass="form-control"
                                  ></asp:TextBox>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="150px" />
                     </asp:TemplateField>
                 </Columns>
                 <HeaderStyle HorizontalAlign="Center"/>
             </asp:GridView>
         </div>
         <div class="card-footer d-flex justify-content-between">
             <asp:Button ID="CancelaRegistroBtn" runat="server" Text="Cancelar" OnClick="CancelaRegistroBtn_Click"/>
             <asp:Button ID="GuardarNotasBtn" runat="server" Text="Guardar notas" OnClick="GuardarNotasBtn_Click"/>
         </div>
     </div>
 </div>

    <div class="modal fade" id="NotasRegistradasModal" tabindex="-1" role="dialog" aria-labelledby="NotasRegistradasModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content successModal">
            <div class="modal-header">
                <h5 class="modal-title" id="NotasRegistradasModalLabel">Asistencias registrada!</h5>
            </div>
            <div class="modal-body" id="modalBody">
               Usted registró las notas de los alumnos con exito.
            </div>
            <hr />
            <div class="modal-footer">
                <asp:LinkButton ID="BtnRegresarAsistencias" runat="server" CssClass="btn btn-success" OnClick="CancelaRegistroBtn_Click">Volver</asp:LinkButton>
            </div>
        </div>
    </div>
    </div>
    
    
    <div class="modal fade" id="NotasRegistradasErrorModal" tabindex="-1" role="dialog" aria-labelledby="NotasRegistradasErrorModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content errorBack">
            <div class="modal-header">
                <h5 class="modal-title" id="NotasRegistradasErrorModalLabel">Error en ingreso de notas</h5>
            </div>
            <div class="modal-body" id="modalBodyError">
               Parece que algunas notas están ingresándose incorrectamente.Las notas deben estar en un rango válido, generalmente entre 0 y 20. Además, es importante que no contengan caracteres especiales
            </div>
            <hr />
            <div class="modal-footer">
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger" >Entendido</asp:LinkButton>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="asistenciasProfesor.js">
        /*
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

        }*/
    </script>
</asp:Content>
