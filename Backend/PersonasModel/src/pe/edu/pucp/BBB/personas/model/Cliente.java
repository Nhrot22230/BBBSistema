/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.personas.model;

import java.util.ArrayList;

/**
 *
 * @author Candi
 */
public class Cliente extends Persona {

  private int idNumerico;
  private String idCadena;
  private int puntos;
  private int puntosRetenidos;
  private Cliente patrocinador;
  private ArrayList<Cliente> patrocinados;
  private String RUC;
  private String razonSocial;
  private String direccion;

  //Constructores
  public Cliente() {
    this.patrocinados = new ArrayList<>();
  }

  public int getIdNumerico() {
    return idNumerico;
  }

  public void setIdNumerico(int idNumerico) {
    this.idNumerico = idNumerico;
  }

  public String getIdCadena() {
    return idCadena;
  }

  public void setIdCadena(String idCadena) {
    this.idCadena = idCadena;
  }
  

  public int getPuntos() {
    return puntos;
  }

  public void setPuntos(int puntos) {
    this.puntos = puntos;
  }

  public int getPuntosRetenidos() {
    return puntosRetenidos;
  }

  public void setPuntosRetenidos(int puntosRetenidos) {
    this.puntosRetenidos = puntosRetenidos;
  }

  public Cliente getPatrocinador() {
    return patrocinador;
  }

  public void setPatrocinador(Cliente patrocinador) {
    this.patrocinador = patrocinador;
  }

  public ArrayList<Cliente> getPatrocinados() {
    return patrocinados;
  }

  public void setPatrocinados(ArrayList<Cliente> patrocinados) {
    this.patrocinados = patrocinados;
  }

  public String getRUC() {
    return RUC;
  }

  public void setRUC(String RUC) {
    this.RUC = RUC;
  }

  public String getRazonSocial() {
    return razonSocial;
  }

  public void setRazonSocial(String razonSocial) {
    this.razonSocial = razonSocial;
  }

  public String getDireccion() {
    return direccion;
  }

  public void setDireccion(String direccion) {
    this.direccion = direccion;
  }

  public void agregarPatrocinado(Cliente cliente) {
    patrocinados.add(cliente);
  }
}
