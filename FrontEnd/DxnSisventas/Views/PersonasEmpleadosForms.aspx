<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PersonasEmpleadosForms.aspx.cs" Inherits="DxnSisventas.Views.PersonasEmpleadosForms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio > Personas > Empleados > Agregar / Editar</p>
    <hr>
  </div>
  <div class="container">
    <div class="card mb-3">
      <div class="card-header">
        <h3>Información del empleado</h3>
      </div>
      <div class="card-body">
        <div class="mb-3 row">
          <label class="col-sm-2 col-form-label">Id Empleado</label>
          <div class="col-sm-4">
            <asp:TextBox ID="TxtId" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
          </div>
          <label class="col-sm-2 col-form-label">DNI</label>
          <div class="col-sm-4">
            <asp:TextBox ID="TxtDNI" runat="server" Enabled="true" CssClass="form-control" onblur="validateTxtDNI()"></asp:TextBox>
            <p id="errMsgDNI" style="color: red; display: none;"></p>
          </div>
        </div>

        <div class="mb-3 row">
          <label class="col-sm-2 col-form-label">Nombre</label>
          <div class="col-sm-4">
            <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" onblur="validateTxtNombre()"></asp:TextBox>
            <p id="errMsgNombre" style="color: red; display: none;"></p>
          </div>
          <label class="col-sm-2 col-form-label">Apellido Paterno</label>
          <div class="col-sm-4">
            <asp:TextBox ID="TxtApellidoPat" runat="server" CssClass="form-control" onblur="validateTxtApellidoPat()"></asp:TextBox>
            <p id="errMsgApellidoPat" style="color: red; display: none;"></p>
          </div>
        </div>

        <div class="mb-3 row">
          <label class="col-sm-2 col-form-label">Rol</label>
          <div class="col-sm-4">
            <asp:DropDownList ID="DropDownListRoles" runat="server" CssClass="form-select">
              <asp:ListItem Text="Repartidor" Value="Repartidor"></asp:ListItem>
              <asp:ListItem Text="Encargado de Ventas" Value="Encargado de Ventas" />
              <asp:ListItem Text="Encargado de Almacen" Value="Encargado de Almacen" />
              <asp:ListItem Text="Administrador" Value="Administrador" />
            </asp:DropDownList>
          </div>
          <label class="col-sm-2 col-form-label">Apellido Materno</label>
          <div class="col-sm-4">
            <asp:TextBox ID="TxtApellidoMat" runat="server" CssClass="form-control" onblur="validateTxtApellidoMat()"></asp:TextBox>
            <p id="errMsgApellidoMat" style="color: red; display: none;"></p>
          </div>
        </div>

        <div class="mb-3 row">
          <label class="col-sm-2 col-form-label">Sueldo</label>
          <div class="col-sm-4">
            <asp:TextBox ID="TxtSueldo" runat="server" type="number" step="any" CssClass="form-control" onblur="validateTxtSueldo()"></asp:TextBox>
            <p id="errMsgSueldo" style="color: red; display: none;"></p>
          </div>
        </div>
      </div>
      <div class="card-footer clearfix">
        <!-- Button para regresar -->
        <asp:Button ID="BtnRegresar" runat="server" Text="Regresar" UseSubmitBehavior="false"
          CssClass="float-start btn btn-secondary" OnClick="BtnRegresar_Click" />
        <!-- Button para guardar -->
        <asp:Button ID="BtnGuardar" runat="server" Text="Guardar"
          CssClass="float-end btn btn-primary" OnClick="BtnGuardar_Click" OnClientClick="return validarFormulario();" />
      </div>
    </div>
  </div>
  <script type="text/javascript" src="/CustomScripts/PersonasEmpleadosForms.js"></script>
</asp:Content>
