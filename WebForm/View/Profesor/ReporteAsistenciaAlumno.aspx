<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.master" AutoEventWireup="true" CodeBehind="ReporteAsistenciaAlumno.aspx.cs" Inherits="WebForm.View.Profesor.ReporteAsistenciaAlumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Reporte de Alumno
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
         <h1>Registro de asistencias del Alumno</h1>
        </div>
         <div class=" d-flex  flex-column gap-4" >
             <div class="container card-body">
                 <div class="d-flex gap-2 gap-md-5 mb-md-3 mb-1 flex-wrap flex-md-nowrap justify-content-center justify-content-md-start">
                     <div>
                         <asp:Label ID="NombreAlumnoLblRep" runat="server" Text="Nombre del alumno:" CssClass="form-label"></asp:Label>
                         <asp:TextBox ID="NombeAlumnoTxtRep" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div>
                         <asp:Label ID="DniAlumnoLblRep" runat="server" Text="DNI del alumno:" CssClass="form-label"></asp:Label>
                         <asp:TextBox ID="DniAlumnoTxtRep" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div>
                         <asp:Label ID="GradoAlumnoLblRep" runat="server" Text="Grado:" CssClass="form-label"></asp:Label>
                         <asp:TextBox ID="GradoAlumnoTxtRep" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div>

                     </div>
                 </div>

                 <div class="d-flex gap-2 gap-md-5 mb-md-3 mb-1 flex-wrap flex-md-nowrap justify-content-center justify-content-md-start">
                     <div>
                         <asp:Label ID="TelefonoAlumnoLblRep" runat="server" Text="Teléfono de contacto:" CssClass="form-label"></asp:Label>
                         <asp:TextBox ID="TelefonoAlumnoTxtRep" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div>
                         <asp:Label ID="NombreTutorLblRep" runat="server" Text="Nombre del tutor:" CssClass="form-label"></asp:Label>
                         <asp:TextBox ID="NombreTutorTxtRep" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div >
                         <asp:Label ID="SalonAlumnoLblRep" runat="server" Text="Salón:" CssClass="form-label"></asp:Label>
                         <asp:TextBox ID="SalonAlumnoTxtRep" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                     </div>
                 </div>
                 <div class="d-flex gap-2 gap-md-5 mb-md-3 mb-1 flex-wrap flex-md-nowrap justify-content-center justify-content-md-start">
                 
                        <div>
                            <asp:Label ID="FechaActualLblRep" runat="server" Text="Fecha Actual: " CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="FechaActualTxtRep" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div>
                            <asp:Label ID="FechaIniReporteLbl" runat="server" Text="Fecha inicio de consulta: " CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="FechaIniReporteTxt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                         </div>
                         <div>
                            <asp:Label ID="FechaFinReporteLbl" runat="server" Text="Fecha fin de consulta: " CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="FechaFinReporteTxt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                         </div>
                     
                    
                 </div>

             </div>
             <asp:GridView ID="AsistenciaAlumnoGrid" runat="server" AutoGenerateColumns="false"
                   AllowPaging ="true" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="AsistenciaAlumnoGrid_PageIndexChanging"
                 PageSize="5">
                 <Columns>
                     <asp:BoundField DataField="fechaFormato" HeaderText="Fecha y Hora registrada" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                      <asp:BoundField DataField="estado" HeaderText="Estado de Asistencia" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                     <asp:BoundField DataField=""/>
                 </Columns>
                 <HeaderStyle HorizontalAlign="Center"/>
             </asp:GridView>
         </div>

         <div class="card-footer d-flex justify-content-between">
            <asp:Button ID="RegresarAsistenciasBtn" runat="server" Text="Regresar" CssClass="btn btn-secondary" OnClick="RegresarAsistenciasBtn_Click" />
            <asp:Button runat="server" ID="MostrarReporteBtn" Text="Generar reporte PDF" CssClass="btn btn-primary"
                         OnClick="MostrarReporteBtn_Click" OnClientClick="openinNewTab()" />
         </div>
    </div>
         
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script>
        function openinNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () {
                window.document.forms[0].target = '';
            }, 0);
        }
    </script>
</asp:Content>
