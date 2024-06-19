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
    <%--<uc:Path ID="MyCustomControl1" runat="server" TiposURL="Asistencia"/>        --%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid mt-2 ms-4 me-4 ">
      <h2 class="mb-4 text-center text-md-start ">Asistencias por día</h2>
        
         <div class="card mb-4">
               <h2 class="card-header">Asistencias por alumno</h2>
                    <div class="d-flex gap-3 border-2 flex-column card-body">
                        <div class="d-flex gap-3 w-75" >
                            <div>
                                <!--Falta agregar advertencias de no ingresar correcto las fechas-->
                              <asp:Label ID="FechaIniSelecLbl" runat="server" Text="Label" CssClass="form-check-label" Font-Bold="true">Fecha Inicial:</asp:Label>
                              <asp:TextBox ID="FechaIniTxt" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="FechFinSelectLbl" runat="server" Text="Label" CssClass="form-check-label" Font-Bold="true">Fecha Final:</asp:Label>
                                <asp:TextBox ID="FechaFinalTxt" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="d-flex gap-4">
                            <asp:Button ID="AsistenciasAlumnoBtn" runat="server" Text="Obtener Asistencias" CssClass="btn btn-success" OnClick="AsistenciasAlumnoBtn_Click1"
                                OnClientClick="openinNewTab()"/>
                        </div>
                        
                    </div>
            </div>
        <asp:Literal ID="Cuack" runat="server"></asp:Literal>
        <asp:GridView
            ID="GriAsistenciasAlumnos"
            runat="server"
            AutoGenerateColumns ="false"
            AllowPaging="true"
            PageSize="30"
            CssClass="table table-hover table-responsive table-striped "
            HeaderStyle-HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField HeaderText="Fecha Asistencia" ItemStyle-CssClass="col-1" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# ((System.DateTime)Eval("fechaHora")).ToString("dd \\de MMM \\del yy") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" ItemStyle-CssClass="col-1" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# GenerarFormatoEstado((WebForm.ServicioWS.estadoAsistencia)Eval("estado")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-1 " ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="JustificarBtn" runat="server"
                            Text='<%# TituloBotonJustificacion((WebForm.ServicioWS.estadoAsistencia)Eval("estado"), (bool) Eval("aceptaJustificacion")) %>'
                            Enabled='<%# VerificarEstadoNoJustificado((WebForm.ServicioWS.estadoAsistencia)Eval("estado"), (bool) Eval("aceptaJustificacion")) %>'
                            CssClass="btn btn-primary w-50" OnClick="JustificarBtn_Click"
                            CommandArgument='<%# Eval("fechaHora").ToString() + "@" + Eval("justificacion") %>' />
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
                    <asp:Calendar ID="InputCalendario" runat="server" Enabled="false" CssClass="d-none"></asp:Calendar>
                    <asp:Label ID="InputJustificacionLbl" runat="server" Text="Introdusca el motivo: " CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="InputJustificacion" TextMode="MultiLine" Columns="30" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
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
    <script src="/Public/js/modalHandler.js"></script>
    <script>
        function openinNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () {
                window.document.forms[0].target = '';
            }, 0);
        }
    </script>
</asp:Content>
