using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxnSisventas.BBBWebService;

namespace DxnSisventas.Views
{
    public partial class ComprobantesForm : System.Web.UI.Page
    {
        public string opcion = "";
        //
        private DocumentosAPIClient apiDocumentos;

        private BindingList<orden> listaOrdenes = null;
        private BindingList<ordenCompra> listaOrdenesCompra = null;
        private BindingList<ordenVenta> listaOrdenesVenta = null;
        private orden ord;
        private comprobante comprobante=null;

        bool tipoOrdenCompra;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empleado"] == null)
                Response.Redirect("/Login.aspx");
        }

        protected void Page_Init()
        {
            apiDocumentos = new DocumentosAPIClient();
            tipoOrdenCompra = true;
            CargarTabla("");
        }
        protected void CargarDatos()
        {
            if (comprobante != null)
            {
                TxtId.Text = comprobante.idComprobanteCadena;
                TxtFechaComprobante.Text = comprobante.fechaEmision.ToString("dd/MM/yyyy");
                //DropDownListTipoComprobante.SelectedValue = comprobante.tipoComprobante.ToString();
                if (comprobante.ordenAsociada != null)
                {
                    TxtIdOrden.Text = comprobante.ordenAsociada.idOrden.ToString();
                    TxtFechaOrden.Text = comprobante.ordenAsociada.fechaCreacion.ToString("dd/MM/yyyy");
                }
            }
        }

        protected void DropDownListTipoOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListTipoOrden.SelectedValue.Equals("Compra"))
            {
                tipoOrdenCompra = true;
            }
            else
            {
                tipoOrdenCompra = false;
            }
            CargarTabla(txtCodOrdenModal.Text);
            CargarModalOrdenes();
            ReestableceInfoOrdenSeleccionada();
        }

        protected void DropDownListTipoOrdenModal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListTipoOrdenModal.SelectedValue.Equals("Compra"))
            {
                tipoOrdenCompra = true;
            }
            else
            {
                tipoOrdenCompra= false; 
            }
            CargarTabla(txtCodOrdenModal.Text);
            CargarModalOrdenes();
            ReestableceInfoOrdenSeleccionada();
        }

        private void ReestableceInfoOrdenSeleccionada()
        {
            if(tipoOrdenCompra) {
                DropDownListTipoOrden.SelectedValue = "Compra";
                DropDownListTipoOrdenModal.SelectedValue = "Compra";
            }
            else
            {
                DropDownListTipoOrden.SelectedValue = "Venta";
                DropDownListTipoOrdenModal.SelectedValue = "Venta";
            }
            TxtIdOrden.Text = "";
            TxtFechaOrden.Text = "";
            Session["OrdenSeleccionada"] = null;
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Comprobantes.aspx");
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            comprobante comprobante = new comprobante();
            comprobante.ordenAsociada = (orden)Session["OrdenSeleccionada"];
            comprobante.fechaEmisionSpecified = true;
            comprobante.fechaEmision = DateTime.Parse(TxtFechaComprobante.Text);
            comprobante.tipoComprobanteSpecified = true;
            comprobante.tipoComprobante = (tipoComprobante)Enum.Parse(typeof(tipoComprobante), DropDownListTipoComprobante.SelectedValue);
            int res = apiDocumentos.insertarComprobante(comprobante);
            string mensaje = res > 0 ? "Comprobante guardado correctamente" : "Error al guardar el comprobante";
            MostrarMensaje(mensaje, res > 0);
            if (res > 0) Response.Redirect("/Views/Comprobantes.aspx");
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarModalOrdenes();
        }

        protected void BtnSeleccionarOrdenModal_Click(object sender, EventArgs e)
        {
            int idOrden = Int32.Parse(((LinkButton)sender).CommandArgument);
            orden[] lista = apiDocumentos.listarOrden(txtCodOrdenModal.Text);
            orden ordenSeleccionada = lista.SingleOrDefault(x => x.idOrden == idOrden);
            Session["OrdenSeleccionada"] = ordenSeleccionada;
            if (ordenSeleccionada is ordenVenta)
            {
                TxtIdOrden.Text = ((ordenVenta)ordenSeleccionada).idOrdenVentaCadena;
            }
            else
            {
                TxtIdOrden.Text = ((ordenCompra)ordenSeleccionada).idOrdenCompraCadena;
            }
            TxtFechaOrden.Text = ordenSeleccionada.fechaCreacion.ToString("dd/MM/yyyy");
            TxtTotal.Text = ordenSeleccionada.total.ToString("N2");
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

        protected void BtnBuscarModal_Click(object sender, EventArgs e)
        {
            bool flag = CargarTabla(txtCodOrdenModal.Text);
            if (flag)
            {
                MostrarMensaje($"Se encontraron {listaOrdenes.Count} ordenes", flag);
            }
            else
            {
                MostrarMensaje("No se encontraron ordenes", flag);
            }
            CargarModalOrdenes();
        }

        private void CargarModalOrdenes()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showModalFormOrdenes", "showModalFormOrdenes();", true);
        }



        protected void gvOrdenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrdenes.PageIndex = e.NewPageIndex;
            GridBindOrdenes();
            CargarModalOrdenes();
        }
        private void GridBindOrdenes()
        {
            gvOrdenes.DataSource = listaOrdenes;
            gvOrdenes.DataBind();
        }

        protected void txtCodOrdenModal_TextChanged(object sender, EventArgs e)
        {
            CargarTabla(txtCodOrdenModal.Text);
        }
        private bool CargarTabla(string search)
        {
            orden[] lista = null;
            if (tipoOrdenCompra)
            {
                lista = apiDocumentos.listarOrdenCompra(search);
            }
            else
            {
                lista = apiDocumentos.listarOrdenVenta(search);
            }
            
            if (lista == null)
            {
                return false;
            }
            listaOrdenes = new BindingList<orden>(lista.ToList());
            GridBindOrdenes();
            return true;
        }


        protected void gvOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem is ordenCompra)
                {
                    ordenCompra ordenCompra = (ordenCompra)e.Row.DataItem;
                    e.Row.Cells[1].Text = DataBinder.Eval(ordenCompra, "idOrdenCompraCadena").ToString();
                }
                else
                {
                    ordenVenta ordenVenta = (ordenVenta)e.Row.DataItem;
                    e.Row.Cells[1].Text = DataBinder.Eval(ordenVenta, "idOrdenVentaCadena").ToString();
                }

                e.Row.Cells[2].Text = ((DateTime)DataBinder.Eval(e.Row.DataItem, "fechaCreacion")).ToString("dd/MM/yyyy");
                e.Row.Cells[3].Text = ((DateTime)DataBinder.Eval(e.Row.DataItem, "fechaCreacion")).ToString("HH:mm:ss");
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
    }
}