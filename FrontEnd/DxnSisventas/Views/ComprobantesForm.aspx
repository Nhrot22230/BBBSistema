<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ComprobantesForm.aspx.cs" Inherits="DxnSisventas.Views.ComprobantesForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CustomStyles/Comprobante.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2> Registro de Comprobante
                </h2>
            </div>
            <div class="card-body">
                <div class="card mb-3">
                    <div class="card-header">
                        <h5>Información del Comprobante</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3 row">
                            <label for="TxtId" class="col-sm-2 col-form-label">ID Comprobante</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TxtId" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>

                            <label for="TxtFecha" class="col-sm-2 col-form-label">Fecha</label>
                            <div class="col-sm-auto">
                                <asp:TextBox ID="TxtFechaComprobante" runat="server" Enabled="false" CssClass="form-control" onblur="validateFechaComprobante()"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="TxtTipoComprobante" class="col-sm-2 col-form-label">Tipo Comprobante</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListTipoComprobante" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Boleta" Value="BoletaSimple"></asp:ListItem>
                                    <asp:ListItem Text="Factura" Value="Factura"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="TxtTotal" class="col-sm-2 col-form-label">Total</label>
                            <div class="col-sm-auto">
                                <asp:TextBox ID="TxtTotal" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-header">
                        <h5>Información de la Orden Asociada</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3 row">
                            <div class="col-sm-4">
                                <asp:Label ID="LbOrden" CssClass="form-label" runat="server" Text="Nro. Orden:"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="TxtIdOrden" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="BtnBuscar" runat="server"
                                        CssClass="btn btn-secondary" Text="<i class='fas fa-solid fa-search pe-2'></i> Buscar" 
                                        OnClick="BtnBuscar_Click" >
                                    </asp:LinkButton>
                                </div>
                                <div id="ordenAsociadaErrorMessage" style="display: none; color: red;"></div>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label CssClass="form-label" runat="server" Text="Tipo de orden:"></asp:Label>
                                <asp:DropDownList ID="DropDownListTipoOrden" AutoPostBack="true" runat="server" CssClass="form-select" OnSelectedIndexChanged="DropDownListTipoOrden_SelectedIndexChanged">
                                    <asp:ListItem Text="Compra" Value="Compra"></asp:ListItem>
                                    <asp:ListItem Text="Venta" Value="Venta"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                                <div class="col-sm-2"></div>
                             
                             <div class="col-sm-4">
                                 <asp:Label ID="LbFechaOrden" CssClass="form-label" runat="server" Text="Fecha:"></asp:Label>
                                 <asp:TextBox ID="TxtFechaOrden" runat="server" Enabled="false" CssClass="form-control" onblur="validateOrdenAsociada()"></asp:TextBox>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            <div class="card-footer clearfix">
                <asp:Button ID="BtnRegresar" runat="server" Text="Regresar"
                    CssClass="float-start btn btn-secondary" OnClick="BtnRegresar_Click" />
                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar"
                    CssClass="float-end btn btn-primary" OnClick="BtnGuardar_Click" OnClientClick="return validarFormularioComprobante();"/>
                <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" CssClass="float-end btn btn-primary" OnClick="BtnEnviar_Click"/>
            </div>
        </div>
    </div>

        <%-- Modal para agregar ordenes --%>
    
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div id="form-modal-ordenes" class="modal">
        <asp:Panel id="PanelOrdenes" runat="server" CssClass="panel-class" DefaultButton="BtnBuscarModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Ordenes</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container row">
                        <div class="col-md-6">
                            <asp:Label CssClass="form-label" runat="server" Text="Ingresar código de la orden:"></asp:Label>
                            <div class="input-group mb-3">
                                    <asp:TextBox CssClass="form-control" ID="txtCodOrdenModal" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="BtnBuscarModal" runat="server" CssClass="btn btn-secondary" Text="<i class='fas fa-solid fa-search pe-2'></i> Buscar" OnClick="BtnBuscarModal_Click" />
                            </div>
                        </div>
                        <div class="col-md-3">
                             <asp:Label CssClass="form-label" runat="server" Text="Tipo de orden:"></asp:Label>
                            <asp:DropDownList ID="DropDownListTipoOrdenModal" AutoPostBack="true" runat="server" CssClass="form-select" OnSelectedIndexChanged="DropDownListTipoOrdenModal_SelectedIndexChanged">
                                <asp:ListItem Text="Compra" Value="Compra"></asp:ListItem>
                                <asp:ListItem Text="Venta" Value="Venta"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row overflow-x-scroll">
                        <asp:GridView ID="gvOrdenes" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="gvOrdenes_PageIndexChanging" OnRowDataBound="gvOrdenes_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="" DataField="idOrden" Visible="false" />
                                <%-- Estamos enlazando de otra manera a traves del evento OnRowDataBound --%>
                                <asp:BoundField HeaderText="ID Orden"/>
                                <asp:BoundField HeaderText="Fecha Creacion"/>
                                <asp:BoundField HeaderText="Hora Creacion"/>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton class="btn btn-success" runat="server" Text="<i class='fas fa-solid fa-check ps-2'></i> Seleccionar" OnClick="BtnSeleccionarOrdenModal_Click" CommandArgument='<%# Eval("idOrden") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
        </asp:Panel>
    </div>

       <!-- Modal para agregar destinatario -->
    <div class="modal fade" id="form-modal-enviar" tabindex="-1">
        <asp:Panel id="PanelEnviarDestinatario" runat="server" CssClass="panel-class" DefaultButton="EnviarModal">
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
                                            <asp:TextBox CssClass="form-control" ID="txtCorreo" runat="server"></asp:TextBox>
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
                                                CssClass="btn btn-primary" 
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
        </asp:Panel>
    </div>
    <script type="text/javascript" src="/CustomScripts/Documentos.js"></script>
</asp:Content>
