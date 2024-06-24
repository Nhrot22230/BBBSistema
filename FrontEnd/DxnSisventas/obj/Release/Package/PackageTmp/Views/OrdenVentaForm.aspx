<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="OrdenVentaForm.aspx.cs" Inherits="DxnSisventas.Views.OrdenVentaForm" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CustomStyles/OrdenVenta.css?v5" rel="stylesheet" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <script src="../CustomScripts/OrdenVenta.js?v5"></script>



    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2 class="card-title">Orden de Venta</h2>
            </div>
            <div class="body">
                <div class="card mb-3">
                    <div class="card-header">
                        <h4>Información de la Orden de Venta
                        </h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="row mb-3">

                                <label for="TxtId" class="col-sm-2 col-form-label">ID Orden Venta:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TxtIdOrdenVenta" runat="server" Enabled="false"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                                <label for="ddlEstado" class="col-sm-2 col-form-label">Estado:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select"
                                        SelectionMode="Single">
                                        <asp:ListItem Text="Pendiente" Enabled="true" Selected="True"
                                            Value="Pendiente"></asp:ListItem>
                                        <asp:ListItem Text="Cancelado" Enabled="true" Value="Cancelado">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Entregado" Enabled="true" Value="Entregado">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label for="ddlMetodoDePago" class="col-sm-2 col-form-label">Metodo Pago:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlMetodoDePago" runat="server" CssClass="form-select"
                                        SelectionMode="Single">
                                        <asp:ListItem Text="Efectivo" Enabled="true" Selected="True"
                                            Value="Efectivo"></asp:ListItem>
                                        <asp:ListItem Text="Tarjeta" Enabled="true" Value="Tarjeta"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label for="ddlTipoVenta" class="col-sm-2 col-form-label">Tipo de Venta:</label>

                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlTipoVenta" runat="server" CssClass="form-select"
                                        SelectionMode="Single" OnSelectedIndexChanged="ddlTipoVenta_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="Presencial" Value="Presencial">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Delivery" Value="Delivery" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row mb-3">

                                <label for="TxtFechaCreacion" class="col-sm-2 col-form-label">
                                    Fecha de
                                        creacion</label>

                                <div class="col-sm-3">
                                    <asp:TextBox ID="TxtFechaCreacion" runat="server" CssClass="form-control"
                                        type="date"></asp:TextBox>
                                </div>
                                <label for="TxtFechaEntrega" class="col-sm-2 col-form-label">
                                    Fecha de
                                        entrega</label>

                                <div class="col-sm-3">
                                    <asp:TextBox ID="TxtFechaEntrega" runat="server" CssClass="form-control"
                                        type="date"></asp:TextBox>
                                </div>
                            </div>

                    
                        </div>
                    </div>

                </div>
                <div class="card mb-3">
                    <div class="card-header">
                        <h4>Información del Cliente
                        </h4>
                    </div>
                    <div class="card-body">
                        <div class="row  mb-3">
                           
                            <label for="TxtIDCliente" class="col-lg-2 col-form-label mt-2">ID Cliente:</label>
                            <div class="col-lg-3 mt-2">
                                <asp:TextBox ID="TxtIDCliente" runat="server" Enabled="false"
                                    CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                            <label for="TxtPuntos" class="col-lg-2 col-form-label mt-2">Puntos:</label>
                            <div class="col-lg-3 mt-2">
                                <asp:TextBox ID="TxtPuntos" runat="server" Enabled="false"
                                    CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                           
                        </div>
                        

                        <div class="row mb-3">
                             <label for="TxtNombreCompletoCliente" class="col-lg-2 col-form-label mt-2">
                                 Nombre
                                     completo:</label>
                             <div class="col-lg-5 mt-2">
                                 <asp:TextBox ID="TxtNombreCompletoCliente" runat="server" Enabled="false"
                                     CssClass="form-control" required="true"></asp:TextBox>
                             </div>

                             <div class="col-lg-2  mt-3">
                                 <asp:LinkButton ID="lbBuscarCliente" runat="server" CssClass="btn btn-primary btn-sm"
                                     OnClick="lbBuscarCliente_Click">Buscar
                                 </asp:LinkButton>
                             </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="panelRepartidor" runat="server">
                    <div class="card mb-3">
                        <div class="card-header">
                            <h4>Información del Repartidor
                            </h4>
                        </div>
                        <div class="card-body">

                            <div class="row mb-3">
                                <label for="TxtIDRepartidor" class="col-lg-2 col-form-label">ID Repartidor:</label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtIDRepartidor" runat="server" Enabled="false"
                                        CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                               
                                
                            </div>
                            <div class="row mb-3">
                                 <label for="TxtNombreCompletoRepartidor" class="col-lg-2 col-form-label">
                                 Nombre
                                 completo:</label>
                                <div class="col-lg-5 mb-3">
                                    <asp:TextBox ID="TxtNombreCompletoRepartidor" runat="server" Enabled="false"
                                        CssClass="form-control" required="true"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 mb-3">
                                    <asp:LinkButton ID="lbBuscarRepartidor" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="lbBuscarRepartidor_Click">
                                        Buscar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>



                <div class="card mb-3">
                    <div class="card-header mb-3">
                        <h4>Detalle de la Orden de Venta
                        </h4>
                    </div>
                    <div class="card-body">


                        <asp:Panel ID="panelBusquedaProducto" runat="server">
                            <div class="row mb-3">
                                <label for="TxtIdProducto" class="col-md-2 col-form-label">ID Producto:</label>
                                <div class="col-md-2 mb-3">
                                    <asp:TextBox ID="TxtIdProducto" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnBuscarProducto" runat="server" Text="Buscar producto"
                                        OnClick="btnBuscarProducto_Click"
                                        CssClass="btn btn-primary btn-sm" />
                                </div>

                            </div>
                            <div class="row mb-3">
                                <label for="TxtNombreProducto" class="col-md-2 col-form-label mb-2">Nombre producto:</label>
                                <div class="col-md-2 mb-3">
                                    <asp:TextBox ID="TxtNombreProducto" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <label for="TxtPrecio" class="col-sm-1 col-form-label mb-2">Precio:</label>
                                <div class="col-sm-1">
                                    <asp:TextBox ID="TxtPrecio" runat="server" CssClass="form-control mb-2" Enabled="false" type="number"
                                        step="1"></asp:TextBox>
                                </div>
                                
                            </div>
                            <div class="row mb-3">
                                <label for="TxtStock" class="col-sm-2 col-form-label mb-2">Stock:</label>
                                <div class="col-sm-2 mb-2">
                                    <asp:TextBox ID="TxtStock" runat="server" CssClass="form-control" Enabled="false" type="number"
                                        step="1"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <label for="TxtCantidad" class="col-sm-2 col-form-label">Cantidad:</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="TxtCantidad"
                                        runat="server"
                                        CssClass="form-control"
                                        type="number"
                                        oninput="verificarStock(this)"
                                        onkeydown="return avoidEnterKey(event);">
                                    step="1"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar producto"
                                        CssClass="btn btn-info" OnClick="btnAgregarProducto_Click" />
                                </div>
                            </div>
                        </asp:Panel>


                        <div class="row">
                            <asp:GridView ID="gvLineasOrdenVenta" AllowPaging="True" PageSize="5" runat="server" 
                                AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered" OnRowDataBound="gvLineasOrdenVenta_RowDataBound"
                                OnPageIndexChanging="gvLineasOrdenVenta_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField  HeaderText="ID Producto" />
                                    <asp:BoundField HeaderText="Producto" />
                                    <asp:BoundField HeaderText="Cantidad" />
                                    <asp:BoundField HeaderText="Precio" />
                                    <asp:BoundField HeaderText="Subtotal" />
                                    
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                                OnClick="BtnEliminar_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row align-items-center justify-content-end mb-2">
                            <asp:Label ID="lblTotalSinDescuento" runat="server" Text="Total sin descuento:" CssClass="col-form-label col-sm-2 text-end" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="TxtTotalSinDescuento" CssClass="form-control col-sm-2" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center justify-content-end mb-2">
                            <asp:Label ID="lblDescuento" runat="server" Text="Descuento (S/.)" CssClass="col-form-label col-sm-2 text-end" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="TxtDescuento" runat="server"
                                    type="number"
                                    step="0.01"
                                    CssClass="form-control"
                                    value="0.00"
                                    onkeydown="return avoidEnterKey(event);">
                                </asp:TextBox>
                            </div>
                        </div>

                  

                        <div class="row align-items-center justify-content-end mb-2">
                            <asp:Label ID="lblTotal" runat="server" Text="Total:" CssClass="col-form-label col-sm-2 text-end" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtTotal" CssClass="form-control col-sm-2" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-footer">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"
                        CssClass="float-end btn btn-success mb-2" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                        CssClass="float-start btn btn-danger" />
                </div>
            </div>
        </div>


        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div id="modalFormBuscarEmpleado" class="modal" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Busqueda de Repartidor</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="container row pb-3 pt-3">
                                    <div class="row align-items-center">
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" ID="TxtPatronBusquedaEmpleado"
                                                runat="server" placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:LinkButton ID="lbBuscarEmpleadoModal" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                                                OnClick="lbBuscarEmpleadoModal_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="container row">
                                    <asp:GridView ID="gvEmpleados" runat="server" AllowPaging="true"
                                        PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped"
                                        OnPageIndexChanging="gvEmpleados_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idEmpleadoCadena" HeaderText="Id" />
                                            <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <%# Eval("apellidoPaterno") + " " + Eval("apellidoMaterno") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton class="btn btn-outline-primary"
                                                        runat="server"
                                                        Text="Seleccionar"
                                                        ID="btnSeleccionarModalEmpleado"
                                                        OnClick="btnSeleccionarModalEmpleado_Click"
                                                        CommandArgument='<%# Eval("idEmpleadoNumerico") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalFormBuscarCliente" class="modal" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Busqueda de Clientes</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="container row pb-3 pt-3">
                                    <asp:Panel runat="server" DefaultButton="lbBuscarClienteModal" class="row align-items-center">
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" ID="TxtPatronBusquedaCliente"
                                                runat="server" placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:LinkButton ID="lbBuscarClienteModal" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                                                OnClick="lbBuscarClienteModal_Click" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="container row">
                                    <asp:GridView ID="gvClientes" runat="server" AllowPaging="true"
                                        PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped"
                                        OnPageIndexChanging="gvClientes_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idCadena" HeaderText="ID Cliente" />
                                            <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <%# Eval("apellidoPaterno") + " " + Eval("apellidoMaterno") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="puntos" HeaderText="Puntos" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton class="btn btn-outline-primary"
                                                        runat="server"
                                                        Text="Seleccionar"
                                                        ID="btnSeleccionarModalCliente"
                                                        OnClick="btnSeleccionarModalCliente_Click"
                                                        CommandArgument='<%# Eval("idNumerico") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>





        <div id="modalFormBuscarProducto" class="modal" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Busqueda de Productos</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="container row pb-3 pt-3">
                                    <asp:Panel runat="server" DefaultButton="lbBuscarProductos" class="row align-items-center">
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" ID="TxtPatronBusquedaProducto"
                                                runat="server" placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:LinkButton ID="lbBuscarProductos" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                                                OnClick="lbBuscarProductos_Click" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="container row">
                                    <asp:GridView ID="gvProductos" runat="server" AllowPaging="true"
                                        PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped"
                                        OnPageIndexChanging="gvProductos_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idProductoCadena" HeaderText="ID Producto" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="precioUnitario" HeaderText="Precio Unitario" />
                                            <asp:BoundField DataField="stock" HeaderText="Stock" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton class="btn btn-outline-primary"
                                                        runat="server"
                                                        Text="Seleccionar"
                                                        ID="btnSeleccionarModalProducto"
                                                        OnClick="btnSeleccionarModalProducto_Click"
                                                        CommandArgument='<%# Eval("idProductoNumerico") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
