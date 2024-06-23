<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
  Inherits="DxnSisventas.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Login Page</title>
  <link
    href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css"
    rel="stylesheet" />
  <link rel="stylesheet" href="CustomStyles/Login.css" />

  <script type="text/javascript" src="Scripts/jquery-3.7.1.min.js"></script>

  <script>
    function showErrorPanel() {
      $(".error-panel").fadeIn().delay(2250).fadeOut();
    }
  </script>
</head>
<body>
  <div class="background-image"></div>
  <div class="login-container">
    <form id="loginForm" class="login-form" runat="server">
      <asp:Panel ID="ErrorPanel" runat="server" CssClass="error-panel">
        <i class="fas fa-exclamation-circle"></i>
        <asp:Label ID="ErrorLabel" runat="server" Text="" CssClass="error-message"></asp:Label>
      </asp:Panel>
      <asp:ScriptManager ID="ScriptManager1" runat="server" />
      <h1 class="login-title">Login</h1>

      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="input-container">
            <asp:TextBox
              ID="username"
              CssClass="input-container"
              placeholder="Username"
              onblur="validateUsername()"
              runat="server" />
            <p
              id="username-error"
              class="error-message"
              style="display: none">
              Username is required!
            </p>
            <!-- Added error message for username -->
          </div>
          <div class="input-container">
            <asp:TextBox
              ID="password"
              CssClass="input-container"
              TextMode="Password"
              placeholder="Password"
              onblur="validatePassword()"
              runat="server" />
            <p
              id="password-error"
              class="error-message"
              style="display: none">
              Password is required!
            </p>
            <!-- Added error message for password -->
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>

        <asp:LinkButton ID="BtnRecuperar" runat="server" Text='Recuperar Contraseña'   CssClass="link-button" OnClick="BtnRecuperar_Click" />
        <br />
      <asp:Button
        ID="LoginButton"
        CssClass="login-button btn"
        OnClick="LoginButton_Click"
        Text="Login"
        runat="server"></asp:Button>
    </form>
  </div>
  <script type="text/javascript" src="CustomScripts/Login.js"></script>
</body>
</html>
