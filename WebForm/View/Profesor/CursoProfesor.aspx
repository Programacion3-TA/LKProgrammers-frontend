<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="CursoProfesor.aspx.cs" Inherits="WebForm.View.CursoProfesor.CursoProfesor" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
    <uc:Path ID="MyCustomControl1" runat="server" TiposURL="Curso"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Curso {Insertar nombre}
    </h1>
    <hr />
    <ul>
        <li>
            <a href="/View/CompetenciaProfesor/CompetenciaProfesor.aspx">Competencias</a>
        </li>
        <li>
            <a href="/View/NotaProfesor/NotaProfesor.aspx">Notas</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
