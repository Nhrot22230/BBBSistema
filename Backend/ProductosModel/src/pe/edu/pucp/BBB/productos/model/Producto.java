/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.productos.model;

/**
 *
 * @author Candi
 */
public class Producto {

  private int idProductoNumerico;
  private String idProductoCadena;
  private String nombre;
  private int stock;
  private double capacidad;
  private UnidadMedida unidadDeMedida;
  private TipoProducto tipo;
  private double precioUnitario;
  private int puntos;

  //Constructores
  public Producto() {
  }

  public int getIdProductoNumerico() {
    return idProductoNumerico;
  }

  public void setIdProductoNumerico(int idProductoActual) {
    this.idProductoNumerico = idProductoActual;
  }

  public String getIdProductoCadena() {
    return idProductoCadena;
  }

  public void setIdProductoCadena(String idProducto) {
    this.idProductoCadena = idProducto;
  }

  public String getNombre() {
    return nombre;
  }

  public void setNombre(String nombre) {
    this.nombre = nombre;
  }

  public int getStock() {
    return stock;
  }

  public void setStock(int stock) {
    this.stock = stock;
  }

  public double getCapacidad() {
    return capacidad;
  }

  public void setCapacidad(double capacidad) {
    this.capacidad = capacidad;
  }

  public UnidadMedida getUnidadDeMedida() {
    return unidadDeMedida;
  }

  public void setUnidadDeMedida(UnidadMedida unidadDeMedida) {
    this.unidadDeMedida = unidadDeMedida;
  }

  public TipoProducto getTipo() {
    return tipo;
  }

  public void setTipo(TipoProducto tipo) {
    this.tipo = tipo;
  }

  public double getPrecioUnitario() {
    return precioUnitario;
  }

  public void setPrecioUnitario(double precioUnitario) {
    this.precioUnitario = precioUnitario;
  }

  public int getPuntos() {
    return puntos;
  }

  public void setPuntos(int puntos) {
    this.puntos = puntos;
  }
}
