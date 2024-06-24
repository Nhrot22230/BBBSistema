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
  public partial class PersonasEmpleados : System.Web.UI.Page
  {
    private PersonasAPIClient personasAPIClient = new PersonasAPIClient();
    private BindingList<empleado> empleados;
    private BindingList<empleado> empleadosFiltrados;

    private bool CargarTabla(String filtro)
    {
      empleado[] lista = personasAPIClient.listarEmpleados(filtro);
      if (lista == null) return false;

      empleados = new BindingList<empleado>(lista.ToList());
      AplicarFiltro();
      GridEmpleado.DataSource = empleadosFiltrados;
      GridEmpleado.DataBind();
      return true;
    }

    private void AplicarFiltro()
    {
      if (DropDownListRoles.SelectedValue == "Todos")
      {
        empleadosFiltrados = empleados;
        return;
      }

      rol rolSelected = (rol)Enum.Parse(typeof(rol), DropDownListRoles.SelectedValue);
      empleadosFiltrados = new BindingList<empleado>(empleados.Where(e => e.rol == rolSelected).ToList());
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Empleados";

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridEmpleado_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void BtnAgregar_Click(object sender, EventArgs e)
    {
      Session["empleadoEditar"] = null;
      Response.Redirect("~/Views/PersonasEmpleadosForms.aspx");
    }
    protected void BtnEditar_Click(object sender, EventArgs e)
    {
      int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
      empleado empEditar = empleados.FirstOrDefault(emp => emp.idEmpleadoNumerico == idEmpleado);
      Session["empleadoEditar"] = empEditar;
      Response.Redirect("~/Views/PersonasEmpleadosForms.aspx");
    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      bool flag = CargarTabla(TxtBuscar.Text);
      if (flag)
      {
        MostrarMensaje($"Se encontraron {empleadosFiltrados.Count} empleados", flag);
      }
      else
      {
        MostrarMensaje("No se encontraron empleados", flag);
      }
    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
      int res = personasAPIClient.eliminarEmpleado(idEmpleado);
      string mensaje = res > 0 ? "Empleado eliminado correctamente" : "Error al eliminar al Empleado";
      MostrarMensaje(mensaje, res > 0);

      CargarTabla("");
    }

    protected void GridEmpleado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GridEmpleado.PageIndex = e.NewPageIndex;
      BindGrid();
    }

    protected void DropDownListRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
      AplicarFiltro();
      BindGrid();
    }

    private void BindGrid()
    {
      GridEmpleado.DataSource = empleadosFiltrados;
      GridEmpleado.DataBind();
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
  }
}