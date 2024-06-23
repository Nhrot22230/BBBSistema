using DxnSisventas.BBBWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas
{
  public partial class Main : System.Web.UI.MasterPage
  {
    private void HandleAdminRole()
    {
      liProductos.Visible = true;
      footerliProductos.Visible = true;
      liUsuarios.Visible = true;
      footerliUsuarios.Visible = true;
      liUsuariosClientes.Visible = true;
      footerliUsuariosClientes.Visible = true;
      liUsuariosEmpleados.Visible = true;
      footerliUsuariosEmpleados.Visible = true;
      liCuentas.Visible = true;
      footerliCuentas.Visible = true;
      liOrdenCompra.Visible = true;
      footerliOrdenCompra.Visible = true;
      liOrdenVenta.Visible = true;
      footerliOrdenVenta.Visible = true;
      liReportes.Visible = true;
      footerliReportes.Visible = true;
      liReporteVenta.Visible = true;
      footerliReporteVenta.Visible = true;
      liReporteAlmacen.Visible = true;
      footerliReporteAlmacen.Visible = true;
    }

    private void HandleSalesRole()
    {
      liUsuarios.Visible = true;
      footerliUsuarios.Visible = true;
      liUsuariosClientes.Visible = true;
      footerliUsuariosClientes.Visible = true;
      liOrdenVenta.Visible = true;
      footerliOrdenVenta.Visible = true;
      liReportes.Visible = true;
      footerliReportes.Visible = true;
      liReporteVenta.Visible = true;
      footerliReporteVenta.Visible = true;
    }

    private void HandleWarehouseRole()
    {
      liProductos.Visible = true;
      footerliProductos.Visible = true;
      liOrdenCompra.Visible = true;
      footerliOrdenCompra.Visible = true;
      liReportes.Visible = true;
      footerliReportes.Visible = true;
      liReporteAlmacen.Visible = true;
      footerliReporteAlmacen.Visible = true;
    }

    private void HandleDeliverryRole()
    {
      liUsuarios.Visible = true;
      footerliUsuarios.Visible = true;
      liUsuariosClientes.Visible = true;
      footerliUsuariosClientes.Visible = true;
      liOrdenVenta.Visible = true;
      footerliOrdenVenta.Visible = true;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      if (Session["empleado"] == null)
      {
        Response.Redirect("~/Login.aspx");
      }

      if (Session["DarkMode"] == null)
      {
        Session["DarkMode"] = false;
      }

      empleado user = (empleado)Session["empleado"];
      rol rolUsuario = user.rol;

      if (rolUsuario.Equals(rol.Administrador))
      {
        HandleAdminRole();
      }
      else if (rolUsuario.Equals(rol.EncargadoVentas))
      {
        HandleSalesRole();
      }
      else if (rolUsuario.Equals(rol.EncargadoAlmacen))
      {
        HandleWarehouseRole();
      }
      else
      {
        HandleDeliverryRole();
      }

      DarkModeCheckBox.Checked = (bool)Session["DarkMode"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      ClearError();
    }

    protected void LbCerrarSesion_Click(object sender, EventArgs e)
    {
      Session["empleado"] = null;
      Session["datosCuenta"] = null;

      Response.Redirect("~/Login.aspx");
    }

    protected void LbProductos_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/Productos.aspx");
    }

    protected void LbInicio_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Home.aspx");
    }

    public void ClearError()
    {
      ErrorPanel.Visible = false;
      ErrorLabel.Text = "";
    }

    public void MostrarError(string mensaje)
    {
      ErrorPanel.CssClass = "notification-panel error-panel";
      ErrorLabel.CssClass = "error-message";
      ErrorPanel.Visible = true;
      ErrorLabel.Text = mensaje;
      ScriptManager.RegisterStartupScript(this, GetType(), "showPanel", "showErrorPanel();", true);
    }

    public void MostrarExito(string mensaje)
    {
      ErrorPanel.CssClass = "notification-panel success-panel";
      ErrorLabel.CssClass = "success-message";
      ErrorPanel.Visible = true;
      ErrorLabel.Text = mensaje;
      ScriptManager.RegisterStartupScript(this, GetType(), "showPanel", "showErrorPanel();", true);
    }

    public bool IsDarkModeEnabled()
    {
      return (bool)Session["DarkMode"];
    }

    protected void LbUsuariosEmpleados_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/PersonasEmpleados.aspx");
    }

    protected void LbUsuariosClientes_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/PersonasClientes.aspx");
    }

    protected void LbOrdenCompra_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/OrdenCompra.aspx");
    }

    protected void LbOrdenVenta_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/OrdenVenta.aspx");
    }

    protected void LbComprobante_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/Comprobantes.aspx");
    }

    protected void LbCuentasEmpleado_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/CuentasEmpleados.aspx");
    }

    protected void LbCuentasClientes_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/CuentasClientes.aspx");
    }

    protected void DarkModeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      Session["DarkMode"] = DarkModeCheckBox.Checked;
    }

    protected void LbReporte_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Views/Test.aspx");
    }

    protected void LbReporteVenta_Click(object sender, EventArgs e)
    {
       Response.Redirect("~/Views/ReporteOrdenVenta.aspx");
    }
  }
}
