using DxnSisventas.BBBWebService;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
    public partial class Comprobantes : System.Web.UI.Page
    {
        private DocumentosAPIClient documentosAPIClient;
        private ReportesAPIClient reportesAPIClient;
        private BindingList<comprobante> BlComprobantes;
        private BindingList<comprobante> BlComprobantesFiltrado;

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Title = "Comprobantes";
            documentosAPIClient = new DocumentosAPIClient();
            reportesAPIClient = new ReportesAPIClient(); 
            CargarTabla("");
            //AplicarFiltro();
            //GridBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            bool flag = CargarTabla(TxtBuscar.Text);
            if (flag)
            {
                MostrarMensaje($"Se encontraron {BlComprobantes.Count} comprobantes", flag);
            }
            else
            {
                MostrarMensaje("No se encontraron comprobantes", flag);
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/ComprobantesForm.aspx");
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            int idComprobante = int.Parse(((LinkButton)sender).CommandArgument);
            Session["IdComprobante"] = idComprobante;
            comprobante comp = BlComprobantes.FirstOrDefault(c => c.idComprobanteNumerico == idComprobante);

            bool flag = comp.ordenAsociada is ordenVenta;

            if (flag)
            {
                Response.Redirect("/Views/ComprobantesForm.aspx?accion=update");
                MostrarMensaje("La orden asociada es de venta", true);
            }
            else
            {
                MostrarMensaje("La orden asociada es de compra", false);
            }
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            int idComprobante = int.Parse(((LinkButton)sender).CommandArgument);
            Session["idComprobanteSeleccionado"] = idComprobante;
            comprobante comp = BlComprobantes.FirstOrDefault(c => c.idComprobanteNumerico == idComprobante);

            if (comp != null)
            {
                Response.Redirect("ComprobantesForm.aspx?accion=ver");
            }
            else
            {
                MostrarMensaje("No se encontro el comprobante", false);
            }
        }

        protected void BtnImprimir_Click(object sender, EventArgs e)
        {
            int idComprobante = int.Parse(((LinkButton)sender).CommandArgument);
            Byte[] FileBuffer = reportesAPIClient.imprimirComprobante(idComprobante);
            if (FileBuffer!=null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-lenght", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            int idComprobante = int.Parse(((LinkButton)sender).CommandArgument);
            int res = documentosAPIClient.eliminarComprobante(idComprobante);
            if (res == 1)
            {
                MostrarMensaje("Comprobante eliminado", true);
            }
            else
            {
                MostrarMensaje("No se pudo eliminar el comprobante", false);
            }
            CargarTabla("");
        }

        protected void GridComprobantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridComprobantes.PageIndex = e.NewPageIndex;
            AplicarFiltro();
            GridBind();
        }

        private void GridBind()
        {
            GridComprobantes.DataSource = BlComprobantesFiltrado;
            GridComprobantes.DataBind();
        }

        private bool CargarTabla(string search)
        {
            comprobante[] lista = documentosAPIClient.listarComprobante(search);
            if (lista == null) return false;

            BlComprobantes = new BindingList<comprobante>(lista.ToList());
            AplicarFiltro();
            GridBind();
            return true;
        }


        protected void DropDownListTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            GridBind();
        }

        private void AplicarFiltro()
        {
            if (DropDownListTipoComprobante.SelectedValue == "Todos" && DropDownListTipoOrdenAsociada.SelectedValue == "Todos")
            {
                BlComprobantesFiltrado = BlComprobantes;
                return;
            }

            if(!(DropDownListTipoComprobante.SelectedValue == "Todos"))
            {
                tipoComprobante tipoSelected = (tipoComprobante)Enum.Parse(typeof(tipoComprobante), DropDownListTipoComprobante.SelectedValue);
                BlComprobantesFiltrado = new BindingList<comprobante>(BlComprobantes.Where(e => e.tipoComprobante == tipoSelected).ToList());
            }

            if(!(DropDownListTipoOrdenAsociada.SelectedValue == "Todos"))
            {
                if (DropDownListTipoOrdenAsociada.SelectedValue.Equals("Compra"))
                {
                    BlComprobantesFiltrado = new BindingList<comprobante>(BlComprobantesFiltrado.Where(e => e.ordenAsociada is ordenCompra).ToList());
                }
                else
                {
                    BlComprobantesFiltrado = new BindingList<comprobante>(BlComprobantesFiltrado.Where(e => e.ordenAsociada is ordenVenta).ToList());
                }
            }
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

        protected void GridComprobantes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el objeto de datos actual
                comprobante comprobante = (comprobante)e.Row.DataItem;

                // Buscar el Label en el TemplateField
                Label lblOrdenVentaCompra = (Label)e.Row.FindControl("LblOrdenVentaCompra");
                e.Row.Cells[1].Text = ((DateTime)DataBinder.Eval(e.Row.DataItem, "fechaEmision")).ToString("dd/MM/yyyy");
                if(((tipoComprobante)DataBinder.Eval(e.Row.DataItem, "tipoComprobante"))==tipoComprobante.Factura)
                {
                    e.Row.Cells[2].Text = "Factura";
                }
                else
                {
                    e.Row.Cells[2].Text = "Boleta";
                }
               
                if (comprobante.ordenAsociada is ordenVenta venta)
                {
                    lblOrdenVentaCompra.Text = venta.idOrdenVentaCadena;
                }
                else if (comprobante.ordenAsociada is ordenCompra compra)
                {
                    lblOrdenVentaCompra.Text = compra.idOrdenCompraCadena;
                }
                else
                {
                    lblOrdenVentaCompra.Text = "N/A";
                }
                e.Row.Cells[4].Text = "S/. " + ((Double)DataBinder.Eval(e.Row.DataItem, "ordenAsociada.total")).ToString("N2");
            }
        }

        protected void DropDownListTipoOrdenAsociada_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            GridBind();
        }

        protected void DropDownListOrdenamientoComprobante_SelectedIndexChanged(object sender,  EventArgs e)
        {
            AplicarFiltro();
            if (DropDownListOrdenamientoComprobante.SelectedValue.Equals("IDAsc"))
            {
                BlComprobantesFiltrado = new BindingList<comprobante>(BlComprobantesFiltrado.OrderBy(x => x.idComprobanteNumerico).ToList());
            }
            else if (DropDownListOrdenamientoComprobante.SelectedValue.Equals("IDDesc"))
            {
                BlComprobantesFiltrado = new BindingList<comprobante>(BlComprobantesFiltrado.OrderByDescending(x => x.idComprobanteNumerico).ToList());
            }
            else if(DropDownListOrdenamientoComprobante.SelectedValue.Equals("TotalAsc"))
            {
                BlComprobantesFiltrado = new BindingList<comprobante>(BlComprobantesFiltrado.OrderBy(x => x.ordenAsociada.total).ToList());
            }
            else
            {
                BlComprobantesFiltrado = new BindingList<comprobante>(BlComprobantesFiltrado.OrderByDescending(x => x.ordenAsociada.total).ToList());
            }
            GridBind();
        }
    }
}