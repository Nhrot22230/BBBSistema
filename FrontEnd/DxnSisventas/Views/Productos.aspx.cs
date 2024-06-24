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
  public partial class Productos : System.Web.UI.Page
  {
    private ProductosAPIClient productosAPIClient;
    private BindingList<producto> BlProductos;

    // Constructor
    public Productos()
    {
      productosAPIClient = new ProductosAPIClient();
    }

    // Page events
    protected void Page_Init(object sender, EventArgs e)
    {
      Page.Title = "Productos";
      CargarTabla("");

      if(Session["empleado"] == null)
      {
        return;
      }

      rol rolUsuario = ((empleado)Session["empleado"]).rol;

      if (rolUsuario.Equals(rol.EncargadoAlmacen))
      {
        Response.Redirect("~/Home.aspx");
      }
      if (rolUsuario.Equals(rol.Repartidor))
      {
        BtnAgregar.Visible = false;
        GvProductos.Columns[8].Visible = false;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      TxtId.Enabled = false;
    }

    // GridView events
    protected void GvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      GvProductos.PageIndex = e.NewPageIndex;
      BindGrid();
    }

    // Button events
    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
      bool flag = CargarTabla(TxtBuscar.Text);
      if (flag)
      {
        MostrarMensaje($"Se encontraron {BlProductos.Count} productos", flag);
      }
      else
      {
        MostrarMensaje("No se encontraron productos", flag);
      }
    }

    protected void BtnAgregar_Click(object sender, EventArgs e)
    {
      LimpiarFormulario();
      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {
      int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
      CargarProductoEnFormulario(idProducto);
      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
      int res = productosAPIClient.eliminarProducto(idProducto);
      string mensaje = res > 0 ? "Producto eliminado correctamente" : "Error al eliminar el producto";
      MostrarMensaje(mensaje, res > 0);

      CargarTabla("");
    }

    protected void ButGuardar_Click(object sender, EventArgs e)
    {
      GuardarProducto();
      CargarTabla("");
    }

    // Helper methods
    private bool CargarTabla(string search)
    {
      producto[] productos = productosAPIClient.listarProductos(search);
      if (productos == null)
      {
        return false;
      }

      BlProductos = new BindingList<producto>(productos.ToList());
      BindGrid();
      return true;
    }

    private void BindGrid()
    {
      GvProductos.DataSource = BlProductos;
      GvProductos.DataBind();
    }

    private void LimpiarFormulario()
    {
      TxtNombre.Text = "";
      TxtStock.Text = "";
      TxtPrecio.Text = "";
      TxtPuntos.Text = "";
      TxtCapacidad.Text = "";
      ddlTipoProducto.SelectedValue = "0";
      ddlUnidadMedida.SelectedValue = "0";
      TxtId.Text = "";
      Session["idProducto"] = null;
    }

    private void CargarProductoEnFormulario(int idProducto)
    {
      producto p = BlProductos.SingleOrDefault(x => x.idProductoNumerico == idProducto);
      if (p != null)
      {
        Session["idProducto"] = p.idProductoNumerico;
        TxtNombre.Text = p.nombre;
        TxtStock.Text = p.stock.ToString();
        TxtPrecio.Text = p.precioUnitario.ToString("N2");
        TxtPuntos.Text = p.puntos.ToString();
        TxtCapacidad.Text = p.capacidad.ToString();
        ddlTipoProducto.SelectedValue = p.tipo.ToString();
        ddlUnidadMedida.SelectedValue = p.unidadDeMedida.ToString();
        TxtId.Text = p.idProductoCadena;
      }
    }

    private void GuardarProducto()
    {
      producto p = new producto
      {
        nombre = TxtNombre.Text,
        stock = Int32.Parse(TxtStock.Text),
        precioUnitario = Math.Round(Double.Parse(TxtPrecio.Text), 2),
        puntos = Int32.Parse(TxtPuntos.Text),
        capacidad = Double.Parse(TxtCapacidad.Text),
        tipo = (tipoProducto)Enum.Parse(typeof(tipoProducto), ddlTipoProducto.SelectedValue),
        tipoSpecified = true,
        unidadDeMedida = (unidadMedida)Enum.Parse(typeof(unidadMedida), ddlUnidadMedida.SelectedValue),
        unidadDeMedidaSpecified = true,
        idProductoNumerico = Session["idProducto"] != null ? (int)Session["idProducto"] : 0
      };

      int res = p.idProductoNumerico > 0 ? productosAPIClient.actualizarProducto(p) : productosAPIClient.insertarProducto(p);
      string mensaje = res > 0 ? "Producto guardado correctamente" : "Error al guardar el producto";
      MostrarMensaje(mensaje, res > 0);
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
