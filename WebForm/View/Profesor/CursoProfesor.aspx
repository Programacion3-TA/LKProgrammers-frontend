<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MainProfesor.Master" AutoEventWireup="true" CodeBehind="CursoProfesor.aspx.cs" Inherits="WebForm.View.CursoProfesor.CursoProfesor" %>
<%@ Register Src="~/Components/Path.ascx" TagName="Path" TagPrefix="uc" %>

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
    <%--<uc:Path ID="MyCustomControl1" runat="server" TiposURL="Curso" />--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex align-items-start flex-column gap-5">
    <asp:Label ID="PageTitle" runat="server" Text="" CssClass="custom-title"></asp:Label>
    <div class="d-flex gap-1 w-100 flex-wrap">
        <asp:Literal ID="BadgesContainer" runat="server" Text=""></asp:Literal> 
    </div>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
