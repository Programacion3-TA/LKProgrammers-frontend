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
    <table class="table table-bordered align-middle w-100 h-100">
        <thead>
            <tr>
                <th scope="col" class="text-center">Hora</th>
                <th scope="col" class="text-center">Lunes</th>
                <th scope="col" class="text-center">Martes</th>
                <th scope="col" class="text-center">Miercoles</th>
                <th scope="col" class="text-center">Jueves</th>
                <th scope="col" class="text-center">Viernes</th>
                <th scope="col" class="text-center">Sábado</th>
            </tr>
        </thead>
        <tbody id="table--tbody--horas" class="table-group-divider">
            <asp:Label ID="CalendarContainer" runat="server"></asp:Label>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
