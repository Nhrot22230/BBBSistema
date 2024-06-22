<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PersonasEmpleados.aspx.cs" Inherits="DxnSisventas.Views.PersonasEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Personas > Empleados</p>
    <hr>
  </div>
  <div class="container">
    <div class="container row">
      <h1>Registro de Empleados</h1>
    </div>
    <div class="container row">
      <div class="col-md-6">
        <asp:Panel runat="server" DefaultButton="BtnBuscar" class="input-group mb-3">
          <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar"></asp:TextBox>
          <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'> </i>"
            CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
        </asp:Panel>
      </div>

    </div>
    <div class="container row">
      <!-- Panel de filtros -->
      <label class="col-sm-2 col-form-label">Filtrar por Rol</label>
      <!-- Seleccionar el tipo de producto -->
      <div class="col-sm-3">
        <asp:DropDownList AutoPostBack="true" ID="DropDownListRoles" runat="server" CssClass="form-select"
          OnSelectedIndexChanged="DropDownListRoles_SelectedIndexChanged">
          <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
          <asp:ListItem Text="Repartidor" Value="Repartidor"></asp:ListItem>
          <asp:ListItem Text="Encargado de Ventas" Value="EncargadoVentas" />
          <asp:ListItem Text="Encargado de Almacen" Value="EncargadoAlmacen" />
          <asp:ListItem Text="Administrador" Value="Administrador" />
        </asp:DropDownList>
      </div>
      <div class="p-3">
        <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
          OnClick="BtnAgregar_Click" CssClass="btn btn-success" />
      </div>
    </div>
    <div class="container row ">
      <!-- PageSize para modificar cuantos registros se muestran por pagina -->
      <asp:GridView ID="GridEmpleado" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="7"
        OnPageIndexChanging="GridEmpleado_PageIndexChanging"
        CssClass="table table-hover table-responsive table-striped"
        OnSelectedIndexChanged="GridEmpleado_SelectedIndexChanged">
        <Columns>
          <asp:BoundField DataField="idEmpleadoCadena" HeaderText="Id" />
          <asp:BoundField DataField="nombre" HeaderText="Nombre" />
          <asp:TemplateField HeaderText="Apellidos">
            <ItemTemplate>
              <%# Eval("apellidoPaterno") + " " + Eval("apellidoMaterno") %>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="sueldo" HeaderText="Sueldo" />
          <asp:BoundField DataField="rol" HeaderText="Rol" />
          <asp:TemplateField HeaderText="">
            <ItemTemplate>
              <asp:LinkButton ID="BtnEditar" runat="server" Text="<i class='fas fa-edit ps-2'>  </i>"
                OnClick="BtnEditar_Click" CommandArgument='<%# Eval("idEmpleadoNumerico") %>' />
              <asp:LinkButton ID="BtnEliminar" runat="server" Text="<i class='fas fa-trash ps-2'>  </i>"
                OnClick="BtnEliminar_Click" CommandArgument='<%# Eval("idEmpleadoNumerico") %>'
                OnClientClick="return confirm('¿Esta seguro de eliminar este registro?');" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="100px" />
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>
