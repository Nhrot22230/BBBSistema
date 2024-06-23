<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenCompraForm.aspx.cs" Inherits="DxnSisventas.Views.OrdenCompraForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/CustomScripts/Documentos.js"></script>
    <script src="../Scripts/jquery-3.7.1.min.js"></script>
    <style>
        .card-header, .modal-header {
            background-color: #002244 !important;
            color: white !important;
        }
        .btn-primary, .btn-info, .btn-success {
            background-color: #002244 !important;
            border-color: #002244 !important;
        }
        .btn-secondary {
            background-color: #778899 !important;
            border-color: #778899 !important;
        }
        .btn-close {
            background-color: #002244 !important;
        }
        .btn-link {
            color: #002244 !important;
        }
        .table {
            background-color: white !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="card">
            <div class="card-header text-center">
                <h2>Registrar Orden de Compra</h2>
            </div>
            <div class="card-body">
                <div id="cardInfo" runat="server" class="card border mb-4">
                    <div class="card-header card-header-info">
                        <h5 class="card-title mb-0">Información del Orden</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3 row">
                            <asp:Label ID="lblID" runat="server" Text="ID: " CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtID" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                            <asp:Label ID="lblEstado" runat="server" Text="Estado: " CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtEstado" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblFecha" runat="server" Text="Creado:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtFecha" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                            <asp:Label ID="lblFechaEnvio" runat="server" Text="Enviado: " CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtFechaEnvio" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card border mb-4">
                    <div class="card-header">
                        <h5 class="card-title mb-0">Detalle de la Orden de Compra</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3 row">
                            <asp:Label ID="lblIDProducto" runat="server" Text="ID del Producto:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtIDProducto" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnBuscarProducto" CssClass="btn btn-primary" runat="server" Text="Buscar Producto" OnClick="btnBuscarProducto_Click" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblNombreProducto" runat="server" Text="Nombre:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtNombreProducto" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblPrecioUnitProducto" runat="server" Text="Precio Unitario:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtPrecioUnitProducto" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblCantidadUnidades" runat="server" Text="Cantidad:" CssClass="col-sm-2 col-form-label" />
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtCantidadUnidades" type="number" runat="server" onkeydown="if (event.keyCode == 13) return false;"  CssClass="form-control" />
                            </div>
                            <div class="col-sm-4">
                                <asp:LinkButton ID="lbAgregarLOV" CssClass="btn btn-success" runat="server" Text="Agregar" OnClick="lbAgregarLOV_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:GridView ID="gvLineasOrdenVenta" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="gvLineasOrdenVenta_PageIndexChanging1">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nombre Producto">
                                        <ItemTemplate>
                                            <%# Eval("producto.nombre") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contenido">
                                        <ItemTemplate>
                                            <%# Eval("producto.capacidad") + " " + Eval("producto.unidadDeMedida") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Precio Unit." DataField="producto.precioUnitario" DataFormatString="{0:C}" HtmlEncode="false" />
                                    <asp:BoundField HeaderText="Cant" DataField="cantidad" />
        <asp:BoundField HeaderText="Subtotal" DataField="subtotal" DataFormatString="{0:C}" HtmlEncode="false" />
        <asp:TemplateField HeaderText="Eliminar">
            <ItemTemplate>
                <asp:LinkButton
                    runat="server" ID="btnEliminarProducto" CssClass="btn btn-link btn-sm p-0" OnClick="btnEliminarProducto_Click" CommandArgument='<%# Eval("producto.idProductoNumerico") + "," + Eval("cantidad") %>'>
                    <i class='fa fa-trash' style="color: #002244;"></i>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


                        </div>
                        <div class="row align-items-center justify-content-end">
                            <asp:Label ID="lblTotal" runat="server" Text="Total:" CssClass="col-form-label col-sm-2 text-end" />
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtTotal" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer d-flex justify-content-between">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-primary" OnClick="btnRegresar_Click1" />
                <div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary me-2" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClick="btnEnviar_Click" />
                </div>
            </div>
        </div>
    </div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <!-- Modal para búsqueda de productos -->
    <div class="modal fade" id="form-modal-producto" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Búsqueda de Productos</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="container row pb-3 pt-3">
                                <div class="row align-items-center">
                                    <div class="col-auto">
                                        <asp:Label CssClass="form-label" runat="server" Text="Ingresar nombre del producto:"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox CssClass="form-control" ID="txtNombreProductoModal" runat="server" OnTextChanged="txtNombreProductoModal_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
<asp:LinkButton
    ID="BuscarProductoModal"
    runat="server"
    CssClass="btn btn-info"
    OnClick="lbBusquedaProductoModal_Click"
    style="color: white;">
    <i class='fa fa-search' style="color: white;"></i> Buscar
</asp:LinkButton>                                    </div>
                                </div>
                            </div>
                            <div class="container row">
                                <asp:GridView ID="gvProductos" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="gvProductos_PageIndexChanging" OnRowDataBound="gvProductos_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Nombre del Producto" DataField="nombre" />
                                         <asp:TemplateField HeaderText="Contenido">
            <ItemTemplate>
                <%# Eval("capacidad") + " " + Eval("unidadDeMedida") %>
            </ItemTemplate>
        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Precio Unit" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton class="btn btn-success" runat="server" Text="Seleccionar" OnClick="btnSeleccionarProductoModal_Click" CommandArgument='<%# Eval("idProductoNumerico") %>' />
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

    <!-- Modal para agregar destinatario -->
    <div class="modal fade" id="form-modal-enviar" tabindex="-1">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Agregar destinatario</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container row pb-3 pt-3">
                            <div class="row align-items-center">
                                <div class="col-auto">
                                    <asp:Label CssClass="form-label" runat="server" Text="Ingresar destinatario:"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox CssClass="form-control" ID="txtCorreo" runat="server" onkeydown="if (event.keyCode == 13) return false;" OnTextChanged="txtCorreo_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="reqCorreo" 
                                        runat="server" 
                                        ControlToValidate="txtCorreo"
                                        ErrorMessage="El campo de correo electrónico es obligatorio."
                                        Display="Dynamic" 
                                        ForeColor="Red" 
                                        SetFocusOnError="true"
                                        ValidationGroup="CorreoGroup">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="regexCorreo" 
                                        runat="server" 
                                        ControlToValidate="txtCorreo"
                                        ErrorMessage="Por favor, ingrese un correo electrónico válido."
                                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" 
                                        Display="Dynamic" 
                                        ForeColor="Red" 
                                        SetFocusOnError="true"
                                        ValidationGroup="CorreoGroup">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton 
                                        ID="EnviarModal" 
                                        runat="server" 
                                        CssClass="btn btn-info" 
                                        Text="<i class='fas fa-paper-plane' style='color:white'></i> <span style='color:white'>Enviar</span>" 
                                        OnClick="lbEnviaroModal_Click" 
                                        ValidationGroup="CorreoGroup">
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>

</asp:Content>
