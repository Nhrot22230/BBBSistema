using System;
using System.Web.UI;

namespace DxnSisventas
{
    public partial class Recuperar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnGetCode_Click(object sender, EventArgs e)
        {
            // Lógica para enviar el código de verificación al correo electrónico
            // ...

            // Mostrar la sección del código
            ScriptManager.RegisterStartupScript(this, GetType(), "showCodeSection", "showCodeSection();", true);
        }

        protected void btnConfirmCode_Click(object sender, EventArgs e)
        {
            // Lógica para verificar el código de verificación
            // ...

            // Mostrar la sección para cambiar la contraseña y ocultar las otras
            ScriptManager.RegisterStartupScript(this, GetType(), "showPasswordSection", "showPasswordSection();", true);
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
