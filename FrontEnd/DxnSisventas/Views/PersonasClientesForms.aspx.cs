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
  public partial class PersonasClientesForms : System.Web.UI.Page
  {
    PersonasAPIClient personasAPIClient = new PersonasAPIClient();
    BindingList<cliente> patrocinadores;

    private bool CargarTabla(string filtro)
    {
      cliente[] listaPatrocinadores = personasAPIClient.listarClientes(filtro);
      if (listaPatrocinadores == null)
      {
        return false;
      }

      patrocinadores = new BindingList<cliente>(listaPatrocinadores.ToList());
      return true;
    }

    private void GridBind()
    {
      GridClientes.DataSource = patrocinadores;
      GridClientes.DataBind();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      if (Session["clienteEditar"] != null)
      {
        cliente clienteEditar = (cliente) Session["clienteEditar"];
        Page.Title = "Editar Cliente: " + clienteEditar.nombre + " " + clienteEditar.idCadena;
        TxtNombre.Text = clienteEditar.nombre;
        TxtApellidoPat.Text = clienteEditar.apellidoPaterno;
        TxtApellidoMat.Text = clienteEditar.apellidoMaterno;
        TxtDireccion.Text = clienteEditar.direccion;
        TxtDNI.Text = clienteEditar.DNI;
        TxtId.Text = clienteEditar.idCadena;
        TxtRUC.Text = clienteEditar.RUC;
        TxtRazonSocial.Text = clienteEditar.razonSocial;
        TxtPuntos.Text = clienteEditar.puntos.ToString();
        if (clienteEditar.patrocinador != null)
        {
          TxtIdPatrocinador.Text = clienteEditar.patrocinador.idCadena;
          TxtNombrePatrocinador.Text = clienteEditar.patrocinador.nombre + " " + clienteEditar.patrocinador.apellidoPaterno + " " + clienteEditar.patrocinador.apellidoMaterno;
        }
      }
      else
      {
        Page.Title = "Nuevo Cliente";
      }

      if (!IsPostBack)
      {
        CargarTabla("");
        GridBind();
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnBuscarPatrocinador_Click(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {

      cliente clienteEditar = Session["clienteEditar"] != null ? (cliente) Session["clienteEditar"] : new cliente();
      clienteEditar.nombre = TxtNombre.Text;
      clienteEditar.apellidoPaterno = TxtApellidoPat.Text;
      clienteEditar.apellidoMaterno = TxtApellidoMat.Text;
      clienteEditar.direccion = TxtDireccion.Text;
      clienteEditar.DNI = TxtDNI.Text;
      clienteEditar.RUC = TxtRUC.Text;
      clienteEditar.razonSocial = TxtRazonSocial.Text;
      clienteEditar.puntos = int.Parse(TxtPuntos.Text);

      if (TxtIdPatrocinador.Text != "")
      {
        clienteEditar.patrocinador = (cliente) Session["patrocinadorSeleccionado"];
      }

      int result;

      if (clienteEditar.idCadena == null)
      {
        result = personasAPIClient.insertarCliente(clienteEditar);
      }
      else
      {
        result = personasAPIClient.actualizarCliente(clienteEditar);
      }

      if (result == 0)
      {
        MostrarMensaje("Error al guardar el cliente", false);
        return;
      }


      LimpiarCampos();
      Response.Redirect("~/Views/PersonasClientes.aspx");
    }

    protected void BtnRegresar_Click(object sender, EventArgs e)
    {
      LimpiarCampos();
      Response.Redirect("~/Views/PersonasClientes.aspx");
    }

    protected void GridClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      CargarTabla(TxtBuscarCliente.Text);
      GridClientes.PageIndex = e.NewPageIndex;
      GridBind();
      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }

    protected void BtnBuscarCliente_Click(object sender, EventArgs e)
    {
      bool flag = CargarTabla(TxtBuscarCliente.Text);
      if (flag)
      {
        MostrarMensaje($"Se encontraron {patrocinadores.Count} clientes", flag);
      }
      else
      {
        MostrarMensaje("No se encontraron clientes", flag);
      }
      GridBind();
      ScriptManager.RegisterStartupScript(this, GetType(), "showModalForm", "showModalForm();", true);
    }

    protected void GridClientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "Select")
      {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridClientes.Rows[index];
        string idPatrocinador = row.Cells[0].Text;

        CargarTabla("");
        cliente patrocinador = patrocinadores.FirstOrDefault(p => p.idCadena == idPatrocinador);

        if (patrocinador == null) return;

        if (patrocinador.idCadena == TxtId.Text)
        {
          MostrarMensaje("No puedes seleccionar al mismo cliente como patrocinador", false);
          return;
        }


        Session["patrocinadorSeleccionado"] = patrocinador;
        TxtIdPatrocinador.Text = patrocinador.idCadena;
        TxtNombrePatrocinador.Text = patrocinador.nombre + " " + patrocinador.apellidoPaterno + " " + patrocinador.apellidoMaterno;
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
    private void LimpiarCampos()
    {
      Session["clienteEditar"] = null;
      Session["patrocinadorSeleccionado"] = null;
      TxtNombre.Text = "";
      TxtApellidoPat.Text = "";
      TxtApellidoMat.Text = "";
      TxtDireccion.Text = "";
      TxtDNI.Text = "";
      TxtId.Text = "";
      TxtRUC.Text = "";
      TxtRazonSocial.Text = "";
      TxtBuscarCliente.Text = "";
    }
  }
}