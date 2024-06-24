using DxnSisventas.BBBWebService;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
    public partial class OrdenVentaForm : System.Web.UI.Page
    {
        // apis
        private DocumentosAPIClient apiDocumentos;
        private PersonasAPIClient apiPersonas;
        private ProductosAPIClient apiProductos;
        // listas
        private BindingList<empleado> empleados;
        private BindingList<producto> productos;
        private BindingList<cliente> clientes;

        //
        private ordenVenta ordenVenta;
        private BindingList<lineaOrden> lineasOrden;

        private static string accion;
        private static bool aplicarDescuento;

        protected void Page_Init(object sender, EventArgs e)
        {
            
            colocarTituloPagina();
            inicializarApis();
            inhabilitarCamposInit();
            accionDePagina();                        
        }

        void accionDePagina()
        {
            accion = Request.QueryString["accion"];
            if (accion.Equals("visualizar") || accion.Equals("editar"))
            {
                
                lbBuscarCliente.Visible = false;
                lbBuscarCliente.Enabled = false;
                ddlTipoVenta.Enabled = false;


                ordenVenta = (ordenVenta)Session["ordenSeleccionada"];
                mostrarDatos();
                if (ddlTipoVenta.SelectedValue.Equals("Presencial"))
                {
                    panelRepartidor.Visible = false;
                }
                if (accion.Equals("visualizar"))
                {
                    desabilitarCampos();
                }

            }
            else if (accion.Equals("new"))
            {
             
                ddlEstado.Enabled = false;
                ordenVenta = new ordenVenta();
                TxtFechaCreacion.Text = DateTime.Now.ToString("yyyy-MM-dd");

                // no se puede crear una orden de venta con estado cancelado
                ddlEstado.Items.Remove(ddlEstado.Items.FindByValue("Cancelado"));
                TxtDescuento.Text = "0";
                TxtTotalSinDescuento.Text = "0.00";
                txtTotal.Text = "0.00";
                TxtPuntos.Text = "0";
                if (!IsPostBack)
                {
                    limpiarSesiones();
                }
            }
            llenarGridLineas();
        }

        private void colocarTituloPagina()
        {
            Page.Title = "Orden de Venta";
        }

        private void inhabilitarCamposInit()
        {
            TxtFechaCreacion.Enabled = false;
            TxtDescuento.Enabled = false;
            TxtFechaEntrega.Enabled = false;
        }

        private void inicializarApis()
        {
            apiDocumentos = new DocumentosAPIClient();
            apiPersonas = new PersonasAPIClient();
            apiProductos = new ProductosAPIClient();
        }

        private void desabilitarCampos()
        {

            // Txt
            TxtDescuento.Enabled = false;
            TxtCantidad.Enabled = false;
            TxtFechaEntrega.Enabled = false;

            // Btn
            btnBuscarProducto.Enabled = false;
            btnAgregarProducto.Enabled = false;

            // lb
            lbBuscarCliente.Visible = false;
            lbBuscarCliente.Enabled = false;
            lbBuscarRepartidor.Visible = false;
            lbBuscarRepartidor.Enabled = false;

            // ddl
            
            ddlMetodoDePago.Enabled = false;
            ddlTipoVenta.Enabled = false;
            ddlEstado.Enabled = false;

            // panels
            if (ddlTipoVenta.SelectedValue.ToString().Equals("Presencial"))
            {
                panelRepartidor.Visible = false;
            }
            panelBusquedaProducto.Visible = false;

            btnGuardar.Enabled = false;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private void mostrarDatos()
        {
            

            // Información de la orden de venta
            TxtIdOrdenVenta.Text = ordenVenta.idOrdenVentaCadena;
            ddlEstado.SelectedValue = ordenVenta.estado.ToString();
            ddlMetodoDePago.SelectedValue = ordenVenta.metodoPago.ToString();
            ddlTipoVenta.SelectedValue = ordenVenta.tipoVenta.ToString();
            TxtFechaCreacion.Text = ordenVenta.fechaCreacion.ToString("yyyy-MM-dd");
            if (ordenVenta.fechaEntrega.ToString("dd/MM/yyyy") == "01/01/0001")
            {
                TxtFechaEntrega.Text = "";
            }
            else
            {
                TxtFechaEntrega.Text = ordenVenta.fechaEntrega.ToString("yyyy-MM-dd");
            }     
            TxtDescuento.Text = ordenVenta.porcentajeDescuento.ToString();
            
            // Información del cliente
            cliente cliente = ordenVenta.cliente;
            Session["clienteSeleccionado"] = cliente;
            TxtIDCliente.Text = ordenVenta.cliente.idCadena;
            TxtNombreCompletoCliente.Text = cliente.nombre + " " +
                cliente.apellidoPaterno + " " + cliente.apellidoMaterno;
            TxtPuntos.Text = cliente.puntos.ToString();
            // Información del repartidor en caso exista
            if(ordenVenta.repartidor != null)
            {
                empleado empleado = ordenVenta.repartidor;
                Session["empleadoSeleccionado"] = empleado;
                TxtIDRepartidor.Text = empleado.idEmpleadoCadena;
                TxtNombreCompletoRepartidor.Text = empleado.nombre + " " +
                    empleado.apellidoPaterno + " " + empleado.apellidoMaterno;
            }
            else
            {
                TxtIDRepartidor.Text = "";
                TxtNombreCompletoRepartidor.Text = "";
            }

            // Detalle de orden de compra
            if (Session["lineasDeOrden"] == null)
            {
                Session["lineasDeOrden"] = new BindingList<lineaOrden>(ordenVenta.lineasOrden.ToList());
                Session["lineasOrdenAntiguas"] = new BindingList<lineaOrden>(ordenVenta.lineasOrden.ToList());
            }
            TxtDescuento.Text = ordenVenta.porcentajeDescuento.ToString();
            txtTotal.Text = ordenVenta.total.ToString("N2");
            TxtTotalSinDescuento.Text = (ordenVenta.total + ordenVenta.porcentajeDescuento).ToString("N2");
        }

        private void calcularDescuento()
        {
            // convertimos puntos a soles
            double descuento = double.Parse(TxtPuntos.Text) * 0.1;
            double totalSinDescuento = double.Parse(TxtTotalSinDescuento.Text);
            if(descuento > totalSinDescuento)
            {
                descuento = totalSinDescuento;  
            }
            TxtDescuento.Text = descuento.ToString("N2");
        }

        private void llenarGridLineas()
        {
            if (Session["lineasDeOrden"] == null)
            {
                lineasOrden = new BindingList<lineaOrden>();
            }
            else
            {
                lineasOrden = (BindingList<lineaOrden>)Session["lineasDeOrden"];
            }
            gvLineasOrdenVenta.DataSource = lineasOrden;
            gvLineasOrdenVenta.DataBind();
        }
        private void llenarGridEmpleados(String busqueda)
        {
            empleado[] empleadosAux = apiPersonas.listarEmpleados(busqueda);
            if(empleadosAux != null)
            {
                empleadosAux = empleadosAux.Where(e => e.rol == rol.Repartidor).ToArray();
            }    
            if(empleadosAux == null)
            {
                empleados = new BindingList<empleado>();
            }
            else
            {
                empleados = new BindingList<empleado>(empleadosAux.ToList());
            }
            gvEmpleados.DataSource = empleados;
            gvEmpleados.DataBind();
        }

        private void llenarGridProductos(String busqueda)
        {
            producto[] productosAxu = apiProductos.listarProductos(busqueda);
            if(productosAxu != null)
            {
                productosAxu = productosAxu.Where(p => p.stock > 0).ToArray();
            }

            if (productosAxu == null)
            {
                productos = new BindingList<producto>();
            }
            else
            {
                productos = new BindingList<producto>(productosAxu.ToList());
            }
            gvProductos.DataSource = productos;
            gvProductos.DataBind();
        }

        private void llenarGridClientes(String busqueda)
        {
            cliente[] clientesAux = apiPersonas.listarClientes(busqueda);
            if (clientesAux == null)
            {
                clientes = new BindingList<cliente>();
            }
            else
            {
                clientes = new BindingList<cliente>(clientesAux.ToList());
            }
            gvClientes.DataSource = clientes;
            gvClientes.DataBind();
        }

        private void CallJavascript(string function, string modalId)
        {
            string script = $"window.onload = function() {{ {function}('{modalId}'); }};";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            int idProducto = Int32.Parse(((Button)sender).CommandArgument);
            lineaOrden linea = lineasOrden.Where(lo => lo.producto.idProductoNumerico == idProducto).FirstOrDefault();
            lineasOrden.Remove(linea);
            Session["lineasDeOrden"] = lineasOrden;
            calcularTotales();
            llenarGridLineas();
        }

        protected void gvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            llenarGridEmpleados(TxtPatronBusquedaEmpleado.Text);
            gvEmpleados.PageIndex = e.NewPageIndex;
            gvEmpleados.DataBind();
        }
        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            llenarGridClientes(TxtPatronBusquedaCliente.Text);
            gvClientes.PageIndex = e.NewPageIndex;
            gvClientes.DataBind();
        }
        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            llenarGridProductos(TxtPatronBusquedaProducto.Text);
            gvProductos.PageIndex = e.NewPageIndex;
            gvProductos.DataBind();
        }
        protected void btnSeleccionarModalEmpleado_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);         
            llenarGridEmpleados("");
            empleado empleado = empleados.Where(c => c.idEmpleadoNumerico == idEmpleado).FirstOrDefault();
            Session["empleadoSeleccionado"] = empleado;
            TxtIDRepartidor.Text = empleado.idEmpleadoCadena;
            TxtNombreCompletoRepartidor.Text = empleado.nombre + " " +
                empleado.apellidoPaterno + " " + empleado.apellidoMaterno;
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }
        protected void btnSeleccionarModalCliente_Click(object sender, EventArgs e)
        {
            int idCliente = Int32.Parse(((LinkButton)sender).CommandArgument);
            llenarGridClientes("");
            cliente cliente = clientes.Where(c => c.idNumerico == idCliente).FirstOrDefault();
            Session["clienteSeleccionado"] = cliente;
            TxtIDCliente.Text = cliente.idCadena.ToString();
            TxtNombreCompletoCliente.Text = cliente.nombre + " " +
                cliente.apellidoPaterno + " " + cliente.apellidoMaterno;
            TxtPuntos.Text = cliente.puntos.ToString();
            TxtDescuento.Text = cliente.puntos * 0.1 + "";
            calcularTotales();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }
        protected void lbBuscarClienteModal_Click(object sender, EventArgs e)
        {
            string busqueda = TxtPatronBusquedaCliente.Text;
            llenarGridClientes(busqueda);
        }
        protected void lbBuscarProductos_Click(object sender, EventArgs e)
        {
            string busqueda = TxtPatronBusquedaProducto.Text;
            llenarGridProductos(busqueda);
        }

        protected void btnSeleccionarModalProducto_Click(object sender, EventArgs e)
        {
            int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
            llenarGridProductos("");
            producto producto = productos.Where(c => c.idProductoNumerico == idProducto).FirstOrDefault();
            Session["productoSeleccionado"] = producto;
            TxtIdProducto.Text = producto.idProductoCadena;
            TxtNombreProducto.Text = producto.nombre;
            TxtPrecio.Text = producto.precioUnitario.ToString("N2");
            TxtStock.Text = producto.stock.ToString();
      
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

        protected void lbBuscarEmpleadoModal_Click(object sender, EventArgs e)
        {
            string busqueda = TxtPatronBusquedaEmpleado.Text;
            llenarGridEmpleados(busqueda);
        }

        protected void lbBuscarCliente_Click(object sender, EventArgs e)
        {
            llenarGridClientes("");
            CallJavascript("showModalForm", "modalFormBuscarCliente");
        }

        protected void lbBuscarRepartidor_Click(object sender, EventArgs e)
        {
            llenarGridEmpleados("");
            CallJavascript("showModalForm", "modalFormBuscarEmpleado");
        }
        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            llenarGridProductos("");
            CallJavascript("showModalForm", "modalFormBuscarProducto");
        }

        private bool validarCantidadIngresada()
        {
            // Validar cantidad ingresada
            if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MostrarMensaje("Ingrese una cantidad válida", false);
                return false;
            }
            return true;
        }

        private bool validarStockDisponible()
        {
            producto productoSeleccionado = (producto)Session["productoSeleccionado"];
            int cantidad = int.Parse(TxtCantidad.Text);
            if (productoSeleccionado.stock < cantidad)
            {
                MostrarMensaje("La cantidad supera el stock disponible", false);
                return false;
            }
            return true;
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (!validarSeleccionProducto()) return;
          
            producto productoSeleccionado = (producto)Session["productoSeleccionado"];


            if (!validarCantidadIngresada()) return;

            int cantidad = int.Parse(TxtCantidad.Text);

            if (!validarStockDisponible()) return;
            
           
            lineaOrden lineaExistente = lineasOrden.FirstOrDefault(lo => lo.producto.idProductoNumerico == productoSeleccionado.idProductoNumerico);
            if (lineaExistente != null)
            {
              
                if (lineaExistente.cantidad + cantidad > productoSeleccionado.stock)
                {
                    MostrarMensaje("La cantidad supera el stock disponible", false);
                    return;
                }
              
                lineaExistente.cantidad += cantidad;
                lineaExistente.subtotal = CalcularSubtotal(lineaExistente.cantidad, lineaExistente.producto.precioUnitario);
            }
            else
            {

                lineaOrden nuevaLinea = CrearNuevaLineaOrden(productoSeleccionado, cantidad);
                lineasOrden.Add(nuevaLinea);   
            }
            
            Session["lineasDeOrden"] = lineasOrden;
            calcularTotales();           
            llenarGridLineas();
            LimpiarCamposProducto();
        }

        private void calcularTotales()
        {
            calcularTotalSinDescuento();
            calcularDescuento();
            txtTotal.Text = double.Parse(TxtTotalSinDescuento.Text) - double.Parse(TxtDescuento.Text) + "";
        }

        private void calcularTotalSinDescuento()
        {
            double total = lineasOrden.Sum(lo => lo.subtotal);
            TxtTotalSinDescuento.Text = total.ToString("N2");
        }

        private bool validarSeleccionProducto()
        {
            if (Session["productoSeleccionado"] == null)
            {
                MostrarMensaje("Debe seleccionar un producto", false);
                return false;
            }
            return true;
        }

        private double CalcularSubtotal(int cantidad, double precioUnitario)
        {
            return cantidad * precioUnitario;
        }
        private lineaOrden CrearNuevaLineaOrden(producto productoSeleccionado, int cantidad)
        {
            return new lineaOrden
            {
                producto = productoSeleccionado,
                cantidad = cantidad,
                subtotal = CalcularSubtotal(cantidad, productoSeleccionado.precioUnitario)
            };
        }
        private void LimpiarCamposProducto()
        {
            TxtIdProducto.Text = string.Empty;
            TxtNombreProducto.Text = string.Empty;
            TxtPrecio.Text = string.Empty;
            TxtCantidad.Text = string.Empty;
            TxtStock.Text = string.Empty;
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
        bool verificarFechaEntrega()
        {
            DateTime fechaEntrega = DateTime.Parse(TxtFechaEntrega.Text);
            DateTime fechaCreacion = DateTime.Parse(TxtFechaCreacion.Text);
            if (fechaEntrega < fechaCreacion)
            {
                return false;
            }
            return true;
        }
        bool validarFechaDeEntrega()
        {
            if (!string.IsNullOrWhiteSpace(TxtFechaEntrega.Text))
            {
                if (!verificarFechaEntrega())
                {
                    MostrarMensaje("La fecha de entrega no puede ser menor a la fecha de creación", false);
                    return false;
                }
                else
                {
                    ordenVenta.fechaEntrega = DateTime.Parse(TxtFechaEntrega.Text);
                }

            }
            return true;
        }
        bool validarCliente()
        {
            if (Session["clienteSeleccionado"] is cliente clienteSeleccionado)
            {
                ordenVenta.cliente = clienteSeleccionado;
                return true;
            }
            else
            {
                MostrarMensaje("Seleccione un cliente.", false);
                return false;
            }
        }
        bool validarRepartidor()
        {
            if (ddlTipoVenta.SelectedValue.Equals("Delivery"))
            {
                if (Session["empleadoSeleccionado"] is empleado empleadoRepartidorSeleccionado)
                {
                    ordenVenta.repartidor = empleadoRepartidorSeleccionado;
                    return true;
                }
                else
                {
                    MostrarMensaje("Seleccione un repartidor válido.", false);
                    return false;
                }
            }
            return true;
        }
        bool validarEncargadoVentas()
        {
            if (Session["empleado"] is empleado empleadoEncargado)
            {
                ordenVenta.encargadoVenta = empleadoEncargado;
                return true;
            }
            else
            {
                MostrarMensaje("El empleado encargado de la venta no es válido.", false);
                return false;
            }
        }
        bool validarLineasOrden()
        {
            if (Session["lineasDeOrden"] is BindingList<lineaOrden> lineasDeOrden && lineasDeOrden.Any())
            {
                ordenVenta.lineasOrden = lineasDeOrden.ToArray();
                return true;
            }
            else
            {
                MostrarMensaje("Agregue al menos una línea a la orden.", false);
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            

            ordenVenta.estadoSpecified = true;
            ordenVenta.estado = (estadoOrden)Enum.Parse(typeof(estadoOrden), 
                ddlEstado.SelectedValue.ToString());
            
            ordenVenta.metodoPagoSpecified = true;
            ordenVenta.metodoPago = (metodoPago)Enum.Parse(typeof(metodoPago), 
                ddlMetodoDePago.SelectedValue.ToString());
            
            ordenVenta.tipoVentaSpecified = true;
            ordenVenta.tipoVenta = (tipoVenta)Enum.Parse(typeof(tipoVenta),
                ddlTipoVenta.SelectedValue.ToString());
            
            ordenVenta.fechaCreacionSpecified = true;
            ordenVenta.fechaCreacion = DateTime.Parse(TxtFechaCreacion.Text);


            ordenVenta.fechaEntregaSpecified = true;
            if (!validarFechaDeEntrega()) return;
            if (!validarCliente()) return;
            if (!validarRepartidor()) return;
            if (!validarEncargadoVentas()) return;
            if (!validarLineasOrden()) return;

            ordenVenta.porcentajeDescuento = double.Parse(TxtDescuento.Text);
            ordenVenta.total = Double.Parse(txtTotal.Text);

           


            int res = 0;
            string mensaje = "";
            if(accion.Equals("new"))
            {
                res = apiDocumentos.insertarOrdenVenta(ordenVenta);        
                if(res <= 0) mensaje = "Error al registrar la orden de venta";
                if(res > 0)
                {
                    if(ordenVenta.estado == estadoOrden.Entregado)
                    {
                        actualizarStockProductos();
                        actualizarPuntosCliente();
                        actualizarPuntosPatrocinador();
                        actualizarPuntosProductos();
                    }
                }                
            }
            else if(accion.Equals("editar"))
            {
                              
                res = apiDocumentos.actualizarOrdenVenta(ordenVenta);
                if(res <= 0) mensaje = "Error al actualizar la orden de venta";
                if(res > 0)
                {
                    actualizarLineasOrden();
                    if (ordenVenta.estado == estadoOrden.Entregado)
                    {
                        actualizarStockProductos();
                        actualizarPuntosCliente();
                        actualizarPuntosPatrocinador();
                        actualizarPuntosProductos();
                    }
                }
       
            }
            if (res <= 0)
            {
                MostrarMensaje(mensaje, false);
                return;
            }
 
            limpiarSesiones();
            Response.Redirect("OrdenVenta.aspx");
        }

        private void actualizarPuntosProductos()
        {
            foreach (lineaOrden linea in lineasOrden)
            {
                producto producto = linea.producto;
                producto.puntos += 1;
                apiProductos.actualizarProducto(producto);
            }
        }

        private void actualizarPuntosPatrocinador()
        {
            cliente cliente = ordenVenta.cliente;
            if(cliente.patrocinador != null)
            {
                cliente patrocinador = cliente.patrocinador;
                patrocinador.puntos += lineasOrden.Sum(lo => lo.producto.puntos) / 10;
                apiPersonas.actualizarCliente(patrocinador);
            }
        }
        private void actualizarPuntosCliente()
        {
            cliente cliente = ordenVenta.cliente;
            // puntos descontandos
            int puntosDescuento = (int)(double.Parse(TxtDescuento.Text) * 10);
            cliente.puntos -= puntosDescuento;
            if(cliente.puntos < 0)
            {
                cliente.puntos = 0;
            }
            // puntos ganados

            //int puntosAumentar = 0;
            //puntosAumentar = (int)lineasOrden.Sum(lo => lo.producto.precioUnitario) * 10;
            cliente.puntos += lineasOrden.Sum(lo => lo.producto.puntos) / 10;
          
            apiPersonas.actualizarCliente(cliente);
        }
        private void actualizarStockProductos()
        {
         
            foreach (lineaOrden linea in lineasOrden)
            {
                producto producto = linea.producto;
                producto.stock -= linea.cantidad;
                apiProductos.actualizarProducto(producto);
            }
        }

        private void limpiarSesiones()
        {
            Session["lineasDeOrden"] = null;
            Session["empleadoSeleccionado"] = null;
            Session["clienteSeleccionado"] = null;
            Session["productoSeleccionado"] = null;
            Session["lineasOrdenAntiguas"] = null;
        }
        private void actualizarLineasOrden()
        {
            // eliminamos las lineas de orden antiguas
            BindingList<lineaOrden> lineasOrdenAntiguas = (BindingList<lineaOrden>)Session["lineasOrdenAntiguas"];
            foreach (lineaOrden linea in lineasOrdenAntiguas)
            {
                apiDocumentos.eliminarLOV(ordenVenta.idOrden, linea.producto.idProductoNumerico);
            }
            // actualizar las lineas de orden actuales
            foreach (lineaOrden linea in lineasOrden)
            {
                apiDocumentos.insertarLOV(linea, ordenVenta.idOrden);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdenVenta.aspx");
        }

        protected void ddlTipoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipoVenta.SelectedValue.Equals("Delivery"))
            {
                panelRepartidor.Visible = true;
                ddlEstado.SelectedValue = "Pendiente";
            }
            else
            {
                ddlEstado.SelectedValue = "Entregado";

                panelRepartidor.Visible = false;
                Session["empleadoSeleccionado"] = null;
                TxtIDRepartidor.Text = "";
                TxtNombreCompletoRepartidor.Text = "";
            }
        }
        protected void gvLineasOrdenVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                lineaOrden linea = (lineaOrden)e.Row.DataItem;
     
                e.Row.Cells[0].Text = linea.producto.idProductoCadena;
                e.Row.Cells[1].Text = linea.producto.nombre;
                e.Row.Cells[2].Text = linea.cantidad.ToString();
                e.Row.Cells[3].Text = linea.producto.precioUnitario.ToString("N2");
                e.Row.Cells[4].Text = linea.subtotal.ToString("N2");
                Button btnEliminar = (Button)e.Row.FindControl("BtnEliminar");
                // quiero ocultar todo acciones
                if(accion.Equals("visualizar"))
                {
                    btnEliminar.Enabled = false;
                    btnEliminar.Visible = false;
                    gvLineasOrdenVenta.Columns[5].Visible = false;
                    
                }
                else
                {
                    btnEliminar.CommandArgument = linea.producto.idProductoNumerico.ToString();
                }
            }
        }

        protected void gvLineasOrdenVenta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLineasOrdenVenta.PageIndex = e.NewPageIndex;
            gvLineasOrdenVenta.DataBind();
        }

        protected void BtnCancelarGenerarComprobante_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdenVenta.aspx");
        }

    }
}