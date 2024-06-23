using DxnSisventas.BBBWebService;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
    public partial class OrdenCompra : System.Web.UI.Page
    {
        private DocumentosAPIClient documentosAPIClient;
        private BindingList<ordenCompra> Blordenes;
        private BindingList<ordenCompra> BlordenesFiltradas;
        private BindingList<comprobante> comprobantes;

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Title = "Ordenes de Compra";
            documentosAPIClient = new DocumentosAPIClient();
            Session["idOrdenVenta"] = null;
            Session["ordenCompra"] = null;
            Session["lineasOrdenVenta"] = null;
            if (Session["correo"] != null) { 
            if (Session["correo"].ToString() == "true")
            {
                MostrarMensaje("Correo Enviado", true);
                Session["correo"] = null;
            }
            }
            CargarTabla("");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnBuscar_Click(object sender, EventArgs e)

        {
            if (TxtBuscar.Text.ToString() != "")
            {
                if (int.Parse(TxtBuscar.Text) <= 0)
                {
                    MostrarMensaje("Ingrese un Nro de orden correcto", false);
                    return;
                }
            }

            bool flag = CargarTabla(TxtBuscar.Text);
            AplicarFiltro();
            if (flag)
            {
                MostrarMensaje($"Se encontraron {BlordenesFiltradas.Count} ordenes de compra", flag);
            }
            else
            {
                MostrarMensaje("No se encontraron ordenes de compra", flag);
            }

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
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.estado.ToString() != estadoOrden.Cancelado.ToString()).ToList());
            if (!verificarFechas())
            {
                MostrarMensaje("Ingrese un rango de fechas correcto", verificarFechas());
                return;
            }
            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            if (TxtBuscar.Text.ToString() != "")
            {
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.idOrdenCompraNumerico == (int.Parse(TxtBuscar.Text))).ToList());
            }
            if (fechaIni != "")
            {
                DateTime dateIni = Convert.ToDateTime(fechaIni);
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.fechaCreacion.Date >= dateIni).ToList());
            }
            if (fechaFin != "")
            {
                DateTime dateFin = Convert.ToDateTime(fechaFin);
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.fechaCreacion.Date <= dateFin).ToList());
            }
            if (Estado.SelectedValue != "Todos")
            {
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.estado.ToString() == Estado.SelectedValue).ToList());

            }

            if (min != "")
            {
                double montomin = double.Parse(min);
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.total >= montomin).ToList());
            }
            if (max != "")
            {
                double montomax = double.Parse(max);
                BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.Where(x => x.total <= montomax).ToList());
            }
            ordenarFechaMonto();
            MostrarMensaje("Se aplico el filtro", true);

        }

        private void ordenarFechaMonto()
        {
            // Verificar si se ha seleccionado algún tipo de ordenamiento por fecha o por monto
            bool ordenarPorFecha = OrdenarPorFecha.SelectedValue != "todos";
            bool ordenarPorMonto = OrdenarPorMonto.SelectedValue != "todos";
            if (!verificarFechas())
            {
                MostrarMensaje("Ingrese un rango de fechas correcto", verificarFechas());
                return;
            }
            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            // Aplicar ordenamiento
            if (ordenarPorFecha && ordenarPorMonto)
            {
                // Ordenar por fecha y monto simultáneamente
                if (OrdenarPorFecha.SelectedValue == "asc" && OrdenarPorMonto.SelectedValue == "asc")
                {
                    BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderBy(x => x.fechaCreacion.Date)
                                                                                            .ThenBy(x => x.total)
                                                                                            .ToList());
                }
                else if (OrdenarPorFecha.SelectedValue == "asc" && OrdenarPorMonto.SelectedValue == "desc")
                {
                    BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderBy(x => x.fechaCreacion.Date)
                                                                                            .ThenByDescending(x => x.total)
                                                                                            .ToList());
                }
                else if (OrdenarPorFecha.SelectedValue == "desc" && OrdenarPorMonto.SelectedValue == "asc")
                {
                    BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderByDescending(x => x.fechaCreacion.Date)
                                                                                            .ThenBy(x => x.total)
                                                                                            .ToList());
                }
                else if (OrdenarPorFecha.SelectedValue == "desc" && OrdenarPorMonto.SelectedValue == "desc")
                {
                    BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderByDescending(x => x.fechaCreacion.Date)
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
                        BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderBy(x => x.fechaCreacion.Date).ToList());
                    }
                    else if (OrdenarPorFecha.SelectedValue == "desc")
                    {
                        BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderByDescending(x => x.fechaCreacion.Date).ToList());
                    }
                    MostrarMensaje("Ordenes ordenadas por fecha", true);
                }
                else if (ordenarPorMonto)
                {
                    // Ordenar solo por monto
                    if (OrdenarPorMonto.SelectedValue == "asc")
                    {
                        BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderBy(x => x.total).ToList());
                    }
                    else if (OrdenarPorMonto.SelectedValue == "desc")
                    {
                        BlordenesFiltradas = new BindingList<ordenCompra>(BlordenesFiltradas.OrderByDescending(x => x.total).ToList());
                    }
                    MostrarMensaje("Ordenes ordenada por monto", true);

                }
            }
            
        }

        private void GridBind()
        {
            GridCompras.DataSource = BlordenesFiltradas;
            GridCompras.DataBind();
        }

        private bool CargarTabla(string search)
        {
            ordenCompra[] lista = documentosAPIClient.listarOrdenCompra(search);
            if (lista == null)
            {
                return false;
            }
            Blordenes = new BindingList<ordenCompra>(lista.ToList());
            BlordenesFiltradas = Blordenes;
            AplicarFiltro();
            GridBind();
            return true;
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
            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            
        }

        protected void FechaFin_TextChanged(object sender, EventArgs e)
        {
            if (!verificarFechas())
            {
                MostrarMensaje("Ingrese un rango de fechas correcto", verificarFechas());
                return;
            }
            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            
        }

        protected void GridCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCompras.PageIndex = e.NewPageIndex;
            AplicarFiltro();
            GridBind();
        }

        protected void GridCompras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/OrdenCompraForm.aspx?accion=new");
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            int idOrdenVenta = int.Parse(((LinkButton)sender).CommandArgument);
            Session["idOrdenVenta"] = idOrdenVenta;
            Response.Redirect("OrdenCompraForm.aspx?accion=ver");
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            int idOrdenVenta = int.Parse(((LinkButton)sender).CommandArgument);
            ordenCompra orden = Blordenes.Where(x => x.idOrdenCompraNumerico == idOrdenVenta).FirstOrDefault();
            orden.estadoSpecified = true;
            orden.fechaCreacionSpecified = true;
            orden.fechaRecepcionSpecified = true;
            orden.estado = estadoOrden.Cancelado;
            bool flag = comprobarComprobanteAsociado(orden);
            if (!flag) return;
            documentosAPIClient.actualizarOrdenCompra(orden);
            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            MostrarMensaje("Orden de Compra eliminada",true);
        }

        protected void Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            GridCompras.PageIndex = 0;
            GridBind();
            

        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {   
            TxtBuscar.Text = string.Empty;
            FechaInicio.Text = string.Empty;
            FechaFin.Text = string.Empty;
            Estado.SelectedIndex = 0;
            TxtMontoMax.Text = string.Empty;
            TxtMontoMin.Text = string.Empty;
            OrdenarPorFecha.SelectedIndex = 0;
            OrdenarPorMonto.SelectedIndex = 0;
            // Selecciona "Todos"
            GridCompras.PageIndex = 0;
            GridCompras.DataSource = Blordenes;
            GridBind();
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
        protected void TxtMontoMin_TextChanged(object sender, EventArgs e)
        {
            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            
        }

        protected void TxtMontoMax_TextChanged(object sender, EventArgs e)
        {

            if (!verificarMontos())
            {
                MostrarMensaje("Ingrese un rango de montos correcto", verificarMontos());
                return;
            }
            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            
        }

        protected void TxtBuscar_TextChanged(object sender, EventArgs e)
        {

            if (TxtBuscar.Text.ToString() != "")
            {
                if (int.Parse(TxtBuscar.Text) <= 0)
                {
                    MostrarMensaje("Ingrese un Nro de orden correcto", false);
                    return;
                }
            }

            GridCompras.PageIndex = 0;
            AplicarFiltro();
            GridBind();
            
        }

        protected void OrdenarPorFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            GridCompras.PageIndex = 0;
            GridBind();
            
        }

        protected void OrdenarPorMonto_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            GridCompras.PageIndex = 0;
            GridBind();
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
        private bool comprobarComprobanteAsociado(ordenCompra orden)
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


/*(I'm only human)
(I'm only, I'm only)
(I'm only human, human)

Maybe I'm foolish, maybe I'm blind
Thinking I can see through this and see what's behind
Got no way to prove it, so maybe I'm lying

But I'm only human after all
I'm only human after all
Don't put your blame on me
Don't put your blame on me

Take a look in the mirror and what do you see?
Do you see it clearer or are you deceived
In what you believe?

'Cause I'm only human after all
You're only human after all
Don't put the blame on me
Don't put your blame on me

(Oh-oh) some people got the real problems
(Oh-oh) some people out of luck
(Oh-oh) some people think I can solve them
(Oh-oh) Lord heavens above

I'm only human after all
I'm only human after all
Don't put the blame on me
Don't put the blame on me

Don't ask my opinion, don't ask me to lie
Then beg for forgiveness for making you cry
Making you cry

'Cause I'm only human after all
I'm only human after all
Don't put your blame on me
Don't put the blame on me

(Oh-oh) oh, some people got the real problems
(Oh-oh) some people out of luck
(Oh-oh) some people think I can solve them
(Oh-oh) Lord heavens above

I'm only human after all
I'm only human after all
Don't put the blame on me
Don't put the blame on me

I'm only human, I make mistakes
I'm only human, that's all it takes
To put the blame on me
Don't put the blame on me

(Ooh) 'cause I'm no prophet or Messiah
(Ooh) you should go looking somewhere higher

I'm only human after all
I'm only human after all
Don't put the blame on me
Don't put the blame on me

I'm only human, I do what I can
I'm just a man, I do what I can
Don't put the blame on me
Don't put your blame on me*/