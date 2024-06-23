<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PersonasClientesForms.aspx.cs" Inherits="DxnSisventas.Views.PersonasClientesForms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Personas > Clientes > Agregar / Editar</p>
    <hr>
  </div>
  <div class="container">
    <!-- Formulario de registro de Cliente -->
    <div class="card">
      <div class="card-header">
        <asp:Label ID="lblTitulo" Text="Datos de Cliente" runat="server"></asp:Label>
      </div>
      <div class="card-body">
        <div class="card">
          <div class="card-header">
            <h3>Información del Cliente</h3>
          </div>
          <div class="card-body">
            <div class="row">
              <!-- Identificador -->
              <label class="col-sm-2 col-form-label">Id Cliente</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtId" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
              </div>
              <label class="col-sm-2 col-form-label">DNI</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtDNI" runat="server" CssClass="form-control" onblur="validateTxtDNI()"></asp:TextBox>
                <p id="errMsgDNI" style="color: red; display: none;"></p>
              </div>
            </div>
            <div class="row">
              <!-- Nombre -->
              <label class="col-sm-2 col-form-label">Nombre</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" onblur="validateTxtNombre()"></asp:TextBox>
                <p id="errMsgNombre" style="color: red; display: none;"></p>
              </div>
              <!-- Apellido Paterno -->
              <label class="col-sm-2 col-form-label">Apellido Paterno</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtApellidoPat" runat="server" CssClass="form-control" onblur="validateTxtApellidoPat()"></asp:TextBox>
                <p id="errMsgApellidoPat" style="color: red; display: none;"></p>
              </div>
            </div>
            <div class="row">
              <!-- Apellido Materno -->
              <label class="col-sm-2 col-form-label">Apellido Materno</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtApellidoMat" runat="server" CssClass="form-control" onblur="validateTxtApellidoMat()"></asp:TextBox>
                <p id="errMsgApellidoMat" style="color: red; display: none;"></p>
              </div>
              <!-- Direccion -->
              <label class="col-sm-2 col-form-label">Dirección</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                <p id="errMsgDireccion" style="color: red; display: none;"></p>
              </div>
            </div>
            <div class="row">
              <!-- RUC -->
              <label class="col-sm-2 col-form-label">RUC</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtRUC" runat="server" CssClass="form-control" onblur="validateTxtRUC()"></asp:TextBox>
                <p id="errMsgRUC" style="color: red; display: none;"></p>
              </div>
              <!-- Razon Social -->
              <label class="col-sm-2 col-form-label">Razon Social</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                <p id="errMsgRazonSocial" style="color: red; display: none;"></p>
              </div>
            </div>
            <div class="row">
              <!-- Puntos -->
              <label class="col-sm-2 col-form-label">Puntos</label>
              <div class="col-sm-4 mb-3">
                <asp:TextBox ID="TxtPuntos" runat="server" CssClass="form-control" onblur="validateTxtPuntos()"></asp:TextBox>
                <p id="errMsgPuntos" style="color: red; display: none;"></p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="card">
      <div class="card-header">
        <h3>Información del Patrocinador</h3>
      </div>
      <div class="card-body">
        <div class="row">
          <!-- Identificador -->
          <label class="col-sm-2 col-form-label">Id Patrocinador</label>
          <div class="col-sm-4 mb-3">
            <asp:TextBox ID="TxtIdPatrocinador" Enabled="false" runat="server" CssClass="form-control" />
          </div>
          <!-- Nombre -->
          <label class="col-sm-2 col-form-label">Nombre</label>
          <div class="col-sm-4 mb-3">
            <asp:TextBox ID="TxtNombrePatrocinador" Enabled="false" runat="server" CssClass="form-control" />
          </div>
          <!-- Buscar -->
          <div class="col-sm-2 mb-3">
            <asp:Button ID="BtnBuscarPatrocinador" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscarPatrocinador_Click" />
          </div>
        </div>
      </div>
      <div class="card-footer">
        <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClientClick="return validateForm();" OnClick="BtnGuardar_Click" />
        <asp:Button ID="BtnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary" OnClick="BtnRegresar_Click" />
      </div>
    </div>
  </div>

  <div id="modalClienteForm" class="modal" tabindex="-1">
    <div class="modal-dialog modal-lg" style="max-width: 80vw;">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Busqueda de clientes</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
            onclick="hideModalForm">
          </button>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="col-12">
              <div class="input-group mb-3">
                <asp:TextBox ID="TxtBuscarCliente" runat="server" CssClass="form-control" placeholder="Buscar Patrocinador" />
                <asp:Button ID="BtnBuscarCliente" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscarCliente_Click" />
              </div>
            </div>
          </div>
          <div class="row">
            <asp:GridView ID="GridClientes" runat="server" CssClass="table table-striped table-hover"
              PageSize="5" AllowPaging="True" OnPageIndexChanging="GridClientes_PageIndexChanging"
              OnRowCommand="GridClientes_RowCommand" AutoGenerateColumns="false">
              <Columns>
                <asp:BoundField DataField="idCadena" HeaderText="IdCliente" />
                <asp:BoundField DataField="DNI" HeaderText="DNI" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellidoPaterno" HeaderText="Apellido Paterno" />
                <asp:BoundField DataField="apellidoMaterno" HeaderText="Apellido Materno" />
                <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                <asp:BoundField DataField="RUC" HeaderText="RUC" />
                <asp:BoundField DataField="razonSocial" HeaderText="Razon Social" />
                <asp:ButtonField ControlStyle-CssClass="btn btn-primary" ButtonType="Button" CommandName="Select" Text="Seleccionar"></asp:ButtonField>
              </Columns>
            </asp:GridView>
          </div>
        </div>
      </div>
    </div>
  </div>
  <script type="text/javascript" src="/CustomScripts/PersonasClientesForms.js"></script>  
</asp:Content>
