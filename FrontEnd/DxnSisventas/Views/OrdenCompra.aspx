<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="DxnSisventas.Views.OrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/CustomScripts/Documentos.js"></script>
    <link href="../CustomStyles/OrdenCompra.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-path">
        <p><i class="fa fa-home"></i>> Inicio > Documentos > Ordenes > OrdenCompra</p>
        <hr>
    </div>
    <div class="container">
        <div class="row">
            <h1>Registro de Ordenes de Compra</h1>
        </div>
        
        <!-- Filtros de búsqueda -->
        <div class="search-container">
            <div class="search-row">
                <!-- Búsqueda por Número de Orden -->
                <div class="search-item col-sm-3">
                    <label class="col-form-label">
                        <i class="fas fa-filter"></i>Buscar Nro. Orden
                    </label>
                    <div class="input-group">
                        <asp:TextBox ID="TxtBuscar" runat="server" type="number" AutoPostBack="true" CssClass="form-control no-arrows text-left" placeholder="Nro. de Orden" OnTextChanged="TxtBuscar_TextChanged"></asp:TextBox>
                        <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'></i>" CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
                    </div>
                </div>
                <!-- Filtro por Fecha -->
                <div class="search-item col-sm-3">
                    <label class="col-form-label"><i class="fas fa-filter"></i>Filtro por Fecha</label>
                    <div class="input-group">
                        <asp:TextBox ID="FechaInicio" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaInicio_TextChanged"></asp:TextBox>
                        <span class="input-group-text">-</span>
                        <asp:TextBox ID="FechaFin" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaFin_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <!-- Filtro por Montos -->
                <div class="search-item col-sm-3">
                    <label class="col-form-label"><i class="fas fa-filter"></i>Filtro por Montos</label>
                    <div class="input-group">
                        <asp:TextBox runat="server" type="number" class="form-control rounded-start no-arrows" ID="TxtMontoMin" AutoPostBack="true" placeholder="Min" MaxLength="10" OnTextChanged="TxtMontoMin_TextChanged"></asp:TextBox>
                        <span class="input-group-text">-</span>
                        <asp:TextBox runat="server" type="number" class="form-control rounded-end no-arrows" ID="TxtMontoMax" AutoPostBack="true" placeholder="Max" MaxLength="10" OnTextChanged="TxtMontoMax_TextChanged"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="search-row">
                <!-- Estado -->
                <div class="search-item col-sm-3">
                    <label class="col-form-label"><i class="fas fa-filter"></i>Estado</label>
                    <asp:DropDownList ID="Estado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Estado_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                        <asp:ListItem Text="Entregado" Value="Entregado"></asp:ListItem>
                        <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <!-- Ordenar por Fecha -->
                <div class="search-item col-sm-3">
                    <label class="col-form-label"><i class="fas fa-solid fa-sort"></i>Ordenar por Fecha</label>
                    <asp:DropDownList ID="OrdenarPorFecha" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="OrdenarPorFecha_SelectedIndexChanged">
                        <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                        <asp:ListItem Text="Mas antiguos primero" Value="asc"></asp:ListItem>
                        <asp:ListItem Text="Mas recientes primero" Value="desc"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <!-- Ordenar por Monto -->
                <div class="search-item col-sm-3">
                    <label class="col-form-label"><i class="fas fa-solid fa-sort"></i>Ordenar por Monto</label>
                    <asp:DropDownList ID="OrdenarPorMonto" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="OrdenarPorMonto_SelectedIndexChanged">
                        <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                        <asp:ListItem Text="Menor a Mayor" Value="asc"></asp:ListItem>
                        <asp:ListItem Text="Mayor a Menor" Value="desc"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="search-row">
                <!-- Botón Nuevo -->
                <div class="search-item">
                    <asp:LinkButton ID="BtnAgregar" runat="server" OnClick="BtnAgregar_Click" CssClass="btn btn-primary btn-sm">
                        <i class="fas fa-plus pe-2"></i> Nuevo
                    </asp:LinkButton>
                </div>
                <!-- Botón Limpiar Filtros -->
                <div class="search-item text-end">
                    <asp:LinkButton ID="BtnLimpiar" runat="server" OnClick="BtnLimpiar_Click" CssClass="btn btn-link text-decoration-none small" Style="color: #000;">
                        <i class="fas fa-times-circle"></i> Limpiar Filtros
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <!-- Gridview para mostrar los registros de la tabla -->
        <div class="container row">
            <asp:GridView ID="GridCompras" runat="server" AutoGenerateColumns="false" 
                AllowPaging="true" PageSize="5" OnPageIndexChanging="GridCompras_PageIndexChanging" 
                CssClass="table table-hover table-responsive table-striped" OnSelectedIndexChanged="GridCompras_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="idOrdenCompraCadena" HeaderText="Id" />
                    <asp:BoundField DataField="fechaCreacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:C}" HtmlEncode="false" />
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnEditar" runat="server" Text='<%# Eval("estado").ToString() == "Entregado" ? "Ver <i class=\"fas fa-eye ps-2\"></i>" : "Editar <i class=\"fas fa-edit ps-2\"></i>" %>' OnClick="BtnEditar_Click" CommandArgument='<%# Eval("idOrdenCompraNumerico") %>' CssClass="link-button" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
