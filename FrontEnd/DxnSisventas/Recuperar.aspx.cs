using System;
using System.Web.UI;

namespace DxnSisventas
{
    public partial class Recuperar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGetCode_Click(object sender, EventArgs e)
        {
            // Aquí iría el código para enviar el correo con el código de verificación.
            // Por ahora, simplemente mostramos la sección de código.
            ScriptManager.RegisterStartupScript(this, GetType(), "showCodeSection", "showCodeSection();", true);
        }

        protected void btnConfirmCode_Click(object sender, EventArgs e)
        {
            // Aquí iría el código para verificar el código ingresado.
        }

        protected void btnChangeEmail_Click(object sender, EventArgs e)
        {
            // Regresamos a la sección de ingreso de correo.
            ScriptManager.RegisterStartupScript(this, GetType(), "showEmailSection", "showEmailSection();", true);
        }
    }
}
