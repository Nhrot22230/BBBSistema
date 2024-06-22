<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="DxnSisventas.Views.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Productos</p>
    <hr>
  </div>
  <div class="container">
    <div class="container row">
      <h1>Productos</h1>
    </div>
    <div class="container row">
      <div class="container row">
        <div class="col-md-6">
          <asp:Panel runat="server" DefaultButton="BtnBuscar" class="input-group mb-3">
            <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar"></asp:TextBox>
            <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'> </i>"
              CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
          </asp:Panel>
        </div>
      </div>
    </div>
    <div class="container row">
      <div class="text p-3">
        <asp:LinkButton 
          ID="BtnAgregar" runat="server" 
          OnClick="BtnAgregar_Click"
          CssClass="btn btn-success"
          Text="<i class='fas fa-plus pe-2'> </i> Agregar"
          />
      </div>
    </div>
    <div class="container row">
      <asp:GridView
        CssClass="table table-hover table-responsive table-striped"
        ID="GvProductos" runat="server"
        AllowPaging="true" PageSize="8" AutoGenerateColumns="false"
        OnPageIndexChanging="GvProductos_PageIndexChanging">
        <Columns>
          <asp:BoundField DataField="idProductoCadena" HeaderText="ID" />
          <asp:BoundField DataField="nombre" HeaderText="Nombre" />
          <asp:BoundField DataField="tipo" HeaderText="Descripción" />
          <asp:BoundField DataField="stock" HeaderText="Stock" />
          <asp:BoundField DataField="precioUnitario" HeaderText="Precio Unitario" ItemStyle-HorizontalAlign="Right" DataFormatString="S/{0}" />
          <asp:BoundField DataField="puntos" HeaderText="Puntos" ItemStyle-HorizontalAlign="Right" />
          <asp:BoundField DataField="capacidad" HeaderText="Capacidad" ItemStyle-HorizontalAlign="Right" />
          <asp:BoundField DataField="unidadDeMedida" HeaderText="Unidad de medida" />

          <asp:TemplateField HeaderText="Opciones">
            <ItemTemplate>
              <asp:LinkButton ID="BtnEditar" runat="server" Text="<i class='fas fa-edit ps-2'>  </i>"
                OnClick="BtnEditar_Click" CommandArgument='<%# Eval("idProductoNumerico") %>' />
              <asp:LinkButton ID="BtnEliminar" runat="server" Text="<i class='fas fa-trash ps-2'>  </i>"
                OnClick="BtnEliminar_Click" CommandArgument='<%# Eval("idProductoNumerico") %>'
                OnClientClick="return confirm('¿Esta seguro de eliminar este registro?');" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="100px" />
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>
  <div id="modalForm" class="modal" tabindex="-1">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Producto</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="container p-3 ">
            <div class="row">
              <div class="col-md-4  mb-3">
                <asp:Label ID="LblId" runat="server" Text="IdProducto:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
              </div>
              <div class="col-md-8 mb-3">
                <asp:Label ID="LblNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" onblur="validateNombre()"></asp:TextBox>
                <div id="nombreErrorMessage" style="display: none; color: red;">El nombre no puede estar vacío.</div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 mb-3">
                <asp:Label ID="LblTipoProducto" runat="server" Text="Tipo de producto:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="form-select">
                  <asp:ListItem Value="Comestible" Text="Comestible" />
                  <asp:ListItem Value="CuidadoPersonal" Text="Cuidado Personal" />
                </asp:DropDownList>
              </div>
              <div class="col-md-6 mb-3">
                <asp:Label ID="Label2" runat="server" Text="Unidad de medida:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlUnidadMedida" runat="server" CssClass="form-select">
                  <asp:ListItem Value="mililitros" Text="mililitros" />
                  <asp:ListItem Value="gramos" Text="gramos" />
                </asp:DropDownList>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6  mb-3">
                <asp:Label ID="LblStock" runat="server" Text="Stock:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtStock" runat="server" type="number" CssClass="form-control" onblur="validateStock()"></asp:TextBox>
                <div id="stockErrorMessage" style="display: none; color: red;">El valor del stock debe ser mayor que 0.</div>
              </div>
              <div class="col-md-6 mb-3">
                <asp:Label ID="LblPrecio" runat="server" Text="Precio Unitario:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtPrecio" runat="server" type="number" step="any" CssClass="form-control" onblur="validatePrecio()"></asp:TextBox>
                <div id="precioErrorMessage" style="display: none; color: red;">El precio debe ser mayor que 0.</div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6  mb-3">
                <asp:Label ID="LblPuntos" runat="server" Text="Puntos:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtPuntos" runat="server" type="number" CssClass="form-control" Enabled="true" onblur="validatePoints()"></asp:TextBox>
                <div id="puntosErrorMessage" style="display: none; color: red;">El valor debe ser mayor o igual que 0.</div>
              </div>
              <div class="col-md-6 mb-3">
                <asp:Label ID="LblCapacidad" runat="server" Text="Capacidad:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TxtCapacidad" runat="server" type="number" CssClass="form-control" onblur="validateCapacidad()"></asp:TextBox>
                <div id="capacidadErrorMessage" style="display: none; color: red;">La capacidad debe ser mayor que 0.</div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
          <asp:Button ID="ButGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"
            OnClick="ButGuardar_Click" OnClientClick="return validarFormulario();" />
        </div>
      </div>
    </div>
  </div>
  
  <script src="/CustomScripts/Productos.js"></script>
</asp:Content>
