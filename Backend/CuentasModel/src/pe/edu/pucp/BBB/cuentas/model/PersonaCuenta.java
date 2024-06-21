/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.cuentas.model;

import pe.edu.pucp.BBB.personas.model.Persona;

/**
 *
 * @author Candi
 */
public class PersonaCuenta {
  private Persona persona;
  private Cuenta cuenta;
  
  public PersonaCuenta(){
    this.persona = null;
    this.cuenta = null;
  }
  
  public PersonaCuenta(Persona persona, Cuenta cuenta){
    this.persona = persona;
    this.cuenta = cuenta;
  }
  
  public Persona getPersona() {
    return persona;
  }

  public void setPersona(Persona persona) {
    this.persona = persona;
  }

  public Cuenta getCuenta() {
    return cuenta;
  }

  public void setCuenta(Cuenta cuenta) {
    this.cuenta = cuenta;
  }
  
}
