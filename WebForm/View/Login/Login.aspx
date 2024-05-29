<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForm.View.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://kit.fontawesome.com/7237f750bf.js" crossorigin="anonymous"></script>
    <link href="/Public/css/bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="/Public/js/bootstrap/bootstrap.js" type="text/javascript"></script>
    <script src="/Public/js/bootstrap/bootstrap.bundle.js" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio de Sesión | Platataforma institucional</title>
    <style>
        body{
            height:100vh;
            background: rgb(255,245,245);
            background: linear-gradient(90deg, rgba(255,245,245,1) 50%, rgba(109,196,111,1) 50%);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="h-100 d-flex align-items-center">
        <!--<div style="width: 100%; height: 100vh; display: flex; justify-content: center; align-items: center;">

            <div style="width: clamp(300px,40%,600px); display: flex; flex-direction: column; border: 0.2em solid #d9d9d9; border-radius: 10px; padding: 5px">
                <p class="h2 pb-2 m-auto">
                    Bienvenido/a
                </p>
                <label class="form-label">
                    <input type="text" name="user" placeholder="Usuario" class="form-control" />
                </label>
                <label class="form-label">
                    <input type="password" name="password" placeholder="Contraseña" class="form-control" />
                </label>
                <div class="d-flex align-content-center justify-content-center">
                    <button type="submit" class="btn btn-primary">
                        Iniciar Sesión
                    </button>
                </div>
            </div>

        </div>-->
    <div class="container h-75 align-items-stretch">
        <div class="row justify-content-center h-100">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header text-center">
                        <h4>San Pedro Nolasco</h4>
                        <img src="/Public/img/logoColegio.png" width="120" />
                        <hr/>
                        <h5>Bienvenido/a</h5>
                    </div>
                    <div class="card-body d-flex flex-column gap-3">
                            <div class="form-group">
                                <asp:Label ID="LblUsuario" runat="server" Text="Usuario" CssClass="form-label"></asp:Label>
                                 <asp:TextBox ID="TxtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario o correo electrónico..."></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblContrasenia" runat="server" Text="Constraseña" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="TxtContrasenia" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña..."></asp:TextBox>
                            </div>
                          <asp:Button ID="BtnIngresar" runat="server" Text="Iniciar sesión" CssClass="btn btn-black btn-block"
                           OnClick="BtnIngresar_Click"  />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
