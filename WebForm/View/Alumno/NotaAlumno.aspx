<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Layout/MainAlumno.master" AutoEventWireup="true" CodeBehind="NotaAlumno.aspx.cs" Inherits="WebForm.View.Alumno.NotaAlumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="container-fluid mt-2 ms-4 me-4 ">
        <h2>Notas</h2>
        <asp:Button Text="Generar PDF" runat="server" CssClass="btn btn-primary" OnClick="BtnGenerarPdf__Click"/>
        <asp:GridView
            ID="GridCursosNotas"
            runat="server"
            AutoGenerateColumns ="false"
            CssClass="table table-hover table-responsive table-striped "
            HeaderStyle-HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button
                            runat="server"
                            Text="Ver notas"
                            OnClick="BtnVerNotas__Click"
                            CssClass="btn btn-primary"
                            CommandArgument='<%# Eval("id") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="modal fade" id="NotasCursoModal" tabindex="-1" role="dialog" aria-labelledby="NotasCursoModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="NotasCursoModalLabel">Notas del curso: <asp:Label ID="NombreCursoLbl" runat="server"></asp:Label></h5>
                </div>
                <div class="modal-body">
                    <%--Cuerpo--%>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView
                                ID="GridViewNotasAlumno"
                                runat="server"
                                AutoGenerateColumns ="false"
                                CssClass="table table-hover table-responsive table-striped "
                                HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                Text='<%# ((WebForm.ServicioWS.competencia)Eval("competencia")).id%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                Text='<%# ((WebForm.ServicioWS.competencia)Eval("competencia")).descripcion%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Peso">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                Text='<%# ((WebForm.ServicioWS.competencia)Eval("competencia")).peso%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="calificacion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle"/>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <h5 class="modal-title">
                        Gráfica de nota final (aproximada)
                    </h5>
                    <div class="w-100 d-flex justify-content-center">
                        <div class="w-50">
                            <canvas id="notasPie"></canvas>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="CerrarModalBtn" runat="server" Text="Cerrar" CssClass="btn btn-secondary"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
        <script src="/Public/js/modalHandler.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.3/dist/chart.umd.min.js"></script>
        <script>

            function graficarPie(datos) {
                const ctx = document.getElementById('notasPie');

                // Sistemas Operativos reference
                new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: ['Falta', 'Aprobado'],
                        datasets: [{
                        label: 'Nota mala vs nota buena',
                        data: datos,
                        borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        },
                       responsive: true
                    }
                });
            }
        </script>
</asp:Content>
