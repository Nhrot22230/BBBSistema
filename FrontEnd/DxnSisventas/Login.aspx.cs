using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxnSisventas.BBBWebService;

namespace DxnSisventas
{
  public partial class Login : System.Web.UI.Page
  {
    private CuentasAPIClient cuentasAPIClient;

    protected void Page_Init(object sender, EventArgs e)
    {
      cuentasAPIClient = new CuentasAPIClient();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      ErrorPanel.Visible = false;

      if (!VerificarDisponibilidadServicio())
      {
        MostrarError("No se pudo establecer conexión con el servicio de cuentas");
        LoginButton.Enabled = false;
      }
      else
      {
        LoginButton.Enabled = true;
      }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
      string usuario = username.Text;
      string pass = password.Text;

      empleado Emp = cuentasAPIClient.iniciarSesionEmpleado(usuario, pass);

      if (Emp != null)
      {
        cuentaEmpleado cuenta = new cuentaEmpleado
        {
          usuario = usuario,
          contrasena = pass
        };

        Session["datosCuenta"] = cuenta;
        Session["empleado"] = Emp;
        Response.Redirect("Home.aspx");
      }
      else
      {
        password.Text = "";
        ErrorPanel.Visible = true;
        ErrorLabel.Text = "Usuario o contraseña incorrectos";
        ScriptManager.RegisterStartupScript(this, GetType(), "showErrorPanel", "showErrorPanel();", true);
      }
    }
    private bool VerificarDisponibilidadServicio()
    {
      try
      {
        // Intenta realizar una operación simple
        cuentasAPIClient.hello(name: "DxnSisventas");
        return true;
      }
      catch (System.Exception ex)
      {

        return false;
      }
    }

    private void MostrarError(string mensaje)
    {
      ErrorPanel.Visible = true;
      ErrorLabel.Text = mensaje;
      ScriptManager.RegisterStartupScript(this, GetType(), "showErrorPanel", "showErrorPanel();", true);
    }

    protected void BtnRecuperar_Click(object sender, EventArgs e)
    {
      Response.Redirect("Recuperar.aspx");
    }
  }
}