/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.model;

import java.util.Date;

/**
 *
 * @author Candi
 */
public class OrdenCompra extends Orden {
  private int idOrdenCompraNumerico;
  private String idOrdenCompraCadena;
  private Date fechaRecepcion;

  public int getIdOrdenCompraNumerico() {
    return idOrdenCompraNumerico;
  }

  public void setIdOrdenCompraNumerico(int idOrdenCompraActual) {
    this.idOrdenCompraNumerico = idOrdenCompraActual;
  }

  public String getIdOrdenCompraCadena() {
    return idOrdenCompraCadena;
  }

  public void setIdOrdenCompraCadena(String idOrdenCompra) {
    this.idOrdenCompraCadena = idOrdenCompra;
  }

  public Date getFechaRecepcion() {
    return fechaRecepcion;
  }

  public void setFechaRecepcion(Date fechaRecepcion) {
    this.fechaRecepcion = fechaRecepcion;
  }
}