<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DxnSisventas.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" href="CustomStyles/Home.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <!-- show path like (Home icon) > ParentPage > CurrentPage -->
  <!-- show path like (Home icon) > Inicio -->
  <div class="page-path">
    <p><i class="fa fa-home"></i>> Inicio</p>
    <hr>
  </div>

  <div class="container mt-5">
    <div class="jumbotron text-center">
      <h1 class="display-4">¡Bienvenido al Sistema!</h1>
      <p class="lead"><%: emp.nombre %> <%: emp.apellidoPaterno %> <%: emp.apellidoMaterno %></p>
    </div>

    <div class="card card-custom">
      <div class="card-header card-header-custom">Datos del Empleado</div>
      <div class="card-body card-body-custom">
        <h5 class="card-title text-primary"><%: emp.nombre %> <%: emp.apellidoPaterno %> <%: emp.apellidoMaterno %></h5>
        <div class="card-text">
          <div class="info-item">
            <i class="fa fa-id-badge icon"></i>
            <strong>ID Empleado:</strong> <%: emp.idEmpleadoCadena %>
          </div>
          <div class="info-item">
            <i class="fa fa-money-bill-wave icon"></i>
            <strong>Sueldo:</strong> <%: emp.sueldo %>
          </div>
          <div class="info-item">
            <i class="fa fa-user-tag icon"></i>
            <strong>Rol:</strong> <%: emp.rol %>
          </div>
          <div class="info-item">
            <i class="fa fa-user icon"></i>
            <strong>Usuario:</strong> <%: datosCuenta.usuario %>
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
