/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.BBB.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import pe.edu.pucp.BBB.correo.access.EnvioCorreos;
import pe.edu.pucp.BBB.correo.dao.CorreoDAO;

/**
 *
 * @author Candi
 */
@WebService(serviceName = "CorreosAPI", targetNamespace = "http://services.webservice.BBB.pucp.edu.pe/")
public class CorreosAPI {

  private CorreoDAO daoCorreo;

  /**
   * This is a sample web service operation
   */

  public CorreosAPI() {
    daoCorreo = new EnvioCorreos();
  }

  @WebMethod(operationName = "enviarCorreoWeb")
  public int enviarCorreoWeb(@WebParam(name = "asunto") String asunto,
    @WebParam(name = "contenido") String contenido, @WebParam(name = "correo") String correo, @WebParam(name = "ruta") byte[] pdfBytes) {
    int resultado = 0;

    try {
      resultado=daoCorreo.enviarCorreo(asunto, contenido, correo, pdfBytes);
      return resultado;
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
      
    }

    return resultado;
  }
  @WebMethod(operationName = "enviarCorreoWebSinArchivo")
  public int enviarCorreoWebSinArchivo(@WebParam(name = "asunto") String asunto,
    @WebParam(name = "contenido") String contenido, @WebParam(name = "correo") String correo) {
    int resultado = 0;

    try {
      resultado=daoCorreo.enviarCorreo(asunto, contenido, correo);
      return resultado;
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }

    return resultado;
  }
}
