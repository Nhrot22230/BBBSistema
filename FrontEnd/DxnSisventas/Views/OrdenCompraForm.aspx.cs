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
  public partial class OrdenCompraForm : System.Web.UI.Page
  {
    private ProductosAPIClient apiProducto = new ProductosAPIClient();
    private DocumentosAPIClient apiOrdenCompra = new DocumentosAPIClient();
    private CorreosAPIClient apiCorreo = new CorreosAPIClient();
    private ReportesAPIClient apiReportes = new ReportesAPIClient();
    // listas
    private BindingList<producto> listaProductos = null;
    private ordenCompra ordenCompra;
    private static List<lineaOrden> lineasOrden;
    private static List<lineaOrden> lineasEliminadas;
    private static List<lineaOrden> lineasAgregadas;
    static OrdenCompraForm()
    {
      lineasOrden = new List<lineaOrden>();
      lineasEliminadas = new List<lineaOrden>();
      lineasAgregadas = new List<lineaOrden>();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      if (apiProducto.listarProductos(txtNombreProductoModal.Text) != null)
      {
        listaProductos = new BindingList<producto>(apiProducto.listarProductos(txtNombreProductoModal.Text).ToList());
        gvProductos.DataSource = listaProductos;
        gvProductos.DataBind();
      }

      String accion = Request.QueryString["accion"];
      if (accion != null && accion == "ver" && Session["idOrdenVenta"] != null)
      {
        int idOrdenVenta = (int)Session["idOrdenVenta"];

        cardInfo.Visible = true;
        if (Session["ordenCompra"] != null)
          ordenCompra = (ordenCompra)Session["ordenCompra"];
        else

          ordenCompra = apiOrdenCompra.listarOrdenCompra(idOrdenVenta.ToString()).FirstOrDefault(x => x.idOrdenCompraNumerico == idOrdenVenta);
        //denCompra.lineasOrden = apiOrdenCompra.listarLineaOrden(ordenCompra.idOrden);
        if (ordenCompra.estado.ToString() == "Entregado")
        {
          gvLineasOrdenVenta.Columns[5].Visible = false;
          ocultarProducto();
        }
        Session["lineasOrdenVenta"] = ordenCompra.lineasOrden;
        lineasOrden = ordenCompra.lineasOrden.ToList();
        mostrarDatos();
      }
      else
      {
        if (accion != null && accion == "new" && Session["idOrdenVenta"] == null)
          btnEnviar.Enabled = false;
        cardInfo.Visible = false;
        lineasAgregadas.Clear();
        ordenCompra = new ordenCompra();
        if (!IsPostBack)
        {
          Session["idOrdenVenta"] = null;
          Session["lineasOrdenVenta"] = null;
          Session["producto"] = null;
          Session["cliente"] = null;
          Session["correo"] = null;
          lineasOrden.Clear();
          lineasEliminadas.Clear();
          lineasAgregadas.Clear();
        }
      }
      if (Session["lineasOrdenVenta"] == null) { }

      //ordenCompra.lineasOrden = new BindingList<lineaOrden>();
      else
        gvLineasOrdenVenta.DataSource = lineasOrden;
      gvLineasOrdenVenta.DataBind();
      calcularTotal();
      gvLineasOrdenVenta.DataSource = lineasOrden;
      gvLineasOrdenVenta.DataBind();
      Session["correo"] = null;
    }

    protected void ocultarProducto()
    {
      lblIDProducto.Visible = false;
      txtIDProducto.Visible = false;
      btnBuscarProducto.Visible = false;
      txtNombreProducto.Visible = false;
      lblNombreProducto.Visible = false;
      lblPrecioUnitProducto.Visible = false;
      txtPrecioUnitProducto.Visible = false;
      lblCantidadUnidades.Visible = false;
      txtCantidadUnidades.Visible = false;
      lbAgregarLOV.Visible = false;
    }
    public void mostrarDatos()
    {
      txtEstado.Text = ordenCompra.estado.ToString();
      txtID.Text = ordenCompra.idOrdenCompraCadena.ToString();
      txtFecha.Text = ordenCompra.fechaCreacion.ToString();
      if (ordenCompra.fechaRecepcion != null && ordenCompra.fechaRecepcion > new DateTime(2000, 1, 1))
        txtFechaEnvio.Text = ordenCompra.fechaRecepcion.ToString();
      else
        txtFechaEnvio.Text = "-";
      if (ordenCompra.estado.ToString() == estadoOrden.Pendiente.ToString())
      {
        btnGuardar.Enabled = true;
        lbAgregarLOV.Enabled = true;
        txtCantidadUnidades.Enabled = true;
        btnBuscarProducto.Enabled = true;

      }
      else
      {
        btnGuardar.Enabled = false;
        lbAgregarLOV.Enabled = false;
        txtCantidadUnidades.Enabled = false;

        btnBuscarProducto.Enabled = false;

      }
    }

    protected void BtnRegresar_Click(object sender, EventArgs e)
    {

      Response.Redirect("/Views/OrdenesCompra.aspx");

    }

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {

      Response.Redirect("/Views/OrdenesCompra.aspx");
    }

    protected void btnBuscarProducto_Click(object sender, EventArgs e)
    {
      string script = "window.onload = function() { showModalFormProducto() };";
      ClientScript.RegisterStartupScript(GetType(), "", script, true);


    }

    protected void lbBusquedaProductoModal_Click(object sender, EventArgs e)
    {
      bool respuesta = apiProducto.listarProductos(txtNombreProductoModal.Text) != null;
      if (respuesta)
      {
        listaProductos = new BindingList<producto>(apiProducto.listarProductos(txtNombreProductoModal.Text).ToList());
        gvProductos.DataSource = listaProductos;
        gvProductos.DataBind();
      }
      else
      {
        MostrarMensaje("No se encontraron resultados", respuesta);
      }
    }

    protected void btnSeleccionarProductoModal_Click(object sender, EventArgs e)
    {
      int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
      producto productoSeleccionado = listaProductos.SingleOrDefault(x => x.idProductoNumerico == idProducto);
      Session["producto"] = productoSeleccionado;
      txtNombreProducto.Text = productoSeleccionado.nombre + " " + productoSeleccionado.capacidad.ToString() + " " + productoSeleccionado.unidadDeMedida;
      txtPrecioUnitProducto.Text = productoSeleccionado.precioUnitario.ToString("N2");
      txtIDProducto.Text = productoSeleccionado.idProductoCadena.ToString();
      ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
    }


    protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        e.Row.Cells[2].Text = ((double)DataBinder.Eval(e.Row.DataItem, "precioUnitario")).ToString("N2");

      }
    }

    protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gvProductos.PageIndex = e.NewPageIndex;
      gvProductos.DataSource = listaProductos;
      gvProductos.DataBind();
    }
    public void calcularTotal()
    {
      ordenCompra.total = 0;
      foreach (lineaOrden lovb in lineasOrden)
        ordenCompra.total += lovb.subtotal;
      txtTotal.Text = ordenCompra.total.ToString("N2");
    }
    protected void lbAgregarLOV_Click(object sender, EventArgs e)
    {
      if (Session["producto"] == null)
      {
        MostrarMensaje("No puede añadir sin haber seleccionado un producto...", Session["producto"] == null);
        return;
      }

      if (txtCantidadUnidades.Text.Trim().Equals("") || int.Parse(txtCantidadUnidades.Text.ToString()) <= 0)
      {
        MostrarMensaje("Debe ingresar una cantidad de unidades correcta...", txtCantidadUnidades.Text.Trim().Equals("") || int.Parse(txtCantidadUnidades.Text.ToString()) <= 0);
        return;
      }


      lineaOrden lov = new lineaOrden();
      lov.producto = (producto)Session["producto"];
      lov.cantidad = Int32.Parse(txtCantidadUnidades.Text);
      lov.subtotal = lov.producto.precioUnitario * lov.cantidad;
      ///linea r orden para la linea agregada
      ///
      lineaOrden lovAgregado = new lineaOrden();
      lovAgregado.producto = (producto)Session["producto"];
      lovAgregado.cantidad = Int32.Parse(txtCantidadUnidades.Text);
      lovAgregado.subtotal = lovAgregado.producto.precioUnitario * lovAgregado.cantidad;
      //
      lineaOrden existingLineaOrden = lineasOrden.Find(x => x.producto.idProductoNumerico == lov.producto.idProductoNumerico);

      if (existingLineaOrden != null)
      { ////// si la orden ya esta
        int cantidadPrevia = lineasOrden.Find(x => x.producto.idProductoNumerico == lov.producto.idProductoNumerico).cantidad;
        existingLineaOrden.cantidad += lov.cantidad;
        existingLineaOrden.subtotal += lov.cantidad * lov.producto.precioUnitario;
        if (Request.QueryString["accion"] == "ver")
        {


          lineaOrden existingLineaOrdenAgregada = lineasAgregadas.Find(x => x.producto.idProductoNumerico == lov.producto.idProductoNumerico);
          if (existingLineaOrdenAgregada != null)
          {   // esta en lineas agregadas
            existingLineaOrdenAgregada.cantidad += lov.cantidad;
            existingLineaOrdenAgregada.subtotal += lov.producto.precioUnitario * lov.cantidad;
          }
          else
          {
            // no esta en lineas agregadas por lo tanto se procede a agregar a lineas eliminadas para actualizar en base de datos
            lineasEliminadas.Add(lov);
            lovAgregado.cantidad += cantidadPrevia;
            lovAgregado.subtotal += cantidadPrevia * lovAgregado.producto.precioUnitario;
            lineasAgregadas.Add(lovAgregado);
          }

        }

      }
      else
      {
        // si la orden no esta 
        lineasOrden.Add(lov);
        if (Request.QueryString["accion"] == "ver")
        {
          lineasAgregadas.Add(lovAgregado);

          ordenCompra.lineasOrden = lineasOrden.ToArray();
          Session["ordenCompra"] = ordenCompra;
        }

      }
      Session["lineasOrdenVenta"] = lineasOrden;

      gvLineasOrdenVenta.DataSource = lineasOrden;
      gvLineasOrdenVenta.DataBind();

      if (Request.QueryString["accion"] == "ver")
      {
        //lineasAgregadas.Add(lov);

        ordenCompra.lineasOrden = lineasOrden.ToArray();
        Session["ordenCompra"] = ordenCompra;
      }

      calcularTotal();

      txtIDProducto.Text = "";
      txtNombreProducto.Text = "";
      txtPrecioUnitProducto.Text = "";
      txtCantidadUnidades.Text = "";
    }


    protected void btnEliminarProducto_Click(object sender, EventArgs e)
    {
      if (ordenCompra.estado.ToString() == estadoOrden.Entregado.ToString())
      {
        MostrarMensaje("La orden ya ha sido Entregada...", ordenCompra.estado.ToString() == estadoOrden.Entregado.ToString());
        return;
      }

      LinkButton btn = (LinkButton)sender;
      string commandArgs = btn.CommandArgument;
      string[] args = commandArgs.Split(',');

      int id = int.Parse(args[0]);
      int cantidad = int.Parse(args[1]);
      lineaOrden linea = lineasOrden.Find(l => l.producto.idProductoNumerico == id && l.cantidad == cantidad);
      lineasOrden.Remove(linea);
      if (Request.QueryString["accion"] == "ver")
        ordenCompra.lineasOrden = lineasOrden.ToArray();
      Session["ordenCompra"] = ordenCompra;
      lineaOrden lineaAgregada = lineasAgregadas.Find(l => l.producto.idProductoNumerico == id);
      if (lineaAgregada != null)
      {
        lineasAgregadas.Remove(lineaAgregada);
      }
      else
      {
        lineasEliminadas.Add(linea);
      }
      gvLineasOrdenVenta.DataSource = lineasOrden;
      gvLineasOrdenVenta.DataBind();
      calcularTotal();
      //Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
      ordenCompra orden = new ordenCompra
      {
        total = double.Parse(txtTotal.Text),
        fechaCreacionSpecified = true,
        estadoSpecified = true,
        fechaCreacion = DateTime.Now,
        estado = estadoOrden.Pendiente,
      };
      orden.lineasOrden = new ArraySegment<lineaOrden>().ToArray();
      for (int i = 0; i < lineasOrden.Count; i++)
      {
        orden.lineasOrden.Append(lineasOrden[i]);
      }
      orden.lineasOrden = lineasOrden.ToArray();


      //Validacion de que exista por lo menos 1 linea de orden de venta
      if (lineasOrden == null || lineasOrden.Count() <= 0 || orden.lineasOrden.Count() == 0)
      {
        MostrarMensaje("Debe agregar un producto...", lineasOrden == null || lineasOrden.Count() <= 0 || orden.lineasOrden.Count() == 0);
        return;
      }
      if (Request.QueryString["accion"] == "ver")
      {
        ordenCompra.lineasOrden = lineasOrden.ToArray();
        ordenCompra.fechaCreacionSpecified = true;
        ordenCompra.estadoSpecified = true;
        ordenCompra.fechaRecepcionSpecified = true;
        foreach (lineaOrden x in lineasEliminadas)
        {
          apiOrdenCompra.eliminarLOV(ordenCompra.idOrden, x.producto.idProductoNumerico);

        }
        foreach (lineaOrden x in lineasAgregadas)
        {
          apiOrdenCompra.insertarLOV(x, ordenCompra.idOrden);

        }
        apiOrdenCompra.actualizarOrdenCompra(ordenCompra);
        lineasEliminadas.Clear();
        lineasAgregadas.Clear();

      }
      else
      {

        apiOrdenCompra.insertarOrdenCompra(orden);
      }
      lineasOrden.Clear();
      Response.Redirect("/Views/OrdenCompra.aspx");
    }

    protected void btnRegresar_Click1(object sender, EventArgs e)
    {
      lineasEliminadas.Clear();
      Session["ordenCompra"] = null;
      Session["idOrdenVenta"] = null;
      lineasOrden.Clear();
      lineasAgregadas.Clear();
      Response.Redirect("/Views/OrdenCompra.aspx");
    }

    protected void txtNombreProductoModal_TextChanged(object sender, EventArgs e)
    {
      if (apiProducto.listarProductos(txtNombreProductoModal.Text) != null)
      {
        listaProductos = new BindingList<producto>(apiProducto.listarProductos(txtNombreProductoModal.Text).ToList());
        gvProductos.DataSource = listaProductos;
        gvProductos.DataBind();
      }
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
      if (lineasOrden == null || lineasOrden.Count() <= 0)
      {
        MostrarMensaje("Debe agregar un producto...", lineasOrden == null || lineasOrden.Count() <= 0);
        return;
      }

      string script = "window.onload = function() { showModalFormEnviar() };";
      ClientScript.RegisterStartupScript(GetType(), "", script, true);
    }

    protected string CrearContenido()
    {
      string contenido = "<html><body style='font-family: Arial, sans-serif;'>";
      double total = 0;
      contenido += "<h2 style='text-align: center;'>Saludos Estimado(a),</h2>";
      contenido += "<p>Adjunto encontrará la orden de compra Nro: " + ordenCompra.idOrdenCompraCadena + ".</p>";
      contenido += "<p>A continuación, detallamos los productos:</p>";
      contenido += "<table style='width: 100%; border-collapse: collapse;'>";
      contenido += "<tr><th style='border: 1px solid #000; padding: 8px; text-align: center;'>ID</th><th style='border: 1px solid #000; padding: 8px;'>Nombre</th><th style='border: 1px solid #000; padding: 8px; text-align: center;'>Cantidad</th><th style='border: 1px solid #000; padding: 8px; text-align: center;'>Subtotal</th></tr>";

      foreach (lineaOrden x in lineasOrden)
      {
        contenido += "<tr>";
        contenido += "<td style='border: 1px solid #000; padding: 8px; text-align: center;'>" + x.producto.idProductoNumerico.ToString() + "</td>";
        contenido += "<td style='border: 1px solid #000; padding: 8px;'>" + x.producto.nombre + " " + x.producto.capacidad.ToString() + " " + x.producto.unidadDeMedida + "</td>";
        contenido += "<td style='border: 1px solid #000; padding: 8px; text-align: center;'>" + x.cantidad.ToString() + "</td>";
        contenido += "<td style='border: 1px solid #000; padding: 8px; text-align: center;'>" + x.subtotal.ToString("N2") + "</td>";
        contenido += "</tr>";
        total += x.subtotal;
      }

      contenido += "</table>";
      contenido += "<p style='text-align: right; font-weight: bold; margin-top: 20px;'>Total: " + total.ToString("N2") + "</p>";
      contenido += "<p>Quedamos a disposición para cualquier consulta.</p>";
      contenido += "<p>Atentamente,<br>BBB Ventas</p>";
      contenido += "</body></html>";

      return contenido;
    }
    private byte[] GetPdfFromWebService(int id)
    {
      byte[] pdfFile = null;
      try
      {
        pdfFile = apiReportes.generarReporteOrdenCompra(id);
      }
      catch (System.Exception ex)
      {
        MostrarMensaje("Error al obtener el archivo PDF" + ex, false);
        // Manejo de errores
      }

      return pdfFile;
    }
    protected string armarpdf()
    {
      byte[] pdfBytes = GetPdfFromWebService(ordenCompra.idOrdenCompraNumerico);
      if (pdfBytes != null)
      {
       
        string tempFolderPath = Server.MapPath("~/Temp");

        
        if (!System.IO.Directory.Exists(tempFolderPath))
        {
          System.IO.Directory.CreateDirectory(tempFolderPath);
        }
        string nombrearch = "Comprobante_" + ordenCompra.idOrdenCompraCadena + ".pdf";
        
        string tempFilePath = System.IO.Path.Combine(tempFolderPath, nombrearch);

        
        System.IO.File.WriteAllBytes(tempFilePath, pdfBytes);

        
        string base64Pdf = Convert.ToBase64String(pdfBytes);
        

        return tempFilePath;
      }
      return null;
    }

    protected void lbEnviaroModal_Click(object sender, EventArgs e)
    {

            string asunto = "Orden de compra Nro: " + ordenCompra.idOrdenCompraCadena;
            string contenido = CrearContenido();
            string correo = txtCorreo.Text.ToString();



            //string path = armarpdf();
            /////////////////////////tiene que terner la misma funcion que el boton de guardar////////////////////
            ///
            ordenCompra orden = new ordenCompra
            {
                total = double.Parse(txtTotal.Text),
                fechaCreacionSpecified = true,
                estadoSpecified = true,
                fechaCreacion = DateTime.Now,
                estado = estadoOrden.Pendiente,
            };
            orden.lineasOrden = new ArraySegment<lineaOrden>().ToArray();
            for (int i = 0; i < lineasOrden.Count; i++)
            {
                orden.lineasOrden.Append(lineasOrden[i]);
            }
            orden.lineasOrden = lineasOrden.ToArray();

            //Validacion de que exista por lo menos 1 linea de orden de venta
            if (lineasOrden == null || lineasOrden.Count() <= 0 || orden.lineasOrden.Count() == 0)
            {
                MostrarMensaje("Debe agregar un producto...", lineasOrden == null || lineasOrden.Count() <= 0 || orden.lineasOrden.Count() == 0);
                return;
            }
            if (Request.QueryString["accion"] == "ver" && txtEstado.Text == "Pendiente")
            {
                ordenCompra.lineasOrden = lineasOrden.ToArray();
                ordenCompra.fechaCreacionSpecified = true;
                ordenCompra.estadoSpecified = true;
                ordenCompra.fechaRecepcionSpecified = true;
                foreach (lineaOrden x in lineasEliminadas)
                {
                    apiOrdenCompra.eliminarLOV(ordenCompra.idOrden, x.producto.idProductoNumerico);

                }
                foreach (lineaOrden x in lineasAgregadas)
                {
                    apiOrdenCompra.insertarLOV(x, ordenCompra.idOrden);

                }
                apiOrdenCompra.actualizarOrdenCompra(ordenCompra);
            }



            //////////////a partir de aca se genera el correo///////////////////////////////////////////
            ordenCompra.estadoSpecified = true;
            ordenCompra.estado = estadoOrden.Entregado;
            ordenCompra.fechaRecepcionSpecified = true;
            ordenCompra.fechaRecepcion = DateTime.Now;
            if (txtEstado.Text != "Entregado")
                apiOrdenCompra.actualizarOrdenCompra(ordenCompra);
            apiCorreo.enviarCorreoWeb(asunto, contenido, correo, GetPdfFromWebService(ordenCompra.idOrdenCompraNumerico));
            Session["correo"] = "true";
            lineasOrden.Clear();
            lineasEliminadas.Clear();
            lineasAgregadas.Clear();
            Response.Redirect("/Views/OrdenCompra.aspx");
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

    protected void gvLineasOrdenVenta_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      gvLineasOrdenVenta.PageIndex = e.NewSelectedIndex;

      gvLineasOrdenVenta.DataSource = lineasOrden;
      gvLineasOrdenVenta.DataBind();
    }

    protected void gvLineasOrdenVenta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gvLineasOrdenVenta.PageIndex = e.NewPageIndex;

      gvLineasOrdenVenta.DataSource = lineasOrden;
      gvLineasOrdenVenta.DataBind();
    }

    protected void gvLineasOrdenVenta_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
      gvLineasOrdenVenta.PageIndex = e.NewPageIndex;

      gvLineasOrdenVenta.DataSource = lineasOrden;
      gvLineasOrdenVenta.DataBind();
    }
  }


}