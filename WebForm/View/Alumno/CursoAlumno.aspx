﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainAlumno.Master" AutoEventWireup="true" CodeBehind="CursoAlumno.aspx.cs" Inherits="WebForm.View.CursoAlumno.CursoAlumno" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
    <style>
    .custom-title {
        font-size: 30px;
        text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
        font-weight: bold;
    }
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <%--<uc:Path ID="MyCustomControl1" runat="server" TiposURL="Curso"/>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex align-items-start flex-column gap-5">
        <h1>
            <asp:Label ID="PageTitle" runat="server" Text="" CssClass=""></asp:Label>
        </h1>
        <div class="d-flex gap-1 w-100 flex-wrap">
            <asp:Literal ID="BadgesContainer" runat="server" Text=""></asp:Literal> 
        </div>

        <div class="accordion accordion-flush w-100" id="accordionFlushExample">
            <asp:Literal ID="SeccionesContainer" runat="server" Text=""></asp:Literal>
        </div>
        <script>
            console.log("Hola")
        </script>
        
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
