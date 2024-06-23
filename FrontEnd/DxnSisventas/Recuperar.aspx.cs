using DxnSisventas.BBBWebService;
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace DxnSisventas
{
    public partial class Recuperar : Page
    {
        CuentasAPIClient apiCuentas;
        CorreosAPIClient apiCorreo;
        BindingList<cuentaEmpleado> listaCuentasEmpleado;
        BindingList<cuentaCliente> listaCuentaClientes;

        static cuentaEmpleado CuentaRecupearEmpleado;
        static cuentaCliente CuentaRecuperarCliente;
        static int numeroAleatorio;
        private int timeLeft = 300;
        private static bool esEmpleado;
        private static bool esCliente;
        protected void Page_Load(object sender, EventArgs e)


        { 
            apiCuentas = new CuentasAPIClient();
            apiCorreo = new CorreosAPIClient();

            cuentaEmpleado[] arrCuentaEmpleado= apiCuentas.listarCuentaEmpleado();
            cuentaCliente[] arrcuentaClientes = apiCuentas.listarCuentaCliente();
            if(arrCuentaEmpleado != null)
            {
                listaCuentasEmpleado = new BindingList<cuentaEmpleado>(arrCuentaEmpleado.ToList());
            }
            if (arrcuentaClientes != null)
            {
                listaCuentasEmpleado = new BindingList<cuentaEmpleado>(arrCuentaEmpleado.ToList());
            }
            if (!IsPostBack)
            {
                // Inicializar el temporizador solo la primera vez que se carga la página
                ViewState["timeLeft"] = timeLeft;
                numeroAleatorio = -111111111;
                esCliente = false;
                esEmpleado = false;
                CuentaRecupearEmpleado= null;
                CuentaRecuperarCliente= null;
            }
            else
            {
                // Recuperar el valor del ViewState si es un postback
                timeLeft = (int)ViewState["timeLeft"];
            }
        }

        string creaContenido(int number)
        {
            string correo = @"
        <html>
        <head>
        </head>
        <body>
            <div>
                <h1>Recuperación de Contraseña</h1>
                <p>Hola,</p>
                <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en BBB Store. Si no solicitaste este cambio, por favor ignora este correo electrónico.</p>
                <p>Para continuar con la recuperación de tu contraseña, por favor usa el siguiente código de verificación:</p>
                <p style='background-color: #4CAF50; color: #ffffff; padding: 10px 20px; border-radius: 5px; font-weight: bold; display: inline-block;'>" + number + @"</p>
                <p>Este código es válido por 5 minutos. Si el código expira, puedes solicitar uno nuevo.</p>
                <p>Gracias por utilizar nuestros servicios.</p>
                <p>Atentamente,<br>Equipo de Soporte de BBB Store</p>
                <p>Este mensaje fue enviado a <strong>tu correo electrónico</strong>. Si recibiste este mensaje por error, por favor contáctanos.</p>
            </div>
        </body>
        </html>";

            return correo;
        }
        protected void btnGetCode_Click(object sender, EventArgs e)
        {
            // Lógica para enviar el código de verificación al correo electrónico
            // ...
            Random random = new Random();

            // Generar un número aleatorio de 6 dígitos



            if (listaCuentasEmpleado.FirstOrDefault(x => x.usuario == txtEmail.Text) != null)
            {
                CuentaRecupearEmpleado = new cuentaEmpleado();
                CuentaRecupearEmpleado = listaCuentasEmpleado.FirstOrDefault(x => x.usuario == txtEmail.Text);
                // Mostrar la sección del código
                numeroAleatorio = random.Next(100000, 1000000);
                if (apiCorreo.enviarCorreoWebSinArchivo("Recuperacion de contraseña : ", creaContenido(numeroAleatorio), txtEmail.Text) == 1)
                {
                    esEmpleado = true;
                    ViewState["timeLeft"] = 300;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showCodeSection", "showCodeSection();", true);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showEmailSection", "showEmailSection();", true);
                }


            }
            else if (listaCuentaClientes.FirstOrDefault(x => x.usuario == txtEmail.Text) != null)
            {

                numeroAleatorio = random.Next(100000, 1000000);
                CuentaRecuperarCliente = new cuentaCliente();
                CuentaRecuperarCliente = listaCuentaClientes.FirstOrDefault(x => x.usuario == txtEmail.Text);

                // Mostrar la sección del código
                if (apiCorreo.enviarCorreoWebSinArchivo("Recuperacion de contraseña : ", creaContenido(numeroAleatorio), txtEmail.Text) == 1)
                {
                    esCliente = true;
                    ViewState["timeLeft"] = 300;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showCodeSection", "showCodeSection();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showEmailSection", "showEmailSection();", true);
                }
            }
            else
            {

            }
            
        }

        protected void btnConfirmCode_Click(object sender, EventArgs e)
        {
            // Lógica para verificar el código de verificación
            bool timerExpired = hiddenFieldTimerExpired.Value == "true";
            

            if (txtCode.Text == numeroAleatorio.ToString())
            {
                if (!timerExpired)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showPasswordSection", "showPasswordSection();", true);
                }
                else
                {
                    LabelExpirado.CssClass = "error-message";
                    LabelExpirado.Text = "El código ha expirado. Por favor, solicita uno nuevo.";
                    txtCode.Text = ""; // Limpiar el campo de código
                }
            }
            else
            {
                LabelExpirado.Text = "Código incorrecto. Por favor, verifica e intenta nuevamente.";
                txtCode.Text = ""; // Limpiar el campo de código
                ScriptManager.RegisterStartupScript(this, GetType(), "showCodeSection", "showCodeSection();", true);
            }

            // Mostrar la sección para cambiar la contraseña y ocultar las otras
            
        }
        protected void btnChangeEmail_Click(object sender, EventArgs e)
        {
            // Lógica para cambiar de correo electrónico
            // ...

            // Mostrar la sección de email
            ScriptManager.RegisterStartupScript(this, GetType(), "showEmailSection", "showEmailSection();", true);
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Ocultar mensaje de error al inicio
            lblError.CssClass = "error-message hidden";

            // Verificar que las contraseñas coincidan
            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                // Cambiar la contraseña en la base de datos
                // ...
                if (esCliente)
                {
                    CuentaRecuperarCliente.contrasena=txtNewPassword.Text;
                    apiCuentas.actualizarCuentaCliente(CuentaRecuperarCliente);
                }
                else if (esEmpleado)
                {
                    CuentaRecupearEmpleado.contrasena= txtConfirmPassword.Text;
                    apiCuentas.actualizarCuentaEmpleado(CuentaRecupearEmpleado);
                }
                Response.Write("<script>alert('Contraseña cambiada exitosamente');</script>");
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Mostrar el mensaje de error
                lblError.CssClass = "error-message";

                // Mantener la sección de contraseña visible
                ScriptManager.RegisterStartupScript(this, GetType(), "showPasswordSection", "showPasswordSection();", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Lógica para regresar a una vista anterior
            // Mostrar la sección de email
            ScriptManager.RegisterStartupScript(this, GetType(), "showEmailSection", "showEmailSection();", true);
        }
    }
}
