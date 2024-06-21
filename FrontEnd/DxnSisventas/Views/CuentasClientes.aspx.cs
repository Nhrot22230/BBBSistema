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
  public partial class CuentasClientes : System.Web.UI.Page
  {
    private CuentasAPIClient cuentasAPIClient = new CuentasAPIClient();
    private BindingList<personaCuenta> BlClientesCuentas;

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Cuentas de Clientes";
      CargarTabla("");
      GridBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void LimpiarCampos()
    {
      Session["cuentaCrearEditar"] = null;
      Session["idCliente"] = null;
      TxtIdEmpleado.Text = "";
      TxtIdCuenta.Text = "";
      TxtUsuario.Text = "";
      TxtContrasena.Text = "";
    }

    private bool CargarTabla(string filtro)
    {
      personaCuenta[] lista = cuentasAPIClient.listarClientesMasCuentas(filtro);
      if (lista == null)
      {
        return false;
      }
      BlClientesCuentas = new BindingList<personaCuenta>(lista.ToList());

      return true;
    }

    private void GridBind()
    {
      GridClienteCuenta.DataSource = BlClientesCuentas;
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
        GridBind();
        MostrarMensaje($"Se encontraron {BlClientesCuentas.Count} resultados", true);
      }
      else
      {
        MostrarMensaje("No se encontraron resultados", false);
      }
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
        Label lblIdCliente = (Label)e.Row.FindControl("LblIdCliente");
        LinkButton btnEditar = (LinkButton)e.Row.FindControl("BtnEditar");
        LinkButton btnEliminar = (LinkButton)e.Row.FindControl("BtnEliminar");

        int idCliente = ((cliente)pc.persona).idNumerico;
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

        btnEditar.CommandArgument = idCliente.ToString();
        btnEliminar.CommandArgument = idCliente.ToString();
        lblIdCliente.Text = ((cliente)pc.persona).idCadena;
      }
    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {
      LinkButton btn = (LinkButton)sender;
      int idCliente = int.Parse(btn.CommandArgument);

      personaCuenta pc = BlClientesCuentas.FirstOrDefault(x => ((cliente)x.persona).idNumerico == idCliente);
      cuentaCliente cuenta = (cuentaCliente)pc.cuenta;
      
      cliente cli = (cliente)pc.persona;
      
      TxtIdEmpleado.Text = cli.idCadena;
      
      Session["cuentaCrearEditar"] = cuenta;
      Session["idCliente"] = cli.idNumerico;
      
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
      int idCliente = int.Parse(btn.CommandArgument);

      int result;

      result = cuentasAPIClient.eliminarCuentaCliente(idCliente);

      if (result == 0)
      {
        MostrarMensaje("Error al eliminar la cuenta", false);
      }
      else
      {
        MostrarMensaje("Cuenta eliminada con éxito", true);
      }

      CargarTabla("");
      GridBind();
      LimpiarCampos();
    }

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {
      if (Session["idCliente"] == null)
      {
        MostrarMensaje("No se ha seleccionado un empleado", false);
        return;
      }

      cuentaCliente cuenta = new cuentaCliente
      {
        usuario = TxtUsuario.Text,
        contrasena = TxtContrasena.Text,
        fid_Cliente = (int)Session["idCliente"]
      };

      int result;
      if (Session["cuentaCrearEditar"] == null)
      {
        result = cuentasAPIClient.insertarCuentaCliente(cuenta);
      }
      else
      {
        cuenta.idCuenta = ((cuentaCliente)Session["cuentaCrearEditar"]).idCuenta;
        result = cuentasAPIClient.actualizarCuentaCliente(cuenta);
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
      GridBind();
    }
  }
}