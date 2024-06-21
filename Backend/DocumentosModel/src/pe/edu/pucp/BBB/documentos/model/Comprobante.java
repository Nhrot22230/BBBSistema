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
public class Comprobante {
  private int idComprobanteNumerico;
  private String idComprobanteCadena;
  private TipoComprobante tipoComprobante;
  private Orden ordenAsociada;
  private Date fechaEmision;

  public int getIdComprobanteNumerico() {
    return idComprobanteNumerico;
  }

  public void setIdComprobanteNumerico(int idComprobanteActual) {
    this.idComprobanteNumerico = idComprobanteActual;
  }

  public String getIdComprobanteCadena() {
    return idComprobanteCadena;
  }

  public void setIdComprobanteCadena(String idComprobante) {
    this.idComprobanteCadena = idComprobante;
  }

  public TipoComprobante getTipoComprobante() {
    return tipoComprobante;
  }

  public void setTipoComprobante(TipoComprobante tipoComprobante) {
    this.tipoComprobante = tipoComprobante;
  }

  public Orden getOrdenAsociada() {
    return ordenAsociada;
  }

  public void setOrdenAsociada(Orden ordenAsociada) {
    this.ordenAsociada = ordenAsociada;
  }

  public Date getFechaEmision() {
    return fechaEmision;
  }

  public void setFechaEmision(Date fechaEmision) {
    this.fechaEmision = fechaEmision;
  }
}
