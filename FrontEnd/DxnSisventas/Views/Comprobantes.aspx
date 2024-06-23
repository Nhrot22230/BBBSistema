<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Comprobantes.aspx.cs" Inherits="DxnSisventas.Views.Comprobantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CustomStyles/Comprobante.css" rel="stylesheet" />
    <script type="text/javascript">
        function openInNewTab() {
         window.document.forms[0].target = '_blank'; 
         setTimeout(function () { window.document.forms[0].target = ''; }, 1);
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Documentos > Comprobantes</p>
    <hr>
  </div>
  <div class="container">
    <div class="container row">
      <h1>Registro de Comprobantes</h1>
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
      <div class="container row">
        
        <!-- Seleccionar el tipo de comprobante -->
        <div class="col-sm-4">
            <asp:Label class="col-form-label" runat="server" Text="<i class='fas fa-solid fa-filter'> </i> Filtrar por Tipo de Comprobante:"></asp:Label>
          <asp:DropDownList AutoPostBack="true" ID="DropDownListTipoComprobante" runat="server" CssClass="form-select" OnSelectedIndexChanged="DropDownListTipoComprobante_SelectedIndexChanged">
            <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
            <asp:ListItem Text="Boleta" Value="BoletaSimple"></asp:ListItem>
            <asp:ListItem Text="Factura" Value="Factura"></asp:ListItem>
          </asp:DropDownList>
        </div>
          <div class="col-sm-5">
            <asp:Label class="col-form-label" runat="server" Text="<i class='fas fa-solid fa-filter'> </i> Filtrar por Tipo de Orden Asociada:"></asp:Label>
             <asp:DropDownList AutoPostBack="true" ID="DropDownListTipoOrdenAsociada" runat="server" CssClass="form-select" OnSelectedIndexChanged="DropDownListTipoOrdenAsociada_SelectedIndexChanged">
            <asp:ListItem Text="-" Value="Todos"></asp:ListItem>
            <asp:ListItem Text="Compra" Value="Compra"></asp:ListItem>
            <asp:ListItem Text="Venta" Value="Venta"></asp:ListItem>
          </asp:DropDownList>
        </div>
          <div class="col-sm-3">
             <asp:Label class="col-form-label" runat="server" Text="<i class='fas fa-solid fa-sort'> </i> Ordenar por:"></asp:Label>
          <asp:DropDownList AutoPostBack="true" ID="DropDownListOrdenamientoComprobante" runat="server" CssClass="form-select" OnSelectedIndexChanged="DropDownListOrdenamientoComprobante_SelectedIndexChanged">
             <asp:ListItem Text="ID Ascendente" Value="IDAsc"></asp:ListItem>
              <asp:ListItem Text="ID Descendente" Value="IDDesc"></asp:ListItem>
            <asp:ListItem Text="Totales más altos" Value="TotalDesc"></asp:ListItem>
            <asp:ListItem Text="Totales más bajos" Value="TotalAsc"></asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="text p-3">
          <asp:LinkButton ID="BtnAgregar" runat="server" Text="<i class='fas fa-plus pe-2'> </i> Agregar"
            OnClick="BtnAgregar_Click" CssClass="btn btn-primary btn-sm" />
        </div>
      </div>
      <div class="row overflow-x-scroll">
        <asp:GridView ID="GridComprobantes" runat="server" AutoGenerateColumns="false"
          AllowPaging="true" PageSize="7" OnPageIndexChanging="GridComprobantes_PageIndexChanging"
          OnRowDataBound="GridComprobantes_RowDataBound"
            CssClass="table table-hover table-responsive table-striped">
          <Columns>
            <asp:BoundField DataField="idComprobanteCadena" HeaderText="ID Comprobante" />
            <asp:BoundField HeaderText="Fecha Emisión" />
            <asp:BoundField HeaderText="Tipo de comprobante" />
            <%--asp:BoundField DataField="ordenAsociada.idOrden" HeaderText="ID Orden Asociada"/> --%>
            <asp:TemplateField HeaderText="ID Orden Asociada" >
              <ItemTemplate>
                <asp:Label ID="LblOrdenVentaCompra" runat="server" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ordenAsociada.total" HeaderText="Total" />
            <asp:TemplateField HeaderText="">
              <ItemTemplate>
                <asp:LinkButton ID="BtnVisualizar" runat="server" Text="<i class='fas fa-solid fa-eye ps-2'>  </i>"
        OnClick="BtnVisualizar_Click" CommandArgument='<%# Eval("idComprobanteNumerico") %>' />
                <%--<asp:LinkButton ID="BtnImprimir" runat="server" Text="<i class='fas fa-solid fa-print ps-2'>  </i>"
                    OnClick="BtnImprimir_Click" OnClientClick="openInNewTab();" CommandArgument='<%# Eval("idComprobanteNumerico") %>' />--%>
                <asp:LinkButton ID="BtnEliminar" runat="server" Text="<i class='fas fa-solid fa-trash ps-2'></i>"
                  OnClick="BtnEliminar_Click" CommandArgument='<%# Eval("idComprobanteNumerico") %>'
                  OnClientClick="return confirm('¿Esta seguro de eliminar este registro?');" />
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
      </div>
    </div>
  </div>
</asp:Content>
