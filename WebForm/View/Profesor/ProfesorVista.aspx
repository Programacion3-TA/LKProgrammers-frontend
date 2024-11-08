﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="ProfesorVista.aspx.cs" Inherits="WebForm.View.ProfesorVista.ProfesorVista" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Principal
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <link href="/Public/css/estilos_cursos_cajas.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <%--<uc:Path ID="MyCustomControl1" runat="server" TiposURL=""/>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div id="cursosContainer" class=" flex-grow-1 d-flex flex-wrap justify-content-md-start align-items-start
    justify-content-center">
        <asp:PlaceHolder ID="CursosContainer" runat="server"></asp:PlaceHolder>
    </div>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
