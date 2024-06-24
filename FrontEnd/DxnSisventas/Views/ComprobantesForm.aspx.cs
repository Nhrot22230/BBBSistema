using System;
using System.Collections;
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
        private BindingList<orden> listaOrdenesFiltrado;
        private CorreosAPIClient apiCorreo;
        private ReportesAPIClient apiReportes;
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
            apiCorreo = new CorreosAPIClient();
            apiReportes = new ReportesAPIClient();
            if(!IsPostBack)
                tipoOrdenCompra = true;
            CargarTabla("");
            String accion = Request.QueryString["accion"];
            if (accion != null && accion == "ver" && Session["idComprobanteSeleccionado"] != null)
            {
                CargarDatos();
                ModoVisualizar();
            }
            else
            {
                ModoAgregar();
            }
        }
        protected void ModoVisualizar()
        {
            DropDownListTipoComprobante.Enabled = false;
            DropDownListTipoOrden.Enabled = false;
            BtnBuscar.Visible = false;
            BtnBuscar.Enabled = false;
            BtnGuardar.Enabled = false;
            BtnGuardar.Visible = false;
            BtnEnviar.Visible = true;
            BtnEnviar.Enabled = true;
            BtnImprimir.Enabled = true;
            BtnImprimir.Visible = true;
        }

        protected void ModoAgregar()
        {
            DropDownListTipoComprobante.Enabled = true;
            DropDownListTipoOrden.Enabled = true;
            BtnBuscar.Visible = true;
            BtnBuscar.Enabled = true;
            BtnGuardar.Enabled = true;
            BtnGuardar.Visible = true;
            BtnEnviar.Visible = false;
            BtnEnviar.Enabled = false;
            BtnImprimir.Enabled=false;
            BtnImprimir.Visible=false;
        }

        protected void CargarDatos()
        {
            int idComprobanteSeleccionado = (int)Session["idComprobanteSeleccionado"];
            comprobante = apiDocumentos.listarComprobante("").SingleOrDefault(x => x.idComprobanteNumerico == idComprobanteSeleccionado);
            if (comprobante != null)
            {
                TxtId.Text = comprobante.idComprobanteCadena;
                TxtFechaComprobante.Text = comprobante.fechaEmision.ToString("dd/MM/yyyy");
                DropDownListTipoComprobante.SelectedValue = comprobante.tipoComprobante.ToString();
                if (comprobante.ordenAsociada != null)
                {

                    TxtFechaOrden.Text = comprobante.ordenAsociada.fechaCreacion.ToString("dd/MM/yyyy");
                    DropDownListTipoOrden.AutoPostBack = false;
                    if (comprobante.ordenAsociada is ordenCompra)
                    {
                        TxtIdOrden.Text = ((ordenCompra)comprobante.ordenAsociada).idOrdenCompraCadena;
                        DropDownListTipoOrden.SelectedValue = "Compra";
                    }
                    else
                    {
                        TxtIdOrden.Text = ((ordenVenta)comprobante.ordenAsociada).idOrdenVentaCadena;
                        DropDownListTipoOrden.SelectedValue = "Venta";
                    }
                    DropDownListTipoOrden.AutoPostBack = true;
                    TxtTotal.Text = comprobante.ordenAsociada.total.ToString("N2");
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
            comprobante.fechaEmision = DateTime.Now;
            comprobante.tipoComprobanteSpecified = true;
            comprobante.tipoComprobante = (tipoComprobante)Enum.Parse(typeof(tipoComprobante), DropDownListTipoComprobante.SelectedValue);
            comprobante.ordenAsociada.estadoSpecified = true;
            comprobante.ordenAsociada.estado = estadoOrden.Entregado;
            if (comprobante.ordenAsociada is ordenVenta)
            {
                ((ordenVenta)comprobante.ordenAsociada).fechaEntregaSpecified = true;
                ((ordenVenta)comprobante.ordenAsociada).fechaEntrega = comprobante.fechaEmision;
                apiDocumentos.actualizarOrdenVenta((ordenVenta)comprobante.ordenAsociada);
            }
            if (comprobante.ordenAsociada is ordenCompra)
            {
                ((ordenCompra)comprobante.ordenAsociada).fechaRecepcionSpecified = true;
                ((ordenCompra)comprobante.ordenAsociada).fechaRecepcion = comprobante.fechaEmision;
                apiDocumentos.actualizarOrdenCompra((ordenCompra)comprobante.ordenAsociada);
            }
            int res = apiDocumentos.insertarComprobante(comprobante);
            string mensaje = res > 0 ? "Comprobante guardado correctamente" : "Error al guardar el comprobante";
            if (res > 0) Response.Redirect("/Views/Comprobantes.aspx");
            MostrarMensaje(mensaje, res > 0);
        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showModalFormEnviar() };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnImprimir_Click(object sender, EventArgs e)
        {
            int idComprobante = comprobante.idComprobanteNumerico;
            byte[] pdfBytes = GetPdfFromWebService(idComprobante);
            
            if (pdfBytes != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-lenght", pdfBytes.Length.ToString());
                Response.BinaryWrite(pdfBytes);
            }
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
            if (ordenSeleccionada is ordenVenta)
            {
                ordenVenta ordenVentaSelec = apiDocumentos.listarOrdenVenta(txtCodOrdenModal.Text).SingleOrDefault(x => x.idOrden == idOrden);
                TxtIdOrden.Text = ordenVentaSelec.idOrdenVentaCadena;
                ordenSeleccionada = ordenVentaSelec;
            }
            else
            {
                ordenCompra ordenCompraSelec = apiDocumentos.listarOrdenCompra(txtCodOrdenModal.Text).SingleOrDefault(x => x.idOrden == idOrden);
                TxtIdOrden.Text = ordenCompraSelec.idOrdenCompraCadena;
                ordenSeleccionada = ordenCompraSelec;
            }
            Session["OrdenSeleccionada"] = ordenSeleccionada;
            TxtFechaOrden.Text = ordenSeleccionada.fechaCreacion.ToString("dd/MM/yyyy");
            TxtTotal.Text = ordenSeleccionada.total.ToString("N2");
            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('','');", true);
        }

        protected void BtnBuscarModal_Click(object sender, EventArgs e)
        {
            actualizaTipoOrdenListarModal();
            bool flag = CargarTabla(txtCodOrdenModal.Text);
            if (flag)
            {
                MostrarMensaje($"Se encontraron {listaOrdenesFiltrado.Count} ordenes", flag);
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


        private void actualizaTipoOrdenListarModal()
        {
            if (DropDownListTipoOrdenModal.SelectedValue.Equals("Compra"))
            {
                tipoOrdenCompra = true;
            }
            else
            {
                tipoOrdenCompra = false;
            }
        }
        protected void gvOrdenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrdenes.PageIndex = e.NewPageIndex;
            actualizaTipoOrdenListarModal();
            CargarTabla(txtCodOrdenModal.Text);
            CargarModalOrdenes();
        }

        private void AplicarFiltro()
        {
            if (tipoOrdenCompra)
            {
                listaOrdenesFiltrado = new BindingList<orden>(apiDocumentos.listarOrdenCompra(txtCodOrdenModal.Text));
            }
            else
            {
                listaOrdenesFiltrado = new BindingList<orden>(apiDocumentos.listarOrdenVenta(txtCodOrdenModal.Text));
            }
            listaOrdenesFiltrado = new BindingList<orden>(listaOrdenesFiltrado.Where(x => x.estado.ToString() != estadoOrden.Cancelado.ToString()).ToList());
        }
        private void GridBindOrdenes()
        {
            gvOrdenes.DataSource = listaOrdenesFiltrado;
            gvOrdenes.DataBind();
        }

        protected void txtCodOrdenModal_TextChanged(object sender, EventArgs e)
        {
            CargarTabla(txtCodOrdenModal.Text);
        }
        private bool CargarTabla(string search)
        {
            AplicarFiltro();
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

        private byte[] GetPdfFromWebService(int idComprobanteNumerico)
        {
            byte[] pdfFile = null;
            try
            {
                pdfFile = apiReportes.imprimirComprobante(idComprobanteNumerico);
            }
            catch (System.Exception ex)
            {
                MostrarMensaje("Error al obtener el archivo PDF" + ex, false);
                return null;
            }

            return pdfFile;
        }


        protected string armarpdf()
        {
            int idComprobante = (int)Session["idComprobanteSeleccionado"];
            byte[] pdfBytes = GetPdfFromWebService(idComprobante);
            if (pdfBytes != null)
            {
                // Definir la ruta del archivo temporal
                string tempFolderPath = Server.MapPath("~/Temp");

                // Crear la carpeta Temp si no existe
                if (!System.IO.Directory.Exists(tempFolderPath))
                {
                    System.IO.Directory.CreateDirectory(tempFolderPath);
                }
                string nombrearch = "Comprobante_" + idComprobante + ".pdf";
                // Definir la ruta completa del archivo PDF
                string tempFilePath = System.IO.Path.Combine(tempFolderPath, nombrearch);

                // Guardar el PDF en la ruta temporal
                System.IO.File.WriteAllBytes(tempFilePath, pdfBytes);

                // Mostrar el PDF en un iframe (opcional)
                string base64Pdf = Convert.ToBase64String(pdfBytes);
                //pdfFrame.Attributes["src"] = "data:application/pdf;base64," + base64Pdf;
                //pdfFrame.Style["display"] = "block";

                // Llamar a la función de envío de correo con el archivo adjunto
                return tempFilePath;
            }
            return null;
        }

        protected string CrearContenido()
        {
            string contenido = "<html><body style='font-family: Arial, sans-serif;'>";
            contenido += "<h2 style='text-align: center;'>Saludos Estimado(a),</h2>";
            contenido += "<p>Adjunto encontrará el comprobante Nro: " + comprobante.idComprobanteCadena.ToString() + ".</p>";


            contenido += "</table>";
            contenido += "<p style='text-align: right; font-weight: bold; margin-top: 20px;'>Total: " + comprobante.ordenAsociada.total.ToString("N2") + "</p>";
            contenido += "<p>Quedamos a disposición para cualquier consulta.</p>";
            contenido += "<p>Atentamente,<br>BBB Ventas</p>";
            contenido += "</body></html>";

            return contenido;
        }
        protected void lbEnviaroModal_Click(object sender, EventArgs e)
        {
            
            string asunto = "Comprobante Nro: " + comprobante.idComprobanteCadena;
            string contenido = CrearContenido();
            string correo = txtCorreo.Text.ToString();

            int idComprobante = (int)Session["idComprobanteSeleccionado"];
            int resultado;
            //string path = armarpdf();

            resultado = apiCorreo.enviarCorreoWeb(asunto, contenido, correo, GetPdfFromWebService(idComprobante));


            if (resultado == 0)
            {
                MostrarMensaje("Ingrese un correo valido", resultado == 0);
                return;
            }
            else
            {
                MostrarMensaje("Correo Enviado", false);

            }
            Response.Redirect("/Views/Comprobantes.aspx");
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