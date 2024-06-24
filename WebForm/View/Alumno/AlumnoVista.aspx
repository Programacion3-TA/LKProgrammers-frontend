<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAlumno.Master" AutoEventWireup="true" CodeBehind="AlumnoVista.aspx.cs" Inherits="WebForm.View.AlumnoVista" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    Principal
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Styles" runat="server">
    <link href="/Public/css/estilos_cursos_cajas.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <!--<script src="./Alumno.js"></script>-->
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Navusuarios" runat="server">
   <!-- <uc:Path ID="MyCustomControl1" runat="server" TiposURL=""/>        -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div id="cursosContainer" class=" flex-grow-1 d-flex flex-wrap justify-content-md-start align-items-start
        justify-content-center">
        <asp:PlaceHolder ID="CursosContainer" runat="server"></asp:PlaceHolder>
    </div>
    <asp:Literal ID="Algo" runat="server"></asp:Literal>
    <asp:Literal ID="Confetti" runat="server"></asp:Literal>
</asp:Content>
