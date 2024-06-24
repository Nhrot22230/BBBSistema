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
    public partial class OrdenVenta : System.Web.UI.Page
    {

        // apis
        private DocumentosAPIClient documentosAPIClient;

        // listas
        private BindingList<ordenVenta> Blordenes;
        private BindingList<ordenVenta> BlordenesFiltradas;
        private BindingList<comprobante> comprobantes;

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Title = "Ordenes de Venta";
            documentosAPIClient = new DocumentosAPIClient();
            CargarTabla("");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void AplicarFiltro()
        {
            BlordenesFiltradas = Blordenes;
            string fechaIni = FechaInicio.Text;
            string fechaFin = FechaFin.Text;
            string min = TxtMontoMin.Text;
            string max = TxtMontoMax.Text;
            if (BlordenesFiltradas == null || Blordenes == null)
            {
                return;
            }

            if (TxtBuscar.Text.ToString() != "")
            {
                CargarTabla(TxtBuscar.Text.ToString());
            }
            if (fechaIni != "")
            {
                DateTime dateIni = Convert.ToDateTime(fechaIni);
                BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.Where(x => x.fechaCreacion.Date >= dateIni).ToList());
            }
            if (fechaFin != "")
            {
                DateTime dateFin = Convert.ToDateTime(fechaFin);
                BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.Where(x => x.fechaCreacion.Date <= dateFin).ToList());
            }
            if (Estado.SelectedValue != "Todos")
            {
                BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.Where(x => x.estado.ToString() == Estado.SelectedValue).ToList());

            }
            if (min != "")
            {
                double montomin = double.Parse(min);
                BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.Where(x => x.total >= montomin).ToList());
            }
            if (max != "")
            {
                double montomax = double.Parse(max);
                BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.Where(x => x.total <= montomax).ToList());
            }
            ordenarFechaMonto();
            GridVentas.PageIndex = 0;
            GridBind();
        }

        private void ordenarFechaMonto()
        {
            // Verificar si se ha seleccionado algún tipo de ordenamiento por fecha o por monto
            bool ordenarPorFecha = OrdenarPorFecha.SelectedValue != "todos";
            bool ordenarPorMonto = OrdenarPorMonto.SelectedValue != "todos";


            // Aplicar ordenamiento
            if (ordenarPorFecha && ordenarPorMonto)
            {
                // Ordenar por fecha y monto simultáneamente
                if (OrdenarPorFecha.SelectedValue == "asc" && OrdenarPorMonto.SelectedValue == "asc")
                {
                    BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderBy(x => x.fechaCreacion.Date)
                                                                                            .ThenBy(x => x.total)
                                                                                            .ToList());
                }
                else if (OrdenarPorFecha.SelectedValue == "asc" && OrdenarPorMonto.SelectedValue == "desc")
                {
                    BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderBy(x => x.fechaCreacion.Date)
                                                                                            .ThenByDescending(x => x.total)
                                                                                            .ToList());
                }
                else if (OrdenarPorFecha.SelectedValue == "desc" && OrdenarPorMonto.SelectedValue == "asc")
                {
                    BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderByDescending(x => x.fechaCreacion.Date)
                                                                                            .ThenBy(x => x.total)
                                                                                            .ToList());
                }
                else if (OrdenarPorFecha.SelectedValue == "desc" && OrdenarPorMonto.SelectedValue == "desc")
                {
                    BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderByDescending(x => x.fechaCreacion.Date)
                                                                                            .ThenByDescending(x => x.total)
                                                                                            .ToList());
                }
            }

            else
            {
                if (ordenarPorFecha)
                {
                    // Ordenar solo por fecha
                    if (OrdenarPorFecha.SelectedValue == "asc")
                    {
                        BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderBy(x => x.fechaCreacion.Date).ToList());
                    }
                    else if (OrdenarPorFecha.SelectedValue == "desc")
                    {
                        BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderByDescending(x => x.fechaCreacion.Date).ToList());
                    }
                }
                else if (ordenarPorMonto)
                {
                    // Ordenar solo por monto
                    if (OrdenarPorMonto.SelectedValue == "asc")
                    {
                        BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderBy(x => x.total).ToList());
                    }
                    else if (OrdenarPorMonto.SelectedValue == "desc")
                    {
                        BlordenesFiltradas = new BindingList<ordenVenta>(BlordenesFiltradas.OrderByDescending(x => x.total).ToList());
                    }
                }
            }
        }

        private void GridBind()
        {
            GridVentas.DataSource = BlordenesFiltradas;
            GridVentas.DataBind();
        }

        private bool CargarTabla(string search)
        {
            ordenVenta[] lista = documentosAPIClient.listarOrdenVenta(search);
            if(lista != null)
            {
                lista = lista.Where(x => x.estado != estadoOrden.Cancelado).ToArray();
                Blordenes = new BindingList<ordenVenta>(lista.ToList());
            }
            else
            {
                Blordenes = new BindingList<ordenVenta>();
            }
            BlordenesFiltradas = Blordenes;
            GridBind();
            return true;
        }

        private void listarComprobatentes()
        {
            comprobante[] listaAux = documentosAPIClient.listarComprobante("");
            if (listaAux == null)
            {
                return;
            }
            comprobantes = new BindingList<comprobante>(listaAux.ToList());
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
        protected bool verificarFechas()
        {
            string fechaIni = FechaInicio.Text;
            string fechaFin = FechaFin.Text;

            if (fechaIni != "" && fechaFin != "")
            {
                DateTime dateIni = Convert.ToDateTime(fechaIni);
                DateTime dateFin = Convert.ToDateTime(fechaFin);
                if (dateIni > dateFin)
                {
                    return false;
                }
            }
            return true;
        }

        protected void FechaInicio_TextChanged(object sender, EventArgs e)
        {
            if (!verificarFechas())
            {
                MostrarMensaje("Ingrese un rango de fechas correcto", verificarFechas());
                return;
            }
            GridVentas.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void FechaFin_TextChanged(object sender, EventArgs e)
        {
            if (!verificarFechas())
            {
                MostrarMensaje("Ingrese un rango de fechas correcto", verificarFechas());
                return;
            }
            GridVentas.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void GridVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridVentas.PageIndex = e.NewPageIndex;
            GridBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {

            Response.Redirect("OrdenVentaForm.aspx?accion=new");
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            int idOrdenVenta = Int32.Parse(((LinkButton)sender).CommandArgument);
            Session["ordenSeleccionada"] = Blordenes.Where(x => x.idOrdenVentaNumerico == idOrdenVenta).FirstOrDefault();
            Response.Redirect("OrdenVentaForm.aspx?accion=visualizar");
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            bool flag = CargarTabla(TxtBuscar.Text);
            if (flag)
            {
                MostrarMensaje($"Se encontraron {Blordenes.Count} ordenes de venta", flag);
            }
            else
            {
                MostrarMensaje("No se encontraron ordenes de venta", flag);
            }
        }

        protected bool verificarMontos()
        {
            string min = TxtMontoMin.Text;
            string max = TxtMontoMax.Text;
            if (min != "")
            {
                if (double.Parse(min) < 0)
                    return false;
            }
            if (max != "")
            {
                if (double.Parse(max) < 0)
                    return false;
            }
            if (min != "" && max != "")
            {
                double montomin = double.Parse(min);
                double montomax = double.Parse(max);
                if (montomin > montomax)
                {
                    return false;
                }
            }
            return true;
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {   //
            // Lógica para limpiar todos los filtros
            TxtBuscar.Text = string.Empty;
            FechaInicio.Text = string.Empty;
            FechaFin.Text = string.Empty;
            Estado.SelectedIndex = 0;
            TxtMontoMax.Text = string.Empty;
            TxtMontoMin.Text = string.Empty;
            OrdenarPorFecha.SelectedIndex = 0;
            OrdenarPorMonto.SelectedIndex = 0;
            // Selecciona "Todos"
            GridVentas.PageIndex = 0;
            GridVentas.DataSource = Blordenes;
            GridBind();
        }

        protected void OrdenarPorFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            MostrarMensaje("Ordenes ordenadas por fecha", true);
        }

        protected void Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void TxtMontoMin_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            AplicarFiltro();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void TxtMontoMax_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            AplicarFiltro();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void OrdenarPorMonto_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            MostrarMensaje("Ordenes ordenadas por monto", true);
        }

        protected void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            MostrarMensaje("Se aplico el filtro", true);
        }

        protected void GridVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                ordenVenta orden = (ordenVenta)e.Row.DataItem;
                e.Row.Cells[0].Text = orden.idOrdenVentaCadena;
                e.Row.Cells[1].Text = orden.fechaCreacion.ToString("dd/MM/yyyy");
                e.Row.Cells[2].Text = orden.cliente.nombre + " " + orden.cliente.apellidoPaterno;
                e.Row.Cells[3].Text = orden.encargadoVenta.nombre + " " + orden.encargadoVenta.apellidoPaterno;
                if(orden.repartidor != null)
                {
                    e.Row.Cells[4].Text = orden.repartidor.nombre + " " +orden.repartidor.apellidoPaterno;
                }
        
                if(orden.fechaEntrega.ToString("MM/yyyy") == "01/0001") { 
                    e.Row.Cells[5].Text = "No asignado";
                }
                else
                {
                    e.Row.Cells[5].Text = orden.fechaEntrega.ToString("dd/MM/yyyy");
                }   
                e.Row.Cells[6].Text = "S/ " + orden.total.ToString("N2");
                e.Row.Cells[7].Text = orden.estado.ToString();
                e.Row.Cells[8].Text = "S/ " + orden.porcentajeDescuento.ToString();
               
                LinkButton btnVisualizar = (LinkButton)e.Row.FindControl("BtnVisualizar");
                btnVisualizar.CommandArgument = orden.idOrdenVentaNumerico.ToString();

                LinkButton btnEditar = (LinkButton)e.Row.FindControl("BtnEditar");
                btnEditar.CommandArgument = orden.idOrdenVentaNumerico.ToString();

                
                LinkButton btnEliminar = (LinkButton)e.Row.FindControl("BtnEliminar");
                btnEliminar.CommandArgument = orden.idOrdenVentaNumerico.ToString();
                
                if(orden.estado == estadoOrden.Entregado || orden.estado == estadoOrden.Cancelado)
                {
                    btnEliminar.Visible = false;
                    btnEditar.Visible = false;
                }


            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            int idOrdenVenta = Int32.Parse(((LinkButton)sender).CommandArgument);
            Session["ordenSeleccionada"] = Blordenes.Where(x => x.idOrdenVentaNumerico == idOrdenVenta).FirstOrDefault();
            Response.Redirect("OrdenVentaForm.aspx?accion=editar");
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            int idOrdenVenta = Int32.Parse(((LinkButton)sender).CommandArgument);
            ordenVenta orden = Blordenes.Where(x => x.idOrdenVentaNumerico == idOrdenVenta).FirstOrDefault();
            orden.estadoSpecified = true;
            orden.fechaEntregaSpecified = true;
            bool flag = comprobarComprobanteAsociado(orden);
            if (!flag) return;
            
            orden.estado = estadoOrden.Cancelado;
            int res = documentosAPIClient.actualizarOrdenVenta(orden);
            if (res > 0)
            {
                MostrarMensaje("Orden de venta eliminada", true);
            }
            else
            {
                MostrarMensaje("No se pudo eliminar la orden de venta", false);
            }
            CargarTabla("");
            
        }

        private bool comprobarComprobanteAsociado(ordenVenta orden)
        {
            listarComprobatentes();
            foreach (comprobante comp in comprobantes)
            {
                if (comp.ordenAsociada.idOrden == orden.idOrden)
                {
                    MostrarMensaje("No se puede eliminar la orden de venta porque tiene un comprobante asociado", false);
                    return false;
                }
            }
            return true;
        }
    }
}