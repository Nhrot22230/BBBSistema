/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.model;

import java.util.ArrayList;
import java.util.Date;

/**
 *
 * @author Candi
 */
public class Orden {

  private int idOrden;
  private ArrayList<LineaOrden> lineasOrden;
  private EstadoOrden estado;
  private Date fechaCreacion;
  private double total;
  
  public Orden(){
    lineasOrden = new ArrayList<>();
  }
  
  public int getIdOrden() {
    return idOrden;
  }

  public void setIdOrden(int idOrden) {
    this.idOrden = idOrden;
  }

  public ArrayList<LineaOrden> getLineasOrden() {
    return lineasOrden;
  }

  public void setLineasOrden(ArrayList<LineaOrden> lineasOrden) {
    this.lineasOrden = lineasOrden;
  }

  public EstadoOrden getEstado() {
    return estado;
  }

  public void setEstado(EstadoOrden estado) {
    this.estado = estado;
  }

  public Date getFechaCreacion() {
    return fechaCreacion;
  }

  public void setFechaCreacion(Date fechaCreacion) {
    this.fechaCreacion = fechaCreacion;
  }

  public double getTotal() {
    return total;
  }

  public void setTotal(double total) {
    this.total = total;
  }
  
  public void agregarLineaOrden(LineaOrden lo){
    this.lineasOrden.add(lo);
  }
}
