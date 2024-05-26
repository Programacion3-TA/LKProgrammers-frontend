<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/Main.Master" AutoEventWireup="true" CodeBehind="Error404.aspx.cs" Inherits="WebForm.View.Error.Error404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Error 404
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Styles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navusuarios" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-xxl-5 align-middle text-center">
        <div class="alert alert-danger" role="alert">
            <h1 class="text-danger-emphasis text-center">
                Error 404: Hubo problemas con la URL ingresada
            </h1>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
