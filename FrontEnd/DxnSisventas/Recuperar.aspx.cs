using DxnSisventas.BBBWebService;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;

namespace DxnSisventas
{
    public partial class Recuperar : Page
    {
        CuentasAPIClient apiCuentas;
        BindingList<cuenta> listaCuentas;
        cuenta cuentaRecuperar;
        CorreosAPIClient correos;
        protected void Page_Load(object sender, EventArgs e)
        {
            apiCuentas = new CuentasAPIClient();
            correos = new CorreosAPIClient();
            cuenta[] arrCuenta= apiCuentas.listarCuentaEmpleado();
            if(arrCuenta != null)
            {
                listaCuentas = new BindingList<cuenta>(arrCuenta.ToList());
            }

        }

        protected void btnGetCode_Click(object sender, EventArgs e)
        {
            // Aquí iría el código para enviar el correo con el código de verificación.
            // Por ahora, simplemente mostramos la sección de código.
            cuentaRecuperar = listaCuentas.Where(x=> x.usuario.ToString()==txtEmail.Text).FirstOrDefault();
            Random random = new Random();

            // Generar un número aleatorio de 6 dígitos
            int numeroAleatorio = random.Next(100000, 1000000);
            if (cuentaRecuperar != null)
                correos.enviarCorreoWebSinArchivo("Recuperar contraseña","Su codigo es "+numeroAleatorio.ToString(),txtEmail.Text);
                Session["cod"]=numeroAleatorio;
                ScriptManager.RegisterStartupScript(this, GetType(), "showCodeSection", "showCodeSection();", true);
            

        }

        protected void btnConfirmCode_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text;
            string codSession  = Session["cod"].ToString();
            if (codSession == code)
                ScriptManager.RegisterStartupScript(this, GetType(), "showPasswordSection", "showPasswordSection();", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "showCodeSection", "showCodeSection();", true);
            
            //apiCuentas.actualizarCuentaEmpleado()
            // Aquí iría el código para verificar el código ingresado.
        }

        protected void btnChangeEmail_Click(object sender, EventArgs e)
        {
            // Regresamos a la sección de ingreso de correo.
            ScriptManager.RegisterStartupScript(this, GetType(), "showEmailSection", "showEmailSection();", true);
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Lógica para cambiar la contraseña
            // Verificar que las contraseñas coincidan
            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                // Cambiar la contraseña en la base de datos
                cuentaRecuperar.contrasena= txtNewPassword.Text;
                // ...
                //apiCuentas.actualizarCuentaEmpleado(cuentaRecuperar);
                Response.Write("<script>alert('Contraseña cambiada exitosamente');</script>");
            }
            else
            {
                Response.Write("<script>alert('Las contraseñas no coinciden');</script>");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Lógica para regresar a una vista anterior
            // ...
        }
    }
}
