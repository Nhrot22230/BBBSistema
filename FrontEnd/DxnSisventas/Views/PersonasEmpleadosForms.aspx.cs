using DxnSisventas.BBBWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
  public partial class PersonasEmpleadosForms : System.Web.UI.Page
  {
    private empleado empTemporal;
    private PersonasAPIClient personasAPIClient = new PersonasAPIClient();

    protected void Page_Init(object sender, EventArgs e)
    {
      TxtId.Enabled = false;
      if (Session["empleadoEditar"] != null)
      {
        empTemporal = (empleado)Session["empleadoEditar"];
        Page.Title = "Editar Empleado: " + empTemporal.idEmpleadoCadena;
        TxtId.Text = empTemporal.idEmpleadoCadena;
        TxtNombre.Text = empTemporal.nombre;
        TxtApellidoPat.Text = empTemporal.apellidoPaterno;
        TxtApellidoMat.Text = empTemporal.apellidoMaterno;
        TxtDNI.Text = empTemporal.DNI;
        TxtSueldo.Text = empTemporal.sueldo.ToString();
        DropDownListRoles.Enabled = ((empleado)Session["empleado"]).rol == rol.Administrador;
        DropDownListRoles.SelectedValue = empTemporal.rol.ToString();
      }
      else
      {
        Page.Title = "Nuevo Empleado";
        empTemporal = new empleado();
        LimpiarCampos();
        DropDownListRoles.Enabled = true;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnRegresar_Click(object sender, EventArgs e)
    {
      LimpiarCampos();
      Response.Redirect("~/Views/PersonasEmpleados.aspx");
    }

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {
      empTemporal.nombre = TxtNombre.Text;
      empTemporal.apellidoPaterno = TxtApellidoPat.Text;
      empTemporal.apellidoMaterno = TxtApellidoMat.Text;
      empTemporal.DNI = TxtDNI.Text;
      empTemporal.sueldo = Double.Parse(TxtSueldo.Text);
      empTemporal.rol = (rol)Enum.Parse(typeof(rol), DropDownListRoles.SelectedValue);
      empTemporal.rolSpecified = true;
      
      int idEmpInsertado;

      if (empTemporal.idEmpleadoCadena == null)
      {
        idEmpInsertado = personasAPIClient.insertarEmpleado(empTemporal);
      }
      else
      {
        idEmpInsertado = personasAPIClient.actualizarEmpleado(empTemporal);
      }

      if (idEmpInsertado == 0)
      {
        MostrarMensaje("Error al guardar el empleado", false);
        return;
      }

      LimpiarCampos();
      Response.Redirect("~/Views/PersonasEmpleados.aspx");
    }

    private void MostrarMensaje(string mensaje, bool exito)
    {
      if (this.Master is Main master)
      {
        if (exito)
        {
          master.MostrarExito(mensaje);
        }
        else
        {
          master.MostrarError(mensaje);
        }
      }
    }

    private void LimpiarCampos()
    {
      Session["empleadoEditar"] = null;
      TxtId.Text = "";
      TxtNombre.Text = "";
      TxtApellidoPat.Text = "";
      TxtApellidoMat.Text = "";
      TxtDNI.Text = "";
      TxtSueldo.Text = "";
    }
  }
}