<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DxnSisventas.Views.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding-bottom: 56.25%; position: relative; display: block; width: 100%">
    <div>
      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></div>
    <iframe 
      title="reporteAlmacen" 
      id="pdfFrame" 
      runat="server" 
      class="mx-auto my-auto"
      style="position: absolute; height: 70vh; width: 800px;"
      frameborder="0" 
      allowfullscreen="">
    </iframe>
  </div>
</asp:Content>
