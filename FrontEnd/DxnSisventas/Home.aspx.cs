using DxnSisventas.BBBWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas
{
  public partial class Home : System.Web.UI.Page
  {
    public empleado emp;
    public cuentaEmpleado datosCuenta;

    protected void Page_Init(object sender, EventArgs e)
    {
      if (Session["empleado"] == null || Session["datosCuenta"] == null)
      {
        Response.Redirect("~/Login.aspx");
      }

      emp = (empleado)Session["empleado"];
      datosCuenta = (cuentaEmpleado)Session["datosCuenta"];
      Page.Title = "Bienvenido, " + emp.nombre + " " + emp.apellidoPaterno + "!";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
  }
}