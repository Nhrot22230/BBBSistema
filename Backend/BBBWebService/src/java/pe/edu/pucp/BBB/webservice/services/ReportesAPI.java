/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.BBB.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import pe.edu.pucp.BBB.reportes.manager.ReporteManager;

/**
 *
 * @author Candi
 */
@WebService(serviceName = "ReportesAPI", targetNamespace="http://services.webservice.BBB.pucp.edu.pe/")
public class ReportesAPI {

  /**
   * This is a sample web service operation
   * @param txt
   * @return 
   */
  @WebMethod(operationName = "hello")
  public String hello(@WebParam(name = "name") String txt) {
    return "Hello " + txt + " !";
  }
  
  @WebMethod(operationName = "generarReporteAlmacen")
  public byte[] generarReporteAlmacen() {
    byte [] file = null;
    
    String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/BBB/webservice/templates/Simple_Blue.jrxml").getPath();
    template_path = template_path.replace("%20", " ");
    
    try {
      file = ReporteManager.generarReporteAlmacen(template_path);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return file;
  }
  
  @WebMethod(operationName = "imprimirComprobante")
  public byte[] imprimirComprobante(int idComprobanteNumerico) {
    byte [] file = null;
    
    String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/BBB/webservice/templates/Comprobante.jrxml").getPath();
    template_path = template_path.replace("%20", " ");
    
    try {
      file = ReporteManager.imprimirComprobante(template_path, idComprobanteNumerico);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    
    return file;
  }
   @WebMethod(operationName = "generarReporteOrdenCompra")
  public byte[] generarReporteOrdenCompra(@WebParam(name = "id") int id) {
    byte [] file = null;
    
    String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/BBB/webservice/templates/OrdenCompraReport.jrxml").getPath();
    template_path = template_path.replace("%20", " ");
    
    try {
      file = ReporteManager.imprimirComprobante(template_path,id);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return file;
  }
  
    @WebMethod(operationName = "reporteOrdenVentaPDF")
    public byte[] reporteOrdenVenta() throws Exception {
        
        byte [] file = null;
    
        String template_path = ReportesAPI.class.getResource("/pe/edu/pucp/BBB/webservice/templates/OrdenesVentas.jrxml").getPath();
        template_path = template_path.replace("%20", " ");

        try {
          file = ReporteManager.generarReporteAlmacen(template_path);
        } catch (Exception ex){
          System.err.println(ex.getMessage());
        }
        return file;
   
    }
    
  
}
