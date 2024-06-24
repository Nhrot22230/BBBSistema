<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrdenVenta.aspx.cs" Inherits="DxnSisventas.Views.OrdenVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CustomStyles/OrdenVenta.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-path">
        <p><i class="fa fa-home"></i>> Inicio > Documentos > Ordenes > OrdenVenta</p>
        <hr>
    </div>
    <div class="container">
        <div class="row mb-3">
            <h1>Registro de Ordenes de Venta</h1>
        </div>
        <div class="row mb-3">
            <div class="col-md-4">
                <!-- Panel de búsqueda -->
                <div class="input-group">
                    <asp:TextBox ID="TxtBuscar" runat="server" AutoPostBack="true" 
                        CssClass="form-control" placeholder="Buscar por ID" OnTextChanged="TxtBuscar_TextChanged"></asp:TextBox>
                    <asp:LinkButton ID="BtnBuscar" runat="server" Text="<i class='fas fa-search'> </i>"
                        CssClass="btn btn-secondary" OnClick="BtnBuscar_Click" />
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <!-- Filtro por Fecha -->
            <div class="col-sm-3">
                <label class="col-form-label">Filtro por Fecha</label>
                <div class="input-group">
                    <asp:TextBox ID="FechaInicio" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaInicio_TextChanged"></asp:TextBox>
                    <span class="input-group-text">-</span>
                    <asp:TextBox ID="FechaFin" runat="server" CssClass="form-control" type="date" AutoPostBack="true" OnTextChanged="FechaFin_TextChanged"></asp:TextBox>
                </div>
            </div>

            <!-- Ordenar por Fecha -->
            <div class="col-sm-3">
                <label class="col-form-label">Ordenar por Fecha</label>
                <asp:DropDownList ID="OrdenarPorFecha" runat="server" CssClass="form-select" AutoPostBack="true"
                    OnSelectedIndexChanged="OrdenarPorFecha_SelectedIndexChanged">
                    <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                    <asp:ListItem Text="Mas antiguos primero" Value="asc"></asp:ListItem>
                    <asp:ListItem Text="Mas recientes primero" Value="desc"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- Estado -->
            <div class="col-sm-3">
                <label class="col-form-label">Estado</label>
                <asp:DropDownList ID="Estado" runat="server" CssClass="form-select" AutoPostBack="true"
                    OnSelectedIndexChanged="Estado_SelectedIndexChanged">
                    <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                    <asp:ListItem Text="Entregado" Value="Entregado" > </asp:ListItem>
                    <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                    <asp:ListItem Text="Cancelado" Value="Cancelado"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row mb-3">
            <!-- Filtro por Montos -->
            <div class="col-sm-3">
                <label class="col-form-label">Filtro por Montos</label>
                <div class="input-group">
                    <asp:TextBox runat="server" type="number" class="form-control rounded-start no-arrows" ID="TxtMontoMin" AutoPostBack="true"
                        placeholder="Min" MaxLength="10" OnTextChanged="TxtMontoMin_TextChanged"></asp:TextBox>
                    <span class="input-group-text">-</span>
                    <asp:TextBox runat="server" type="number" class="form-control rounded-end no-arrows" ID="TxtMontoMax" AutoPostBack="true"
                        placeholder="Max" MaxLength="10" OnTextChanged="TxtMontoMax_TextChanged"></asp:TextBox>
                </div>
            </div>
            <!-- Ordenar por Monto -->
            <div class="col-sm-3">
                <label class="col-form-label">Ordenar por Monto</label>
                <asp:DropDownList ID="OrdenarPorMonto" runat="server" CssClass="form-select" AutoPostBack="true"
                    OnSelectedIndexChanged="OrdenarPorMonto_SelectedIndexChanged">
                    <asp:ListItem Text="Ninguno" Value="todos"></asp:ListItem>
                    <asp:ListItem Text="Menor a Mayor" Value="asc"></asp:ListItem>
                    <asp:ListItem Text="Mayor a Menor" Value="desc"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="row mb-3">
            <!-- Botón Nuevo -->
            <div class="col-sm-3">
                <asp:LinkButton ID="BtnAgregar" runat="server" OnClick="BtnAgregar_Click" CssClass="btn btn-primary btn-sm">
                    <i class="fas fa-plus pe-2"></i> Nuevo
                </asp:LinkButton>
            </div>
            <!-- Botón Limpiar Filtros -->
            <div class="col align-self-end text-end">
                <asp:LinkButton ID="BtnLimpiar" runat="server" OnClick="BtnLimpiar_Click" CssClass="btn btn-link text-decoration-none small" Style="color: #000;">
                    <i class="fas fa-times-circle"></i> Limpiar Filtros
                </asp:LinkButton>
            </div>
        </div>
        <div class="container row ">
            <asp:GridView ID="GridVentas" runat="server" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="GridVentas_PageIndexChanging"
                CssClass="table table-hover table-responsive table-striped" OnRowDataBound="GridVentas_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Id" />
                    <asp:BoundField HeaderText="Fecha creación" />
                    <asp:BoundField HeaderText="Cliente" />
                    <asp:BoundField HeaderText="EncargadoVenta" />
                    <asp:BoundField HeaderText="Repartidor" />
                    <asp:BoundField HeaderText="Fecha de entrega" />
                    <asp:BoundField HeaderText="Monto" />
                    <asp:BoundField HeaderText="Estado" />
                    <asp:BoundField HeaderText="Descuento" />
                    
                    <asp:TemplateField HeaderText="Opciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnVisualizar" runat="server" Text="<i class='fas fa-eye ps-2'>  </i>"
                                OnClick="BtnVisualizar_Click" />

                            <asp:LinkButton ID="BtnEditar" runat="server" Text="<i class='fas fa-edit'></i>"
                                OnClick="BtnEditar_Click">
                            </asp:LinkButton>


                            <asp:LinkButton ID="BtnEliminar" 
                                Onclick="BtnEliminar_Click"
                                runat="server" 
                                Text="<i class='fas fa-solid fa-trash ps-2'></i>">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>






    </div>
</asp:Content>
