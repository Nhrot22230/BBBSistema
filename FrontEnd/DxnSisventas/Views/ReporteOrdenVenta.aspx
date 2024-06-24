<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReporteOrdenVenta.aspx.cs" Inherits="DxnSisventas.Views.ReporteOrdenVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
  <div><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></div>
  <iframe title="reporteAlmacen" id="pdfFrame" runat="server" style="height: 70vh; width:800px;"></iframe>
</div>
</asp:Content>
