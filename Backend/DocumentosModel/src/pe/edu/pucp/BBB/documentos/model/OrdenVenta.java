/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.model;

import java.util.Date;
import pe.edu.pucp.BBB.personas.model.Cliente;
import pe.edu.pucp.BBB.personas.model.Empleado;

/**
 *
 * @author Candi
 */
public class OrdenVenta extends Orden {

  private int idOrdenVentaNumerico;
  private String idOrdenVentaCadena;
  private Empleado encargadoVenta;
  private Cliente cliente;
  private TipoVenta tipoVenta;
  private Empleado repartidor;
  private double porcentajeDescuento;
  private MetodoPago metodoPago;
  private Date fechaEntrega;

  public int getIdOrdenVentaNumerico() {
    return idOrdenVentaNumerico;
  }

  public void setIdOrdenVentaNumerico(int idOrdenVentaActual) {
    this.idOrdenVentaNumerico = idOrdenVentaActual;
  }

  public String getIdOrdenVentaCadena() {
    return idOrdenVentaCadena;
  }

  public void setIdOrdenVentaCadena(String idOrdenVenta) {
    this.idOrdenVentaCadena = idOrdenVenta;
  }

  public Empleado getEncargadoVenta() {
    return encargadoVenta;
  }

  public void setEncargadoVenta(Empleado encargadoVenta) {
    this.encargadoVenta = encargadoVenta;
  }

  public Cliente getCliente() {
    return cliente;
  }

  public void setCliente(Cliente cliente) {
    this.cliente = cliente;
  }

  public TipoVenta getTipoVenta() {
    return tipoVenta;
  }

  public void setTipoVenta(TipoVenta tipoVenta) {
    this.tipoVenta = tipoVenta;
  }

  public Empleado getRepartidor() {
    return repartidor;
  }

  public void setRepartidor(Empleado repartidor) {
    this.repartidor = repartidor;
  }

  public double getPorcentajeDescuento() {
    return porcentajeDescuento;
  }

  public void setPorcentajeDescuento(double porcentajeDescuento) {
    this.porcentajeDescuento = porcentajeDescuento;
  }

  public MetodoPago getMetodoPago() {
    return metodoPago;
  }

  public void setMetodoPago(MetodoPago metodoPago) {
    this.metodoPago = metodoPago;
  }

  public Date getFechaEntrega() {
    return fechaEntrega;
  }

  public void setFechaEntrega(Date fechaEntrega) {
    this.fechaEntrega = fechaEntrega;
  }
}
