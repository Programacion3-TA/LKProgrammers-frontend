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
            </tr>
        </thead>
        <tbody id="table--tbody--horas" class="table-group-divider">
            <tr>
                <th scope="row" class="text-center min-w-200">8:00 - 8:30</th>
                <td class="text-center">-</td>
                <td class="text-center">Matemática</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
            </tr>
            <tr>
                <th scope="row" class="text-center">8:30 - 9:00</th>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
            </tr>
            <tr>
                <th scope="row" class="text-center">9:00 - 9:30</th>
                <td class="text-center text-primary-emphasis bg-primary-subtle">Matemática</td>
                <td class="text-center">-</td>
                <td class="text-center text-success-emphasis bg-success-subtle">Ciencias naturales</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
            </tr>
            <tr>
                <th scope="row" class="text-center">9:30 - 10:00</th>
                <td class="text-center">-</td>
                <td class="text-center text-primary-emphasis bg-primary-subtle">Matemática</td>
                <td class="text-center text-success-emphasis bg-success-subtle">Ciencias naturales</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
            </tr>
            <tr class="table-active">
                <th scope="row" class="text-center">10:00 - 10:30</th>
                <td colspan="5" class="text-center">Recreo</td>
            </tr>
            <tr>
                <th scope="row" class="text-center">10:30 - 11:00</th>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center text-warning-emphasis bg-warning-subtle">Lenguaje</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
            </tr>
            <tr>
                <th scope="row" class="text-center">11:00 - 11:30</th>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center text-warning-emphasis bg-warning-subtle">Lenguaje</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
            </tr>
            <tr>
                <th scope="row" class="text-center">11:30 - 12:00</th>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center text-danger-emphasis bg-danger-subtle">Historia</td>
            </tr>
            <tr>
                <th scope="row" class="text-center">12:00 - 12:30</th>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center">-</td>
                <td class="text-center text-danger-emphasis bg-danger-subtle">Historia</td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
