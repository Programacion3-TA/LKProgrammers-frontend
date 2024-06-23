<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAlumno.Master" AutoEventWireup="true" CodeBehind="CalendarioAlumno.aspx.cs" Inherits="WebForm.View.CalendarioAlumno.CalendarioAlumno" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <!--<uc:Path ID="MyCustomControl1" runat="server" TiposURL="Calendario"/>-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="w-100 d-flex justify-content-between  mb-3 ">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="d-flex gap-2 align-items-center">
                        <asp:Button ID="BtnAgregar" CssClass="btn btn-primary" runat="server" Text="-15" OnClick="BtnRestar_Click"/>
                        <span>
                            Bloques:
                            <asp:Literal ID="LblBloques" runat="server"></asp:Literal>
                            min
                        </span>
                        <asp:Button ID="BtnRestar" CssClass="btn btn-primary" runat="server" Text="+15" OnClick="BtnAgregar_Click"/>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        <asp:Button runat="server" Text="Reporte de Horario" CssClass="btn btn-primary" OnClick="BtnReporteHorario_Click" />
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table class="table table-bordered align-middle w-100 h-100">
                <thead>
                    <tr>
                        <th scope="col" class="text-center">Hora</th>
                        <asp:Literal ID="CalendarHeader" runat="server"></asp:Literal>
                    </tr>
                </thead>
                <tbody id="table--tbody--horas" class="table-group-divider">
                    <asp:Label ID="CalendarContainer" runat="server"></asp:Label>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
