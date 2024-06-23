<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PersonasClientes.aspx.cs" Inherits="DxnSisventas.Views.PersonasClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Personas > Clientes</p>
    <hr>
  </div>
  <div class="container">
    <div class="container row">
      <h1>Registro de Cliente</h1>
    </div>
    <div class="container row">
      <div class="container row">
        <div class="col-md-6">
          <div class="input-group mb-3">
            <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar"></asp:TextBox>
            <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'> </i>"
              CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
          </div>
        </div>
      </div>
    </div>
    <div class="container row">
      <div class=" p-3">
        <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
          OnClick="BtnAgregar_Click" CssClass="btn btn-success" />
      </div>
    </div>
    <div class="container row ">
      <!-- PageSize para modificar cuantos registros se muestran por pagina -->
      <asp:GridView ID="GridCliente" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="7"
        OnPageIndexChanging="GridCliente_PageIndexChanging"
        CssClass="table table-hover table-responsive table-striped">
        <Columns>
          <asp:BoundField DataField="idCadena" HeaderText="Id" />
          <asp:BoundField DataField="DNI" HeaderText="DNI" />
          <asp:BoundField DataField="nombre" HeaderText="Nombre" />
          <asp:TemplateField HeaderText="Apellidos">
            <ItemTemplate>
              <%# Eval("apellidoPaterno") + " " + Eval("apellidoMaterno") %>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="puntos" HeaderText="Puntos" />
          <asp:BoundField DataField="direccion" HeaderText="Direccion" />
          <asp:BoundField DataField="RUC" HeaderText="RUC" />
          <asp:BoundField DataField="RazonSocial" HeaderText="RazonSocial" />

          <asp:TemplateField HeaderText="">
            <ItemTemplate>
              <asp:LinkButton runat="server" Text="<i class='fas fa-edit ps-2'>  </i>" OnClick="BtnEditar_Click"
                CommandArgument='<%# Eval("idNumerico") %>' />
              <asp:LinkButton runat="server" Text="<i class='fas fa-trash ps-2'>  </i>" OnClick="BtnEliminar_Click"
                CommandArgument='<%# Eval("idNumerico") %>'
                OnClientClick="return confirm('¿Esta seguro de eliminar este registro?');" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="100px" />
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>
