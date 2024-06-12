<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAlumno.Master" AutoEventWireup="true" CodeBehind="AsistenciaAlumno.aspx.cs" Inherits="WebForm.View.AsistenciaAlumno.AsistenciaAlumno" 
    EnableEventValidation="false"%>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Mis Asistencias
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>
    .row-style{
        text-align:center;
    }
   .JustifyButton{
       text-decoration:none;
       background-color:#0D2F37;
       color:white;
       border-radius:15px;
   }
   .JustifyButton:hover{
       background-color:#0d2f37bf;
   }
  


        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Asistencia"/>        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid mt-2 ms-4 me-4 ">
      <h2 class="mb-4 text-center text-md-start ">Asistencias por día</h2>
        <asp:GridView ID="GriAsistenciasAlumnos" runat="server"
        AutoGenerateColumns ="false" AllowPaging="true"
        PageSize="30" CssClass="table table-hover table-responsive table-striped "
            HeaderStyle-HorizontalAlign="Center">
        <Columns>
            <asp:BoundField DataField="fechaFormato" HeaderText="Fecha Asistencia" ItemStyle-CssClass="col-1" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="estado" HeaderText="Asistencia" ItemStyle-CssClass="col-1"/>
            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-1 " ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="JustificarBtn" runat="server" Text="Justificar la asistencia"
                        Enabled='<%# Eval("estado").ToString() != "Presente" %>'
                        CssClass="btn btn-primary" OnClick="JustificarBtn_Click"
                        CommandArgument='<%#Eval("fechaHora")%>'/>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"  />
            </asp:TemplateField>
        </Columns>
         <RowStyle CssClass="row-style" />
         <AlternatingRowStyle CssClass="row-style" />
        </asp:GridView>
    </div>

    <!--Modal-->

   <div class="modal fade" id="JustificarAsistenciaModal" tabindex="-1" role="dialog" aria-labelledby="JustificarAsistenciaModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="JustificarAsistenciaModalLabel">Justificar la asistencia</h5>
      </div>
      <div class="modal-body">
          <asp:Label ID="" runat="server" Text="Introdusca el motivo: " CssClass="form-label"></asp:Label>
          <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Columns="30" Rows="5" CssClass="form-control"></asp:TextBox>
      </div>
      <div class="modal-footer">
          <asp:Button ID="CerrarModalBtn" runat="server" Text="Cerrar" CssClass="btn btn-secondary"/>
          <asp:Button ID="EnviarJustifiBtn" runat="server" Text="Enviar Justificación" CssClass="btn btn-primary" OnClick="EnviarJustifiBtn_Click"/>
      </div>
    </div>
  </div>
</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="Alumno.js"></script>
</asp:Content>
