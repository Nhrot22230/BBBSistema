using DxnSisventas.BBBWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
  public partial class CuentasEmpleados : System.Web.UI.Page
  {
    private CuentasAPIClient cuentasAPIClient = new CuentasAPIClient();
    private BindingList<personaCuenta> BlEmpleadosCuentas;
    private BindingList<personaCuenta> BlEmpleadosCuentasFiltrado;

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Cuentas de Empleados";

      if (Session["empleado"] == null)
      {
        return;
      }

      rol rolUsuario = ((empleado)Session["empleado"]).rol;
      if (rolUsuario != rol.Administrador)
      {
        Response.Redirect("~/Home.aspx");
      }

      CargarTabla("");
      AplicarFiltro();
      GridBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void AplicarFiltro()
    {
      string filtro = DropDownListRoles.SelectedValue;
      if (filtro == "Todos")
      {
        BlEmpleadosCuentasFiltrado = BlEmpleadosCuentas;
        return;
      }
      rol selectedRol = (rol)Enum.Parse(typeof(rol), filtro);
      BlEmpleadosCuentasFiltrado = new BindingList<personaCuenta>(BlEmpleadosCuentas.Where(
        x => ((empleado)x.persona).rol == selectedRol
        ).ToList());
    }

    private bool CargarTabla(string filtro)
    {
      personaCuenta[] lista = cuentasAPIClient.listarEmpleadosMasCuentas(filtro);
      if (lista == null)
      {
        return false;
      }
      BlEmpleadosCuentas = new BindingList<personaCuenta>(lista.ToList());

      return true;
    }

    private void GridBind()
    {
      GridClienteCuenta.DataSource = BlEmpleadosCuentasFiltrado;
      GridClienteCuenta.DataBind();
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

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      bool flag = CargarTabla(TxtBuscar.Text);
      if (flag)
      {
        AplicarFiltro();
        GridBind();
        MostrarMensaje($"Se encontraron {BlEmpleadosCuentasFiltrado.Count} resultados", true);
      }
      else
      {
        MostrarMensaje("No se encontraron resultados", false);
      }
    }

    protected void DropDownListRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
      AplicarFiltro();
      GridBind();
      MostrarMensaje("Se aplicó el filtro", true);
    }

    protected void GridClienteCuenta_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
      GridClienteCuenta.PageIndex = e.NewPageIndex;
      GridBind();
    }

    protected void GridClienteCuenta_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        empleado logedUser = (empleado)Session["empleado"];

        personaCuenta pc = (personaCuenta)e.Row.DataItem;
        Label lblIdEmpleado = (Label)e.Row.FindControl("LblIdEmpleado");
        LinkButton btnEditar = (LinkButton)e.Row.FindControl("BtnEditar");
        LinkButton btnEliminar = (LinkButton)e.Row.FindControl("BtnEliminar");

        int idEmpleado = ((empleado)pc.persona).idEmpleadoNumerico;
        if (pc.cuenta == null)
        {
          btnEditar.Text = "Crear Cuenta";
          btnEditar.CssClass = "btn btn-success";
          btnEliminar.Enabled = false;
          btnEliminar.Visible = false;
        }

        if (logedUser.rol != rol.Administrador)
        {
          btnEditar.Visible = false;
          btnEliminar.Visible = false;
        }

        btnEditar.CommandArgument = idEmpleado.ToString();
        btnEliminar.CommandArgument = idEmpleado.ToString();
        lblIdEmpleado.Text = ((empleado)pc.persona).idEmpleadoCadena;
      }
    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {
      LinkButton btn = (LinkButton)sender;
      int idEmpleado = int.Parse(btn.CommandArgument);

      personaCuenta pc = BlEmpleadosCuentasFiltrado.FirstOrDefault(x => ((empleado)x.persona).idEmpleadoNumerico == idEmpleado);
      cuentaEmpleado cuenta = (cuentaEmpleado)pc.cuenta;
      empleado emp = (empleado)pc.persona;
      TxtIdEmpleado.Text = emp.idEmpleadoCadena;
      Session["cuentaCrearEditar"] = cuenta;
      Session["idEmpleado"] = emp.idEmpleadoNumerico;
      if (cuenta != null)
      {
        TxtIdCuenta.Text = cuenta.idCuenta.ToString();
        TxtUsuario.Text = cuenta.usuario;
      }
      ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalCuenta();", true);
    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      LinkButton btn = (LinkButton)sender;
      int idEmpleado = int.Parse(btn.CommandArgument);

      int result;

      result = cuentasAPIClient.eliminarCuentaEmpleado(idEmpleado);

      if (result == 0)
      {
        MostrarMensaje("Error al eliminar la cuenta", false);
      }
      else
      {
        MostrarMensaje("Cuenta eliminada con éxito", true);
      }

      CargarTabla("");
      AplicarFiltro();
      GridBind();
      LimpiarCampos();
    }

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {
      if (Session["idEmpleado"] == null)
      {
        MostrarMensaje("No se ha seleccionado un empleado", false);
        return;
      }

      cuentaEmpleado cuenta = new cuentaEmpleado
      {
        usuario = TxtUsuario.Text,
        contrasena = TxtContrasena.Text,
        fid_Empleado = (int)Session["idEmpleado"]
      };

      int result;
      if (Session["cuentaCrearEditar"] == null)
      {
        result = cuentasAPIClient.insertarCuentaEmpleado(cuenta);
      }
      else
      {
        cuenta.idCuenta = ((cuentaEmpleado)Session["cuentaCrearEditar"]).idCuenta;
        result = cuentasAPIClient.actualizarCuentaEmpleado(cuenta);
      }

      if (result == 0)
      {
        MostrarMensaje("Error al guardar la cuenta", false);
      }
      else
      {
        MostrarMensaje("Cuenta guardada con éxito", true);
      }

      LimpiarCampos();
      CargarTabla("");
      AplicarFiltro();
      GridBind();
    }

    private void LimpiarCampos()
    {
      Session["cuentaCrearEditar"] = null;
      Session["idEmpleado"] = null;
      TxtIdEmpleado.Text = "";
      TxtIdCuenta.Text = "";
      TxtUsuario.Text = "";
      TxtContrasena.Text = "";
    }
  }
}