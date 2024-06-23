<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="DxnSisventas.Recuperar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es-es">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html;" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <meta charset="utf-8" />
  <title>Recuperar Contraseña</title>
  <link rel="stylesheet" href="/Content/all.min.css" />
  <link rel="stylesheet" href="/Content/fontawesome.min.css" />
  <link rel="stylesheet" href="/Content/bootstrap.min.css" />
  <link rel="stylesheet" href="/CustomStyles/Master.css" />
  <style>
    .container {
      max-width: 500px;
      margin-top: 50px;
    }

    .hidden {
      display: none;
    }

    #timer {
      font-size: 1.2em;
      margin-top: 10px;
    }

    .error-message {
      color: red;
      margin-top: 10px;
    }
  </style>
  <script type="text/javascript" src="/Scripts/bootstrap.bundle.min.js"></script>
  <script type="text/javascript" src="/Scripts/jquery-3.7.1.min.js"></script>
  <script>
    let timer;
    let timeLeft = 300;
    function showErrorPanel() {
      $(".notification-panel").fadeIn().delay(2250).fadeOut();
    }
    function startTimer() {
      document.getElementById('<%= hiddenFieldTimerExpired.ClientID %>').value = 'false';
          clearInterval(timer);
          timer = setInterval(function () {
            if (timeLeft <= 0) {
              clearInterval(timer);
              document.getElementById('<%= hiddenFieldTimerExpired.ClientID %>').value = 'true';
                  document.getElementById('<%= timer.ClientID %>').innerText = 'Código expirado';

                } else {
                  let minutes = Math.floor(timeLeft / 60);
                  let seconds = timeLeft % 60;
                  document.getElementById('<%= timer.ClientID %>').innerText = minutes + ":" + (seconds < 10 ? '0' : '') + seconds;
                timeLeft--;
              }
            }, 1000);
    }

    function resetTimer() {
      clearInterval(timer);
      timeLeft = 300; // Reiniciar el contador a 5 minutos
      startTimer(); // Iniciar el timer nuevamente
    }

    function showCodeSection() {
      document.getElementById('<%= emailSection.ClientID %>').classList.add('hidden');
          document.getElementById('<%= codeSection.ClientID %>').classList.remove('hidden');
      startTimer();
    }

    function showEmailSection() {
      clearInterval(timer);
      timeLeft = 300;
      document.getElementById('<%= codeSection.ClientID %>').classList.add('hidden');
          document.getElementById('<%= emailSection.ClientID %>').classList.remove('hidden');
          document.getElementById('<%= timer.ClientID %>').innerText = '00:00';
    }

    function showPasswordSection() {
      document.getElementById('<%= emailSection.ClientID %>').classList.add('hidden');
          document.getElementById('<%= codeSection.ClientID %>').classList.add('hidden');
          document.getElementById('<%= passwordSection.ClientID %>').classList.remove('hidden');
    }

    function togglePasswordVisibility(inputId) {
      let input = document.getElementById(inputId);
      if (input.type === "password") {
        input.type = "text";
      } else {
        input.type = "password";
      }
    }
  </script>
</head>
<body>
  <form id="form1" runat="server">

    <div class="container">
      <div class="card">
        <div class="card-header text-center">
          <h2>Recuperar Contraseña</h2>
        </div>
        <div class="card-body">
          <asp:ScriptManager ID="ScriptManager1" runat="server" />
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

            <ContentTemplate>
              <asp:Panel ID="ErrorPanel" runat="server" CssClass="notification-panel error-panel">
                <i class="fas fa-exclamation-circle"></i>
                <asp:Label ID="ErrorLabel" runat="server" Text="" CssClass="error-message"></asp:Label>
              </asp:Panel>
                <asp:Panel runat="server" DefaultButton="btnGetCode">
              <div id="emailSection" class="col-sm-7" runat="server">
                <div class="form-group">
                  <label for="txtEmail">Correo Electrónico</label>

                  <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Ingrese su correo electrónico"></asp:TextBox>
                  <asp:RequiredFieldValidator
                    ID="reqCorreo"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El campo de correo electrónico es obligatorio."
                    Display="Dynamic"
                    ForeColor="Red"
                    SetFocusOnError="true"
                    ValidationGroup="CorreoGroup">
                  </asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator
                    ID="regexCorreo"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="Por favor, ingrese un correo electrónico válido."
                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                    Display="Dynamic"
                    ForeColor="Red"
                    SetFocusOnError="true"
                    ValidationGroup="CorreoGroup">
                  </asp:RegularExpressionValidator>
                    <br />
                </div>
                <asp:Button ID="btnGetCode" CssClass="btn btn-primary btn-block" runat="server" ValidationGroup="CorreoGroup" Text="Obtener Código" OnClick="btnGetCode_Click" OnClientClick="resetTimer();" />
              </div>
                   
               </asp:Panel>
              <asp:Panel runat="server" DefaultButton="btnConfirmCode">
                <div id="codeSection" class="hidden" runat="server">
                  <div class="form-group">
                    <label for="txtCode">Código de Verificación</label>
                    <asp:HiddenField ID="hiddenFieldTimerExpired" runat="server" Value="false" />
                      <div class="col-sm-3">
                    <asp:TextBox ID="txtCode" CssClass="form-control" runat="server" placeholder="codigo" ValidationGroup="codigoGroup" MaxLength="6"></asp:TextBox>
                          </div>
                    <asp:RegularExpressionValidator ID="revCode" runat="server" ControlToValidate="txtCode"
                      ErrorMessage="Debe ingresar exactamente 6 dígitos numéricos positivos." ValidationExpression="^[1-9]\d{5}$"
                      ValidationGroup="codigoGroup">
                    </asp:RegularExpressionValidator>
                  </div>   
                  <div id="timer" runat="server">05:00</div>
                    <div class="d-flex justify-content-between">
                  <asp:Button ID="btnConfirmCode" CssClass="btn btn-success btn-block" runat="server" Text="Confirmar Código" OnClick="btnConfirmCode_Click" />
                  <asp:Label ID="LabelExpirado" CssClass="error-message hidden" runat="server" />
                  <asp:Button ID="btnChangeEmail" CssClass="btn btn-secondary btn-block" runat="server" CausesValidation="false" Text="Cambiar Correo" OnClick="btnChangeEmail_Click" />
                        </div>
                </div>
              </asp:Panel>
              <asp:Panel runat="server" DefaultButton="btnChangePassword">
                <div id="passwordSection" class="hidden" runat="server">
                  <div class="form-group">
                    <label for="txtNewPassword">Nueva Contraseña</label>
                    <div class="input-group">
                      <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server" placeholder="Ingrese la nueva contraseña" TextMode="Password"></asp:TextBox>
                      <div class="input-group-append">
                        <button class="btn btn-outline-secondary" type="button" onclick="togglePasswordVisibility('<%= txtNewPassword.ClientID %>')">Mostrar</button>
                      </div>
                    </div>
                  </div>
                  <div class="form-group">
                    <label for="txtConfirmPassword">Confirmar Nueva Contraseña</label>
                    <div class="input-group">
                      <asp:TextBox ID="txtConfirmPassword" CssClass="form-control" runat="server" placeholder="Confirme la nueva contraseña" TextMode="Password"></asp:TextBox>
                      <div class="input-group-append">
                        <button class="btn btn-outline-secondary" type="button" onclick="togglePasswordVisibility('<%= txtConfirmPassword.ClientID %>')">Mostrar</button>
                      </div>
                    </div>
                    <asp:Label ID="lblError" CssClass="error-message hidden" runat="server" Text="Las contraseñas no coinciden." />
                  </div>
                  <asp:Button ID="btnChangePassword" CssClass="btn btn-primary btn-block" runat="server" Text="Cambiar Contraseña" OnClick="btnChangePassword_Click" />
                </div>
              </asp:Panel>
                <br />
                <div class="d-flex justify-content-between">
              <asp:Button ID="Button1" CssClass="btn btn-secondary btn-block" runat="server" Text="Regresar" ValidationGroup="Ninguno" OnClick="btnRegresar_Click" />
              <asp:Button ID="btnBack" CssClass="btn btn-secondary btn-block" runat="server" Text="Iniciar Session" CausesValidation="false" ValidationGroup="Ninguno" OnClick="btnBack_Click" />
                </div>
                    </ContentTemplate>
          </asp:UpdatePanel>
        </div>
      </div>
    </div>
  </form>
</body>
</html>
