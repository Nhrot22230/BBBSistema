<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="DxnSisventas.Recuperar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Recuperar Contraseña</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
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
    </style>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script>
        let timer;
        let timeLeft = 300; // 5 minutes in seconds

        function startTimer() {
            timer = setInterval(function () {
                if (timeLeft <= 0) {
                    clearInterval(timer);
                    document.getElementById('<%= timer.ClientID %>').innerText = 'Código expirado';
                } else {
                    let minutes = Math.floor(timeLeft / 60);
                    let seconds = timeLeft % 60;
                    document.getElementById('<%= timer.ClientID %>').innerText = minutes + ":" + (seconds < 10 ? '0' : '') + seconds;
                    timeLeft--;
                }
            }, 1000);
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
                    <div id="emailSection" runat="server">
                        <div class="form-group">
                            <label for="txtEmail">Correo Electrónico</label>
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Ingrese su correo electrónico" TextMode="Email"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnGetCode" CssClass="btn btn-primary btn-block" runat="server" Text="Obtener Código" OnClick="btnGetCode_Click" />
                    </div>

                    <div id="codeSection" class="hidden" runat="server">
                        <div class="form-group">
                            <label for="txtCode">Código de Verificación</label>
                            <asp:TextBox ID="txtCode" CssClass="form-control" runat="server" placeholder="Ingrese el código"></asp:TextBox>
                        </div>
                        <div id="timer" runat="server">00:00</div>
                        <asp:Button ID="btnConfirmCode" CssClass="btn btn-success btn-block" runat="server" Text="Confirmar Código" OnClick="btnConfirmCode_Click" />
                        <asp:Button ID="btnChangeEmail" CssClass="btn btn-secondary btn-block" runat="server" Text="Cambiar Correo" OnClick="btnChangeEmail_Click" />
                    </div>

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
                        </div>
                        <asp:Button ID="btnChangePassword" CssClass="btn btn-primary btn-block" runat="server" Text="Cambiar Contraseña" OnClick="btnChangePassword_Click" />
                    </div>

                    <asp:Button ID="btnBack" CssClass="btn btn-secondary btn-block" runat="server" Text="Regresar" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
