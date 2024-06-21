/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.personas.model;

/**
 *
 * @author Candi
 */
public class Empleado extends Persona {

  private int idEmpleadoNumerico;
  private String idEmpleadoCadena;
  private double sueldo;
  private boolean empleadoActivo;
  private Rol rol;

  // Constructores
  public Empleado() {
  }

  public int getIdEmpleadoNumerico() {
    return idEmpleadoNumerico;
  }

  public void setIdEmpleadoNumerico(int idEmpleadoActual) {
    this.idEmpleadoNumerico = idEmpleadoActual;
  }

  public String getIdEmpleadoCadena() {
    return idEmpleadoCadena;
  }

  public void setIdEmpleadoCadena(String idEmpleado) {
    this.idEmpleadoCadena = idEmpleado;
  }

  public double getSueldo() {
    return sueldo;
  }

  public void setSueldo(double sueldo) {
    this.sueldo = sueldo;
  }

  public boolean isEmpleadoActivo() {
    return empleadoActivo;
  }

  public void setEmpleadoActivo(boolean empleadoActual) {
    this.empleadoActivo = empleadoActual;
  }

  public Rol getRol() {
    return rol;
  }

  public void setRol(Rol rol) {
    this.rol = rol;
  }
}
